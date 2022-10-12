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
		bool showNumber;
		int number;

		Communication.Passanger passanger;

		private ToolTip toolTip;
		public ToolTip ToolTip{
			get => toolTip;
			set {
				var tt = toolTip?.GetToolTip(this);
				toolTip?.SetToolTip(this, null);
				toolTip = value;
				toolTip?.SetToolTip(this, tt);
			}
		}

		public Communication.Passanger Passanger{
			get{ return passanger; }
			set{ passanger = value; set(); }
		}

		public bool ShowNumber{
			get => showNumber;
			set{
				showNumber = value;
				setNumberLabel();
			}
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
			setNumberLabel();
		}

		private void set() {
			if(passanger == null) {
				FullNameLabel.Text = "Не задан";
				BirthdayLabel.Text = "";
			}
			else {
				var sb = new StringBuilder();
				sb.Append(passanger.surname).Append(" ");
				sb.Append(passanger.name[0]).Append(". ");
				if(passanger.middleName != null && passanger.middleName.Length > 0) 
					sb.Append(passanger.middleName[0]).Append(". ");

				FullNameLabel.Text = sb.ToString();

				sb.Clear().Append(passanger.surname).Append(" ").Append(passanger.name).Append(" ").Append(passanger.middleName);
				ToolTip?.SetToolTip(FullNameLabel,sb.ToString()); /*
					TODO: should the tooTip message be manually remove it when this component gets gisposed?
				*/
				BirthdayLabel.Text = passanger.birthday.ToString("dd.MM.yyyy");
			}
		}

		private void setNumberLabel() {
			if(!showNumber) numberLabel.Text = "";
			else numberLabel.Text = number.ToString().PadLeft(2, ' ');
		}
	}
}
