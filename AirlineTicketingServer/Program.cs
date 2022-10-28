using Communication;
using FlightsOptions;
using FlightsSeats;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

namespace AirlineTicketingServer {
	partial class Program {
		internal static readonly string[] flightClasses = new string[]{ "Эконом", "Комфорт", "Бизнес", "Первый класс" };
		internal static readonly List<City> cities = new List<City>();

		static Program() {
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
		static void Main(string[] args) {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

			ServiceHost clientHost = null;
			ServiceHost operatorHost = null;

            try {
				string adress = "net.tcp://localhost:8080";
				var clientService = LoggingProxy<ClientCommunication.MessageService>.Create("`Client server`", new MainClientMessageService());
				var operatorService = LoggingProxy<OperatorViewCommunication.MessageService>.Create("`Operator server`",new MainOperatorMessageService());
				
				clientHost = new ServiceHost(clientService, new Uri[] { new Uri(adress) });
				clientHost.AddServiceEndpoint(typeof(ClientCommunication.MessageService), new NetTcpBinding(), "client-query");
				clientHost.Opened += (a, b) => Console.WriteLine("Client server opened");
				clientHost.Open();

				operatorHost = new ServiceHost(operatorService, new Uri[] { new Uri(adress) });
				operatorHost.AddServiceEndpoint(typeof(OperatorViewCommunication.MessageService), new NetTcpBinding(), "operator-view");
				operatorHost.Opened += (a, b) => Console.WriteLine("Operator server opened");
				operatorHost.Open();

				Console.ReadKey();
			}
			catch(Exception e) {
				Console.Write("Error on startup:\n{0}", e);
			}
			finally {
				if(clientHost != null) clientHost.Close();
				if(operatorHost != null) operatorHost.Close();
			}
		}
	}
}
