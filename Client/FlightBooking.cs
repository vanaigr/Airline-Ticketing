using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightBooking : Form {
		public Communication.MessageService service;
		public int flightId;

		public int flightInfo;

		public FlightBooking(Communication.MessageService service, int flightId) {
			InitializeComponent();
		}
	}
}
