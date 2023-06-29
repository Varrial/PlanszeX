using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Planszex.Services
{
    public class ImageService
    {
        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static string GetImage(string name, int width, int height)
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, @$"wwwroot\img\Cache\{width}x{height}");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            basePath = Path.Combine(basePath, @$"{name}.png");
            if (!File.Exists(basePath))
            {
                string originalPath = Path.Combine(Environment.CurrentDirectory, @$"wwwroot\img\Products\{name}.png");
                Image image = Image.FromFile(originalPath);
                Bitmap bitmap = ResizeImage(image, width, height);
                bitmap.Save(basePath);
                return $"/img/Cache/{width}x{height}/{name}.png";
            }
            else return $"/img/Cache/{width}x{height}/{name}.png";

        }
    }
}
