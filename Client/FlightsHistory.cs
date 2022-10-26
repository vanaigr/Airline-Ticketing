using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightsHistory : Form {
		private Communication.MessageService service;
		private CustomerData customer;

		public FlightsHistory(
			Communication.MessageService service,
			string[] classesNames,
			CustomerData customer
		) {
			this.service = service;
			this.customer = customer;

			InitializeComponent();

			Misc.addBottomDivider(tableLayoutPanel2);

			try{
				if(customer.LoggedIn && customer.bookedFlights == null) {
					var result = service.getBookedFlights(customer.customer.Value);
					if(result) {
						customer.bookedFlights = result.s.ToList();
						customer.localBookedFlights.Clear();
						customer.localBookedFlightsDetails.Clear();
					}
					else {
						setStatus(result.f.message, null);
					}
				}
			}
			catch(Exception e) {
				setStatus(null, e);
			}

			flightsTable.SuspendLayout();
			flightsTable.RowStyles.Clear();
			flightsTable.RowCount = 0;

			if((customer.bookedFlights?.Count ?? 0) + customer.localBookedFlights.Count == 0) {
				var label = new Label();
				label.AutoSize = true;
				label.Dock = DockStyle.Fill;
				label.TextAlign = ContentAlignment.TopCenter;
				label.Text = "Нет оформленных билетов";
				label.Margin = new Padding(0, 10, 0, 10);
				label.Font = new Font("Segoe UI", 18.0F, FontStyle.Regular, GraphicsUnit.Point, 204);

				flightsTable.Controls.Add(label);
			}
			else {
				flightsTable.RowCount = (customer.bookedFlights?.Count ?? 0) + customer.localBookedFlights.Count;

				for(int i = 0; i < customer.localBookedFlights.Count; i++) {

					var it = new BookedFlightInfoControl(
						service, setStatus,
						customer, classesNames, 
						customer.localBookedFlights[i], 
						customer.localBookedFlightsDetails[i]
					);
					it.Dock = DockStyle.Top;
					it.Margin = new Padding(0, 5, 0, 5);
					it.OnDelete += (a, b) => {
						it.Dispose();
					};

					flightsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					flightsTable.Controls.Add(it, 0, flightsTable.RowCount++);
				}

				if(customer.bookedFlights != null) {
					for(int i = 0; i < customer.bookedFlights.Count; i++) {

						var it = new BookedFlightInfoControl(
							service, setStatus,
							customer, classesNames, 
							customer.bookedFlights[i], null
						);
						it.Dock = DockStyle.Top;
						it.Margin = new Padding(0, 5, 0, 5);
						it.OnDelete += (a, b) => {
							it.Dispose();
						};

						flightsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
						flightsTable.Controls.Add(it, 0, flightsTable.RowCount++);
					}
				}
			}

			flightsTable.ResumeLayout(false);
			flightsTable.PerformLayout();
		}

		private void setStatus(string msg, Exception e) {
			statusLabel.Text = msg ?? "Неизвестная ошибка";
			statusTooltip.SetToolTip(statusLabel, e?.ToString() ??  msg ?? "");
		}
	}
}
