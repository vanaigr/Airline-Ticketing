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

		public BaggageOption() {
			InitializeComponent();
		}

		private void addPaddingRow() {
			mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 3));
			mainTable.RowCount++;
		}

		private void updateDisplay() {
			mainTable.SuspendLayout();

			mainTable.RowStyles.Clear();
			mainTable.Controls.Clear();
			mainTable.RowCount = 0;

			//mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
			//mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

			if(baggage == null) {
				mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				mainTable.RowCount++;

				var countLabel = new Label();
				countLabel.Font = new Font(countLabel.Font.FontFamily, 10.0f);
				countLabel.TextAlign = ContentAlignment.MiddleCenter;
				countLabel.Dock = DockStyle.Fill;
				countLabel.Text = "Без багажа";

				mainTable.Controls.Add(countLabel, 0, mainTable.RowCount-1);
			}
			else if(baggage.RestrictionWeight) {
				{
					mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					mainTable.RowCount++;

					var countLabel = new Label();
					countLabel.Font = new Font(countLabel.Font.FontFamily, 10.0f);
					countLabel.TextAlign = ContentAlignment.MiddleCenter;
					countLabel.Dock = DockStyle.Fill;
					countLabel.Text = baggage.count + " x " + baggage.maxWeightKg + " кг" + (
						baggage.RestrictionSize ? "*" : ""	
					);

					mainTable.Controls.Add(countLabel, 0, mainTable.RowCount-1);
				}

				if(baggage.RestrictionSize) {
					//addPaddingRow();

					mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					mainTable.RowCount++;

					var sizeRestriction = new Label();
					sizeRestriction.ForeColor = Color.LightGray;
					sizeRestriction.TextAlign = ContentAlignment.MiddleCenter;
					sizeRestriction.Dock = DockStyle.Fill;
					var md = baggage.maxDim;
					sizeRestriction.Text = "*до " + md.x + "x" + md.y + "x" + md.z;

					mainTable.Controls.Add(sizeRestriction, 0, mainTable.RowCount-1);
				}

			}
			else if(baggage.RestrictionSize) {
				mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				mainTable.RowCount++;

				var countLabel = new Label();
				countLabel.Font = new Font(countLabel.Font.FontFamily, 10.0f);
				countLabel.TextAlign = ContentAlignment.MiddleCenter;
				countLabel.Dock = DockStyle.Fill;
				countLabel.Text = baggage.count + " x " + baggage.maxWeightKg + " кг" + (
					baggage.RestrictionSize ? "*" : ""	
				);

				mainTable.Controls.Add(countLabel, 0, mainTable.RowCount-1);
			}
			else {
				
			}

			{
				addPaddingRow();

				mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				mainTable.RowCount++;

				var price = new Label();
				price.TextAlign = ContentAlignment.MiddleCenter;
				price.Dock = DockStyle.Fill;
				if(baggage == null || baggage.IsFree) price.Text = "Бесплатно";
				else price.Text = baggage.costRub + " руб.";

				mainTable.Controls.Add(price, 0, mainTable.RowCount-1);
			}

			mainTable.ResumeLayout(false);
			mainTable.PerformLayout();
		}
	}
}
