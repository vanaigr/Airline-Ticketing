
namespace ClientCommunication {
	partial class PassangerSettings {
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
			this.mainTabs = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.passangerUpdate = new ClientCommunication.PassangerList();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.passangerOptions = new ClientCommunication.PassangerOptions();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.seatSelect = new System.Windows.Forms.CheckBox();
			this.seatPositionTextbox = new System.Windows.Forms.TextBox();
			this.seatClassCombobox = new Common.Misc.ComboboxThatCanAtLeastHaveCustomizableBackgroundColor();
			this.seatClassLabel = new System.Windows.Forms.Label();
			this.statusLabel = new System.Windows.Forms.Label();
			this.statusToolitp = new System.Windows.Forms.ToolTip(this.components);
			this.mainTabs.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTabs
			// 
			this.mainTabs.Controls.Add(this.tabPage1);
			this.mainTabs.Controls.Add(this.tabPage2);
			this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.mainTabs.Location = new System.Drawing.Point(0, 0);
			this.mainTabs.Margin = new System.Windows.Forms.Padding(0);
			this.mainTabs.Name = "mainTabs";
			this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(803, 434);
			this.mainTabs.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.passangerUpdate);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(795, 406);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Данные пассажира";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// passangerUpdate
			// 
			this.passangerUpdate.AutoSize = true;
			this.passangerUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangerUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangerUpdate.Location = new System.Drawing.Point(0, 0);
			this.passangerUpdate.Margin = new System.Windows.Forms.Padding(0);
			this.passangerUpdate.Name = "passangerUpdate";
			this.passangerUpdate.Size = new System.Drawing.Size(795, 406);
			this.passangerUpdate.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.passangerOptions);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(795, 406);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Опции";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// passangerOptions
			// 
			this.passangerOptions.BackColor = System.Drawing.SystemColors.Control;
			this.passangerOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangerOptions.Location = new System.Drawing.Point(0, 0);
			this.passangerOptions.Margin = new System.Windows.Forms.Padding(0);
			this.passangerOptions.Name = "passangerOptions";
			this.passangerOptions.Size = new System.Drawing.Size(795, 406);
			this.passangerOptions.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.mainTabs, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 473);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.RoyalBlue;
			this.tableLayoutPanel2.ColumnCount = 9;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.applyButton, 8, 0);
			this.tableLayoutPanel2.Controls.Add(this.cancelButton, 7, 0);
			this.tableLayoutPanel2.Controls.Add(this.deleteButton, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.seatSelect, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.seatPositionTextbox, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.seatClassCombobox, 5, 0);
			this.tableLayoutPanel2.Controls.Add(this.seatClassLabel, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.statusLabel, 6, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 434);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(803, 39);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// applyButton
			// 
			this.applyButton.AutoSize = true;
			this.applyButton.BackColor = System.Drawing.Color.White;
			this.applyButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.applyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
			this.applyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.applyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.applyButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.applyButton.Location = new System.Drawing.Point(715, 6);
			this.applyButton.Margin = new System.Windows.Forms.Padding(6);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(82, 27);
			this.applyButton.TabIndex = 0;
			this.applyButton.Text = "Применить";
			this.applyButton.UseVisualStyleBackColor = false;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.AutoSize = true;
			this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
			this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cancelButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.cancelButton.Location = new System.Drawing.Point(642, 6);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(6);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(61, 27);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.AutoSize = true;
			this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
			this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.deleteButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.deleteButton.Location = new System.Drawing.Point(6, 6);
			this.deleteButton.Margin = new System.Windows.Forms.Padding(6);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(63, 27);
			this.deleteButton.TabIndex = 2;
			this.deleteButton.Text = "Удалить";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// seatSelect
			// 
			this.seatSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.seatSelect.AutoSize = true;
			this.seatSelect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seatSelect.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.seatSelect.Location = new System.Drawing.Point(98, 10);
			this.seatSelect.Name = "seatSelect";
			this.seatSelect.Size = new System.Drawing.Size(102, 19);
			this.seatSelect.TabIndex = 5;
			this.seatSelect.Text = "Номер места:";
			this.seatSelect.UseVisualStyleBackColor = true;
			this.seatSelect.CheckedChanged += new System.EventHandler(this.seatSelect_CheckedChanged);
			// 
			// seatPositionTextbox
			// 
			this.seatPositionTextbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.seatPositionTextbox.BackColor = System.Drawing.Color.White;
			this.seatPositionTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.seatPositionTextbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seatPositionTextbox.Location = new System.Drawing.Point(203, 8);
			this.seatPositionTextbox.Margin = new System.Windows.Forms.Padding(0);
			this.seatPositionTextbox.Name = "seatPositionTextbox";
			this.seatPositionTextbox.Size = new System.Drawing.Size(100, 23);
			this.seatPositionTextbox.TabIndex = 4;
			this.seatPositionTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatPositionTextbox_KeyPress);
			this.seatPositionTextbox.Leave += new System.EventHandler(this.seatPositionTextbox_Leave);
			// 
			// seatClassCombobox
			// 
			this.seatClassCombobox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.seatClassCombobox.BackColor = System.Drawing.Color.White;
			this.seatClassCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.seatClassCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.seatClassCombobox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seatClassCombobox.FormattingEnabled = true;
			this.seatClassCombobox.Location = new System.Drawing.Point(364, 8);
			this.seatClassCombobox.Margin = new System.Windows.Forms.Padding(0);
			this.seatClassCombobox.Name = "seatClassCombobox";
			this.seatClassCombobox.Size = new System.Drawing.Size(100, 23);
			this.seatClassCombobox.TabIndex = 6;
			this.seatClassCombobox.SelectedIndexChanged += new System.EventHandler(this.seatClassCombobox_SelectedIndexChanged);
			// 
			// seatClassLabel
			// 
			this.seatClassLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.seatClassLabel.AutoSize = true;
			this.seatClassLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.seatClassLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.seatClassLabel.Location = new System.Drawing.Point(306, 12);
			this.seatClassLabel.Name = "seatClassLabel";
			this.seatClassLabel.Size = new System.Drawing.Size(55, 15);
			this.seatClassLabel.TabIndex = 7;
			this.seatClassLabel.Text = "seatClass";
			this.seatClassLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoEllipsis = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.ForeColor = System.Drawing.Color.Salmon;
			this.statusLabel.Location = new System.Drawing.Point(467, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(166, 39);
			this.statusLabel.TabIndex = 8;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PassangerSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(803, 473);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PassangerSettings";
			this.Text = "Выбор места";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PassangerSettings_FormClosing);
			this.mainTabs.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabs;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private PassangerList passangerUpdate;
		private PassangerOptions passangerOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.TextBox seatPositionTextbox;
		private System.Windows.Forms.CheckBox seatSelect;
		private Common.Misc.ComboboxThatCanAtLeastHaveCustomizableBackgroundColor seatClassCombobox;
		private System.Windows.Forms.Label seatClassLabel;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip statusToolitp;
	}
}