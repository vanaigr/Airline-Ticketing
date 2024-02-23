using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operator {
    public partial class FlightDisplay : UserControl {
        Context context;
        Flight flight;

        public Flight CurrentFlight{ get{ return flight; } }

        public FlightDisplay() {
            InitializeComponent();
        }

        public void updateFromFlight(
            Context context,
            Flight flight
        ) {
            this.context = context;
            this.flight = flight;

            var depart = flight.departureTime.AddMinutes(context.cities[flight.fromCode].timeOffsetMinutes);
            var arrive = flight.departureTime.AddMinutes(flight.arrivalOffsetMinutes)
                .AddMinutes(context.cities[flight.toCode].timeOffsetMinutes);

            fromCityCode.Text = flight.fromCode;
            toCityCode.Text = flight.toCode;

            fromDate.Text = depart.Date.ToString("d MMM, ddd");
            toDate.Text = arrive.Date.ToString("d MMM, ddd");

            fromTime.Text = depart.TimeOfDay.ToString(@"h\:mm");
            toTime.Text = arrive.TimeOfDay.ToString(@"h\:mm");

            var span = new TimeSpan(0, flight.arrivalOffsetMinutes, 0);

            var spanText = new StringBuilder();
            spanText.Append("в пути ");
            if(span.Days != 0) spanText.Append(span.Days).Append("д ");
            if(span.Hours != 0) spanText.Append(span.Hours).Append("ч ");
            if(span.Minutes != 0) spanText.Append(span.Minutes).Append("м ");

            flightNameLabel.Text = flight.flightName;
            airplaneNameLabel.Text = flight.airplaneName;

            var availableClassesNames = new Dictionary<int, string>();
            foreach(var key in flight.optionsForClasses.Keys) {
                availableClassesNames.Add(key, context.classesNames[key]);
            }

            economyClassSeatsLabel.Text = "Заняо в эконом классе: "  + (flight.seatCountForClasses[0] - flight.availableSeatsForClasses[0]) + "/" + flight.seatCountForClasses[0];
            comfortClassSeatsLabel.Text = "Занято в комфорт классе: " + (flight.seatCountForClasses[1] - flight.availableSeatsForClasses[1]) + "/" + flight.seatCountForClasses[1];
            businessClassSeatsLabel.Text = "Занято в бизнес классе: " + (flight.seatCountForClasses[2] - flight.availableSeatsForClasses[2]) + "/" + flight.seatCountForClasses[2];
            firstClassSeatsLabel.Text = "Занято в первом классе: "  + (flight.seatCountForClasses[3] - flight.availableSeatsForClasses[3]) + "/" + flight.seatCountForClasses[3];

            economyClassSeatsLabel.ForeColor  = flight.seatCountForClasses[0] == 0 ? SystemColors.ControlDarkDark : SystemColors.ControlText;
            comfortClassSeatsLabel.ForeColor  = flight.seatCountForClasses[1] == 0 ? SystemColors.ControlDarkDark : SystemColors.ControlText;
            businessClassSeatsLabel.ForeColor = flight.seatCountForClasses[2] == 0 ? SystemColors.ControlDarkDark : SystemColors.ControlText;
            firstClassSeatsLabel.ForeColor  = flight.seatCountForClasses[3] == 0 ? SystemColors.ControlDarkDark : SystemColors.ControlText;
        }

        private void FlightDisplay_MouseHover(object sender, EventArgs e) {
            this.BackColor = Color.FromArgb(255, 240, 240, 240);
        }

        private void FlightDisplay_MouseLeave(object sender, EventArgs e) {
            this.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e) {
            this.OnClick(e);
        }
    }
}
