using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;

using Communication;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace AirlineTicketingServer {
	
	class Program {
		
		[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
		class MainMessageService : MessageService {
			private readonly Dictionary<int, string> flightClasses = new Dictionary<int, string>();

			public MainMessageService() {
				using(
				var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

				using(
				var command = new SqlCommand("SELECT [Id], [Name] From [Flights].[Classes]", connection)) {
				command.CommandType = System.Data.CommandType.Text;

				connection.Open();
				var result = command.ExecuteReader();
				while(result.Read()) flightClasses.Add((int) result[0], (string) result[1]);
				connection.Close();

				}

				AvailableFlightsUpdate.checkAndUpdate(new SqlConnectionView(connection, true));
				}				
			}

			string testLoginPasswordValid(string login, string password) {
				if(login == null) login = "";
				if(password == null) password = "";
				var invalidLogin = LoginRegister.checkLogin(login);
				var invalidPassword = LoginRegister.checkPassword(password);

				if(!invalidLogin.ok || !invalidPassword.ok) {
					var loginError = invalidLogin.error;
					var passError = invalidPassword.error;
					return loginError + "\n" + passError;
				}
				else return null;
			}

			public Response<AvailableOptionsResponse> availableOptions() {
				return new Response<AvailableOptionsResponse>(
					"", new AvailableOptionsResponse {
						flightClasses = this.flightClasses 
					}
				);
			}

			public Response<MatchingFlightsResponse> matchingFlights(MatchingFlightsParams p) {
				throw new NotImplementedException(); 
			}

			public Response<RegisterResponse> register(string login, string password) {
				var error = testLoginPasswordValid(login, password);
				if(error != null) return new Response<RegisterResponse>(error, null);

				var registered = LoginRegister.register(login, password);

				if(registered) return new Response<RegisterResponse>(
					"Аккаунт зарегестрирован", new RegisterResponse()
				);
				else return new Response<RegisterResponse>(
					"Аккаунт с таким именем пользователя уже существует", null
				);
			}

			public Response<TestLoginResponse> testLogin(string login, string password) {
				var error = testLoginPasswordValid(login, password);
				if(error != null) return new Response<TestLoginResponse>(error, null);

				var loggedIn = LoginRegister.login(login, password);

				if(loggedIn.status == LoginRegister.LoginResultStatus.USER_NOT_EXISTS) return new Response<TestLoginResponse>(
					"Пользователь с данным логином не найден", null
				);
				else if(loggedIn.status == LoginRegister.LoginResultStatus.WRONG_PASSWORD) return new Response<TestLoginResponse>(
					"Неправильный пароль", null
				);

				var userId = loggedIn.userID;

				return new Response<TestLoginResponse>(
					"Вход выполнен", new TestLoginResponse()
				);
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
						"Responding ({0}ms) to {1} with code {2}",
						watch.Elapsed.TotalMilliseconds,
						methodCall.MethodName,
						result
					);
		            return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
		        }
		        catch (Exception e) {
					watch.Stop();
					Console.WriteLine(
						"Error while responding ({0} ms) to {1}. Responsding with error {2}",
						watch?.Elapsed.TotalMilliseconds,
						(msg as IMethodCallMessage)?.MethodName,
						e.ToString()
					);

		            if (e is TargetInvocationException && e.InnerException != null) {
		                return new ReturnMessage(new FaultException<object>(null), msg as IMethodCallMessage);
					} 
					else throw e;
		        }
		    }
		}
		static void Main(string[] args) {
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
