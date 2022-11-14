using Communication;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace OperatorCommunication {	
	[Serializable] public sealed class BookedPassanger {
		public int seatIndex;
		public bool canceled;
		public string name;
		public string surname;
		public string middleName;
		public string pnr;
		public DateTime birthday;
		public Documents.Document document;
	}

	[Serializable] public sealed class FlightDetails {
		public FlightsSeats.SeatsScheme seats;
		public byte[] seatsClasses;
		public List<BookedPassanger> passangersAndSeats;
		public List<bool> passangerArrived;
	}

	[Serializable] public sealed class PassangerSearchParams {
		public string name, surname, middleName, pnr;
	}


	[ServiceContract]
	public interface OperatorService {
		[OperationContract] Parameters parameters();

		[OperationContract] Either<List<Flight>, InputError> findMatchingFlights(MatchingFlightsParams matchingFlightsParams);

		[OperationContract] Either<FlightDetails, InputError> getFlightDetails(int flightId);

		[OperationContract] Either<Success, InputError> updateArrivalStatus(int flightId, Dictionary<int/*seatIndex*/, bool> seatArrived);

		[OperationContract] Either<List<PassangerBookedFlight>, InputError> getPassangerBookedFlights(PassangerSearchParams ps);

		[OperationContract] Either<Success, InputError> deleteBookedSeat(string pnr);
	}
}
