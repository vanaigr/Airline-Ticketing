using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightBooking : Form {
		private Communication.MessageService service;

		private Dictionary<int, string> classesNames;
		private FlightAndCities flightAndCities;

		struct PassangerSeat {
			public Point loc;
		}

		private PassangerSeat[] passangesrsSeat = new PassangerSeat[0];

		private int passangersCount;

		public FlightAndCities CurrentFlight{ get{ return this.flightAndCities; } }

		public FlightBooking(Communication.MessageService service) {
			InitializeComponent();
			Misc.unfocusOnEscape(this);
			this.service = service;
			passangersDisplayList.AutoScrollMargin = new System.Drawing.Size(SystemInformation.HorizontalScrollBarHeight, SystemInformation.VerticalScrollBarWidth);
		}

		public void setFromFlight(
			Dictionary<int, string> classesNames,
			FlightAndCities flightAndCities,
			int selectedClassIndex
		) {
			this.passangersCount = 0;

			this.classesNames = classesNames;
			this.flightAndCities = flightAndCities;

			var flight = flightAndCities.flight;

			headerContainer.SuspendLayout();
			flightNameLabel.Text = flight.flightName;
			aitrplaneNameLavel.Text = flight.airplaneName;
			departureDatetimeLabel.Text = flight.departureTime.ToString("d MMMM, dddd, h:mm");
			departureLocationLabel.Text = flightAndCities.fromCityCode;
			headerContainer.ResumeLayout(false);
			headerContainer.PerformLayout();

			//Misc.addDummyButton(classSelector.Parent);
			
			classSelector.DataSource = new BindingSource{ DataSource = classesNames };
			classSelector.DisplayMember = "Value";
			classSelector.SelectedIndex = selectedClassIndex;
			classSelector.PerformLayout();

			Misc.addDummyButton(classSelector.Parent);

			recalculateSeats();
		}

		private void update() {
			seatSelectTable.SuspendLayout();

			seatSelectTable.Controls.Clear();
			seatSelectTable.RowStyles.Clear();
			seatSelectTable.ColumnStyles.Clear();

			var seats = flightAndCities.flight.seats;

			seatSelectTable.ColumnCount = seats.Length + 1;
			seatSelectTable.RowCount = seats.Width + 1;

			var xPercent = 1.0f / (seats.Width + 1);
			var zPercent = 1.0f / (seats.Length + 1);

			//columns
			seatSelectTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, zPercent));
			for(int z = 0; z < seats.Length; z++) {
				seatSelectTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, zPercent));
				seatSelectTable.Controls.Add(new Label{ Text = "" + (1+z), TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 1+z, 0);
			}
			//rows
			seatSelectTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, xPercent));
			for(int x = 0; x < seats.Width; x++) {
				seatSelectTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, xPercent));
				seatSelectTable.Controls.Add(new Label{ Text = (char) ('A' + x) + "", TextAlign = System.Drawing.ContentAlignment.MiddleCenter, Dock = DockStyle.Fill }, 0, 1+x);
			}

			for(int z = 0; z < seats.Length; z++)
			for(int x = 0; x < seats.Width ; x++) {
				var seat = seats[x, z];

				var it = new SeatNumericUpDown();
				if(seat.classIndex != ((KeyValuePair<int, string>) classSelector.SelectedValue).Key) {
					it.Enabled = false;
				}
				it.ValueChanged += (a, b) => recalculateSeats();
				seatSelectTable.Controls.Add(it, 1+z, 1+x);
			}

			seatSelectTable.ResumeLayout(false);
			seatSelectTable.PerformLayout();
		}

		private void classSelector_SelectedIndexChanged(object sender, EventArgs e) {
			update();
		}

		private void FlightBooking_Load(object sender, EventArgs e) {
			ActiveControl = Misc.addDummyButton(this);
		}

		private void button1_Click(object sender, EventArgs e) {
			passangersDisplayList.SuspendLayout();
			passangersDisplayList.ColumnStyles.Insert(1, new ColumnStyle(SizeType.AutoSize));
			passangersDisplayList.Controls.Add(new PassangerDisplay(passangersCount+1) { Dock = DockStyle.Fill });
			passangersDisplayList.ColumnCount++;
			passangersDisplayList.ResumeLayout(false);
			passangersDisplayList.PerformLayout();
			passangersCount ++;
			recalculateSeats();
		}

		private void recalculateSeats() {
			seatSelectTable.SuspendLayout();

			var seats = flightAndCities.flight.seats;

			seatHint.RemoveAll();

			this.passangesrsSeat = new PassangerSeat[passangersCount];
			var seatsCorrect = true;
			int autofilledCount = 0;

			var passangerSeats = new List<List<SeatNumericUpDown>>();
			for(int i = 0; i < passangersCount; i++) passangerSeats.Add(new List<SeatNumericUpDown>(2));

			for(int z = 0; z < seats.Length; z++)
			for(int x = 0; x < seats.Width ; x++) {
				var c = (SeatNumericUpDown) seatSelectTable.GetControlFromPosition(z+1, x+1);
				var v = (int) c.Value;
				if(v >= 0 && v <= passangerSeats.Count) {
					if(v != 0) {
						passangerSeats[v-1].Add(c);
						this.passangesrsSeat[v-1] = new PassangerSeat{ loc = new Point(x, z) };
					}

					c.markFine();
					seatHint.SetToolTip(c, null);
				}
				else {
					c.markError();
					seatHint.SetToolTip(c, "Пассажира " + v + " не существует");
					seatsCorrect = false;
				}
			}

			for(int p = 0; p < passangerSeats.Count; p++) {
				var pSeats = passangerSeats[p];
				if(pSeats.Count > 1) {
					seatsCorrect = false;
					foreach(var c in pSeats) {
						c.markError();
						seatHint.SetToolTip(c, "Для пассажира " + ((int) c.Value) + " задано " + pSeats.Count + " мест");
					}
				}

				if(pSeats.Count == 0) autofilledCount++;
			}

			var sb = new StringBuilder();
			if(autofilledCount != passangersCount) {
				if(sb.Length != 0) sb.Append(", в");
				else sb.Append("В");
				sb.Append("ыбрано вручную: ").Append(passangersCount - autofilledCount);
			}
			if(autofilledCount != 0) {
				if(sb.Length != 0) sb.Append(", в");
				else sb.Append("В");
				sb.Append("ыбрано автоматически: ").Append(autofilledCount);
			}
			if(!seatsCorrect) {
				if(sb.Length != 0) sb.Append(", о");
				else sb.Append("О");
				sb.Append("бнаружена ошибка в заполнении");
			}

			selectedStatusLabel.Text = sb.ToString();

			seatSelectTable.ResumeLayout(false);
			seatSelectTable.PerformLayout();
		}
	}

	class SeatNumericUpDown : NumericUpDown {
		public SeatNumericUpDown() : base() {
			Controls.RemoveAt(0);
			Value = 0;
			Margin = new Padding(0);

			var tb = ((TextBox) Controls[0]);

			tb.Multiline = true;
			tb.BorderStyle = BorderStyle.Fixed3D;

			this.Padding = new Padding(0);
			BorderStyle = BorderStyle.None;
			TextAlign = HorizontalAlignment.Center;
			Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			Dock = DockStyle.Fill;
		}

		protected override void OnTextBoxResize(object source, EventArgs e) {
			Controls[0].Width = Width;
		}
		
		protected override void UpdateEditText() {
		    if (this.Value != 0) base.UpdateEditText();
		    else base.Text = "";
		}

		protected override void OnParentEnabledChanged(EventArgs e) {
			base.OnParentEnabledChanged(e);
			Enabled = Parent.Enabled;
		}

		public void markFine() {
			if(Enabled) {
				BackColor = Color.White;
			}
		}

		public void markError() {
			if(Enabled) {
				BackColor = Color.MistyRose;
			}
		}
	}
}