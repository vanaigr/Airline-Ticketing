
namespace Client {
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
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
			this.fromTime = new System.Windows.Forms.Label();
			this.toTime = new System.Windows.Forms.Label();
			this.fromCityCode = new System.Windows.Forms.Label();
			this.toCityCode = new System.Windows.Forms.Label();
			this.toDate = new System.Windows.Forms.Label();
			this.flightTime = new System.Windows.Forms.Label();
			this.fromDate = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel3.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.ColumnCount = 3;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
			tableLayoutPanel3.Controls.Add(this.label7, 2, 0);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10);
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new System.Drawing.Size(751, 100);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.ColumnCount = 5;
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			tableLayoutPanel4.Controls.Add(this.fromTime, 0, 0);
			tableLayoutPanel4.Controls.Add(this.toTime, 4, 0);
			tableLayoutPanel4.Controls.Add(this.fromCityCode, 1, 0);
			tableLayoutPanel4.Controls.Add(this.toCityCode, 3, 0);
			tableLayoutPanel4.Controls.Add(this.toDate, 4, 2);
			tableLayoutPanel4.Controls.Add(this.flightTime, 1, 2);
			tableLayoutPanel4.Controls.Add(this.fromDate, 0, 2);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel4.Location = new System.Drawing.Point(13, 13);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 3;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel4.Size = new System.Drawing.Size(210, 74);
			tableLayoutPanel4.TabIndex = 0;
			// 
			// fromTime
			// 
			this.fromTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromTime.Location = new System.Drawing.Point(3, 0);
			this.fromTime.Name = "fromTime";
			this.fromTime.Size = new System.Drawing.Size(46, 32);
			this.fromTime.TabIndex = 0;
			this.fromTime.Text = "fromTime";
			this.fromTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// toTime
			// 
			this.toTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toTime.Location = new System.Drawing.Point(159, 0);
			this.toTime.Name = "toTime";
			this.toTime.Size = new System.Drawing.Size(48, 32);
			this.toTime.TabIndex = 1;
			this.toTime.Text = "toTime";
			this.toTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// fromCityCode
			// 
			this.fromCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromCityCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromCityCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.fromCityCode.Location = new System.Drawing.Point(55, 0);
			this.fromCityCode.Name = "fromCityCode";
			this.fromCityCode.Size = new System.Drawing.Size(25, 32);
			this.fromCityCode.TabIndex = 2;
			this.fromCityCode.Text = "fromCityCode";
			this.fromCityCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// toCityCode
			// 
			this.toCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toCityCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toCityCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.toCityCode.Location = new System.Drawing.Point(128, 0);
			this.toCityCode.Name = "toCityCode";
			this.toCityCode.Size = new System.Drawing.Size(25, 32);
			this.toCityCode.TabIndex = 3;
			this.toCityCode.Text = "toCicyCode";
			this.toCityCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// toDate
			// 
			this.toDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.toDate.Location = new System.Drawing.Point(159, 42);
			this.toDate.Name = "toDate";
			this.toDate.Size = new System.Drawing.Size(48, 32);
			this.toDate.TabIndex = 5;
			this.toDate.Text = "toDate";
			this.toDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// flightTime
			// 
			tableLayoutPanel4.SetColumnSpan(this.flightTime, 3);
			this.flightTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flightTime.Location = new System.Drawing.Point(55, 42);
			this.flightTime.Name = "flightTime";
			this.flightTime.Size = new System.Drawing.Size(98, 32);
			this.flightTime.TabIndex = 6;
			this.flightTime.Text = "flightTime";
			this.flightTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// fromDate
			// 
			this.fromDate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.fromDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.fromDate.Location = new System.Drawing.Point(3, 42);
			this.fromDate.Name = "fromDate";
			this.fromDate.Size = new System.Drawing.Size(46, 32);
			this.fromDate.TabIndex = 7;
			this.fromDate.Text = "fromDate";
			this.fromDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label7
			// 
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(239, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(499, 80);
			this.label7.TabIndex = 1;
			this.label7.Text = "КУПИ КУПИ КУПИ";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FlightDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(tableLayoutPanel3);
			this.MinimumSize = new System.Drawing.Size(750, 100);
			this.Name = "FlightDisplay";
			this.Size = new System.Drawing.Size(751, 100);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label fromTime;
		private System.Windows.Forms.Label toTime;
		private System.Windows.Forms.Label fromCityCode;
		private System.Windows.Forms.Label toCityCode;
		private System.Windows.Forms.Label toDate;
		private System.Windows.Forms.Label flightTime;
		private System.Windows.Forms.Label fromDate;
		private System.Windows.Forms.Label label7;
	}
}
