using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperatorView {
	public partial class PassangersView : Form {
		class SeatsOccupation {
			[DisplayName("Класс")]
			public int classId{ get; set; }

			[DisplayName("Забронировано")]
			public int bookedCount{ get; set; }

			[DisplayName("Занято")]
			public int occupiedCount{ get; set; }

			[DisplayName("Отменено")]
			public int canceledCount{ get; set; }

			[DisplayName("Всего")]
			public int count{ get; set; }
		}

		OperatorViewCommunication.MessageService service;

		Communication.FlightAndCities fac;
		List<SeatsOccupation> seatsOccupationForClasses;

		public OperatorViewCommunication.FlightDetails details;

		Context context;

		DateTime lastUpdateTime;
		Timer updateTimer;

		public PassangersView(
			OperatorViewCommunication.MessageService service, 
			Communication.FlightAndCities fac, 
			OperatorViewCommunication.FlightDetails details,
			Context context
		) {
			this.service = service;
			this.fac = fac;
			this.details = details;
			this.context = context;

			InitializeComponent();

			Misc.addBottomDivider(headerTable);
			Misc.addTopDivider(footerTable2);
			Misc.unfocusOnEscape(this);

			splitContainer1_SizeChanged(null, null);

			flightNameLabel.Text = fac.flight.flightName;
			airplaneNameLabel.Text = fac.flight.airplaneName;
			fromDatetime.Text = fac.flight.departureTime
				.AddMinutes(context.cities[fac.fromCityCode].timeOffsetMinutes).ToString("d HH:mm");
			fromCityCodeLabel.Text = fac.fromCityCode;

			var now = DateTime.Now;
			lastUpdateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			updateCountdown();
			checkUpdateCountdown();


			recalculateStats();

			dataGridView1.SuspendLayout();

			dataGridView1.Enabled = false;
			
			dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			dataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
			dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

			dataGridView1.ResumeLayout(false);
			dataGridView1.PerformLayout();

			passangersDataGridView.EditMode = DataGridViewEditMode.EditOnKeystroke;
		}

		private void splitContainer1_SizeChanged(object sender, EventArgs e) {
		    if(splitContainer1.Panel1.Height > headerTable.Height) {
		        splitContainer1.SplitterDistance = headerTable.Height;
		    }
		}

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
		    if (splitContainer1.Panel1.Height > headerTable.Height) {
		        splitContainer1.SplitterDistance = headerTable.Height;
		    }
		}

		private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
			var height = 0;
			foreach (DataGridViewRow dr in dataGridView1.Rows) {
			    height += dr.Height;
			}

			dataGridView1.Height = height;
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			if(e.ColumnIndex == 0) {
				e.Value = context.classesNames[(int) e.Value];
				e.FormattingApplied = true;
			}
		}

		private void PassangersView_Shown(object sender, EventArgs e) {
			dataGridView1.ClearSelection();
			
			//showCanceledCheckbox.Checked = false;
			showCanceledCheckbox.Checked = true;
		}

		struct PassangerBinding {
			public int index;
			public OperatorViewCommunication.FlightDetails details;

			[DisplayName("Место")] public string Seat{ get{
				return details.seats.ToName(details.passangersAndSeats[index].seatIndex);
			} }

			[DisplayName("Фамилия")] public string Surname{ get{
				return details.passangersAndSeats[index].surname;
			} }

			[DisplayName("Имя")] public string Name{ get{
				return details.passangersAndSeats[index].name;
			} }
			
			[DisplayName("Отчество")] public string MiddleName{ get{
				return details.passangersAndSeats[index].middleName;
			} }

			[DisplayName("Дата рождения")] public string Birthday{ get{
				return details.passangersAndSeats[index].birthday.ToString("d");
			} }

			[DisplayName("Документ")] public string Document{ get{
				return details.passangersAndSeats[index].document.ToString();
			} }

			[DisplayName("Отменён")] public bool Canceled{ get{
				return details.passangersAndSeats[index].canceled;
			} }

			[DisplayName("Прибыл")] public bool Arrived{
				get{ return details.passangerArrived[index]; } 
				set{ details.passangerArrived[index] = value; } 
			}
		}

		private void passangersDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			if(((PassangerBinding) passangersDataGridView.Rows[e.RowIndex].DataBoundItem).Canceled) {
				e.CellStyle.BackColor = Color.FromArgb(245, 245, 245);
				e.CellStyle.ForeColor = SystemColors.ControlDarkDark;
			}
			else {
				e.CellStyle.BackColor = Color.White;
				e.CellStyle.ForeColor = SystemColors.ControlText;
			}
		}

		private void showCanceledCheckbox_CheckedChanged(object sender, EventArgs e) {
			var rows = new List<PassangerBinding>(details.passangerArrived.Count);
			for(int i = 0; i < details.passangerArrived.Count; i++) {
				if(!details.passangersAndSeats[i].canceled || showCanceledCheckbox.Checked)
					rows.Add(new PassangerBinding{ index = i, details = details });
			}

			passangersDataGridView.SuspendLayout();

			passangersDataGridView.DataSource = new BindingSource{ DataSource = rows };

			passangersDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			passangersDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			passangersDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			passangersDataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			passangersDataGridView.Columns[0].ReadOnly = true;
			passangersDataGridView.Columns[1].ReadOnly = true;
			passangersDataGridView.Columns[2].ReadOnly = true;
			passangersDataGridView.Columns[3].ReadOnly = true;
			passangersDataGridView.Columns[4].ReadOnly = true;
			passangersDataGridView.Columns[5].ReadOnly = true;
			passangersDataGridView.Columns[6].ReadOnly = true;
			passangersDataGridView.Columns[7].ReadOnly = false;

			for(int i = 0; i < passangersDataGridView.Rows.Count; i++) {
				passangersDataGridView.Rows[i].ReadOnly = 
					((PassangerBinding) passangersDataGridView.Rows[i].DataBoundItem).Canceled;
			}

			passangersDataGridView.ResumeLayout(false);
			passangersDataGridView.PerformLayout();
		}

		private void passangersDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			if(e.ColumnIndex == 7) {
				recalculateStats();
			}
		}

		private void recalculateStats() {
			var bookedCount = new int[context.classesNames.Length];
			var canceledCount = new int[context.classesNames.Length];
			var occupiedCount = new int[context.classesNames.Length];

			for(int i = 0; i < details.passangersAndSeats.Count; i++) {
				var classId = details.seatsClasses[details.passangersAndSeats[i].seatIndex];

				if(details.passangerArrived[i]) occupiedCount[classId]++;

				if(details.passangersAndSeats[i].canceled) canceledCount[classId]++;
				else bookedCount[classId]++;
			}

			seatsOccupationForClasses = new List<SeatsOccupation>(context.classesNames.Length);

			for(int i = 0; i < context.classesNames.Length; i++) {
				seatsOccupationForClasses.Add(new SeatsOccupation{ 
					classId = i,
					bookedCount = bookedCount[i],
					occupiedCount = occupiedCount[i],
					canceledCount = canceledCount[i],
					count = fac.flight.seatCountForClasses[i]
				});
			}

			dataGridView1.DataSource = new BindingSource{ DataSource = seatsOccupationForClasses };

			dataGridView1.ClearSelection();
		}

		private void startTimer(DateTime now) {
			updateTimer?.Dispose();

			updateTimer = new Timer();
			updateTimer.Interval = 999 - now.Millisecond;
			updateTimer.Tick += (a, b) => checkUpdateCountdown();
			updateTimer.Start();
		}

		private void checkUpdateCountdown() {
			var now = DateTime.Now;
			var thisTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

			if(lastUpdateTime != thisTime) {
				lastUpdateTime = thisTime;
				updateCountdown();
			}

			startTimer(now);
		}

		private void updateCountdown() {
			var diff = fac.flight.departureTime - lastUpdateTime;

			
			remainingTimeLabel.Text = "До вылёта: " + timespanString(diff);
			remainingTimeLabel.ForeColor = diff.Ticks >= 0 ? SystemColors.ControlText : Color.Firebrick;
		}

		static string timespanString(TimeSpan timespan) { 
		    if (timespan.Ticks < 0) return "-" + timespanString(timespan.Negate());
			var days = (int) Math.Floor(timespan.TotalDays);
		    return (days != 0 ? 
				Math.Floor(timespan.TotalDays).ToString() + " дней, " 
				: "") + timespan.Hours.ToString("00") + ":"
				+ timespan.Minutes.ToString("00") + ":" + timespan.Seconds.ToString("00");
		}

		private void registerDepartureButton_Click(object sender, EventArgs e) {
			var seatsArrival = new Dictionary<int, bool>(details.passangersAndSeats.Count);
			for(int i = 0; i < details.passangersAndSeats.Count; i++) {
				if(!details.passangersAndSeats[i].canceled) { 
					seatsArrival.Add(
						details.passangersAndSeats[i].seatIndex, 
						details.passangerArrived[i]
					);
				}
			}

			try{
				var result = service.updateArrivaltatus(fac.flight.id, seatsArrival);

				if(result) {
					statusLabel.Text = "Фиксация прошла успешно";
					statusLabel.ForeColor = SystemColors.ControlText;
					statusTooltip.SetToolTip(statusLabel, statusLabel.Text);
				}
				else {
					statusLabel.Text = result.f.message;
					statusLabel.ForeColor = Color.Firebrick;
					statusTooltip.SetToolTip(statusLabel, result.f.message);
				}
			}
			catch(Exception ex) {
				statusLabel.Text = "Неизвестная ошибка";
				statusLabel.ForeColor = Color.Firebrick;
				statusTooltip.SetToolTip(statusLabel, ex.ToString());
			}
		}
	}
}
