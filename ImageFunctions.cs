// The NiTiS-Dev licenses this file to you under the MIT license.

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageResizer;
public static class ImageFunctions
{
    public static Bitmap ResizeImage(Image image, int width, int height, InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic)
    {
		Rectangle destRect = new Rectangle(0, 0, width, height);
		Bitmap destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = interpolationMode;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

			using ImageAttributes wrapMode = new();
			wrapMode.SetWrapMode(WrapMode.TileFlipXY);
			graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
		}

        return destImage;
    }
}
