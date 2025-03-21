﻿using Communication;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace ClientCommunication {
    [Serializable] public struct Account {
        public string login, password;

        public Account(string login, string password) {
            this.login = login;
            this.password = password;
        }
    }

    [Serializable] public struct BookingFlightResult {
        public int? customerBookedFlightId;
        public DateTime bookingFinishedTime;
        public BookedSeatInfo[] seatsInfo;
    }

    [Serializable] public sealed class PassangerBookedFlightAndDetails {
        public bool cancelled;
        public int passangerId;
        public Passanger passanger;
        public BookedFlight flight;
        public BookedFlightDetails details;
    }

    [Serializable] public struct LoginError {
        public string message;

        public LoginError(string message) { this.message = message; }
    }

    [Serializable] public struct LoginOrInputError {
        int error;
        LoginError loginError;
        InputError inputError;

        public LoginError LoginError { get { Common.Debug2.AssertPersistent(error == 0); return loginError; } set { error = 0; loginError = value; } }
        public InputError InputError { get { Common.Debug2.AssertPersistent(error == 1); return inputError; } set { error = 1; inputError = value; } }

        public bool isLoginError { get { return error == 0; } }
        public bool isInputError { get { return error == 1; } }
    }

    [ServiceContract] public interface ClientService {
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

        [OperationContract] Either<PassangerBookedFlightAndDetails, InputError> getBookedFlightFromSurnameAndPNR(string surname, string pnr);

        [OperationContract] Either<Success, InputError> deleteBookedSeat(string surname, string pnr);
    }
}
