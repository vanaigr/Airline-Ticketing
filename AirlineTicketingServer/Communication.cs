using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Communication {
	[Serializable] public struct Response<Result> {
		public string message;
		public Result result;
		public bool statusOk { get{ return result != null; } }

		public Response(string message, Result result) {
			this.message = message;
			this.result = result;
		}

		public override string ToString() {
			return "{ \"" + message + "\", " + (result == null ? "null" : result.GetType().Name) + " }";
		}
	}
	
	[Serializable] public sealed class RegisterResponse {
	}
	
	[Serializable] public sealed class TestLoginResponse {
	}

	[Serializable] public sealed class AvailableOptionsParams {
	}
	[Serializable] public sealed class AvailableOptionsResponse {
		public Dictionary<int, string> flightClasses;
	}

	[Serializable] public sealed class MatchingFlightsParams {
		string from;
		string to;
		DateTime when;
		int adultCount;
		int childrenCount;
		int babyCount;
	}
	[Serializable] public sealed class MatchingFlightsResponse {
		List<AvailableFlight> availableFlights;
	}
	[Serializable] public struct AvailableFlight {
		int id;
		public DateTime departureTime;

		string name;
		string airplaneName;
		List<String> route;
	}


	
	[ServiceContract]
	public interface MessageService {
		[FaultContract(typeof(object))] [OperationContract] 
		Response<RegisterResponse> register(string login, string password);


		[FaultContract(typeof(object))] [OperationContract] 
		Response<TestLoginResponse> testLogin(string login, string password);		
		

		[FaultContract(typeof(object))] [OperationContract] 
		Response<AvailableOptionsResponse> availableOptions();	


		[FaultContract(typeof(object))] [OperationContract] 
		Response<MatchingFlightsResponse> matchingFlights(MatchingFlightsParams p);	
	}
}
