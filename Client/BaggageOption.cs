using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class BaggageOption : UserControl {
		private FlightsOptions.Baggage baggage;

		public FlightsOptions.Baggage Baggage{
			get {
				return baggage;
			}
			set {
				baggage = value;
				updateDisplay();		
			}
		}

		public int Index;

		public BaggageOption() {
			InitializeComponent();

			touchArea.Click += (a, b) => this.OnClick(b);
			touchArea.BringToFront();
		}

		private void updateDisplay() {
			mainTable.SuspendLayout();

			mainParamLabel.Text = "";
			axilParamLabel.Text = "";
			priceLabel.Text = "";
			

			if(baggage.count == 0) {
				mainParamLabel.Text = "Без багажа";
			}
			else if(baggage.RestrictionWeight) {
				mainParamLabel.Text = baggage.count + " x " + baggage.maxWeightKg + " кг" + (
					baggage.RestrictionSize ? "*" : ""	
				);

				if(baggage.RestrictionSize) {
					var md = baggage.maxDim;
					axilParamLabel.Text = "*до " + md.x + "x" + md.y + "x" + md.z;
				}

			}
			else if(baggage.RestrictionSize) {
				mainParamLabel.Text = baggage.count + " x сумка*";

				var md = baggage.maxDim;
				axilParamLabel.Text = "*до " + md.x + "x" + md.y + "x" + md.z;
			}
			else mainParamLabel.Text = baggage.count + " x сумка";

			
			if(baggage.IsFree) priceLabel.Text = "Бесплатно";
			else priceLabel.Text = baggage.costRub + " руб.";

			mainTable.ResumeLayout(false);
			mainTable.PerformLayout();
		}
	}
}
