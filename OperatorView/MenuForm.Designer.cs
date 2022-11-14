
namespace Operator {
	partial class MenuForm {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.findFlightsButton = new System.Windows.Forms.Button();
			this.findPassangerButton = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.findFlightsButton, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.findPassangerButton, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.statusLabel, 1, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 148);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// findFlightsButton
			// 
			this.findFlightsButton.AutoSize = true;
			this.findFlightsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.findFlightsButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.findFlightsButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.findFlightsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.findFlightsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.findFlightsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findFlightsButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.findFlightsButton.Location = new System.Drawing.Point(20, 73);
			this.findFlightsButton.Margin = new System.Windows.Forms.Padding(0);
			this.findFlightsButton.Name = "findFlightsButton";
			this.findFlightsButton.Size = new System.Drawing.Size(278, 33);
			this.findFlightsButton.TabIndex = 1;
			this.findFlightsButton.Text = "Найти рейсы";
			this.findFlightsButton.UseVisualStyleBackColor = true;
			this.findFlightsButton.Click += new System.EventHandler(this.findFlightsButton_Click);
			// 
			// findPassangerButton
			// 
			this.findPassangerButton.AutoSize = true;
			this.findPassangerButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.findPassangerButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.findPassangerButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.findPassangerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.findPassangerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.findPassangerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findPassangerButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.findPassangerButton.Location = new System.Drawing.Point(20, 20);
			this.findPassangerButton.Margin = new System.Windows.Forms.Padding(0);
			this.findPassangerButton.Name = "findPassangerButton";
			this.findPassangerButton.Size = new System.Drawing.Size(278, 33);
			this.findPassangerButton.TabIndex = 0;
			this.findPassangerButton.Text = "Найти бронь";
			this.findPassangerButton.UseVisualStyleBackColor = true;
			this.findPassangerButton.Click += new System.EventHandler(this.findPassangerButton_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(23, 126);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(272, 15);
			this.statusLabel.TabIndex = 2;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MenuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(318, 148);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MenuForm";
			this.Text = "Опции";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button findFlightsButton;
		private System.Windows.Forms.Button findPassangerButton;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip statusTooltip;
	}
}