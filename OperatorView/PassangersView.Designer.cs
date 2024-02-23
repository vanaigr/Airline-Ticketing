
namespace Operator {
    partial class PassangersView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            this.updateTimer.Dispose();
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.footerTable2 = new System.Windows.Forms.TableLayoutPanel();
            this.footerTable = new System.Windows.Forms.TableLayoutPanel();
            this.registerDepartureButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusLabel = new System.Windows.Forms.Label();
            this.passangersDataGridView = new System.Windows.Forms.DataGridView();
            this.headerTable = new System.Windows.Forms.TableLayoutPanel();
            this.flightNameLabel = new System.Windows.Forms.Label();
            this.airplaneNameLabel = new System.Windows.Forms.Label();
            this.fromDatetime = new System.Windows.Forms.Label();
            this.fromCityCodeLabel = new System.Windows.Forms.Label();
            this.remainingTimeLabel = new System.Windows.Forms.Label();
            this.showCanceledCheckbox = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.toCityCodeLabel = new System.Windows.Forms.Label();
            this.toCityDatetimeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.footerTable2.SuspendLayout();
            this.footerTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passangersDataGridView)).BeginInit();
            this.headerTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.footerTable2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.passangersDataGridView, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(935, 519);
            this.tableLayoutPanel2.TabIndex = 0;
            //
            // footerTable2
            //
            this.footerTable2.AutoSize = true;
            this.footerTable2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.footerTable2.ColumnCount = 1;
            this.footerTable2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerTable2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.footerTable2.Controls.Add(this.footerTable, 0, 0);
            this.footerTable2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerTable2.Location = new System.Drawing.Point(0, 437);
            this.footerTable2.Margin = new System.Windows.Forms.Padding(0);
            this.footerTable2.Name = "footerTable2";
            this.footerTable2.RowCount = 1;
            this.footerTable2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerTable2.Size = new System.Drawing.Size(935, 82);
            this.footerTable2.TabIndex = 6;
            //
            // footerTable
            //
            this.footerTable.AutoSize = true;
            this.footerTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.footerTable.BackColor = System.Drawing.Color.White;
            this.footerTable.ColumnCount = 5;
            this.footerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.footerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.footerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.footerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.footerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.footerTable.Controls.Add(this.registerDepartureButton, 3, 2);
            this.footerTable.Controls.Add(this.dataGridView1, 1, 2);
            this.footerTable.Controls.Add(this.statusLabel, 1, 1);
            this.footerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerTable.Location = new System.Drawing.Point(0, 0);
            this.footerTable.Margin = new System.Windows.Forms.Padding(0);
            this.footerTable.Name = "footerTable";
            this.footerTable.RowCount = 4;
            this.footerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.footerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.footerTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.footerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.footerTable.Size = new System.Drawing.Size(935, 82);
            this.footerTable.TabIndex = 2;
            //
            // registerDepartureButton
            //
            this.registerDepartureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.registerDepartureButton.AutoSize = true;
            this.registerDepartureButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.registerDepartureButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.registerDepartureButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.registerDepartureButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.registerDepartureButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.registerDepartureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerDepartureButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.registerDepartureButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.registerDepartureButton.Location = new System.Drawing.Point(821, 30);
            this.registerDepartureButton.Margin = new System.Windows.Forms.Padding(0);
            this.registerDepartureButton.Name = "registerDepartureButton";
            this.registerDepartureButton.Size = new System.Drawing.Size(104, 42);
            this.registerDepartureButton.TabIndex = 5;
            this.registerDepartureButton.Text = "Зафиксировать\r\nприбытие";
            this.registerDepartureButton.UseVisualStyleBackColor = false;
            this.registerDepartureButton.Click += new System.EventHandler(this.registerDepartureButton_Click);
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(10, 30);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(805, 42);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            //
            // statusLabel
            //
            this.statusLabel.AutoEllipsis = true;
            this.footerTable.SetColumnSpan(this.statusLabel, 3);
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.Location = new System.Drawing.Point(10, 10);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(915, 17);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // passangersDataGridView
            //
            this.passangersDataGridView.AllowUserToResizeRows = false;
            this.passangersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.passangersDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.passangersDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.passangersDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.passangersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.passangersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passangersDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.passangersDataGridView.Location = new System.Drawing.Point(0, 0);
            this.passangersDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.passangersDataGridView.Name = "passangersDataGridView";
            this.passangersDataGridView.Size = new System.Drawing.Size(935, 437);
            this.passangersDataGridView.TabIndex = 3;
            this.passangersDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.passangersDataGridView_CellFormatting);
            this.passangersDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.passangersDataGridView_CellValueChanged);
            //
            // headerTable
            //
            this.headerTable.AutoScroll = true;
            this.headerTable.AutoSize = true;
            this.headerTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.headerTable.BackColor = System.Drawing.Color.White;
            this.headerTable.ColumnCount = 12;
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.headerTable.Controls.Add(this.flightNameLabel, 1, 1);
            this.headerTable.Controls.Add(this.airplaneNameLabel, 1, 3);
            this.headerTable.Controls.Add(this.remainingTimeLabel, 7, 1);
            this.headerTable.Controls.Add(this.showCanceledCheckbox, 9, 1);
            this.headerTable.Controls.Add(this.toCityDatetimeLabel, 5, 3);
            this.headerTable.Controls.Add(this.fromCityCodeLabel, 3, 1);
            this.headerTable.Controls.Add(this.toCityCodeLabel, 3, 3);
            this.headerTable.Controls.Add(this.fromDatetime, 5, 1);
            this.headerTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.headerTable.Location = new System.Drawing.Point(0, 0);
            this.headerTable.Margin = new System.Windows.Forms.Padding(0);
            this.headerTable.Name = "headerTable";
            this.headerTable.RowCount = 5;
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.headerTable.Size = new System.Drawing.Size(935, 53);
            this.headerTable.TabIndex = 1;
            //
            // flightNameLabel
            //
            this.flightNameLabel.AutoSize = true;
            this.flightNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flightNameLabel.Location = new System.Drawing.Point(13, 10);
            this.flightNameLabel.Name = "flightNameLabel";
            this.flightNameLabel.Size = new System.Drawing.Size(67, 15);
            this.flightNameLabel.TabIndex = 0;
            this.flightNameLabel.Text = "flightName";
            //
            // airplaneNameLabel
            //
            this.airplaneNameLabel.AutoSize = true;
            this.airplaneNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.airplaneNameLabel.Location = new System.Drawing.Point(13, 28);
            this.airplaneNameLabel.Name = "airplaneNameLabel";
            this.airplaneNameLabel.Size = new System.Drawing.Size(81, 15);
            this.airplaneNameLabel.TabIndex = 1;
            this.airplaneNameLabel.Text = "airplaneName";
            //
            // fromDatetime
            //
            this.fromDatetime.AutoSize = true;
            this.fromDatetime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromDatetime.Location = new System.Drawing.Point(194, 10);
            this.fromDatetime.Name = "fromDatetime";
            this.fromDatetime.Size = new System.Drawing.Size(81, 15);
            this.fromDatetime.TabIndex = 2;
            this.fromDatetime.Text = "fromDatetime";
            //
            // fromCityCodeLabel
            //
            this.fromCityCodeLabel.AutoSize = true;
            this.fromCityCodeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromCityCodeLabel.Location = new System.Drawing.Point(103, 10);
            this.fromCityCodeLabel.Name = "fromCityCodeLabel";
            this.fromCityCodeLabel.Size = new System.Drawing.Size(82, 15);
            this.fromCityCodeLabel.TabIndex = 3;
            this.fromCityCodeLabel.Text = "fromCityCode";
            //
            // remainingTimeLabel
            //
            this.remainingTimeLabel.AutoSize = true;
            this.remainingTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.remainingTimeLabel.Location = new System.Drawing.Point(290, 10);
            this.remainingTimeLabel.Name = "remainingTimeLabel";
            this.remainingTimeLabel.Size = new System.Drawing.Size(87, 15);
            this.remainingTimeLabel.TabIndex = 4;
            this.remainingTimeLabel.Text = "remainingTime";
            //
            // showCanceledCheckbox
            //
            this.showCanceledCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showCanceledCheckbox.AutoSize = true;
            this.showCanceledCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showCanceledCheckbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showCanceledCheckbox.Location = new System.Drawing.Point(389, 21);
            this.showCanceledCheckbox.Name = "showCanceledCheckbox";
            this.headerTable.SetRowSpan(this.showCanceledCheckbox, 3);
            this.showCanceledCheckbox.Size = new System.Drawing.Size(160, 19);
            this.showCanceledCheckbox.TabIndex = 5;
            this.showCanceledCheckbox.Text = "Показывать отменённые";
            this.showCanceledCheckbox.UseVisualStyleBackColor = true;
            this.showCanceledCheckbox.CheckedChanged += new System.EventHandler(this.showCanceledCheckbox_CheckedChanged);
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.headerTable);
            this.splitContainer1.Panel1MinSize = 10;
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(935, 576);
            this.splitContainer1.SplitterDistance = 53;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            //
            // toCityCodeLabel
            //
            this.toCityCodeLabel.AutoSize = true;
            this.toCityCodeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toCityCodeLabel.Location = new System.Drawing.Point(103, 28);
            this.toCityCodeLabel.Name = "toCityCodeLabel";
            this.toCityCodeLabel.Size = new System.Drawing.Size(67, 15);
            this.toCityCodeLabel.TabIndex = 6;
            this.toCityCodeLabel.Text = "toCityCode";
            //
            // toCityDatetimeLabel
            //
            this.toCityDatetimeLabel.AutoSize = true;
            this.toCityDatetimeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toCityDatetimeLabel.Location = new System.Drawing.Point(194, 28);
            this.toCityDatetimeLabel.Name = "toCityDatetimeLabel";
            this.toCityDatetimeLabel.Size = new System.Drawing.Size(87, 15);
            this.toCityDatetimeLabel.TabIndex = 7;
            this.toCityDatetimeLabel.Text = "toCityDatetime";
            //
            // PassangersView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 576);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PassangersView";
            this.Text = "Просмотр пассажиов";
            this.Shown += new System.EventHandler(this.PassangersView_Shown);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.footerTable2.ResumeLayout(false);
            this.footerTable2.PerformLayout();
            this.footerTable.ResumeLayout(false);
            this.footerTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passangersDataGridView)).EndInit();
            this.headerTable.ResumeLayout(false);
            this.headerTable.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel footerTable;
        private System.Windows.Forms.Button registerDepartureButton;
        private System.Windows.Forms.TableLayoutPanel headerTable;
        private System.Windows.Forms.Label flightNameLabel;
        private System.Windows.Forms.Label airplaneNameLabel;
        private System.Windows.Forms.Label fromDatetime;
        private System.Windows.Forms.Label fromCityCodeLabel;
        private System.Windows.Forms.Label remainingTimeLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox showCanceledCheckbox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ToolTip statusTooltip;
        private System.Windows.Forms.TableLayoutPanel footerTable2;
        private System.Windows.Forms.DataGridView passangersDataGridView;
        private System.Windows.Forms.Label toCityCodeLabel;
        private System.Windows.Forms.Label toCityDatetimeLabel;
    }
}
