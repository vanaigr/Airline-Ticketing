using Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Communication {
    [Serializable] public struct Parameters {
        public string[] flightClasses;
        public List<City> cities;
    }

    [Serializable] public sealed class MatchingFlightsParams {
        public string fromCode;
        public string toCode;
        public DateTime when;
    }

    [Serializable] public sealed class Flight {
        public int id;
        public DateTime departureTime;
        public int arrivalOffsetMinutes;

        public string fromCode;
        public string toCode;

        public string flightName;
        public string airplaneName;

        public Dictionary<int, FlightsOptions.Options> optionsForClasses;
        public int[] seatCountForClasses;
        public int[] availableSeatsForClasses;
    }

    [Serializable] public struct City {
        public string country, code;
        public int timeOffsetMinutes;
        public string name { get; set; } //display in combobox requires property
    }


    [Serializable] public struct SelectedSeat {
        public SeatAndOptions seatAndOptions;
        public bool fromTempPassangers;
        public int passangerId;
    }

    [Serializable] public struct SeatAndOptions {
        public FlightsOptions.SelectedOptions selectedOptions;
        public int? seatIndex;
        public int selectedSeatClass;
    }

    [Serializable] public struct SeatCost {
        public int basePrice;
        public int seatCost;
        public int baggageCost;
        public int totalCost;
    }

    [Serializable] public sealed class BookedFlight {
        public int? bookedFlightId;
        public Flight availableFlight;
        public int bookedPassangerCount;
        public DateTime bookingFinishedTime;
    }

    [Serializable] public struct BookedSeatInfo {
        public int passangerId;
        public string pnr;
        public int selectedSeat;
        public SeatCost cost;
    }


    [Serializable] public sealed class BookedFlightDetails {
        public BookedSeatInfo[] bookedSeats;
        public SeatAndOptions[] seatsAndOptions;
        public FlightsSeats.Seats seats;
    }

    [Serializable] public sealed class PassangerBookedFlight {
        public bool cancelled;
        public int passangerId;
        public Passanger passanger;
        public BookedFlight flight;

        public BookedSeatInfo bookedSeat;
        public SeatAndOptions seatAndOptions;
    }


    [Serializable]
    public sealed class Passanger {
        public bool archived;
        public string name;
        public string surname;
        public string middleName;
        public DateTime birthday;
        public Documents.Document document;

        public override bool Equals(object o) {
            if(o == null || !(o is Passanger)) return false;
            var s = (Passanger)o;
            return Equals(name, s.name) && Equals(surname, s.surname) && Equals(middleName, s.middleName)
                    && Equals(birthday, s.birthday) && Equals(document, s.document);
        }

        public override int GetHashCode() {
            int hashCode = -644653966;
            hashCode=hashCode*-1521134295+archived.GetHashCode();
            hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(name);
            hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(surname);
            hashCode=hashCode*-1521134295+EqualityComparer<string>.Default.GetHashCode(middleName);
            hashCode=hashCode*-1521134295+birthday.GetHashCode();
            hashCode=hashCode*-1521134295+EqualityComparer<Document>.Default.GetHashCode(document);
            return hashCode;
        }
    }


    [Serializable] public struct Success { };

    [Serializable] public struct InputError {
        public string message;

        public InputError(string message) { this.message = message; }
    }
    [Serializable] public sealed class Either<TS, TF> {
        private bool isFirst;
        private TS first;
        private TF second;

        public static Either<TS, TF> Success(TS value) {
            return new Either<TS, TF> { isFirst = true, first = value };
        }

        public static Either<TS, TF> Failure(TF value) {
            return new Either<TS, TF> { isFirst = false, second = value };
        }

        public bool IsSuccess { get { return isFirst; } }

        public TS s { get { return Success(); } }
        public TF f { get { return Failure(); } }

        public TS Success() {
            Common.Debug2.AssertPersistent(isFirst);
            return first;
        }

        public TF Failure() {
            Common.Debug2.AssertPersistent(!isFirst);
            return second;
        }

        public static bool operator true(Either<TS, TF> it) { return it.IsSuccess; }
        public static bool operator false(Either<TS, TF> it) { return !it.IsSuccess; }
    }
}
