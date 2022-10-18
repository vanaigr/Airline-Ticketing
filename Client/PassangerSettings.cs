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
		private Dictionary<int, string> classesNames;
		
		private SeatHandling seatHandling;

		private BookingPassanger passanger;

		public bool useSeatIndex{ get{ return seatSelect.Checked; } }

		private int seatClassId {
			get {
				if(seatSelect.Checked) return seatHandling.classAt((int)passanger.seatIndex);
				else return passanger.seatClassId;
			}
		}

		public interface SeatHandling {
			string getSeatString(int seatIndex);
			int? indexFromSeatString(string seatString);
			int classAt(int seatIndex);
			bool canPlaceAt(int index);
		}

		bool ignore__ = false;

		public PassangerSettings(
			Communication.MessageService service, CustomerData customer,
			SeatHandling seatHandling,
			BookingPassanger passanger,
			Dictionary<int , FlightsOptions.Options> optionsForClasses, 
			Dictionary<int, string> allClassesNames
		) {
			this.ignore__ = true;

			this.service = service;
			this.customer = customer;
			this.seatHandling = seatHandling;
			this.passanger = passanger;
			this.classesNames = new Dictionary<int, string>();

			foreach(var classId in optionsForClasses.Keys) {
				classesNames.Add(classId, allClassesNames[classId]);
			}

			Misc.unfocusOnEscape(this, (a, b) => {
				if(b.KeyCode == Keys.Escape && this.ActiveControl == null && mainTabs.SelectedIndex == 0) {
					this.passangerUpdate.selectNone();
				}
			});

			InitializeComponent();

			Misc.addDummyButton(this);
			Misc.addTopDivider(tableLayoutPanel2);

			this.passangerUpdate.init(service, customer, passanger);
			this.passangerOptions.init(optionsForClasses, passanger);
			
			this.seatPositionTextbox.Text = seatHandling.getSeatString(passanger.seatIndex);
			
			seatSelect.Checked = passanger.useIndex;

			this.seatClassCombobox.DataSource = new BindingSource{ DataSource = classesNames };
			seatClassCombobox.DisplayMember = "Value";
			seatClassCombobox.SelectedItem = new KeyValuePair<int, string>(
				passanger.seatClassId, classesNames[passanger.seatClassId]
			);

			updateClass();
			updateSeatSelectDisplay();

			this.ignore__ = false;
		}

		private void seatSelect_CheckedChanged(object sender, EventArgs e) {
			if(ignore__) return;
			passanger.useIndex = seatSelect.Checked;
			updateClass();
			updateSeatSelectDisplay();
		}

		private void updateSeatSelectDisplay() {
			if(passanger.useIndex) {
				seatPositionTextbox.Visible = true;
				seatClassCombobox.Visible = false;
			}
			else {
				seatPositionTextbox.Visible = false;
				seatClassCombobox.Visible = true;
			}
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
			MessageBox.Show("Заданное место занято или не существует.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void seatPositionTextbox_Leave(object sender, EventArgs e) {
			var newIndex = seatHandling.indexFromSeatString(seatPositionTextbox.Text);
			if(newIndex != null && seatHandling.canPlaceAt((int) newIndex)) {
				passanger.seatIndex = (int) newIndex;
				updateClass();
			}
			else messageSeatError();
		}

		private void PassangerSettings_FormClosing(object sender, FormClosingEventArgs e) {
			if(useSeatIndex) {
				var newIndex = seatHandling.indexFromSeatString(seatPositionTextbox.Text);
				if(newIndex != null && seatHandling.canPlaceAt((int)newIndex)) {
					passanger.seatIndex = (int)newIndex;
					updateClass();
				}
				else {
					messageSeatError();
					e.Cancel = true;
				}
			}

			if(!passangerUpdate.onClose()) {
				e.Cancel = true;
			}
		}

		private void seatPositionTextbox_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == (char) Keys.Return) {
				var newIndex = seatHandling.indexFromSeatString(seatPositionTextbox.Text);
				if(newIndex != null && seatHandling.canPlaceAt((int)newIndex)) {
					passanger.seatIndex = (int)newIndex;
					updateClass();
				}
				else messageSeatError();
			}
		}

		private void updateClass() {
			passangerOptions.setForClass(seatClassId);
		}

		private void seatClassCombobox_SelectedIndexChanged(object sender, EventArgs e) {
			if(ignore__) return;
			passanger.seatClassId = ((KeyValuePair<int, string>) seatClassCombobox.SelectedItem).Key;
			updateClass();
		}
	}
}
