
namespace Operator {
	partial class FlightDisplay {
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
			this.fromTime = new System.Windows.Forms.Label();
			this.toTime = new System.Windows.Forms.Label();
			this.fromCityCode = new System.Windows.Forms.Label();
			this.fromDate = new System.Windows.Forms.Label();
			this.toCityCode = new System.Windows.Forms.Label();
			this.toDate = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.flightNameLabel = new System.Windows.Forms.Label();
			this.airplaneNameLabel = new System.Windows.Forms.Label();
			this.economyClassSeatsLabel = new System.Windows.Forms.Label();
			this.comfortClassSeatsLabel = new System.Windows.Forms.Label();
			this.businessClassSeatsLabel = new System.Windows.Forms.Label();
			this.firstClassSeatsLabel = new System.Windows.Forms.Label();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.AutoSize = true;
			tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
			tableLayoutPanel3.ColumnCount = 7;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel3.Controls.Add(this.fromTime, 0, 2);
			tableLayoutPanel3.Controls.Add(this.toTime, 0, 4);
			tableLayoutPanel3.Controls.Add(this.fromCityCode, 2, 2);
			tableLayoutPanel3.Controls.Add(this.fromDate, 1, 2);
			tableLayoutPanel3.Controls.Add(this.toCityCode, 2, 4);
			tableLayoutPanel3.Controls.Add(this.toDate, 1, 4);
			tableLayoutPanel3.Controls.Add(this.button1, 6, 0);
			tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
			tableLayoutPanel3.Controls.Add(this.economyClassSeatsLabel, 4, 2);
			tableLayoutPanel3.Controls.Add(this.comfortClassSeatsLabel, 6, 2);
			tableLayoutPanel3.Controls.Add(this.businessClassSeatsLabel, 4, 4);
			tableLayoutPanel3.Controls.Add(this.firstClassSeatsLabel, 6, 4);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10);
			tableLayoutPanel3.RowCount = 5;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.Size = new System.Drawing.Size(750, 100);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// fromTime
			// 
			this.fromTime.AutoSize = true;
			this.fromTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromTime.Location = new System.Drawing.Point(10, 43);
			this.fromTime.Margin = new System.Windows.Forms.Padding(0);
			this.fromTime.Name = "fromTime";
			this.fromTime.Size = new System.Drawing.Size(67, 19);
			this.fromTime.TabIndex = 8;
			this.fromTime.Text = "fromTime";
			this.fromTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toTime
			// 
			this.toTime.AutoSize = true;
			this.toTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toTime.Location = new System.Drawing.Point(10, 65);
			this.toTime.Margin = new System.Windows.Forms.Padding(0);
			this.toTime.Name = "toTime";
			this.toTime.Size = new System.Drawing.Size(67, 25);
			this.toTime.TabIndex = 9;
			this.toTime.Text = "toTime";
			this.toTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fromCityCode
			// 
			this.fromCityCode.AutoSize = true;
			this.fromCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromCityCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromCityCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fromCityCode.Location = new System.Drawing.Point(144, 43);
			this.fromCityCode.Margin = new System.Windows.Forms.Padding(0);
			this.fromCityCode.Name = "fromCityCode";
			this.fromCityCode.Size = new System.Drawing.Size(94, 19);
			this.fromCityCode.TabIndex = 10;
			this.fromCityCode.Text = "fromCityCode";
			this.fromCityCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fromDate
			// 
			this.fromDate.AutoSize = true;
			this.fromDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fromDate.Location = new System.Drawing.Point(77, 43);
			this.fromDate.Margin = new System.Windows.Forms.Padding(0);
			this.fromDate.Name = "fromDate";
			this.fromDate.Size = new System.Drawing.Size(67, 19);
			this.fromDate.TabIndex = 14;
			this.fromDate.Text = "fromDate";
			this.fromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toCityCode
			// 
			this.toCityCode.AutoSize = true;
			this.toCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toCityCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toCityCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.toCityCode.Location = new System.Drawing.Point(144, 65);
			this.toCityCode.Margin = new System.Windows.Forms.Padding(0);
			this.toCityCode.Name = "toCityCode";
			this.toCityCode.Size = new System.Drawing.Size(94, 25);
			this.toCityCode.TabIndex = 11;
			this.toCityCode.Text = "toCicyCode";
			this.toCityCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toDate
			// 
			this.toDate.AutoSize = true;
			this.toDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toDate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.toDate.Location = new System.Drawing.Point(77, 65);
			this.toDate.Margin = new System.Windows.Forms.Padding(0);
			this.toDate.Name = "toDate";
			this.toDate.Size = new System.Drawing.Size(67, 25);
			this.toDate.TabIndex = 12;
			this.toDate.Text = "toDate";
			this.toDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.AutoSize = true;
			this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button1.BackColor = System.Drawing.Color.RoyalBlue;
			this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.button1.Location = new System.Drawing.Point(651, 10);
			this.button1.Margin = new System.Windows.Forms.Padding(0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 27);
			this.button1.TabIndex = 3;
			this.button1.Text = "Продолжить";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel3.SetColumnSpan(this.flowLayoutPanel1, 3);
			this.flowLayoutPanel1.Controls.Add(this.flightNameLabel);
			this.flowLayoutPanel1.Controls.Add(this.airplaneNameLabel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 14);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(172, 19);
			this.flowLayoutPanel1.TabIndex = 5;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// flightNameLabel
			// 
			this.flightNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.flightNameLabel.AutoSize = true;
			this.flightNameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.flightNameLabel.Location = new System.Drawing.Point(0, 0);
			this.flightNameLabel.Margin = new System.Windows.Forms.Padding(0);
			this.flightNameLabel.Name = "flightNameLabel";
			this.flightNameLabel.Size = new System.Drawing.Size(76, 19);
			this.flightNameLabel.TabIndex = 1;
			this.flightNameLabel.Text = "flightName";
			this.flightNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// airplaneNameLabel
			// 
			this.airplaneNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.airplaneNameLabel.AutoSize = true;
			this.airplaneNameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.airplaneNameLabel.Location = new System.Drawing.Point(79, 0);
			this.airplaneNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.airplaneNameLabel.Name = "airplaneNameLabel";
			this.airplaneNameLabel.Size = new System.Drawing.Size(93, 19);
			this.airplaneNameLabel.TabIndex = 2;
			this.airplaneNameLabel.Text = "airplaneName";
			this.airplaneNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// economyClassSeatsLabel
			// 
			this.economyClassSeatsLabel.AutoSize = true;
			this.economyClassSeatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.economyClassSeatsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.economyClassSeatsLabel.Location = new System.Drawing.Point(258, 43);
			this.economyClassSeatsLabel.Margin = new System.Windows.Forms.Padding(0);
			this.economyClassSeatsLabel.Name = "economyClassSeatsLabel";
			this.economyClassSeatsLabel.Size = new System.Drawing.Size(239, 19);
			this.economyClassSeatsLabel.TabIndex = 15;
			this.economyClassSeatsLabel.Text = "label1";
			this.economyClassSeatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comfortClassSeatsLabel
			// 
			this.comfortClassSeatsLabel.AutoSize = true;
			this.comfortClassSeatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comfortClassSeatsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comfortClassSeatsLabel.Location = new System.Drawing.Point(500, 43);
			this.comfortClassSeatsLabel.Margin = new System.Windows.Forms.Padding(0);
			this.comfortClassSeatsLabel.Name = "comfortClassSeatsLabel";
			this.comfortClassSeatsLabel.Size = new System.Drawing.Size(240, 19);
			this.comfortClassSeatsLabel.TabIndex = 16;
			this.comfortClassSeatsLabel.Text = "label2";
			this.comfortClassSeatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// businessClassSeatsLabel
			// 
			this.businessClassSeatsLabel.AutoSize = true;
			this.businessClassSeatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.businessClassSeatsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.businessClassSeatsLabel.Location = new System.Drawing.Point(258, 65);
			this.businessClassSeatsLabel.Margin = new System.Windows.Forms.Padding(0);
			this.businessClassSeatsLabel.Name = "businessClassSeatsLabel";
			this.businessClassSeatsLabel.Size = new System.Drawing.Size(239, 25);
			this.businessClassSeatsLabel.TabIndex = 17;
			this.businessClassSeatsLabel.Text = "label3";
			this.businessClassSeatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// firstClassSeatsLabel
			// 
			this.firstClassSeatsLabel.AutoSize = true;
			this.firstClassSeatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.firstClassSeatsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.firstClassSeatsLabel.Location = new System.Drawing.Point(500, 65);
			this.firstClassSeatsLabel.Margin = new System.Windows.Forms.Padding(0);
			this.firstClassSeatsLabel.Name = "firstClassSeatsLabel";
			this.firstClassSeatsLabel.Size = new System.Drawing.Size(240, 25);
			this.firstClassSeatsLabel.TabIndex = 18;
			this.firstClassSeatsLabel.Text = "label4";
			this.firstClassSeatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FlightDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(tableLayoutPanel3);
			this.MinimumSize = new System.Drawing.Size(750, 100);
			this.Name = "FlightDisplay";
			this.Size = new System.Drawing.Size(750, 100);
			this.MouseLeave += new System.EventHandler(this.FlightDisplay_MouseLeave);
			this.MouseHover += new System.EventHandler(this.FlightDisplay_MouseHover);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label fromCityCode;
		private System.Windows.Forms.Label fromTime;
		private System.Windows.Forms.Label fromDate;
		private System.Windows.Forms.Label toCityCode;
		private System.Windows.Forms.Label toTime;
		private System.Windows.Forms.Label toDate;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label flightNameLabel;
		private System.Windows.Forms.Label airplaneNameLabel;
		private System.Windows.Forms.Label economyClassSeatsLabel;
		private System.Windows.Forms.Label comfortClassSeatsLabel;
		private System.Windows.Forms.Label businessClassSeatsLabel;
		private System.Windows.Forms.Label firstClassSeatsLabel;
	}
}
