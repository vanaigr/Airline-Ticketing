
namespace Client {
	partial class PassangerAdd {
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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			this.nameText = new System.Windows.Forms.TextBox();
			this.surnameText = new System.Windows.Forms.TextBox();
			this.middleNameText = new System.Windows.Forms.TextBox();
			this.birthdayDatetime = new System.Windows.Forms.DateTimePicker();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.documentTypeCombobox = new System.Windows.Forms.ComboBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.passangersDisplay = new System.Windows.Forms.TableLayoutPanel();
			this.deleteButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.saveAndCloseButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.passangerTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.addButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			tableLayoutPanel3.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(36, 13);
			label1.TabIndex = 0;
			label1.Text = "Имя:*";
			// 
			// label2
			// 
			label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(196, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(63, 13);
			label2.TabIndex = 1;
			label2.Text = "Фамилия:*";
			// 
			// label3
			// 
			label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(384, 15);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(57, 13);
			label3.TabIndex = 2;
			label3.Text = "Отчество:";
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.BackColor = System.Drawing.Color.WhiteSmoke;
			tableLayoutPanel3.ColumnCount = 1;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
			tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 2);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 5;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.Size = new System.Drawing.Size(575, 407);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.AutoSize = true;
			tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel4.BackColor = System.Drawing.Color.White;
			tableLayoutPanel4.ColumnCount = 3;
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel4.Controls.Add(label1, 0, 0);
			tableLayoutPanel4.Controls.Add(label2, 1, 0);
			tableLayoutPanel4.Controls.Add(label3, 2, 0);
			tableLayoutPanel4.Controls.Add(this.nameText, 0, 1);
			tableLayoutPanel4.Controls.Add(this.surnameText, 1, 1);
			tableLayoutPanel4.Controls.Add(this.middleNameText, 2, 1);
			tableLayoutPanel4.Controls.Add(label4, 0, 3);
			tableLayoutPanel4.Controls.Add(this.birthdayDatetime, 0, 4);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
			tableLayoutPanel4.RowCount = 5;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.Size = new System.Drawing.Size(575, 123);
			tableLayoutPanel4.TabIndex = 0;
			// 
			// nameText
			// 
			this.nameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameText.Location = new System.Drawing.Point(8, 31);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(182, 22);
			this.nameText.TabIndex = 3;
			// 
			// surnameText
			// 
			this.surnameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.surnameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.surnameText.Location = new System.Drawing.Point(196, 31);
			this.surnameText.Name = "surnameText";
			this.surnameText.Size = new System.Drawing.Size(182, 22);
			this.surnameText.TabIndex = 4;
			// 
			// middleNameText
			// 
			this.middleNameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.middleNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.middleNameText.Location = new System.Drawing.Point(384, 31);
			this.middleNameText.Name = "middleNameText";
			this.middleNameText.Size = new System.Drawing.Size(183, 22);
			this.middleNameText.TabIndex = 5;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(8, 66);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(93, 13);
			label4.TabIndex = 6;
			label4.Text = "Дата рождения:*";
			// 
			// birthdayDatetime
			// 
			this.birthdayDatetime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.birthdayDatetime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.birthdayDatetime.Location = new System.Drawing.Point(8, 82);
			this.birthdayDatetime.Name = "birthdayDatetime";
			this.birthdayDatetime.Size = new System.Drawing.Size(182, 23);
			this.birthdayDatetime.TabIndex = 7;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.AutoSize = true;
			this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel5.Controls.Add(this.documentTypeCombobox, 0, 1);
			this.tableLayoutPanel5.Controls.Add(label5, 0, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 143);
			this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.Size = new System.Drawing.Size(575, 73);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// documentTypeCombobox
			// 
			this.documentTypeCombobox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.documentTypeCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.documentTypeCombobox.FormattingEnabled = true;
			this.documentTypeCombobox.Location = new System.Drawing.Point(8, 31);
			this.documentTypeCombobox.Name = "documentTypeCombobox";
			this.documentTypeCombobox.Size = new System.Drawing.Size(182, 24);
			this.documentTypeCombobox.TabIndex = 0;
			// 
			// label5
			// 
			label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(8, 15);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(90, 13);
			label5.TabIndex = 1;
			label5.Text = "Тип документа:*";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer1.Size = new System.Drawing.Size(800, 450);
			this.splitContainer1.SplitterDistance = 215;
			this.splitContainer1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.passangersDisplay, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.deleteButton, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.addButton, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 450);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// passangersDisplay
			// 
			this.passangersDisplay.AutoSize = true;
			this.passangersDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangersDisplay.ColumnCount = 1;
			this.tableLayoutPanel1.SetColumnSpan(this.passangersDisplay, 2);
			this.passangersDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.passangersDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangersDisplay.Location = new System.Drawing.Point(3, 3);
			this.passangersDisplay.Margin = new System.Windows.Forms.Padding(0);
			this.passangersDisplay.Name = "passangersDisplay";
			this.passangersDisplay.RowCount = 1;
			this.passangersDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.passangersDisplay.Size = new System.Drawing.Size(209, 421);
			this.passangersDisplay.TabIndex = 4;
			// 
			// deleteButton
			// 
			this.deleteButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Location = new System.Drawing.Point(3, 424);
			this.deleteButton.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(103, 23);
			this.deleteButton.TabIndex = 2;
			this.deleteButton.Text = "Удалить";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 1);
			this.tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(581, 450);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.AutoSize = true;
			this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel6.ColumnCount = 3;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel6.Controls.Add(this.saveAndCloseButton, 2, 0);
			this.tableLayoutPanel6.Controls.Add(this.saveButton, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.statusLabel, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 416);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.Size = new System.Drawing.Size(575, 31);
			this.tableLayoutPanel6.TabIndex = 2;
			// 
			// saveAndCloseButton
			// 
			this.saveAndCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.saveAndCloseButton.AutoSize = true;
			this.saveAndCloseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.saveAndCloseButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.saveAndCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.saveAndCloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.saveAndCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.saveAndCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveAndCloseButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.saveAndCloseButton.Location = new System.Drawing.Point(457, 3);
			this.saveAndCloseButton.Name = "saveAndCloseButton";
			this.saveAndCloseButton.Size = new System.Drawing.Size(115, 25);
			this.saveAndCloseButton.TabIndex = 5;
			this.saveAndCloseButton.Text = "Сохранить и выйти";
			this.saveAndCloseButton.UseVisualStyleBackColor = false;
			this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.saveButton.AutoSize = true;
			this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.saveButton.BackColor = System.Drawing.Color.Transparent;
			this.saveButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.saveButton.Location = new System.Drawing.Point(379, 3);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(72, 25);
			this.saveButton.TabIndex = 6;
			this.saveButton.Text = "Сохранить";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(3, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(370, 31);
			this.statusLabel.TabIndex = 7;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// statusTooltip
			// 
			this.statusTooltip.AutoPopDelay = 5000;
			this.statusTooltip.InitialDelay = 500;
			this.statusTooltip.ReshowDelay = 100;
			// 
			// addButton
			// 
			this.addButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.addButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.addButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.addButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.addButton.Location = new System.Drawing.Point(108, 424);
			this.addButton.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(104, 23);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Создать";
			this.addButton.UseVisualStyleBackColor = false;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// PassangerAdd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Name = "PassangerAdd";
			this.Text = "PassangerAdd";
			this.Load += new System.EventHandler(this.PassangerAdd_Load);
			this.Shown += new System.EventHandler(this.PassangerAdd_Shown);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			tableLayoutPanel4.ResumeLayout(false);
			tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel passangersDisplay;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.ToolTip passangerTooltip;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.TextBox surnameText;
		private System.Windows.Forms.TextBox middleNameText;
		private System.Windows.Forms.DateTimePicker birthdayDatetime;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.ComboBox documentTypeCombobox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Button saveAndCloseButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip statusTooltip;
		private System.Windows.Forms.Button addButton;
	}
}