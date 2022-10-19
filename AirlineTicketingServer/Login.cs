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

		public static bool register(SqlConnectionView cv, string login, string password) {
			using(cv) {
			using(
			var command = new SqlCommand("Customers.addCustomer", cv.connection)) {

			var salt = new byte[saltLength];
			rand.NextBytes(salt);

			var passE = PasswordHashing.computePasswordHash(
				salt, Encoding.ASCII.GetBytes(password)
			);

			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@Login", Encoding.ASCII.GetBytes(login));
			command.Parameters.AddWithValue("@Salt", salt);
			command.Parameters.AddWithValue("@PasswordHash", passE);
			command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int));
			command.Parameters["@CustomerID"].Direction = ParameterDirection.Output;

			cv.Open();
			command.ExecuteNonQuery();
			command.Dispose();
			cv.Dispose();

			var idParam = command.Parameters["@CustomerID"].Value;
			return idParam != DBNull.Value;
			}}
		}


		public enum LoginResultStatus { 
			OK, USER_NOT_EXISTS, WRONG_PASSWORD
		}

		public struct LoginResult {
			public LoginResultStatus status; 
			public int userID;
		}

		public static LoginResult login(SqlConnectionView cv, string login, string password) {
			using(cv) {
			using(
			var command = new SqlCommand(
				@"SELECT TOP 1
				  [LoginInfo].[CustomerID] AS ID,
				  [LoginInfo].[Salt] AS Salt,
				  [LoginInfo].[PasswordHash] AS PasswordHash
				FROM [Customers].[LoginInfo] AS [LoginInfo]
				WHERE [LoginInfo].[Login] = @Login", 
				cv.connection
			)) {
			command.CommandType = System.Data.CommandType.Text;
			command.Parameters.AddWithValue("@Login", Encoding.ASCII.GetBytes(login));

			cv.Open();
			using(
			var result = command.ExecuteReader()) {

			if(result.Read()) {
				var passEInDB = (byte[]) result["PasswordHash"];
				var salt = (byte[])result["Salt"];
				var id = (int) result["ID"];
				result.Close();
				command.Dispose();
				cv.Dispose();
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
					userID = id
				};
				else return new LoginResult {
					status = LoginResultStatus.WRONG_PASSWORD
				};
			}
			else return new LoginResult {
				status = LoginResultStatus.USER_NOT_EXISTS
			};
			}}}
		}
		
		static readonly string forbiddenSymbols = "[^A-Za-z0-9!@#$%%^&*();:?\\-=_+\\.]";
		static readonly string forbiddenSymbolsMessage = "содержать тлолько заглавные и строчные символы латиницы, цифры, или символы !@#$%%^&*();:?-=_+.";

		public static Validation.CheckResult checkLogin(string login) {
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

			if(error) return new Validation.CheckResult{
				ok = false,
				errorMsg = result.ToString()
			};
			else return new Validation.CheckResult{
				ok = true,
				errorMsg = ""
			};
		}

		public static Validation.CheckResult checkPassword(string password) {
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

			if(error) return new Validation.CheckResult {
				ok = false,
				errorMsg = result.ToString()
			};
			else return new Validation.CheckResult {
				ok = true,
				errorMsg = ""
			};
		}
	}
}

