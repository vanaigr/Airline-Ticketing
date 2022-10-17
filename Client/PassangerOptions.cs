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
		private Dictionary<int, string> classesNames;
		public PassangerOptions() {
			InitializeComponent();

			Misc.fixFlowLayoutPanelHeight(this.baggageOptions);
		}

		public void init(
			Dictionary<int, FlightsOptions.Options> optionsForClasses,
			Dictionary<int, string> classesNames
		) {
			this.optionsForClasses = optionsForClasses;

			this.classesNames = new Dictionary<int, string>();
			foreach(var classId in optionsForClasses.Keys) {
				this.classesNames.Add(classId, classesNames[classId]);
			}
			
			classSelector.DataSource = new BindingSource{ DataSource = classesNames };
			classSelector.DisplayMember = "Value";
			classSelector.SelectedIndex = 0;
		}

		private void classSelector_SelectedIndexChanged(object sender, EventArgs e) {
			var options = optionsForClasses[((KeyValuePair<int, string>)classSelector.SelectedItem).Key];

			baggageOptions.SuspendLayout();
			baggageOptions.Controls.Clear();
			var hasFreeBaggage = false;
			foreach(var b in options.baggageOptions.baggage) if(b.costRub == 0) {
				hasFreeBaggage = true; 
				break;
			}

			if(!hasFreeBaggage) this.baggageOptions.Controls.Add(
				new BaggageOption{ Baggage = null, Margin = new Padding(3) }
			);

			foreach(var b in options.baggageOptions.baggage) this.baggageOptions.Controls.Add(
				new BaggageOption{ Baggage = b, Margin = new Padding(3) }
			);

			this.baggageOptions.Controls.Add(
				new BaggageOption{ Baggage = new FlightsOptions.Baggage(costRub: 0, count: 1, maxDim: new FlightsOptions.Size3{ x=55, y=40, z=20 }), Margin = new Padding(3) }
			);

			this.baggageOptions.Controls.Add(
				new BaggageOption{ Baggage = new FlightsOptions.Baggage(costRub: 0, count: 1, maxWeightKg: 10, maxDim: new FlightsOptions.Size3{ x=55, y=40, z=20 }), Margin = new Padding(3) }
			);

			baggageOptions.ResumeLayout(false);
			baggageOptions.PerformLayout();


		}
	}
}
