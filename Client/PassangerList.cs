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
		private Dictionary<int, PassangerDisplay> passangersDisplays; 

		private int? selectedPassangerId;

		private enum State {
			none, select, add, edit
		}

		private State curState;
		private void setStateAdd() {
			curState = State.add;
			
			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = true;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = null;

			clearPassangerData();

			saveButton.Visible = true;
			saveButton.Text = "Добавить";
		}
				
		private void setStateSelect(int newSelectedPassangerId) {
			curState = State.select;

			deleteButton.Enabled = true;
			editButton.Enabled = true;
			passangerDataTable.Enabled = false;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = newSelectedPassangerId;
			passangersDisplays[newSelectedPassangerId].BackColor = Color.Gainsboro;
			
			setDataFromPassanger(customer.passangers[newSelectedPassangerId]);

			saveButton.Visible = false;
		}

		private void setStateEdit() {
			curState = State.edit;

			deleteButton.Enabled = true;
			editButton.Enabled = false;
			passangerDataTable.Enabled = true;

			saveButton.Visible = true;
			saveButton.Text = "Сохранить";
		}

		public void setStateNone() {
			curState = State.none;

			deleteButton.Enabled = false;
			editButton.Enabled = false;
			passangerDataTable.Enabled = false;

			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = null;

			clearPassangerData();

			saveButton.Visible = false;
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

		public PassangerList() {
			InitializeComponent();
			//Misc.unfocusOnEscape(this, (a, e) => {
			//	if(this.ActiveControl == null) {
			//		if(!promptSaveCustomer()) return;
			//		setStateNone();
			//		e.Handled = true;
			//	}
			//});
			//Misc.addDummyButton(this);

			
		}

		public void selectNone() {
			if(!promptSaveCustomer()) return;
			setStateNone();
		}

		public void init(Communication.MessageService sq, CustomerData customer, int? defaultPassangerId) {
			this.service = sq;
			this.customer = customer;
			this.passangersDisplays = new Dictionary<int, PassangerDisplay>();

			setupPassangersDisplay(defaultPassangerId);
		}

		public bool onClose() {
			return promptSaveCustomer();
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
					this.Focus();
					var mb = MessageBox.Show(
						"Данные пассажира были изменены. Хотите сохранить изменения?",
						"",
						MessageBoxButtons.YesNoCancel,
						MessageBoxIcon.Warning,
						MessageBoxDefaultButton.Button3
					);

					if(mb == DialogResult.Yes) {
						var result = saveEditedPassanger(curPassanger, (int) selectedPassangerId);
						return result.Item1;
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
					return result.Item1;
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
				Passanger = passanger, Number = index,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => {
				if(!promptSaveCustomer()) return;
				setStateSelect(((PassangerDisplay) a).Number);
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
				if(result.Item1) setStateSelect(result.Item2); 
			}
			else if(curState == State.edit) {
				var passangerId = (int) selectedPassangerId;
				var oldPassanger = customer.passangers[passangerId];
				if(!oldPassanger.Equals(passanger)) {
					var result = saveEditedPassanger(passanger, passangerId);
					if(result.Item1) setStateSelect(result.Item2);
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

		private Tuple<bool, int> saveNewPassanger(Communication.Passanger passanger) {
			var response = service.addPassanger((Communication.Customer) customer.customer, passanger);
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
			var response = service.replacePassanger((Communication.Customer) customer.customer, passangerId, passanger);
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

		private bool removePassanger(int index) {
			var response = service.removePassanger((Communication.Customer) customer.customer, index);
			if(response) { 
				if(index == selectedPassangerId) setStateNone();
				passangersDisplays[index].Dispose();
				passangersDisplays.Remove(index);
				customer.passangers.Remove(index);

				return true;
			}
			else {
				promptFillCustomer(response.f.message);
			}
			return false;
			
		}

		private void editButton_Click(object sender, EventArgs e) {
			Debug.Assert(curState == State.select || curState == State.none);
			setStateEdit();
		}

		private void addButton_Click(object sender, EventArgs e) {
			Debug.Assert(curState == State.select || curState == State.none);
			setStateAdd();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			if(result == DialogResult.Yes) removePassanger((int) selectedPassangerId);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = pass.Number;
			var result = MessageBox.Show(
				"Вы точно хотите удалить данного пассажира?", "",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2
			);
			removePassanger(number);
		}

		private void изменитьToolStripMenuItem_Click(object sender, EventArgs e) {
			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = pass.Number;
			if(!promptSaveCustomer()) return;
			setStateSelect(number);
			setStateEdit();
		}
	}
}
