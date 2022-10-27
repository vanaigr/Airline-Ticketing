using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientCommunication {
	public partial class FlightsHistory : Form {
		private ClientCommunication.MessageService service;
		private CustomerData customer;

		public FlightsHistory(
			ClientCommunication.MessageService service,
			string[] classesNames,
			CustomerData customer
		) {
			this.service = service;
			this.customer = customer;

			InitializeComponent();

			Misc.addBottomDivider(tableLayoutPanel2);

			try{
				if(customer.LoggedIn) {
					var result = service.getBookedFlights(customer.customer.Value);
					if(result) {
						customer.flightsBooked.Clear();
						customer.bookedFlightsDetails.Clear();
						customer.newBookedFlightIndex = 0;

						foreach(var it in result.s) {
							customer.flightsBooked.Add(customer.newBookedFlightIndex++, it);
						}
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

			if(customer.flightsBooked.Count == 0) {
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
				flightsTable.RowCount = customer.flightsBooked.Count;

				foreach(var pair in customer.flightsBooked) {
					ClientCommunication.BookedFlightDetails details;
					customer.bookedFlightsDetails.TryGetValue(pair.Key, out details);

					var it = new BookedFlightInfoControl(
						service, customer, pair.Key, 
						classesNames, setStatus
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

			flightsTable.ResumeLayout(false);
			flightsTable.PerformLayout();
		}

		private void setStatus(string msg, Exception e) {
			statusLabel.Text = msg ?? "Неизвестная ошибка";
			statusTooltip.SetToolTip(statusLabel, e?.ToString() ??  msg ?? "");
		}
	}
}
