using ClientCommunication;
using Communication;
using FlightsOptions;
using FlightsSeats;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ServiceModel;

namespace Server {

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = false)]
partial class ClientMessageService : ClientService {
	Either<Success, LoginError> checkAccountDataValid(Account c) {
		if(c.login == null) c.login = "";
		if(c.password == null) c.password = "";
		var invalidLogin = LoginRegister.checkLogin(c.login);
		var invalidPassword = LoginRegister.checkPassword(c.password);

		if(!invalidLogin.ok || !invalidPassword.ok) {
			var loginError = invalidLogin.errorMsg;
			var passError = invalidPassword.errorMsg;
			return Either<Success, LoginError>.Failure(new LoginError(loginError + "\n" + passError));
		}
		else return Either<Success, LoginError>.Success(new Success());
	}

	Parameters ClientService.parameters() {
		return new Parameters {
			flightClasses = Program.flightClasses,
			cities = Program.cities
		};
	}

	Either<List<Flight>, InputError> ClientService.findMatchingFlights(MatchingFlightsParams p) {
		var err = Validation.ErrorString.Create();
		if(p.fromCode == null) err.ac("город вылета должен быть заполнен");
		if(p.toCode == null) err.ac("город прилёта должен быть заполнен");
		if(p.when == null) err.ac("дата вылета должа быть заполнена");

		if(err) return Either<List<Flight>, InputError>.Failure(new InputError(err.Message));
		else return Either<List<Flight>, InputError>.Success(DatabaseFlights.findMatchingFlights(p, mustBeAbeToBook: true));
	}

	Either<Success, LoginError> ClientService.registerAccount(Account c) {
		var result = checkAccountDataValid(c);
		if(!result.IsSuccess) return result;

		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var registered = LoginRegister.register(new SqlConnectionView(connection, true), c.login, c.password);

		if(registered) return Either<Success, LoginError>.Success(new Success());
		else return Either<Success, LoginError>.Failure(
			new LoginError("Аккаунт с таким именем пользователя уже существует")
		);
		}
	}

	Either<Success, LoginError> ClientService.logInAccount(Account c) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var res = getUserId(new SqlConnectionView(connection, true), c);
		if(res.IsSuccess) return Either<Success, LoginError>.Success(new Success());
		else return Either<Success, LoginError>.Failure(res.f);
		}
	}

	private Either<int, LoginError> getUserId(SqlConnectionView cv, Account c) {
		using(cv) {
		var error = checkAccountDataValid(c);
		if(!error.IsSuccess) return Either<int, LoginError>.Failure(error.Failure());
		
		var loggedIn = LoginRegister.login(cv, c.login, c.password);
		cv.Dispose();

		if(loggedIn.status == LoginRegister.LoginResultStatus.USER_NOT_EXISTS) 
			 return Either<int, LoginError>.Failure(new LoginError("Пользователь с данным логином не найден"));
		else if(loggedIn.status == LoginRegister.LoginResultStatus.WRONG_PASSWORD) 
			return Either<int, LoginError>.Failure(new LoginError("Неправильный пароль"));

		return Either<int, LoginError>.Success(loggedIn.userID);
		}
	}

	Either<int, LoginOrInputError> ClientService.addPassanger(Account c, Passanger passanger) {
		var it = ValidatePassanger.validate(passanger);
		if(it.error) return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ InputError = new InputError(it.errorMsg) });

		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = getUserId(new SqlConnectionView(connection, false), c);
		if(!userIdRes.IsSuccess) return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });
		return Either<int, LoginOrInputError>.Success(
			DatabasePassanger.add(new SqlConnectionView(connection, true), userIdRes.Success(), passanger)
		);
		}
	}

	Either<int, LoginOrInputError> ClientService.replacePassanger(Account c, int index, Passanger passanger) {
		var it = ValidatePassanger.validate(passanger);
		if(it.error) return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ InputError = new InputError(it.errorMsg) });

		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = getUserId(new SqlConnectionView(connection, false), c);
		if(!userIdRes.IsSuccess) return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });

		var replacedId = DatabasePassanger.replace(new SqlConnectionView(connection, true), userIdRes.Success(), index, passanger);
		if(replacedId != null) return Either<int, LoginOrInputError>.Success((int) replacedId);
		else return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ InputError = new InputError(
			"Данный пассажир не может быть изменён, так как он уже был выбран при покупке билета"
		) });
		}
	}

	Either<Dictionary<int, Passanger>, LoginError> ClientService.getPassangers(Account c) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = getUserId(new SqlConnectionView(connection, false), c);
		if(!userIdRes.IsSuccess) return Either<Dictionary<int, Passanger>, LoginError>.Failure(userIdRes.Failure());
		return Either<Dictionary<int, Passanger>, LoginError>.Success(
			DatabasePassanger.getAll(new SqlConnectionView(connection, true), userIdRes.Success())
		);
		}
	}

	Either<Success, LoginOrInputError> ClientService.removePassanger(Account customer, int index) {
	using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
		if(!userIdRes.IsSuccess) return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });

		var result = DatabasePassanger.remove(new SqlConnectionView(connection, true), userIdRes.s, index);

		if(result.ok) return Either<Success, LoginOrInputError>.Success(new Success());
		else return Either<Success, LoginOrInputError>.Failure(
			new LoginOrInputError{ InputError = new InputError(
				result.errorMsg
			)}
		);
	}}

	private Either<Dictionary<int, Options>, InputError> extractOptions(
		SqlConnectionView cv, int availableFlightId
	) {
		using(cv) {
		using(
		var command = new SqlCommand(
			@"select top 1 
				[fi].[Options],
				[ap].[SeatsScheme]
			from (
				select * from [Flights].[AvailableFlights] as [af]
				where [af].[Id] = @AvailableFlight
			) as [af]

			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightInfo] = [fi].[Id]
			
			inner join [Flights].[Airplanes] as [ap]
			on [fi].[Airplane] = [ap].[Id]",
			cv.connection
		)) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", availableFlightId);

		cv.Open();
		using(
		var result = command.ExecuteReader()) {

		if(!result.Read()) return Either<Dictionary<int, Options>, InputError>.Failure(new InputError(
			"Выбранного рейса не существует"
		));

		var optionsBin = (byte[]) result[0];

		result.Close();
		command.Dispose();
		cv.Dispose();

		var options = DatabaseOptions.optionsFromBytes(optionsBin);

		return Either<Dictionary<int, Options>, InputError>.Success(options);
		}}}
	}
	
	private Either<SeatCost[], InputError> calculateSeatsCosts(
		Dictionary<int, Options> options, SeatAndOptions[] seats
	) {
		var seatsCost = new SeatCost[seats.Length];

		for(int i = 0; i < seats.Length; i++) {
			var seat = seats[i];
			var opt = options[seat.selectedSeatClass];

			var hi = seat.selectedOptions.baggageOptions.handLuggageIndex;
			var bi = seat.selectedOptions.baggageOptions.baggageIndex;
			if(
				bi < 0 || bi >= opt.baggageOptions.baggage.Count ||
				hi < 0 || hi >= opt.baggageOptions.handLuggage.Count
			) return Either<SeatCost[], InputError>.Failure( 
				new InputError{ message = "для пассажира" + (seats.Length == 1 ? "" : " " + i) + " не заданы опции для багажа" }
			);

			if(seat.selectedOptions.servicesOptions.seatSelected != (seat.seatIndex != null)) {
				return Either<SeatCost[], InputError>.Failure(new InputError{ 
						message = "для пассажира" + (seats.Length == 1 ? "" : " " + i) 
						+ " неправильно задана опция автовыбора места" 
				});
			}

			var baggageOption = opt.baggageOptions.baggage[bi];
			var handLuggageOption = opt.baggageOptions.handLuggage[hi];

			var sd = new SeatCost(){
				basePrice = opt.servicesOptions.basePriceRub,
				baggageCost = baggageOption.costRub + handLuggageOption.costRub,
				seatCost = opt.servicesOptions.seatChoiceCostRub * (seat.selectedOptions.servicesOptions.seatSelected ? 1 : 0)
			};

			sd.totalCost = sd.basePrice + sd.baggageCost + sd.seatCost;

			seatsCost[i] = sd;
		}

		return Either<SeatCost[], InputError>.Success(seatsCost);
	}

	Either<SeatCost[], InputError> ClientService.calculateSeatsCost(int flightId, SeatAndOptions[] seatsAndOptions) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var optionsResult = extractOptions(new SqlConnectionView(connection, true), flightId);
		connection.Dispose();
		if(optionsResult) return calculateSeatsCosts(optionsResult.s, seatsAndOptions);
		else return Either<SeatCost[], InputError>.Failure(optionsResult.f);
		}
	}

	Either<BookingFlightResult, LoginOrInputError> ClientService.bookFlight(Account? customer, SelectedSeat[] selectedSeats, Dictionary<int, Passanger> tempPassangers, int flightId) {
		if(selectedSeats.Length == 0) return Either<BookingFlightResult, LoginOrInputError>.Failure(new LoginOrInputError{
			InputError = new InputError("Должен быть добавлен хотя бы один пассажир")
		});

		var seatsAndOptions = new SeatAndOptions[selectedSeats.Length];
		for(int i = 0; i < seatsAndOptions.Length; i++) seatsAndOptions[i] = selectedSeats[i].seatAndOptions;

		//fill seats tables				
        DataTable bookingTable = new DataTable();
        bookingTable.Columns.Add("Id", typeof(int));
        bookingTable.Columns.Add("TempPassanger", typeof(bool));
        bookingTable.Columns.Add("Passanger", typeof(int));
        bookingTable.Columns.Add("SelectedSeat", typeof(short));
        bookingTable.Columns.Add("Class", typeof(byte));
        bookingTable.Columns.Add("SelectedOptions", typeof(byte[]));

		ISet<int> addedTempPassangers = new HashSet<int>();

		DataTable tempPassangersTable = new DataTable();
        tempPassangersTable.Columns.Add("Id", typeof(int));
        tempPassangersTable.Columns.Add("Birthday", typeof(DateTime));
        tempPassangersTable.Columns.Add("Document", typeof(byte[]));
        tempPassangersTable.Columns.Add("Name", typeof(string));
        tempPassangersTable.Columns.Add("Surname", typeof(string));
        tempPassangersTable.Columns.Add("MiddleName", typeof(string));

        for(int i = 0; i < selectedSeats.Length; i++) {
			var seat = selectedSeats[i];

            var dr = bookingTable.NewRow();
			dr[0] = i;
			dr[1] = seat.fromTempPassangers;
            dr[2] = seat.passangerId;
			dr[3] = seat.seatAndOptions.seatIndex != null ? seat.seatAndOptions.seatIndex : (object) DBNull.Value;
			dr[4] = seat.seatAndOptions.selectedSeatClass;
            dr[5] = DatabaseOptions.selectedOptionsToBytes(seat.seatAndOptions.selectedOptions);
            bookingTable.Rows.Add(dr);

			if(seat.fromTempPassangers && !addedTempPassangers.Contains(seat.passangerId)) {
				addedTempPassangers.Add(seat.passangerId);

				var tp = tempPassangers[seat.passangerId];
				var tr = tempPassangersTable.NewRow();
				tr[0] = seat.passangerId;
				tr[1] = tp.birthday;
				tr[2] = DatabaseDocument.toBytes(tp.document);
				tr[3] = tp.name;
				tr[4] = tp.surname;
				tr[5] = tp.middleName != null ? tp.middleName : (object) DBNull.Value;

				tempPassangersTable.Rows.Add(tr);
			}
        }

		using(
		var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

		using(
		var bookFlight = new SqlCommand("[Flights].[BookFlight]", connection)) {
		bookFlight.CommandType = CommandType.StoredProcedure;

		var customerParam = bookFlight.Parameters.Add("@Customer", SqlDbType.Int);
		bookFlight.Parameters.AddWithValue("@AvailableFlight", flightId);
		var customerBookedFlightIdParam = bookFlight.Parameters.Add("@CustomerBookedFlightId", SqlDbType.Int);
		var errorAlreadyArchivedParam = bookFlight.Parameters.Add("@AlreadyArchived", SqlDbType.Bit);
		var errorSeatParam = bookFlight.Parameters.Add("@ErrorSeatId", SqlDbType.Int);
		var errorPassangerParam = bookFlight.Parameters.Add("@ErrorTempPassangerId ", SqlDbType.Int);
		customerBookedFlightIdParam.Direction = ParameterDirection.Output;
		errorAlreadyArchivedParam.Direction = ParameterDirection.Output;
		errorSeatParam.Direction = ParameterDirection.Output;
		errorPassangerParam.Direction = ParameterDirection.Output;


		var bookSeatsParam = bookFlight.Parameters.AddWithValue("@BookSeats", bookingTable);
		bookSeatsParam.SqlDbType = SqlDbType.Structured;
		bookSeatsParam.TypeName = "[Flights].[BookSeat]";
		
		var tempPassangersParam = bookFlight.Parameters.AddWithValue("@TempPassangers", tempPassangersTable);
		tempPassangersParam.SqlDbType = SqlDbType.Structured;
		tempPassangersParam.TypeName = "[Flights].[TempPassangers]";

		if(customer != null) {
			var userIdRes = getUserId(new SqlConnectionView(connection, false), (Account) customer);
			if(!userIdRes.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });
			customerParam.Value = userIdRes.s;
		}
		else customerParam.Value = DBNull.Value;

		//calculate price
		var optionsResult = extractOptions(new SqlConnectionView(connection, false), flightId);
		if(!optionsResult.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(
			new LoginOrInputError{ InputError = optionsResult.f }
		);

		var costsResult = calculateSeatsCosts(optionsResult.s, seatsAndOptions);
		if(!costsResult.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(
			new LoginOrInputError{ InputError = costsResult.f }
		);
		var costs = costsResult.s;
		
		//book flight
		var flightBookingResult = new DataSet();
		var dataAdapter = new SqlDataAdapter(bookFlight);
		dataAdapter.Fill(flightBookingResult);
		bookFlight.Dispose();
		connection.Dispose();

		//return expected error
		var es = Validation.ErrorString.Create();
		
		if((bool) errorAlreadyArchivedParam.Value) {
			es.ac("на данный рейс уже невозможно оформить билеты");
		}
		if(!(errorSeatParam.Value is DBNull)) { 
			var errorSeat = (int) errorSeatParam.Value;
			es.ac("место пассажира " + errorSeat + " уже занято или не существует");
		}
		if(!(errorPassangerParam.Value is DBNull)) { 
			var errorPassanger = (int) errorPassangerParam.Value;
			es.ac("Пассажир " + errorPassanger + " не может быть добавлен");
		}
		
		if(es.Error) return Either<BookingFlightResult, LoginOrInputError>.Failure(new LoginOrInputError{
			InputError = new InputError(es.Message)
		});

		//throw unexpected error
		if(flightBookingResult == null || flightBookingResult.Tables.Count != 3
			|| flightBookingResult.Tables[0] == null || flightBookingResult.Tables[1] == null
			|| flightBookingResult.Tables[2] == null
		) throw new Exception();

		//extract data from procedure
		Dictionary<int, int> localPassangersDatabaseIds;
		{
			var table = flightBookingResult.Tables[0];

			localPassangersDatabaseIds = new Dictionary<int, int>(table.Rows.Count);

			var localIds = table.Columns["LocalId"].Ordinal;
			var dbIds = table.Columns["DatabaseId"].Ordinal;
			for(var i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				localPassangersDatabaseIds.Add((int) row[localIds], (int) row[dbIds]);
			}
		}

		Dictionary<int, int> selectedSeatsIndices; 
		{
			var table = flightBookingResult.Tables[1];

			selectedSeatsIndices = new Dictionary<int, int>(table.Rows.Count);

			var ids = table.Columns["Id"].Ordinal;
			var setIndices = table.Columns["SeatIndex"].Ordinal;
			for(var i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				selectedSeatsIndices.Add((int) row[ids], (int) row[setIndices]);
			}
		}

		Dictionary<int, string> passangersPNRs; 
		{
			var table = flightBookingResult.Tables[2];

			passangersPNRs = new Dictionary<int, string>(table.Rows.Count);

			var ids = table.Columns["TempId"].Ordinal;
			var pnr = table.Columns["PNR"].Ordinal;
			for(var i = 0; i < table.Rows.Count; i++) {
				var row = table.Rows[i];
				passangersPNRs.Add((int) row[ids], (string) row[pnr]);
			}
		}

		//prepare result
		var result = new BookedSeatInfo[selectedSeats.Length];
		for(int i = 0; i < result.Length; i++) {
			var seat = selectedSeats[i];

			var bookingSeatInfo = new BookedSeatInfo();

			if(seat.fromTempPassangers) {
				bookingSeatInfo.passangerId
					= localPassangersDatabaseIds[seat.passangerId];
			}
			else {
				bookingSeatInfo.passangerId = seat.passangerId;
			}

			if(seat.seatAndOptions.seatIndex == null) {
				bookingSeatInfo.selectedSeat = selectedSeatsIndices[i];
			}
			else bookingSeatInfo.selectedSeat = (int) seat.seatAndOptions.seatIndex;

			bookingSeatInfo.pnr = passangersPNRs[i];

			bookingSeatInfo.cost = costs[i];

			result[i] = bookingSeatInfo;
		}
		
		return Either<BookingFlightResult, LoginOrInputError>.Success(new BookingFlightResult{
			customerBookedFlightId = customerBookedFlightIdParam.Value is DBNull ? 
				null : (int?) customerBookedFlightIdParam.Value,
			seatsInfo = result
		});
		}}
	}


	Either<Seats, InputError> ClientService.getSeatsForFlight(int flightId) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(DatabaseSeatsExtraction.commandText, connection)) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", flightId);

		var rsr = new DatabaseSeatsExtraction();

		connection.Open();
		using(
		var result = command.ExecuteReader()) { 
		var res = rsr.execute(result);

		result.Close();
		command.Dispose();
		connection.Dispose();

		if(res.IsSuccess) return Either<Seats, InputError>.Success(rsr.calculate());
		else return Either<Seats, InputError>.Failure(res.f);

		}}}
	}

	class RawBookedFlight {
		public int id;
		public int availableFlightId;
		public string flightName;
		public string airplaneName;
		public DateTime departureDatetime;
		public int arrivalOffsetMinutes;
		public byte[] optionsBin;
		public string fromCode;
		public string toCode;
		public int bookedPassangersCount;
		public DateTime bookedDatetime;
	}

	Either<BookedFlight[], LoginError> ClientService.getBookedFlights(Account customer) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

		using(var command = new SqlCommand(
			@"
			select 
				[cbf].[Id] as [BookedFlightId],
				[af].[Id] as [AvailableFlightId],
				concat([fi].[AirlineDesignator], ' ', cast([fi].[Number] as char(4))) as [FlightName],
				[ap].[Name] as [AirplaneName],
				dateadd(minute, [af].[DepartureTimeMinutes], cast([af].[DepartureDate] as datetime)) as [DepDatetime],
				[fi].[ArrivalOffsetMinutes] as [ArrivalOffsetMinutes],
				[fi].[Options] as [Options],
				[fi].[FromCity],
				[fi].[ToCity],
				[cbf].[PassangersCount],
				[cbf].[BookedDatetime]
			from (
				select *
				from [Customers].[CustomersBookedFlights] as [cbf]
				where [cbf].[Customer] = @Customer
					and [cbf].[PassangersCount] > 0
			) as [cbf]

			inner join [Flights].[AvailableFlights] as [af]
			on [af].[Id] = [cbf].[AvailableFlight]
			
			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightInfo]  = [fi].[Id]
			
			inner join [Flights].[Airplanes] as [ap]
			on [fi].[Airplane] = [ap].[Id]
			
			order by [cbf].[BookedDatetime] asc;
			", 
			connection
		)) {
		command.CommandType = CommandType.Text;
		var customerParam = command.Parameters.Add("@Customer", SqlDbType.Int);
		
		var rawFlights = new List<RawBookedFlight>();

		connection.Open();

		var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
		if(!userIdRes.IsSuccess) return Either<BookedFlight[], LoginError>.Failure(userIdRes.Failure());

		customerParam.Value = userIdRes.s;

		using(
		var reader = command.ExecuteReader()) {
		while(reader.Read()) {
			var it = new RawBookedFlight();
			it.id = (int) reader[0];
			it.availableFlightId = (int) reader[1];
			it.flightName = (string) reader[2];
			it.airplaneName = (string) reader[3];
			it.departureDatetime = (DateTime) reader[4];
			it.arrivalOffsetMinutes = (int) reader[5];
			it.optionsBin = (byte[]) reader[6];
			it.fromCode = (string) reader[7];
			it.toCode = (string) reader[8];
			it.bookedPassangersCount = (int) reader[9];
			it.bookedDatetime = (DateTime) reader[10];
			rawFlights.Add(it);
		}
		reader.Close();
		command.Dispose();
		connection.Dispose();

		var flights = new BookedFlight[rawFlights.Count];
		for(int i = 0; i < flights.Length; i++) {
			var rawFlight = rawFlights[flights.Length-1 - i];
			var it = new BookedFlight{
				bookedFlightId = rawFlight.id,
				availableFlight = new Flight{
					id = rawFlight.availableFlightId,

					departureTime = rawFlight.departureDatetime,
					arrivalOffsteMinutes = rawFlight.arrivalOffsetMinutes,

					flightName = rawFlight.flightName,
					airplaneName = rawFlight.airplaneName,

					optionsForClasses = DatabaseOptions.optionsFromBytes(rawFlight.optionsBin),
					availableSeatsForClasses = null,

					fromCode = rawFlight.fromCode,
					toCode = rawFlight.toCode,
				},
				bookedPassangerCount = rawFlight.bookedPassangersCount,
				bookingFinishedTime = rawFlight.bookedDatetime
			};				

			flights[i] = it;
		}

		return Either<BookedFlight[], LoginError>.Success(flights);
		}}}
	}

	private struct RawPassangerData {
		public int passanger;
		public short seatIndex;
		public byte[] selectedOptionsBin;
		public string pnr;
	}

	Either<BookedFlightDetails, LoginOrInputError> ClientService.getBookedFlightDetails(Account customer, int bookedFlightId) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

		using(
		var command = new SqlCommand(
			@"
			declare @AvailableFlight int;
			select top 1 @AvailableFlight = [cbf].[AvailableFlight]
			from [Customers].[CustomersBookedFlights] as [cbf]
			where [cbf].[Customer] = @Customer and [cbf].[Id] = @BookedFlightId;

			if(@AvailableFlight is null) begin
				raiserror('AvailableFlight is null', 11, 2);
				goto skipEverything;
			end;


			select [afs].[Passanger], [afs].[SeatIndex], [afs].[SelectedOptions], [afs].[PNR]
			from [Flights].[AvailableFlightsSeats] as [afs]
			where [afs].[AvailableFlight] = @AvailableFlight
				and [afs].[CustomerBookedFlightId] = @BookedFlightId
				and [afs].[CanceledIndex] = 0
			order by [afs].[SeatIndex] asc;


			select top 1 [fi].[Options]
			from (
				select [af].[FlightInfo] 
				from [Flights].[AvailableFlights] as [af]
				where [af].[Id] = @AvailableFlight
			) as [af]

			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightInfo] = [fi].[Id];

			" + DatabaseSeatsExtraction.commandText
			+ "\nskipEverything:;",
			connection
		)) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@BookedFlightId", bookedFlightId);
		var customerParam = command.Parameters.Add("@Customer", SqlDbType.Int);

		var rawPassangersData = new List<RawPassangerData>();
		var extractor = new DatabaseSeatsExtraction();

		connection.Open();

		var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
		if(!userIdRes.IsSuccess) return Either<BookedFlightDetails, LoginOrInputError>.Failure(
			new LoginOrInputError{ LoginError = userIdRes.Failure() }
		);
		var userId = userIdRes.s;

		customerParam.Value = userId;

		byte[] optionsBin;
		Either<Success, InputError> executionResult;
		try{
		
		using(
		var result = command.ExecuteReader()) {
		while(result.Read()) {
			rawPassangersData.Add(new RawPassangerData{
				passanger = (int) result[0],
				seatIndex = (short) result[1],
				selectedOptionsBin = (byte[]) result[2],
				pnr = (string) result[3]
			});
		}

		Common.Debug2.AssertPersistent(result.NextResult());

		if(!result.Read()) return Either<BookedFlightDetails, LoginOrInputError>.Failure(new LoginOrInputError{
			InputError = new InputError("Данный рейс не найден")
		});
		optionsBin = (byte[]) result[0];

		Common.Debug2.AssertPersistent(result.NextResult());
		executionResult = extractor.execute(result);

		result.Close();
		command.Dispose();
		connection.Dispose();
		}

		} 
		catch(SqlException e) {
			if(e.State == 2) return Either<BookedFlightDetails, LoginOrInputError>.Failure(new LoginOrInputError{
				InputError = new InputError("Данный рейс не найден")
			});
			else throw e;
		}

		if(!executionResult.IsSuccess) return Either<BookedFlightDetails, LoginOrInputError>.Failure(new LoginOrInputError{
			InputError = executionResult.f
		});

		var seats = extractor.calculate();

		var options = DatabaseOptions.optionsFromBytes(optionsBin);

		var selectedSeatsOptions = new SeatAndOptions[rawPassangersData.Count];
		for(int i = 0; i < selectedSeatsOptions.Length; i++) {
			var rp = rawPassangersData[i];
			var selOptions = DatabaseOptions.selectedOptionsFromBytes(rp.selectedOptionsBin);

			var it = new SeatAndOptions();

			it.seatIndex = selOptions.servicesOptions.seatSelected ? (int?) rp.seatIndex : null;
			it.selectedOptions = selOptions;
			it.selectedSeatClass = seats.Class(rp.seatIndex);

			selectedSeatsOptions[i] = it;
		}

		var costsResult = calculateSeatsCosts(options, selectedSeatsOptions);
		if(!costsResult.IsSuccess) return Either<BookedFlightDetails, LoginOrInputError>.Failure(
			new LoginOrInputError{ InputError = new InputError("Ошибка вычисления цены: " + costsResult.f.message) }
		);

		var bookedSeats = new BookedSeatInfo[rawPassangersData.Count];
		for(int i = 0; i < selectedSeatsOptions.Length; i++) {
			var it = new BookedSeatInfo();

			var rpd = rawPassangersData[i];

			it.pnr = rpd.pnr;
			it.passangerId = rpd.passanger;
			it.selectedSeat = rpd.seatIndex;
			it.cost = costsResult.s[i];

			bookedSeats[i] = it;
		}

		return Either<BookedFlightDetails, LoginOrInputError>.Success(new BookedFlightDetails{
			bookedSeats = bookedSeats,
			seatsAndOptions = selectedSeatsOptions,
			seats = seats
		});
		}}
	}

	Either<Success, LoginOrInputError> ClientService.deleteBookedSeat(string surname, string pnr) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(
			"[Flights].[UnbookSeat]", connection
		)) {
		command.CommandType = CommandType.StoredProcedure;
		command.Parameters.AddWithValue("@Surname", surname);
		command.Parameters.AddWithValue("@PNR", pnr);

		connection.Open();
		try{ command.ExecuteNonQuery(); }
		catch(SqlException e) {
			if(e.State == 2) {
				return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError("Данный рейс не найден")
				});
			}
			else if(e.State == 5) {
				return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError("Данная бронь не найдена или не может быть отменена")
				});
			}
			else if(e.State == 10) {
				return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError("Бронь на данный полёт уже нельзя отменить")
				});
			}
			else if(e.State == 20) {
				return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError("Внутренняя ошибка")
				});
			}
			else throw e;
		}

		return Either<Success, LoginOrInputError>.Success(new Success());
		}}
	}

	Either<BookedFlightPassanger, InputError> ClientService.getBookedFlightFromSurnameAndPNR(string surname, string pnr) {
        using (var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
        using (
        var command = new SqlCommand(
            @"
            select 
			    [pi].[Id], [pi].[Archived], [pi].[Name], [pi].[Surname],
			    [pi].[MiddleName], [pi].[Birthday], [pi].[Document],
			
			    [afs].[SeatIndex], [aps].[SeatClass],
				[afs].[SelectedOptions], [fi].[Options]
			from (
			    select top 1 *
			    from [Flights].[AvailableFlightsSeats] as [afs]
			    where [afs].[PNR] = @PNR
			) as [afs]
			
			inner join [Customers].[Passanger] as [pi]
			on [afs].[Passangers] = [pi].[Id] and [pi].[Surname] = @Surname
			
			inner join [Flights].[AvailableFlights] as [af]
			on [afs].[AvailableFLight] = [af].[Id]
			
			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightOptions] = [fi].[Id]
			
			inner join [Flights].[AirplanesSeats] as [aps]
			on [afs].[SeatIndex] = [aps].[SeatIndex]
				 and [fi].[Airplane] = [aps].[Airplane]
			",
            connection
        )) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@PNR", pnr);
		command.Parameters.AddWithValue("@Surname", surname);
		
		var p = new Passanger();

		using (
		var result = command.ExecuteReader()) {
		if(!result.Read()) return Either<BookedFlightPassanger, InputError>.Failure(
			new InputError("Пассажир не найден")
		);
		
		var pId = (int) result[0];
		p.archived = (bool) result[1];
		p.name = (string) result[2];
		p.surname = (string) result[3];
		p.middleName = (string) (result[4] is DBNull ? null : result[4]);
		p.birthday = (DateTime) result[5];
		var documentBin = (byte[]) result[6];
		var seatIndex = (short) result[7];
		var seatClass = (byte) result[8];
		var selectedOptionsBin = (byte[]) result[9];
		var optionsBin = (byte[]) result[10];
		Common.Debug2.AssertPersistent(!result.Read());

		result.Close();
		command.Dispose();
		connection.Dispose();
		
		var options = DatabaseOptions.optionsFromBytes(optionsBin);
		var selectedOptions = DatabaseOptions.selectedOptionsFromBytes(selectedOptionsBin);

		var seatAndOptions = new SeatAndOptions[1];
		seatAndOptions[0] = new SeatAndOptions{
			seatIndex = seatIndex,
			selectedOptions = selectedOptions,
			selectedSeatClass = seatClass
		};
		
		var costsRes = calculateSeatsCosts(options, seatAndOptions);
		if(!costsRes.IsSuccess) return Either<BookedFlightPassanger, InputError>.Failure(costsRes.f);
		var costs = costsRes.s;

		p.document = DatabaseDocument.fromBytes(documentBin);

		return Either<BookedFlightPassanger, InputError>.Success(new BookedFlightPassanger {
			passanger = p,
			bookedSeat = new BookedSeatInfo{ 
				passangerId = pId, selectedSeat = seatIndex,
				pnr = pnr, cost = costs[0]
			},
			seatAndOptions = new SeatAndOptions {
				seatIndex = seatIndex,
				selectedOptions = selectedOptions,
				selectedSeatClass = seatClass
			}
		});
		}}}
    }
}

}
