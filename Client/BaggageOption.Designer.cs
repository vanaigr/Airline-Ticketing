
namespace Client {
	partial class BaggageOption {
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
			this.mainTable = new System.Windows.Forms.TableLayoutPanel();
			this.mainParamLabel = new System.Windows.Forms.Label();
			this.axilParamLabel = new System.Windows.Forms.Label();
			this.priceLabel = new System.Windows.Forms.Label();
			this.touchArea = new Common.TransparentPanel();
			this.mainTable.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTable
			// 
			this.mainTable.AutoSize = true;
			this.mainTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.mainTable.BackColor = System.Drawing.Color.Transparent;
			this.mainTable.ColumnCount = 3;
			this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.mainTable.Controls.Add(this.mainParamLabel, 1, 1);
			this.mainTable.Controls.Add(this.axilParamLabel, 1, 2);
			this.mainTable.Controls.Add(this.priceLabel, 1, 4);
			this.mainTable.Location = new System.Drawing.Point(0, 0);
			this.mainTable.Margin = new System.Windows.Forms.Padding(0);
			this.mainTable.Name = "mainTable";
			this.mainTable.Padding = new System.Windows.Forms.Padding(3);
			this.mainTable.RowCount = 6;
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.mainTable.Size = new System.Drawing.Size(85, 60);
			this.mainTable.TabIndex = 0;
			// 
			// mainParamLabel
			// 
			this.mainParamLabel.AutoSize = true;
			this.mainParamLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainParamLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mainParamLabel.Location = new System.Drawing.Point(6, 6);
			this.mainParamLabel.Margin = new System.Windows.Forms.Padding(0);
			this.mainParamLabel.Name = "mainParamLabel";
			this.mainParamLabel.Size = new System.Drawing.Size(73, 17);
			this.mainParamLabel.TabIndex = 0;
			this.mainParamLabel.Text = "mainParam";
			this.mainParamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// axilParamLabel
			// 
			this.axilParamLabel.AutoSize = true;
			this.axilParamLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axilParamLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.axilParamLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.axilParamLabel.Location = new System.Drawing.Point(6, 23);
			this.axilParamLabel.Margin = new System.Windows.Forms.Padding(0);
			this.axilParamLabel.Name = "axilParamLabel";
			this.axilParamLabel.Size = new System.Drawing.Size(73, 13);
			this.axilParamLabel.TabIndex = 1;
			this.axilParamLabel.Text = "axilParam";
			this.axilParamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// priceLabel
			// 
			this.priceLabel.AutoSize = true;
			this.priceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.priceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.priceLabel.Location = new System.Drawing.Point(6, 39);
			this.priceLabel.Margin = new System.Windows.Forms.Padding(0);
			this.priceLabel.Name = "priceLabel";
			this.priceLabel.Size = new System.Drawing.Size(73, 15);
			this.priceLabel.TabIndex = 2;
			this.priceLabel.Text = "price";
			this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// touchArea
			// 
			this.touchArea.AutoSize = true;
			this.touchArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.touchArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.touchArea.Location = new System.Drawing.Point(0, 0);
			this.touchArea.Name = "touchArea";
			this.touchArea.Size = new System.Drawing.Size(85, 60);
			this.touchArea.TabIndex = 3;
			// 
			// BaggageOption
			// 
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.touchArea);
			this.Controls.Add(this.mainTable);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "BaggageOption";
			this.Size = new System.Drawing.Size(85, 60);
			this.mainTable.ResumeLayout(false);
			this.mainTable.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel mainTable;
		private System.Windows.Forms.Label mainParamLabel;
		private System.Windows.Forms.Label axilParamLabel;
		private System.Windows.Forms.Label priceLabel;
		private Common.TransparentPanel touchArea;
	}
}
