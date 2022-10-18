using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerOptions : UserControl {
		private Dictionary<int, FlightsOptions.Options> optionsForClasses;

		private BookingPassanger passanger;

		public PassangerOptions() {
			InitializeComponent();
			Misc.fixFlowLayoutPanelHeight(this.baggageOptions);
		}

		public void init(
			Dictionary<int, FlightsOptions.Options> optionsForClasses,
			BookingPassanger passanger
		) {
			this.optionsForClasses = optionsForClasses;
			this.passanger = passanger;
		}

		public void setForClass(int classId) {
			var options = optionsForClasses[classId];

			var baggages = options.baggageOptions.baggage;

			baggageOptions.SuspendLayout();
			baggageOptions.Controls.Clear();

			for(int i = 0; i < baggages.Count; i++) {
				var b = baggages[i];
				if(b.IsFree) baggageOptions.Controls.Add(
					new BaggageOption {
						Baggage = null,
						Margin = new Padding(3),
						Index = i
					}
				);
			}

			for(int i = 0; i < baggages.Count; i++) {
				var b = baggages[i];
				if(!b.IsFree) baggageOptions.Controls.Add(
					new BaggageOption {
						Baggage = b,
						Margin = new Padding(3),
						Index = i
					}
				);
			}

			baggageOptions.ResumeLayout(false);
			baggageOptions.PerformLayout();
		}

		private void baggageOptionClicked(BaggageOption option) {

		}
	}
}
