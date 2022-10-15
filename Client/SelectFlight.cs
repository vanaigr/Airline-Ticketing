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
		private CustomerData customer;

		Dictionary<int, string> avaliableFlightClasses;
		List<City> cities;
		
		public SelectFlight() {
			customer = new CustomerData();

            InitializeComponent();

			Misc.unfocusOnEscape(this);

			pictureBox1.Image = TintImage.applyTint(pictureBox1.Image, Color.RoyalBlue);

			reconnect();

			try {
			fromLoc.SelectedIndex = 2;
			toLoc.SelectedIndex = 1;

			findFlightsButton_Click(findFlightsButton, new EventArgs());

			customer = new CustomerData("User123", "789456123");
			customer.passangers = service.getPassangers((Customer) customer.customer).s;
			} catch(Exception){ }
		}

		void setupAvailableOptions() {
			try {
				var options = service.availableOptions();
				avaliableFlightClasses = options.flightClasses;
				cities = options.cities;
				updateErrorDisplay(false, null, null);
			}
			catch(Exception e) {
				updateErrorDisplay(true, null, e);
			}
		}

		void updateErrorDisplay(bool isError, string message, Exception e) {
			if(isError) { 
				statusLabel.Text = message ?? "Неизвестная ошибка";
				this.elementHint.SetToolTip(this.statusLabel, "" + e);
			}
			else {
				statusLabel.Text = "";
				this.elementHint.SetToolTip(this.statusLabel, null);
			}
		}

		void updateLoginInfo() {
			loginLayoutPanel.SuspendLayout();
			loginLayoutPanel.Controls.Clear();

			Misc.addDummyButton(loginLayoutPanel);

			if(customer.LoggedIn) {
				var unloginButton = new Button();

				unloginButton.AutoSize = true;
				unloginButton.BackColor = Color.Transparent;
				unloginButton.FlatAppearance.BorderColor = Color.Gray;
				unloginButton.FlatAppearance.BorderSize = 1;
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
				accountName.Text = customer.customer?.login;

				loginLayoutPanel.Controls.Add(accountName);
			}
			else {
				var loginButton = new Button();

				loginButton.AutoSize = true;
				loginButton.BackColor = Color.Transparent;
				loginButton.FlatAppearance.BorderColor = Color.Gray;
				loginButton.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
				loginButton.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
				loginButton.FlatStyle = FlatStyle.Flat;
				//loginButton.Name = "LoginButton";
				loginButton.Text = "Войти или зарегестрироваться";
				loginButton.Click += new EventHandler(this.LoginButton_Click);
				
				loginLayoutPanel.Controls.Add(loginButton);	
			}

			loginLayoutPanel.ResumeLayout(false);
			loginLayoutPanel.PerformLayout();
		}

		void LoginButton_Click(object sender, EventArgs e) {
			var form = new LoginRegisterForm(service, customer);
			var result = form.ShowDialog();
			if(result == DialogResult.OK) {
				updateLoginInfo();
			}
		}

		void UnloginButton_Click(object sender, EventArgs e) {
			customer.unlogin();
			updateLoginInfo();
		}

		private void findFlightsButton_Click(object sender, EventArgs e) {
			try {
				var fromCode = (fromLoc.SelectedItem as City?)?.code;
				var toCode = (toLoc.SelectedItem as City?)?.code;
				
				var response = service.matchingFlights(new MatchingFlightsParams{
					fromCode = fromCode, toCode = toCode,
					when = fromDepDate.Value
				});

				if(response) {
					var result = response.s;

					flightsTable.SuspendLayout();

					flightsTable.Controls.Clear();
					flightsTable.RowStyles.Clear();

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
						var flightAndCities = new FlightAndCities{
							flight = flight, fromCityCode = fromCode, toCityCode = toCode,
						};
						flightDisplay.updateFromFlight(avaliableFlightClasses, flightAndCities);
						flightDisplay.Dock = DockStyle.Top;
						flightDisplay.Click += new EventHandler(openFlightBooking);
						flightsTable.RowStyles.Add(new RowStyle());
						flightsTable.Controls.Add(flightDisplay, flightsTable.RowCount, 0);
					}

					flightsTable.ResumeLayout(false);
					flightsTable.PerformLayout();

					updateErrorDisplay(false, null, null);
				}
				else {
					updateErrorDisplay(true, response.f.message, null);
				}
			}
			catch(FaultException<ExceptionDetail> ex) {
				updateErrorDisplay(true, null, ex);
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

		private void openFlightBooking(object sender, EventArgs e) {
			var flightDisplay = (FlightDisplay) sender;
			var fic = flightDisplay.CurrentFlight;
			var classId = flightDisplay.SelectedClass;

			FlightBooking booking;
			if(openedBookings.TryGetValue(fic.flight.id, out booking)) {
				booking.Focus();
				booking.setSelectedClass(classId);
			}
			else {
				booking = new FlightBooking(service, customer);
				booking.setFromFlight(avaliableFlightClasses, fic, classId);
				booking.FormClosed += (obj, args) => { openedBookings.Remove(((FlightBooking) obj).CurrentFlight.flight.id); };

				openedBookings.Add(fic.flight.id, booking);
				booking.Show();
			}
		}

		void reconnect() {
			updateErrorDisplay(false, null, null);
			(service as IDisposable)?.Dispose();
			service = ServerQuery.Create();

			customer.unlogin();

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

		//https://stackoverflow.com/a/3526775/18704284
		private void SelectFlight_KeyDown(object sender, KeyEventArgs e) {
			var form = (Form) sender;
			if(e.KeyCode == Keys.Escape) {
				form.ActiveControl = null;
				e.Handled = true;
			}
			else e.Handled = false;
		}

		private void flightsTable_Paint(object sender, PaintEventArgs e) {
			ActiveControl = Misc.addDummyButton(this);
		}
	}
}
