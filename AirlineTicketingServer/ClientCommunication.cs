using Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;


namespace ClientCommunication {
	[Serializable] public struct Account {
		public string login, password;

		public Account(string login, string password) {
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
		public Flight availableFlight;
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
		public string pnr;
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

		public LoginError LoginError { get { Common.Debug2.AssertPersistent(error == 0); return loginError; } set { error = 0; loginError = value; } }
		public InputError InputError { get { Common.Debug2.AssertPersistent(error == 1); return inputError; } set { error = 1; inputError = value; } }

		public bool isLoginError { get { return error == 0; } }
		public bool isInputError { get { return error == 1; } }
	}

	[ServiceContract]
	public interface ClientService {
		[OperationContract] Parameters parameters();


		[OperationContract] Either<Success, LoginError> registerAccount(Account account);

		[OperationContract] Either<Success, LoginError> logInAccount(Account account);


		[OperationContract] Either<List<Flight>, InputError> findMatchingFlights(MatchingFlightsParams p);

		[OperationContract] Either<FlightsSeats.Seats, InputError> getSeatsForFlight(int flightId);


		[OperationContract] Either<Dictionary<int, Passanger>, LoginError> getPassangers(Account account);

		[OperationContract] Either<int, LoginOrInputError> addPassanger(Account account, Passanger passanger);

		[OperationContract] Either<int, LoginOrInputError> replacePassanger(Account account, int id, Passanger passanger);

		[OperationContract] Either<Success, LoginOrInputError> removePassanger(Account account, int id);


		[OperationContract] Either<SeatCost[], InputError> calculateSeatsCost(int flightId, SeatAndOptions[] seats);

		[OperationContract] Either<BookingFlightResult, LoginOrInputError> bookFlight(Account? customer, SelectedSeat[] selectedSeats, Dictionary<int, Passanger> tempPassangers, int flightId);

		[OperationContract] Either<BookedFlight[], LoginError> getBookedFlights(Account customer);

		[OperationContract] Either<BookedFlightDetails, LoginOrInputError> getBookedFlightDetails(Account customer, int bookedFlightId);

		[OperationContract] Either<BookedFlightPassanger, InputError> getBookedFlightFromSurnameAndPNR(string surname, string pnr);

		[OperationContract] Either<Success, LoginOrInputError> deleteBookedSeat(string surname, string pnr);
	}
}