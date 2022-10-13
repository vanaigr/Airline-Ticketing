
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
			this.loginLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.findFlightsButton = new System.Windows.Forms.Button();
			this.fromDepDate = new System.Windows.Forms.DateTimePicker();
			this.toLoc = new Client.SelectFlight.CityComboBox();
			this.fromLoc = new Client.SelectFlight.CityComboBox();
			this.flightsTable = new System.Windows.Forms.TableLayoutPanel();
			this.elementHint = new System.Windows.Forms.ToolTip(this.components);
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.ColumnCount = 5;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			tableLayoutPanel1.Controls.Add(this.loginLayoutPanel, 4, 0);
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 0);
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
			this.loginLayoutPanel.AutoSize = true;
			this.loginLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.loginLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.loginLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loginLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.loginLayoutPanel.Location = new System.Drawing.Point(871, 3);
			this.loginLayoutPanel.Name = "loginLayoutPanel";
			this.loginLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
			this.loginLayoutPanel.Size = new System.Drawing.Size(201, 51);
			this.loginLayoutPanel.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
			this.tableLayoutPanel3.Size = new System.Drawing.Size(201, 51);
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
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(34, 10);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 31);
			this.statusLabel.TabIndex = 1;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 4;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel4.Controls.Add(this.findFlightsButton, 3, 0);
			this.tableLayoutPanel4.Controls.Add(this.fromDepDate, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.toLoc, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.fromLoc, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(230, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(615, 51);
			this.tableLayoutPanel4.TabIndex = 3;
			// 
			// findFlightsButton
			// 
			this.findFlightsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.findFlightsButton.AutoSize = true;
			this.findFlightsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.findFlightsButton.BackColor = System.Drawing.Color.Transparent;
			this.findFlightsButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.findFlightsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.findFlightsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.findFlightsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findFlightsButton.Location = new System.Drawing.Point(524, 13);
			this.findFlightsButton.Name = "findFlightsButton";
			this.findFlightsButton.Size = new System.Drawing.Size(88, 25);
			this.findFlightsButton.TabIndex = 2;
			this.findFlightsButton.Text = "Найти";
			this.findFlightsButton.UseVisualStyleBackColor = false;
			this.findFlightsButton.Click += new System.EventHandler(this.findFlightsButton_Click);
			// 
			// fromDepDate
			// 
			this.fromDepDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fromDepDate.CustomFormat = "d MMM, ddd";
			this.fromDepDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDepDate.Location = new System.Drawing.Point(371, 15);
			this.fromDepDate.Name = "fromDepDate";
			this.fromDepDate.Size = new System.Drawing.Size(147, 20);
			this.fromDepDate.TabIndex = 15;
			// 
			// toLoc
			// 
			this.toLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.toLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.toLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.toLoc.FormattingEnabled = true;
			this.toLoc.Location = new System.Drawing.Point(187, 15);
			this.toLoc.Name = "toLoc";
			this.toLoc.Size = new System.Drawing.Size(178, 21);
			this.toLoc.TabIndex = 14;
			// 
			// fromLoc
			// 
			this.fromLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fromLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.fromLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fromLoc.FormattingEnabled = true;
			this.fromLoc.Location = new System.Drawing.Point(3, 15);
			this.fromLoc.Name = "fromLoc";
			this.fromLoc.Size = new System.Drawing.Size(178, 21);
			this.fromLoc.TabIndex = 13;
			// 
			// flightsTable
			// 
			this.flightsTable.AutoScroll = true;
			this.flightsTable.ColumnCount = 1;
			this.flightsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.flightsTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flightsTable.Location = new System.Drawing.Point(0, 57);
			this.flightsTable.Name = "flightsTable";
			this.flightsTable.Padding = new System.Windows.Forms.Padding(10);
			this.flightsTable.RowCount = 1;
			this.flightsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.flightsTable.Size = new System.Drawing.Size(1075, 393);
			this.flightsTable.TabIndex = 4;
			this.flightsTable.Paint += new System.Windows.Forms.PaintEventHandler(this.flightsTable_Paint);
			// 
			// SelectFlight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(1075, 450);
			this.Controls.Add(this.flightsTable);
			this.Controls.Add(tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(400, 39);
			this.Name = "SelectFlight";
			this.Text = "SelectFlight";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button findFlightsButton;
	}
}