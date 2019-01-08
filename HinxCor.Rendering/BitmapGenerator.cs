using HinxCor.Rendering.Text;
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
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
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
            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    rect.X = 0;
                    break;
                case StringAlignment.Center:
                    rect.X = bmp.Width / 2;
                    break;
                case StringAlignment.Far:
                    rect.X = bmp.Width;
                    break;
            }
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
        /// <param name="charSpacing">字符间距</param>
        /// <param name="brush">字体笔刷</param>
        /// <param name="format">文本格式</param>
        public static void DrawChar(ref Graphics g, char c, Font font, ref Rectangle rect, float charSpacing, Brush brush, StringFormat format)
        {
            //string sc = c.ToString();
            //float wid = g.MeasureString(sc, font).Width;
            //g.DrawString(sc, font, new SolidBrush(fontColor), rect, format);
            //g.Flush();
            //rect.X += (int)(wid + textSpacing);

            string sc = c.ToString();
            float wid = g.MeasureString(sc.Replace(' ', 'B'), font).Width;
            g.DrawString(sc, font, brush, rect.X, rect.Y, format);
            //g.Flush();
            rect.X += (int)(wid - defspacing + charSpacing);
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
        /// <param name="text">文本内容</param>
        /// <param name="font">文本字体</param>
        /// <returns></returns>
        public static Bitmap DrawString(string text, Font font)
        {
            return DrawString(text, font, 0, 0, Color.Gray, StringFormat.GenericTypographic);
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
        /// 绘制bitmap文本;
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="rect"></param>
        /// <param name="renderingHint"></param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, Rectangle rect, TextRenderingHint renderingHint)
        {
            return DrawString(str, font, rect, 0, 0, Color.Gray, Color.Transparent, StringFormat.GenericDefault, renderingHint);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="rect"></param>
        /// <param name="renderingHint"></param>
        /// <returns></returns>
        public static Bitmap DrawString(Bitmap bmp, string str, Font font, Rectangle rect, TextRenderingHint renderingHint)
        {

            return DrawString(bmp, str, font, rect, 0, 0, Color.Gray, Color.Transparent, StringFormat.GenericDefault, renderingHint);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="rect"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="charSpacing"></param>
        /// <param name="fontColor"></param>
        /// <param name="backColor"></param>
        /// <param name="format"></param>
        /// <param name="renderingHint"></param>
        /// <returns></returns>
        public static Bitmap DrawString(Bitmap bmp, string str, Font font, Rectangle rect, float lineSpacing, float charSpacing, Color fontColor, Color backColor, StringFormat format, TextRenderingHint renderingHint)
        {
            str = str.Replace('\r', ' ');
            Graphics g = Graphics.FromImage(bmp);
            var lines = SplitAsLine(str);

            var brush = new SolidBrush(fontColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            defspacing = g.MeasureString(" ", font).Width;
            int prest = rect.X;
            g.TextRenderingHint = renderingHint;
            for (int i = 0; i < lines.Length; i++)
            {
                rect.Y += (int)((lineSpacing + font.Height));
                rect.X = prest;
                for (int j = 0; j < lines[i].Length; j++)
                    DrawChar(ref g, lines[i][j], font, ref rect, charSpacing, brush, format);
                g.Flush();
            }
            g.Flush();
            return bmp;
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
            str = str.Replace('\r', ' ');
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizef = rect.Width == 0 ? g.MeasureString(str, font) : g.MeasureString(str, font, rect.Width, format);
            //var lines = Regex.Split(str, System.Environment.NewLine); 当前windows 环境下 LF CR, 但是 CMD 输入只能是 LF
            var lines = SplitAsLine(str);
            int maxCharset = 0;
            for (int i = 0; i < lines.Length; i++)
                maxCharset = lines[i].Length > maxCharset ? lines[i].Length : maxCharset;

            sizef.Width += charSpacing * (maxCharset - 1);
            sizef.Height += lineSpacing * (lines.Length - 1);

            bmp = new Bitmap(bmp, (int)sizef.Width, (int)sizef.Height);
            g = Graphics.FromImage(bmp);

            return null;
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
        /// <param name="renderingHint">渲染格式标签</param>
        /// <returns></returns>
        public static Bitmap DrawString(string str, Font font, Rectangle rect, float lineSpacing, float charSpacing, Color fontColor, Color backColor, TextRenderingHint renderingHint)
        {
            var format = StringFormat.GenericDefault;
            str = str.Replace('\r', ' ');
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var sizef = rect.Width == 0 ? g.MeasureString(str, font) : g.MeasureString(str, font, rect.Width, format);
            //var lines = Regex.Split(str, System.Environment.NewLine); 当前windows 环境下 LF CR, 但是 CMD 输入只能是 LF
            var lines = SplitAsLine(str);
            int maxCharset = 0;
            for (int i = 0; i < lines.Length; i++)
                maxCharset = lines[i].Length > maxCharset ? lines[i].Length : maxCharset;

            //末尾 不用添加空白
            sizef.Width += charSpacing * (maxCharset - 1);
            sizef.Height += lineSpacing * (lines.Length - 1);

            bmp = new Bitmap(bmp, (int)sizef.Width, (int)sizef.Height);
            g = Graphics.FromImage(bmp);

            var brush = new SolidBrush(fontColor);

            g.Clear(backColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //-->弃用匿名函数 :181229 10.10
            //Action<char> drawc = c =>
            //{
            //    string sc = c.ToString();
            //    float wid = g.MeasureString(sc.Replace(' ', 'B'), font).Width;
            //    g.DrawString(sc, font, brush, rect.X, rect.Y, format);
            //    //g.Flush();
            //    rect.X += (int)(wid - defspacing + charSpacing);
            //};

            defspacing = g.MeasureString(" ", font).Width;
            g.TextRenderingHint = renderingHint;
            for (int i = 0; i < lines.Length; i++)
            {
                rect.Y = (int)((lineSpacing + font.Height) * i);//光标移到下一行
                rect.X = 0;
                for (int j = 0; j < lines[i].Length; j++)
                    //drawc(lines[i][j]); //-->改用 本地函数 :181229 10.10
                    DrawChar(ref g, lines[i][j], font, ref rect, charSpacing, brush, format);
                g.Flush();//每一行写入一次
            }
            g.Flush();
            return bmp;
        }

        private static float defspacing;

        /// <summary>
        /// 不同情况下切割成多行文本
        /// </summary>
        /// <param name="str">原文本</param>
        /// <returns>行文本数组</returns>
        public static string[] SplitAsLine(string str)
        {
            var args = Regex.Split(str, System.Environment.NewLine);
            List<string> array = new List<string>();
            for (int i = 0; i < args.Length; i++)
                array.AddRange(args[i].Split('\n'/*, '\r'*/)); // not linux

            //var args = str.Split('\n', '\r');// \n \r 和 \n\r
            //for (int i = 0; i < args.Length; i++)
            //    array.AddRange(Regex.Split(args[i], Environment.NewLine));
            return array.ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="rect"></param>
        /// <param name="format"></param>
        /// <param name="lineSpacing"></param>
        /// <param name="charSpacing"></param>
        /// <param name="brush"></param>
        /// <param name="backColor"></param>
        /// <param name="renderingHint"></param>
        /// <returns></returns>
        public static Bitmap DrawText(string str, Font font, Rectangle rect, StringFormat format, float lineSpacing, float charSpacing, Brush brush, Color backColor, TextRenderingHint renderingHint)//
        {
            str = str.Replace('\r', ' ');//防止为本不和谐,/r置空
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var lines = SplitAsLine(str);

            //应该还是得限定rect
            bool isSizeAble = false;
            if (rect.Width == 0)
            {
                int maxCharset = 0;
                for (int i = 0; i < lines.Length; i++)
                    maxCharset = lines[i].Length > maxCharset ? lines[i].Length : maxCharset;

                var sizef = /*rect.Width == 0 ?*/ g.MeasureString(str, font)/* : g.MeasureString(str, font, rect.Width, format)*/;
                sizef.Width += charSpacing * (maxCharset - 1);
                sizef.Height += lineSpacing * (lines.Length - 1);
                bmp = new Bitmap(bmp, (int)sizef.Width, (int)sizef.Height);
                isSizeAble = false;
            }
            else
            {
                bmp = new Bitmap(bmp, rect.Size);
                isSizeAble = true;
            }
            g = Graphics.FromImage(bmp);
            //charSpacing -= 
            var kk = g.MeasureString("a", font).Width + g.MeasureString("a", font).Width - g.MeasureString("aa", font).Width;
            //var hh = g.MeasureString("a\na", font).Width + g.MeasureString("a\na", font).Width - g.MeasureString("a\na\na\na", font).Width;

            //graphic 设置
            g.Clear(backColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = renderingHint;

            var lineHei = (font.Height + lineSpacing) * 0.98f;//行距微调
            var align = Totxtalignment(format.Alignment);
            //字符和字符之间的默认间距
            if (!isSizeAble)
            {
                //不限制大小绘制
                PointF p = new PointF(0, 0);//开始位置
                for (int i = 0; i < lines.Length; i++)
                {
                    //都是从左到右遍历,但是起始位置不一样
                    //var lwid = g.MeasureString(lines[i], font).Width + (lines.Length - 1) * (charSpacing) + g.MeasureString(lines[i][0].ToString(), font).Width;
                    var lwid = getLineLength(g, lines[i], font, charSpacing);
                    switch (align)
                    {
                        case TextAlignment.Left:
                            p.X = 0;
                            break;
                        case TextAlignment.Center:
                            p.X = (bmp.Width - lwid) / 2;
                            break;
                        case TextAlignment.Right:
                            p.X = (bmp.Width - lwid);
                            break;
                        default:
                            throw new Exception("对齐方式有问题,不能为空");
                    }
                    p.Y = i * (lineHei);
                    DrawLine(ref g, font, brush, lines[i], p, charSpacing - kk);
                    g.Flush();
                }
            }
            else
            {
                throw new Exception("暂不支持制定区域绘制文字");
            }
            return bmp;
        }

        private static float getLineLength(Graphics g, string str, Font f, float charSpacing)
        {
            return g.MeasureString(str, f).Width + charSpacing * (str.Length - 1);
        }

        private static TextAlignment Totxtalignment(StringAlignment align)
        {
            switch (align)
            {
                case StringAlignment.Near:
                    return TextAlignment.Left;
                case StringAlignment.Center:
                    return TextAlignment.Center;
                case StringAlignment.Far:
                    return TextAlignment.Right;
                default:
                    return TextAlignment.Left;
            }
        }

        /// <summary>
        /// 在graphic上绘制一行,可以charSpacing拓展
        /// </summary>
        /// <param name="g"></param>
        /// <param name="f"></param>
        /// <param name="brush"></param>
        /// <param name="text"></param>
        /// <param name="point"></param>
        /// <param name="charSpacing"></param>
        public static void DrawLine(ref Graphics g, Font f, Brush brush, string text, PointF point, float charSpacing)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string c = text[i].ToString();
                DrawChar(ref g, c, point, brush, f);
                float wid = g.MeasureString(c == " " ? "a" : c, f).Width + charSpacing;
                point.X += wid;
            }
        }

        /// <summary>
        /// 在graphic上绘制字符
        /// </summary>
        /// <param name="g"></param>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="f"></param>
        public static void DrawChar(ref Graphics g, string c, PointF p, Brush b, Font f)
        {
            g.DrawString(c, f, b, p);
        }
    }
}
