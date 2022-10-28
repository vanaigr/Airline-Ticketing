using Communication;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace OperatorViewCommunication {	

	[KnownType(typeof(Documents.Passport))]
	[KnownType(typeof(Documents.InternationalPassport))]
	[Serializable] public sealed class BookedPassanger {
		public int seatIndex;
		public bool canceled;
		public string name;
		public string surname;
		public string middleName;
		public DateTime birthday;
		public Documents.Document document;
	}

	[Serializable] public sealed class FlightDetails {
		public FlightsSeats.SeatsScheme seats;
		public byte[] seatsClasses;
		public List<BookedPassanger> passangersAndSeats;
		public List<bool> passangerArrived;
	}


	[ServiceContract]
	public interface MessageService {
		[OperationContract] AvailableOptionsResponse availableOptions();

		[OperationContract] Either<List<AvailableFlight>, InputError> matchingFlights(MatchingFlightsParams matchingFlightsParams);

		[OperationContract] Either<FlightDetails, InputError> flightDetails(int flightId);

		[OperationContract] Either<Success, InputError> updateArrivaltatus(int flightId, Dictionary<int/*seatIndex*/, bool> seatArrived);
	}
}
