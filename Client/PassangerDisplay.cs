using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerDisplay : UserControl {
		int number;

		public PassangerDisplay(int number) {
			InitializeComponent();

			this.number = number;

			this.SuspendLayout();
			var clickArea = new TransparentPanel();
			clickArea.Dock = DockStyle.Fill;
			clickArea.Click += (a, e) => { this.OnClick(e); };
			this.Controls.Add(clickArea);
			clickArea.BringToFront();
			clickArea.Cursor = Cursors.Hand;
			this.ResumeLayout(false);
			this.PerformLayout();

			setEmpty();
		}

		void set(string fullName, DateTime birthday) {
			this.FullNameLabel.Text = fullName;
			this.BirthdayLabel.Text = birthday.ToString("dd.MM.yyyy");
			this.numberLabel.Text = "" + number;
		}
		void setEmpty() {
			this.FullNameLabel.Text = "Не задан";
			this.BirthdayLabel.Text = "";
			this.numberLabel.Text = "" + number;
		}
	}
}
