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
		
		private SeatString seatString;

		public bool useSeatIndex{ get{ return seatSelect.Checked; } }

		public int seatClassId{ get{ 
			var index = this.seatClassCombobox.SelectedIndex;
			if(index == -1) {
				throw new Exception();
			}
			else {
				return ((KeyValuePair<int, string>) seatClassCombobox.SelectedItem).Key;
			}
		} }

		public PassangerSettings(
			Communication.MessageService service, CustomerData customer, 
			int? defaultPassangerId, int fallbackClassId,
			SeatString seatString,
			Dictionary<int , FlightsOptions.Options> optionsForClasses, Dictionary<int, string> allClassesNames
		) {
			this.service = service;
			this.customer = customer;
			this.seatString = seatString;
			classesNames = new Dictionary<int, string>();

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

			this.passangerUpdate.init(service, customer, defaultPassangerId);
			this.passangerOptions.init(optionsForClasses, allClassesNames);

			var ss = seatString.get();
			this.seatPositionTextbox.Text = ss;
			
			seatSelect.Checked = true; /*
				winforms seem to not send the cheched event when Changed 
				it set to the same one a second time
			*/
			if(ss == null) seatSelect.Checked = false;
			else seatSelect.Checked = true;

			this.seatClassCombobox.DataSource = new BindingSource{ DataSource = classesNames };
			seatClassCombobox.DisplayMember = "Value";
			seatClassCombobox.SelectedItem = new KeyValuePair<int, string>(fallbackClassId, classesNames[fallbackClassId]);
		}

		public int? PassangerIndex {
			get{ return this.passangerUpdate.SelectedPassangerIndex; }
		}

		private void seatSelect_CheckedChanged(object sender, EventArgs e) {
			if(seatSelect.Checked) {
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
			MessageBox.Show("Заданное место занято или не существует. Вы можете оставить строку места " +
			"пустой для автоматического определения места для пассажира или удалить пассажира", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void seatPositionTextbox_Leave(object sender, EventArgs e) {
			if(!seatString.set(seatPositionTextbox.Text)) messageSeatError();
		}

		private void PassangerSettings_FormClosing(object sender, FormClosingEventArgs e) {
			if(useSeatIndex && !seatString.set(seatPositionTextbox.Text)) {
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
