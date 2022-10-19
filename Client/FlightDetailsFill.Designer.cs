
namespace Client {
	partial class FlightDetailsFill {
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
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
			System.Windows.Forms.Label label2;
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.selectedStatusLabel = new System.Windows.Forms.Label();
			this.seatSelectTable = new Client.SeatsTable();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.flowLayoutPanelHeightBug = new System.Windows.Forms.Panel();
			this.passangersPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.addAutoseat = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.headerContainer = new System.Windows.Forms.TableLayoutPanel();
			this.continueButton = new System.Windows.Forms.Button();
			this.aitrplaneNameLavel = new System.Windows.Forms.Label();
			this.flightNameLabel = new System.Windows.Forms.Label();
			this.departureLocationLabel = new System.Windows.Forms.Label();
			this.departureDatetimeLabel = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.seatHint = new System.Windows.Forms.ToolTip(this.components);
			this.passangerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.passangerTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.flowLayoutPanelHeightBug.SuspendLayout();
			this.headerContainer.SuspendLayout();
			this.passangerMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoScroll = true;
			tableLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(0, 10);
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			tableLayoutPanel1.Controls.Add(this.panel1, 0, 5);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 53);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.Size = new System.Drawing.Size(880, 449);
			tableLayoutPanel1.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(label2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.selectedStatusLabel, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.seatSelectTable, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 105);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel2.RowCount = 5;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(860, 100);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label2.Location = new System.Drawing.Point(13, 10);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(48, 15);
			label2.TabIndex = 2;
			label2.Text = "Места:";
			// 
			// selectedStatusLabel
			// 
			this.selectedStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.selectedStatusLabel.AutoSize = true;
			this.selectedStatusLabel.Location = new System.Drawing.Point(746, 77);
			this.selectedStatusLabel.Margin = new System.Windows.Forms.Padding(0);
			this.selectedStatusLabel.Name = "selectedStatusLabel";
			this.selectedStatusLabel.Size = new System.Drawing.Size(104, 13);
			this.selectedStatusLabel.TabIndex = 4;
			this.selectedStatusLabel.Text = "selectedSeatsStatus";
			// 
			// seatSelectTable
			// 
			this.seatSelectTable.AutoSize = true;
			this.seatSelectTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.seatSelectTable.ColumnCount = 1;
			this.seatSelectTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.seatSelectTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.seatSelectTable.Location = new System.Drawing.Point(10, 31);
			this.seatSelectTable.Margin = new System.Windows.Forms.Padding(0);
			this.seatSelectTable.Name = "seatSelectTable";
			this.seatSelectTable.Padding = new System.Windows.Forms.Padding(40, 20, 30, 20);
			this.seatSelectTable.RowCount = 1;
			this.seatSelectTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.seatSelectTable.Size = new System.Drawing.Size(840, 40);
			this.seatSelectTable.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanelHeightBug, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.addAutoseat, 0, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 10);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(860, 85);
			this.tableLayoutPanel3.TabIndex = 6;
			this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.label1, 2);
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Пассажиры:";
			// 
			// flowLayoutPanelHeightBug
			// 
			this.flowLayoutPanelHeightBug.Controls.Add(this.passangersPanel);
			this.flowLayoutPanelHeightBug.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanelHeightBug.Location = new System.Drawing.Point(107, 31);
			this.flowLayoutPanelHeightBug.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanelHeightBug.Name = "flowLayoutPanelHeightBug";
			this.flowLayoutPanelHeightBug.Size = new System.Drawing.Size(743, 33);
			this.flowLayoutPanelHeightBug.TabIndex = 2;
			// 
			// passangersPanel
			// 
			this.passangersPanel.AutoSize = true;
			this.passangersPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangersPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.passangersPanel.Location = new System.Drawing.Point(0, 0);
			this.passangersPanel.Margin = new System.Windows.Forms.Padding(0);
			this.passangersPanel.Name = "passangersPanel";
			this.passangersPanel.Size = new System.Drawing.Size(743, 0);
			this.passangersPanel.TabIndex = 0;
			// 
			// addAutoseat
			// 
			this.addAutoseat.AutoSize = true;
			this.addAutoseat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.addAutoseat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.addAutoseat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.addAutoseat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addAutoseat.Location = new System.Drawing.Point(13, 34);
			this.addAutoseat.Name = "addAutoseat";
			this.addAutoseat.Size = new System.Drawing.Size(91, 38);
			this.addAutoseat.TabIndex = 3;
			this.addAutoseat.Text = "Добавить без\r\nвыбора места";
			this.addAutoseat.UseVisualStyleBackColor = true;
			this.addAutoseat.Click += new System.EventHandler(this.addAutoseat_Click);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Location = new System.Drawing.Point(13, 218);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(0, 0);
			this.panel1.TabIndex = 8;
			// 
			// headerContainer
			// 
			this.headerContainer.AutoSize = true;
			this.headerContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.headerContainer.BackColor = System.Drawing.Color.White;
			this.headerContainer.ColumnCount = 8;
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.headerContainer.Controls.Add(this.continueButton, 6, 1);
			this.headerContainer.Controls.Add(this.aitrplaneNameLavel, 1, 3);
			this.headerContainer.Controls.Add(this.flightNameLabel, 1, 1);
			this.headerContainer.Controls.Add(this.departureLocationLabel, 2, 1);
			this.headerContainer.Controls.Add(this.departureDatetimeLabel, 2, 3);
			this.headerContainer.Controls.Add(this.statusLabel, 4, 1);
			this.headerContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerContainer.Location = new System.Drawing.Point(0, 0);
			this.headerContainer.Name = "headerContainer";
			this.headerContainer.RowCount = 5;
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.headerContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.headerContainer.Size = new System.Drawing.Size(880, 53);
			this.headerContainer.TabIndex = 0;
			// 
			// continueButton
			// 
			this.continueButton.AutoSize = true;
			this.continueButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.continueButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.continueButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.continueButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.continueButton.Location = new System.Drawing.Point(774, 13);
			this.continueButton.Name = "continueButton";
			this.headerContainer.SetRowSpan(this.continueButton, 3);
			this.continueButton.Size = new System.Drawing.Size(93, 27);
			this.continueButton.TabIndex = 6;
			this.continueButton.Text = "Продолжить";
			this.continueButton.UseVisualStyleBackColor = false;
			this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
			// 
			// aitrplaneNameLavel
			// 
			this.aitrplaneNameLavel.AutoSize = true;
			this.aitrplaneNameLavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.aitrplaneNameLavel.Location = new System.Drawing.Point(13, 28);
			this.aitrplaneNameLavel.Name = "aitrplaneNameLavel";
			this.aitrplaneNameLavel.Size = new System.Drawing.Size(89, 15);
			this.aitrplaneNameLavel.TabIndex = 4;
			this.aitrplaneNameLavel.Text = "aitrplaneName";
			// 
			// flightNameLabel
			// 
			this.flightNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flightNameLabel.AutoSize = true;
			this.flightNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.flightNameLabel.Location = new System.Drawing.Point(13, 10);
			this.flightNameLabel.Name = "flightNameLabel";
			this.flightNameLabel.Size = new System.Drawing.Size(67, 15);
			this.flightNameLabel.TabIndex = 0;
			this.flightNameLabel.Text = "flightName";
			// 
			// departureLocationLabel
			// 
			this.departureLocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.departureLocationLabel.AutoSize = true;
			this.departureLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.departureLocationLabel.Location = new System.Drawing.Point(108, 10);
			this.departureLocationLabel.Name = "departureLocationLabel";
			this.departureLocationLabel.Size = new System.Drawing.Size(107, 15);
			this.departureLocationLabel.TabIndex = 1;
			this.departureLocationLabel.Text = "departureLocation";
			// 
			// departureDatetimeLabel
			// 
			this.departureDatetimeLabel.AutoSize = true;
			this.departureDatetimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.departureDatetimeLabel.Location = new System.Drawing.Point(108, 28);
			this.departureDatetimeLabel.Name = "departureDatetimeLabel";
			this.departureDatetimeLabel.Size = new System.Drawing.Size(110, 15);
			this.departureDatetimeLabel.TabIndex = 2;
			this.departureDatetimeLabel.Text = "departureDatetime";
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(224, 10);
			this.statusLabel.Name = "statusLabel";
			this.headerContainer.SetRowSpan(this.statusLabel, 3);
			this.statusLabel.Size = new System.Drawing.Size(544, 33);
			this.statusLabel.TabIndex = 7;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// passangerMenu
			// 
			this.passangerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem});
			this.passangerMenu.Name = "passangerMenu";
			this.passangerMenu.Size = new System.Drawing.Size(119, 26);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// FlightDetailsFill
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(880, 502);
			this.Controls.Add(tableLayoutPanel1);
			this.Controls.Add(this.headerContainer);
			this.Name = "FlightDetailsFill";
			this.Text = "Бронирование мест";
			this.Load += new System.EventHandler(this.FlightBooking_Load);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.flowLayoutPanelHeightBug.ResumeLayout(false);
			this.flowLayoutPanelHeightBug.PerformLayout();
			this.headerContainer.ResumeLayout(false);
			this.headerContainer.PerformLayout();
			this.passangerMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel headerContainer;
		private System.Windows.Forms.Label flightNameLabel;
		private System.Windows.Forms.Label departureLocationLabel;
		private System.Windows.Forms.Label departureDatetimeLabel;
		private System.Windows.Forms.Label aitrplaneNameLavel;
		private System.Windows.Forms.Button continueButton;
		private System.Windows.Forms.ToolTip seatHint;
		private System.Windows.Forms.Label selectedStatusLabel;
		private System.Windows.Forms.FlowLayoutPanel passangersPanel;
		private System.Windows.Forms.Label label1;
		private SeatsTable seatSelectTable;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ContextMenuStrip passangerMenu;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.Panel flowLayoutPanelHeightBug;
		private System.Windows.Forms.ToolTip passangerTooltip;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button addAutoseat;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip statusTooltip;
	}
}