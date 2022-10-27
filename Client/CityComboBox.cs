using Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common {
	class CityComboBox : ComboBox {
		private StringBuilder sb;

		public static readonly Color ForeColor2 = Color.DarkGray;

		public CityComboBox() {
			sb = new StringBuilder();
			DrawMode = DrawMode.OwnerDrawFixed;
		}

		private Size measureItem(int index, Graphics g) {
		    var item = (City) Items[index];

			//draw city name
			sb.Clear().Append(item.name).Append(", ");
			var nameString = sb.ToString();
			var nameSize = g.MeasureString(nameString, this.Font);

			//draw country name
			var countryString = sb.Clear().Append(item.country).Append(' ').ToString();
			var countrySize =  g.MeasureString(countryString, this.Font);

			//draw city code
			var codeString = item.code;
			var codeSize =  g.MeasureString(codeString, this.Font);

			var width = (int) Math.Ceiling(nameSize.Width + countrySize.Width + 10 + codeSize.Width);
			var height =  4 + (int) Math.Ceiling(
				Math.Max(Math.Max(nameSize.Height, countrySize.Height), codeSize.Height)
			);

			return new Size(width, height);
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e) {
			if(!(e.Index >= 0 && e.Index < Items.Count)) {
				base.OnMeasureItem(e);
			}
			else { 
				var size = measureItem(e.Index, e.Graphics);
				e.ItemWidth = size.Width;
				e.ItemHeight = size.Height;
			}
		}

		protected override void OnDrawItem(DrawItemEventArgs e) {
		    e.DrawBackground();
		    e.DrawFocusRectangle();

			if(!(e.Index >= 0 && e.Index < Items.Count)) return;
		
		    var item = (City) Items[e.Index];

			//draw city name
			sb.Clear().Append(item.name).Append(", ");
			var nameString = sb.ToString();
			var nameSize = e.Graphics.MeasureString(nameString, e.Font);
		    e.Graphics.DrawString(
				nameString, e.Font, new SolidBrush(e.ForeColor), 
				e.Bounds.Left, e.Bounds.Top + 2
			);

			//draw country name
			var countryString = sb.Clear().Append(item.country).Append(' ').ToString();
			var countrySize =  e.Graphics.MeasureString(countryString, e.Font);

			e.Graphics.DrawString(
				countryString, e.Font, new SolidBrush(ForeColor2), 
				e.Bounds.Left + nameSize.Width, e.Bounds.Top + 2
			);

			//draw city code
			var codeString = item.code;
			var codeSize =  e.Graphics.MeasureString(codeString, e.Font);

			e.Graphics.DrawString(
				codeString, e.Font, new SolidBrush(ForeColor), 
				e.Bounds.Right - codeSize.Width,
				e.Bounds.Top + 2
			);
		
		    base.OnDrawItem(e);
		}

		protected override void OnDropDown(EventArgs e) {
			//https://stackoverflow.com/a/11564932/18704284
		    int width = DropDownWidth;
		    using(
			Graphics g = CreateGraphics()) { 
		    Font font = Font;
		    int vertScrollBarWidth =  (Items.Count > MaxDropDownItems) 
				? SystemInformation.VerticalScrollBarWidth : 0;
			
		    for(int i = 0; i < this.Items.Count; i++) {
		        var newWidth = measureItem(i, g).Width + vertScrollBarWidth;
		        if(width < newWidth) width = newWidth;
		    }

		    DropDownWidth = width;
			}
			
			base.OnDropDown(e);
		}
	}
}
