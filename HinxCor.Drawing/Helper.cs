﻿using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace HinxCor.Drawing
{
    public static class Helper
    {
        public static Bitmap GetImage()
        {
            return default(Bitmap);
        }

        public static Bitmap CreateBitmapImage(string sImageText, Font objFont)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();
            return (objBmpImage);
        }

        public static Bitmap CreateBitmapImage(string sImageText)
        {
            Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            return CreateBitmapImage(sImageText, objFont);
            //Bitmap objBmpImage = new Bitmap(1, 1);

            //int intWidth = 0;
            //int intHeight = 0;

            //// Create the Font object for the image text drawing.

            //// Create a graphics object to measure the text's width and height.
            //Graphics objGraphics = Graphics.FromImage(objBmpImage);

            //// This is where the bitmap size is determined.
            //intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            //intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            //// Create the bmpImage again with the correct size for the text and font.
            //objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            //// Add the colors to the new bitmap.
            //objGraphics = Graphics.FromImage(objBmpImage);

            //// Set Background color
            //objGraphics.Clear(Color.White);
            //objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            //objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            //objGraphics.Flush();
            //return (objBmpImage);
        }

        public static Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Bitmap bmp = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {

                Font font = new Font(fontname, fontsize);
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();
            }
            return bmp;
        }
    }
}
