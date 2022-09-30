using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Communication {


	[Serializable()]
	public class Response {
		public bool statusOk;
		public string message;
		public object result;
	}

	[Serializable] class AvailableOptionsResult {
		public Dictionary<int, string> flightClasses;
	}

	[Serializable]
	class Flight {
		string name;

	}

	//[Serializable] class MatchingFlightsResult {
	//	List<>
	//}

	
	[Serializable()]
	public class Params {
		public string login;
		public string password;
		public object action;
	}

	[Serializable] class RegisterAction {}
	[Serializable] class QueryAvailableOptionsAction {}

	[Serializable] class FindFlightsByCriteriaAction {
		string from;
		string to;
		DateTime when;
		int adultCount;
		int childrenCount;
		int babyCount;
	}

	[ServiceKnownType(typeof(RegisterAction))]
	[ServiceKnownType(typeof(QueryAvailableOptionsAction))]
	[ServiceKnownType(typeof(AvailableOptionsResult))]
	[ServiceKnownType(typeof(FindFlightsByCriteriaAction))]
	//[ServiceKnownType(typeof(MatchingFlightsResult))]
	[ServiceKnownType(typeof(Flight))]
	[ServiceContract]
	public interface MessageService {
		[OperationContract]
		Response execute(Params p);
	}
}
