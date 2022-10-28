using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using Common;
using Communication;
using OperatorViewCommunication;

namespace OperatorView {
	public partial class SelectFlight : Form {
		private MessageService service;

		string[] classesNames;
		List<City> cities;
		
		public SelectFlight() {
            InitializeComponent();

			Misc.unfocusOnEscape(this);
			Misc.addBottomDivider(tableLayoutPanel1);

			pictureBox1.Image = TintImage.applyTint(pictureBox1.Image, Color.RoyalBlue);

			reconnect();

			try {
			fromLoc.SelectedIndex = 2;
			toLoc.SelectedIndex = 1;
			
			findFlightsButton_Click(findFlightsButton, new EventArgs());
			} catch(Exception){ }

			setupAvailableOptions();
		}

		void setupAvailableOptions() {
			try {
				var options = service.availableOptions();
				classesNames = options.flightClasses;
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
				this.elementHint.SetToolTip(this.statusLabel, e?.ToString() ?? message);
			}
			else {
				statusLabel.Text = "";
				this.elementHint.SetToolTip(this.statusLabel, null);
			}
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
						flightDisplay.updateFromFlight(classesNames, flightAndCities);
						flightDisplay.Dock = DockStyle.Top;
						flightDisplay.Click += new EventHandler(openPassangersView);
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

		private void pictureBox1_Click(object sender, EventArgs e) {
			reconnect();
		}

		private Dictionary<int, PassangersView> openedViews = new Dictionary<int, PassangersView>();

		private void openPassangersView(object sender, EventArgs e) {
			var flightDisplay = (FlightDisplay) sender;
			var fic = flightDisplay.CurrentFlight;

			var flightId = fic.flight.id;

			PassangersView view;
			if(openedViews.TryGetValue(flightId, out view)) {
				view.Focus();
			}
			else {
				try {
					var result = service.flightDetails(fic.flight.id);

					if(result) { 
						view = new PassangersView(service, fic, result.s, classesNames);
						view.FormClosed += (obj, args) => { openedViews.Remove(flightId); };

						updateErrorDisplay(false, null, null);
					
						openedViews.Add(fic.flight.id, view);
						view.Show();
					}
					else {
						updateErrorDisplay(true, result.f.message, null);
					}
				}
				catch(Exception ex) {
					updateErrorDisplay(true, null, ex);
				}
			}
		}

		void reconnect() {
			updateErrorDisplay(false, null, null);
			(service as IDisposable)?.Dispose();
			service = OperatorViewServerQuery.Create();

			setupAvailableOptions();

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
