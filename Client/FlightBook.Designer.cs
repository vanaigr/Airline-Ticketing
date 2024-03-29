﻿
namespace Client {
    partial class FlightBook {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightBook));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.passangersSummaryPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bookFlightButton = new System.Windows.Forms.Button();
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.statusLabel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.totalPriceLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.bookFlightButton, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.passangersSummaryPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 313);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // passangersSummaryPanel
            //
            this.passangersSummaryPanel.AutoScroll = true;
            this.passangersSummaryPanel.AutoSize = true;
            this.passangersSummaryPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.passangersSummaryPanel, 5);
            this.passangersSummaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passangersSummaryPanel.Location = new System.Drawing.Point(10, 10);
            this.passangersSummaryPanel.Margin = new System.Windows.Forms.Padding(0);
            this.passangersSummaryPanel.Name = "passangersSummaryPanel";
            this.passangersSummaryPanel.Size = new System.Drawing.Size(550, 256);
            this.passangersSummaryPanel.TabIndex = 0;
            //
            // bookFlightButton
            //
            this.bookFlightButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bookFlightButton.AutoSize = true;
            this.bookFlightButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bookFlightButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.bookFlightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.bookFlightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bookFlightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bookFlightButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bookFlightButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bookFlightButton.Location = new System.Drawing.Point(456, 276);
            this.bookFlightButton.Margin = new System.Windows.Forms.Padding(0);
            this.bookFlightButton.Name = "bookFlightButton";
            this.bookFlightButton.Size = new System.Drawing.Size(104, 27);
            this.bookFlightButton.TabIndex = 2;
            this.bookFlightButton.Text = "Забронировать";
            this.bookFlightButton.UseVisualStyleBackColor = false;
            this.bookFlightButton.Click += new System.EventHandler(this.bookFlightButton_Click);
            //
            // totalPriceLabel
            //
            this.totalPriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.totalPriceLabel.AutoSize = true;
            this.totalPriceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.totalPriceLabel.Location = new System.Drawing.Point(13, 282);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(38, 15);
            this.totalPriceLabel.TabIndex = 3;
            this.totalPriceLabel.Text = "label1";
            //
            // statusLabel
            //
            this.statusLabel.AutoEllipsis = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.Location = new System.Drawing.Point(67, 276);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(376, 27);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // FlightBook
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(570, 313);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FlightBook";
            this.Text = "Бронирование билетов";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bookFlightButton;
        private System.Windows.Forms.Label totalPriceLabel;
        private System.Windows.Forms.ToolTip statusTooltip;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.FlowLayoutPanel passangersSummaryPanel;
    }
}
