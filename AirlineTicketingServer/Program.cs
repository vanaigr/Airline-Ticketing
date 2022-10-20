using Communication;
using FlightsOptions;
using SeatsScheme;
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
			private readonly Dictionary<int, string> flightClasses;
			private readonly List<City> cities;

			public MainMessageService() {
				
				
				/*var sizes = new Point[]{ new Point(2, 4), new Point(25, 6) };
				var seatsClasses = new List<byte>(2*4 + 25 * 6);
				for(int i = 0; i < 2*4; i++) seatsClasses.Add(3);
				for(int i = 0; i < 25*6; i++) seatsClasses.Add(1);
				
				var seatsScheme = new SeatsScheme.SeatsScheme(sizes.Cast<SeatsScheme.Point>().GetEnumerator());
				var seatsAndClasses = new SeatsSchemeAndClasses{ scheme = seatsScheme, classes = seatsClasses.ToArray() };*/

				
				/*var economOptins = new Options{
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
				optionsForClasses.Add(1, economOptins);
				optionsForClasses.Add(3, busunessOptions);*/

				flightClasses = flightClasses = new Dictionary<int, string>();
				cities = new List<City>();

				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

				using(
				var selectClasses = new SqlCommand("select [Id], [Name] from [Flights].[Classes]")) {
				selectClasses.CommandType = System.Data.CommandType.Text;

				using(
				var selectCities = new SqlCommand("select [Id], [Name], [Country] from [Flights].[Cities]")) {
				selectCities.CommandType = System.Data.CommandType.Text;

				selectClasses.Connection = connection;
				connection.Open();
				using(
				var result = selectClasses.ExecuteReader()) {
				while(result.Read()) flightClasses.Add((int) result[0], (string) result[1]);
				}
				selectClasses.Dispose();

				selectCities.Connection = connection;
				using(
				var result = selectCities.ExecuteReader()) {
				while(result.Read()) cities.Add(new City{ 
					code = (string) result[0], 
					name = (string) result[1],
					country = (string) result[2] 
				});
				}
				}}

				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 1);
				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 2);
				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 3);

				/*{
					var connView = new SqlConnectionView(connection, false);
					using(
					var selectClassesCommand = new SqlCommand(
						@"insert into [Flights].[Airplanes]([Name], [SeatsCount], [SeatsScheme])
						values (@Name, @SeatsCount, @SeatsScheme)",
						connView.connection
					)) {
					selectClassesCommand.CommandType = System.Data.CommandType.Text;
					selectClassesCommand.Parameters.AddWithValue("@Name", "Airbus A320");
					selectClassesCommand.Parameters.AddWithValue("@SeatsCount", 158);
					selectClassesCommand.Parameters.AddWithValue("@SeatsScheme", BinarySeats.toBytes(seatsAndClasses));
	
					connView.Open();
					selectClassesCommand.ExecuteNonQuery();
					}
					connView.Dispose();
					Console.WriteLine("AAAAAAA");
				}*/

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
				public byte[] seatsAndClasses;
				public byte[] seatsOccupation;
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
					@"select [AvailableFlight], [FlightName], [AirplaneName], [DepartureDatetime], 
						[ArivalOffsetMinutes], [OptionsBin], [SeatsAndClasses], [SeatsOccupation]
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
					seatsAndClasses = (byte[]) result[6],
					seatsOccupation = (byte[]) result[7],
				});
				}}}

				var list = new List<AvailableFlight>(rawAvailableFlights.Count);
				for(int i = rawAvailableFlights.Count-1; i >= 0 ; i--) {
					var raf = rawAvailableFlights[i];
					rawAvailableFlights.RemoveAt(i);
					var sac = BinarySeats.fromBytes(raf.seatsAndClasses);
					list.Add(new AvailableFlight{
						id = raf.id,
						departureTime = raf.departureTime,
						arrivalOffsteMinutes = raf.arrivalOffsteMinutes,

						flightName = raf.flightName,
						airplaneName = raf.airplaneName,

						optionsForClasses = BinaryOptions.fromBytes(raf.optionsBin),
						seats = new SeatsScheme.Seats(sac.scheme, sac.classes, raf.seatsOccupation)
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
				return Either<int, LoginOrInputError>.Success(
					DatabasePassanger.replace(new SqlConnectionView(connection, true), userIdRes.Success(), index, passanger)
				);
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

			Either<object, LoginError> MessageService.removePassanger(Customer customer, int index) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
				if(!userIdRes.IsSuccess) return Either<object, LoginError>.Failure(userIdRes.Failure());
				DatabasePassanger.remove(new SqlConnectionView(connection, true), userIdRes.s, index);
				return Either<object, LoginError>.Success(null);
				}
			}

			private int[] calcPassangersSeatsIndices(
				SqlConnectionView cv, 
				Seats seats, byte[] originalOccupation,
				SeatAndOptions[] seatsAndOptions
			) {
				var occupation = (byte[]) originalOccupation.Clone();
				var seatsIndices = new int[seatsAndOptions.Length];
				for(int i = 0; i < seatsIndices.Length; i++) seatsIndices[i] = -1;

				var remainingSeatsIndices = new List<int>();

				for(int i = 0; i < seatsAndOptions.Length; i++) {
					var so = seatsAndOptions[i];
					if(so.useSeatIndex) {
						var occupied = Occupation.Occupied(occupation, seats.Size, so.seatIndex);
						if(!occupied) {
							seatsIndices[i] = so.seatIndex;
							Occupation.Occupy(occupation, seats.Size, so.seatIndex);
						}
					}
					else remainingSeatsIndices.Add(i);
				}

				{
					for(int i = 0; i < seats.Size; i++) {
						var seatClass = seats.Class(i);

						if(!Occupation.Occupied(occupation, seats.Size, i)) {
							for(int j = 0; j < remainingSeatsIndices.Count; j++) {
								var sIndex = remainingSeatsIndices[j];
								var s = seatsAndOptions[sIndex];
								if(s.seatClassId == seatClass) {
									seatsIndices[remainingSeatsIndices[j]] = i;
									remainingSeatsIndices.RemoveAt(j);
									Occupation.Occupy(occupation, seats.Size, i);
									break;
								}
							}
						}
					}
				}

				return seatsIndices;
			}

			struct SeatsData {
				public SeatsScheme.Seats seats;
				public Dictionary<int, FlightsOptions.Options> options;
				public int[] seatsIndices;
			};

			private Either<SeatsData, InputError> getDataForSeats(
				SqlConnectionView cv,
				int flightId, SeatAndOptions[] seatsAndOptions
			) {
				using(cv) {
				using(
				var command = new SqlCommand(
					@"select top 1
						[fo].[Data], 
						[ap].[SeatsScheme]
					from (
						select * from [Flights].[AvailableFlights] as [af]
						where [af].[Id] = @FlightId
					) as [af]
					inner join [Flights].[FlightsInfo] as [fi]
					on [af].[FlightInfo] = [fi].[Id]

					inner join [Flights].[FlightOptions] as [fo]
					on [fi].[Id] = [fo].[FlightId]

					inner join [Flights].[Airplanes] as [ap]
					on [fi].[AirplaneId] = [ap].[Id];

					select top 1 [afso].[Occupation]
					from [Flights].[AvailableFlightsSeatsOccupation] as [afso]
					where [afso].[AvailableFlight] = @FlightId",
					cv.connection
				)) {
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@FlightId", flightId);

				cv.Open();
				using(
				var result = command.ExecuteReader()) {

				if(!result.Read()) return Either<SeatsData, InputError>.Failure(new InputError(
					"Выбранного рейса не существует"
				));

				var optionsBin = (byte[]) result[0];
				var seatsSchemeBin = (byte[]) result[1];

				if(!result.NextResult()) return Either<SeatsData, InputError>.Failure(new InputError(
					"Выбранного рейса не существует" //not quite correct
				));

				if(!result.Read()) return Either<SeatsData, InputError>.Failure(new InputError(
					"Выбранного рейса не существует"
				));

				var occupation = (byte[]) result[0];

				result.Close();
				command.Dispose();

				var ssAndClases = BinarySeats.fromBytes(seatsSchemeBin);
				var seats = new Seats(ssAndClases.scheme, ssAndClases.classes, occupation);
				var seatsIndices = calcPassangersSeatsIndices(
					new SqlConnectionView(cv.connection, true),
					seats, occupation,
					seatsAndOptions
				);
											
				cv.Dispose();
				var options = BinaryOptions.fromBytes(optionsBin);

				return Either<SeatsData, InputError>.Success(new SeatsData {
					seats = seats, options = options, seatsIndices = seatsIndices
				});
				}}}
			}

			Either<SeatData[], InputError> MessageService.seatsData(int flightId, SeatAndOptions[] seatsAndOptions) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var result = getDataForSeats(new SqlConnectionView(
					connection,
					true
				), flightId, seatsAndOptions);
				connection.Dispose();
				
				if(!result.IsSuccess) return Either<SeatData[], InputError>.Failure(result.f);

				var seats = result.s.seats;
				var options = result.s.options;
				var seatsIndices = result.s.seatsIndices;

				var seatsData = new SeatData[seatsAndOptions.Length];

				for(int i = 0; i < seatsAndOptions.Length; i++) {
					var so = seatsAndOptions[i];
					int classId; 
					if(so.useSeatIndex) classId = seats.Class(so.seatIndex);
					else classId = so.seatClassId;

					var opt = options[classId];

					var hi = so.selectedOptions.baggageOptions.handLuggageIndex;
					var bi = so.selectedOptions.baggageOptions.baggageIndex;
					if(
						bi < 0 || bi >= opt.baggageOptions.baggage.Count ||
						hi < 0 || hi >= opt.baggageOptions.handLuggage.Count
					) return Either<SeatData[], InputError>.Failure(new InputError{
						message = "не заданы опции для багажа"
					});

					var baggageOption = opt.baggageOptions.baggage[bi];
					var handLuggageOption = opt.baggageOptions.handLuggage[hi];

					var sd = new SeatData(){
						baggageCost = baggageOption.costRub + handLuggageOption.costRub,
						unoccuppied = seatsIndices[i] != -1,
					};

					sd.totalCost = opt.basePriceRub + sd.baggageCost + 
							opt.servicesOptions.seatChoiceCostRub * (so.useSeatIndex ? 1 : 0);

					seatsData[i] = sd;
				}

				return Either<SeatData[], InputError>.Success(seatsData);
				}
			}

			public Either<object, LoginOrInputError> bookFlight(Customer customer, SelectedSeat[] selectedSeats, int flightId) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userIdRes = getUserId(new SqlConnectionView(connection, false), customer);
				if(!userIdRes.IsSuccess) return Either<object, LoginOrInputError>.Failure(new LoginOrInputError{ LoginError = userIdRes.Failure() });

				var seatsAndOptions = new SeatAndOptions[selectedSeats.Length];
				for(int i = 0; i < seatsAndOptions.Length; i++) {
					seatsAndOptions[i] = selectedSeats[i].seatAndOptions;
				}

				var result = getDataForSeats(
					new SqlConnectionView(connection, false), 
					flightId, seatsAndOptions
				);
				if(!result.IsSuccess) return Either<object, LoginOrInputError>.Failure(
					new LoginOrInputError{ InputError = result.f }
				);

				var seats = result.s.seats;
				var options = result.s.options;
				var seatsIndices = result.s.seatsIndices;
				
				for(int i = 0; i < seatsAndOptions.Length; i++) {
					var so = seatsAndOptions[i];
					int classId; 
					if(so.useSeatIndex) classId = seats.Class(so.seatIndex);
					else classId = so.seatClassId;

					var opt = options[classId];

					var hi = so.selectedOptions.baggageOptions.handLuggageIndex;
					var bi = so.selectedOptions.baggageOptions.baggageIndex;
					if(
						bi < 0 || bi >= opt.baggageOptions.baggage.Count ||
						hi < 0 || hi >= opt.baggageOptions.handLuggage.Count
					) return Either<object, LoginOrInputError>.Failure(
						new LoginOrInputError{ InputError = new InputError(
						"для пассажира " + i + " заданы некорректные опции багажа"
						) }
					);
				}

				using(
				var bookFlight = new SqlCommand("[Flights].[BookFlight]", connection)) {
				bookFlight.CommandType = CommandType.StoredProcedure;

				bookFlight.Parameters.AddWithValue("@AvailableFlight", flightId);
				bookFlight.Parameters.AddWithValue("@Customer", userIdRes.s);
				
                DataTable bookingTable = new DataTable();
                bookingTable.Columns.Add("Passanger", typeof(int));
                bookingTable.Columns.Add("SelectedSeat", typeof(int));
                bookingTable.Columns.Add("SelectedOptions", typeof(byte[]));

                for(int i = 0; i < selectedSeats.Length; i++) {
					var selectedSeat = selectedSeats[i];
					var index = seatsIndices[i];
					if(index == -1) return Either<object, LoginOrInputError>.Failure(
						new LoginOrInputError{ InputError = new InputError { message = 
							"Место пассажира " + i + " уже занято или не найдено"
						} }
					);
                    var dr = bookingTable.NewRow();
                    dr[0] = selectedSeat.passangerId;
					dr[1] = index;
                    dr[2] = BinaryOptions.toBytes(selectedSeat.seatAndOptions.selectedOptions);
                    bookingTable.Rows.Add(dr);
                }

				var tableParam = bookFlight.Parameters.AddWithValue("@BookSeats", bookingTable);
				tableParam.SqlDbType = SqlDbType.Structured;
				tableParam.TypeName = "[Flights].[BookSeat]";


				bookFlight.ExecuteNonQuery();

                return Either<object, LoginOrInputError>.Success(null);
                }
				}
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
