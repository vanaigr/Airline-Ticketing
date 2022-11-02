using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientCommunication {
	public partial class FlightBook : Form {
		private MessageService service;
		private Dictionary<int, string> classesNames;

		private CustomerData customer;
		private List<BookingPassanger> bookingPassangers;

		private Dictionary<int, Passanger> localPassangers;
		private SeatAndOptions[] seatsAndOptions;
		private SelectedSeat[] selectedSeats;

		private AvailableFlight flight;
		private FlightsSeats.Seats seats;

		private BookingStatus status;
		
		public FlightBook(
			MessageService service,
			CustomerData customer, List<BookingPassanger> bookingPassangers,
			AvailableFlight flight, FlightsSeats.Seats seats,
			Dictionary<int, string> classesNames,
			BookingStatus status
		) {
			this.service = service;
			this.classesNames = classesNames;

			this.customer = customer;
			this.bookingPassangers = bookingPassangers;

			this.flight = flight;
			this.seats = seats;

			this.status = status;

			InitializeComponent();

			Misc.fixFlowLayoutPanelHeight(passangersSummaryPanel);
			Misc.unfocusOnEscape(this);

			this.passangersSummaryPanel.SuspendLayout();

			if(status.booked) {
				updateSum(null);
				statusOk("Бронирование было выполено успешно");
				bookFlightButton.Enabled = false;
			}
			else {
				seatsAndOptions = new ClientCommunication.SeatAndOptions[bookingPassangers.Count];
				for(int i = 0; i < seatsAndOptions.Length; i++) {
					var p = bookingPassangers[i];
					var seatClassId = p.ClassId(seats);

					seatsAndOptions[i] = new ClientCommunication.SeatAndOptions{
						selectedSeatClass = seatClassId,
						seatIndex = p.manualSeatSelected ? p.seatIndex : (int?) null,
						selectedOptions = new FlightsOptions.SelectedOptions(
							new FlightsOptions.SelectedBaggageOptions(
								p.baggageOptionIndexForClass[seatClassId],
								p.handLuggageOptionIndexForClass[seatClassId]
							), 
							new FlightsOptions.SelectedServicesOptions(
								p.manualSeatSelected
							)
						)
					};
				}

				localPassangers = new Dictionary<int, Passanger>();
				selectedSeats = new ClientCommunication.SelectedSeat[seatsAndOptions.Length];

				for(int i = 0; i < bookingPassangers.Count; i++) {
					var index = (int) bookingPassangers[i].passangerIndex;
					var idInfo = customer.passangerIds[index];

					selectedSeats[i] = new ClientCommunication.SelectedSeat{
						fromTempPassangers = idInfo.IsLocal,
						passangerId = idInfo.IsLocal ? index : idInfo.DatabaseId,
						seatAndOptions = seatsAndOptions[i]
					};

					if(idInfo.IsLocal) {
						localPassangers[index] = customer.passangers[index];
					}
				}
				
				try {
					ClientCommunication.SeatCost[] seatsCost;

					if(!status.booked) {
						var result = service.seatsData(this.flight.id, seatsAndOptions);

						if(result) {
							seatsCost = result.s;
						}
						else {
							var e = result.f.message;
							totalPriceLabel.Text = "";
							statusError(e);
							bookFlightButton.Enabled = false;
							return;
						}
					}
					else seatsCost = null;
					
					updateSum(seatsCost);

					statusOk("");
					bookFlightButton.Enabled = true;
				}
				catch(Exception e) {
					totalPriceLabel.Text = "";
					statusError("Неизвестная ошибка", e.ToString());
					bookFlightButton.Enabled = false;
				}
			}

			this.passangersSummaryPanel.ResumeLayout(false);
			this.passangersSummaryPanel.PerformLayout();
		}

		private void updateSum(ClientCommunication.SeatCost[] seatsCost) {
			var totalSum = 0;

			var flightDetails = status.booked ? status.BookedFlightDetails(customer) : null;

			for(int i = 0; i < bookingPassangers.Count; i++) {
				var passanger = bookingPassangers[i];
				var it = new BookingPassangerSummaryControl();

				ClientCommunication.BookedSeatInfo? bookedSeatInfo;;
				ClientCommunication.SeatCost seatCost;

				if(status.booked) {
					bookedSeatInfo = flightDetails.bookedSeats[i];
					seatCost = bookedSeatInfo.Value.cost;
				}
				else {
					bookedSeatInfo = null;
					seatCost = seatsCost[i];
				}

				it.set(
					customer, passanger,
					seats, this.flight.optionsForClasses, 
					bookedSeatInfo, seatCost, 
					classesNames
				);
				passangersSummaryPanel.Controls.Add(it);
				totalSum += seatCost.totalCost;
			}
			
			totalPriceLabel.Text = "Итого: " + totalSum + " руб.";
		}

		private void statusOk(string msg) {
			statusLabel.ForeColor = SystemColors.ControlText;
			statusLabel.Text = msg;
			statusTooltip.SetToolTip(statusLabel, msg);
		}

		private void statusError(string msg, string msg2 = null) {
			statusLabel.ForeColor = Color.Firebrick;
			statusLabel.Text = msg;
			statusTooltip.SetToolTip(statusLabel, msg2 ?? msg);
		}

		private void bookFlightButton_Click(object sender, EventArgs e) {
			bookFlight();
		}

		private void bookFlight() {
			if(status.booked) throw new InvalidOperationException();

			var booked = false;
			try {
				var result = service.bookFlight(customer.customer, selectedSeats, localPassangers, flight.id);

				if(result) {
					booked = true;
					var booking = result.s;
					
					for(int i = 0; i < bookingPassangers.Count; i++) {
						var ss = booking.seatsInfo[i];
						var bp = bookingPassangers[i];

						customer.passangerIds[(int) bp.passangerIndex] = new PassangerIdData(ss.passangerId);
						customer.passangers[(int) bp.passangerIndex].archived = true;
					}

					var newIndex = customer.newBookedFlightIndex++;

					customer.flightsBooked.Add(newIndex, new ClientCommunication.BookedFlight{
						bookedFlightId = booking.customerBookedFlightId, bookingFinishedTime = booking.bookingFinishedTime,
						availableFlight = flight, bookedPassangerCount = bookingPassangers.Count,
					});

					customer.bookedFlightsDetails.Add(newIndex, new ClientCommunication.BookedFlightDetails{
						bookedSeats = booking.seatsInfo, seats = seats, seatsAndOptions = seatsAndOptions
					});

					statusOk("Бронирование выполено успешно");
					bookFlightButton.Enabled = false;

					status.bookedFlightIndex = newIndex;
					status.booked = true;
				}
				else {
					string msg;
					if(result.f.isInputError) msg = result.f.InputError.message;
					else msg = result.f.LoginError.message;

					statusError(msg);
				}
			}
			catch(Exception ex) {
				if(booked) statusError("Неизвестная ошибка после окончания оформления полёта", ex.ToString());
				else statusError("Неизвестная ошибка", ex.ToString());
			}
		}
	}
}
