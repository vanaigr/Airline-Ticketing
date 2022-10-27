using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common {
	public class TransparentPanel : Panel {
		private FlowLayoutPanel flowLayoutPanel1;

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
				return cp;
			}
		}
		protected override void OnPaintBackground(PaintEventArgs e) {
			//base.OnPaintBackground(e);
		}

		private void InitializeComponent() {
			flowLayoutPanel1 = new FlowLayoutPanel();
			SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
			flowLayoutPanel1.TabIndex = 0;
			ResumeLayout(false);

		}
	}
}
