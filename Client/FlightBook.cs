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
		private CustomerData customer;

		private Dictionary<int, Communication.Passanger> localPassangers;
		private Communication.SelectedSeat[] selectedSeats;

		private Communication.AvailableFlight flight;
		private FlightsSeats.Seats seats;
		
		public FlightBook(
			Communication.MessageService service,
			CustomerData customer, List<BookingPassanger> passangers,
			Communication.AvailableFlight flight, FlightsSeats.Seats seats,
			Dictionary<int, string> classesNames
		) {
			this.service = service;
			this.customer = customer;
			this.flight = flight;
			this.seats = seats;

			InitializeComponent();

			Misc.fixFlowLayoutPanelHeight(passangersSummaryPanel);
			Misc.unfocusOnEscape(this);

			this.passangersSummaryPanel.SuspendLayout();

			var seatsAndOptions = new Communication.SeatAndOptions[passangers.Count];
			for(int i = 0; i < seatsAndOptions.Length; i++) {
				var p = passangers[i];
				var seatClassId = p.ClassId(seats);

				seatsAndOptions[i] = new Communication.SeatAndOptions{
					selectedSeatClass = seatClassId,
					seatIndex = p.manualSeatSelected ? p.seatIndex : (int?) null,
					selectedOptions = new FlightsOptions.SelectedOptions(
						new FlightsOptions.SelectedBaggageOptions(
							p.baggageOptionIndexForClass[seatClassId],
							p.handLuggageOptionIndexForClass[seatClassId]
						)
					)
				};
			}

			localPassangers = new Dictionary<int, Communication.Passanger>();

			selectedSeats = new Communication.SelectedSeat[seatsAndOptions.Length];
			for(int i = 0; i < selectedSeats.Length; i++) {
				var passanger = passangers[i];
				var index = (int) passangers[i].passangerIndex;
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
				var result = service.seatsData(flight.id, seatsAndOptions);

				if(result) {
					var list = result.s;

					var totalSum = 0;

					for(int i = 0; i < passangers.Count; i++) {
						var passanger = passangers[i];
						var it = new BookingPassangerSummaryControl();
						it.set(
							customer, passanger,
							seats, flight.optionsForClasses, 
							list[i], classesNames
						);
						passangersSummaryPanel.Controls.Add(it);
						totalSum += list[i].totalCost;
					}

					
					totalPriceLabel.Text = "Итого: " + totalSum + " руб.";
					statusOk("");
					bookFlight.Enabled = true;
				}
				else {
					var e = result.f.message;
					totalPriceLabel.Text = "";
					statusError(e);
					bookFlight.Enabled = false;
				}
			}
			catch(Exception e) {
				totalPriceLabel.Text = "";
				statusError("Неизвестная ошибка", e.ToString());
				bookFlight.Enabled = false;
			}

			this.passangersSummaryPanel.ResumeLayout(false);
			this.passangersSummaryPanel.PerformLayout();
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

		private void bookFlight_Click(object sender, EventArgs e) {
			try {
				var result = service.bookFlight(customer.customer, localPassangers, selectedSeats, flight.id);

				if(result) {
					statusOk("Бронирование выполенно успешно");
					//TODO: fix passangers
					bookFlight.Enabled = false;
				}
				else {
					string msg;
					if(result.f.isInputError) msg = result.f.InputError.message;
					else msg = result.f.LoginError.message;

					statusError(msg);
				}
			}
			catch(Exception ex) {
				statusError("Неизвестная ошибка", ex.ToString());
			}
		}
	}
}
