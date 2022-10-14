using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
			set{
				selectedPassangerId = value;
			}
        }

		public PassangerAdd(Communication.MessageService sq, Communication.Customer customer) {
			this.service = sq;
			this.customer = customer;
			this.passangersDisplays = new Dictionary<int, PassangerDisplay>();

			InitializeComponent();
			Misc.unfocusOnEscape(this);
		}

		private void setupPassangersDisplay() {
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

			if(selectedPassangerId == null) noPassangerSelection();
			else passangerSelected((int) selectedPassangerId);
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

		private void addButton_Click(object sender, EventArgs e) {
			noPassangerSelection();
			
            nameText.Text = "";
            surnameText.Text = "";
            middleNameText.Text = "";
            birthdayDatetime.Value = DateTime.Today;
            //TODO: fill document
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			PassangerDisplay it;
			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId]?.Dispose();

			noPassangerSelection();
		}

		private void passangerSelected(int passangerId) {
			noPassangerSelection();

			deleteButton.Enabled = true;
			selectedPassangerId = passangerId;
			passangersDisplays[passangerId].BackColor = Color.LightGray;

            var p = customer.passangers[passangerId];
            nameText.Text = p.name;
            surnameText.Text = p.surname;
            middleNameText.Text = p.middleName;
            birthdayDatetime.Value = p.birthday;
            //TODO: fill document

			saveButton.Text = "Сохранить";
			saveAndCloseButton.Text = "Сохранить и выйти";
		}

		private void noPassangerSelection() {
			deleteButton.Enabled = false;
			if(selectedPassangerId != null) passangersDisplays[(int) selectedPassangerId].BackColor = Color.White;
			selectedPassangerId = null;

			saveButton.Text = "Добавить";
			saveAndCloseButton.Text = "Добавить и выйти";
		}

		private PassangerDisplay addPassangerDisplay(Communication.Passanger passanger, int index) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Number = index,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => passangerSelected(((PassangerDisplay) a).Number);

			passangersDisplay.RowCount++;
			passangersDisplay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			passangersDisplay.Controls.Add(it);

			return it;
		}

		private void saveAndCloseButton_Click(object sender, EventArgs e) {
			if(addPassanger()) {
				this.DialogResult = DialogResult.OK;
			}
		}

		private void saveButton_Click(object sender, EventArgs eventArgs) {
			addPassanger();
		}

		private bool addPassanger() {
			var passanger = new Communication.Passanger{
				name = nameText.Text,
				surname = surnameText.Text,
				middleName = middleNameText.Text,
				birthday = birthdayDatetime.Value,
				document = new Documents.Passport{ Number = 1212785785 }
			};

			try {
				if(selectedPassangerId == null) { 
					var response = service.addPassanger(customer, passanger);
					if(response) { 
						var index = response.s;
						var display = addPassangerDisplay(passanger, index);
						customer.passangers.Add(index, passanger);
						passangersDisplays.Add(index, display);
						passangerSelected(index);

						statusLabel.Text = "";
						statusTooltip.SetToolTip(statusLabel, null);

						return true;
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
				else if(
					!customer.passangers[(int) selectedPassangerId].Equals(passanger)
				) {
					Console.WriteLine("AAAA");
					
					var response = service.replacePassanger(customer, (int) selectedPassangerId, passanger);
					if(response) { 
						var index = response.s;
						customer.passangers[index] = passanger;
						passangerSelected((int) selectedPassangerId);

						statusLabel.Text = "";
						statusTooltip.SetToolTip(statusLabel, null);

						return true;
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
					return true;
				}
			}
			catch(Exception e) {
				statusLabel.Text = "Неизвестная ошибка";
				statusTooltip.SetToolTip(statusLabel, e.ToString());
			}

			return false;
		}

		private void PassangerAdd_Load(object sender, EventArgs e) {
			setupPassangersDisplay();
		}
	}
}
