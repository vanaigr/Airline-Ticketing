using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client {
	static class TintImage {
		public static Image applyTint(Image image, Color tint) {
			var iaPicture = new ImageAttributes();
			var colorMatrix = new ColorMatrix();
			colorMatrix.Matrix00 = tint.R / 255.0f;
			colorMatrix.Matrix11 = tint.G / 255.0f;
			colorMatrix.Matrix22 = tint.B / 255.0f;
			colorMatrix.Matrix33 = tint.A / 255.0f;
			colorMatrix.Matrix44 = 1;
			iaPicture.SetColorMatrix(colorMatrix);

			var newBitmap = new Bitmap(image.Width, image.Height);

			using(
			var gfxPicture = Graphics.FromImage(newBitmap)) {
			var rctPicture = new Rectangle(0, 0, image.Width, image.Height);
			gfxPicture.DrawImage(image, rctPicture, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, iaPicture);
			}

			return newBitmap;
		}	
	}
}
