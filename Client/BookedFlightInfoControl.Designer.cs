
namespace Client {
    partial class BookedFlightInfoControl {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flightNameLabel = new System.Windows.Forms.Label();
            this.departureLocationLabel = new System.Windows.Forms.Label();
            this.airplaneNameLabel = new System.Windows.Forms.Label();
            this.departireDatetimeLabel = new System.Windows.Forms.Label();
            this.bookedSeatsCountLabel = new System.Windows.Forms.Label();
            this.proceedButton = new System.Windows.Forms.Button();
            this.arrivalLocationLabel = new System.Windows.Forms.Label();
            this.arrivalDatetimeLabel = new System.Windows.Forms.Label();
            this.bookingFinishedTimeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flightNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.departureLocationLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.airplaneNameLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.departireDatetimeLabel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.bookedSeatsCountLabel, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.proceedButton, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.arrivalLocationLabel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.arrivalDatetimeLabel, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.bookingFinishedTimeLabel, 6, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // flightNameLabel
            //
            this.flightNameLabel.AutoSize = true;
            this.flightNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flightNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flightNameLabel.Location = new System.Drawing.Point(10, 10);
            this.flightNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.flightNameLabel.Name = "flightNameLabel";
            this.flightNameLabel.Size = new System.Drawing.Size(95, 15);
            this.flightNameLabel.TabIndex = 0;
            this.flightNameLabel.Text = "flightNameLabel";
            this.flightNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // departureLocationLabel
            //
            this.departureLocationLabel.AutoSize = true;
            this.departureLocationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.departureLocationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.departureLocationLabel.Location = new System.Drawing.Point(111, 10);
            this.departureLocationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.departureLocationLabel.Name = "departureLocationLabel";
            this.departureLocationLabel.Size = new System.Drawing.Size(104, 15);
            this.departureLocationLabel.TabIndex = 1;
            this.departureLocationLabel.Text = "departureLocation";
            this.departureLocationLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // airplaneNameLabel
            //
            this.airplaneNameLabel.AutoSize = true;
            this.airplaneNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.airplaneNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.airplaneNameLabel.Location = new System.Drawing.Point(10, 31);
            this.airplaneNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.airplaneNameLabel.Name = "airplaneNameLabel";
            this.airplaneNameLabel.Size = new System.Drawing.Size(95, 15);
            this.airplaneNameLabel.TabIndex = 2;
            this.airplaneNameLabel.Text = "airplaneName";
            //
            // departireDatetimeLabel
            //
            this.departireDatetimeLabel.AutoSize = true;
            this.departireDatetimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.departireDatetimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.departireDatetimeLabel.Location = new System.Drawing.Point(111, 31);
            this.departireDatetimeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.departireDatetimeLabel.Name = "departireDatetimeLabel";
            this.departireDatetimeLabel.Size = new System.Drawing.Size(104, 15);
            this.departireDatetimeLabel.TabIndex = 3;
            this.departireDatetimeLabel.Text = "departireDatetime";
            //
            // bookedSeatsCountLabel
            //
            this.bookedSeatsCountLabel.AutoSize = true;
            this.bookedSeatsCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookedSeatsCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bookedSeatsCountLabel.Location = new System.Drawing.Point(318, 10);
            this.bookedSeatsCountLabel.Margin = new System.Windows.Forms.Padding(0);
            this.bookedSeatsCountLabel.Name = "bookedSeatsCountLabel";
            this.bookedSeatsCountLabel.Size = new System.Drawing.Size(127, 15);
            this.bookedSeatsCountLabel.TabIndex = 4;
            this.bookedSeatsCountLabel.Text = "bookedSeatsCount";
            this.bookedSeatsCountLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // proceedButton
            //
            this.proceedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.proceedButton.AutoSize = true;
            this.proceedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.proceedButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.proceedButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.proceedButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.proceedButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.proceedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.proceedButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.proceedButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.proceedButton.Location = new System.Drawing.Point(454, 13);
            this.proceedButton.Name = "proceedButton";
            this.tableLayoutPanel1.SetRowSpan(this.proceedButton, 3);
            this.proceedButton.Size = new System.Drawing.Size(93, 27);
            this.proceedButton.TabIndex = 5;
            this.proceedButton.Text = "Просмотреть";
            this.proceedButton.UseVisualStyleBackColor = false;
            this.proceedButton.Click += new System.EventHandler(this.proceedButton_Click);
            //
            // arrivalLocationLabel
            //
            this.arrivalLocationLabel.AutoSize = true;
            this.arrivalLocationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arrivalLocationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.arrivalLocationLabel.Location = new System.Drawing.Point(221, 10);
            this.arrivalLocationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.arrivalLocationLabel.Name = "arrivalLocationLabel";
            this.arrivalLocationLabel.Size = new System.Drawing.Size(87, 15);
            this.arrivalLocationLabel.TabIndex = 6;
            this.arrivalLocationLabel.Text = "arrivalLocation";
            this.arrivalLocationLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // arrivalDatetimeLabel
            //
            this.arrivalDatetimeLabel.AutoSize = true;
            this.arrivalDatetimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arrivalDatetimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.arrivalDatetimeLabel.Location = new System.Drawing.Point(221, 31);
            this.arrivalDatetimeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.arrivalDatetimeLabel.Name = "arrivalDatetimeLabel";
            this.arrivalDatetimeLabel.Size = new System.Drawing.Size(87, 15);
            this.arrivalDatetimeLabel.TabIndex = 7;
            this.arrivalDatetimeLabel.Text = "arrivalDatetime";
            //
            // bookingFinishedTimeLabel
            //
            this.bookingFinishedTimeLabel.AutoSize = true;
            this.bookingFinishedTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookingFinishedTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bookingFinishedTimeLabel.Location = new System.Drawing.Point(321, 31);
            this.bookingFinishedTimeLabel.Name = "bookingFinishedTimeLabel";
            this.bookingFinishedTimeLabel.Size = new System.Drawing.Size(121, 15);
            this.bookingFinishedTimeLabel.TabIndex = 8;
            this.bookingFinishedTimeLabel.Text = "bookingFinishedTime";
            //
            // BookedFlightInfoControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BookedFlightInfoControl";
            this.Size = new System.Drawing.Size(560, 56);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label flightNameLabel;
        private System.Windows.Forms.Label departureLocationLabel;
        private System.Windows.Forms.Label airplaneNameLabel;
        private System.Windows.Forms.Label departireDatetimeLabel;
        private System.Windows.Forms.Label bookedSeatsCountLabel;
        private System.Windows.Forms.Button proceedButton;
        private System.Windows.Forms.Label arrivalLocationLabel;
        private System.Windows.Forms.Label arrivalDatetimeLabel;
        private System.Windows.Forms.Label bookingFinishedTimeLabel;
    }
}
