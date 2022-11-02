using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientCommunication {
	public partial class BookedFlightInfoControl : UserControl {
		private ClientCommunication.MessageService service;

		private int bookedFlightIndex;
		private ClientCommunication.BookedFlight bookedFlight;
		private CustomerData customer;
		private Context context;

		private SetStatus setStatus;

		public event EventHandler OnDelete;

		public BookedFlightInfoControl(
			MessageService service, CustomerData customer, 
			Context context,
			int bookedFlightIndex,
			SetStatus setStatus
		) {
			this.service = service;
			this.customer = customer;
			this.context = context;
			this.bookedFlightIndex = bookedFlightIndex;
			this.setStatus = setStatus;

			InitializeComponent();

			this.bookedFlight = customer.flightsBooked[bookedFlightIndex];
			var af = bookedFlight.availableFlight;

			flightNameLabel.Text = af.flightName;
			airplaneNameLabel.Text = af.airplaneName;
			departureLocationLabel.Text = bookedFlight.availableFlight.fromCode;
			departireDatetimeLabel.Text = af.departureTime.AddMinutes(context.cities[bookedFlight.availableFlight.fromCode].timeOffsetMinutes)
				.ToString("d MMMM, ddd, HH:mm");
			arrivalLocationLabel.Text = bookedFlight.availableFlight.toCode;
			arrivalDatetimeLabel.Text = af.departureTime.AddMinutes(af.arrivalOffsteMinutes)
				.AddMinutes(context.cities[bookedFlight.availableFlight.toCode].timeOffsetMinutes).ToString("d MMMM, ddd, HH:mm");
			bookingFinishedTimeLabel.Text = "Дата бронирования (по локальному времени): " + bookedFlight.bookingFinishedTime.ToString("d MMMM, ddd, HH:mm");
			updateBookedSeatsCount();
		}

		private Form openedFlightDetails = null;

		private void proceedButton_Click(object sender, EventArgs e) {
			if(openedFlightDetails != null) {
				openedFlightDetails.BringToFront();
				return;
			}

			try {
				if(!customer.bookedFlightsDetails.ContainsKey(bookedFlightIndex)) {
					if(bookedFlight.bookedFlightId == null) {
						setStatus("Невозможно получить информацию о данном рейсе так как его идентификатор неизвестен", null);
						return;
					}
					else if(customer.LoggedIn) {
						var result = service.getBookedFlightDetails(customer.customer.Value, (int) bookedFlight.bookedFlightId);

						if(result) customer.bookedFlightsDetails.Add(bookedFlightIndex, result.s);
						else {
							if(result.f.isLoginError) setStatus(result.f.LoginError.message, null);
							else setStatus(result.f.InputError.message, null);

							return;
						}
					}
					else {
						setStatus("Невозможно получить информацию о данном рейсе для неавторизованного пользователя", null);
						return;
					}
				}

				var status = new BookingStatus{ 
					booked = true, 
					bookedFlightIndex = bookedFlightIndex,
				};
				
				var form = new FlightDetailsFill(
					service, customer, context, 
					status, null, null
				);

				form.FormClosed += (a, b) => {
					openedFlightDetails = null;
				};

				form.OnBookedPassangersChanged += (a, b) => {
					if(bookedFlight.bookedPassangerCount == 0) OnDelete?.Invoke(this, new EventArgs());
					else updateBookedSeatsCount();
				};

				openedFlightDetails = form;
				form.Show();
			}
			catch(Exception ex) {
				setStatus(null, ex);
			}
		}

		private void updateBookedSeatsCount() {
			bookedSeatsCountLabel.Text = "Забронированно мест: " + bookedFlight.bookedPassangerCount;
		}
	}

	public delegate void SetStatus(string msg, Exception details);
}
