
namespace Client {
	partial class PassangerList {
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
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
			this.nameText = new System.Windows.Forms.TextBox();
			this.surnameText = new System.Windows.Forms.TextBox();
			this.middleNameText = new System.Windows.Forms.TextBox();
			this.birthdayDatetime = new System.Windows.Forms.DateTimePicker();
			this.passangerDataTable = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.documentTypeCombobox = new System.Windows.Forms.ComboBox();
			this.passangersDisplay = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.saveButton = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.addButton = new System.Windows.Forms.Button();
			this.deleteButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.passangerTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.passangerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel4.SuspendLayout();
			tableLayoutPanel8.SuspendLayout();
			this.passangerDataTable.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			tableLayoutPanel7.SuspendLayout();
			this.passangerMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label1.Location = new System.Drawing.Point(8, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(62, 13);
			label1.TabIndex = 0;
			label1.Text = "Фамилия:*";
			// 
			// label2
			// 
			label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label2.Location = new System.Drawing.Point(189, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(37, 13);
			label2.TabIndex = 1;
			label2.Text = "Имя:*";
			// 
			// label3
			// 
			label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label3.Location = new System.Drawing.Point(370, 15);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(60, 13);
			label3.TabIndex = 2;
			label3.Text = "Отчество:";
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
			tableLayoutPanel4.Controls.Add(this.nameText, 1, 1);
			tableLayoutPanel4.Controls.Add(this.surnameText, 0, 1);
			tableLayoutPanel4.Controls.Add(this.middleNameText, 2, 1);
			tableLayoutPanel4.Controls.Add(label4, 0, 3);
			tableLayoutPanel4.Controls.Add(this.birthdayDatetime, 0, 4);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel4.Location = new System.Drawing.Point(6, 6);
			tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
			tableLayoutPanel4.RowCount = 5;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel4.Size = new System.Drawing.Size(553, 128);
			tableLayoutPanel4.TabIndex = 0;
			// 
			// nameText
			// 
			this.nameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nameText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameText.Location = new System.Drawing.Point(189, 31);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(175, 25);
			this.nameText.TabIndex = 4;
			// 
			// surnameText
			// 
			this.surnameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.surnameText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.surnameText.Location = new System.Drawing.Point(8, 31);
			this.surnameText.Name = "surnameText";
			this.surnameText.Size = new System.Drawing.Size(175, 25);
			this.surnameText.TabIndex = 3;
			// 
			// middleNameText
			// 
			this.middleNameText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.middleNameText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.middleNameText.Location = new System.Drawing.Point(370, 31);
			this.middleNameText.Name = "middleNameText";
			this.middleNameText.Size = new System.Drawing.Size(175, 25);
			this.middleNameText.TabIndex = 5;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label4.Location = new System.Drawing.Point(8, 69);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(98, 13);
			label4.TabIndex = 6;
			label4.Text = "Дата рождения:*";
			// 
			// birthdayDatetime
			// 
			this.birthdayDatetime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.birthdayDatetime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.birthdayDatetime.Location = new System.Drawing.Point(8, 85);
			this.birthdayDatetime.Name = "birthdayDatetime";
			this.birthdayDatetime.Size = new System.Drawing.Size(174, 25);
			this.birthdayDatetime.TabIndex = 7;
			// 
			// label5
			// 
			label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			label5.Location = new System.Drawing.Point(8, 15);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(94, 13);
			label5.TabIndex = 1;
			label5.Text = "Тип документа:*";
			// 
			// tableLayoutPanel8
			// 
			tableLayoutPanel8.AutoSize = true;
			tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel8.ColumnCount = 2;
			tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			tableLayoutPanel8.Controls.Add(this.passangerDataTable, 1, 0);
			tableLayoutPanel8.Controls.Add(this.passangersDisplay, 0, 0);
			tableLayoutPanel8.Controls.Add(this.tableLayoutPanel6, 1, 1);
			tableLayoutPanel8.Controls.Add(tableLayoutPanel7, 0, 1);
			tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel8.Name = "tableLayoutPanel8";
			tableLayoutPanel8.RowCount = 2;
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel8.Size = new System.Drawing.Size(807, 505);
			tableLayoutPanel8.TabIndex = 0;
			// 
			// passangerDataTable
			// 
			this.passangerDataTable.BackColor = System.Drawing.Color.WhiteSmoke;
			this.passangerDataTable.ColumnCount = 1;
			this.passangerDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.passangerDataTable.Controls.Add(tableLayoutPanel4, 0, 0);
			this.passangerDataTable.Controls.Add(this.tableLayoutPanel5, 0, 2);
			this.passangerDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangerDataTable.Location = new System.Drawing.Point(242, 0);
			this.passangerDataTable.Margin = new System.Windows.Forms.Padding(0);
			this.passangerDataTable.Name = "passangerDataTable";
			this.passangerDataTable.Padding = new System.Windows.Forms.Padding(6);
			this.passangerDataTable.RowCount = 5;
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.passangerDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.passangerDataTable.Size = new System.Drawing.Size(565, 468);
			this.passangerDataTable.TabIndex = 1;
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
			this.tableLayoutPanel5.Location = new System.Drawing.Point(6, 154);
			this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.Size = new System.Drawing.Size(553, 74);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// documentTypeCombobox
			// 
			this.documentTypeCombobox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.documentTypeCombobox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.documentTypeCombobox.FormattingEnabled = true;
			this.documentTypeCombobox.Location = new System.Drawing.Point(8, 31);
			this.documentTypeCombobox.Name = "documentTypeCombobox";
			this.documentTypeCombobox.Size = new System.Drawing.Size(175, 25);
			this.documentTypeCombobox.TabIndex = 0;
			// 
			// passangersDisplay
			// 
			this.passangersDisplay.AutoScroll = true;
			this.passangersDisplay.ColumnCount = 1;
			this.passangersDisplay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.passangersDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangersDisplay.Location = new System.Drawing.Point(0, 0);
			this.passangersDisplay.Margin = new System.Windows.Forms.Padding(0);
			this.passangersDisplay.Name = "passangersDisplay";
			this.passangersDisplay.Padding = new System.Windows.Forms.Padding(6);
			this.passangersDisplay.RowCount = 1;
			this.passangersDisplay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.passangersDisplay.Size = new System.Drawing.Size(242, 468);
			this.passangersDisplay.TabIndex = 4;
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.AutoSize = true;
			this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel6.BackColor = System.Drawing.Color.White;
			this.tableLayoutPanel6.ColumnCount = 3;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel6.Controls.Add(this.saveButton, 2, 0);
			this.tableLayoutPanel6.Controls.Add(this.statusLabel, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(242, 468);
			this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(6);
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(565, 37);
			this.tableLayoutPanel6.TabIndex = 2;
			// 
			// saveButton
			// 
			this.saveButton.AutoSize = true;
			this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.saveButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.saveButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.saveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.saveButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.saveButton.Location = new System.Drawing.Point(483, 6);
			this.saveButton.Margin = new System.Windows.Forms.Padding(0);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(76, 25);
			this.saveButton.TabIndex = 6;
			this.saveButton.Text = "Сохранить";
			this.saveButton.UseVisualStyleBackColor = false;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// statusLabel
			// 
			this.statusLabel.AutoEllipsis = true;
			this.statusLabel.BackColor = System.Drawing.Color.Transparent;
			this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusLabel.ForeColor = System.Drawing.Color.Firebrick;
			this.statusLabel.Location = new System.Drawing.Point(9, 9);
			this.statusLabel.Margin = new System.Windows.Forms.Padding(3);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(468, 19);
			this.statusLabel.TabIndex = 7;
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel7
			// 
			tableLayoutPanel7.AutoSize = true;
			tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel7.BackColor = System.Drawing.Color.White;
			tableLayoutPanel7.ColumnCount = 5;
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			tableLayoutPanel7.Controls.Add(this.addButton, 4, 0);
			tableLayoutPanel7.Controls.Add(this.deleteButton, 2, 0);
			tableLayoutPanel7.Controls.Add(this.editButton, 0, 0);
			tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel7.Location = new System.Drawing.Point(0, 468);
			tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel7.Name = "tableLayoutPanel7";
			tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(6);
			tableLayoutPanel7.RowCount = 1;
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel7.Size = new System.Drawing.Size(242, 37);
			tableLayoutPanel7.TabIndex = 2;
			// 
			// addButton
			// 
			this.addButton.AutoSize = true;
			this.addButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.addButton.BackColor = System.Drawing.Color.Transparent;
			this.addButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.addButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.addButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.addButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.addButton.Location = new System.Drawing.Point(160, 6);
			this.addButton.Margin = new System.Windows.Forms.Padding(0);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(76, 25);
			this.addButton.TabIndex = 4;
			this.addButton.Text = "Добавить";
			this.addButton.UseVisualStyleBackColor = false;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.AutoSize = true;
			this.deleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.deleteButton.BackColor = System.Drawing.Color.Transparent;
			this.deleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.deleteButton.Location = new System.Drawing.Point(83, 6);
			this.deleteButton.Margin = new System.Windows.Forms.Padding(0);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(74, 25);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Удалить";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// editButton
			// 
			this.editButton.AutoSize = true;
			this.editButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.editButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.editButton.Location = new System.Drawing.Point(6, 6);
			this.editButton.Margin = new System.Windows.Forms.Padding(0);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(74, 25);
			this.editButton.TabIndex = 5;
			this.editButton.Text = "Изменить";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// statusTooltip
			// 
			this.statusTooltip.AutoPopDelay = 5000;
			this.statusTooltip.InitialDelay = 500;
			this.statusTooltip.ReshowDelay = 100;
			// 
			// passangerMenu
			// 
			this.passangerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem,
            this.изменитьToolStripMenuItem});
			this.passangerMenu.Name = "passangerMenu";
			this.passangerMenu.Size = new System.Drawing.Size(129, 48);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// изменитьToolStripMenuItem
			// 
			this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
			this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.изменитьToolStripMenuItem.Text = "Изменить";
			this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
			// 
			// PassangerList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(tableLayoutPanel8);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "PassangerList";
			this.Size = new System.Drawing.Size(807, 505);
			tableLayoutPanel4.ResumeLayout(false);
			tableLayoutPanel4.PerformLayout();
			tableLayoutPanel8.ResumeLayout(false);
			tableLayoutPanel8.PerformLayout();
			this.passangerDataTable.ResumeLayout(false);
			this.passangerDataTable.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			tableLayoutPanel7.ResumeLayout(false);
			tableLayoutPanel7.PerformLayout();
			this.passangerMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolTip passangerTooltip;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.TextBox surnameText;
		private System.Windows.Forms.TextBox middleNameText;
		private System.Windows.Forms.DateTimePicker birthdayDatetime;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.ComboBox documentTypeCombobox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolTip statusTooltip;
		private System.Windows.Forms.TableLayoutPanel passangersDisplay;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.TableLayoutPanel passangerDataTable;
		private System.Windows.Forms.ContextMenuStrip passangerMenu;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
	}
}