using ClientCommunication;
using Communication;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Server {
	public static class DatabaseAccount {
		static readonly Random rand = new Random();
		static readonly int saltLength = 10;

		class IncorrectPasswordLength : Exception {
			public IncorrectPasswordLength() {}
			public IncorrectPasswordLength(string message) : base(message) {}
			public IncorrectPasswordLength(string message, Exception inner) : base(message, inner) {}
		}

		class PasswordHashing {
			private static readonly byte[] salt2 = new byte[] { 151, 232, 178, 30, 118, 115, 133, 45, 198, 114 };
			private static System.Security.Cryptography.SHA512 sha512 
				= System.Security.Cryptography.SHA512.Create();

			public static readonly int maxLengh = (448/8) - salt2.Length;

			public static byte[] computePasswordHash(byte[] salt, byte[] password) {
				var arrLength = salt.Length + salt2.Length + password.Length;
				if(arrLength > (448/8) || password.Length == 0) {
					throw new IncorrectPasswordLength();
				}

				var passBytes = new byte[arrLength];
				salt.CopyTo(passBytes, 0);
				salt2.CopyTo(passBytes, salt.Length);
				password.CopyTo(passBytes, salt.Length + salt2.Length);

				return sha512.ComputeHash(passBytes);
			}
		}

		public static Either<Success, LoginError> checkAccountDataValid(Account c) {
			if(c.login == null) c.login = "";
			if(c.password == null) c.password = "";
			var invalidLogin = DatabaseAccount.checkLogin(c.login);
			var invalidPassword = DatabaseAccount.checkPassword(c.password);

			if(!invalidLogin.ok || !invalidPassword.ok) {
				var loginError = invalidLogin.errorMsg;
				var passError = invalidPassword.errorMsg;
				return Either<Success, LoginError>.Failure(new LoginError(
					loginError + (!invalidLogin.ok ? "\n" : "") + passError
				));
			}
			else return Either<Success, LoginError>.Success(new Success());
		}

		public static Either<int, LoginError> getUserId(SqlConnectionView cv, Account c) {
			using(cv) {
			var error = checkAccountDataValid(c);
			if(!error.IsSuccess) return Either<int, LoginError>.Failure(error.Failure());
			
			var loggedIn = DatabaseAccount.login(cv, c.login, c.password);
			cv.Dispose();

			if(loggedIn.status == DatabaseAccount.LoginResultStatus.USER_NOT_EXISTS) 
				 return Either<int, LoginError>.Failure(new LoginError("Пользователь с данным логином не найден"));
			else if(loggedIn.status == DatabaseAccount.LoginResultStatus.WRONG_PASSWORD) 
				return Either<int, LoginError>.Failure(new LoginError("Неправильный пароль"));

			return Either<int, LoginError>.Success(loggedIn.userID);
			}
		}

		public static bool register(SqlConnectionView cv, string login, string password) {
			using(cv) {
			using(
			var command = new SqlCommand("Customers.addCustomer", cv.connection)) {

			var salt = new byte[saltLength];
			rand.NextBytes(salt);

			var passE = PasswordHashing.computePasswordHash(
				salt, Encoding.ASCII.GetBytes(password)
			);
			Common.Debug2.AssertPersistent(passE.Length == 64);
			
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
				@"select top 1
				  [LoginInfo].[CustomerID],
				  [LoginInfo].[Salt],
				  [LoginInfo].[PasswordHash]
				from [Customers].[LoginInfo] as [LoginInfo]
				where [LoginInfo].[Login] = @Login", 
				cv.connection
			)) {
			command.CommandType = CommandType.Text;
			command.Parameters.AddWithValue("@Login", Encoding.ASCII.GetBytes(login));

			cv.Open();
			using(
			var result = command.ExecuteReader()) {

			if(!result.Read())  return new LoginResult {
				status = LoginResultStatus.USER_NOT_EXISTS
			};
			
			var id = (int) result[0];
			var salt = (byte[])result[1];
			var passEInDB = (byte[]) result[2];
			result.Close();
			command.Dispose();
			cv.Dispose();
			Common.Debug2.AssertPersistent(salt.Length == saltLength);

			var passE = PasswordHashing.computePasswordHash(
				salt, Encoding.ASCII.GetBytes(password)
			);

			Common.Debug2.AssertPersistent(passEInDB.Length == passE.Length && passE.Length == 64);

			bool passEqual = true;
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
			
			}}}
		}

		
		static readonly string forbiddenSymbols = "[^A-Za-z0-9!@#$%^&*();:?\\-=_+\\.]";
		static readonly string forbiddenSymbolsMessage = "содержать только заглавные и строчные символы латиницы, цифры, или символы !@#$%^&*();:?-=_+.";

		static Validation.CheckResult checkLogin(string login) {
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

		static Validation.CheckResult checkPassword(string password) {
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

