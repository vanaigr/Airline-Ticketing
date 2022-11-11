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
		var result = DatabaseAccount.checkAccountDataValid(c);
		if(!result.IsSuccess) return result;

		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var registered = DatabaseAccount.register(new SqlConnectionView(connection, true), c.login, c.password);

		if(registered) return Either<Success, LoginError>.Success(new Success());
		else return Either<Success, LoginError>.Failure(
			new LoginError("Аккаунт с таким именем пользователя уже существует")
		);
		}
	}

	Either<Success, LoginError> ClientService.logInAccount(Account c) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var res = DatabaseAccount.getUserId(new SqlConnectionView(connection, true), c);
		if(res.IsSuccess) return Either<Success, LoginError>.Success(new Success());
		else return Either<Success, LoginError>.Failure(res.f);
		}
	}

	Either<int, LoginOrInputError> ClientService.addPassanger(Account c, Passanger passanger) {
		var it = ValidatePassanger.validate(passanger);
		if(it.error) return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{ InputError = new InputError(it.errorMsg) });
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), c);
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
		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), c);
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
		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), c);
		if(!userIdRes.IsSuccess) return Either<Dictionary<int, Passanger>, LoginError>.Failure(userIdRes.Failure());
		return Either<Dictionary<int, Passanger>, LoginError>.Success(
			DatabasePassanger.getAll(new SqlConnectionView(connection, true), userIdRes.Success())
		);
		}
	}

	Either<Success, LoginOrInputError> ClientService.removePassanger(Account customer, int index) {
	using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), customer);
		if(!userIdRes.IsSuccess) return Either<Success, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });

		var result = DatabasePassanger.remove(new SqlConnectionView(connection, true), userIdRes.s, index);

		if(result.ok) return Either<Success, LoginOrInputError>.Success(new Success());
		else return Either<Success, LoginOrInputError>.Failure(
			new LoginOrInputError{ InputError = new InputError(
				result.errorMsg
			)}
		);
	}}

	Either<SeatCost[], InputError> ClientService.calculateSeatsCost(int flightId, SeatAndOptions[] seatsAndOptions) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		var optionsResult = DatabaseFlights.extractOptions(new SqlConnectionView(connection, true), flightId);
		connection.Dispose();
		if(optionsResult) return DatabaseFlights.calculateSeatsCosts(optionsResult.s, seatsAndOptions);
		else return Either<SeatCost[], InputError>.Failure(optionsResult.f);
		}
	}

	Either<BookingFlightResult, LoginOrInputError> ClientService.bookFlight(Account? account, SelectedSeat[] selectedSeats, Dictionary<int, Passanger> tempPassangers, int flightId) {
		return SeatsBooking.bookFlight(account, selectedSeats, tempPassangers, flightId);
	}


	Either<Seats, InputError> ClientService.getSeatsForFlight(int flightId) {
		Stopwatch sw = new Stopwatch();
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(DatabaseSeatsExtraction.commandText, connection)) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", flightId);

		var rsr = new DatabaseSeatsExtraction();

		connection.Open();
		using(
		var result = command.ExecuteReader()) { 
		sw.Restart();
		sw.Start();
		var res = rsr.execute(result);
		sw.Stop();
		var t1 = sw.Elapsed.TotalMilliseconds * 1000;

		result.Close();
		command.Dispose();
		connection.Dispose();

		sw.Restart();
		sw.Start();
		var c = rsr.calculate();
		sw.Stop();
		var t2 = sw.Elapsed.TotalMilliseconds * 1000;

		Console.WriteLine("t1={0}, t2={1}", t1, t2);

		if(res.IsSuccess) return Either<Seats, InputError>.Success(c);
		else return Either<Seats, InputError>.Failure(res.f);

		}}}
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
		
		var bookedFlights = new List<BookedFlight>();
		var optionsBinList = new List<byte[]>();

		connection.Open();

		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), customer);
		if(!userIdRes.IsSuccess) return Either<BookedFlight[], LoginError>.Failure(userIdRes.Failure());

		customerParam.Value = userIdRes.s;

		using(
		var reader = command.ExecuteReader()) {
		while(reader.Read()) {
			var it = new BookedFlight();
			var af = new Flight();
			it.availableFlight = af;

			it.bookedFlightId = (int) reader[0];
			af.id = (int) reader[1];
			af.flightName = (string) reader[2];
			af.airplaneName = (string) reader[3];
			af.departureTime = (DateTime) reader[4];
			af.arrivalOffsetMinutes = (int) reader[5];
			var optionsBin = (byte[]) reader[6];
			af.fromCode = (string) reader[7];
			af.toCode = (string) reader[8];
			it.bookedPassangerCount = (int) reader[9];
			it.bookingFinishedTime = (DateTime) reader[10];

			optionsBinList.Add(optionsBin);
			bookedFlights.Add(it);
		}
		reader.Close();
		command.Dispose();
		connection.Dispose();

		for(int i = 0; i < bookedFlights.Count; i++) {
			var flight = bookedFlights[i];
			var optionsBin = optionsBinList[i];

			flight.availableFlight.optionsForClasses
				= DatabaseOptions.optionsFromBytes(optionsBin);
		}

		return Either<BookedFlight[], LoginError>.Success(bookedFlights.ToArray());
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

		var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), customer);
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

		var costsResult = DatabaseFlights.calculateSeatsCosts(options, selectedSeatsOptions);
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

	public Either<PassangerBookedFlightAndDetails, InputError> getBookedFlightFromSurnameAndPNR(string surname, string pnr) {
		var es = Validation.ErrorString.Create();

		if(surname == null) es.ac("фамилия должна быть заполнена");
		if(pnr == null) es.ac("PNR должен быть заполнен");

		if(es.Error) return Either<PassangerBookedFlightAndDetails, InputError>.Failure(
			new InputError(es.Message)
		);

		var extractor = new DatabaseSeatsExtraction();

		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(DatabaseSeatsExtraction.commandText, connection)) { 
		command.CommandType = CommandType.Text;
		var afParam = command.Parameters.Add("@AvailableFlight", SqlDbType.Int);
		
		var res = SeatsBooking.getBookedFlight(
			new SqlConnectionView(connection, false),
			new OperatorCommunication.PassangerSearchParams{
				surname = surname, pnr = pnr
			}
		);
		if(!res.IsSuccess) return Either<PassangerBookedFlightAndDetails, InputError>.Failure(res.f);
		var flights = res.s;
		if(flights.Count != 1) return Either<PassangerBookedFlightAndDetails, InputError>.Failure(new InputError(
			"Пассажи не найден"
		));
		var flight = flights[0];
		afParam.Value = flight.flight.availableFlight.id;
		
		connection.Open2();
		using(
		var result = command.ExecuteReader()) {
		var execRes = extractor.execute(result);
		result.Close();
		command.Dispose();
		connection.Close();
		if(!execRes.IsSuccess) return Either<PassangerBookedFlightAndDetails, InputError>.Failure(execRes.f);

		var calcRes = extractor.calculate();
		
		return Either<PassangerBookedFlightAndDetails, InputError>.Success(new PassangerBookedFlightAndDetails{
			cancelled = flight.cancelled,
			flight = flight.flight,
			passanger = flight.passanger,
			passangerId = flight.passangerId,
			details = new BookedFlightDetails{ 
				bookedSeats = new BookedSeatInfo[]{ flight.bookedSeat },
				seatsAndOptions = new SeatAndOptions[]{ flight.seatAndOptions },
				seats = calcRes
			}
		});
		}}}
	}

	public Either<Success, InputError> deleteBookedSeat(string surname, string pnr) {
		var es = Validation.ErrorString.Create();

		if(surname == null) es.ac("фамилия должна быть заполнена");
		if(pnr == null) es.ac("PNR должен быть заполнен");

		if(es.Error) return Either<Success, InputError>.Failure(
			new InputError(es.Message)
		);
		return SeatsBooking.deleteBookedSeat(surname, pnr);
	}

}
}
