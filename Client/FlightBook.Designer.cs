
namespace Client {
	partial class FlightBook {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bookFlight = new System.Windows.Forms.Button();
			this.totalPriceLabel = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.passangersSummaryPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.bookFlight, 4, 2);
			this.tableLayoutPanel1.Controls.Add(this.totalPriceLabel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.statusLabel, 2, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 348);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bookFlight
			// 
			this.bookFlight.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.bookFlight.AutoSize = true;
			this.bookFlight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bookFlight.BackColor = System.Drawing.Color.RoyalBlue;
			this.bookFlight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.bookFlight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.bookFlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bookFlight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bookFlight.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.bookFlight.Location = new System.Drawing.Point(604, 311);
			this.bookFlight.Margin = new System.Windows.Forms.Padding(0);
			this.bookFlight.Name = "bookFlight";
			this.bookFlight.Size = new System.Drawing.Size(104, 27);
			this.bookFlight.TabIndex = 2;
			this.bookFlight.Text = "Забронировать";
			this.bookFlight.UseVisualStyleBackColor = false;
			this.bookFlight.Click += new System.EventHandler(this.bookFlight_Click);
			// 
			// totalPriceLabel
			// 
			this.totalPriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.totalPriceLabel.AutoSize = true;
			this.totalPriceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.totalPriceLabel.Location = new System.Drawing.Point(13, 317);
			this.totalPriceLabel.Name = "totalPriceLabel";
			this.totalPriceLabel.Size = new System.Drawing.Size(38, 15);
			this.totalPriceLabel.TabIndex = 3;
			this.totalPriceLabel.Text = "label1";
			// 
			// statusLabel
			// 
			this.statusLabel.AutoEllipsis = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.Location = new System.Drawing.Point(67, 311);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(524, 27);
			this.statusLabel.TabIndex = 4;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// passangersSummaryPanel
			// 
			this.passangersSummaryPanel.AutoSize = true;
			this.passangersSummaryPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangersSummaryPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.passangersSummaryPanel.Location = new System.Drawing.Point(0, 0);
			this.passangersSummaryPanel.Margin = new System.Windows.Forms.Padding(0);
			this.passangersSummaryPanel.Name = "passangersSummaryPanel";
			this.passangersSummaryPanel.Size = new System.Drawing.Size(692, 0);
			this.passangersSummaryPanel.TabIndex = 0;
			// 
			// panel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 5);
			this.panel1.Controls.Add(this.passangersSummaryPanel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(13, 13);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(692, 4);
			this.panel1.TabIndex = 1;
			// 
			// FlightBook
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(718, 348);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "FlightBook";
			this.Text = "Бронирование билетов";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bookFlight;
		private System.Windows.Forms.Label totalPriceLabel;
		private System.Windows.Forms.ToolTip statusTooltip;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel passangersSummaryPanel;
	}
}