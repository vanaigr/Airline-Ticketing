using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AirlineTicketingServer {
	class LoginRegister {
		static readonly Random rand = new Random();
		static readonly int saltLength = 10;

		public static bool register(string login, string password) {
			using(
			var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
			
			using(
			var command = new SqlCommand("Customers.addCustomer", connection)) {

			var salt = new byte[saltLength];
			rand.NextBytes(salt);

			var passE = PasswordHashing.computePasswordHash(
				salt, Encoding.ASCII.GetBytes(password)
			);

			command.CommandType = System.Data.CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@Login", Encoding.ASCII.GetBytes(login));
			command.Parameters.AddWithValue("@Salt", salt);
			command.Parameters.AddWithValue("@PasswordHash", passE);
			command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
			command.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();

			var idParam = command.Parameters["@CustomerID"].Value;
			return idParam != DBNull.Value;

			}
			}
		}


		public enum LoginResultStatus { 
			OK, USER_NOT_EXISTS, WRONG_PASSWORD
		}

		public struct LoginResult {
			public LoginResultStatus status; 
			public int userID;
		}

		public static LoginResult login(string login, string password) {
			using(
			var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
			
			using(
			var command = new SqlCommand(
				@"SELECT TOP 1
				  [LoginInfo].[CustomerID] AS ID,
				  [LoginInfo].[Salt] AS Salt,
				  [LoginInfo].[PasswordHash] AS PasswordHash
				FROM [Customers].[LoginInfo] AS [LoginInfo]
				WHERE [LoginInfo].[Login] = @Login", 
				connection
			)) {
			command.CommandType = System.Data.CommandType.Text;
			command.Parameters.AddWithValue("@Login", Encoding.ASCII.GetBytes(login));

			connection.Open();
			var result = command.ExecuteReader();

			if(result.Read()) {
				var passEInDB = (byte[]) result["PasswordHash"];
				var salt = (byte[])result["Salt"];
				Debug.Assert(salt.Length == saltLength);
				var passE = PasswordHashing.computePasswordHash(
					salt, Encoding.ASCII.GetBytes(password)
				);

				bool passEqual = true;
				Debug.Assert(passEInDB.Length == passE.Length && passE.Length == 64);

				for(int i = 0; i < passEInDB.Length; i++) {
					if(passEInDB[i] != passE[i]) {
						passEqual = false;
						break;
					}
				}

				if(passEqual) return new LoginResult {
					status = LoginResultStatus.OK,
					userID = (int) result["ID"]
				};
				else return new LoginResult {
					status = LoginResultStatus.WRONG_PASSWORD
				};
			}
			else return new LoginResult {
				status = LoginResultStatus.USER_NOT_EXISTS
			};
			
			}
			}
		}

		public struct CheckResult {
			public bool ok;
			public string error;
		}
		
		static readonly string forbiddenSymbols = "[^A-Za-z0-9!@#$%%^&*();:?\\-=_+\\.]";
		static readonly string forbiddenSymbolsMessage = "содержать тлолько заглавные и строчные символы латиницы, цифры, или символы !@#$%%^&*();:?-=_+.";

		public static CheckResult checkLogin(string login) {
			var result = new StringBuilder();
			result.Append("Логин должен ");
			bool error = false;

			if(login.Length < 4 || login.Length > 64) {
				if(error) result.Append(", ");
				error = true;
				result.Append("иметь длину от 4 до 64 символов");
			}
			if(Regex.IsMatch(login, forbiddenSymbols)) {
				if(error) result.Append(", ");
				error = true;
				result.Append(forbiddenSymbolsMessage);
			}

			if(error) return new CheckResult{
				ok = false,
				error = result.ToString()
			};
			else return new CheckResult{
				ok = true,
				error = ""
			};
		}

		public static CheckResult checkPassword(string password) {
			var result = new StringBuilder();
			result.Append("Пароль должен ");
			bool error = false;

			int availableLength = PasswordHashing.maxLengh - saltLength;
			if(password.Length < 8 || password.Length > availableLength) {
				if(error) result.Append(", ");
				error = true;
				result.Append("иметь длину от 8 до ");
				result.Append(availableLength);
				result.Append(" символов");
			}
			if(Regex.IsMatch(password, forbiddenSymbols)) {
				if(error) result.Append(", ");
				error = true;
				result.Append(forbiddenSymbolsMessage);
			}

			if(error) return new CheckResult {
				ok = false,
				error = result.ToString()
			};
			else return new CheckResult {
				ok = true,
				error = ""
			};
		}
	}
}

