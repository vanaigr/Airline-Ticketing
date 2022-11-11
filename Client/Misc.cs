using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ComponentModel;

namespace Common {
	static class Misc {
		public static Color selectionColor3 = Color.FromArgb(255, Color.FromArgb(0x00EAEAEA));

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
				panel = it;
				parent = it.Parent;

				parent.Resize += (a, b) => resizeFix();
				it.Resize += (a, b) => resizeFix();
			}

			private void resizeFix() {
				if(ignoreResize__) return;
				ignoreResize__ = true;
				var tableSize = panel.Size;

				//haha, very funny, Winforms, you broke my fix of your bug.
				//The new bug is: when the form is deactivated (hidden),
				//size caclulations for flow layout panel are still
				//performed incorrectly and even though they are performed
				//again correctly when the window is activated (opened)
				//the size update is not applied for whatever reason.
				//The solution is to reject the bad calculations in the first place.
				//example:
				/*
					{Width=741, Height=47}	- correct size
					form deactivated		- control added and new window is opened on top
					form activated			- new window closed
					form deactivated		- control added and new window is opened on top
					form activated			- new window closed
					{Width=1, Height=94}	- incorrect size
					{Width=1, Height=94}	- calculated twice -_-
					form deactivated		- this window hidden
					form activated			- this window opened
					{Width=741, Height=47}	- correct calculations
					{Width=741, Height=47}	- twice again
											- and here the height is still incorrect (94)
											- but the width is!
											- probably because panel is correct size
											- and its parent isn't
				*/
				if(tableSize.Width != 1 && tableSize.Height != 1) {
					parent.SuspendLayout();
					parent.Size = tableSize;
					parent.ResumeLayout(false);
					parent.PerformLayout();
				}

				ignoreResize__ = false;
			}
		}

		public static void addBottomDivider(TableLayoutPanel p) {
			var it = new Panel();
			it.Dock = DockStyle.Fill;
			it.BorderStyle = BorderStyle.FixedSingle;
			it.Margin = new Padding(0);

			p.Controls.Add(it, 0, p.RowCount++);
			p.RowStyles.Add(new RowStyle(SizeType.Absolute, 1));
			p.SetColumnSpan(it, p.ColumnCount);
		}

		public static void addTopDivider(TableLayoutPanel p) {
			var it = new Panel();
			it.Dock = DockStyle.Fill;
			it.BorderStyle = BorderStyle.FixedSingle;
			it.Margin = new Padding(0);

			p.SuspendLayout();

			for(int i = p.Controls.Count-1; i >= 0; i--) {
				var control = p.Controls[i];
				var pos = p.GetPositionFromControl(control);

				p.Controls.RemoveAt(i);
				p.Controls.Add(control, pos.Column, pos.Row+1);
			}

			p.Controls.Add(it, 0, 0);
			p.RowCount++;
			p.RowStyles.Insert(0, new RowStyle(SizeType.Absolute, 1));
			p.SetColumnSpan(it, p.ColumnCount);

			p.ResumeLayout(false);
			p.PerformLayout();
		}

		public static void setBetterFont(Control c, float? size = null, GraphicsUnit? gu = null) {
			var cf = c.Font;
			c.Font = new Font("Segoe UI", size ?? cf.Size, cf.Style, gu ?? cf.Unit, cf.GdiCharSet);
		}

		//https://stackoverflow.com/a/66310028/18704284
		public class ComboboxThatCanAtLeastHaveCustomizableBackgroundColor : ComboBox {

			public ComboboxThatCanAtLeastHaveCustomizableBackgroundColor() {
				BorderColor = Color.DimGray;
				FlatStyle = FlatStyle.Flat;
			}

			[Browsable(true)]
			[Category("Appearance")]
			[DefaultValue(typeof(Color), "DimGray")]
			public Color BorderColor { get; set; }

			private const int WM_PAINT = 0xF;
			private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
			protected override void WndProc(ref Message m) {
				base.WndProc(ref m);
				if(m.Msg == WM_PAINT) {
					using(var g = Graphics.FromHwnd(Handle)) {
						// Uncomment this if you don't want the "highlight border".
						/*
		                using (var p = new Pen(this.BorderColor, 1))
		                {
		                    g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
		                }*/
						using(var p = new Pen(BorderColor, 2)) {
							g.DrawRectangle(p, 0, 0, Width, Height);
						}
					}
				}
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
