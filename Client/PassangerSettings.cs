using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerSettings : Form {
		private Communication.MessageService service;
		private  CustomerData customer;
		public PassangerSettings(
			Communication.MessageService service, CustomerData customer, int? defaultPassangerId, 
			Dictionary<int , FlightsOptions.Options> optionsForClasses, Dictionary<int, string> classesNames
		) {
			this.service = service;
			this.customer = customer;

			Misc.unfocusOnEscape(this, (a, b) => {
				if(b.KeyCode == Keys.Escape && this.ActiveControl == null && mainTabs.SelectedIndex == 0) {
					this.passangerUpdate.selectNone();
				}
			});
			Misc.addDummyButton(this);

			InitializeComponent();
			this.passangerUpdate.init(service, customer, defaultPassangerId);
			this.passangerOptions.init(optionsForClasses, classesNames);
		}
	}
}
