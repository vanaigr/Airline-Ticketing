using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightDisplay : UserControl {
		public FlightDisplay() {
			InitializeComponent();
		}

		public void updateFromFlight(
			Communication.AvailableFlight flight, 
			string fromCityCodeText, string toCityCodeText
		) {
			var depart = flight.departureTime;
			var arrive = flight.departureTime.AddMinutes(flight.arrivalOffsteMinutes);

			fromCityCode.Text = fromCityCodeText;
			toCityCode.Text = toCityCodeText;

			fromDate.Text = depart.Date.ToString("M MMM, ddd");
			toDate.Text = arrive.Date.ToString("M MMM, ddd");
			
			fromTime.Text = depart.TimeOfDay.ToString(@"h\:mm");
			toTime.Text = arrive.TimeOfDay.ToString(@"h\:mm");

			var span = new TimeSpan(0, flight.arrivalOffsteMinutes, 0);

			var spanText = new StringBuilder();
			spanText.Append("в пути ");
			if(span.Days != 0) spanText.Append(span.Days).Append("д ");
			if(span.Hours != 0) spanText.Append(span.Hours).Append("ч ");
			if(span.Minutes != 0) spanText.Append(span.Minutes).Append("м ");

			flightTime.Text = spanText.ToString();
		}
	}
}
