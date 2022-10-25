using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class BookedFlightInfoControl : UserControl {
		private Communication.MessageService service;

		private Communication.BookedFlight bookedFlight;
		private Communication.BookedFlightDetails details;
		private CustomerData customer;
		private string[] classesNames;
		
		private FlightAndCities fis;

		private SetStatus setStatus;

		public BookedFlightInfoControl(
			Communication.MessageService service,
			SetStatus setStatus,
			CustomerData customer,
			string[] classesNames,
			Communication.BookedFlight bookedFlight,
			Communication.BookedFlightDetails details
		) {
			this.service = service;
			this.setStatus = setStatus;
			this.customer = customer;
			this.classesNames = classesNames;
			this.bookedFlight = bookedFlight;
			this.details = details;
			
			fis = new FlightAndCities();
			fis.flight = bookedFlight.availableFlight;
			fis.fromCityCode = bookedFlight.fromCode;
			fis.toCityCode = bookedFlight.toCode;

			InitializeComponent();

			var af = bookedFlight.availableFlight;

			flightNameLabel.Text = af.flightName;
			airplaneNameLabel.Text = af.airplaneName;
			departureLocationLabel.Text = bookedFlight.fromCode;
			departireDatetimeLabel.Text = af.departureTime.ToString("d MMMM, ddd, HH:mm");
			arrivalLocationLabel.Text = bookedFlight.toCode;
			arrivalDatetimeLabel.Text = af.departureTime.AddMinutes(af.arrivalOffsteMinutes).ToString("d MMMM, ddd, HH:mm");
			bookedSeatsCountLabel.Text = "Забронированно мест: " + bookedFlight.bookedPassangerCount;
			bookingFinishedTimeLabel.Text = "Дата бронирования: " + bookedFlight.bookingFinishedTime.ToString("d MMMM, ddd, HH:mm");
		}

		private Form openedFlightDetails = null;

		private void proceedButton_Click(object sender, EventArgs e) {
			if(openedFlightDetails != null) {
				openedFlightDetails.BringToFront();
				return;
			}

			try {
				Communication.BookedFlightDetails bfDetails;

				if(details != null) bfDetails = details;
				else {
					if(customer.LoggedIn) {
						var result = service.getBookedFlightDetails(customer.customer.Value, bookedFlight.bookedFlightId);

						if(result) bfDetails = result.s;
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
					seatsInfo = new Communication.BookedSeatInfo[
						bfDetails.bookedSeats.Length
					] 
				};
				List<BookingPassanger> bookingPassangers;

				bookingPassangers = new List<BookingPassanger>(bfDetails.bookedSeats.Length);

				for(int i = 0; i <bfDetails.bookedSeats.Length; i++) {
					var bs = bfDetails.bookedSeats[i];
					var so = bfDetails.seatsAndOptions[i];

					var baggageOption = new Dictionary<int, int>(1);
					baggageOption.Add(so.selectedSeatClass, so.selectedOptions.baggageOptions.baggageIndex);
					var handLuggageOption = new Dictionary<int, int>(1);
					handLuggageOption.Add(so.selectedSeatClass, so.selectedOptions.baggageOptions.handLuggageIndex);

					var index = findPasangerIndexByDatabaseId(customer, bs.passangerId);

					var it = new BookingPassanger(
						(int) index, so.selectedOptions.servicesOptions.seatSelected,
						bs.selectedSeat, so.selectedSeatClass, baggageOption, handLuggageOption
					);
					
					bookingPassangers.Add(it);
					status.seatsInfo[i] = bs;
				}

				var form = new FlightDetailsFill(
					null, customer, status,
					bookingPassangers, classesNames,
					fis, bfDetails.seats
				);

				form.FormClosed += (a, b) => openedFlightDetails = null;

				openedFlightDetails = form;
				form.Show();
			}
			catch(Exception ex) {
				setStatus(null, ex);
			}
		}

		private static int? findPasangerIndexByDatabaseId(CustomerData customer, int databaseId) {
			foreach(var pair in customer.passangerIds) {
				if(!pair.Value.IsLocal && pair.Value.DatabaseId == databaseId) return pair.Key;
			}
			return null;
		}
	}

	public delegate void SetStatus(string msg, Exception details);
}
