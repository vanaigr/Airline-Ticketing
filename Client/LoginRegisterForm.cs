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
using Client;
using ClientCommunication;
using Common;

namespace Client {

	public partial class LoginRegisterForm : Form {
		CustomerContext customer;
		ClientService service;

		public AbortAction beforeChangeAccount;

		public delegate bool AbortAction();

		public LoginRegisterForm(ClientService service, CustomerContext customer) {
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

		public void setFine(string message) {
			statusLabel.ForeColor = SystemColors.ControlText;
			statusLabel.Text = message;
			statusTooltip.SetToolTip(statusLabel, message);
		}

		private void RegisterButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			try {
				var newCust = new Account(login, password);
				var response = service.registerAccount(newCust);
				if(response) {
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
				var abort = beforeChangeAccount?.Invoke();
				if(abort == true) {
					setFine("Вход отменён");
					return;
				}

				var newCust = new Account(login, password);
				var response = service.logInAccount(newCust);
				if(response) {
					var response2 = service.getPassangers(newCust);
					if(response2) {

						customer.unlogin();

						customer.setFrom(newCust);

						foreach(var newPassanger in response2.s) {
							var index = customer.newPassangerIndex++;
							customer.passangers.Add(index, newPassanger.Value);
							customer.passangerIds.Add(index, new PassangerIdData(newPassanger.Key));
						}

						setFine(null);
						
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

		bool focused = false;

		private void PasswordText_Enter(object sender, EventArgs e) {
			focused = true;
			tableLayoutPanel3.Refresh();
		}

		private void PasswordText_Leave(object sender, EventArgs e) {
			focused = false;
			tableLayoutPanel3.Refresh();
		}

		private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e) {
			if(focused) {
				var cr = tableLayoutPanel3.ClientRectangle;
				using(var p = new Pen(Color.FromArgb(0,120,215))) {
					e.Graphics.DrawRectangle(p, new Rectangle(cr.X, cr.Y, cr.Width-1, cr.Height-1));
				}
			}
			else {
				var cr = tableLayoutPanel3.ClientRectangle;
				using(var p = new Pen(Color.FromArgb(122,122,122))) {
					e.Graphics.DrawRectangle(p, new Rectangle(cr.X, cr.Y, cr.Width-1, cr.Height-1));
				}
			}
			//ControlPaint.DrawBorder(e.Graphics, tableLayoutPanel3.ClientRectangle, Color.FromArgb(0,120,215), ButtonBorderStyle.Solid);
		}
	}
}
