using Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;


namespace ClientCommunication {
	[Serializable] public struct Customer {
		public string login, password;

		public Customer(string login, string password) {
			this.login = login;
			this.password = password;
		}
	}

	[Serializable] public struct SelectedSeat {
		public SeatAndOptions seatAndOptions;
		public bool fromTempPassangers;
		public int passangerId;
	}

	[Serializable]
	public struct SeatAndOptions {
		public FlightsOptions.SelectedOptions selectedOptions;
		public int? seatIndex;
		public int selectedSeatClass;
	}

	[Serializable]
	public struct SeatCost {
		public int basePrice;
		public int seatCost;
		public int baggageCost;
		public int totalCost;
	}

	[Serializable]
	public sealed class BookedFlight {
		public int? bookedFlightId;
		public AvailableFlight availableFlight;
		public string fromCode, toCode;
		public int bookedPassangerCount;
		public DateTime bookingFinishedTime;
	}

	[Serializable]
	public sealed class BookedFlightDetails {
		public BookedSeatInfo[] bookedSeats;
		public SeatAndOptions[] seatsAndOptions;
		public FlightsSeats.Seats seats;
	}

	[Serializable]
	public struct BookingFlightResult {
		public int? customerBookedFlightId;
		public DateTime bookingFinishedTime;
		public BookedSeatInfo[] seatsInfo;
	}

	[Serializable]
	public struct BookedSeatInfo {
		public int passangerId;
		public int selectedSeat;
		public SeatCost cost;
	}

	[Serializable]
	public struct LoginError {
		public string message;

		public LoginError(string message) { this.message = message; }
	}

	[Serializable]
	public struct LoginOrInputError {
		int error;
		LoginError loginError;
		InputError inputError;

		public LoginError LoginError { get { Debug.Assert(error == 0); return loginError; } set { error = 0; loginError = value; } }
		public InputError InputError { get { Debug.Assert(error == 1); return inputError; } set { error = 1; inputError = value; } }

		public bool isLoginError { get { return error == 0; } }
		public bool isInputError { get { return error == 1; } }
	}

	[ServiceContract]
	public interface MessageService {
		[OperationContract] AvailableOptionsResponse availableOptions();


		[OperationContract] Either<object, LoginError> register(Customer customer);

		[OperationContract] Either<object, LoginError> logIn(Customer customer);


		[OperationContract] Either<List<AvailableFlight>, InputError> matchingFlights(MatchingFlightsParams p);

		[OperationContract] Either<FlightsSeats.Seats, InputError> seatsForFlight(int flightId);


		[OperationContract] Either<Dictionary<int, Passanger>, LoginError> getPassangers(Customer customer);

		[OperationContract] Either<int, LoginOrInputError> addPassanger(Customer customer, Passanger passanger);

		[OperationContract] Either<int, LoginOrInputError> replacePassanger(Customer customer, int index, Passanger passanger);

		[OperationContract] Either<object, LoginOrInputError> removePassanger(Customer customer, int index);


		[OperationContract] Either<SeatCost[], InputError> seatsData(int flightId, SeatAndOptions[] seats);

		[OperationContract] Either<BookingFlightResult, LoginOrInputError> bookFlight(Customer? customer, SelectedSeat[] selectedSeats, Dictionary<int, Passanger> tempPassangers, int flightId);

		[OperationContract] Either<BookedFlight[], LoginError> getBookedFlights(Customer customer);

		[OperationContract] Either<BookedFlightDetails, LoginOrInputError> getBookedFlightDetails(Customer customer, int bookedFlightId);

		[OperationContract] Either<int/*remainingPassangersCount*/, LoginOrInputError> deleteBookedFlightSeat(Customer customer, int bookedFlightId, int seatIndex);
	}
}