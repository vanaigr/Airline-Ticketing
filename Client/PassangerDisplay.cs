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

		Communication.Passanger passanger;

		public Communication.Passanger Passanger{
			get{ return passanger; }
			set{ passanger = value; set(); }
		}

		public int Number{ get => number; set{ number = value; setNumberLabel(); } }
		public PassangerDisplay() : base() {
			InitializeComponent();

			this.SuspendLayout();
			var clickArea = new TransparentPanel();
			clickArea.Dock = DockStyle.Fill;
			clickArea.Click += (a, e) => { this.OnClick(e); };
			this.Controls.Add(clickArea);
			clickArea.BringToFront();
			clickArea.Cursor = Cursors.Hand;
			this.ResumeLayout(false);
			this.PerformLayout();

			set();
		}

		public PassangerDisplay(int number) : this() {
			Number = number;
		}

		private void set() {
			if(passanger == null) {
				FullNameLabel.Text = "Не задан";
				BirthdayLabel.Text = "";
			}
			else {
				FullNameLabel.Text = passanger.fullName;
				BirthdayLabel.Text = passanger.birthday.ToString("dd.MM.yyyy");
			}
		}

		private void setNumberLabel() {
			this.numberLabel.Text = number.ToString().PadLeft(2, ' ');
		}
	}
}
