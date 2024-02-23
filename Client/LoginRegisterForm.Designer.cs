
namespace Client {
    partial class LoginRegisterForm {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginRegisterForm));
            this.LoginText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.statusTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            //
            // LoginText
            //
            this.LoginText.BackColor = System.Drawing.Color.White;
            this.LoginText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginText.Location = new System.Drawing.Point(84, 13);
            this.LoginText.MaxLength = 64;
            this.LoginText.Name = "LoginText";
            this.LoginText.Size = new System.Drawing.Size(243, 27);
            this.LoginText.TabIndex = 0;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.MaximumSize = new System.Drawing.Size(600, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 33);
            this.label2.TabIndex = 2;
            this.label2.Text = "Логин:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.MaximumSize = new System.Drawing.Size(600, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Пароль:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // LoginButton
            //
            this.LoginButton.AutoSize = true;
            this.LoginButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoginButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.LoginButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.LoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.LoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LoginButton.Location = new System.Drawing.Point(254, 3);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(63, 32);
            this.LoginButton.TabIndex = 100;
            this.LoginButton.Text = "Войти";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            //
            // RegisterButton
            //
            this.RegisterButton.AutoSize = true;
            this.RegisterButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RegisterButton.BackColor = System.Drawing.Color.Transparent;
            this.RegisterButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.RegisterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            this.RegisterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.RegisterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterButton.Location = new System.Drawing.Point(3, 3);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(162, 32);
            this.RegisterButton.TabIndex = 3;
            this.RegisterButton.Text = "Зарегестрироваться";
            this.RegisterButton.UseVisualStyleBackColor = false;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(171, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 38);
            this.label4.TabIndex = 0;
            this.label4.Text = "или";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // statusLabel
            //
            this.statusLabel.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.statusLabel, 3);
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusLabel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.statusLabel.Location = new System.Drawing.Point(3, 43);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(314, 17);
            this.statusLabel.TabIndex = 1;
            //
            // showPassword
            //
            this.showPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.showPassword.BackColor = System.Drawing.Color.White;
            this.showPassword.Image = ((System.Drawing.Image)(resources.GetObject("showPassword.Image")));
            this.showPassword.Location = new System.Drawing.Point(222, 3);
            this.showPassword.Margin = new System.Windows.Forms.Padding(0);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(20, 20);
            this.showPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showPassword.TabIndex = 10;
            this.showPassword.TabStop = false;
            this.showPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.showPassword.MouseLeave += new System.EventHandler(this.showPassword_MouseLeave);
            this.showPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            //
            // PasswordText
            //
            this.PasswordText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordText.BackColor = System.Drawing.Color.White;
            this.PasswordText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PasswordText.Location = new System.Drawing.Point(4, 3);
            this.PasswordText.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.PasswordText.MaxLength = 64;
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(218, 20);
            this.PasswordText.TabIndex = 0;
            this.PasswordText.UseSystemPasswordChar = true;
            this.PasswordText.Enter += new System.EventHandler(this.PasswordText_Enter);
            this.PasswordText.Leave += new System.EventHandler(this.PasswordText_Leave);
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LoginText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 165);
            this.tableLayoutPanel1.TabIndex = 11;
            //
            // tableLayoutPanel3
            //
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.PasswordText, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.showPassword, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(84, 46);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(243, 26);
            this.tableLayoutPanel3.TabIndex = 1;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.LoginButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.RegisterButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusLabel, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 95);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(320, 60);
            this.tableLayoutPanel2.TabIndex = 4;
            //
            // LoginRegisterForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(340, 165);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginRegisterForm";
            this.Text = "Вход и регистрация";
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox LoginText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.ToolTip statusTooltip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}

