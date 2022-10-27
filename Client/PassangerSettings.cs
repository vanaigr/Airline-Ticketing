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
		private BookingStatus status;

		private Dictionary<int, string> classesNames;
		
		private FlightsSeats.Seats seats;
		private SeatHandling seatHandling;

		private BookingPassanger passanger;
		private int bookingPassangerIndex;

		public bool useSeatIndex{ get{ return seatSelect.Checked; } }

		private int seatClassId {
			get {
				return passanger.ClassId(seats);
			}
		}

		public interface SeatHandling {
			bool canPlaceAt(int index);
		}

		bool ignore__ = false;

		public PassangerSettings(
			Communication.MessageService service, CustomerData customer, BookingStatus status,
			int flightId, FlightsSeats.Seats seats, SeatHandling seatHandling,
			BookingPassanger passanger, int bookingPassangerIndex,
			Dictionary<int , FlightsOptions.Options> optionsForClasses, 
			Dictionary<int, string> allClassesNames
		) {
			this.ignore__ = true;

			this.service = service;
			this.customer = customer;
			this.status = status;

			this.seats = seats;
			this.seatHandling = seatHandling;
			this.passanger = passanger;
			this.classesNames = new Dictionary<int, string>();
			this.bookingPassangerIndex = bookingPassangerIndex;

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

			this.passangerUpdate.init(service, customer, status, passanger);
			this.passangerOptions.init(
				service, customer, status, flightId, seats, 
				optionsForClasses, passanger, bookingPassangerIndex
			);
			
			this.seatPositionTextbox.Text = seats.Scheme.ToName(passanger.seatIndex);
			
			seatSelect.Checked = passanger.manualSeatSelected;

			this.seatClassCombobox.DataSource = new BindingSource{ DataSource = classesNames };
			seatClassCombobox.DisplayMember = "Value";
			seatClassCombobox.SelectedItem = new KeyValuePair<int, string>(
				passanger.seatClassId, classesNames[passanger.seatClassId]
			);

			updateClass();
			updateSeatSelectDisplay();

			this.ignore__ = false;

			if(status.booked) updateStatusBooked();
		}

		private void updateStatusBooked() {
			deleteButton.Text = "Удалить бронь";
			if(status.BookedFlight(customer).bookedFlightId == null) deleteButton.Enabled = false;
			else deleteButton.Enabled = true;

			cancelButton.Visible = false;
			cancelButton.Enabled = false;

			seatSelect.Enabled = false;
			seatPositionTextbox.Enabled = false;
			seatClassCombobox.Enabled = false;

			applyButton.Text = "Выйти";
			updateAutoseatClass();
		}

		private void seatSelect_CheckedChanged(object sender, EventArgs e) {
			if(ignore__) return;
			passanger.manualSeatSelected = seatSelect.Checked;
			updateClass();
			updateSeatSelectDisplay();
		}

		private void updateSeatSelectDisplay() {
			if(passanger.manualSeatSelected) {
				seatPositionTextbox.Visible = true;
				seatClassLabel.Visible = true;
				seatClassCombobox.Visible = false;
			}
			else {
				seatPositionTextbox.Visible = false;
				seatClassLabel.Visible = false;
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
			if(status.booked) {
				var bookedFlight = status.BookedFlight(customer);
				var bookedFlightDetails = status.BookedFlightDetails(customer);
				if(bookedFlight.bookedFlightId == null) throw new InvalidOperationException();

				var dResult = MessageBox.Show(
					"Вы точно хотите отменить бронь данного места?", "",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
				);

				if(dResult == DialogResult.Yes) try {
					var result = service.deleteBookedFlightSeat(
						customer.customer.Value, (int) bookedFlight.bookedFlightId, 
						bookedFlightDetails.bookedSeats[bookingPassangerIndex].selectedSeat
					);

					if(result) {
						Debug.Assert(result.s == bookedFlightDetails.bookedSeats.Length-1);

						DialogResult = DialogResult.Abort;
					}
					else {
						var msg = result.f.isInputError ? result.f.InputError.message : result.f.LoginError.message;
						statusLabel.Text = msg;
						statusToolitp.SetToolTip(statusLabel, msg);
					}
				}
				catch(Exception ex) {
					statusLabel.Text = "Неизвестная ошибка";
					statusToolitp.SetToolTip(statusLabel, ex.ToString());
				}
			}
			else { 
				var dResult = MessageBox.Show(
					"Вы точно хотите отменить выбор данного места?", "",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
				);

				if(dResult == DialogResult.Yes) DialogResult = DialogResult.Abort;
			}
		}

		private void messageSeatError() {
			MessageBox.Show("Заданное место занято или не существует.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void seatPositionTextbox_Leave(object sender, EventArgs e) {
			if(status.booked) return;
			var newIndex = seats.Scheme.FromName(seatPositionTextbox.Text);
			if(newIndex != null && seatHandling.canPlaceAt((int) newIndex)) {
				passanger.seatIndex = (int) newIndex;
				updateClass();
			}
			else messageSeatError();
		}

		private void PassangerSettings_FormClosing(object sender, FormClosingEventArgs e) {
			if(status.booked) return;
			if(useSeatIndex) {
				var newIndex = seats.Scheme.FromName(seatPositionTextbox.Text);
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
			if(status.booked) return;
			if(e.KeyChar == (char) Keys.Return) {
				var newIndex = seats.Scheme.FromName(seatPositionTextbox.Text);
				if(newIndex != null && seatHandling.canPlaceAt((int)newIndex)) {
					passanger.seatIndex = (int)newIndex;
					updateClass();
				}
				else messageSeatError();
			}
		}

		private void updateClass() {
			seatClassLabel.Text = "(" + classesNames[seatClassId] + ")";

			if(!status.booked) passangerOptions.updateForClassAndSeat();
		}

		private void updateAutoseatClass() {
			if(!status.booked) throw new InvalidOperationException();

			var si = status.BookedFlightDetails(customer).bookedSeats[bookingPassangerIndex];
			seatClassLabel.Text = "[" + seats.Scheme.ToName(si.selectedSeat) + "]";

			if(!status.booked) passangerOptions.updateForClassAndSeat();
		}

		private void seatClassCombobox_SelectedIndexChanged(object sender, EventArgs e) {
			if(ignore__) return;
			passanger.seatClassId = ((KeyValuePair<int, string>) seatClassCombobox.SelectedItem).Key;
			updateClass();
		}
	}
}
