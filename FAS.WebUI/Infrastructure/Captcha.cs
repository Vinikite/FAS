using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FAS.WebUI.Infrastructure
{
    public class Captcha : Disposable
    {
        public const string CaptchaKey = "CaptchaImageText";

        public string Text { get; private set; }
        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        protected string FamilyName { get; set; }
        protected Random Random { get; set; }

        public Captcha(string text, int width, int height, string familyName)
        {
            Text = text;
            setDimensions(width, height);
            setFamilyName(familyName);
            generateImage();
        }

        protected override void DisposeCore()
        {
            if (Image != null)
                Image.Dispose();
        }

        private void setDimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", "Argument out of range, must be greater than zero.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", "Argument out of range, must be greater than zero.");
            Width = width;
            Height = height;
        }

        private void setFamilyName(string name)
        {
            try
            {
                Font font = new Font(name, 12f);
                FamilyName = name;
                font.Dispose();
            }
            catch (Exception)
            {
                FamilyName = FontFamily.GenericSerif.Name;
            }
        }

        private void generateImage()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            HatchBrush brush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);

            graphics.FillRectangle(brush, rectangle);

            SizeF size;
            Font font;
            float fontSize = rectangle.Height + 1;

            do
            {
                fontSize--;
                font = new Font(FamilyName, fontSize, FontStyle.Bold);
                size = graphics.MeasureString(Text, font);
            } while (size.Width > rectangle.Width);

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            GraphicsPath path = new GraphicsPath();
            path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, rectangle, format);
            float v = 4f;
            Random = new System.Random();
            PointF[] points =
            {
                new PointF(Random.Next(rectangle.Width) / v, Random.Next(rectangle.Height) / v),
                new PointF(rectangle.Width - Random.Next(rectangle.Width) / v, Random.Next(rectangle.Height) / v),
                new PointF(Random.Next(rectangle.Width) / v, rectangle.Height - Random.Next(rectangle.Height) / v),
                new PointF(rectangle.Width - Random.Next(rectangle.Width) / v, rectangle.Height - Random.Next(rectangle.Height) / v)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            path.Warp(points, rectangle, matrix, WarpMode.Perspective, 0f);

            brush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            graphics.FillPath(brush, path);

            int m = Math.Max(rectangle.Width, rectangle.Height);
            for (int i = 0; i < (int)(rectangle.Width * rectangle.Height / 30f); i++)
            {
                int x = Random.Next(rectangle.Width);
                int y = Random.Next(rectangle.Height);
                int w = Random.Next(m / 50);
                int h = Random.Next(m / 50);
                graphics.FillEllipse(brush, x, y, w, h);
            }

            font.Dispose();
            brush.Dispose();
            graphics.Dispose();

            Image = bitmap;
        }
    }
}