
namespace Client {
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
			this.mainTabs = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.passangerUpdate = new Client.PassangerUpdate();
			this.passangerOptions = new Client.PassangerOptions();
			this.mainTabs.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainTabs
			// 
			this.mainTabs.Controls.Add(this.tabPage1);
			this.mainTabs.Controls.Add(this.tabPage2);
			this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Location = new System.Drawing.Point(0, 0);
			this.mainTabs.Margin = new System.Windows.Forms.Padding(0);
			this.mainTabs.Name = "mainTabs";
			this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(800, 450);
			this.mainTabs.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.passangerUpdate);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(792, 424);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Данные пассажира";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.passangerOptions);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(792, 424);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Опции";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// passangerUpdate
			// 
			this.passangerUpdate.AutoSize = true;
			this.passangerUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.passangerUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangerUpdate.Location = new System.Drawing.Point(0, 0);
			this.passangerUpdate.Margin = new System.Windows.Forms.Padding(0);
			this.passangerUpdate.Name = "passangerUpdate";
			this.passangerUpdate.Size = new System.Drawing.Size(792, 424);
			this.passangerUpdate.TabIndex = 0;
			// 
			// passangerOptions
			// 
			this.passangerOptions.BackColor = System.Drawing.SystemColors.Control;
			this.passangerOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passangerOptions.Location = new System.Drawing.Point(0, 0);
			this.passangerOptions.Margin = new System.Windows.Forms.Padding(0);
			this.passangerOptions.Name = "passangerOptions";
			this.passangerOptions.Size = new System.Drawing.Size(792, 424);
			this.passangerOptions.TabIndex = 0;
			// 
			// PassangerSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.mainTabs);
			this.Name = "PassangerSettings";
			this.Text = "SappangerSettings";
			this.mainTabs.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabs;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private PassangerUpdate passangerUpdate;
		private PassangerOptions passangerOptions;
	}
}