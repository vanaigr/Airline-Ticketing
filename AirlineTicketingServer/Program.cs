using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;

using Communication;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Linq;
using FlightsOptions;

namespace AirlineTicketingServer {
	
	class Program {
		
		[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
		class MainMessageService : MessageService {
			private readonly Dictionary<int, string> flightClasses;
			private readonly List<City> cities;

			class A {
				public List<int> l;
			}

			public MainMessageService() {
				/*var sizes = new SeatsScheme.Point[]{ new SeatsScheme.Point(2, 4), new SeatsScheme.Point(25, 6) };
				var seats = new List<SeatsScheme.SeatStatus>(2*4 + 25 * 6);
				for(int i = 0; i < 2*4; i++) seats.Add(
					new SeatsScheme.SeatStatus{ Class = 3, Occupied = false }
				);
				for(int i = 0; i < 25*6; i++) seats.Add(
					new SeatsScheme.SeatStatus{ Class = 1, Occupied = false }
				);
				
				var seatsScheme = new SeatsScheme.Seats(seats.GetEnumerator(), sizes.Cast<SeatsScheme.Point>().GetEnumerator());*/

				
				/*var economOptins = new Options{
					baggageOptions = new BaggageOptions{
						baggage = new List<Baggage> {
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
					}
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
					}
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

				selectCities.Connection = connection;
				using(
				var result = selectCities.ExecuteReader()) {
				while(result.Read()) cities.Add(new City{ 
					code = (string) result[0], 
					name = (string) result[1],
					country = (string) result[2] 
				});
				}

				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 1);
				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 3);
				//DatabaseOptions.writeToDatabaseFlightOptions(new SqlConnectionView(connection, false), optionsForClasses, 6);

				//DatabaseSeats.writeToDatabaseAirplanesSeats(new SqlConnectionView(connection, false), 15, seatsScheme);

				}}

				AvailableFlightsUpdate.checkAndUpdate(new SqlConnectionView(connection, true));
				}				
			}

			string testLoginPasswordValid(Customer c) {
				if(c.login == null) c.login = "";
				if(c.password == null) c.password = "";
				var invalidLogin = LoginRegister.checkLogin(c.login);
				var invalidPassword = LoginRegister.checkPassword(c.password);

				if(!invalidLogin.ok || !invalidPassword.ok) {
					var loginError = invalidLogin.errorMsg;
					var passError = invalidPassword.errorMsg;
					return loginError + "\n" + passError;
				}
				else return null;
			}

			public AvailableOptionsResponse availableOptions() {
				return new AvailableOptionsResponse {
					flightClasses = flightClasses,
					cities = cities
				};
			}

			public List<AvailableFlight> matchingFlights(MatchingFlightsParams p) {
				if(p.fromCode == null) throw new FaultException<object>(null, "Город вылета должен быть заполнен");
				if(p.toCode == null) throw new FaultException<object>(null, "Город прилёта должен быть заполнен");
				if(p.when == null) throw new FaultException<object>(null, "Дата вылета должа быть заполнена");
				
				var list = new List<AvailableFlight>();
				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

				using(
				var selectClasses = new SqlCommand(
					@"select [AvailableFlight], [FlightName], [AirplaneName], [DepartureDatetime], [ArivalOffsetMinutes], [OptionsBin], [SeatsBin]
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
				while(result.Read()) list.Add(new AvailableFlight{
					id = (int) result[0], flightName = (string) result[1],
					airplaneName = (string) result[2], departureTime = (DateTime) result[3],
					arrivalOffsteMinutes = (int) result[4],
					optionsForClasses = BinaryOptions.fromBytes((byte[])result[5]),
					seatsScheme = BinarySeats.fromBytes((byte[]) result[6])
				});
				}}}
				Console.WriteLine(list.Count);
				return list;
			}

			public void register(Customer c) {
				var error = testLoginPasswordValid(c);
				if(error != null) throw new FaultException<object>(null, error);

				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var registered = LoginRegister.register(new SqlConnectionView(connection, true), c.login, c.password);
				connection.Dispose();

				if(!registered) throw new FaultException<object>(
					null, 
					"Аккаунт с таким именем пользователя уже существует"
				);
				}
			}

			public void logIn(Customer c) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				getUserId(new SqlConnectionView(connection, true), c);
				}
			}

			private int getUserId(SqlConnectionView cv, Customer c) {
				using(cv) {
				var error = testLoginPasswordValid(c);
				if(error != null) throw new FaultException<object>(null, error);
				
				var loggedIn = LoginRegister.login(cv, c.login, c.password);
				cv.Dispose();

				if(loggedIn.status == LoginRegister.LoginResultStatus.USER_NOT_EXISTS) 
					 throw new FaultException<object>(null, "Пользователь с данным логином не найден");
				else if(loggedIn.status == LoginRegister.LoginResultStatus.WRONG_PASSWORD) 
					throw new FaultException<object>(null, "Неправильный пароль");

				return loggedIn.userID;
				}
			}

			int MessageService.addPassanger(Customer c, Passanger passanger) {
				var it = ValidatePassanger.validate(passanger);
				if(it.error) throw new FaultException<object>(null, it.errorMsg);

				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userId = getUserId(new SqlConnectionView(connection, false), c);
				return DatabasePassanger.add(new SqlConnectionView(connection, true), userId, passanger);
				}
			}

			int MessageService.replacePassanger(Customer c, int index, Passanger passanger) {
				var it = ValidatePassanger.validate(passanger);
				if(it.error) throw new FaultException<object>(null, it.errorMsg);

				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userId = getUserId(new SqlConnectionView(connection, false), c);
				return DatabasePassanger.replace(new SqlConnectionView(connection, true), userId, index, passanger);
				}
			}

			public Dictionary<int, Passanger> getPassangers(Customer c) {
				using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
				var userId = getUserId(new SqlConnectionView(connection, false), c);
				return DatabasePassanger.getAll(new SqlConnectionView(connection, true), userId);
				}
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
					Console.WriteLine(
						"Responding ({0}ms) to `{1}` with `{2}`",
						watch.Elapsed.TotalMilliseconds,
						methodCall.MethodName,
						result
					);
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
						if(e.InnerException is FaultException<object>) {
							return new ReturnMessage(e.InnerException, msg as IMethodCallMessage);
						}
		                else return new ReturnMessage(new FaultException<object>(null, "Неизвестная ошибка"), msg as IMethodCallMessage);
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
