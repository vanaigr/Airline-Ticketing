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
		class PassangerAndDisplay {
			public Communication.Passanger passanger;
			public PassangerDisplay display;
		}
		private Communication.MessageService service;
		private Communication.Customer customer;
		private Dictionary<int, PassangerAndDisplay> passangers; 

		private Communication.Passanger lastPassanger;
		private PassangerDisplay selectedPassanger;

        public Communication.Passanger SelectedPassanger{
            get{
				return selectedPassanger?.Passanger;
			}
        }

        public int? SelectedPassangerIndex {
            get{
				return selectedPassanger?.Number;
			}
			set{
				if(value == null) noPassangerSelection();
				else passangerSelected(passangers[(int)value].display);
			}
        }

		public PassangerAdd(Communication.MessageService sq, Communication.Customer customer) {
			this.service = sq;
			this.customer = customer;
			this.passangers = new Dictionary<int, PassangerAndDisplay>();

			InitializeComponent();
			Misc.unfocusOnEscape(this);

			try{
			updatePassangersDisplay();} catch(Exception){ }

			
		}

		private void updatePassangersDisplay() {
			passangers.Clear();
			noPassangerSelection();

			passangersDisplay.SuspendLayout();
			passangersDisplay.Controls.Clear();
			passangersDisplay.RowStyles.Clear();

			var passangersOnly = service.getPassangers(customer);
			
			passangersDisplay.RowCount = passangers.Count;

			foreach(var passanger in passangersOnly) {
				var display = addPassangerDisplay(passanger.Value, passanger.Key);
				passangers.Add(passanger.Key, new PassangerAndDisplay {
						passanger = passanger.Value, display = display
				});
			}

			passangersDisplay.ResumeLayout(false);
			passangersDisplay.PerformLayout();
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
			selectedPassanger?.Dispose();
			noPassangerSelection();
		}

		private void passangerSelected(PassangerDisplay pd) {
			noPassangerSelection();

			deleteButton.Enabled = true;
			selectedPassanger = pd;
			selectedPassanger.BackColor = Color.CornflowerBlue;

            var p = selectedPassanger.Passanger;
            nameText.Text = p.name;
            surnameText.Text = p.surname;
            middleNameText.Text = p.middleName;
            birthdayDatetime.Value = p.birthday;
            //TODO: fill document
		}

		private void noPassangerSelection() {
			deleteButton.Enabled = false;
			if(selectedPassanger != null) selectedPassanger.BackColor = Color.White;
			selectedPassanger = null;
			lastPassanger = new Communication.Passanger();
		}

		private PassangerDisplay addPassangerDisplay(Communication.Passanger passanger, int index) {
			var it = new PassangerDisplay{ 
				Passanger = passanger, Number = index,
				Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
				ToolTip = passangerTooltip,
			};
			it.Click += (a, b) => passangerSelected((PassangerDisplay) a);

			passangersDisplay.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			passangersDisplay.Controls.Add(it);

			return it;
		}

		private void saveAndCloseButton_Click(object sender, EventArgs e) {
			saveButton_Click(sender, e);
			this.DialogResult = DialogResult.OK;
		}

		private void saveButton_Click(object sender, EventArgs eventArgs) {
			var passanger = new Communication.Passanger{
				name = nameText.Text,
				surname = surnameText.Text,
				middleName = middleNameText.Text,
				birthday = birthdayDatetime.Value,
				document = new Documents.Passport{ Number = 1212785785 }
			};

			try {
				if(selectedPassanger == null) { 
					var index = service.addPassanger(customer, passanger);
					var display = addPassangerDisplay(passanger, index);
					passangers.Add(index, new PassangerAndDisplay {
						passanger = passanger,
						display = display
					});
					passangerSelected(display);
				}
				else if(lastPassanger != passanger) {
					var index = service.replacePassanger(customer, selectedPassanger.Number, passanger);
					passangers[index].passanger = passanger;
					passangerSelected(selectedPassanger);
				}
				
				statusLabel.Text = "";
				statusTooltip.SetToolTip(statusLabel, null);
			} catch(FaultException<object> e) {
				statusLabel.Text = e.Message;
				statusTooltip.SetToolTip(statusLabel, e.ToString());
			} catch(Exception e) {
				statusLabel.Text = "Неизвестная ошибка";
				statusTooltip.SetToolTip(statusLabel, e.ToString());
			}
		}
	}
}
