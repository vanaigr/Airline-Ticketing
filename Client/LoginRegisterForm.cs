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
using Communication;

namespace Client {

	public partial class LoginRegisterForm : Form {
		CustomerData customer;
		MessageService service;

		public LoginRegisterForm(MessageService service, CustomerData customer) {
			this.service = service;
			this.customer = customer;

			InitializeComponent();
			Misc.unfocusOnEscape(this);
			
			LoginText.Text = customer.customer?.login;
			PasswordText.Text = customer.customer?.password;
		}

		public void setError(string message) {
			statusLabel.ForeColor = Color.Firebrick;
			statusLabel.Text = message;
			statusTooltip.SetToolTip(statusLabel, message);
		}

		private void RegisterButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			try {
				var newCust = new Customer(login, password);
				var response = service.register(newCust);
				if(response) { 
					Console.WriteLine(response.IsSuccess);
					customer.setFrom(newCust);

					statusLabel.ForeColor = SystemColors.ControlText;
					statusLabel.Text = "Аккаунт зарегистрирован";
				}
				else {
					setError(response.f.message);
				}
			}
			catch(FaultException<ExceptionDetail> ex) {
				statusLabel.ForeColor = Color.Firebrick;
				statusLabel.Text = "Неизвестная ошибка";
				statusTooltip.SetToolTip(statusLabel, ex.ToString());
			}
		}

		private void LoginButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;
			
			try {
				var newCust = new Customer(login, password);
				var response = service.logIn(newCust);
				if(response) {
					var response2 = service.getPassangers(newCust);
					if(response2) {
						customer.unlogin() /*
							all the passangers used previously are lost
							if the user was unlogged
						*/;
						//TODO: fix looging in if any flight is being booked

						customer.setFrom(newCust);

						foreach(var newPassanger in response2.s) {
							var index = customer.newPassangerIndex++;
							customer.passangers.Add(index, newPassanger.Value);
							customer.passangerIds.Add(index, new PassangerIdData(newPassanger.Key));
						}

						statusLabel.ForeColor = SystemColors.ControlText;
						statusLabel.Text = "";
						
						DialogResult = DialogResult.OK;
						Close();
					}
					else setError(response2.f.message);
				}
				else setError(response.f.message);
			}
			catch(FaultException<ExceptionDetail> ex) {
				statusLabel.ForeColor = Color.Firebrick;
				statusLabel.Text = "Неизвестная ошибка";
				statusTooltip.SetToolTip(statusLabel, ex.ToString());
			}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
			PasswordText.UseSystemPasswordChar = false;
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
			PasswordText.UseSystemPasswordChar = true;
		}

		private void showPassword_MouseLeave(object sender, EventArgs e) {
			PasswordText.UseSystemPasswordChar = true;
		}
	}
}
