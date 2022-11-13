using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Operator {
	public partial class PassangersList : Form {
		private Context context;
		private List<Communication.PassangerBookedFlight> bookedFlights;

		private sealed class PassangerBookedFlightDisplay {
			public Communication.PassangerBookedFlight flight;

			[DisplayName("Имя")] public string Name{ get{ return flight.passanger.name; } }
			[DisplayName("Фамилия")] public string Surname{ get{ return flight.passanger.surname; } }
			[DisplayName("Отчество")] public string MiddleName{ get{ return flight.passanger.middleName; } }
			[DisplayName("PNR")] public string PNR{ get{ return flight.bookedSeat.pnr; } }
			[DisplayName("Код рейса")] public string FlightName{ get{ return flight.flight.availableFlight.flightName; } }
			[DisplayName("Время отлёта")] public string DepDatetime{ get{ return flight.flight.availableFlight.departureTime.ToString("dd.MM.yyyy, HH:mm"); } }
		}

		public PassangersList(
			Context context,
			List<Communication.PassangerBookedFlight> bookedFlights
		) {
			this.context = context;
			this.bookedFlights = bookedFlights;

			InitializeComponent();

			Common.Misc.unfocusOnEscape(this, (a, b) => { if(b.KeyCode == Keys.Escape) passangerGridView.ClearSelection(); });

			onlyAvailableCB.Checked = false;
			onlyAvailableCB.Checked = true;
		}

		private void updateGridView() {
			var now = DateTime.UtcNow;
			List<PassangerBookedFlightDisplay> filteredFlights = new List<PassangerBookedFlightDisplay>();
			for(int i = 0; i < bookedFlights.Count; i++) {
				if(bookedFlights[i].flight.availableFlight.departureTime >= now || !onlyAvailableCB.Checked) {
					filteredFlights.Add(new PassangerBookedFlightDisplay{ flight = bookedFlights[i] });
				}
			}

			passangerGridView.SuspendLayout();

			passangerGridView.DataSource = new BindingSource{ DataSource = filteredFlights };

			passangerGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			passangerGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			passangerGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			passangerGridView.ResumeLayout(false);
			passangerGridView.PerformLayout();
		}

		private void passangerGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
			e.CellStyle.BackColor = e.RowIndex % 2 == 0 ? Color.FromArgb(255, 255, 255) : Color.FromArgb(240, 240, 240);

			//if(e.ColumnIndex == 5) {
			//	if((int) e.Value == -1) {
			//		e.Value = "Итого";
			//		e.FormattingApplied = true;
			//	}
			//	else { 
			//		e.Value = context.classesNames[(int) e.Value];
			//		e.FormattingApplied = true;
			//	}
			//}
		}

		private void onlyAvailableCB_CheckedChanged(object sender, EventArgs e) {
			updateGridView();
		}

		private void passangerGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			try{
			Clipboard.SetText(
				passangerGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
			);
			} catch(Exception ex) { }
		}
	}
}
