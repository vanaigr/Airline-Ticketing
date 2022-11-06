using Client;
using ClientCommunication;
using Common;
using Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightDetailsFill : Form {
		private Context context;
		private ClientService service;
		private Customer customer;

		private List<PassangerDisplay> curPassangersDisplays;
		private List<BookingPassanger> bookingPassangers;

		private Dictionary<int, string> classesNames;
		private Flight flight;
		private FlightsSeats.Seats seats;

		private BookingStatus status;

		public event EventHandler OnBookedPassangersChanged;

		public Flight CurrentFlight{ get{ return flight; } }

		public FlightDetailsFill(
			ClientService service, Customer customer,
			Context context,
			BookingStatus status,
			Flight flight, FlightsSeats.Seats seats
		) {
			this.context = context;
			this.status = status;
			this.service = service;
			this.customer = customer;

			if(status.booked) {
				var bookedFlight = status.BookedFlight(this.customer);
				var bookedFlightDetails = status.BookedFlightDetails(this.customer);

				this.flight = bookedFlight.availableFlight;

				this.seats = bookedFlightDetails.seats;
				
				this.bookingPassangers = new List<BookingPassanger>(bookedFlightDetails.bookedSeats.Length);
				this.curPassangersDisplays = new List<PassangerDisplay>();

				for(int i = 0; i < bookedFlightDetails.bookedSeats.Length; i++) {
					var bs = bookedFlightDetails.bookedSeats[i];
					var so = bookedFlightDetails.seatsAndOptions[i];

					var baggageOption = new Dictionary<int, int>(1);
					baggageOption.Add(so.selectedSeatClass, so.selectedOptions.baggageOptions.baggageIndex);
					var handLuggageOption = new Dictionary<int, int>(1);
					handLuggageOption.Add(so.selectedSeatClass, so.selectedOptions.baggageOptions.handLuggageIndex);

					var index = (int) this.customer.findPasangerIndexByDatabaseId(bs.passangerId);

					var it = new BookingPassanger(
						index, so.selectedOptions.servicesOptions.seatSelected,
						bs.selectedSeat, so.selectedSeatClass, baggageOption, handLuggageOption
					);
					
					bookingPassangers.Add(it);				
				}
			}
			else {
				this.seats = seats;
				this.flight = flight;

				this.bookingPassangers = new List<BookingPassanger>();
				this.curPassangersDisplays = new List<PassangerDisplay>();
			}

			this.classesNames = new Dictionary<int, string>();
			foreach(var classId in this.flight.optionsForClasses.Keys) {
				this.classesNames.Add(classId, context.classesNames[classId]);
			}


			InitializeComponent();

			Misc.unfocusOnEscape(this);
			this.seatSelectTable.BackColor2 = Color.LightGray;
			Misc.addBottomDivider(headerContainer);
			Misc.fixFlowLayoutPanelHeight(passangersPanel);

			headerContainer.SuspendLayout();
			flightNameLabel.Text = this.flight.flightName;
			aitrplaneNameLavel.Text = this.flight.airplaneName;
			departureDatetimeLabel.Text = this.flight.departureTime
				.AddMinutes(context.cities[this.flight.fromCode].timeOffsetMinutes).ToString("d MMMM, dddd, HH:mm");
			departureLocationLabel.Text = this.flight.fromCode;
			headerContainer.ResumeLayout(false);
			headerContainer.PerformLayout();
			
			for(int i = 0; i < bookingPassangers.Count; i++) {
				addPassangerDisplay(i);
				var pIndex = bookingPassangers[i].passangerIndex;
				if(pIndex != null) curPassangersDisplays[i].Passanger = this.customer.passangers[(int) pIndex];	
				else curPassangersDisplays[i].Passanger = null;
			}

			seatSelectTable.update(this.seats, addOrUpdatePassanger);
			updateSeatsStatusText();
			if(status.booked) updateStatusBooked();
		}

		private void updateStatusBooked() {
			continueButton.Text = "Просмотреть";
			addAutoseat.Visible = false;
			passangerMenu.Enabled = false;

			seatSelectTable.SuspendLayout();

			foreach(var it in seatSelectTable) {
				it.Enabled = false;
			}

			var bookedFlightDetails = status.BookedFlightDetails(customer);

			for(int i = 0; i < bookingPassangers.Count; i++) {
				var seatTableLoc = seatSelectTable.getSeatLocation(bookedFlightDetails.bookedSeats[i].selectedSeat);
				var seat = (SeatButton) seatSelectTable.GetControlFromPosition(seatTableLoc.X, seatTableLoc.Y);

				seat.Occupied = false;
				seat.Enabled = true;
				seat.Value = i;
			}

			seatSelectTable.ResumeLayout(false);
			seatSelectTable.PerformLayout();
		}

		private void FlightBooking_Load(object sender, EventArgs e) {
			ActiveControl = Misc.addDummyButton(this);
		}

        private void selectCurrentPassanger(int index) {
			var passanger = bookingPassangers[index];

			var seatHandling = new SeatHandlingFromScheme(
				bookingPassangers, seats,
				index
			);

			var newBookingPassanger = passanger.Copy();

            var selectionForm = new PassangerSettings(
				service, customer, status,
				flight.id, 
				seats, seatHandling, 
				newBookingPassanger, index,
				flight.optionsForClasses, classesNames
			);
			var result = selectionForm.ShowDialog();

			if(status.booked) {
				if(result  == DialogResult.Abort) {
					deletePassanger(index);

					var bookedFlight = status.BookedFlight(customer);
					var bookedFlightDetails = status.BookedFlightDetails(customer);

					if(bookedFlightDetails.bookedSeats.Length < 2) {
						bookedFlight.bookedPassangerCount = 0;
						customer.flightsBooked.Remove(status.bookedFlightIndex);
						customer.bookedFlightsDetails.Remove(status.bookedFlightIndex);
					}
					else { 
						bookedFlight.bookedPassangerCount--;
						bookedFlightDetails.bookedSeats = removedAt(bookedFlightDetails.bookedSeats, index);
						bookedFlightDetails.seatsAndOptions = removedAt(bookedFlightDetails.seatsAndOptions, index);
					}

					seats.SetOccupied(passanger.seatIndex, false);
				}
			}
			else { 
				if(result == DialogResult.OK) {
					if(passanger.manualSeatSelected) {
						var seatLoc = seatSelectTable.getSeatLocation(passanger.seatIndex);
						var seat = (SeatButton) seatSelectTable.GetControlFromPosition(seatLoc.X, seatLoc.Y);
						seat.Value = null;
					}
					
					if(newBookingPassanger.manualSeatSelected) {
						var seatLoc = seatSelectTable.getSeatLocation(newBookingPassanger.seatIndex);
						var seat = (SeatButton) seatSelectTable.GetControlFromPosition(seatLoc.X, seatLoc.Y);
						seat.Value = index;
					}

					bookingPassangers[index] = newBookingPassanger;
				}
				else if(result == DialogResult.Abort) {
					deletePassanger(index);
				}

				for(int i = 0; i < bookingPassangers.Count; i++) {
					var bookingPassanger = bookingPassangers[i];
					var display = curPassangersDisplays[i];

					if(bookingPassanger.passangerIndex != null) {
						Passanger p;
						var exists = customer.passangers.TryGetValue((int) bookingPassanger.passangerIndex, out p);
						if(exists) {
							display.Passanger = p;
						}
						else {
							bookingPassanger.passangerIndex = null;
							display.Passanger = null;
						}		
					}
				}
			}

			updateSeatsStatusText();

			OnBookedPassangersChanged?.Invoke(this, new EventArgs());
        }

		private void addOrUpdatePassanger(SeatButton button, FlightsSeats.Point location) {
			int index;
			if(button.Value == null) index = addPassanger();
			else index = (int) button.Value;

			var passanger = bookingPassangers[index];
			passanger.manualSeatSelected = true;
			passanger.seatIndex = seats.Scheme.coordToIndex(location.x, location.z);
			button.Value = index;

			updateSeatsStatusText();
			
			selectCurrentPassanger(index);
		}

		private void updateSeatsStatusText() {
			var autofilledCount = 0;
			foreach(var passanger in bookingPassangers) {
				if(!passanger.manualSeatSelected) autofilledCount++;
			}

			var sb = new StringBuilder();
			if(autofilledCount != bookingPassangers.Count) {
				if(sb.Length != 0) sb.Append(", в");
				else sb.Append("В");
				sb.Append("ыбрано вручную: ").Append(bookingPassangers.Count - autofilledCount);
			}
			if(autofilledCount != 0) {
				if(sb.Length != 0) sb.Append(", в");
				else sb.Append("В");
				sb.Append("ыбрано автоматически: ").Append(autofilledCount);
			}

			selectedStatusLabel.Text = sb.ToString();
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			if(status.booked) throw new InvalidOperationException();

			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = pass.Number;
			deletePassanger(number);

			updateSeatsStatusText();
		}

		private void continueButton_Click(object sender, EventArgs e) {
			if(status.booked) {
				new FlightBook(
					service, 
					customer, bookingPassangers, 
					flight, seats, 
					classesNames, status
				).ShowDialog();
				return;
			}

			for(int i = 0; i < bookingPassangers.Count; i++) {
				var passanger = bookingPassangers[i];

				Validation.ErrorString es = Validation.ErrorString.Create();

				var sb = new StringBuilder().Append("Для пассажира ").Append("" + i).Append(" должны быть заданы: ");

				if(passanger.passangerIndex == null) {
					es.ac("данные пассажира");
				}

				int passangerSeatClassId;
				if(passanger.manualSeatSelected) {
					passangerSeatClassId = seats.Class(passanger.seatIndex);
				}
				else passangerSeatClassId = passanger.seatClassId;

				int baggageIndex;
				bool isBaggageSelected = passanger.baggageOptionIndexForClass.TryGetValue(passangerSeatClassId, out baggageIndex);

				int handLuggageIndex;
				bool isHandLuggageSelected = passanger.handLuggageOptionIndexForClass.TryGetValue(passangerSeatClassId, out handLuggageIndex);

				if(!isBaggageSelected) {
					es.ac("данные багажа");
				}

				if(!isHandLuggageSelected) {
					es.ac("данные ручной клади");
				}

				if(es.Error) {
					var errorString = sb.Append(es.Message).ToString();

					statusLabel.Text = errorString;
					statusTooltip.SetToolTip(statusLabel, errorString);
					return;
				}
			}

			if(bookingPassangers.Count == 0) {
				var errorString = "Должен присутствовать хотя бы один пассажир";

				statusLabel.Text = errorString;
				statusTooltip.SetToolTip(statusLabel, errorString);

				return;
			}

			statusLabel.Text = null;
			statusTooltip.SetToolTip(statusLabel, null);

			new FlightBook(
				service, 
				customer, bookingPassangers, 
				flight, seats, 
				classesNames, status
			).ShowDialog();

			if(status.booked) {
				updateStatusBooked();
			}
		}

		private void addAutoseat_Click(object sender, EventArgs e) {
			if(status.booked) throw new InvalidOperationException();

			var index = addPassanger();
			updateSeatsStatusText();
			selectCurrentPassanger(index);
		}

		private int addPassanger() {
			if(status.booked) throw new InvalidOperationException();

			var enumerator = classesNames.Keys.GetEnumerator();
			enumerator.MoveNext();
			var index = bookingPassangers.Count;
			bookingPassangers.Add(new BookingPassanger(
				enumerator.Current
			));
			enumerator.Dispose();

			addPassangerDisplay(index);

			return index;
		}

		private void addPassangerDisplay(int index) {
			var display = new PassangerDisplay() { Number = index };
			curPassangersDisplays.Add(display);
			
			display.ContextMenuStrip = passangerMenu;
			display.Click += (a, b) => selectCurrentPassanger(((PassangerDisplay) a).Number);
			display.ShowNumber = true;
			display.ToolTip = passangerTooltip;

			var passangersDisplayList = passangersPanel;

			passangersDisplayList.SuspendLayout();
			passangersDisplayList.Controls.Add(display);
			passangersDisplayList.ResumeLayout(false);
			passangersDisplayList.PerformLayout();
		}

		private void deletePassanger(int index) {
			passangersPanel.SuspendLayout();
			seatSelectTable.SuspendLayout(); //is this really needed here?

			for(int i = index+1; i < bookingPassangers.Count; i++) {
				var display = curPassangersDisplays[i];
				display.Number = i-1;
			}
			foreach(var seat in seatSelectTable) {
				if(seat.Value > index) seat.Value--;
				else if(seat.Value == index) seat.Value = null;
			}
			curPassangersDisplays[index].Dispose();
			curPassangersDisplays.RemoveAt(index);
			bookingPassangers.RemoveAt(index);

			passangersPanel.ResumeLayout(false);
			seatSelectTable.ResumeLayout(false);
			passangersPanel.PerformLayout();
			seatSelectTable.PerformLayout();
		}

		static T[] removedAt<T>(T[] arr, int index) {
			var newArr = new T[arr.Length-1];
	
			if(index >= 1) Array.Copy(arr, newArr, index);
			if(index <= arr.Length-2) Array.Copy(arr, index+1, newArr, index, arr.Length-1 - index);
	
			return newArr;
		}

		class SeatHandlingFromScheme : PassangerSettings.SeatHandling {
			private List<BookingPassanger> passangers;
			private FlightsSeats.Seats seats;
			private int baggagePassangerIndex;

			public SeatHandlingFromScheme(
				List<BookingPassanger> passangers,
				FlightsSeats.Seats seats,
				int baggagePassangerIndex
			) {
				this.passangers = passangers;
				this.seats = seats;
				this.baggagePassangerIndex = baggagePassangerIndex;
			}

			bool PassangerSettings.SeatHandling.canPlaceAt(int index) {
				if(seats.Occupied(index)) return false;

				for(int i = 0; i < passangers.Count; i++) {
					if(i != baggagePassangerIndex && passangers[i].manualSeatSelected && passangers[i].seatIndex == index) {
						return false;
					}
				}
				return true;
			}
		}
	}
	class SeatButton : Label {
		private int? value;

		public int? Value {
			get{ return value; }
			set{
				if(value == null) Text = "";
				else Text = "" + value;
				this.value = value;
				setColors();
			}
		}

		private bool occupied;

		public bool Occupied{
			get{ return occupied; }
			set{
				occupied = value;
				setColors();
			}
		}

		public SeatButton() : base() {
			Misc.setBetterFont(this);

			TextAlign = ContentAlignment.MiddleCenter;
			Dock = DockStyle.Fill;
			Margin = new Padding(3);
			Value = null;
			AutoSize = true;
			setColors();

			EnabledChanged += (a, b) => setColors();
		}

		private void setColors() {
			 if(occupied) {
				BackColor = Color.FromArgb(unchecked((int) 0xff727883u));
				ForeColor = SystemColors.ControlText;
			}
			else if(Value != null) {
				BackColor = FlightDisplay.freeColor; 
				ForeColor = SystemColors.ControlLightLight;
			}
			else {
				BackColor = Color.CornflowerBlue;
				ForeColor = SystemColors.ControlLightLight;
			}
		}
	}

	class SeatsTable : TableLayoutPanel {
		public Color BackColor2;

		private FlightsSeats.SeatsScheme seatsScheme;
		private Dictionary<int, Point> seatsIndexToTableLocation;

		public SeatsTable() : base() { 
			DoubleBuffered = true; 
			seatsIndexToTableLocation = new Dictionary<int, Point>();
		}

		public Point getSeatLocation(FlightsSeats.Point seatPos) {
			return seatsIndexToTableLocation[seatsScheme.coordToIndex(seatPos)];
		}

		public Point getSeatLocation(int seatIndex) {
			return seatsIndexToTableLocation[seatIndex];
		}

		public IEnumerator<SeatButton> GetEnumerator() {
			return new SeatsEnumerator(this);
		}

		private class SeatsEnumerator : IEnumerator<SeatButton> {
			private Dictionary<int, Point>.Enumerator enumerator;
			private SeatsTable table;

			public SeatsEnumerator(SeatsTable table) {
				this.table = table;
				enumerator = this.table.seatsIndexToTableLocation.GetEnumerator();
			}

			public SeatButton Current { get{
				var loc = enumerator.Current.Value;
				return (SeatButton) table.GetControlFromPosition(loc.X, loc.Y);
			} }

			object System.Collections.IEnumerator.Current => Current;

			public void Dispose() {
				enumerator.Dispose();
			}

			public bool MoveNext() {
				return enumerator.MoveNext();
			}

			public void Reset() {
				throw new NotImplementedException();
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e) {
			base.OnPaintBackground(e);
			var g = e.Graphics;

			var size = this.Size;
			var pad = this.Padding;

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			using(var b = new SolidBrush(BackColor2)) {
			var start = new PointF((pad.Left + this.GetColumnWidths()[0]) * 1.5f, pad.Top * 0.7f  + this.GetRowHeights()[0]);
			var end = new PointF(size.Width - pad.Right * 2f, size.Height - pad.Bottom * 0.7f);
			
			//draw main body
			g.FillRectangle(b, point4(start.X, start.Y, end.X, end.Y));

			/*draw back side*/ {
				var cirDiam = end.Y - start.Y;
				var cirRad = cirDiam * 0.5f;
				g.FillEllipse(b, point4(end.X - cirRad, start.Y, end.X + cirRad, end.Y));
			}

			/*draw front side*/ {
				var cirDiamX = (end.Y - start.Y) * 3;
				var cirRadX = cirDiamX * 0.5f;
				g.FillEllipse(b, point4(start.X - cirRadX, start.Y, start.X + cirRadX, end.Y));
			}

			/*draw wings*/ {
				var center = end.X * 0.6f + start.X * 0.4f;
				var rad = (end.X - start.X) * 0.5f * 0.5f;
				using(
				var path = new System.Drawing.Drawing2D.GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding)) {
				path.StartFigure();
				path.AddLine(-rad, 0, 0, -rad * 5);
				path.AddLine(0, -rad * 5, rad * 0.7f, -rad * 5);
				path.AddLine(rad * 0.7f, -rad * 5, 0, 0);
				//path.AddLine(0, 0, -rad, 0);
				path.CloseFigure();

				using(
				var p2 = (System.Drawing.Drawing2D.GraphicsPath) path.Clone()) {
				p2.Transform(new System.Drawing.Drawing2D.Matrix(
					1, 0, 0, 1, center, start.Y+1
				));
				g.FillPath(b, p2);
				}

				path.Transform(new System.Drawing.Drawing2D.Matrix(
					1, 0, 0, -1, center, end.Y-1
				));
				g.FillPath(b, path);
				}
			}
			}
		}

		public delegate void ButtonClicked(SeatButton button, FlightsSeats.Point seatLoc);

		public void update(FlightsSeats.Seats seats, ButtonClicked clicked) {
			seatsScheme = seats.Scheme;

			seatsIndexToTableLocation.Clear();

			this.SuspendLayout();

			this.Controls.Clear();
			this.RowStyles.Clear();
			this.ColumnStyles.Clear();

			var seatsWidthLCM = 1;
			for(int i = 0; i < seatsScheme.SizesCount; i++) {
				var size = seatsScheme.sizeAtIndex(i);
				seatsWidthLCM = Math2.lcm(seatsWidthLCM, size.x);
			}

			this.ColumnCount = seatsScheme.TotalLength + seatsScheme.SizesCount;
			this.RowCount = seatsWidthLCM + 1;

			//columns
			for(int z = 0; z < ColumnCount; z++) {
				this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
			}
			//https://stackoverflow.com/q/36169745/18704284
			//hack around the fact that last column tekes more space than others
			this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0));
			this.ColumnCount++;


			//rows
			this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			for(int x = 1; x < RowCount; x++) {
				this.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
			}

			/*add seats*/ { 
				int z = 0;
				int tableZ = 0;
				for(int i = 0; i < seatsScheme.SizesCount; i++) {
					var size = seatsScheme.sizeAtIndex(i);

					var xSpan = seatsWidthLCM / size.x;

					//add row names
					var widthNaming = FlightsSeats.WidthsNaming.widthsNaming[size.x];
					for(int x = 0; x < size.x; x++) {
						var label = new Label{ 
							Text = widthNaming[x].ToString(), 
							TextAlign = ContentAlignment.MiddleCenter, 
							Dock = DockStyle.Fill, 
							BackColor = Color.Transparent 
						};
						this.Controls.Add(label, tableZ, 1+x*xSpan);
						this.SetRowSpan(label, xSpan);
					}
					tableZ++;

					for(int zO = 0; zO < size.z; zO++, z++) {
						//add column name
						this.Controls.Add(new Label{ 
							Text = "" + (1+z), TextAlign = ContentAlignment.MiddleCenter, 
							Dock = DockStyle.Fill, BackColor = Color.Transparent 
						}, tableZ, 0);

						for(int x = 0; x < size.x; x++) {
							var seatLoc = new FlightsSeats.Point{x=x,z=z};
							var seat = seats[x, z];

							var it = new SeatButton();
							if(seat.Occupied) {
								it.Enabled = false;
								it.Occupied = true;
							}
							else it.Occupied = false;
							it.Click += (a, b) => clicked(it, seatLoc);

							var tablePos = new Point(tableZ, 1 + x*xSpan);
							this.Controls.Add(it, tablePos.X, tablePos.Y);
							this.SetRowSpan(it, xSpan);
							seatsIndexToTableLocation.Add(this.seatsScheme.coordToIndex(seatLoc), tablePos);
						}
						tableZ++;
					}
				}
			}

			this.ResumeLayout(false);
			this.PerformLayout();
		}

		static RectangleF point4(float x1, float y1, float x2, float y2) {
			return new RectangleF(x1, y1, x2 - x1, y2 - y1);
		}
	}

	public class BookingStatus {
		public bool booked;
		public int bookedFlightIndex;
		//public Communication.BookedSeatInfo[] seatsInfo;

		public BookedFlight BookedFlight(Customer it) {
			return it.flightsBooked[bookedFlightIndex];
		}

		public BookedFlightDetails BookedFlightDetails(Customer it) {
			return it.bookedFlightsDetails[bookedFlightIndex];
		}
	}
}