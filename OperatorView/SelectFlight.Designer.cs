
using System.Drawing;

namespace OperatorView {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFlight));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.loginLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.findFlightsButton = new System.Windows.Forms.Button();
			this.fromDepDate = new System.Windows.Forms.DateTimePicker();
			this.toLoc = new Common.CityComboBox();
			this.fromLoc = new Common.CityComboBox();
			this.flightsTable = new System.Windows.Forms.TableLayoutPanel();
			this.elementHint = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Controls.Add(this.loginLayoutPanel, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1096, 51);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// loginLayoutPanel
			// 
			this.loginLayoutPanel.AutoSize = true;
			this.loginLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.loginLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.loginLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.loginLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loginLayoutPanel.Location = new System.Drawing.Point(884, 0);
			this.loginLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.loginLayoutPanel.Name = "loginLayoutPanel";
			this.loginLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 10, 10, 10);
			this.loginLayoutPanel.Size = new System.Drawing.Size(212, 51);
			this.loginLayoutPanel.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.statusLabel, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(211, 51);
			this.tableLayoutPanel3.TabIndex = 2;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(10, 13);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(15, 25);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.AutoEllipsis = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(35, 10);
			this.statusLabel.Margin = new System.Windows.Forms.Padding(0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(176, 31);
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
			this.tableLayoutPanel4.Location = new System.Drawing.Point(231, 0);
			this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(633, 51);
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
			this.findFlightsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.findFlightsButton.Location = new System.Drawing.Point(539, 12);
			this.findFlightsButton.Name = "findFlightsButton";
			this.findFlightsButton.Size = new System.Drawing.Size(91, 27);
			this.findFlightsButton.TabIndex = 2;
			this.findFlightsButton.Text = "Найти";
			this.findFlightsButton.UseVisualStyleBackColor = false;
			this.findFlightsButton.Click += new System.EventHandler(this.findFlightsButton_Click);
			// 
			// fromDepDate
			// 
			this.fromDepDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fromDepDate.CustomFormat = "d MMM, ddd";
			this.fromDepDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromDepDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDepDate.Location = new System.Drawing.Point(381, 14);
			this.fromDepDate.Name = "fromDepDate";
			this.fromDepDate.Size = new System.Drawing.Size(152, 23);
			this.fromDepDate.TabIndex = 15;
			// 
			// toLoc
			// 
			this.toLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.toLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.toLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.toLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toLoc.FormattingEnabled = true;
			this.toLoc.Location = new System.Drawing.Point(192, 13);
			this.toLoc.Name = "toLoc";
			this.toLoc.Size = new System.Drawing.Size(183, 24);
			this.toLoc.TabIndex = 14;
			// 
			// fromLoc
			// 
			this.fromLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.fromLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.fromLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fromLoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromLoc.FormattingEnabled = true;
			this.fromLoc.Location = new System.Drawing.Point(3, 13);
			this.fromLoc.Name = "fromLoc";
			this.fromLoc.Size = new System.Drawing.Size(183, 24);
			this.fromLoc.TabIndex = 13;
			// 
			// flightsTable
			// 
			this.flightsTable.AutoScroll = true;
			this.flightsTable.ColumnCount = 1;
			this.flightsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.flightsTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flightsTable.Location = new System.Drawing.Point(0, 51);
			this.flightsTable.Name = "flightsTable";
			this.flightsTable.Padding = new System.Windows.Forms.Padding(10);
			this.flightsTable.RowCount = 1;
			this.flightsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.flightsTable.Size = new System.Drawing.Size(1096, 399);
			this.flightsTable.TabIndex = 4;
			this.flightsTable.Paint += new System.Windows.Forms.PaintEventHandler(this.flightsTable_Paint);
			// 
			// SelectFlight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(1096, 450);
			this.Controls.Add(this.flightsTable);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(400, 39);
			this.Name = "SelectFlight";
			this.Text = "Выбор рейса";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel loginLayoutPanel;
		private System.Windows.Forms.DateTimePicker fromDepDate;
		private Common.CityComboBox toLoc;
		private Common.CityComboBox fromLoc;
		private System.Windows.Forms.ToolTip elementHint;
		private System.Windows.Forms.TableLayoutPanel flightsTable;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button findFlightsButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}