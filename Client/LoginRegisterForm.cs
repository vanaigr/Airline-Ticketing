using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using Communication;

namespace Client {

	public partial class LoginRegisterForm : Form {
		public string Login { get; private set; }
		public string Password { get; private set; }

		MessageService service;

		public LoginRegisterForm(MessageService service) {
			InitializeComponent();
			Misc.unfocusOnEscape(this);
			this.service = service;
		}

		private void RegisterButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			try {
				service.register(login, password);

				label1.ForeColor = SystemColors.ControlText;
				label1.Text = "Аккаунт зарегистрирован";
			} 
			catch(FaultException<object> ex) {
				label1.ForeColor = Color.Firebrick;
				label1.Text = ex.Message;
			}
			catch(Exception ex) {
				label1.ForeColor = Color.Firebrick;
				label1.Text = "Неизвестная ошибка: " + ex;
			}
		}

		private void LoginButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			try {
				service.logIn(login, password);

				label1.ForeColor = SystemColors.ControlText;
				label1.Text = "";

				Login = login;
				Password = password;
				DialogResult = DialogResult.OK;
				Close();
			}
			catch(FaultException<object> ex) {
				label1.ForeColor = Color.Firebrick;
				label1.Text = ex.Message;
			}
			catch(Exception ex) {
				label1.ForeColor = Color.Firebrick;
				label1.Text = "Неизвестная ошибка: " + ex;
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
