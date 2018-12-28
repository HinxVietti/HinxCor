using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace HinxCor.Rendering
{
    /// <summary>
    /// BITMAP 生成工具
    /// </summary>
    public class BitmapGenerator
    {

        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            return GetBitmap(text, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, StringFormat format)
        {
            Font f = new Font("微软雅黑", 24);
            return GetBitmap(text, f, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font)
        {
            Color fcolor = Color.Gray;
            return GetBitmap(text, font, fcolor);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, StringFormat format)
        {
            Color gray = Color.Gray;
            return GetBitmap(text, font, gray, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Color fontColor)
        {
            Color backColor = Color.Transparent;
            return GetBitmap(text, font, fontColor, backColor);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Color fontColor, StringFormat format)
        {
            Color backColor = Color.Transparent;
            return GetBitmap(text, font, fontColor, backColor, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Color fontColor, Color backColor)
        {
            return GetBitmap(text, font, default(Rectangle), fontColor, backColor);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Color fontColor, Color backColor, StringFormat format)
        {
            return GetBitmap(text, font, default(Rectangle), fontColor, backColor, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="fontColor">字体颜色</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Rectangle rect, Color fontColor)
        {
            StringFormat format = new StringFormat();
            return GetBitmap(text, font, rect, fontColor, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Rectangle rect, Color fontColor, StringFormat format)
        {
            return GetBitmap(text, font, rect, fontColor, Color.Transparent, format);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Rectangle rect, Color fontColor, Color backColor, StringFormat format)
        {
            return GetBitmap(text, font, rect, fontColor, backColor, format, TextRenderingHint.AntiAlias);
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <param name="renderingHint">渲染标签</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Rectangle rect, Color fontColor, Color backColor, StringFormat format, TextRenderingHint renderingHint)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var size = g.MeasureString(text, font);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);

            g = Graphics.FromImage(bmp);

            //设置背景
            g.Clear(backColor);

            // 蚂蚁线填充边框
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // The interpolation mode determines how intermediate values between two endpoints are calculated.
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // This one is important
            g.TextRenderingHint = renderingHint;

            g.DrawString(text, font, new SolidBrush(fontColor), rect, format);

            g.Flush();
            return bmp;
        }
        /// <summary>
        /// 绘制bitmap 文本
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">绘制区域</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <returns>绘制结果</returns>
        public static Bitmap GetBitmap(string text, Font font, Rectangle rect, Color fontColor, Color backColor)
        {
            return GetBitmap(text, font, rect, fontColor, backColor, new StringFormat());
        }

        /// <summary>
        /// 在Graphic 中绘制 字符串,此处控制 TXT spacing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="c"></param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">文本区域</param>
        /// <param name="textSpacing"></param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <param name="renderingHint">渲染格式标签</param>
        public static void DrawChar(ref Graphics g, char c, Font font, ref Rectangle rect, float textSpacing, Color fontColor, StringFormat format, TextRenderingHint renderingHint)
        {
            string sc = c.ToString();
            float wid = g.MeasureString(sc, font).Width;
            g.DrawString(sc, font, new SolidBrush(fontColor), rect, format);
            g.Flush();
            rect.X += (int)(wid + textSpacing);
        }



        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str)
        {
            StringFormat format = new StringFormat();
            return DrawString(str, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, StringFormat format)
        {
            return DrawString(str, 0, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Color fontColor, StringFormat format)
        {
            return DrawString(str, 0, fontColor, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, float lineSpacing, StringFormat format)
        {
            return DrawString(str, lineSpacing, Color.Gray, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, float lineSpacing, Color fontColor, StringFormat format)
        {
            return DrawString(str, lineSpacing, 0, fontColor, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="charSpacing">字间距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, float lineSpacing, float charSpacing, Color fontColor, StringFormat format)
        {
            Font font = new Font("微软雅黑", 24);
            return DrawString(str, font, default(Rectangle), lineSpacing, charSpacing, fontColor, Color.Transparent, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="charSpacing">字间距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, float lineSpacing, float charSpacing, Color fontColor, StringFormat format)
        {
            return DrawString(str, font, default(Rectangle), lineSpacing, charSpacing, fontColor, Color.Transparent, format);
        }
        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="charSpacing">字间距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, float lineSpacing, float charSpacing, Color fontColor, Color backColor, StringFormat format)
        {
            return DrawString(str, font, default(Rectangle), lineSpacing, charSpacing, fontColor, backColor, format);
        }

        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">文本区域</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="charSpacing">字间距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, Rectangle rect, float lineSpacing, float charSpacing, Color fontColor, Color backColor, StringFormat format)
        {
            //AntiAlias 是最平滑锐利的
            return DrawString(str, font, rect, lineSpacing, charSpacing, fontColor, backColor, format, TextRenderingHint.AntiAlias);
        }

        /// <summary>
        /// 绘制bitmap文本;可以使用行距
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <param name="rect">文本区域</param>
        /// <param name="lineSpacing">行距</param>
        /// <param name="charSpacing">字间距</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <param name="format">文本格式</param>
        /// <param name="renderingHint">渲染格式标签</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, Rectangle rect, float lineSpacing, float charSpacing, Color fontColor, Color backColor, StringFormat format, TextRenderingHint renderingHint)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizef = rect.Width == 0 ? g.MeasureString(str, font) : g.MeasureString(str, font, rect.Width, format);
            //var lines = Regex.Split(str, System.Environment.NewLine); 当前windows 环境下 LF CR, 但是 CMD 输入只能是 LF
            var lines = SplitAsLine(str);
            int maxCharset = 0;
            for (int i = 0; i < lines.Length; i++)
                maxCharset = lines[i].Length > maxCharset ? lines[i].Length : maxCharset;

            sizef.Width += charSpacing * maxCharset;
            sizef.Height += lineSpacing * lines.Length;

            bmp = new Bitmap(bmp, (int)sizef.Width, (int)sizef.Height);
            g = Graphics.FromImage(bmp);

            g.Clear(backColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Action<char> drawc = c =>
            {
                string sc = c.ToString();
                float wid = g.MeasureString(sc, font).Width;
                g.DrawString(sc, font, new SolidBrush(fontColor), rect.X, rect.Y, format);

                g.Flush();
                rect.X += (int)(wid - 7 + charSpacing);
            };

            for (int i = 0; i < lines.Length; i++)
            {
                rect.Y = (int)((lineSpacing + font.Height) * i);//光标移到下一行
                rect.X = 0;
                for (int j = 0; j < lines[i].Length; j++)
                    drawc(lines[i][j]);
                //DrawChar(ref g, lines[i][j], font, ref rect, charSpacing, fontColor, format, renderingHint);
                g.Flush();//每一行写入一次
            }
            g.Flush();
            return bmp;
        }

        /// <summary>
        /// 不同情况下切割成多行文本
        /// </summary>
        /// <param name="str">原文本</param>
        /// <returns>行文本数组</returns>
        public static string[] SplitAsLine(string str)
        {
            var args = str.Split('\n', '\r');// \n \r 和 \n\r
            List<string> array = new List<string>();
            for (int i = 0; i < args.Length; i++)
                array.AddRange(Regex.Split(args[i], Environment.NewLine));
            return array.ToArray();
        }
    }
}
