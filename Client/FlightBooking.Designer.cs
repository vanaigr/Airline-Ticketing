
namespace Client {
	partial class FlightBooking {
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
			this.headerContainer = new System.Windows.Forms.TableLayoutPanel();
			this.aitrplaneNameLavel = new System.Windows.Forms.Label();
			this.flightNameLabel = new System.Windows.Forms.Label();
			this.departureLocationLabel = new System.Windows.Forms.Label();
			this.departureDatetimeLabel = new System.Windows.Forms.Label();
			this.classSelector = new System.Windows.Forms.ComboBox();
			this.headerContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// headerContainer
			// 
			this.headerContainer.AutoSize = true;
			this.headerContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.headerContainer.BackColor = System.Drawing.Color.White;
			this.headerContainer.ColumnCount = 5;
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.Controls.Add(this.aitrplaneNameLavel, 0, 2);
			this.headerContainer.Controls.Add(this.flightNameLabel, 0, 0);
			this.headerContainer.Controls.Add(this.departureLocationLabel, 1, 0);
			this.headerContainer.Controls.Add(this.departureDatetimeLabel, 1, 2);
			this.headerContainer.Controls.Add(this.classSelector, 4, 0);
			this.headerContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerContainer.Location = new System.Drawing.Point(0, 0);
			this.headerContainer.Name = "headerContainer";
			this.headerContainer.Padding = new System.Windows.Forms.Padding(10);
			this.headerContainer.RowCount = 3;
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.headerContainer.Size = new System.Drawing.Size(1162, 49);
			this.headerContainer.TabIndex = 0;
			// 
			// aitrplaneNameLavel
			// 
			this.aitrplaneNameLavel.AutoSize = true;
			this.aitrplaneNameLavel.Location = new System.Drawing.Point(13, 26);
			this.aitrplaneNameLavel.Name = "aitrplaneNameLavel";
			this.aitrplaneNameLavel.Size = new System.Drawing.Size(75, 13);
			this.aitrplaneNameLavel.TabIndex = 4;
			this.aitrplaneNameLavel.Text = "aitrplaneName";
			// 
			// flightNameLabel
			// 
			this.flightNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flightNameLabel.AutoSize = true;
			this.flightNameLabel.Location = new System.Drawing.Point(13, 10);
			this.flightNameLabel.Name = "flightNameLabel";
			this.flightNameLabel.Size = new System.Drawing.Size(57, 13);
			this.flightNameLabel.TabIndex = 0;
			this.flightNameLabel.Text = "flightName";
			// 
			// departureLocationLabel
			// 
			this.departureLocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.departureLocationLabel.AutoSize = true;
			this.departureLocationLabel.Location = new System.Drawing.Point(94, 10);
			this.departureLocationLabel.Name = "departureLocationLabel";
			this.departureLocationLabel.Size = new System.Drawing.Size(93, 13);
			this.departureLocationLabel.TabIndex = 1;
			this.departureLocationLabel.Text = "departureLocation";
			// 
			// departureDatetimeLabel
			// 
			this.departureDatetimeLabel.AutoSize = true;
			this.departureDatetimeLabel.Location = new System.Drawing.Point(94, 26);
			this.departureDatetimeLabel.Name = "departureDatetimeLabel";
			this.departureDatetimeLabel.Size = new System.Drawing.Size(94, 13);
			this.departureDatetimeLabel.TabIndex = 2;
			this.departureDatetimeLabel.Text = "departureDatetime";
			// 
			// classSelector
			// 
			this.classSelector.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.classSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.classSelector.FormattingEnabled = true;
			this.classSelector.Location = new System.Drawing.Point(1004, 14);
			this.classSelector.Name = "classSelector";
			this.headerContainer.SetRowSpan(this.classSelector, 3);
			this.classSelector.Size = new System.Drawing.Size(145, 21);
			this.classSelector.TabIndex = 5;
			this.classSelector.SelectedIndexChanged += new System.EventHandler(this.classSelector_SelectedIndexChanged);
			// 
			// FlightBooking
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1162, 453);
			this.Controls.Add(this.headerContainer);
			this.Name = "FlightBooking";
			this.Text = "FlightBooking";
			this.Load += new System.EventHandler(this.FlightBooking_Load);
			this.headerContainer.ResumeLayout(false);
			this.headerContainer.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel headerContainer;
		private System.Windows.Forms.Label flightNameLabel;
		private System.Windows.Forms.Label departureLocationLabel;
		private System.Windows.Forms.Label departureDatetimeLabel;
		private System.Windows.Forms.Label aitrplaneNameLavel;
		private System.Windows.Forms.ComboBox classSelector;
	}
}