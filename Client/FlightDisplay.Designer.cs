
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
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            this.fromCityCode = new System.Windows.Forms.Label();
            this.fromTime = new System.Windows.Forms.Label();
            this.fromDate = new System.Windows.Forms.Label();
            this.toCityCode = new System.Windows.Forms.Label();
            this.toTime = new System.Windows.Forms.Label();
            this.toDate = new System.Windows.Forms.Label();
            this.flightTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.termsOptionsTable = new System.Windows.Forms.TableLayoutPanel();
            this.servicesOptionsTable = new System.Windows.Forms.TableLayoutPanel();
            this.baggageOptionsTable = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.availableSeatsCount = new System.Windows.Forms.Label();
            this.classType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flightNameLabel = new System.Windows.Forms.Label();
            this.airplaneNameLabel = new System.Windows.Forms.Label();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel3
            //
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            tableLayoutPanel3.ColumnCount = 5;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 2);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 4, 2);
            tableLayoutPanel3.Controls.Add(this.label4, 2, 0);
            tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 4, 0);
            tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(10);
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new System.Drawing.Size(1099, 143);
            tableLayoutPanel3.TabIndex = 1;
            //
            // tableLayoutPanel4
            //
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel4.Controls.Add(this.fromCityCode, 2, 0);
            tableLayoutPanel4.Controls.Add(this.fromTime, 0, 0);
            tableLayoutPanel4.Controls.Add(this.fromDate, 1, 0);
            tableLayoutPanel4.Controls.Add(this.toCityCode, 2, 2);
            tableLayoutPanel4.Controls.Add(this.toTime, 0, 2);
            tableLayoutPanel4.Controls.Add(this.toDate, 1, 2);
            tableLayoutPanel4.Controls.Add(this.flightTime, 0, 1);
            tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel4.Location = new System.Drawing.Point(13, 47);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel4.Size = new System.Drawing.Size(205, 83);
            tableLayoutPanel4.TabIndex = 0;
            //
            // fromCityCode
            //
            this.fromCityCode.AutoSize = true;
            this.fromCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromCityCode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromCityCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fromCityCode.Location = new System.Drawing.Point(128, 0);
            this.fromCityCode.Margin = new System.Windows.Forms.Padding(0);
            this.fromCityCode.Name = "fromCityCode";
            this.fromCityCode.Size = new System.Drawing.Size(77, 27);
            this.fromCityCode.TabIndex = 10;
            this.fromCityCode.Text = "fromCityCode";
            this.fromCityCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // fromTime
            //
            this.fromTime.AutoSize = true;
            this.fromTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromTime.Location = new System.Drawing.Point(0, 0);
            this.fromTime.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.fromTime.Name = "fromTime";
            this.fromTime.Size = new System.Drawing.Size(67, 27);
            this.fromTime.TabIndex = 8;
            this.fromTime.Text = "fromTime";
            this.fromTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // fromDate
            //
            this.fromDate.AutoSize = true;
            this.fromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromDate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fromDate.Location = new System.Drawing.Point(70, 0);
            this.fromDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(55, 27);
            this.fromDate.TabIndex = 14;
            this.fromDate.Text = "fromDate";
            this.fromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // toCityCode
            //
            this.toCityCode.AutoSize = true;
            this.toCityCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toCityCode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toCityCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toCityCode.Location = new System.Drawing.Point(128, 54);
            this.toCityCode.Margin = new System.Windows.Forms.Padding(0);
            this.toCityCode.Name = "toCityCode";
            this.toCityCode.Size = new System.Drawing.Size(77, 29);
            this.toCityCode.TabIndex = 11;
            this.toCityCode.Text = "toCicyCode";
            this.toCityCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // toTime
            //
            this.toTime.AutoSize = true;
            this.toTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toTime.Location = new System.Drawing.Point(0, 54);
            this.toTime.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toTime.Name = "toTime";
            this.toTime.Size = new System.Drawing.Size(67, 29);
            this.toTime.TabIndex = 9;
            this.toTime.Text = "toTime";
            this.toTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // toDate
            //
            this.toDate.AutoSize = true;
            this.toDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toDate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toDate.Location = new System.Drawing.Point(70, 54);
            this.toDate.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(55, 29);
            this.toDate.TabIndex = 12;
            this.toDate.Text = "toDate";
            this.toDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // flightTime
            //
            tableLayoutPanel4.SetColumnSpan(this.flightTime, 3);
            this.flightTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flightTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flightTime.Location = new System.Drawing.Point(3, 27);
            this.flightTime.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.flightTime.Name = "flightTime";
            this.flightTime.Size = new System.Drawing.Size(202, 27);
            this.flightTime.TabIndex = 13;
            this.flightTime.Text = "flightTime";
            this.flightTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            tableLayoutPanel1.Controls.Add(this.termsOptionsTable, 1, 1);
            tableLayoutPanel1.Controls.Add(this.servicesOptionsTable, 2, 1);
            tableLayoutPanel1.Controls.Add(this.baggageOptionsTable, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(242, 44);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(847, 89);
            tableLayoutPanel1.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Багаж";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(282, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Условия";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(564, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Услуги";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // termsOptionsTable
            //
            this.termsOptionsTable.AutoSize = true;
            this.termsOptionsTable.ColumnCount = 1;
            this.termsOptionsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 281F));
            this.termsOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.termsOptionsTable.Location = new System.Drawing.Point(282, 15);
            this.termsOptionsTable.Margin = new System.Windows.Forms.Padding(0);
            this.termsOptionsTable.Name = "termsOptionsTable";
            this.termsOptionsTable.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.termsOptionsTable.RowCount = 1;
            this.termsOptionsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.termsOptionsTable.Size = new System.Drawing.Size(282, 74);
            this.termsOptionsTable.TabIndex = 4;
            //
            // servicesOptionsTable
            //
            this.servicesOptionsTable.AutoSize = true;
            this.servicesOptionsTable.ColumnCount = 1;
            this.servicesOptionsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 282F));
            this.servicesOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicesOptionsTable.Location = new System.Drawing.Point(564, 15);
            this.servicesOptionsTable.Margin = new System.Windows.Forms.Padding(0);
            this.servicesOptionsTable.Name = "servicesOptionsTable";
            this.servicesOptionsTable.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.servicesOptionsTable.RowCount = 1;
            this.servicesOptionsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.servicesOptionsTable.Size = new System.Drawing.Size(283, 74);
            this.servicesOptionsTable.TabIndex = 5;
            //
            // baggageOptionsTable
            //
            this.baggageOptionsTable.AutoSize = true;
            this.baggageOptionsTable.ColumnCount = 1;
            this.baggageOptionsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 281F));
            this.baggageOptionsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baggageOptionsTable.Location = new System.Drawing.Point(0, 15);
            this.baggageOptionsTable.Margin = new System.Windows.Forms.Padding(0);
            this.baggageOptionsTable.Name = "baggageOptionsTable";
            this.baggageOptionsTable.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.baggageOptionsTable.RowCount = 1;
            this.baggageOptionsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.baggageOptionsTable.Size = new System.Drawing.Size(282, 74);
            this.baggageOptionsTable.TabIndex = 3;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(234, 10);
            this.label4.Name = "label4";
            tableLayoutPanel3.SetRowSpan(this.label4, 3);
            this.label4.Size = new System.Drawing.Size(1, 123);
            this.label4.TabIndex = 2;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.availableSeatsCount, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.classType, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.button1, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(242, 10);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(847, 27);
            this.tableLayoutPanel2.TabIndex = 4;
            //
            // availableSeatsCount
            //
            this.availableSeatsCount.AutoSize = true;
            this.availableSeatsCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableSeatsCount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.availableSeatsCount.Location = new System.Drawing.Point(113, 2);
            this.availableSeatsCount.Name = "availableSeatsCount";
            this.availableSeatsCount.Size = new System.Drawing.Size(113, 23);
            this.availableSeatsCount.TabIndex = 2;
            this.availableSeatsCount.Text = "availableSeatsCount";
            this.availableSeatsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // classType
            //
            this.classType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.classType.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.classType.Location = new System.Drawing.Point(0, 2);
            this.classType.Margin = new System.Windows.Forms.Padding(0);
            this.classType.Name = "classType";
            this.classType.Size = new System.Drawing.Size(100, 21);
            this.classType.TabIndex = 1;
            this.classType.SelectedIndexChanged += new System.EventHandler(this.classType_SelectedIndexChanged);
            //
            // button1
            //
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(758, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.tableLayoutPanel2.SetRowSpan(this.button1, 3);
            this.button1.Size = new System.Drawing.Size(89, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "Продолжить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // flowLayoutPanel1
            //
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.ColumnCount = 2;
            this.flowLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel1.Controls.Add(this.flightNameLabel, 0, 0);
            this.flowLayoutPanel1.Controls.Add(this.airplaneNameLabel, 1, 0);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 10);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RowCount = 1;
            this.flowLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.flowLayoutPanel1.Size = new System.Drawing.Size(208, 24);
            this.flowLayoutPanel1.TabIndex = 5;
            //
            // flightNameLabel
            //
            this.flightNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flightNameLabel.AutoSize = true;
            this.flightNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flightNameLabel.Location = new System.Drawing.Point(0, 4);
            this.flightNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.flightNameLabel.Name = "flightNameLabel";
            this.flightNameLabel.Size = new System.Drawing.Size(67, 15);
            this.flightNameLabel.TabIndex = 1;
            this.flightNameLabel.Text = "flightName";
            this.flightNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // airplaneNameLabel
            //
            this.airplaneNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.airplaneNameLabel.AutoSize = true;
            this.airplaneNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.airplaneNameLabel.Location = new System.Drawing.Point(70, 4);
            this.airplaneNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.airplaneNameLabel.Name = "airplaneNameLabel";
            this.airplaneNameLabel.Size = new System.Drawing.Size(81, 15);
            this.airplaneNameLabel.TabIndex = 2;
            this.airplaneNameLabel.Text = "airplaneName";
            this.airplaneNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // FlightDisplay
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(tableLayoutPanel3);
            this.MinimumSize = new System.Drawing.Size(750, 100);
            this.Name = "FlightDisplay";
            this.Size = new System.Drawing.Size(1099, 143);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.Label flightTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label availableSeatsCount;
        private System.Windows.Forms.TableLayoutPanel baggageOptionsTable;
        private System.Windows.Forms.TableLayoutPanel termsOptionsTable;
        private System.Windows.Forms.TableLayoutPanel servicesOptionsTable;
        private System.Windows.Forms.ComboBox classType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label flightNameLabel;
        private System.Windows.Forms.Label airplaneNameLabel;
    }
}
