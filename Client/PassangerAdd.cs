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
		private Dictionary<int, Communication.Passanger> passangers; 

		private PassangerDisplay selectedPassanger;

		public PassangerAdd(Communication.MessageService sq, Communication.Customer customer) {
			this.service = sq;
			this.customer = customer;

			InitializeComponent();
			Misc.unfocusOnEscape(this);

			try{
			updatePassangersDisplay();} catch(Exception){ }

			noPassangerSelection();
		}

		private void updatePassangersDisplay() {
			passangersDisplay.SuspendLayout();
			passangersDisplay.Controls.Clear();
			passangersDisplay.RowStyles.Clear();

			passangers = service.getPassangers(customer);
			
			passangersDisplay.RowCount = passangers.Count;

			foreach(var passanger in passangers) {
				addPassangerDisplay(passanger.Value, passanger.Key);
			}

			passangersDisplay.ResumeLayout(false);
			passangersDisplay.PerformLayout();
		}

		private void addButton_Click(object sender, EventArgs e) {
			noPassangerSelection();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			selectedPassanger?.Dispose();
			noPassangerSelection();
		}

		private void passangerSelected(PassangerDisplay pd) {
			deleteButton.Enabled = true;
			selectedPassanger = pd;
		}

		private void noPassangerSelection() {
			deleteButton.Enabled = false;
			selectedPassanger = null;
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
			this.Close();
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
				int index;
				if(selectedPassanger == null) { 
					index = service.addPassanger(customer, passanger);
					var display = addPassangerDisplay(passanger, index);
					selectedPassanger = display;
				}
				else {
					index = service.replacePassanger(customer, selectedPassanger.Number, passanger);
					selectedPassanger.Passanger = passanger;
				}
				passangers.Add(index, passanger);
				
				statusLabel.Text = "лёха";
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
