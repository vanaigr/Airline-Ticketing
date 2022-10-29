using ClientCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientCommunication {
	public class BookingPassanger {
		public int? passangerIndex;
		public bool manualSeatSelected;
		public int seatIndex;
		public int seatClassId;
		public Dictionary<int, int> baggageOptionIndexForClass;
		public Dictionary<int, int> handLuggageOptionIndexForClass;

		private BookingPassanger() { }

		public BookingPassanger(
			int? passangerIndex,
			bool manualSeatSelected,
			int seatIndex,
			int seatClassId,
			Dictionary<int, int> baggageOptionIndexForClass,
			Dictionary<int, int> handLuggageOptionIndexForClass
		) {
			this.passangerIndex = passangerIndex;
			this.manualSeatSelected = manualSeatSelected;
			this.seatIndex = seatIndex;
			this.seatClassId = seatClassId;
			this.baggageOptionIndexForClass = baggageOptionIndexForClass;
			this.handLuggageOptionIndexForClass = handLuggageOptionIndexForClass;
		}

		public BookingPassanger(int defaultClass, int defaultDocumentId, Documents.Document defaultDocument) {
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
			handLuggageOptionIndexForClass = new Dictionary<int, int>(handLuggageOptionIndexForClass),
		}; }

		public int ClassId(FlightsSeats.Seats seats) {
			if(manualSeatSelected) return seats.Class(seatIndex);
			else return seatClassId;
		}
	}
}
