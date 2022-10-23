using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Validation;

namespace AirlineTicketingServer {
	static class ValidatePassanger {
		public static Validation.CheckResult validate(Communication.Passanger it) {
			var sb = new StringBuilder();
			var err = false;

			if(it.name == null || it.name.Length < 1) {
				err = true;
				sb.AC("имя должно быть заполнено");
			}
			if(it.surname == null || it.surname.Length < 1) {
				err = true;
				sb.AC("фамилия должна быть заполнена");
			}
			
			if(err) return  Validation.CheckResult.Err(sb.ToString());
			else return Validation.CheckResult.Ok();
		}
	}
	
	static class DatabasePassanger {
		struct RawPassanger {
			public int index;
			public string name, surname, middleName;
			public DateTime birthday;
			public byte[] documentBin;
		}
		public static Dictionary<int, Communication.Passanger> getAll(SqlConnectionView cv, int customerId) {
			using(cv) {
			using(
			var command = new SqlCommand(@"
				select [pi].[Id], [pi].[Name], [pi].[Surname], [pi].[MiddleName], [pi].[Birthday], [pi].[Document] 
				from [Customers].[Passanger] as [pi] 
				where [pi].[Customer] = @Customer
			", cv.connection)) {
			command.CommandType = System.Data.CommandType.Text;
			command.Parameters.AddWithValue("@Customer", customerId);

			var rawPassangers = new List<RawPassanger>();

			cv.Open();
			using(
			var reader = command.ExecuteReader()){
			while(reader.Read()) rawPassangers.Add(new RawPassanger{
				index = (int) reader[0],
				name = (string) reader[1],
				surname = (string) reader[2],
				middleName = (string) reader[3],
				birthday = (DateTime) reader[4],
				documentBin = (byte[]) reader[5]
			});
			reader.Close();
			command.Dispose();
			cv.Dispose();

			var passangers = new Dictionary<int, Communication.Passanger>(rawPassangers.Count);
			foreach(var rp in rawPassangers) {
				using(
				var ms = new MemoryStream(rp.documentBin, false)) {
				var document = (Documents.Document) new BinaryFormatter().Deserialize(ms);
				passangers.Add(rp.index, new Communication.Passanger{
					name = rp.name, surname = rp.surname, middleName = rp.middleName,
					birthday = rp.birthday, document = document
				});
				}
			}
			return passangers;
			}}}
		}

		public static int replace(SqlConnectionView cv, int customerId, int index, Communication.Passanger passanger) {
			using(cv) {
			using(
			var ms = new MemoryStream()) {
			new BinaryFormatter().Serialize(ms, passanger.document);
			var documentBin = ms.ToArray();
			ms.Dispose();

			using(
			var command = new SqlCommand("[Customers].[ReplacePassanger]", cv.connection)) {
			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@Birthday", passanger.birthday);
			command.Parameters.AddWithValue("@Document", documentBin);
			command.Parameters.AddWithValue("@Name", passanger.name);
			command.Parameters.AddWithValue("@Surname", passanger.surname);
			command.Parameters.AddWithValue("@MiddleName", passanger.middleName);
			command.Parameters.AddWithValue("@CustomerId", customerId);
			command.Parameters.AddWithValue("@Index", index);
			command.Parameters.Add("@NewIndex", System.Data.SqlDbType.Int);
			command.Parameters["@NewIndex"].Direction = System.Data.ParameterDirection.Output;

			cv.Open();
			command.ExecuteNonQuery();
			return (int) command.Parameters["@NewIndex"].Value;
			}}}
		}

		public static int add(SqlConnectionView cv, int customerId, Communication.Passanger passanger) {
			using(cv) {
			using(
			var ms = new MemoryStream()) {
			new BinaryFormatter().Serialize(ms, passanger.document);
			var documentBin = ms.ToArray();
			ms.Dispose();

			using(
			var command = new SqlCommand("[Customers].[AddPassanger]",cv.connection)) {
			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@Birthday", passanger.birthday);
			command.Parameters.AddWithValue("@Document", documentBin);
			command.Parameters.AddWithValue("@Name", passanger.name);
			command.Parameters.AddWithValue("@Surname", passanger.surname);
			command.Parameters.AddWithValue("@MiddleName", passanger.middleName);
			command.Parameters.AddWithValue("@CustomerId", customerId);
			var result = command.Parameters.Add("@NewId", System.Data.SqlDbType.Int);
			result.Direction = System.Data.ParameterDirection.Output;

			cv.Open();
			command.ExecuteNonQuery();
			return (int) result.Value;
			}}}
		}

		private static readonly Regex foreignKeyRegex = new Regex(
			"The (.+) statement conflicted with the (.+) constraint \"(.+)\"\\. The conflict occurred in database \"(.+)\", table \"(.+)\"\\."
		);

		public static CheckResult remove(SqlConnectionView cv, int customerId, int index) {
			using(cv) {
			using(
			var command = new SqlCommand("[Flights].[RemovePassanger]", cv.connection)) {
			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@Customer", customerId);
			command.Parameters.AddWithValue("@Index", index);
			var resultParam = command.Parameters.Add("@Result", System.Data.SqlDbType.Int);
			resultParam.Direction = System.Data.ParameterDirection.ReturnValue;

			cv.Open();
			try {
				command.ExecuteNonQuery();
				return new CheckResult{ ok = false, errorMsg = "Ошибка удаления" };
			} 
			catch(SqlException e) {
				command.Dispose();
				cv.Dispose();

				var match = foreignKeyRegex.Match(e.Message);
				if(match.Success && match.Groups[2].Value == "REFERENCE" && match.Groups[3].Value == "AvailableFlights_ForeignCustomer"
					&& match.Groups[5].Value == "Flights.AvailableFlightsSeats") {
					return new CheckResult{ ok = false, errorMsg = "Удаление невозможно, так как данный пассажир зарегестрирован на один или более рейсов" };
				}
				else throw e;
			}

			}}
		}
	}
}
