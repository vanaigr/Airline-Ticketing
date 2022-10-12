
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
			System.Windows.Forms.Label label2;
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.selectedStatusLabel = new System.Windows.Forms.Label();
			this.seatSelectTable = new Client.SeatsTable();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.flowLayoutPanelHeightBug = new System.Windows.Forms.Panel();
			this.passangersPanel = new Client.BugfixFlowLayoutPanel();
			this.headerContainer = new System.Windows.Forms.TableLayoutPanel();
			this.button2 = new System.Windows.Forms.Button();
			this.aitrplaneNameLavel = new System.Windows.Forms.Label();
			this.flightNameLabel = new System.Windows.Forms.Label();
			this.departureLocationLabel = new System.Windows.Forms.Label();
			this.departureDatetimeLabel = new System.Windows.Forms.Label();
			this.classSelector = new System.Windows.Forms.ComboBox();
			this.seatHint = new System.Windows.Forms.ToolTip(this.components);
			this.passangerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.passangerTooltip = new System.Windows.Forms.ToolTip(this.components);
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
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 57);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			tableLayoutPanel1.RowCount = 4;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.Size = new System.Drawing.Size(849, 400);
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
			this.tableLayoutPanel2.Controls.Add(this.selectedStatusLabel, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.seatSelectTable, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 86);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(829, 93);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(13, 10);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(42, 13);
			label2.TabIndex = 2;
			label2.Text = "Места:";
			// 
			// selectedStatusLabel
			// 
			this.selectedStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.selectedStatusLabel.AutoSize = true;
			this.selectedStatusLabel.Location = new System.Drawing.Point(715, 70);
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
			this.seatSelectTable.Location = new System.Drawing.Point(10, 30);
			this.seatSelectTable.Margin = new System.Windows.Forms.Padding(0);
			this.seatSelectTable.Name = "seatSelectTable";
			this.seatSelectTable.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
			this.seatSelectTable.RowCount = 1;
			this.seatSelectTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.seatSelectTable.Size = new System.Drawing.Size(809, 40);
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
			this.tableLayoutPanel3.Controls.Add(this.button1, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanelHeightBug, 1, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 10);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(829, 66);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.label1, 2);
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Пассажиры:";
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.Dock = System.Windows.Forms.DockStyle.Top;
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button1.Location = new System.Drawing.Point(13, 26);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(77, 27);
			this.button1.TabIndex = 1;
			this.button1.Text = "Добавить";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// flowLayoutPanelHeightBug
			// 
			this.flowLayoutPanelHeightBug.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanelHeightBug.Controls.Add(this.passangersPanel);
			this.flowLayoutPanelHeightBug.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanelHeightBug.Location = new System.Drawing.Point(93, 23);
			this.flowLayoutPanelHeightBug.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanelHeightBug.Name = "flowLayoutPanelHeightBug";
			this.flowLayoutPanelHeightBug.Size = new System.Drawing.Size(726, 33);
			this.flowLayoutPanelHeightBug.TabIndex = 2;
			this.flowLayoutPanelHeightBug.Resize += new System.EventHandler(this.flowLayoutPanelHeightBug_Resize);
			// 
			// passangersPanel
			// 
			this.passangersPanel.AutoSize = true;
			this.passangersPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangersPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.passangersPanel.Location = new System.Drawing.Point(0, 0);
			this.passangersPanel.Margin = new System.Windows.Forms.Padding(0);
			this.passangersPanel.Name = "passangersPanel";
			this.passangersPanel.Size = new System.Drawing.Size(726, 0);
			this.passangersPanel.TabIndex = 0;
			this.passangersPanel.Resize += new System.EventHandler(this.passangersPanel_Resize);
			// 
			// headerContainer
			// 
			this.headerContainer.AutoSize = true;
			this.headerContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.headerContainer.BackColor = System.Drawing.Color.White;
			this.headerContainer.ColumnCount = 7;
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.headerContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.headerContainer.Controls.Add(this.button2, 6, 0);
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
			this.headerContainer.Size = new System.Drawing.Size(849, 57);
			this.headerContainer.TabIndex = 0;
			// 
			// button2
			// 
			this.button2.AutoSize = true;
			this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button2.BackColor = System.Drawing.Color.RoyalBlue;
			this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.button2.Location = new System.Drawing.Point(743, 13);
			this.button2.Name = "button2";
			this.headerContainer.SetRowSpan(this.button2, 3);
			this.button2.Size = new System.Drawing.Size(93, 31);
			this.button2.TabIndex = 6;
			this.button2.Text = "Продолжить";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// aitrplaneNameLavel
			// 
			this.aitrplaneNameLavel.AutoSize = true;
			this.aitrplaneNameLavel.Location = new System.Drawing.Point(13, 30);
			this.aitrplaneNameLavel.Name = "aitrplaneNameLavel";
			this.aitrplaneNameLavel.Size = new System.Drawing.Size(75, 13);
			this.aitrplaneNameLavel.TabIndex = 4;
			this.aitrplaneNameLavel.Text = "aitrplaneName";
			// 
			// flightNameLabel
			// 
			this.flightNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.flightNameLabel.AutoSize = true;
			this.flightNameLabel.Location = new System.Drawing.Point(13, 14);
			this.flightNameLabel.Name = "flightNameLabel";
			this.flightNameLabel.Size = new System.Drawing.Size(57, 13);
			this.flightNameLabel.TabIndex = 0;
			this.flightNameLabel.Text = "flightName";
			// 
			// departureLocationLabel
			// 
			this.departureLocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.departureLocationLabel.AutoSize = true;
			this.departureLocationLabel.Location = new System.Drawing.Point(94, 14);
			this.departureLocationLabel.Name = "departureLocationLabel";
			this.departureLocationLabel.Size = new System.Drawing.Size(93, 13);
			this.departureLocationLabel.TabIndex = 1;
			this.departureLocationLabel.Text = "departureLocation";
			// 
			// departureDatetimeLabel
			// 
			this.departureDatetimeLabel.AutoSize = true;
			this.departureDatetimeLabel.Location = new System.Drawing.Point(94, 30);
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
			this.classSelector.Location = new System.Drawing.Point(606, 18);
			this.classSelector.Name = "classSelector";
			this.headerContainer.SetRowSpan(this.classSelector, 3);
			this.classSelector.Size = new System.Drawing.Size(121, 21);
			this.classSelector.TabIndex = 5;
			this.classSelector.SelectedIndexChanged += new System.EventHandler(this.classSelector_SelectedIndexChanged);
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
			// FlightBooking
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(849, 457);
			this.Controls.Add(tableLayoutPanel1);
			this.Controls.Add(this.headerContainer);
			this.Name = "FlightBooking";
			this.Text = "FlightBooking";
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
		private System.Windows.Forms.ComboBox classSelector;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ToolTip seatHint;
		private System.Windows.Forms.Label selectedStatusLabel;
		private BugfixFlowLayoutPanel passangersPanel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private SeatsTable seatSelectTable;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ContextMenuStrip passangerMenu;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.Panel flowLayoutPanelHeightBug;
		private System.Windows.Forms.ToolTip passangerTooltip;
	}
}