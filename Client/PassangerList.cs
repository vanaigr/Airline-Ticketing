using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerList : UserControl {
		private Communication.MessageService service;
		private CustomerData customer;
		private BookingStatus status;

		private Dictionary<int, PassangerDisplay> passangersDisplays;

		private BookingPassanger passanger;

		private int? currentPassangerIndex {
			get { return passanger.passangerIndex; }
			set { passanger.passangerIndex = value; }
		}

		private enum State {
			none, select, add, edit
		}

		private State curState;
		private void setStateAdd() {
			if(status.booked) throw new InvalidOperationException();

			curState = State.add;
			
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			addButton.Enabled = false;
			passangerDataTable.Enabled = true;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = null;

			clearPassangerData();

			saveButton.Visible = true;
			saveButton.Text = "Добавить";
		}
				
		private void setStateSelect(int newSelectedPassangerId) {
			curState = State.select;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			addButton.Enabled = true;
			passangerDataTable.Enabled = false;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = newSelectedPassangerId;
			passangersDisplays[newSelectedPassangerId].BackColor = Misc.selectionColor3;
			
			setDataFromPassanger(customer.passangers[newSelectedPassangerId]);

			saveButton.Visible = false;
		}

		private void setStateEdit() {
			if(status.booked) throw new InvalidOperationException();

			curState = State.edit;

			deleteButton.Enabled = true;
			editButton.Enabled = false;
			addButton.Enabled = true;
			passangerDataTable.Enabled = true;

			saveButton.Visible = true;

			var passanger = customer.passangers[(int) currentPassangerIndex];
			if(passanger.archived) saveButton.Text = "Сохранить копию";
			else saveButton.Text = "Сохранить";
		}

		public void setStateNone() {
			curState = State.none;

			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = false;

			if(currentPassangerIndex != null) passangersDisplays[(int) currentPassangerIndex].BackColor = Color.White;
			currentPassangerIndex = null;

			clearPassangerData();

			saveButton.Visible = false;
		}

		public PassangerList() {
			InitializeComponent();
		}

		public void selectNone() {
			if(!promptSaveCustomer()) return;
			setStateNone();
		}

		public void init(Communication.MessageService sq, CustomerData customer, BookingStatus status, BookingPassanger passanger) {
			this.service = sq;
			this.status = status;
			this.customer = customer;
			this.passanger = passanger;
			this.passangersDisplays = new Dictionary<int, PassangerDisplay>();
			
			setupPassangersDisplay(passanger.passangerIndex);

			if(status.booked) updateStatusBooked();
		}

		public void updateStatusBooked() {
			deleteButton.Visible = false;
			deleteButton.Enabled = false;

			editButton.Visible = false;
			editButton.Enabled = false;

			addButton.Visible = false;
			addButton.Enabled = false;

			saveButton.Enabled = false;
			saveButton.Visible = false;

			passangerMenu.Enabled = false;

			passangersDisplay.Enabled = false;
			passangerDataTable.Enabled = false;
		}

		public bool onClose() {
			return promptSaveCustomer();
		}

		private void setupPassangersDisplay(int? defaultPassangerIndex) {
			passangersDisplay.SuspendLayout();
			passangersDisplay.Controls.Clear();
			passangersDisplay.RowStyles.Clear();

			passangersDisplay.RowCount = customer.passangers.Count;

			foreach(var passanger in customer.passangers) {
				var display = addPassangerDisplay(passanger.Value, passanger.Key);
				passangersDisplays.Add(passanger.Key, display);
			}

			passangersDisplay.ResumeLayout(false);
			passangersDisplay.PerformLayout();

			if(defaultPassangerIndex != null) setStateSelect((int) defaultPassangerIndex);
			else setStateNone();
		}

		private void promptFillCustomer(string msg = null) {
			var text = "Данные о пользователе должны быть заполнены корректно";
			statusLabel.Text = text;
			statusTooltip.SetToolTip(statusLabel, text);
		
			var lrf = new LoginRegisterForm(service, customer);
		
			lrf.ShowDialog();
			if(msg != null) lrf.setError(msg);
		
			if(lrf.DialogResult == DialogResult.OK) {
				statusLabel.Text = null;
				statusTooltip.SetToolTip(statusLabel, null);
			}
		}

		//returns false if save aborted
		private bool promptSaveCustomer() {
			if(curState == State.edit) {
				var prevPassanger = customer.passangers[(int) currentPassangerIndex];
				var curPassanger = formPassangerFromData();
				if(!prevPassanger.Equals(curPassanger)) {
					this.Focus();
					var mb = MessageBox.Show(
						"Данные пассажира были изменены. Хотите сохранить изменения?",
						"",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3
					);

					if(mb == DialogResult.Yes) {
						var result = saveEditedPassanger(curPassanger, (int) currentPassangerIndex);
						return result != null;
					}
					else if(mb == DialogResult.No) return true;
					else {
						Debug.Assert(mb == DialogResult.Cancel);
						return false;
					}
				} 
				else return true;
			}
			else if(curState == State.add) {
				this.Focus();
				var curPassanger = formPassangerFromData();
				var mb = MessageBox.Show(
					"Сохранить данные нового пассажира?",
					"",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button3
				);

				if(mb == DialogResult.Yes) {
					var result = saveNewPassanger(curPassanger);
					return result != null;
				}
				else if(mb == DialogResult.No) return true;
				else {
					Debug.Assert(mb == DialogResult.Cancel);
					return false;
				}
			}
			else return true;
		}

		private PassangerDisplay addPassangerDisplay(Communication.Passanger passanger, int index) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Data = index,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => {
				if(!promptSaveCustomer()) return;
				setStateSelect((int) ((PassangerDisplay) a).Data);
			};
			it.ContextMenuStrip = passangerMenu;

			passangersDisplay.RowCount++;
			passangersDisplay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			passangersDisplay.Controls.Add(it);

			return it;
		}

		private void saveButton_Click(object sender, EventArgs eventArgs) {
			save();
		}

		private void save() {
			if(status.booked) throw new InvalidOperationException();

			var passanger = formPassangerFromData();
			if(curState == State.add) {
				var result = saveNewPassanger(passanger);
				if(result != null) setStateSelect((int) result); 
			}
			else if(curState == State.edit) {
				var oldPassanger = customer.passangers[(int) currentPassangerIndex];
				if(!oldPassanger.Equals(passanger)) {
					var result = saveEditedPassanger(passanger, (int) currentPassangerIndex);
					if(result != null) setStateSelect((int) result);
				}
			}
			else Debug.Assert(curState == State.select || curState == State.none);
		}

		private Communication.Passanger formPassangerFromData() { 
			return new Communication.Passanger{
				name = nameText.Text,
				surname = surnameText.Text,
				middleName = middleNameText.Text,
				birthday = birthdayDatetime.Value,
				document = new Documents.Passport{ Number = 1212785785 }
			};
		}

		private void setDataFromPassanger(Communication.Passanger p) {
			nameText.Text = p.name;
			surnameText.Text = p.surname;
			middleNameText.Text = p.middleName;
			birthdayDatetime.Value = p.birthday;
			//TODO: fill document
		} 

		private void clearPassangerData() {
			nameText.Text = "";
            surnameText.Text = "";
            middleNameText.Text = "";
            birthdayDatetime.Value = DateTime.Today;
            //TODO: clear document
		}

		private void setErr(Exception e) { 
			statusLabel.Text = "Неизвестная ошибка";
			statusTooltip.SetToolTip(statusLabel, e.ToString());
		}

		private void setFine() {
			statusLabel.Text = "";
			statusTooltip.SetToolTip(statusLabel, null);
		}

		private int? saveNewPassanger(Communication.Passanger passanger) {
			if(status.booked) throw new InvalidOperationException();

			try {

			if(customer.LoggedIn) {
				var response = service.addPassanger(customer.Get(), passanger);
				if(response) { 
					var id = response.s;
					var index = customer.newPassangerIndex++;

					customer.passangers.Add(index, passanger);
					customer.passangerIds.Add(index, new PassangerIdData(id));

					var display = addPassangerDisplay(passanger, index);
					passangersDisplays.Add(index, display);
				
					setFine();
				
					return index;
				}
				else if(response.f.isLoginError) {
					promptFillCustomer(response.f.LoginError.message);
				}
				else {
					var msg = response.f.InputError.message;
					statusLabel.Text = msg;
					statusTooltip.SetToolTip(statusLabel, msg);
				}
			}
			else {
				var index = customer.newPassangerIndex++;

				customer.passangers.Add(index, passanger);
				customer.passangerIds.Add(index, new PassangerIdData(null));

				var display = addPassangerDisplay(passanger, index);
				passangersDisplays.Add(index, display);
				
				setFine();
				
				return index;
			}

			}catch(Exception e) { setErr(e); }

			return null;
		}

		private int? saveEditedPassanger(Communication.Passanger passanger, int index) {
			if(status.booked) throw new InvalidOperationException();

			try {
			var passangerIdData = customer.passangerIds[index];

			if(passangerIdData.IsLocal) {
				customer.passangers[index] = passanger;

				var passangerDisplay = passangersDisplays[index];
				passangerDisplay.Passanger = passanger;
				passangerDisplay.Data = index;

				return index;
			}
			else {
				if(!customer.LoggedIn) throw new InvalidOperationException(
					"Незарегестрированный пользователь не может менять данные пассажиров, зарегестрированных в базе"
				);
				//note: this is a potential bug
				//because button text is not updated
				//and in states that the copy will be saved
				//but actually the passanger itself is saved
				//and not ist copy if there is some error
				//in updating button text
				if(customer.passangers[index].archived) return saveNewPassanger(passanger);

				var response = service.replacePassanger(customer.Get(), passangerIdData.DatabaseId, passanger);
				if(response) { 
					var id = response.s;

					customer.passangers[index] = passanger;
					customer.passangerIds[index] = new PassangerIdData(id);

					var passangerDisplay = passangersDisplays[index];
					passangerDisplay.Passanger = passanger;
					passangerDisplay.Data = index;
					
					setFine();

					return index;
				}
				else if(response.f.isLoginError) {
					promptFillCustomer(response.f.LoginError.message);
				}
				else {
					var msg = response.f.InputError.message;
					statusLabel.Text = msg;
					statusTooltip.SetToolTip(statusLabel, msg);
				}
			}

			} catch(Exception e) { setErr(e); }
			return null;
		}

		private bool removePassanger(int index) {
			if(status.booked) throw new InvalidOperationException();

			try{
			var passangerIdData = customer.passangerIds[index];
			
			if(passangerIdData.IsLocal) {
				passangersDisplays[index].Dispose();
				passangersDisplays.Remove(index);

				customer.passangers.Remove(index);
				customer.passangerIds.Remove(index);
				return true;
			}
			else {
				if(!customer.LoggedIn) throw new InvalidOperationException(
					"Незарегестрированный пользователь не может удалять пассажиров, зарегестрированных в базе"
				);

				var response = service.removePassanger(customer.Get(), passangerIdData.DatabaseId);
				if(response) { 
					if(index == currentPassangerIndex) setStateNone();

					customer.passangers.Remove(index);
					customer.passangerIds.Remove(index);

					passangersDisplays[index].Dispose();
					passangersDisplays.Remove(index);

					setFine();

					return true;
				}
				else {
					if(response.f.isLoginError) promptFillCustomer(response.f.LoginError.message);
					else {
						var msg = response.f.InputError.message;
						statusLabel.Text = msg;
						statusTooltip.SetToolTip(statusLabel, msg);
					}
				}
			}
			} catch(Exception e) { setErr(e); }

			return false;
		}

		private void editButton_Click(object sender, EventArgs e) {
			Debug.Assert(curState == State.select || curState == State.none);
			setStateEdit();
		}

		private void addButton_Click(object sender, EventArgs e) {
			Debug.Assert(curState == State.edit || curState == State.select || curState == State.none);
			promptSaveCustomer();
			setStateAdd();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger((int) currentPassangerIndex);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (int) pass.Data;
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger(number);
		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (int) pass.Data;
			if(!promptSaveCustomer()) return;
			setStateSelect(number);
			setStateEdit();
		}
	}
}
