using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Communication {
	
	[Serializable] public class Customer {
		public string login, password;

		public Customer() { }

		public Customer(string login, string password) {
			this.login = login;
			this.password = password;
		}

		internal void unlogin() {
			login = null; password = null;
		}

		internal void setFrom(Customer o) {
			login = o.login;
			password = o.password;
		}
	}

	[Serializable] public struct AvailableOptionsResponse {
		public Dictionary<int, string> flightClasses;
		public List<City> cities;
	}

	[Serializable] public class MatchingFlightsParams {
		public string fromCode;
		public string toCode;
		public DateTime when;
	}

	[Serializable] public class AvailableFlight {
		public int id;
		public DateTime departureTime;
		public int arrivalOffsteMinutes;

		public string flightName;
		public string airplaneName;

		public Dictionary<int, FlightsOptions.Options> optionsForClasses;
		public SeatsScheme.Seats seatsScheme;

		public override string ToString() {
			var sb = new StringBuilder();

			sb.AppendFormat(
				"{{ id={0}, date={1}, name={2}, airplane={3}, arrivalOffsteMinutes={4} }}",
				id, departureTime, flightName, airplaneName, arrivalOffsteMinutes
			);

			return sb.ToString();
		}
	}

	[Serializable] public struct City {
		public string country, code;
		public string name { get; set; } //display in combobox requires property
	}

    [KnownType(typeof(Documents.Passport))]
    [KnownType(typeof(Documents.InternationalPassport))]
	[Serializable] public class Passanger {
		public string name;
		public string surname;
		public string middleName;
		public DateTime birthday;
		public Documents.Document document;

		public static bool operator==(Passanger f, Passanger s) {
			if (System.Object.ReferenceEquals(f, s)) return true;
			else if (((object)f == null) || ((object)s == null)) return false;
			else return f.name == s.name && f.surname == s.surname && f.middleName == s.middleName
					&& f.birthday == s.birthday && f.document == s.document;
		}

		public static bool operator!=(Passanger f, Passanger s) {
			return !(f == s);
		}
	}
	
	[Serializable] public class Either<TS, TF> {
		private bool isFirst;
		private TS first;
		private TF second;

		public static Either<TS, TF> Success(TS value) {
			return new Either<TS, TF>{ isFirst = true, first = value };
		}

		public static Either<TS, TF> Failure(TF value) {
			return new Either<TS, TF>{ isFirst = false, second = value };
		}

		public bool IsSuccess { get{ return isFirst; } }

		public TS s{ get{ return Success(); } }
		public TF f{ get{ return Failure(); } }

		public TS Success() {
			Debug.Assert(isFirst);
			return first;
		}

		public TF Failure() {
			Debug.Assert(!isFirst);
			return second;
		}

		public static bool operator true(Either<TS, TF> it) { return it.IsSuccess; }
		public static bool operator false(Either<TS, TF> it) { return !it.IsSuccess; }
	}

	[Serializable] public struct LoginError { 
		public string message; 

		public LoginError(string message) { this.message = message; }
	}

	[Serializable] public struct InputError { 
		public string message; 

		public InputError(string message) { this.message = message; }
	}

	[Serializable] public struct PassangerError { 
		int error;
		LoginError loginError;
		InputError inputError;

		public LoginError LoginError{ get{ Debug.Assert(error == 0); return loginError; } set{ error = 0; loginError = value; } }
		public InputError InputError{ get{ Debug.Assert(error == 1); return inputError; } set{ error = 1; inputError = value; } }

		public bool isLoginError{ get{ return error == 0; } }
		public bool isInputError{ get{ return error == 1; } }
	}

	[ServiceContract]
	public interface MessageService {
		[OperationContract] Either<object, LoginError> register(Customer customer);

		[OperationContract] Either<object, LoginError> logIn(Customer customer);	
		
		[OperationContract] AvailableOptionsResponse availableOptions();	

		[OperationContract] Either<List<AvailableFlight>, InputError> matchingFlights(MatchingFlightsParams p);	

		[OperationContract] Either<Dictionary<int, Passanger>, LoginError> getPassangers(Customer customer);

		[OperationContract] Either<int, PassangerError> addPassanger(Customer customer, Passanger passanger);

		[OperationContract] Either<int, PassangerError> replacePassanger(Customer customer, int index, Passanger passanger);
	}
}
