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
					Math.Max(
						e.Bounds.Left + nameSize.Width + countrySize.Width, 
						e.Bounds.Right - codeSize.Width
					),
					e.Bounds.Top + 2
				);
		 
		        base.OnDrawItem(e);
		    }
		}
}
