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
	public partial class SelectFlight : Form {
		private ServerQuery query;

		string Login;
		string Password;
		bool loggedIn;

		Dictionary<int, string> avaliableFlightClasses;

		public SelectFlight() {
			query = new ServerQuery();

			InitializeComponent();
			setupAvailableOptions();
			updateLoginInfo();
		}

		void setupAvailableOptions() {
			try {
				var result = query.query(new Communication.Params {
					login = Login,
					password = Password,
					action = new Communication.QueryAvailableOptionsAction()
				});

				if(!result.statusOk) throw new Exception(result.message);

				var options = (Communication.AvailableOptionsResult)result.result;
				avaliableFlightClasses = options.flightClasses;

				var source = new BindingSource();
				source.DataSource = avaliableFlightClasses.Values;
				classSelector.DataSource = source;
			}
			catch(Exception e) {
				statusLabel.ForeColor = Color.Firebrick;
				var errorMessage = e.ToString();
				statusLabel.Text = errorMessage;
				this.elementHint.SetToolTip(this.statusLabel, errorMessage);
			}
		}

		void updateLoginInfo() {
			flowLayoutPanel1.Controls.Clear();

			if(loggedIn) {
				var unloginButton = new Button();

				unloginButton.AutoSize = true;
				unloginButton.BackColor = Color.Transparent;
				unloginButton.FlatAppearance.BorderColor = SystemColors.ControlLight;
				unloginButton.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
				unloginButton.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
				unloginButton.FlatStyle = FlatStyle.Flat;
				unloginButton.Location = new Point(11, 68);
				unloginButton.Name = "UnloginButton";
				unloginButton.TabIndex = 6;
				unloginButton.Text = "Выйти";
				unloginButton.UseVisualStyleBackColor = false;
				unloginButton.Click += new System.EventHandler(this.UnloginButton_Click);

				flowLayoutPanel1.Controls.Add(unloginButton);

				var accountName = new Label();

				accountName.AutoSize = true;
				accountName.Location = new Point(736, 10);
				accountName.Name = "AccountName";
				accountName.Padding = new Padding(8);
				accountName.TabIndex = 0;
				accountName.Text = Login;

				flowLayoutPanel1.Controls.Add(accountName);
			}
			else {
				var loginButton = new Button();

				loginButton.AutoSize = true;
				loginButton.BackColor = Color.Transparent;
				loginButton.FlatAppearance.BorderColor = SystemColors.ControlLight;
				loginButton.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
				loginButton.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
				loginButton.FlatStyle = FlatStyle.Flat;
				loginButton.Location = new Point(11, 68);
				loginButton.Name = "LoginButton";
				loginButton.TabIndex = 6;
				loginButton.Text = "Войти или зарегестрироваться";
				loginButton.UseVisualStyleBackColor = false;
				loginButton.Click += new EventHandler(this.LoginButton_Click);

				flowLayoutPanel1.Controls.Add(loginButton);
			}
		}

		void LoginButton_Click(object sender, EventArgs e) {
			var form = new LoginRegisterForm(query);
			var result = form.ShowDialog();
			if(result == DialogResult.OK) {
				Login = form.Login;
				Password = form.Password;
				loggedIn = true;

				updateLoginInfo();
			}
		}

		void UnloginButton_Click(object sender, EventArgs e) {
			Login = null;
			Password = null;
			loggedIn = false;

			updateLoginInfo();
		}

		private void classSelector_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {

		}
	}
}
