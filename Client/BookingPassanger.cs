using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client {
	public class BookingPassanger {
		public int? passangerIndex;
		public bool manualSeatSelected;
		public int seatIndex;
		public int seatClassId;
		public Dictionary<int, int> baggageOptionIndexForClass;
		public Dictionary<int, int> handLuggageOptionIndexForClass;

		private BookingPassanger() { }

		public BookingPassanger(int defaultClass) {
			seatClassId = defaultClass;
			baggageOptionIndexForClass = new Dictionary<int, int>();
			handLuggageOptionIndexForClass = new Dictionary<int, int>();
		}

		public BookingPassanger Copy() { return new BookingPassanger {
			passangerIndex = this.passangerIndex,
			manualSeatSelected = this.manualSeatSelected,
			seatIndex = this.seatIndex,
			seatClassId = this.seatClassId,
			baggageOptionIndexForClass = new Dictionary<int, int>(baggageOptionIndexForClass),
			handLuggageOptionIndexForClass = new Dictionary<int, int>(handLuggageOptionIndexForClass)
		}; }

		public int ClassId(FlightsSeats.Seats seats) {
			if(manualSeatSelected) return seats.Class(seatIndex);
			else return seatClassId;
		}
	}
}
