using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightDisplay : UserControl {
		private static string rub = '\u20BD'.ToString();
		private static string check = '\u2713'.ToString();
		private static string cross = '\u274C'.ToString();
		private static string nbs = '\u00A0'.ToString();

		private static Color paidColor = Color.FromArgb(255, 0xff, 0x99, 0x1f);
		private static Color freeColor = Color.FromArgb(255, 0x97, 0xba, 0x1e);
		private static Color unavailableColor = Color.FromArgb(255, 0x95, 0xa0, 0xb3);
		private static Color foreColor = Color.White;

		enum Status { free, paid, unavailable }

		public FlightDisplay() {
			InitializeComponent();

			var panel1 = new Client.TransparentPanel();

			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(852, 124);
			panel1.TabIndex = 0;
			panel1.MouseEnter += new System.EventHandler(this.FlightDisplay_MouseHover);
			panel1.MouseLeave += new System.EventHandler(this.FlightDisplay_MouseLeave);
            panel1.Click += new System.EventHandler(this.FlightDisplay_Click);
            panel1.Cursor = Cursors.Hand;

			this.Controls.Add(panel1);
			panel1.BringToFront();
		}

		static ListItemLabel listItem(Status status) {
			var l = new ListItemLabel();
			if(status == Status.free) {
				l.Text = check;
				l.BackColor2 = freeColor;
			}
			else if(status == Status.paid) {
				l.Text = rub;
				l.BackColor2 = paidColor;
			}
			else if(status == Status.unavailable) {
				l.Text = cross;
				l.BackColor2 = unavailableColor;
			}
			else Debug.Assert(false);
			l.Font = new Font(l.Font.FontFamily, 8);
			l.ForeColor = Color.White;
			l.Dock = DockStyle.Fill;
			l.ForeColor = foreColor;
			return l;
		}

		static void addListItem(TableLayoutPanel table, Status status, string text) {
			table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

			var li = listItem(status);
			li.Margin = new Padding(0, 0, 0, 5);
			li.Dock = DockStyle.Fill;
			table.Controls.Add(li);

			var textLabel = new Label();
			textLabel.TextAlign = ContentAlignment.MiddleLeft;
			textLabel.AutoSize = true;
			textLabel.Margin = new Padding(0, 0, 0, 5);
			textLabel.Dock = DockStyle.Fill;
			textLabel.Text = text;
			table.Controls.Add(textLabel);
		}

		public void updateFromFlight(
			Communication.AvailableFlight flight, 
			string fromCityCodeText, string toCityCodeText,
			string flightName, string className
		) {
			var depart = flight.departureTime;
			var arrive = flight.departureTime.AddMinutes(flight.arrivalOffsteMinutes);

			fromCityCode.Text = fromCityCodeText;
			toCityCode.Text = toCityCodeText;
			
			fromDate.Text = depart.Date.ToString("M MMM, ddd");
			toDate.Text = arrive.Date.ToString("M MMM, ddd");
			
			fromTime.Text = depart.TimeOfDay.ToString(@"h\:mm");
			toTime.Text = arrive.TimeOfDay.ToString(@"h\:mm");
			
			var span = new TimeSpan(0, flight.arrivalOffsteMinutes, 0);
			
			var spanText = new StringBuilder();
			spanText.Append("в пути ");
			if(span.Days != 0) spanText.Append(span.Days).Append("д ");
			if(span.Hours != 0) spanText.Append(span.Hours).Append("ч ");
			if(span.Minutes != 0) spanText.Append(span.Minutes).Append("м ");
			
			flightTime.Text = spanText.ToString();

			this.flightName.Text = flightName;
			this.classType.Text = className;
			this.availableSeatsCount.Text = "Свободных мест: " + flight.availableSeatsCount;

			{ //baggage options
				baggageOptionsTable.Controls.Clear();
				baggageOptionsTable.RowStyles.Clear();
				baggageOptionsTable.ColumnStyles.Clear();

				baggageOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				baggageOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

				var bo = flight.options.baggageOptions;

				baggageOptionsTable.ColumnCount = 2;
				

				var sb = new StringBuilder();

				void addBaggageList(string label, string baggagePaid, List<FlightsOptions.Baggage> baggages) { //appendBaggage
					var freeBaggage = new List<FlightsOptions.Baggage>();
					var paidBaggage = new List<FlightsOptions.Baggage>();
					
					foreach(var baggage in baggages) {
						if(baggage.IsFree) {
							freeBaggage.Add(baggage);
						}
						else {
							paidBaggage.Add(baggage);
						}
					}

					foreach(var baggage in freeBaggage) {
						sb.Clear().Append(label).Append(' ');
						sb.Append(baggage.Count);
						if(baggage.Count == 1) sb.Append(" сумка");
						else sb.Append(" сумок");
						if(baggage.RestrictionWeight) {
							sb.Append(" до ")
							  .Append(baggage.WeightKgRestrictionPerSingle)
							  .Append(nbs)
							  .Append("кг");
						}
						if(baggage.RestrictionWeight && baggage.RestrictionSize) sb.Append(",");
						if(baggage.RestrictionSize) {
							sb.Append(" до ") 
							  .Append(baggage.SizeRestrictionPerSingle.x)
							  .Append('x')
							  .Append(baggage.SizeRestrictionPerSingle.y)
							  .Append('x')
							  .Append(baggage.SizeRestrictionPerSingle.z)
							  .Append(nbs)
							  .Append("см");
						}
						if((baggage.RestrictionWeight || baggage.RestrictionSize) && baggage.Count != 1) sb.Append(" за шт.");

						addListItem(baggageOptionsTable, Status.free, sb.ToString());
					}

					if(freeBaggage.Count == 0) {
						addListItem(baggageOptionsTable, Status.paid, baggagePaid);;
					}
				}

				addBaggageList("Багаж", "Багаж платный", bo.baggage);
				addBaggageList("Ручная кладь", "Ручная кладь платная", bo.handLuggage);

				baggageOptionsTable.Controls.Add(new Label{ Height = 0, Width = 0 });
			}

			{ //terms options
				termsOptionsTable.Controls.Clear();
				termsOptionsTable.RowStyles.Clear();
				termsOptionsTable.ColumnStyles.Clear();

				termsOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				termsOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				termsOptionsTable.ColumnCount = 2;

				var options = flight.options.termsOptions;
				
				if(options.CanChangeFlights) {
					if(options.ChangeFlightCostRub == 0) addListItem(termsOptionsTable, Status.free, "Обмен без сборов");
					else addListItem(termsOptionsTable, Status.paid, "Обмен со сбором " + options.ChangeFlightCostRub + nbs + rub);
				}
				else addListItem(termsOptionsTable, Status.unavailable, "Обмен недоступен");

				if(options.Refundable) {
					if(options.RefundCostRub == 0) addListItem(termsOptionsTable, Status.free, "Полный возврат");
					else addListItem(termsOptionsTable, Status.paid, "Возврат со сбором " + options.RefundCostRub + nbs + rub);
				}
				else addListItem(termsOptionsTable, Status.unavailable, "Возврат недоступен");
			}

			{ //terms options
				termsOptionsTable.Controls.Clear();
				termsOptionsTable.RowStyles.Clear();
				termsOptionsTable.ColumnStyles.Clear();

				termsOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				termsOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				termsOptionsTable.ColumnCount = 2;

				var options = flight.options.termsOptions;
				
				if(options.CanChangeFlights) {
					if(options.ChangeFlightCostRub == 0) addListItem(termsOptionsTable, Status.free, "Обмен без сборов");
					else addListItem(termsOptionsTable, Status.paid, "Обмен со сбором " + options.ChangeFlightCostRub + nbs + rub);
				}
				else addListItem(termsOptionsTable, Status.unavailable, "Обмен недоступен");

				if(options.Refundable) {
					if(options.RefundCostRub == 0) addListItem(termsOptionsTable, Status.free, "Полный возврат");
					else addListItem(termsOptionsTable, Status.paid, "Возврат со сбором " + options.RefundCostRub + nbs + rub);
				}
				else addListItem(termsOptionsTable, Status.unavailable, "Возврат недоступен");

				termsOptionsTable.Controls.Add(new Label{ Height = 0, Width = 0 });
			}

			{ //services options
				servicesOptionsTable.Controls.Clear();
				servicesOptionsTable.RowStyles.Clear();
				servicesOptionsTable.ColumnStyles.Clear();

				servicesOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				servicesOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				servicesOptionsTable.ColumnCount = 2;

				var options = flight.options.servicesOptions;
				
				if(options.seatChoiceCostRub == 0) {
					addListItem(servicesOptionsTable, Status.free, "Выбор мекста при регистрации");
				}
				else {
					addListItem(servicesOptionsTable, Status.paid, "Выбор мекста платный");
				}

				servicesOptionsTable.Controls.Add(new Label{ Height = 0, Width = 0 });
			}
		}

		private void FlightDisplay_MouseHover(object sender, EventArgs e) {
			this.BackColor = Color.FromArgb(255, 240, 240, 240);
		}

		private void FlightDisplay_MouseLeave(object sender, EventArgs e) {
			this.BackColor = Color.White;
		}

        private void FlightDisplay_Click(object sender, EventArgs e) {
            this.OnClick(e);
        }
	}
}
