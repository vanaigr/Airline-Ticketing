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
	public partial class SelectFlight : Form {
		private MessageService service;

		string Login;
		string Password;
		bool loggedIn;

		Dictionary<int, string> avaliableFlightClasses;
		List<City> cities;
		public SelectFlight() {
            InitializeComponent();

			pictureBox1.Image = TintImage.applyTint(pictureBox1.Image, Color.RoyalBlue);

			reconnect();

			try {
			fromLoc.SelectedIndex = 2;
			toLoc.SelectedIndex = 1;

			findFlightsButton_Click(findFlightsButton, new EventArgs());
			} catch(Exception){ }
		}

		void setupAvailableOptions() {
			try {
				var options = service.availableOptions();
				avaliableFlightClasses = options.flightClasses;
				cities = options.cities;
			}
			catch(Exception e) {
				statusLabel.ForeColor = Color.Firebrick;
				var errorMessage = e.ToString();
				statusLabel.Text = errorMessage;
				this.elementHint.SetToolTip(this.statusLabel, errorMessage);
			}
		}

		void updateLoginInfo() {
			loginLayoutPanel.Controls.Clear();

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

				loginLayoutPanel.Controls.Add(unloginButton);

				var accountName = new Label();

				accountName.AutoSize = true;
				accountName.Location = new Point(736, 10);
				accountName.Name = "AccountName";
				accountName.Padding = new Padding(8);
				accountName.TabIndex = 0;
				accountName.Text = Login;

				loginLayoutPanel.Controls.Add(accountName);
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

				loginLayoutPanel.Controls.Add(loginButton);
			}
		}

		void LoginButton_Click(object sender, EventArgs e) {
			var form = new LoginRegisterForm(service);
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

		private void findFlightsButton_Click(object sender, EventArgs e) {
			try {
				var fromCode = (fromLoc.SelectedItem as City?)?.code;
				var toCode = (toLoc.SelectedItem as City?)?.code;
				
				var result = service.matchingFlights(new MatchingFlightsParams{
					fromCode = fromCode, toCode = toCode,
					when = fromDepDate.Value
				});

				while (flightsTable.Controls.Count > 0) flightsTable.Controls[flightsTable.Controls.Count-1].Dispose();

				if(result.Count == 0) {
					var noResultsLabel = new Label();
					noResultsLabel.Font = new Font(noResultsLabel.Font.FontFamily, 12);
					noResultsLabel.Text = "Результаты не найдены";
					noResultsLabel.TextAlign = ContentAlignment.TopCenter;

					noResultsLabel.Dock = DockStyle.Top;
					flightsTable.RowStyles.Add(new RowStyle());
					flightsTable.Controls.Add(noResultsLabel, flightsTable.RowCount, 0);
				}
				else foreach(var flight in result) {
					var flightDisplay = new FlightDisplay();
					flightDisplay.updateFromFlight(avaliableFlightClasses, flight, fromCode, toCode, flight.flightName);
					flightDisplay.Dock = DockStyle.Top;
					flightDisplay.Click += (a, b) => { openFlightBooking(flight.id); };
					flightsTable.RowStyles.Add(new RowStyle());
					flightsTable.Controls.Add(flightDisplay, flightsTable.RowCount, 0);
				}
			}
			catch(FaultException<object> ex) {
				statusLabel.ForeColor = Color.Firebrick;
				statusLabel.Text = ex.Message;
				Console.WriteLine(ex);
			}
			catch(Exception ex) {
				statusLabel.ForeColor = Color.Firebrick;
				statusLabel.Text = "Неизвестная ошибка";
				Console.WriteLine(ex);
			}
		}

		class CityComboBox : ComboBox {
			private StringBuilder sb;

			public static readonly Color ForeColor2 = Color.DarkGray;

			public CityComboBox() {
				sb = new StringBuilder();
				DrawMode = DrawMode.OwnerDrawFixed;
			}

			protected override void OnDrawItem(DrawItemEventArgs e) {
		        e.DrawBackground();
		        e.DrawFocusRectangle();

				if(!(e.Index >= 0 && e.Index < Items.Count)) return;
		 
		        var item = (City) Items[e.Index];

				//draw city name
				sb.Clear().Append(item.name).Append(", ");
				var nameString = sb.ToString();
				var nameSize = e.Graphics.MeasureString(nameString, e.Font);
		        e.Graphics.DrawString(
					nameString, e.Font, new SolidBrush(e.ForeColor), 
					e.Bounds.Left, e.Bounds.Top + 2
				);

				//draw country name
				var countryString = sb.Clear().Append(item.country).Append(' ').ToString();
				var countrySize =  e.Graphics.MeasureString(countryString, e.Font);

				e.Graphics.DrawString(
					countryString, e.Font, new SolidBrush(ForeColor2), 
					e.Bounds.Left + nameSize.Width, e.Bounds.Top + 2
				);

				//draw city code
				var codeString = item.code;
				var codeSize =  e.Graphics.MeasureString(codeString, e.Font);

				e.Graphics.DrawString(
					codeString, e.Font, new SolidBrush(ForeColor), 
					Math.Max(
						e.Bounds.Left + nameSize.Width + countrySize.Width, 
						e.Bounds.Right - codeSize.Width
					),
					e.Bounds.Top + 2
				);
		 
		        base.OnDrawItem(e);
		    }
		}

		private void pictureBox1_Click(object sender, EventArgs e) {
			reconnect();
		}

		private Dictionary<int/*flightId*/, FlightBooking> openedBookings = new Dictionary<int, FlightBooking>();

		private void openFlightBooking(int flightId) {
			FlightBooking booking;
			if(openedBookings.TryGetValue(flightId, out booking)) {
				booking.Focus();
			}
			else {
				booking = new FlightBooking(service, flightId);
				openedBookings.Add(flightId, booking);
				booking.Show();
			}
		}

		void reconnect() {
			statusLabel.Text = "";
			(service as IDisposable)?.Dispose();
			service = ServerQuery.Create();

			loggedIn = false;

			setupAvailableOptions();
            updateLoginInfo();

			fromLoc.DisplayMember = "name";
			toLoc.DisplayMember = "name";

			fromLoc.BindingContext = new BindingContext();
			fromLoc.DataSource = cities;
			toLoc.BindingContext = new BindingContext();
			toLoc.DataSource = cities;

			fromLoc.SelectedIndex = -1;
			toLoc.SelectedIndex = -1;

			flightsTable.Controls.Clear();
		}
	}
}
