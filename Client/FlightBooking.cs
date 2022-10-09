using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Client {
	public partial class FlightBooking : Form {
		private Communication.MessageService service;

		private Dictionary<int, string> classesNames;
		private FlightAndCities flightAndCities;

		public FlightAndCities CurrentFlight{ get{ return this.flightAndCities; } }

		public FlightBooking(Communication.MessageService service) {
			InitializeComponent();
			Misc.unfocusOnEscape(this);

			this.service = service;
		}

		public void setFromFlight(
			Dictionary<int, string> classesNames,
			FlightAndCities flightAndCities,
			int selectedClassIndex
		) {
			this.classesNames = classesNames;
			this.flightAndCities = flightAndCities;

			var flight = flightAndCities.flight;

			headerContainer.SuspendLayout();
			flightNameLabel.Text = flight.flightName;
			aitrplaneNameLavel.Text = flight.airplaneName;
			departureDatetimeLabel.Text = flight.departureTime.ToString("d MMMM, dddd, h:mm");
			departureLocationLabel.Text = flightAndCities.fromCityCode;
			headerContainer.ResumeLayout(false);
			headerContainer.PerformLayout();

			//Misc.addDummyButton(classSelector.Parent);
			
			classSelector.DataSource = new BindingSource{ DataSource = classesNames };
			classSelector.DisplayMember = "Value";
			classSelector.SelectedIndex = selectedClassIndex;
			classSelector.PerformLayout();

			Misc.addDummyButton(classSelector.Parent);
		}

		private void update() {
			
		}

		private void classSelector_SelectedIndexChanged(object sender, EventArgs e) {
			update();
		}

		private void FlightBooking_Load(object sender, EventArgs e) {
			ActiveControl = Misc.addDummyButton(this);
		}
	}
}
