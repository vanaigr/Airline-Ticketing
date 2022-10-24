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
			
			private Either<SeatCost[], InputError> calculateSeatsCosts(
				SqlConnectionView cv, int availableFlightId, SeatAndOptions[] seats
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

				if(!result.Read()) return Either<SeatCost[], InputError>.Failure(new InputError(
					"Выбранного рейса не существует"
				));

				var optionsBin = (byte[]) result[0];

				result.Close();
				command.Dispose();
				cv.Dispose();

				var options = BinaryOptions.fromBytes(optionsBin);

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

					var baggageOption = opt.baggageOptions.baggage[bi];
					var handLuggageOption = opt.baggageOptions.handLuggage[hi];

					var sd = new SeatCost(){
						basePrice = opt.basePriceRub,
						baggageCost = baggageOption.costRub + handLuggageOption.costRub,
						seatCost = opt.servicesOptions.seatChoiceCostRub * (seat.seatIndex != null ? 1 : 0)
					};

					sd.totalCost = sd.basePrice + sd.baggageCost + sd.seatCost;

					seatsCost[i] = sd;
				}

				return Either<SeatCost[], InputError>.Success(seatsCost);
				}}}
			}

			Either<SeatCost[], InputError> MessageService.seatsData(int flightId, SeatAndOptions[] seatsAndOptions) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var result = calculateSeatsCosts(new SqlConnectionView(connection, true), flightId, seatsAndOptions);
				connection.Dispose();
				return result;
				}
			}

			public Either<object, LoginOrInputError> bookFlight(Customer? customer, Dictionary<int, Passanger> tempPassangers, SelectedSeat[] selectedSeats, int flightId) {
				if(selectedSeats.Length == 0) return Either<object, LoginOrInputError>.Failure(new LoginOrInputError{
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
				var costsResult = calculateSeatsCosts(
					new SqlConnectionView(connection, false), flightId, seatsAndOptions
				);
				if(!costsResult.IsSuccess) return Either<object, LoginOrInputError>.Success(
					new LoginOrInputError{ InputError = costsResult.f }
				);

				//try logging user in
				int? userId;
				if(customer != null) {
					var userIdRes = getUserId(new SqlConnectionView(connection, false), (Customer) customer);
					if(!userIdRes.IsSuccess) return Either<object, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });
					userId = userIdRes.s;
				}
				else userId = null;

				using(
				var bookFlight = new SqlCommand("[Flights].[BookFlight]", connection)) {
				bookFlight.CommandType = CommandType.StoredProcedure;

				var customerParam = bookFlight.Parameters.Add("@Customer", SqlDbType.Int);
				if(userId != null) customerParam.Value = userId;
				else customerParam.Value = DBNull.Value;
				bookFlight.Parameters.AddWithValue("@AvailableFlight", flightId);
				var errorSeatParam = bookFlight.Parameters.Add("@ErrorSeatId", SqlDbType.Int);
				var errorPassangerParam = bookFlight.Parameters.Add("@ErrorTempPassangerId ", SqlDbType.Int);
				errorSeatParam.Direction = ParameterDirection.Output;
				errorPassangerParam.Direction = ParameterDirection.Output;

				var bookSeatsParam = bookFlight.Parameters.AddWithValue("@BookSeats", bookingTable);
				bookSeatsParam.SqlDbType = SqlDbType.Structured;
				bookSeatsParam.TypeName = "[Flights].[BookSeat]";
				
				var tempPassangersParam = bookFlight.Parameters.AddWithValue("@TempPassangers", tempPassangersTable);
				tempPassangersParam.SqlDbType = SqlDbType.Structured;
				tempPassangersParam.TypeName = "[Flights].[TempPassangers]";
				
				//book flights
				bookFlight.ExecuteNonQuery();
				
				var es = Validation.ErrorString.Create();

				if(!(errorSeatParam.Value is DBNull)) { 
					var errorSeat = (int) errorSeatParam.Value;
					es.ac("место пассажира " + errorSeat + " уже занято или не существует");
				}
				if(!(errorPassangerParam.Value is DBNull)) { 
					var errorPassanger = (int) errorPassangerParam.Value;
					es.ac("Пассажир " + errorPassanger + " не может быть добавлен");
				}
				
				if(!es.Error) return Either<object, LoginOrInputError>.Success(null);
				else return Either<object, LoginOrInputError>.Failure(new LoginOrInputError{
					InputError = new InputError(es.Message)
				});
				}}
			}

			private struct RawSeat {
				public short index;
				public byte classId;
				public bool occupied;
			};

			public Either<Seats, InputError> seatsForFlight(int flightId) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				using(
				var command = new SqlCommand(@"
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
					) as [afs]

					inner join [Flights].[AirplanesSeats] as [aps]
					on [afs].[Airplane] = [aps].[Airplane] and [afs].[SeatIndex] = [aps].[SeatIndex]
					order by [afs].[SeatIndex] ASC;
				", connection)) {
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@AvailableFlight", flightId);
				
				var rawSeats = new List<RawSeat>();

				connection.Open();
				using(
				var result = command.ExecuteReader()) {

				if(!result.Read()) return Either<Seats, InputError>.Failure(new InputError(
					"Данный рейс не существует"
				));
				var seatsSchemeBin = (byte[]) result[0];

				Debug.Assert(result.NextResult());

				while(result.Read()) rawSeats.Add(new RawSeat{
					index = (short) result[0],
					classId = (byte) result[1],
					occupied = (bool) result[2]
				});

				result.Close();
				command.Dispose();
				connection.Close();

				var seatsScheme = BinarySeats.fromBytes(seatsSchemeBin);

				Debug.Assert(seatsScheme.SeatsCount == rawSeats.Count);

				var classes = new byte[seatsScheme.SeatsCount];
				for(int i = 0; i < classes.Length; i++) {
					Debug.Assert(rawSeats[i].index == i);
					classes[i] = rawSeats[i].classId;
				}

				var occupation = new bool[seatsScheme.SeatsCount];
				for(int i = 0; i < occupation.Length; i++) {
					Debug.Assert(rawSeats[i].index == i);
					occupation[i] = rawSeats[i].occupied;
				}

				return Either<Seats, InputError>.Success(new Seats(
					seatsScheme, ((IEnumerable<byte>) classes).GetEnumerator(), 
					((IEnumerable<bool>) occupation).GetEnumerator()
				));

				}}}
			}

			/*public Either<Seats, InputError> availableFlightDetails(int availableFlightId) {
				byte[] seatsSchemeBin = null;
				var occupiedSeatsIndices = new List<int>();

				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				using(
				var selectClasses = new SqlCommand(
					@"
					select [ap].[SeatsScheme]
					from (
						select [af].[FlightInfo] as [Id]
						from [Flights].[AvailableFlights] as [af]
						where [af].[Id] = @AvailableFlight
					) as [fiId]
					inner join [Flights].[FlightsInfo] as [fi]
					on [fi].[Id] = [fiId].[Id]
					inner join [Flights].[Airplanes] as [ap]
					on [ap].[Id] = [fi].[AirplaneID];
					
					select [afs].[SeatIndex]
					from [Flights].[AvailableFlightsSeats] as [afs]
					where [afs].[AvailableFligth] = @AvailableFlight;",
					connection
				)) {
				selectClasses.CommandType = CommandType.Text;
				selectClasses.Parameters.AddWithValue("@AvailableFlight", availableFlightId);

				connection.Open();
				using(
				var result = selectClasses.ExecuteReader()) {
				//read seatsScheme
				if(result.Read()) seatsSchemeBin = (byte[]) result[0];

				//read occupied seats
				if(!result.NextResult()) throw new Exception("No second result");
				while(result.Read()) occupiedSeatsIndices.Add((int) result[0]);
				}}}

				if(seatsSchemeBin == null) return Either<Seats, InputError>.Failure(new InputError{ message = "Данного рейса не существует" });

				var scheme = BinarySeatsScheme.fromBytes(seatsSchemeBin);
				
				return Either<Seats, InputError>.Success(
					new SeatsScheme.Seats(scheme, occupiedSeatsIndices)
				);
			}*/
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
