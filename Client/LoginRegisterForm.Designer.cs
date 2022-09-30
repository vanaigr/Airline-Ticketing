
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginRegisterForm));
			this.LoginText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.LoginButton = new System.Windows.Forms.Button();
			this.RegisterButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.showPassword = new System.Windows.Forms.PictureBox();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.PasswordText = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// LoginText
			// 
			this.LoginText.BackColor = System.Drawing.Color.WhiteSmoke;
			this.LoginText.Location = new System.Drawing.Point(69, 9);
			this.LoginText.MaxLength = 64;
			this.LoginText.Name = "LoginText";
			this.LoginText.Size = new System.Drawing.Size(202, 20);
			this.LoginText.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 12);
			this.label2.MaximumSize = new System.Drawing.Size(600, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Логин:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 41);
			this.label3.MaximumSize = new System.Drawing.Size(600, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Пароль:";
			// 
			// LoginButton
			// 
			this.LoginButton.BackColor = System.Drawing.Color.Transparent;
			this.LoginButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.LoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.LoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LoginButton.Location = new System.Drawing.Point(11, 68);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 6;
			this.LoginButton.Text = "Войти";
			this.LoginButton.UseVisualStyleBackColor = false;
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
			// 
			// RegisterButton
			// 
			this.RegisterButton.BackColor = System.Drawing.Color.Transparent;
			this.RegisterButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.RegisterButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
			this.RegisterButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
			this.RegisterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.RegisterButton.Location = new System.Drawing.Point(148, 68);
			this.RegisterButton.Name = "RegisterButton";
			this.RegisterButton.Size = new System.Drawing.Size(124, 23);
			this.RegisterButton.TabIndex = 7;
			this.RegisterButton.Text = "Зарегестрироваться";
			this.RegisterButton.UseVisualStyleBackColor = false;
			this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(106, 73);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "или";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.label1.Location = new System.Drawing.Point(10, 101);
			this.label1.MaximumSize = new System.Drawing.Size(250, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 9;
			// 
			// showPassword
			// 
			this.showPassword.Image = ((System.Drawing.Image)(resources.GetObject("showPassword.Image")));
			this.showPassword.Location = new System.Drawing.Point(251, 38);
			this.showPassword.Name = "showPassword";
			this.showPassword.Size = new System.Drawing.Size(20, 20);
			this.showPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.showPassword.TabIndex = 10;
			this.showPassword.TabStop = false;
			this.showPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.showPassword.MouseLeave += new System.EventHandler(this.showPassword_MouseLeave);
			this.showPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			// 
			// PasswordText
			// 
			this.PasswordText.BackColor = System.Drawing.Color.WhiteSmoke;
			this.PasswordText.Location = new System.Drawing.Point(69, 38);
			this.PasswordText.MaxLength = 64;
			this.PasswordText.Name = "PasswordText";
			this.PasswordText.Size = new System.Drawing.Size(176, 20);
			this.PasswordText.TabIndex = 3;
			this.PasswordText.UseSystemPasswordChar = true;
			// 
			// LoginRegisterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(284, 191);
			this.Controls.Add(this.PasswordText);
			this.Controls.Add(this.showPassword);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.RegisterButton);
			this.Controls.Add(this.LoginButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.LoginText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LoginRegisterForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox showPassword;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.TextBox PasswordText;
	}
}

