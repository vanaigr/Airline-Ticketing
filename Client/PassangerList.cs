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
		private Dictionary<PassangerId, PassangerDisplay> passangersDisplays;

		private BookingPassanger passanger;

		private PassangerId currentPassangerId {
			get { return passanger.passangerId; }
			set { passanger.passangerId = value; }
		}

		private enum State {
			none, select, add, edit
		}

		private State curState;
		private void setStateAdd() {
			curState = State.add;
			
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			addButton.Enabled = false;
			passangerDataTable.Enabled = true;

			if(currentPassangerId.isValid) passangersDisplays[currentPassangerId].BackColor = Color.White;
			currentPassangerId = new PassangerId();

			clearPassangerData();

			saveButton.Visible = true;
			saveButton.Text = "Добавить";
		}
				
		private void setStateSelect(PassangerId newSelectedPassangerId) {
			curState = State.select;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			addButton.Enabled = true;
			passangerDataTable.Enabled = false;

			if(currentPassangerId.isValid) passangersDisplays[currentPassangerId].BackColor = Color.White;
			currentPassangerId = newSelectedPassangerId;
			passangersDisplays[currentPassangerId].BackColor = Misc.selectionColor3;
			
			setDataFromPassanger(customer.passangerAt(currentPassangerId));

			saveButton.Visible = false;
		}

		private void setStateEdit() {
			curState = State.edit;

			deleteButton.Enabled = true;
			editButton.Enabled = false;
			addButton.Enabled = true;
			passangerDataTable.Enabled = true;

			saveButton.Visible = true;

			var passanger = customer.passangerAt(currentPassangerId);
			if(passanger.archived) saveButton.Text = "Сохранить копию";
			else saveButton.Text = "Сохранить";
		}

		public void setStateNone() {
			curState = State.none;

			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = false;

			if(currentPassangerId.isValid) passangersDisplays[currentPassangerId].BackColor = Color.White;
			currentPassangerId = new PassangerId();

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

		public void init(Communication.MessageService sq, CustomerData customer, BookingPassanger passanger) {
			this.service = sq;
			this.customer = customer;
			this.passanger = passanger;
			this.passangersDisplays = new Dictionary<PassangerId, PassangerDisplay>();

			setupPassangersDisplay(passanger.passangerId);
		}

		public bool onClose() {
			return promptSaveCustomer();
		}

		private void setupPassangersDisplay(PassangerId defaultPassangerId) {
			passangersDisplay.SuspendLayout();
			passangersDisplay.Controls.Clear();
			passangersDisplay.RowStyles.Clear();

			passangersDisplay.RowCount = customer.localPassangers.Count + customer.databasePassangers.Count;

			foreach(var passanger in customer.localPassangers) {
				var id = PassangerId.fromLocalIndex(passanger.Key);
				var display = addPassangerDisplay(passanger.Value, id);
				passangersDisplays.Add(id, display);
			}

			foreach(var passanger in customer.databasePassangers) {
				var id = PassangerId.fromDatabaseIndex(passanger.Key);
				var display = addPassangerDisplay(passanger.Value, id);
				passangersDisplays.Add(id, display);
			}

			passangersDisplay.ResumeLayout(false);
			passangersDisplay.PerformLayout();

			if(defaultPassangerId.isValid) setStateSelect(defaultPassangerId);
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
				var prevPassanger = customer.passangerAt(currentPassangerId);
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
						var result = saveEditedPassanger(curPassanger, currentPassangerId);
						return result.isValid;
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
					return result.isValid;
				}
				else if(mb == DialogResult.No) return true;
				else {
					Debug.Assert(mb == DialogResult.Cancel);
					return false;
				}
			}
			else return true;
		}

		private PassangerDisplay addPassangerDisplay(Communication.Passanger passanger, PassangerId id) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Data = id,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => {
				if(!promptSaveCustomer()) return;
				setStateSelect((PassangerId) ((PassangerDisplay) a).Data);
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
			var passanger = formPassangerFromData();
			if(curState == State.add) {
				var result = saveNewPassanger(passanger);
				if(result.isValid) setStateSelect(result); 
			}
			else if(curState == State.edit) {
				var oldPassanger = customer.passangerAt(currentPassangerId);
				if(!oldPassanger.Equals(passanger)) {
					var result = saveEditedPassanger(passanger, currentPassangerId);
					if(result.isValid) setStateSelect(result);
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

		private PassangerId saveNewPassanger(Communication.Passanger passanger) {
			try {

			if(customer.LoggedIn) {
				var response = service.addPassanger(customer.Get(), passanger);
				if(response) { 
					var id = PassangerId.fromDatabaseIndex(response.s);
					var display = addPassangerDisplay(passanger, id);
					customer.databasePassangers.Add(id.Index, passanger);
					passangersDisplays.Add(id, display);
				
					setFine();
				
					return id;
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
				var id = PassangerId.fromLocalIndex(customer.newLocalPassangerId++);
				var display = addPassangerDisplay(passanger, id);
				customer.localPassangers.Add(id.Index, passanger);
				passangersDisplays.Add(id, display);
				
				setFine();
				
				return id;
			}

			}catch(Exception e) { setErr(e); }

			return new PassangerId();
		}

		private PassangerId saveEditedPassanger(Communication.Passanger passanger, PassangerId passangerId) {
			try {
			if(!passangerId.isValid) return new PassangerId();
			else if(passangerId.IsLocal) {
				customer.localPassangers[passangerId.Index] = passanger;
				var passangerDisplay = passangersDisplays[passangerId];
				passangerDisplay.Passanger = passanger;
				passangerDisplay.Data = passangerId;

				return passangerId;
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
				if(customer.passangerAt(passangerId).archived) return saveNewPassanger(passanger);

				var response = service.replacePassanger(customer.Get(), passangerId.Index, passanger);
				if(response) { 
					var id = PassangerId.fromDatabaseIndex(response.s);
					customer.databasePassangers[id.Index] = passanger;
					var passangerDisplay = passangersDisplays[id];
					passangerDisplay.Passanger = passanger;
					passangerDisplay.Data = id;
					
					setFine();

					return id;
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
			return new PassangerId();
		}

		private bool removePassanger(PassangerId index) {
			try{
			if(!index.isValid) return true;
			else if(index.IsLocal) {
				passangersDisplays[index].Dispose();
				passangersDisplays.Remove(index);
				customer.localPassangers.Remove(index.Index);
				return true;
			}
			else {
				if(!customer.LoggedIn) throw new InvalidOperationException(
					"Незарегестрированный пользователь не может удалять пассажиров, зарегестрированных в базе"
				);

				var response = service.removePassanger(customer.Get(), index.Index);
				if(response) { 
					if(Equals(index, currentPassangerId)) setStateNone();
					passangersDisplays[index].Dispose();
					passangersDisplays.Remove(index);
					customer.databasePassangers.Remove(index.Index);

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
			if(result == DialogResult.Yes) removePassanger(currentPassangerId);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (PassangerId) pass.Data;
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger(number);
		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = (PassangerId) pass.Data;
			if(!promptSaveCustomer()) return;
			setStateSelect(number);
			setStateEdit();
		}
	}
}
