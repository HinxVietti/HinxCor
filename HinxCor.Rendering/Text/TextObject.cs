using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace HinxCor.Rendering.Text
{
    /// <summary>
    /// 文本对象;如需使用请手动设置 Rect,renderingHint.
    /// </summary>
    public class TextObject
    {
        private static Dictionary<Color, SolidBrush> m_Brushes;
        private static Dictionary<Color, SolidBrush> Brushes
        {
            get
            {
                if (m_Brushes == null) m_Brushes = new Dictionary<Color, SolidBrush>();
                return m_Brushes;
            }
            set
            {
                m_Brushes = value;
            }
        }

        /// <summary>
        /// 对象的文本内容
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 文本的颜色
        /// </summary>
        public Color fontColor { get; set; }
        /// <summary>
        /// 为本背景色
        /// </summary>
        public Color bgColor { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public float FontSize { get; set; }
        /// <summary>
        /// 字体名称
        /// </summary>
        public string fontName { get; set; }
        /// <summary>
        /// 文本格式
        /// </summary>
        public StringFormat format { get; set; }
        /// <summary>
        /// 字体样式
        /// </summary>
        public FontStyle FontStyle { get; set; }
        /// <summary>
        /// 行间距
        /// </summary>
        public float LineSpacing { get; set; }
        /// <summary>
        /// 字间距
        /// </summary>
        public float CharSpacing { get; set; }
        /// <summary>
        /// 绘制矩阵(区域)
        /// </summary>
        public Rectangle Rect { get; set; }
        /// <summary>
        /// 文本的字体
        /// </summary>
        public Font font
        {
            get
            {
                return new Font(this.fontName, this.FontSize, this.FontStyle);
            }
        }
        private Bitmap bmp;
        /// <summary>
        /// 虚拟的Graphic
        /// </summary>
        public Graphics graphics
        {
            get
            {
                if (bmp == null) bmp = new Bitmap(1, 1);
                return Graphics.FromImage(bmp);
            }
        }
        /// <summary>
        /// 空格的间距
        /// </summary>
        public float wordSpacing { get { return graphics.MeasureString(" ", font).Width; } }
        /// <summary>
        /// 绘制提示
        /// </summary>
        public TextRenderingHint renderingHint { get; set; }
        /// <summary>
        /// 垂直的字体;默认false
        /// </summary>
        public bool VerticalFont { get; set; }
        /// <summary>
        /// 字体笔刷
        /// </summary>
        public Brush Brush
        {
            get
            {
                if (!Brushes.ContainsKey(fontColor))
                    Brushes.Add(fontColor, new SolidBrush(fontColor));
                return Brushes[fontColor];
            }
        }
        /// <summary>
        /// 绘制文本BMP
        /// </summary>
        public Bitmap BMP { get { return Drawer(this); } }

        //public Func<string, Font, Rectangle, float, float, Brush, Color, StringFormat, TextRenderingHint, Bitmap> Drawer { get; set; }
        /// <summary>
        /// 绘制BMP的方法,默认使用内置,可以自定义
        /// </summary>
        public Func<TextObject, Bitmap> Drawer { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="align">字体对其方式</param>
        /// <param name="style">字体样式</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, Color fcolor, float fontsize, string fontname, StringAlignment align, FontStyle style, float linesp, float charsp)
        {
            Construct(text, fontColor, Color.Transparent, fontsize, fontname, align, style, linesp, charsp);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="align">字体对其方式</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, Color fcolor, float fontsize, string fontname, StringAlignment align, float linesp, float charsp)
        {
            Construct(text, fontColor, Color.Transparent, fontsize, fontname, align, FontStyle.Regular, linesp, charsp);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, Color fcolor, float fontsize, string fontname, float linesp, float charsp)
        {
            Construct(text, fontColor, Color.Transparent, fontsize, fontname, StringAlignment.Far, FontStyle.Regular, linesp, charsp);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, float fontsize, string fontname, float linesp, float charsp)
        {
            Construct(text, Color.Gray, Color.Transparent, fontsize, fontname, StringAlignment.Far, FontStyle.Regular, linesp, charsp);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        public TextObject(string text, float fontsize, string fontname)
        {
            Construct(text, Color.Gray, Color.Transparent, fontsize, fontname, StringAlignment.Far, FontStyle.Regular, 0, 0);
        }
        /// <summary>
        /// 构造
        /// </summary> 
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        public TextObject(float fontsize, string fontname)
        {
            Construct("HinxCor.Rendering.Text", Color.Gray, Color.Transparent, fontsize, fontname, StringAlignment.Far, FontStyle.Regular, 0, 0);
        }
        /// <summary>
        /// 构造
        /// </summary>
        public TextObject()
        {
            Construct("HinxCor.Rendering.Text", Color.Gray, Color.Transparent, 24, "微软雅黑", StringAlignment.Far, FontStyle.Regular, 0, 0);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(Color fcolor, float fontsize, string fontname, float linesp, float charsp)
        {
            Construct("HinxCor.Rendering.Text", fontColor, Color.Transparent, fontsize, fontname, StringAlignment.Far, FontStyle.Regular, linesp, charsp);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="bgcolor">背景色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="align">字体对其方式</param>
        /// <param name="style">字体样式</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, Color fcolor, Color bgcolor, float fontsize, string fontname, StringAlignment align, FontStyle style, float linesp, float charsp)
        {
            Construct(text, fontColor, bgcolor, fontsize, fontname, align, style, linesp, charsp);
            //StringFormat fm = StringFormat.GenericDefault;
            //fm.Alignment = align;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fcolor">文本颜色</param>
        /// <param name="bgcolor">背景色</param>
        /// <param name="fontsize">字号</param>
        /// <param name="fontname">字体名</param>
        /// <param name="format">文本格式</param>
        /// <param name="style">字体样式</param>
        /// <param name="linesp">行距</param>
        /// <param name="charsp">字间距</param>
        public TextObject(string text, Color fcolor, Color bgcolor, float fontsize, string fontname, StringFormat format, FontStyle style, float linesp, float charsp)
        {
            //this.text = text;
            //this.fontColor = fcolor;
            //this.bgColor = bgcolor;
            //this.FontSize = fontsize;
            //this.fontName = fontname;
            //this.format = format;
            //this.FontStyle = style;
            //this.LineSpacing = linesp;
            //this.CharSpacing = charsp;
            //this.font = new Font(this.fontName, this.FontSize, this.FontStyle);
            Construct(text, fontColor, bgcolor, fontsize, fontname, format, style, linesp, charsp);
        }

        private void Construct(string text, Color fcolor, Color bgcolor, float fontsize, string fontname, StringAlignment align, FontStyle style, float linesp, float charsp)
        {
            StringFormat fm = StringFormat.GenericDefault;
            fm.Alignment = align;
            Construct(text, fontColor, bgcolor, fontsize, fontname, fm, style, linesp, charsp);
        }
        private void Construct(string text, Color fcolor, Color bgcolor, float fontsize, string fontname, StringFormat format, FontStyle style, float linesp, float charsp)
        {
            this.text = text;
            this.fontColor = fcolor;
            this.bgColor = bgcolor;
            this.FontSize = fontsize;
            this.fontName = fontname;
            this.format = format;
            this.FontStyle = style;
            this.LineSpacing = linesp;
            this.CharSpacing = charsp;
            this.Rect = Rectangle.Empty;
            this.renderingHint = TextRenderingHint.AntiAlias;
            this.VerticalFont = false;
            //this.font = new Font(this.fontName, this.FontSize, this.FontStyle);
            this.Drawer = txtobj =>
            {
                return BitmapGenerator.DrawText(txtobj.text, txtobj.font, txtobj.Rect, txtobj.format, txtobj.LineSpacing,
                    txtobj.CharSpacing, txtobj.Brush, txtobj.bgColor, txtobj.renderingHint);
                ////TODO: 修复,该绘制方法有点bug fixed. 2019年1月3日15:59:05
                //return BitmapGenerator.DrawString(txtobj.text, txtobj.font, txtobj.Rect, txtobj.LineSpacing,
                //    txtobj.CharSpacing, txtobj.fontColor, txtobj.bgColor, txtobj.format, txtobj.renderingHint);
            };
        }

        /// <summary>
        /// 设置字体属性;
        /// FName;
        /// FSize;
        /// FStyle
        /// </summary>
        /// <param name="font"></param>
        public void SetFont(Font font)
        {
            this.fontName = font.Name;
            this.FontSize = font.Size;
            this.FontStyle = font.Style;
        }


    }
}
