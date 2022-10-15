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
	public partial class PassangerAdd : Form {
		private Communication.MessageService service;
		private Communication.Customer customer;
		private Dictionary<int, PassangerDisplay> passangersDisplays; 

		private int? selectedPassangerId;

		private enum State {
			none, select, add, edit
		}

		private State curState;
		private void setStateAdd() {
			Debug.Assert(curState == State.select || curState == State.none);

			curState = State.add;
			
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = true;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = null;

			clearPassangerData();

			saveButton.Visible = true;
			saveButton.Text = "Добавить";
			saveAndCloseButton.Text = "Добавить и выйти";
		}
				
		private void setStateSelect(int newSelectedPassangerId) {
			if(!promptSaveCustomer()) return;

			curState = State.select;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			passangerDataTable.Enabled = false;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = newSelectedPassangerId;
			passangersDisplays[newSelectedPassangerId].BackColor = Color.LightGray;
			
			setDataFromPassanger(customer.passangers[newSelectedPassangerId]);

			saveButton.Visible = false;
			saveAndCloseButton.Text = "Выбрать и выйти";
		}

		private void setStateEdit() {
			Debug.Assert(curState == State.select || curState == State.none);
			curState = State.edit;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			passangerDataTable.Enabled = true;

			saveButton.Visible = true;
			saveButton.Text = "Сохранить";
			saveAndCloseButton.Text = "Сохранить и выйти";
		}

		public void setStateNone() {
			if(!promptSaveCustomer()) return;

			curState = State.none;

			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = false;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = null;

			clearPassangerData();

			saveButton.Visible = false;
			saveAndCloseButton.Text = "Отмена";
		}

        public Communication.Passanger SelectedPassanger{
            get{
				Communication.Passanger p;
				if(customer.LoggedIn && selectedPassangerId != null
					&& customer.passangers.TryGetValue((int) selectedPassangerId, out p)
				) return p;
				else return null;
			}
        }

        public int? SelectedPassangerIndex {
            get{
				return selectedPassangerId;
			}
        }

		public PassangerAdd(Communication.MessageService sq, Communication.Customer customer, int? defaultPassangerId) {
			this.service = sq;
			this.customer = customer;
			this.passangersDisplays = new Dictionary<int, PassangerDisplay>();

			InitializeComponent();
			Misc.unfocusOnEscape(this, (a, e) => {
				if(this.ActiveControl == null) {
					setStateNone();
					e.Handled = true;
				}
			});
			Misc.addDummyButton(this);

			setupPassangersDisplay(defaultPassangerId);
		}

		private void setupPassangersDisplay(int? defaultPassangerId) {
			var triedLoggingIn = false;
			do{
				if(customer.LoggedIn) {
					var passangers = customer.passangers;

					passangersDisplay.SuspendLayout();
					passangersDisplay.Controls.Clear();
					passangersDisplay.RowStyles.Clear();

					passangersDisplay.RowCount = passangers.Count;

					foreach(var passanger in passangers) {
						var display = addPassangerDisplay(passanger.Value, passanger.Key);
						passangersDisplays.Add(passanger.Key, display);
					}

					passangersDisplay.ResumeLayout(false);
					passangersDisplay.PerformLayout();

					break;
				}
				else if(triedLoggingIn) break;
				else {
					promptFillCustomer();
					triedLoggingIn = true;
				}
			} while(true);

			if(defaultPassangerId == null) setStateNone();
			else setStateSelect((int) defaultPassangerId);
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
				var prevPassanger = customer.passangers[(int) selectedPassangerId];
				var curPassanger = formPassangerFromData();
				if(!prevPassanger.Equals(curPassanger)) {
					var mb = MessageBox.Show(
						"Данные пассажира были изменены. Хотите сохранить изменения?",
						"",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3
					);

					if(mb == DialogResult.Yes) {
						var result = saveEditedPassanger(curPassanger, (int) selectedPassangerId);
						if(!result.Item1) return false;
					}
					else if(mb == DialogResult.No) {
						//nothing
					}
					else {
						Debug.Assert(mb == DialogResult.Cancel);
						return false;
					}
				}
			}
			else if(curState == State.add) {
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
					if(!result.Item1) return false;
				}
				else if(mb == DialogResult.No) {
					//nothing
				}
				else {
					Debug.Assert(mb == DialogResult.Cancel);
					return false;
				}
			}

			return true;
		}

		private PassangerDisplay addPassangerDisplay(Communication.Passanger passanger, int index) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Number = 999999, //999... is temporary placed in order to test wheter it is used anywhere or not
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => setStateSelect(index);

			passangersDisplay.RowCount++;
			passangersDisplay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			passangersDisplay.Controls.Add(it);

			return it;
		}

		private void saveAndCloseButton_Click(object sender, EventArgs e) {
			var result = save();
			if(result != null) this.DialogResult = (DialogResult) result;
		}

		private void saveButton_Click(object sender, EventArgs eventArgs) {
			save();
		}

		private DialogResult? save() {
			var passanger = formPassangerFromData();
			if(curState == State.add) {
				var result = saveNewPassanger(passanger);
				if(result.Item1) {
					setStateSelect(result.Item2); 
					return DialogResult.OK;
				}
			}
			else if(curState == State.edit) {
				var passangerId = (int) selectedPassangerId;
				var oldPassanger = customer.passangers[passangerId];
				if(!oldPassanger.Equals(passanger)) {
					var result = saveEditedPassanger(passanger, passangerId);
					if(result.Item1) {
						setStateSelect(result.Item2);
						return DialogResult.OK;
					}
				}
				else return DialogResult.OK;
			}
			else if(curState == State.select) {
				return DialogResult.OK;
			}
			else {
				Debug.Assert(curState == State.none);
				return DialogResult.Cancel;
			}

			return null;
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

		private Tuple<bool, int> saveNewPassanger(Communication.Passanger passanger) {
			var response = service.addPassanger(customer, passanger);
			if(response) { 
				var index = response.s;
				var display = addPassangerDisplay(passanger, index);
				customer.passangers.Add(index, passanger);
				passangersDisplays.Add(index, display);

				statusLabel.Text = "";
				statusTooltip.SetToolTip(statusLabel, null);

				return new Tuple<bool, int>(true, index);
			}
			else if(response.f.isLoginError) {
				promptFillCustomer(response.f.LoginError.message);
			}
			else {
				var msg = response.f.InputError.message;
				statusLabel.Text = msg;
				statusTooltip.SetToolTip(statusLabel, msg);
			}

			return new Tuple<bool, int>(false, 0);
		}

		private Tuple<bool, int> saveEditedPassanger(Communication.Passanger passanger, int passangerId) {
			var response = service.replacePassanger(customer, passangerId, passanger);
			if(response) { 
				var id = response.s;
				customer.passangers[id] = passanger;
				passangersDisplays[id].Passanger = passanger;
				
				statusLabel.Text = "";
				statusTooltip.SetToolTip(statusLabel, null);

				return new Tuple<bool, int>(true, id);
			}
			else if(response.f.isLoginError) {
				promptFillCustomer(response.f.LoginError.message);
			}
			else {
				var msg = response.f.InputError.message;
				statusLabel.Text = msg;
				statusTooltip.SetToolTip(statusLabel, msg);
			}
			return new Tuple<bool, int>(false, 0);
		}

		private void editButton_Click(object sender, EventArgs e) {
			setStateEdit();
		}

		private void addButton_Click(object sender, EventArgs e) {
			setStateAdd();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId]?.Dispose();
			setStateNone();
		}
	}
}
