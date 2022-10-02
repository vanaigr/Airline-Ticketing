using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Communication {

	[Serializable] public struct AvailableOptionsResponse {
		public Dictionary<int, string> flightClasses;
		public List<City> cities;
	}

	[Serializable] public struct MatchingFlightsParams {
		string fromCode;
		string toCode;
		DateTime when;
		int adultCount;
		int childrenCount;
		int babyCount;
	}

	[Serializable] public struct AvailableFlight {
		int id;
		public DateTime departureTime;

		string name;
		string airplaneName;
		List<String> route;
	}

	[Serializable] public struct City {
		public string country, code;
		public string name { get; set; } //display in combobox requires property
	}

	[ServiceContract]
	public interface MessageService {
		[FaultContract(typeof(object))] [OperationContract] 
		void register(string login, string password);


		[FaultContract(typeof(object))] [OperationContract] 
		void logIn(string login, string password);		
		

		[FaultContract(typeof(object))] [OperationContract] 
		AvailableOptionsResponse availableOptions();	


		[FaultContract(typeof(object))] [OperationContract] 
		List<AvailableFlight> matchingFlights(MatchingFlightsParams p);	
	}
}
