
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.statusLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.fromDepDate = new System.Windows.Forms.DateTimePicker();
			this.toLoc = new Client.SelectFlight.CityComboBox();
			this.fromLoc = new Client.SelectFlight.CityComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.adultCount = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.childrenCount = new System.Windows.Forms.NumericUpDown();
			this.babyCount = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.classSelector = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.findFlightsButton = new System.Windows.Forms.Button();
			this.elementHint = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.adultCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.childrenCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.babyCount)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(495, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(486, 51);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 57);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoScroll = true;
			this.flowLayoutPanel2.Controls.Add(this.statusLabel);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
			this.flowLayoutPanel2.Size = new System.Drawing.Size(486, 51);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Location = new System.Drawing.Point(13, 10);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 13);
			this.statusLabel.TabIndex = 0;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label8, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label7, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.label6, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.fromDepDate, 2, 3);
			this.tableLayoutPanel2.Controls.Add(this.toLoc, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.fromLoc, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.label2, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.adultCount, 2, 6);
			this.tableLayoutPanel2.Controls.Add(this.label3, 1, 7);
			this.tableLayoutPanel2.Controls.Add(this.label4, 1, 8);
			this.tableLayoutPanel2.Controls.Add(this.childrenCount, 2, 7);
			this.tableLayoutPanel2.Controls.Add(this.babyCount, 2, 8);
			this.tableLayoutPanel2.Controls.Add(this.label10, 1, 10);
			this.tableLayoutPanel2.Controls.Add(this.classSelector, 2, 10);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 2, 11);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 57);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel2.RowCount = 12;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(296, 393);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.label1, 3);
			this.label1.Location = new System.Drawing.Point(8, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Время и место отправления";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(19, 72);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(63, 26);
			this.label8.TabIndex = 19;
			this.label8.Text = "Когда:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(19, 45);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 27);
			this.label7.TabIndex = 18;
			this.label7.Text = "Куда:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(19, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 27);
			this.label6.TabIndex = 17;
			this.label6.Text = "Откуда:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fromDepDate
			// 
			this.fromDepDate.CustomFormat = "d MMMM, dddd";
			this.fromDepDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromDepDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fromDepDate.Location = new System.Drawing.Point(88, 75);
			this.fromDepDate.Name = "fromDepDate";
			this.fromDepDate.Size = new System.Drawing.Size(200, 20);
			this.fromDepDate.TabIndex = 15;
			// 
			// toLoc
			// 
			this.toLoc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.toLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.toLoc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.toLoc.FormattingEnabled = true;
			this.toLoc.Location = new System.Drawing.Point(88, 48);
			this.toLoc.Name = "toLoc";
			this.toLoc.Size = new System.Drawing.Size(200, 21);
			this.toLoc.TabIndex = 14;
			// 
			// fromLoc
			// 
			this.fromLoc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.fromLoc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.fromLoc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.fromLoc.FormattingEnabled = true;
			this.fromLoc.Location = new System.Drawing.Point(88, 21);
			this.fromLoc.Name = "fromLoc";
			this.fromLoc.Size = new System.Drawing.Size(200, 21);
			this.fromLoc.TabIndex = 13;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.label5, 3);
			this.label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(8, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(280, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Пассажиры и класс";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(19, 111);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 26);
			this.label2.TabIndex = 22;
			this.label2.Text = "Взрослые:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// adultCount
			// 
			this.adultCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.adultCount.Location = new System.Drawing.Point(88, 114);
			this.adultCount.Name = "adultCount";
			this.adultCount.Size = new System.Drawing.Size(200, 20);
			this.adultCount.TabIndex = 23;
			this.adultCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(19, 137);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 26);
			this.label3.TabIndex = 24;
			this.label3.Text = "Дети:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(19, 163);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 26);
			this.label4.TabIndex = 25;
			this.label4.Text = "Младенцы:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// childrenCount
			// 
			this.childrenCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.childrenCount.Location = new System.Drawing.Point(88, 140);
			this.childrenCount.Name = "childrenCount";
			this.childrenCount.Size = new System.Drawing.Size(200, 20);
			this.childrenCount.TabIndex = 26;
			// 
			// babyCount
			// 
			this.babyCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.babyCount.Location = new System.Drawing.Point(88, 166);
			this.babyCount.Name = "babyCount";
			this.babyCount.Size = new System.Drawing.Size(200, 20);
			this.babyCount.TabIndex = 27;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label10.Location = new System.Drawing.Point(19, 189);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(63, 27);
			this.label10.TabIndex = 28;
			this.label10.Text = "Класс:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// classSelector
			// 
			this.classSelector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.classSelector.FormattingEnabled = true;
			this.classSelector.Location = new System.Drawing.Point(88, 192);
			this.classSelector.Name = "classSelector";
			this.classSelector.Size = new System.Drawing.Size(200, 21);
			this.classSelector.TabIndex = 29;
			this.classSelector.SelectedIndexChanged += new System.EventHandler(this.classSelector_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.findFlightsButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(88, 360);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 25);
			this.panel1.TabIndex = 30;
			// 
			// findFlightsButton
			// 
			this.findFlightsButton.BackColor = System.Drawing.Color.Transparent;
			this.findFlightsButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.findFlightsButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.findFlightsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.findFlightsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.findFlightsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findFlightsButton.Location = new System.Drawing.Point(125, 0);
			this.findFlightsButton.Name = "findFlightsButton";
			this.findFlightsButton.Size = new System.Drawing.Size(75, 25);
			this.findFlightsButton.TabIndex = 0;
			this.findFlightsButton.Text = "Найти";
			this.findFlightsButton.UseVisualStyleBackColor = false;
			this.findFlightsButton.Click += new System.EventHandler(this.findFlightsButton_Click);
			// 
			// SelectFlight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(984, 450);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(400, 39);
			this.Name = "SelectFlight";
			this.Text = "SelectFlight";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.adultCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.childrenCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.babyCount)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker fromDepDate;
		private CityComboBox toLoc;
		private CityComboBox fromLoc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown adultCount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown childrenCount;
		private System.Windows.Forms.NumericUpDown babyCount;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox classSelector;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip elementHint;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button findFlightsButton;
	}
}