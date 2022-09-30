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

	public partial class LoginRegisterForm : Form {
		public string Login { get; private set; }
		public string Password { get; private set; }

		ServerQuery query;

		public LoginRegisterForm(ServerQuery query) {
			InitializeComponent();
			this.query = query;
		}

		private void RegisterButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			var result = query.query(new Communication.Params {
				login = login,
				password = password,
				action = new Communication.RegisterAction()
			});

			if(result.statusOk) {
				label1.ForeColor = SystemColors.ControlText;
			}
			else {
				label1.ForeColor = Color.Firebrick;
			}
			label1.Text = result.message;
		}

		private void LoginButton_Click(object sender, EventArgs e) {
			var login = LoginText.Text;
			var password = PasswordText.Text;

			var result = query.query(new Communication.Params { login = login, password = password });
			
			if(result.statusOk) {
				label1.ForeColor = SystemColors.ControlText;
			}
			else {
				label1.ForeColor = Color.Firebrick;
			}
			label1.Text = result.message;

			if(result.statusOk) {
				Login = login;
				Password = password;
				DialogResult = DialogResult.OK;
				Close();
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
