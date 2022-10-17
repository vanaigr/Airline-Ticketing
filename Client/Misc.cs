using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	static class Misc {
		public static Control addDummyButton(Control it) {
			/*
				HACK: for some reason first (or maybe last) programmatically added  focusable control receives focus
				automatically and it cannot be removed by setting ActiveControl to null
				so this dummy button is added in order for these contros to lose their focus.
			*/
			var name = "HACK_Client_Misc_addDummyButton";
			foreach(var button in it.Controls.Find(name, false)) button.Dispose();
			
			var dummy = new Button();
			dummy.Name = name;
			dummy.Size = new Size(0, 0);
			it.Controls.Add(dummy);

			return dummy;
		}

		//https://stackoverflow.com/a/3526775/18704284
		public static void unfocusOnEscape(Form form, KeyEventHandler inner = null) {
			form.KeyPreview = true;

			form.KeyDown += (a, e) => {
				inner?.Invoke(a, e);
				if(!e.Handled && e.KeyCode == Keys.Escape) {
					form.ActiveControl = null;
					e.Handled = true;
				}
			};
		}

		public static void fixFlowLayoutPanelHeight(FlowLayoutPanel it) {
			new FlowLayoutPanelHeightBugfix(it);
		}

		private class FlowLayoutPanelHeightBugfix {
			private bool ignoreResize__ = false;
			private FlowLayoutPanel panel;
			private Control parent;

			public FlowLayoutPanelHeightBugfix(FlowLayoutPanel it) {
				this.panel = it;
				this.parent = it.Parent;
				parent.Resize += new EventHandler(resizeFix);
				it.Resize += new EventHandler(resizeFix);
			}

			private void resizeFix(object sender, EventArgs e) {
				if(ignoreResize__) return;
				ignoreResize__ = true;
				var tableSize = panel.Size;
				parent.SuspendLayout();
				parent.Size = tableSize;
				parent.ResumeLayout(false);
				parent.PerformLayout();
				ignoreResize__ = false;
			}
		}
	}

	static class Math2 {
		public static int gcd(int a, int b) {
			if(b == 0) return a; else return gcd(b, a % b);
		}
		
		public static int lcm(int a, int b) { return a / gcd(a, b) * b; }
	}
}
