using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ClientCommunication {
	class ListItemLabel : Label {
		public Color BackColor2 = Color.Transparent;
		
		public ListItemLabel() {
			AutoSize = true;
			UseCompatibleTextRendering = true;
			TextAlign = ContentAlignment.MiddleCenter;
			Padding = new Padding(3);
		}

		protected override void OnPaint(PaintEventArgs e) {
			{
		    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

		    var rect = ClientRectangle;
		    rect.Width--;
		    rect.Height--;

			var center = new PointF(rect.X + rect.Width/2.0f, rect.Y + rect.Height/2.0f);
			var dim = Math.Min(rect.Width, rect.Height);

		    using (var brush = new SolidBrush(BackColor2)) {
		        e.Graphics.FillEllipse(brush, new RectangleF(center.X - dim/2.0f, center.Y - dim/2.0f, dim, dim));
		    }
			}

			{
			var flags = 0
				| TextFormatFlags.SingleLine 
				| TextFormatFlags.ExternalLeading
				| TextFormatFlags.NoClipping
				| TextFormatFlags.NoFullWidthCharacterBreak
				| TextFormatFlags.PreserveGraphicsClipping
				| TextFormatFlags.PreserveGraphicsTranslateTransform
				//| TextFormatFlags.Internal
				| TextFormatFlags.NoPadding
				| TextFormatFlags.HorizontalCenter
				| TextFormatFlags.VerticalCenter
			;

			var rect = ClientRectangle;
			TextRenderer.DrawText(
				e.Graphics, Text, Font, rect,
                ForeColor, Color.Transparent, flags
			);
			}
		}

		public override Size GetPreferredSize(Size proposedSize) {
			var size = base.GetPreferredSize(proposedSize);
			return new Size(size.Height, size.Height);
		}
	}
}
