using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Diagnostics;
using System.Collections.Generic;

using Communication;

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

			public Response execute(Params p) {
				if(p.action is QueryAvailableOptionsAction) {
					return new Response {
						statusOk = true, message = "",
						result = new AvailableOptionsResult { flightClasses = this.flightClasses }
					};
				}

				if(p.login == null) p.login = "";
				if(p.password == null) p.password = "";
				var invalidLogin = LoginRegister.checkLogin(p.login);
				var invalidPassword = LoginRegister.checkPassword(p.password);

				if(!invalidLogin.ok || !invalidPassword.ok) {
					var loginError = invalidLogin.error;
					var passError = invalidPassword.error;
					return new Response { statusOk = false, message = loginError + "\n" + passError };
				}

				if(p.action is RegisterAction) {
					var registered = LoginRegister.register(p.login, p.password);

					if(registered) return new Response {
						statusOk = true,
						message = "Аккаунт зарегестрирован"
					};
					else return new Response {
						statusOk = false,
						message = "Аккаунт с таким именем пользователя уже существует"
					};
				}
				else {
					var loggedIn = LoginRegister.login(p.login, p.password);

					if(loggedIn.status == LoginRegister.LoginResultStatus.USER_NOT_EXISTS) return new Response {
						statusOk = false,
						message = "Пользователь с данным логином не найден"
					};
					else if(loggedIn.status == LoginRegister.LoginResultStatus.WRONG_PASSWORD) return new Response {
						statusOk = false,
						message = "Неправильный пароль"
					};

					var userId = loggedIn.userID;

					return new Response {
						statusOk = true,
						message = "Вход выполнен"
					};
				}
			}	
		}

		[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
		class LoggingTimingMessageService : MessageService {
			MessageService innerService;

			public LoggingTimingMessageService(MessageService innerService) {
				this.innerService = innerService;
			}

			public Response execute(Params p) {
				var watch = new System.Diagnostics.Stopwatch();
				
				try {
					watch.Start();
					var result = innerService.execute(p);
					watch.Stop();
					Console.WriteLine(
						"Responding ({3}ms) to {0} with code {1} and {2}",
						p.action == null ? "no cation" : "action = " + p.action.GetType().Name,
						result.statusOk ? "OK" : "ERROR",
						result.result == null ? "null" : result.result.GetType().Name,
						watch.Elapsed.TotalMilliseconds
					);
					return result;
				}
				catch(Exception e) {
					watch.Stop();
					var result = new Response { statusOk = false, message = "Неизвестная ошибка" };
					Console.WriteLine(
						"Error while responding ({4}ms) to {0}. Responsding with code {1} and {2} for error {3}",
						p.action == null ? "no cation" : "action = " + p.action.GetType().Name,
						result.statusOk ? "OK" : "ERROR",
						result.result == null ? "null" : result.result.GetType().Name,
						e.ToString(),
						watch.Elapsed.TotalMilliseconds
					);
					return result;
				}
			}
		}

		static void Main(string[] args) {
			try {
				string adress = "http://localhost:8080/test";
				var service = new LoggingTimingMessageService(new MainMessageService());
				
				ServiceHost host = new ServiceHost(service, new Uri[] { new Uri(adress) });
				var binding = new BasicHttpBinding();
				host.AddServiceEndpoint(typeof(MessageService), binding, "");
				host.Open();
			}
			catch(Exception e) {
				Console.Write("Error on startup:\n{0}", e);
			}

			Console.ReadKey();
		}
	}
}
