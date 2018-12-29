using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using SharpFont;
using SharpFont.Gdi;

namespace HinxCor.Drawing
{
    /// <summary>
    /// fz
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 获取一个默认的bmp
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetImage()
        {
            return default(Bitmap);
        }

        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <returns></returns>
        public static Bitmap CreateBitmapImage()
        {
            string infoString = "";  // enough space for one line of output
            int ascent;             // font family ascent in design units
            float ascentPixel;      // ascent converted to pixels
            int descent;            // font family descent in design units
            float descentPixel;     // descent converted to pixels
            int lineSpacing;        // font family line spacing in design units
            float lineSpacingPixel; // line spacing converted to pixels

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               16, FontStyle.Regular,
               GraphicsUnit.Pixel);
            PointF pointF = new PointF(10, 10);
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            // Display the font size in pixels.
            infoString = "font.Size returns " + font.Size + ".";
            // e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the font family em height in design units.
            infoString = "fontFamily.GetEmHeight() returns " +
               fontFamily.GetEmHeight(FontStyle.Regular) + ".";
            // e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down two lines.
            pointF.Y += 2 * font.Height;

            // Display the ascent in design units and pixels.
            ascent = fontFamily.GetCellAscent(FontStyle.Regular);

            // 14.484375 = 16.0 * 1854 / 2048
            ascentPixel =
               font.Size * ascent / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The ascent is " + ascent + " design units, " + ascentPixel +
               " pixels.";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the descent in design units and pixels.
            descent = fontFamily.GetCellDescent(FontStyle.Regular);

            // 3.390625 = 16.0 * 434 / 2048
            descentPixel =
               font.Size * descent / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The descent is " + descent + " design units, " +
               descentPixel + " pixels.";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the line spacing in design units and pixels.
            lineSpacing = fontFamily.GetLineSpacing(FontStyle.Regular);

            // 18.398438 = 16.0 * 2355 / 2048
            lineSpacingPixel =
            font.Size * lineSpacing / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The line spacing is " + lineSpacing + " design units, " +
               lineSpacingPixel + " pixels.";

            Bitmap bmp = new Bitmap(1, 1);
            var bmpGraphic = Graphics.FromImage(bmp);
            int intWidth = (int)bmpGraphic.MeasureString(infoString, font).Width;
            int intHeight = (int)bmpGraphic.MeasureString(infoString, font).Height;

            bmp = new Bitmap(bmp, new Size(intWidth, intHeight));
            bmpGraphic.DrawString(infoString, font, solidBrush, pointF);
            // Create the bmpImage again with the correct size for the text and font.
            return bmp;
        }

        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <param name="sImageText"></param>
        /// <param name="objFont"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmapImage(string sImageText, Font objFont)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);
            //
            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;
            intWidth *= 20;
            intHeight *= 20;
            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 20, 20);
            objGraphics.Flush();
            return (objBmpImage);
        }
        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <param name="sImageText"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <param name="str"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void DrawString(string str, int x, int y)
        {

        }

        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="c"></param>
        /// <param name="f"></param>
        /// <param name="b"></param>
        public static void DrawChar(ref Bitmap bmp, char c, Font f, Brush b)
        {
            string s = c.ToString();
            Graphics g = Graphics.FromImage(bmp);
            var sf = g.MeasureString(s, f);
            g.DrawString(s, f, b, 5, 5);
            g.Flush();
            //return bmp;
        }

        /// <summary>
        /// 创建bmp
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="fontname"></param>
        /// <param name="fontsize"></param>
        /// <param name="bgcolor"></param>
        /// <param name="fcolor"></param>
        /// <param name="width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
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


        internal static Bitmap RenderString(Library library, Face face, string text, Color foreColor, Color backColor)
        {
            var measuredChars = new List<DebugChar>();
            var renderedChars = new List<DebugChar>();
            float penX = 0, penY = 0;
            float stringWidth = 0; // the measured width of the string
            float stringHeight = 0; // the measured height of the string
            float overrun = 0;
            float underrun = 0;
            float kern = 0;
            int spacingError = 0;
            bool trackingUnderrun = true;
            int rightEdge = 0; // tracking rendered right side for debugging

            // Bottom and top are both positive for simplicity.
            // Drawing in .Net has 0,0 at the top left corner, with positive X to the right
            // and positive Y downward.
            // Glyph metrics have an origin typically on the left side and at baseline
            // of the visual data, but can draw parts of the glyph in any quadrant, and
            // even move the origin (via kerning).
            float top = 0, bottom = 0;

            // Measure the size of the string before rendering it. We need to do this so
            // we can create the proper size of bitmap (canvas) to draw the characters on.
            for (int i = 0; i < text.Length; i++)
            {
                #region Load character
                char c = text[i];

                // Look up the glyph index for this character.
                uint glyphIndex = face.GetCharIndex(c);

                // Load the glyph into the font's glyph slot. There is usually only one slot in the font.
                face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

                // Refer to the diagram entitled "Glyph Metrics" at http://www.freetype.org/freetype2/docs/tutorial/step2.html.
                // There is also a glyph diagram included in this example (glyph-dims.svg).
                // The metrics below are for the glyph loaded in the slot.
                float gAdvanceX = (float)face.Glyph.Advance.X; // same as the advance in metrics
                float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
                float gWidth = face.Glyph.Metrics.Width.ToSingle();
                var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
                #endregion
                #region Underrun
                // Negative bearing would cause clipping of the first character
                // at the left boundary, if not accounted for.
                // A positive bearing would cause empty space.
                underrun += -(gBearingX);
                if (stringWidth == 0)
                    stringWidth += underrun;
                if (trackingUnderrun)
                    rc.Underrun = underrun;
                if (trackingUnderrun && underrun <= 0)
                {
                    underrun = 0;
                    trackingUnderrun = false;
                }
                #endregion
                #region Overrun
                // Accumulate overrun, which coould cause clipping at the right side of characters near
                // the end of the string (typically affects fonts with slanted characters)
                if (gBearingX + gWidth > 0 || gAdvanceX > 0)
                {
                    overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
                    if (overrun <= 0) overrun = 0;
                }
                overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
                // On the last character, apply whatever overrun we have to the overall width.
                // Positive overrun prevents clipping, negative overrun prevents extra space.
                if (i == text.Length - 1)
                    stringWidth += overrun;
                rc.Overrun = overrun; // accumulating (per above)
                #endregion

                #region Top/Bottom
                // If this character goes higher or lower than any previous character, adjust
                // the overall height of the bitmap.
                float glyphTop = (float)face.Glyph.Metrics.HorizontalBearingY;
                float glyphBottom = (float)(face.Glyph.Metrics.Height - face.Glyph.Metrics.HorizontalBearingY);
                if (glyphTop > top)
                    top = glyphTop;
                if (glyphBottom > bottom)
                    bottom = glyphBottom;
                #endregion

                // Accumulate the distance between the origin of each character (simple width).
                stringWidth += gAdvanceX;
                rc.RightEdge = stringWidth;
                measuredChars.Add(rc);

                #region Kerning (for NEXT character)
                // Calculate kern for the NEXT character (if any)
                // The kern value adjusts the origin of the next character (positive or negative).
                if (face.HasKerning && i < text.Length - 1)
                {
                    char cNext = text[i + 1];
                    kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                    // sanity check for some fonts that have kern way out of whack
                    if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
                        kern = 0;
                    rc.Kern = kern;
                    stringWidth += kern;
                }

                #endregion
            }

            stringHeight = top + bottom;

            // If any dimension is 0, we can't create a bitmap
            if (stringWidth == 0 || stringHeight == 0)
                return null;

            // Create a new bitmap that fits the string.
            Bitmap bmp = new Bitmap((int)Math.Ceiling(stringWidth), (int)Math.Ceiling(stringHeight));
            trackingUnderrun = true;
            underrun = 0;
            overrun = 0;
            stringWidth = 0;
            using (var g = Graphics.FromImage(bmp))
            {
                #region Set up graphics
                // HighQuality and GammaCorrected both specify gamma correction be applied (2.2 in sRGB)
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms534094(v=vs.85).aspx
                g.CompositingQuality = CompositingQuality.HighQuality;
                // HighQuality and AntiAlias both specify antialiasing
                g.SmoothingMode = SmoothingMode.HighQuality;
                // If a background color is specified, blend over it.
                g.CompositingMode = CompositingMode.SourceOver;

                g.Clear(backColor);
                #endregion

                // Draw the string into the bitmap.
                // A lot of this is a repeat of the measuring steps, but this time we have
                // an actual bitmap to work with (both canvas and bitmaps in the glyph slot).
                for (int i = 0; i < text.Length; i++)
                {
                    #region Load character
                    char c = text[i];

                    // Same as when we were measuring, except RenderGlyph() causes the glyph data
                    // to be converted to a bitmap.
                    uint glyphIndex = face.GetCharIndex(c);
                    face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
                    face.Glyph.RenderGlyph(RenderMode.Normal);
                    FTBitmap ftbmp = face.Glyph.Bitmap;

                    float gAdvanceX = (float)face.Glyph.Advance.X;
                    float gBearingX = (float)face.Glyph.Metrics.HorizontalBearingX;
                    float gWidth = (float)face.Glyph.Metrics.Width;

                    var rc = new DebugChar(c, gAdvanceX, gBearingX, gWidth);
                    #endregion
                    #region Underrun
                    // Underrun
                    underrun += -(gBearingX);
                    if (penX == 0)
                        penX += underrun;
                    if (trackingUnderrun)
                        rc.Underrun = underrun;
                    if (trackingUnderrun && underrun <= 0)
                    {
                        underrun = 0;
                        trackingUnderrun = false;
                    }
                    #endregion
                    #region Draw glyph
                    // Whitespace characters sometimes have a bitmap of zero size, but a non-zero advance.
                    // We can't draw a 0-size bitmap, but the pen position will still get advanced (below).
                    if ((ftbmp.Width > 0 && ftbmp.Rows > 0))
                    {
                        // Get a bitmap that .Net can draw (GDI+ in this case).
                        Bitmap cBmp = ftbmp.ToGdipBitmap(foreColor);
                        rc.Width = cBmp.Width;
                        rc.BearingX = face.Glyph.BitmapLeft;
                        int x = (int)Math.Round(penX + face.Glyph.BitmapLeft);
                        int y = (int)Math.Round(penY + top - (float)face.Glyph.Metrics.HorizontalBearingY);
                        //Not using g.DrawImage because some characters come out blurry/clipped. (Is this still true?)
                        g.DrawImageUnscaled(cBmp, x, y);
                        rc.Overrun = face.Glyph.BitmapLeft + cBmp.Width - gAdvanceX;
                        // Check if we are aligned properly on the right edge (for debugging)
                        rightEdge = Math.Max(rightEdge, x + cBmp.Width);
                        spacingError = bmp.Width - rightEdge;
                    }
                    else
                    {
                        rightEdge = (int)(penX + gAdvanceX);
                        spacingError = bmp.Width - rightEdge;
                    }
                    #endregion

                    #region Overrun
                    if (gBearingX + gWidth > 0 || gAdvanceX > 0)
                    {
                        overrun -= Math.Max(gBearingX + gWidth, gAdvanceX);
                        if (overrun <= 0) overrun = 0;
                    }
                    overrun += (float)(gBearingX == 0 && gWidth == 0 ? 0 : gBearingX + gWidth - gAdvanceX);
                    if (i == text.Length - 1) penX += overrun;
                    rc.Overrun = overrun;
                    #endregion

                    // Advance pen positions for drawing the next character.
                    penX += (float)face.Glyph.Advance.X; // same as Metrics.HorizontalAdvance?
                    penY += (float)face.Glyph.Advance.Y;

                    rc.RightEdge = penX;
                    spacingError = bmp.Width - (int)Math.Round(rc.RightEdge);
                    renderedChars.Add(rc);

                    #region Kerning (for NEXT character)
                    // Adjust for kerning between this character and the next.
                    if (face.HasKerning && i < text.Length - 1)
                    {
                        char cNext = text[i + 1];
                        kern = (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                        if (kern > gAdvanceX * 5 || kern < -(gAdvanceX * 5))
                            kern = 0;
                        rc.Kern = kern;
                        penX += (float)kern;
                    }
                    #endregion

                }

            }
            bool printedHeader = false;
            if (spacingError != 0)
            {
                for (int i = 0; i < renderedChars.Count; i++)
                {
                    //if (measuredChars[i].RightEdge != renderedChars[i].RightEdge)
                    //{
                    if (!printedHeader)
                        DebugChar.PrintHeader();
                    printedHeader = true;
                    Debug.Print(measuredChars[i].ToString());
                    Debug.Print(renderedChars[i].ToString());
                    //}
                }
                string msg = string.Format("Right edge: {0,3} ({1}) {2}",
                    spacingError,
                    spacingError == 0 ? "perfect" : spacingError > 0 ? "space  " : "clipped",
                    face.FamilyName);
                System.Diagnostics.Debug.Print(msg);
                //throw new ApplicationException(msg);
            }
            return bmp;
        }

        #region class DebugChar

        private class DebugChar
        {
            public char Char { get; set; }
            public float AdvanceX { get; set; }
            public float BearingX { get; set; }
            public float Width { get; set; }
            public float Underrun { get; set; }
            public float Overrun { get; set; }
            public float Kern { get; set; }
            public float RightEdge { get; set; }
            internal DebugChar(char c, float advanceX, float bearingX, float width)
            {
                this.Char = c; this.AdvanceX = advanceX; this.BearingX = bearingX; this.Width = width;
            }

            public override string ToString()
            {
                return string.Format("'{0}' {1,5:F0} {2,5:F0} {3,5:F0} {4,5:F0} {5,5:F0} {6,5:F0} {7,5:F0}",
                    this.Char, this.AdvanceX, this.BearingX, this.Width, this.Underrun, this.Overrun,
                    this.Kern, this.RightEdge);
            }
            public static void PrintHeader()
            {
                Debug.Print("    {1,5} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5}",
                    "", "adv", "bearing", "wid", "undrn", "ovrrn", "kern", "redge");
            }
        }

        #endregion
    }
}
