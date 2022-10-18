using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerSettings : Form {
		private Communication.MessageService service;
		private CustomerData customer;
		
		private SeatString seatString;

		public PassangerSettings(
			Communication.MessageService service, CustomerData customer, 
			int? defaultPassangerId, 
			SeatString seatString,
			Dictionary<int , FlightsOptions.Options> optionsForClasses, Dictionary<int, string> classesNames
		) {
			this.service = service;
			this.customer = customer;
			this.seatString = seatString;

			Misc.unfocusOnEscape(this, (a, b) => {
				if(b.KeyCode == Keys.Escape && this.ActiveControl == null && mainTabs.SelectedIndex == 0) {
					this.passangerUpdate.selectNone();
				}
			});

			InitializeComponent();

			Misc.addDummyButton(this);
			Misc.addTopDivider(tableLayoutPanel2);

			this.passangerUpdate.init(service, customer, defaultPassangerId);
			this.passangerOptions.init(optionsForClasses, classesNames);

			this.seatPositionTextbox.Text = seatString.get();
		}

		public int? PassangerIndex {
			get{ return this.passangerUpdate.SelectedPassangerIndex; }
		}

		private void applyButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Abort;
		}

		private void messageSeatError() {
			MessageBox.Show("Заданное место занято или не существует. Вы можете оставить строку места " +
			"пустой для автоматического определения места для пассажира или удалить пассажира", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void seatPositionTextbox_Leave(object sender, EventArgs e) {
			if(!seatString.set(seatPositionTextbox.Text)) messageSeatError();
		}

		private void PassangerSettings_FormClosing(object sender, FormClosingEventArgs e) {
			if(!seatString.set(seatPositionTextbox.Text)) {
				messageSeatError();
				e.Cancel = true;
			}

			if(!passangerUpdate.onClose()) {
				e.Cancel = true;
			}
		}

		public interface SeatString {
			string get();

			bool set(string it);
		}
	}
}
