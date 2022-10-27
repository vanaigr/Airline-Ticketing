using Communication;
using FlightsOptions;
using FlightsSeats;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;

namespace AirlineTicketingServer {

	class Program {
		
		[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = false)]
		class MainMessageService : MessageService {
			private readonly string[] flightClasses = new string[]{ "Эконом", "Комфорт", "Бизнес", "Первый класс" };
			private readonly List<City> cities;

			public MainMessageService() {
				cities = new List<City>();

				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

				using(
				var selectCities = new SqlCommand("select [Id], [Name], [Country] from [Flights].[Airports]")) {
				selectCities.CommandType = System.Data.CommandType.Text;

				selectCities.Connection = connection;
				connection.Open();
				using(
				var result = selectCities.ExecuteReader()) {
				while(result.Read()) cities.Add(new City{ 
					code = (string) result[0], 
					name = (string) result[1],
					country = (string) result[2] 
				});
				}
				
				}


				if(false) { //update options
					var economOptins = new Options{
					baggageOptions = new BaggageOptions{
						baggage = new List<Baggage> {
							new Baggage(costRub: 0, count: 0),
							new Baggage(costRub: 2500, count: 1, maxWeightKg: 23),
							new Baggage(costRub: 5000, count: 2, maxWeightKg: 23)
						},
						handLuggage = new List<Baggage>{
							new Baggage(costRub: 0, count: 1, maxWeightKg: 10, maxDim: new Size3{ x=55, y=40, z=20 }),
						},
					},
					termsOptions = new TermsOptions {
						changeFlightCostRub = 3250,
						refundCostRub = -1
					},
					servicesOptions = new ServicesOptions {
						seatChoiceCostRub = 450
					},
					basePriceRub = 2400
				};
				var busunessOptions = new Options{
					baggageOptions = new BaggageOptions{
						baggage = new List<Baggage> {
							new Baggage(costRub: 0, count: 1, maxWeightKg: 32),
							new Baggage(costRub: 2500, count: 2, maxWeightKg: 32),
							new Baggage(costRub: 5000, count: 3, maxWeightKg: 32)
						},
						handLuggage = new List<Baggage>{
							new Baggage(costRub: 0, count: 1, maxWeightKg: 15, maxDim: new Size3{ x=55, y=40, z=20 }),
						},
					},
					termsOptions = new TermsOptions {
						changeFlightCostRub = 3250,
						refundCostRub = -1
					},
					servicesOptions = new ServicesOptions {
						seatChoiceCostRub = 0
					},
					basePriceRub = 3500
				};
				var optionsForClasses = new Dictionary<int, Options>(2);
				optionsForClasses.Add(0, economOptins);
				optionsForClasses.Add(2, busunessOptions);

					var connView = new SqlConnectionView(connection, false);
					using(
					var selectClassesCommand = new SqlCommand(
						//@"insert into [dbo].[Table]([Options]) values (@Options)",
						@"update [Flights].[FlightInfo] set [Options] = @Options where [Id] = @id",
						connView.connection
					)) {
					selectClassesCommand.CommandType = System.Data.CommandType.Text;
					var id = selectClassesCommand.Parameters.Add("@Id", SqlDbType.Int);
					selectClassesCommand.Parameters.AddWithValue("@Options", BinaryOptions.toBytes(optionsForClasses));
	
					connView.Open();
					for(int i = 3; i < 6; i++){ 
						id.Value = i;
						selectClassesCommand.ExecuteNonQuery();
					}
					}
					connView.Dispose();
				}

				if(false) { //write seats scheme
					var sizes = new Point[]{ new Point(2, 4), new Point(25, 6) };
					var seatsScheme = new FlightsSeats.SeatsScheme(((IEnumerable<Point>)sizes).GetEnumerator());

					var connView = new SqlConnectionView(connection, false);
					using(
					var selectClassesCommand = new SqlCommand(
						@"insert into [Flights].[Airplanes](                      
						[Name]         ,           
						[EconomyClassSeatsCount]  ,
						[ComfortClassSeatsCount]  ,
						[BusinessClassSeatsCount] ,
						[FirstClassSeatsCount]  , 
						[SeatsScheme]         )
						values ('Airbus A320', 150, 0, 8, 0, @Scheme);",
						connView.connection
					)) {
					selectClassesCommand.CommandType = System.Data.CommandType.Text;
					selectClassesCommand.Parameters.AddWithValue("@Name", "Airbus A320");
					selectClassesCommand.Parameters.AddWithValue("@Scheme", BinarySeats.toBytes(seatsScheme));
	
					connView.Open();
					selectClassesCommand.ExecuteNonQuery();
					}
					connView.Dispose();
				}
				
				AvailableFlightsUpdate.checkAndUpdate(new SqlConnectionView(connection, true));
				}				
			}

			Either<object, LoginError> testLoginPasswordValid(Customer c) {
				if(c.login == null) c.login = "";
				if(c.password == null) c.password = "";
				var invalidLogin = LoginRegister.checkLogin(c.login);
				var invalidPassword = LoginRegister.checkPassword(c.password);

				if(!invalidLogin.ok || !invalidPassword.ok) {
					var loginError = invalidLogin.errorMsg;
					var passError = invalidPassword.errorMsg;
					return Either<object, LoginError>.Failure(new LoginError(loginError + "\n" + passError));
				}
				else return Either<object, LoginError>.Success(null);
			}

			public AvailableOptionsResponse availableOptions() {
				return new AvailableOptionsResponse {
					flightClasses = flightClasses,
					cities = cities
				};
			}

			struct RawAvailableFlight {
				public int id;
				public DateTime departureTime;
				public int arrivalOffsteMinutes;

				public string flightName;
				public string airplaneName;

				public byte[] optionsBin;
				public int[] availableSeatsForClasses;
			}

			public Either<List<AvailableFlight>, InputError> matchingFlights(MatchingFlightsParams p) {
				var err = Validation.ErrorString.Create();
				if(p.fromCode == null) err.ac("город вылета должен быть заполнен");
				if(p.toCode == null) err.ac("город прилёта должен быть заполнен");
				if(p.when == null) err.ac("дата вылета должа быть заполнена");
				if(err) return Either<List<AvailableFlight>, InputError>.Failure(new InputError(err.Message));
				
				var rawAvailableFlights = new List<RawAvailableFlight>();

				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				using(
				var selectClasses = new SqlCommand(
					@"select 
						[AvailableFlight], [FlightName], [AirplaneName], [DepartureDatetime], 
						[ArivalOffsetMinutes], [OptionsBin],
						[AvailableEconomyClassSeatsCount], 
						[AvailableComfortClassSeatsCount],
						[AvailableBusinessClassSeatsCount],
						[AvailableFirstClassSeatsCount]
					from [Flights].[FindFlights](@fromCity, @toCity, @time) 
					order by [DepartureDatetime] desc",
					connection
				)) {
				selectClasses.CommandType = CommandType.Text;
				selectClasses.Parameters.AddWithValue("@fromCity", p.fromCode);
				selectClasses.Parameters.AddWithValue("@toCity", p.toCode);
				selectClasses.Parameters.AddWithValue("@time", p.when);

				connection.Open();
				using(
				var result = selectClasses.ExecuteReader()) {
				while(result.Read()) rawAvailableFlights.Add(new RawAvailableFlight{
					id = (int) result[0], flightName = (string) result[1],
					airplaneName = (string) result[2], departureTime = (DateTime) result[3],
					arrivalOffsteMinutes = (int) result[4],
					optionsBin = (byte[]) result[5],
					availableSeatsForClasses = new int[]{
						(short) result[6], (short) result[7],
						(short) result[8], (short) result[9],
					}
				});
				}}}

				var list = new List<AvailableFlight>(rawAvailableFlights.Count);
				for(int i = rawAvailableFlights.Count-1; i >= 0 ; i--) {
					var raf = rawAvailableFlights[i];
					rawAvailableFlights.RemoveAt(i);
					list.Add(new AvailableFlight{
						id = raf.id,
						departureTime = raf.departureTime,
						arrivalOffsteMinutes = raf.arrivalOffsteMinutes,

						flightName = raf.flightName,
						airplaneName = raf.airplaneName,

						optionsForClasses = BinaryOptions.fromBytes(raf.optionsBin),
						availableSeatsForClasses = raf.availableSeatsForClasses
					});
				}

				return Either<List<AvailableFlight>, InputError>.Success(list);
			}

			public Either<object, LoginError> register(Customer c) {
				var result = testLoginPasswordValid(c);
				if(!result.IsSuccess) return result;

				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var registered = LoginRegister.register(new SqlConnectionView(connection, true), c.login, c.password);
				connection.Dispose();

				if(registered) return Either<object, LoginError>.Success(null);
				else return Either<object, LoginError>.Failure(
					new LoginError("Аккаунт с таким именем пользователя уже существует")
				);
				}
			}

			public Either<object, LoginError> logIn(Customer c) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var res = getUserId(new SqlConnectionView(connection, true), c);
				if(res.IsSuccess) return Either<object, LoginError>.Success(null);
				else return Either<object, LoginError>.Failure(res.f);
				}
			}

			private Either<int, LoginError> getUserId(SqlConnectionView cv, Customer c) {
				using(cv) {
				var error = testLoginPasswordValid(c);
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

			Either<int, LoginOrInputError> MessageService.addPassanger(Customer c, Passanger passanger) {
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

			Either<int, LoginOrInputError> MessageService.replacePassanger(Customer c, int index, Passanger passanger) {
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

			Either<Dictionary<int, Passanger>, LoginError> MessageService.getPassangers(Customer c) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userIdRes = getUserId(new SqlConnectionView(connection, false), c);
				if(!userIdRes.IsSuccess) return Either<Dictionary<int, Passanger>, LoginError>.Failure(userIdRes.Failure());
				return Either<Dictionary<int, Passanger>, LoginError>.Success(
					DatabasePassanger.getAll(new SqlConnectionView(connection, true), userIdRes.Success())
				);
				}
			}

			Either<object, LoginOrInputError> MessageService.removePassanger(Customer customer, int index) {
			using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
				if(!userIdRes.IsSuccess) return Either<object, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });

				var result = DatabasePassanger.remove(new SqlConnectionView(connection, true), userIdRes.s, index);

				if(result.ok) return Either<object, LoginOrInputError>.Success(null);
				else return Either<object, LoginOrInputError>.Failure(
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

				var options = BinaryOptions.fromBytes(optionsBin);

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
						basePrice = opt.basePriceRub,
						baggageCost = baggageOption.costRub + handLuggageOption.costRub,
						seatCost = opt.servicesOptions.seatChoiceCostRub * (seat.selectedOptions.servicesOptions.seatSelected ? 1 : 0)
					};

					sd.totalCost = sd.basePrice + sd.baggageCost + sd.seatCost;

					seatsCost[i] = sd;
				}

				return Either<SeatCost[], InputError>.Success(seatsCost);
			}

			Either<SeatCost[], InputError> MessageService.seatsData(int flightId, SeatAndOptions[] seatsAndOptions) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var optionsResult = extractOptions(new SqlConnectionView(connection, true), flightId);
				connection.Dispose();
				if(optionsResult) return calculateSeatsCosts(optionsResult.s, seatsAndOptions);
				else return Either<SeatCost[], InputError>.Failure(optionsResult.f);
				}
			}

			Either<BookingFlightResult, LoginOrInputError> MessageService.bookFlight(Customer? customer, SelectedSeat[] selectedSeats, Dictionary<int, Passanger> tempPassangers, int flightId) {
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
                    dr[5] = BinaryOptions.toBytes(seat.seatAndOptions.selectedOptions);
                    bookingTable.Rows.Add(dr);

					if(seat.fromTempPassangers && !addedTempPassangers.Contains(seat.passangerId)) {
						addedTempPassangers.Add(seat.passangerId);

						var tp = tempPassangers[seat.passangerId];
						var tr = tempPassangersTable.NewRow();
						tr[0] = seat.passangerId;
						tr[1] = tp.birthday;
						tr[2] = BinaryDocument.toBytes(tp.document);
						tr[3] = tp.name;
						tr[4] = tp.surname;
						tr[5] = tp.middleName != null ? tp.middleName : (object) DBNull.Value;

						tempPassangersTable.Rows.Add(tr);
					}
                }

				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

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
					var userIdRes = getUserId(new SqlConnectionView(connection, false), (Customer) customer);
					if(!userIdRes.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });
					customerParam.Value = userIdRes.s;
				}
				else customerParam.Value = DBNull.Value;
				
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
				if(flightBookingResult == null || flightBookingResult.Tables.Count != 2
					|| flightBookingResult.Tables[0] == null || flightBookingResult.Tables[1] == null
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

			private class DatabaseSeatsExtraction {
				private List<RawSeat> rawSeatsOccupation;
				private byte[] seatsSchemeBin;

				private struct RawSeat {
					public short index;
					public byte classId;
					public bool occupied;
				};

				public static readonly string commandText = @"
					--get seats scheme
					select top 1 [ap].[SeatsScheme]
					from (
						select top 1 [af].[FlightInfo]
						from [Flights].[AvailableFlights] as [af]
						where @AvailableFlight = [af].[Id]
					) as [afFlightInfo]
					
					inner join [Flights].[FlightInfo] as [fi]
					on [afFlightInfo].[FlightInfo] = [fi].[Id]

					inner join [Flights].[Airplanes] as [ap]
					on [fi].[Airplane] = [ap].[Id];


					--get seats occupation
					select 
						[afs].[SeatIndex],
						[aps].[Class], 
						cast(case when [afs].[Passanger] is not null then 1 else 0 end as bit) as [Occupied]
					from (
						select *
						from [Flights].[AvailableFlightsSeats] as [afs]
						where @AvailableFlight = [afs].[AvailableFlight]
							and [afs].[CanceledIndex] = 0
					) as [afs]

					inner join [Flights].[AirplanesSeats] as [aps]
					on [afs].[Airplane] = [aps].[Airplane] and [afs].[SeatIndex] = [aps].[SeatIndex]
					order by [afs].[SeatIndex] ASC;
				";

				public DatabaseSeatsExtraction() {				
					rawSeatsOccupation = new List<RawSeat>();
				}

				public Either<Success, InputError> execute(SqlDataReader result) {
					if(!result.Read()) return Either<Success, InputError>.Failure(new InputError(
						"Данный рейс не существует"
					));
					this.seatsSchemeBin = (byte[]) result[0];

					Debug.Assert(result.NextResult());

					while(result.Read()) this.rawSeatsOccupation.Add(new RawSeat{
						index = (short) result[0],
						classId = (byte) result[1],
						occupied = (bool) result[2]
					});

					return Either<Success, InputError>.Success(new Success());
				}

				public Seats calculate() {
					var seatsScheme = BinarySeats.fromBytes(seatsSchemeBin);

					Debug.Assert(seatsScheme.SeatsCount == rawSeatsOccupation.Count);

					var classes = new byte[seatsScheme.SeatsCount];
					for(int i = 0; i < classes.Length; i++) {
						Debug.Assert(rawSeatsOccupation[i].index == i);
						classes[i] = rawSeatsOccupation[i].classId;
					}

					var occupation = new bool[seatsScheme.SeatsCount];
					for(int i = 0; i < occupation.Length; i++) {
						Debug.Assert(rawSeatsOccupation[i].index == i);
						occupation[i] = rawSeatsOccupation[i].occupied;
					}

					return new Seats(
						seatsScheme, ((IEnumerable<byte>) classes).GetEnumerator(), 
						((IEnumerable<bool>) occupation).GetEnumerator()
					);
				}
			}


			public Either<Seats, InputError> seatsForFlight(int flightId) {
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

			Either<BookedFlight[], LoginError> MessageService.getBookedFlights(Customer customer) {
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
						availableFlight = new AvailableFlight{
							id = rawFlight.availableFlightId,
							departureTime = rawFlight.departureDatetime,
							arrivalOffsteMinutes = rawFlight.arrivalOffsetMinutes,
							flightName = rawFlight.flightName,
							airplaneName = rawFlight.airplaneName,
							optionsForClasses = BinaryOptions.fromBytes(rawFlight.optionsBin),
							availableSeatsForClasses = null
						},
						fromCode = rawFlight.fromCode,
						toCode = rawFlight.toCode,
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
			}

			Either<BookedFlightDetails, LoginOrInputError> MessageService.getBookedFlightDetails(Customer customer, int bookedFlightId) {
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


					select [afs].[Passanger], [afs].[SeatIndex], [afs].[SelectedOptions]
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
						selectedOptionsBin = (byte[]) result[2]
					});
				}

				Debug.Assert(result.NextResult());

				if(!result.Read()) return Either<BookedFlightDetails, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError("Данный рейс не найден")
				});
				optionsBin = (byte[]) result[0];

				Debug.Assert(result.NextResult());
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

				var options = BinaryOptions.fromBytes(optionsBin);

				var selectedSeatsOptions = new SeatAndOptions[rawPassangersData.Count];
				for(int i = 0; i < selectedSeatsOptions.Length; i++) {
					var rp = rawPassangersData[i];
					var selOptions = BinaryOptions.selectedOptionsFromBytes(rp.selectedOptionsBin);

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

					it.passangerId = rawPassangersData[i].passanger;
					it.selectedSeat = rawPassangersData[i].seatIndex;
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

			Either<int, LoginOrInputError> MessageService.deleteBookedFlightSeat(Customer customer, int bookedFlightId, int seatIndex) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				using(
				var command = new SqlCommand(
					"[Flights].[UnbookSeat]", connection
				)) {
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@BookedFlightId", bookedFlightId);
				command.Parameters.AddWithValue("@SeatIndex", seatIndex);
				var customerParam = command.Parameters.Add("@Customer", SqlDbType.Int);
				var remainingPassangersParam = command.Parameters.Add("@RemainingPassangersCount", SqlDbType.Int);
				remainingPassangersParam.Direction = ParameterDirection.Output;

				connection.Open();
				var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
				if(!userIdRes.IsSuccess) return Either<int, LoginOrInputError>.Failure(
					new LoginOrInputError{ LoginError = userIdRes.Failure() }
				);
				customerParam.Value = userIdRes.s;

				int remainingPassangersCount;
				try{
				command.ExecuteNonQuery();
				remainingPassangersCount = (int) remainingPassangersParam.Value;
				}
				catch(SqlException e) {
					if(e.State == 2) {
						return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{
							InputError = new InputError("Данный рейс не найден")
						});
					}
					else if(e.State == 5) {
						return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{
							InputError = new InputError("Данная бронь не найдена или не может быть отменена")
						});
					}
					else if(e.State == 10) {
						return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{
							InputError = new InputError("Бронь на данный полёт уже нельзя отменить")
						});
					}
					else if(e.State == 20) {
						return Either<int, LoginOrInputError>.Failure(new LoginOrInputError{
							InputError = new InputError("Внутренняя ошибка")
						});
					}
					else throw e;
				}

				return Either<int, LoginOrInputError>.Success(remainingPassangersCount);
				}}
			}
		}

		[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
		public class LoggingProxy : System.Runtime.Remoting.Proxies.RealProxy {
		    private readonly MessageService instance;
		
		    private LoggingProxy(MessageService instance) : base(typeof(MessageService)) {
		        this.instance = instance;
		    }
		
		    public static MessageService Create(MessageService instance) {
		        return (MessageService) new LoggingProxy(instance).GetTransparentProxy();
		    }
		
		    public override IMessage Invoke(IMessage msg) {
				Stopwatch watch = null;
				try {
					var methodCall = (IMethodCallMessage) msg;
					var method = (MethodInfo) methodCall.MethodBase;
				
					watch = new Stopwatch();
					watch.Start();
		            var result = method.Invoke(instance, methodCall.InArgs);
					watch.Stop();

					var sb = new StringBuilder();

					sb.AppendFormat(
						"Responding ({0}ms) to `{1}` with `{2}` = {{ ",
						watch.Elapsed.TotalMilliseconds, methodCall.MethodName, result.GetType()
					);

					var first = true;
					foreach(var field in result.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)) {
						if(!first) sb.Append(", ");
						sb.Append(field.GetValue(result)?.GetType().ToString() ?? "null");
						first = false;
					}
					sb.Append(" }");
					sb.Append("\n");

					Console.WriteLine("{0}", sb.ToString());
		            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
		        }
		        catch (Exception e) {
					watch.Stop();
					Console.WriteLine(
						"\nError while responding ({0} ms) to `{1}`: {2}\n",
						watch?.Elapsed.TotalMilliseconds,
						(msg as IMethodCallMessage)?.MethodName,
						e.ToString()
					);

		            if (e is TargetInvocationException && e.InnerException != null) {
						return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
					} 
					else throw e;
		        }
		    }
		}
		static void Main(string[] args) {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            try {
				string adress = "net.tcp://localhost:8080";
				var service = LoggingProxy.Create(new MainMessageService());
				
				ServiceHost host = new ServiceHost(service, new Uri[] { new Uri(adress) });
				var binding = new NetTcpBinding();
				host.AddServiceEndpoint(typeof(MessageService), binding, "client-query");
				host.Opened += (a, b) => Console.WriteLine("Server opened");
				host.Open();
			}
			catch(Exception e) {
				Console.Write("Error on startup:\n{0}", e);
			}

			Console.ReadKey();
		}
	}
}
