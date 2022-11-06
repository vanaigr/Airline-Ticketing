using Common;
using Communication;
using System.Text;
using System.Windows.Forms;

namespace Client {
	public partial class PassangerDisplay : UserControl {
		bool showNumber;
		int number;

		Passanger passanger;

		private string tooltipMessage;

		private ToolTip toolTip;
		public ToolTip ToolTip{
			get{ return toolTip; }
			set{
				toolTip?.SetToolTip(clickArea, null);
				toolTip = value;
				toolTip?.SetToolTip(clickArea, tooltipMessage);
			}
		}

		public Passanger Passanger{
			get{ return passanger; }
			set{ passanger = value; set(); }
		}

		public bool ShowNumber{
			get{ return showNumber; }
			set{
				showNumber = value;
				setNumberLabel();
			}
		}

		public int Number{ get{ return number; } set{ number = value; setNumberLabel(); } }

		public object Data;
		private TransparentPanel clickArea;

		public PassangerDisplay() : base() {
			InitializeComponent();

			this.SuspendLayout();
			clickArea = new TransparentPanel();
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
				tooltipMessage = sb.ToString();
				ToolTip?.SetToolTip(clickArea, tooltipMessage); /*
					is the tooTip message be manually removed when this component gets disposed?
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
