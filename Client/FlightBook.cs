using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightBook : Form {
		private Communication.MessageService service;
		private Dictionary<int, string> classesNames;

		private CustomerData customer;
		private List<BookingPassanger> bookingPassangers;

		private Dictionary<int, Communication.Passanger> localPassangers;
		private Communication.SeatAndOptions[] seatsAndOptions;
		private Communication.SelectedSeat[] selectedSeats;

		private FlightAndCities flightAndCities;
		private FlightsSeats.Seats seats;

		private BookingStatus status;
		
		public FlightBook(
			Communication.MessageService service,
			CustomerData customer, List<BookingPassanger> bookingPassangers,
			FlightAndCities flightAndCities, FlightsSeats.Seats seats,
			Dictionary<int, string> classesNames,
			BookingStatus status
		) {
			this.service = service;
			this.classesNames = classesNames;

			this.customer = customer;
			this.bookingPassangers = bookingPassangers;

			this.flightAndCities = flightAndCities;
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
				seatsAndOptions = new Communication.SeatAndOptions[bookingPassangers.Count];
				for(int i = 0; i < seatsAndOptions.Length; i++) {
					var p = bookingPassangers[i];
					var seatClassId = p.ClassId(seats);

					seatsAndOptions[i] = new Communication.SeatAndOptions{
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

				localPassangers = new Dictionary<int, Communication.Passanger>();
				selectedSeats = new Communication.SelectedSeat[seatsAndOptions.Length];

				for(int i = 0; i < bookingPassangers.Count; i++) {
					var index = (int) bookingPassangers[i].passangerIndex;
					var idInfo = customer.passangerIds[index];

					selectedSeats[i] = new Communication.SelectedSeat{
						fromTempPassangers = idInfo.IsLocal,
						passangerId = idInfo.IsLocal ? index : idInfo.DatabaseId,
						seatAndOptions = seatsAndOptions[i]
					};

					if(idInfo.IsLocal) {
						localPassangers[index] = customer.passangers[index];
					}
				}
				
				try {
					Communication.SeatCost[] seatsCost;

					if(!status.booked) {
						var result = service.seatsData(flightAndCities.flight.id, seatsAndOptions);

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

		private void updateSum(Communication.SeatCost[] seatsCost) {
			var totalSum = 0;

			for(int i = 0; i < bookingPassangers.Count; i++) {
				var passanger = bookingPassangers[i];
				var it = new BookingPassangerSummaryControl();

				Communication.BookedSeatInfo? bookedSeatInfo;;
				Communication.SeatCost seatCost;

				if(status.booked) {
					bookedSeatInfo = status.seatsInfo[i];
					seatCost = bookedSeatInfo.Value.cost;
				}
				else {
					bookedSeatInfo = null;
					seatCost = seatsCost[i];
				}

				it.set(
					customer, passanger,
					seats, flightAndCities.flight.optionsForClasses, 
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
				var result = service.bookFlight(customer.customer, selectedSeats, localPassangers, flightAndCities.flight.id);

				if(result) {
					booked = true;
					var booking = result.s;

					status.bookedFlightId = booking.customerBookedFlightId;
					status.seatsInfo = booking.seatsInfo;
					
					for(int i = 0; i < bookingPassangers.Count; i++) {
						var ss = status.seatsInfo[i];
						var bp = bookingPassangers[i];

						customer.passangerIds[(int) bp.passangerIndex] = new PassangerIdData(ss.passangerId);
						customer.passangers[(int) bp.passangerIndex].archived = true;
					}

					customer.localBookedFlights.Add(new Communication.BookedFlight{
						availableFlight = flightAndCities.flight, bookedPassangerCount = bookingPassangers.Count,
						fromCode = flightAndCities.fromCityCode, toCode = flightAndCities.toCityCode
					});
					customer.localBookedFlightsDetails.Add(new Communication.BookedFlightDetails{
						bookedSeats = status.seatsInfo, seats = seats, seatsAndOptions = seatsAndOptions
					});

					statusOk("Бронирование выполено успешно");
					bookFlightButton.Enabled = false;
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
