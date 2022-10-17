using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class FlightDetailsFill : Form {
		private Communication.MessageService service;
		private CustomerData customer;

		struct PassangerData {
			public int? passangerIndex;
			public int? seatIndex;
		}

		private List<PassangerData> passangers;
		private List<PassangerDisplay> displays;

		private Dictionary<int, string> classesNames;
		private FlightAndCities flightAndCities;

		public FlightAndCities CurrentFlight{ get{ return this.flightAndCities; } }

		public FlightDetailsFill(Communication.MessageService service, CustomerData customer) {
			this.service = service;
			this.customer = customer;

			InitializeComponent();

			Misc.unfocusOnEscape(this);
			passangersPanel.AutoScrollMargin = new System.Drawing.Size(SystemInformation.HorizontalScrollBarHeight, SystemInformation.VerticalScrollBarWidth);

			this.seatSelectTable.BackColor2 = Color.LightGray;//Color.FromArgb(unchecked((int) 0xffbcc5d6));

			//tableLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			//tableLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

			Misc.fixFlowLayoutPanelHeight(passangersPanel);

			passangers = new List<PassangerData>();
			displays = new List<PassangerDisplay>();

			button1_Click(null, null);
		}

		public bool setFromFlight(
			Dictionary<int, string> classesNames,
			FlightAndCities flightAndCities,
			int selectedClassId
		) {
			this.classesNames = new Dictionary<int, string>();
			foreach(var classId in flightAndCities.flight.optionsForClasses.Keys) {
				this.classesNames.Add(classId, classesNames[classId]);
			}

			//this.classesNames = classesNames;
			this.flightAndCities = flightAndCities;

			var flight = flightAndCities.flight;

			headerContainer.SuspendLayout();
			flightNameLabel.Text = flight.flightName;
			aitrplaneNameLavel.Text = flight.airplaneName;
			departureDatetimeLabel.Text = flight.departureTime.ToString("d MMMM, dddd, h:mm");
			departureLocationLabel.Text = flightAndCities.fromCityCode;
			headerContainer.ResumeLayout(false);
			headerContainer.PerformLayout();
			
			classSelector.SuspendLayout();
			classSelector.DataSource = new BindingSource{ DataSource = this.classesNames };
			classSelector.DisplayMember = "Value";
			setSelectedClass(selectedClassId);
			classSelector.ResumeLayout(false);
			classSelector.PerformLayout();

			Misc.addDummyButton(classSelector.Parent);

			recalculateSeats();

			return true;
		}

		public void setSelectedClass(int selectedClassId) {
			var classE = classesNames.GetEnumerator();
			var i = 0;
			for(; classE.MoveNext() && classE.Current.Key != selectedClassId; i++);
			classSelector.SelectedIndex = i;

		}

		private void classSelector_SelectedIndexChanged(object sender, EventArgs e) {
			seatSelectTable.update(flightAndCities.flight.seats, ((KeyValuePair<int, string>) classSelector.SelectedValue).Key, recalculateSeats);
			recalculateSeats();
		}

		private void FlightBooking_Load(object sender, EventArgs e) {
			ActiveControl = Misc.addDummyButton(this);
		}

		private void button1_Click(object sender, EventArgs e) {
			passangers.Add(new PassangerData{ passangerIndex = null, seatIndex = null });
			var display = new PassangerDisplay() { Number = passangers.Count, Anchor = AnchorStyles.Top | AnchorStyles.Bottom };
			displays.Add(display);
			display.ContextMenuStrip = passangerMenu;
			display.Click += (a, b) => selectPassanger((PassangerDisplay) a);
			display.ShowNumber = true;
			display.ToolTip = passangerTooltip;

			var passangersDisplayList = passangersPanel;

			passangersDisplayList.SuspendLayout();
			passangersDisplayList.Controls.Add(display);
			passangersDisplayList.ResumeLayout(false);
			passangersDisplayList.PerformLayout();

			recalculateSeats();
		}

        private void selectPassanger(PassangerDisplay it) {
			var oldPassanger = passangers[it.Number-1];
            var selectionForm = new PassangerSettings(
				service, customer, oldPassanger.passangerIndex, 
				flightAndCities.flight.optionsForClasses, classesNames
			);
			var result = selectionForm.ShowDialog();
			//if(result == DialogResult.OK) {
			//	var newPassanger = passangers[it.Number-1];
			//	newPassanger.passangerIndex = selectionForm.SelectedPassangerIndex;
			//	passangers[it.Number-1] = newPassanger;
			//	it.Passanger = selectionForm.SelectedPassanger;
			//}

			for(int i = 0; i < passangers.Count; i++) {
				var passangerData = passangers[i];
				if(passangerData.passangerIndex != null) {
					Communication.Passanger passanger;
					var exists = customer.passangers.TryGetValue((int) passangerData.passangerIndex, out passanger);
					if(exists) {
						displays[i].Passanger = passanger;
					}
					else {
						passangerData.passangerIndex = null;
						passangers[i] = passangerData;
						displays[i].Passanger = null;
					}		
				}
			}
        }

		private void recalculateSeats() {
			if(flightAndCities == null) return;
			seatSelectTable.SuspendLayout();

			seatHint.RemoveAll();

			var seatsScheme = flightAndCities.flight.seats.Scheme;
			
			var seatsCorrect = true;
			int autofilledCount = 0;

			var passangesrsSeatLocation = new int[passangers.Count];
			var passangerSeatsDisplays = new List<SeatNumericUpDown>[passangers.Count];
			for(int i = 0; i < passangers.Count; i++) passangerSeatsDisplays[i] = new List<SeatNumericUpDown>();

			for(int z = 0; z < seatsScheme.TotalLength; z++) {
				var width = seatsScheme.WidthForRow(z);
				for(int x = 0; x < width ; x++) {
					var pos = seatSelectTable.getSeatLocation(new SeatsScheme.Point(z, x));
					var c = (SeatNumericUpDown) seatSelectTable.GetControlFromPosition(pos.X, pos.Y);
					var v = (int) c.Value;
					if(v >= 0 && v-1 < passangers.Count) {
						if(v != 0) {
							passangerSeatsDisplays[v-1].Add(c);
							passangesrsSeatLocation[v-1] = seatsScheme.coordToIndex(x, z);
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
			}

			for(int p = 0; p < passangers.Count; p++) {
				var displays = passangerSeatsDisplays[p];
				if(displays.Count == 1) {
					var passanger = passangers[p];
					passanger.seatIndex = passangesrsSeatLocation[p];
					passangers[p] = passanger;
				}
				else if(displays.Count > 1) {
					seatsCorrect = false;
					foreach(var c in displays) {
						Debug.Assert(c.Value == passangers[p].passangerIndex);
						c.markError();
						seatHint.SetToolTip(c, "Для пассажира " + ((int) c.Value) + " задано " + displays.Count + " мест");
					}
				}
				else autofilledCount++;
			}

			var sb = new StringBuilder();
			if(autofilledCount != passangers.Count) {
				if(sb.Length != 0) sb.Append(", в");
				else sb.Append("В");
				sb.Append("ыбрано вручную: ").Append(passangers.Count - autofilledCount);
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

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e) {
			passangersPanel.SuspendLayout();
			seatSelectTable.SuspendLayout(); //is this really needed here?

			if(passangers.Count == 1) return;

			var pass = (PassangerDisplay) passangerMenu.SourceControl;
			var number = pass.Number;
			foreach(var passDis in displays) {
				if(passDis.Number > number) passDis.Number--;
			}
			foreach(var seat in seatSelectTable) {
				if(seat.Value > number) seat.Value--;
				else if(seat.Value == number) seat.Value = 0;
			}
			passangers.RemoveAt(pass.Number-1);
			displays.RemoveAt(pass.Number-1);
			pass.Dispose();

			passangersPanel.ResumeLayout(false);
			seatSelectTable.ResumeLayout(false);
			passangersPanel.PerformLayout();
			seatSelectTable.PerformLayout();
		}

		private void continueButton_Click(object sender, EventArgs e) {
			foreach(var passanger in passangers) if(passanger.passangerIndex == null) {
				MessageBox.Show(
					"Данные для всех пассажиров должны бвть заданы", "", MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}
		}
	}
	class SeatNumericUpDown : NumericUpDown {
		public SeatNumericUpDown() : base() {
			Controls.RemoveAt(0);
			Margin = new Padding(1, 3, 1, 0);

			var tb = (TextBox) Controls[0];


			tb.MouseWheel += (a, e) => {
				Console.WriteLine("CCCCCCCCCCC");
				((HandledMouseEventArgs) e).Handled = true;
			};

			tb.Multiline = true;
			tb.BorderStyle = BorderStyle.None;

			this.Padding = new Padding(0);
			BorderStyle = BorderStyle.None;
			TextAlign = HorizontalAlignment.Center;
			Anchor = AnchorStyles.Left | AnchorStyles.Right;

			Value = 0;
		}

		protected override void OnTextBoxResize(object source, EventArgs e) {
			Controls[0].Width = Width;
		}
		
		protected override void UpdateEditText() {
		    if (this.Value != 0) base.UpdateEditText();
		    else base.Text = "";
		}

		private void setColors() {
			 if(!Enabled) {
				BackColor = Color.FromArgb(unchecked((int) 0xff98a3b8u));//Color.LightSteelBlue;
				ForeColor = SystemColors.ControlText;
			}
			else if(!error) {
				if(Value != 0) {
					BackColor = FlightDisplay.freeColor; 
					ForeColor = SystemColors.ControlLightLight;
				}
				else {
					BackColor = Color.CornflowerBlue;
					ForeColor = SystemColors.ControlLightLight;
				}
			}
			else {
				BackColor = Color.MistyRose;
				ForeColor = SystemColors.ControlText;
			}
		}

		protected override void OnValueChanged(EventArgs e) {
			base.OnValueChanged(e);
			setColors();
		}

		private bool error;

		public void markFine() {
			error = false;
			setColors();
		}

		public void markError() {
			error = true;
			setColors();
		}

		protected override void OnMouseWheel(MouseEventArgs e) {
			//base.OnMouseWheel(e);
		}
	}

	class SeatsTable : TableLayoutPanel {
		public Color BackColor2;

		private Dictionary<SeatsScheme.Point, Point> seatsLocationToTableLocation;

		private static Dictionary<int, char[]> widthsNaming = new Dictionary<int, char[]>();

		static SeatsTable() {
			widthsNaming.Add(4, new char[]{ 'A', 'C', 'D', 'F' });
			widthsNaming.Add(6, new char[]{ 'A', 'B', 'C', 'D', 'E', 'F' });
		}

		public SeatsTable() : base() { 
			DoubleBuffered = true; 
			seatsLocationToTableLocation = new Dictionary<SeatsScheme.Point, Point>();
		}

		public Point getSeatLocation(SeatsScheme.Point seatPos) {
			return seatsLocationToTableLocation[seatPos];
		}

		public IEnumerator<SeatNumericUpDown> GetEnumerator() {
			return new SeatsEnumerator(this);
		}

		private class SeatsEnumerator : IEnumerator<SeatNumericUpDown> {
			private Dictionary<SeatsScheme.Point, Point>.Enumerator enumerator;
			private SeatsTable table;

			public SeatsEnumerator(SeatsTable table) {
				this.table = table;
				enumerator = this.table.seatsLocationToTableLocation.GetEnumerator();
			}

			public SeatNumericUpDown Current { get{
				var loc = enumerator.Current.Value;
				return (SeatNumericUpDown) table.GetControlFromPosition(loc.X, loc.Y);
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
			var end = new PointF(size.Width - pad.Right * 1.5f, size.Height - pad.Bottom * 0.7f);
			
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

		public void update(SeatsScheme.Seats seats, int classId, Action update) {
			seatsLocationToTableLocation.Clear();

			this.SuspendLayout();

			this.Controls.Clear();
			this.RowStyles.Clear();
			this.ColumnStyles.Clear();

			var seatsScheme = seats.Scheme;

			var seatsWidthLCM = 1;
			for(int i = 0; i < seatsScheme.SizesCount; i++) {
				var size = seatsScheme.sizeAtIndex(i);
				seatsWidthLCM = Math2.lcm(seatsWidthLCM, size.x);
			}

			this.ColumnCount = seatsScheme.TotalLength + seatsScheme.SizesCount;
			this.RowCount = seatsWidthLCM + 1;

			//columns
			for(int z = 0; z < ColumnCount; z++) {
				this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			}
			//https://stackoverflow.com/q/36169745/18704284
			//hack around the fact that last column tekes more space than others
			this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0));
			this.ColumnCount++;

			//rows
			for(int x = 0; x < RowCount; x++) {
				this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			}

			/*add seats*/ { 
				int z = 0;
				int tableZ = 0;
				for(int i = 0; i < seatsScheme.SizesCount; i++) {
					var size = seatsScheme.sizeAtIndex(i);

					var xSpan = seatsWidthLCM / size.x;

					//add row names
					var widthNaming = widthsNaming[size.x];
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
							var it = new SeatNumericUpDown();
							var seat = seats[x, z];
							if(seat.Occupied) { //|| seat.Class != classId
								it.Enabled = false;
							}
							it.ValueChanged += (a, b) => update();
							var tablePos = new Point(tableZ, 1 + x*xSpan);
							this.Controls.Add(it, tablePos.X, tablePos.Y);
							this.SetRowSpan(it, xSpan);
							seatsLocationToTableLocation.Add(new SeatsScheme.Point{x=x,z=z}, tablePos);
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
}