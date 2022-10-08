
using System.Drawing;

namespace Client {
	partial class SelectFlight {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFlight));
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label8;
			System.Windows.Forms.Label label7;
			System.Windows.Forms.Label label6;
			this.loginLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusLabel = new System.Windows.Forms.Label();
			this.fromDepDate = new System.Windows.Forms.DateTimePicker();
			this.toLoc = new Client.SelectFlight.CityComboBox();
			this.fromLoc = new Client.SelectFlight.CityComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.findFlightsButton = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.flightsTable = new System.Windows.Forms.TableLayoutPanel();
			this.elementHint = new System.Windows.Forms.ToolTip(this.components);
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			label1 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel1.Controls.Add(this.loginLayoutPanel, 1, 0);
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(1075, 57);
			tableLayoutPanel1.TabIndex = 2;
			// 
			// loginLayoutPanel
			// 
			this.loginLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.loginLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loginLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.loginLayoutPanel.Location = new System.Drawing.Point(540, 3);
			this.loginLayoutPanel.Name = "loginLayoutPanel";
			this.loginLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
			this.loginLayoutPanel.Size = new System.Drawing.Size(532, 51);
			this.loginLayoutPanel.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 510F));
			this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.statusLabel, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(531, 51);
			this.tableLayoutPanel3.TabIndex = 2;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(15, 25);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Location = new System.Drawing.Point(34, 10);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(504, 31);
			this.statusLabel.TabIndex = 1;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Controls.Add(label8, 1, 3);
			tableLayoutPanel2.Controls.Add(label7, 1, 2);
			tableLayoutPanel2.Controls.Add(label6, 1, 1);
			tableLayoutPanel2.Controls.Add(this.fromDepDate, 2, 3);
			tableLayoutPanel2.Controls.Add(this.toLoc, 2, 2);
			tableLayoutPanel2.Controls.Add(this.fromLoc, 2, 1);
			tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 11);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
			tableLayoutPanel2.RowCount = 12;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Size = new System.Drawing.Size(272, 393);
			tableLayoutPanel2.TabIndex = 3;
			// 
			// label1
			// 
			label1.AutoSize = true;
			tableLayoutPanel2.SetColumnSpan(label1, 3);
			label1.Location = new System.Drawing.Point(8, 5);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(151, 13);
			label1.TabIndex = 21;
			label1.Text = "Время и место отправления";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Dock = System.Windows.Forms.DockStyle.Fill;
			label8.Location = new System.Drawing.Point(18, 72);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(46, 26);
			label8.TabIndex = 19;
			label8.Text = "Когда:";
			label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Dock = System.Windows.Forms.DockStyle.Fill;
			label7.Location = new System.Drawing.Point(18, 45);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(46, 27);
			label7.TabIndex = 18;
			label7.Text = "Куда:";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Dock = System.Windows.Forms.DockStyle.Fill;
			label6.Location = new System.Drawing.Point(18, 18);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(46, 27);
			label6.TabIndex = 17;
			label6.Text = "Откуда:";
			label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fromDepDate
			// 
			this.fromDepDate.CustomFormat = "d MMM, ddd";
			this.fromDepDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromDepDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDepDate.Location = new System.Drawing.Point(70, 75);
			this.fromDepDate.Name = "fromDepDate";
			this.fromDepDate.Size = new System.Drawing.Size(194, 20);
			this.fromDepDate.TabIndex = 15;
			// 
			// toLoc
			// 
			this.toLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.toLoc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.toLoc.FormattingEnabled = true;
			this.toLoc.Location = new System.Drawing.Point(70, 48);
			this.toLoc.Name = "toLoc";
			this.toLoc.Size = new System.Drawing.Size(194, 21);
			this.toLoc.TabIndex = 14;
			// 
			// fromLoc
			// 
			this.fromLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.fromLoc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fromLoc.FormattingEnabled = true;
			this.fromLoc.Location = new System.Drawing.Point(70, 21);
			this.fromLoc.Name = "fromLoc";
			this.fromLoc.Size = new System.Drawing.Size(194, 21);
			this.fromLoc.TabIndex = 13;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel1, 3);
			this.flowLayoutPanel1.Controls.Add(this.findFlightsButton);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(183, 101);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(81, 284);
			this.flowLayoutPanel1.TabIndex = 30;
			// 
			// findFlightsButton
			// 
			this.findFlightsButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.findFlightsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.findFlightsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.findFlightsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findFlightsButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.findFlightsButton.Location = new System.Drawing.Point(3, 256);
			this.findFlightsButton.Name = "findFlightsButton";
			this.findFlightsButton.Size = new System.Drawing.Size(75, 25);
			this.findFlightsButton.TabIndex = 2;
			this.findFlightsButton.Text = "Найти";
			this.findFlightsButton.UseVisualStyleBackColor = false;
			this.findFlightsButton.Click += new System.EventHandler(this.findFlightsButton_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 57);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(tableLayoutPanel2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.flightsTable);
			this.splitContainer1.Size = new System.Drawing.Size(1075, 393);
			this.splitContainer1.SplitterDistance = 272;
			this.splitContainer1.TabIndex = 0;
			// 
			// flightsTable
			// 
			this.flightsTable.AutoScroll = true;
			this.flightsTable.ColumnCount = 1;
			this.flightsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.flightsTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flightsTable.Location = new System.Drawing.Point(0, 0);
			this.flightsTable.Name = "flightsTable";
			this.flightsTable.Padding = new System.Windows.Forms.Padding(10);
			this.flightsTable.RowCount = 1;
			this.flightsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.flightsTable.Size = new System.Drawing.Size(799, 393);
			this.flightsTable.TabIndex = 4;
			// 
			// SelectFlight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(1075, 450);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(400, 39);
			this.Name = "SelectFlight";
			this.Text = "SelectFlight";
			tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel loginLayoutPanel;
		private System.Windows.Forms.DateTimePicker fromDepDate;
		private CityComboBox toLoc;
		private CityComboBox fromLoc;
		private System.Windows.Forms.ToolTip elementHint;
		private System.Windows.Forms.TableLayoutPanel flightsTable;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button findFlightsButton;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}