using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using ClientCommunication;
using Common;
using Communication;

namespace Client {
	public partial class SelectFlight : Form {
		private ClientService service;
		private Customer customer;

		//string[] avaliableFlightClasses;
		//List<City> cities;
		Context context;

		private Dictionary<int/*flightId*/, FlightDetailsFill> openedBookings = new Dictionary<int, FlightDetailsFill>();
		
		public SelectFlight() {
			customer = new Customer();

            InitializeComponent();

			Misc.unfocusOnEscape(this);
			Misc.addBottomDivider(tableLayoutPanel1);

			pictureBox1.Image = TintImage.applyTint(pictureBox1.Image, Color.RoyalBlue);

			reconnect();

			try {
			//fromLoc.SelectedIndex = 2;
			//toLoc.SelectedIndex = 1;
			
			//findFlightsButton_Click(findFlightsButton, new EventArgs());
			
			//customer = new CustomerData("User123", "789456123");
			} catch(Exception){ }

			setupAvailableOptions();
		}

		void setupAvailableOptions() {
			try {
				var options = service.parameters();
				var avaliableFlightClasses = options.flightClasses;
				var citiesList = options.cities;
				var cities = new Dictionary<string, City>(citiesList.Count);
				foreach(var city in citiesList) {
					cities.Add(city.code, city);
				}

				context = new Context{ 
					classesNames = avaliableFlightClasses,
					cities = cities
				};
				updateErrorDisplay(false, null, null);
			}
			catch(Exception e) {
				updateErrorDisplay(true, null, e);
			}
		}

		void updateErrorDisplay(bool isError, string message, Exception e) {
			if(isError) { 
				statusLabel.Text = message ?? "Неизвестная ошибка";
				this.elementHint.SetToolTip(this.statusLabel, e?.ToString() ?? message);
			}
			else {
				statusLabel.Text = "";
				this.elementHint.SetToolTip(this.statusLabel, null);
			}
		}

		void updateLoginInfo() {
			loginLayoutPanel.SuspendLayout();
			loginLayoutPanel.Controls.Clear();

			loginLayoutPanel.ColumnStyles.Clear();

			loginLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			loginLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
			loginLayoutPanel.ColumnCount = 2;
			

			var loginActionButton = new Button();

			Misc.setBetterFont(loginActionButton, 9, GraphicsUnit.Point);
			loginActionButton.Dock = DockStyle.Fill;
			loginActionButton.AutoSize = true;
			loginActionButton.BackColor = Color.Transparent;
			loginActionButton.FlatAppearance.BorderColor = Color.Gray;
			loginActionButton.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
			loginActionButton.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
			loginActionButton.FlatStyle = FlatStyle.Flat;

			if(customer.LoggedIn) { 
				loginActionButton.Text = "Выйти";
				loginActionButton.Click += new EventHandler(this.UnloginButton_Click);
			}
			else {
				loginActionButton.Text = "Вход/регестрация";
				loginActionButton.Click += new EventHandler(this.LoginButton_Click);
			}

			loginLayoutPanel.Controls.Add(loginActionButton, 1, 0);

			var accountName = new Label();

			Misc.setBetterFont(accountName, 9, GraphicsUnit.Point);
			accountName.Font = new Font(accountName.Font.Name, accountName.Font.SizeInPoints, FontStyle.Underline); 
			accountName.Cursor = Cursors.Hand;
			accountName.ForeColor = SystemColors.ControlText;
			accountName.Dock = DockStyle.Fill;
			accountName.AutoSize = true;
			accountName.Padding = new Padding(8);
			accountName.TabIndex = 0;
			accountName.MinimumSize = new Size(20, 20);
			accountName.Text = customer.LoggedIn ? customer.customer.Value.login : "Аккаунт";
			accountName.Click += (a, b) => openBookedFlightsHistory();

			loginLayoutPanel.Controls.Add(accountName, 0, 0);

			loginLayoutPanel.ResumeLayout(false);
			loginLayoutPanel.PerformLayout();
		}

		void LoginButton_Click(object sender, EventArgs e) {
			var form = new LoginRegisterForm(service, customer);
			form.beforeChangeAccount = () => warnEverythingWillBeClosed();  

			var result = form.ShowDialog();
			if(result == DialogResult.OK) {
				clearOpenedBookings();
				updateLoginInfo();
			}
		}

		void UnloginButton_Click(object sender, EventArgs e) {
			bool abort = warnEverythingWillBeClosed();
			if(abort) return;

			clearOpenedBookings();
			customer.unlogin();

			updateLoginInfo();
		}

		private void clearOpenedBookings() {
			foreach(var it in openedBookings) {
				it.Value.Dispose();
			}
			openedBookings.Clear();
		}

		private void findFlightsButton_Click(object sender, EventArgs e) {
			try {
				var fromCode = (fromLoc.SelectedItem as City?)?.code;
				var toCode = (toLoc.SelectedItem as City?)?.code;
				
				var response = service.findMatchingFlights(new MatchingFlightsParams{
					fromCode = fromCode, toCode = toCode,
					when = fromDepDate.Value
				});

				if(response) {
					var result = response.s;

					flightsTable.SuspendLayout();

					flightsTable.Controls.Clear();
					flightsTable.RowStyles.Clear();
					flightsTable.RowCount = 0;

					if(result.Count == 0) {
						var noResultsLabel = new Label();
						noResultsLabel.Font = new Font(noResultsLabel.Font.FontFamily, 12);
						noResultsLabel.Text = "Результаты не найдены";
						noResultsLabel.TextAlign = ContentAlignment.TopCenter;

						noResultsLabel.Dock = DockStyle.Top;
						flightsTable.RowStyles.Add(new RowStyle());
						flightsTable.Controls.Add(noResultsLabel, flightsTable.RowCount++, 0);
					}
					else foreach(var flight in result) {
						var flightDisplay = new FlightDisplay();
						flightDisplay.updateFromFlight(context, flight);
						flightDisplay.Dock = DockStyle.Top;
						flightDisplay.Click += new EventHandler(openFlightBooking);
						flightsTable.RowStyles.Add(new RowStyle());
						flightsTable.Controls.Add(flightDisplay, flightsTable.RowCount++, 0);
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
		private void pictureBox1_Click(object sender, EventArgs e) {
			reconnect();
		}

		private void openFlightBooking(object sender, EventArgs e) {
			var flightDisplay = (FlightDisplay) sender;
			var flight = flightDisplay.CurrentFlight;

			FlightDetailsFill booking;
			if(openedBookings.TryGetValue(flight.id, out booking)) {
				booking.Focus();
			}
			else {
				try {
					var result = service.getSeatsForFlight(flight.id);

					if(result) {
						booking = new FlightDetailsFill(
							service, customer, 
							context, new BookingStatus(), 
							flight, result.s
						);
						booking.FormClosed += (obj, args) => { openedBookings.Remove(((FlightDetailsFill) obj).CurrentFlight.id); };

						openedBookings.Add(flight.id, booking);
						booking.Show();

						updateErrorDisplay(false, null, null);
					}
					else updateErrorDisplay(true, result.f.message, null);
				}
				catch(Exception ex) {
					updateErrorDisplay(true, null, ex);
				}
			}
		}

		void reconnect() {
			updateErrorDisplay(false, null, null);
			(service as IDisposable)?.Dispose();
			service = ClientServerQuery.Create();

			customer.unlogin();

			setupAvailableOptions();
            updateLoginInfo();

			fromLoc.DisplayMember = "name";
			toLoc.DisplayMember = "name";

			fromLoc.DataSource = new BindingSource(){ DataSource = context?.cities.Values };
			//fromLoc.DisplayMember = "Value";
			toLoc.DataSource = new BindingSource(){ DataSource = context?.cities.Values };
			//fromLoc.DisplayMember = "Value";

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

		private void openBookedFlightsHistory() {
			var it = new FlightsHistory(service, context, customer);
			it.ShowDialog();
		}

		private bool warnEverythingWillBeClosed() {
			if(openedBookings.Count != 0
				|| (!customer.LoggedIn && customer.flightsBooked.Count != 0)
			) { 
				var result = MessageBox.Show(
					"При входе/выходе из аккаунта все данные в выбранных рейсах будут потеряны. Продолжить?",
					"", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
				);

				return result != DialogResult.Yes;
			}
			else return false;
		}

		private void showBookedPassangerButton_Click(object sender, EventArgs e) {

		}
	}
}
