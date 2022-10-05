using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightDisplay : UserControl {
		private static string rub = '\u20BD'.ToString();
		private static string check = '\u2713'.ToString();

		private static Color paidColor = Color.FromArgb(255, 0xff, 0x99, 0x1f);
		private static Color freeColor = Color.FromArgb(255, 0x97, 0xba, 0x1e);
		private static Color foreColor = Color.White;

		public FlightDisplay() {
			InitializeComponent();
		}

		static ListItemLabel listItem(bool free) {
			var l = new ListItemLabel();
			if(free) {
				l.Text = check;
				l.BackColor = freeColor;
				l.Font = new Font(l.Font.FontFamily, 8);
			}
			else {
				l.Text = rub;
				l.Font = new Font(l.Font.FontFamily, 8);
				l.BackColor = paidColor;
			}
			l.Dock = DockStyle.Fill;
			l.ForeColor = foreColor;
			return l;
		}

		static void addListItem(TableLayoutPanel table, bool free, string text) {
			table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

			var li = listItem(free);
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
			string flightName, string className, int availableSeatsCount
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
			this.availableSeatsCount.Text = "Свободных мест: " + availableSeatsCount;

			{
				baggageOptionsTable.Controls.Clear();
				baggageOptionsTable.RowStyles.Clear();
				baggageOptionsTable.ColumnStyles.Clear();

				baggageOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
				baggageOptionsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

				var bo = flight.options.baggageOptions;

				baggageOptionsTable.ColumnCount = 2;
				

				var sb = new StringBuilder();

				int addBaggageList(string label, string baggagePaid, List<FlightsOptions.Baggage> baggages) { //appendBaggage
					var count = 0;

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
							sb.Append(" до")
							  .Append(baggage.WeightKgRestrictionPerSingle)
							  .Append(" кг");
						}
						if(baggage.RestrictionWeight && baggage.RestrictionSize) sb.Append(",");
						if(baggage.RestrictionSize) {
							sb.Append(" до ") 
							  .Append(baggage.SizeRestrictionPerSingle.x)
							  .Append('x')
							  .Append(baggage.SizeRestrictionPerSingle.y)
							  .Append('x')
							  .Append(baggage.SizeRestrictionPerSingle.z)
							  .Append(" см");
						}
						if((baggage.RestrictionWeight || baggage.RestrictionSize) && baggage.Count != 1) sb.Append(" за шт.");

						count++;
						addListItem(baggageOptionsTable, true, sb.ToString());
					}

					if(freeBaggage.Count == 0) {
						count++;
						addListItem(baggageOptionsTable, false, baggagePaid);;
					}

					return count;
				}

				int rowsCount = addBaggageList("Багаж", "Багаж платный", bo.baggage);
				rowsCount += addBaggageList("Ручная кладь", "Ручная кладь платная", bo.handLuggage);

				rowsCount++;
				baggageOptionsTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				baggageOptionsTable.Controls.Add(new Label{ Height = 0, Width = 0 });

				baggageOptionsTable.RowCount = rowsCount;
			}
		}

	}
}
