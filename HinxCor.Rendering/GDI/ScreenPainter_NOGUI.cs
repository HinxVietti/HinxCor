using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HinxCor.Rendering
{
    using Math = System.Math;

    /// <summary>
    /// 没有GUI的屏幕绘制工具
    /// </summary>
    public class ScreenPainter_NOGUI
    {
        //Action<Bitmap> OnMainImageChanged;
        //Action<Bitmap> OnGlyphChanged;

        //public ScreenPainter_NOGUI(Action<Bitmap> onMainImageChanged, Action<Bitmap> onGlyphChanged)
        //{
        //    this.OnGlyphChanged = onGlyphChanged;
        //    this.OnMainImageChanged = onMainImageChanged;
        //}

        private Bitmap bitmap, bitmap_glyph;
        private Graphics glyph;
        private Graphics bmp;
        private Action<Graphics> PrintCmd;
        private Stack<Bitmap> BmpStack /*= new Stack<Bitmap>(10)*/;
        private bool UseStack = false;
        /// <summary>
        /// 平滑模式
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; }
        /// <summary>
        /// 获取主画面
        /// </summary>
        /// <returns></returns>
        public Bitmap GetMainContent()
        {
            return bitmap;
        }

        /// <summary>
        /// 获取当前笔画
        /// </summary>
        /// <returns></returns>
        public Bitmap GetGlyph()
        {
            return bitmap_glyph;
        }

        /// <summary>
        /// 是否可以Undo
        /// </summary>
        public bool Undoable { get; protected set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="hei"></param>
        /// <param name="smoothingMode"></param>
        /// <param name="usestack">不适用stack可以节省资源, false不使用</param>
        public ScreenPainter_NOGUI(int wid, int hei, SmoothingMode smoothingMode = SmoothingMode.HighQuality, bool usestack = false)
        {
            UseStack = usestack;
            if (usestack)
            {
                BmpStack = new Stack<Bitmap>();
            }
            SmoothingMode = SmoothingMode;
            bitmap = new Bitmap(wid, hei);
            bitmap_glyph = new Bitmap(wid, hei);
            remapbmp();
            glyph = Graphics.FromImage(bitmap_glyph);
            glyph.SmoothingMode = SmoothingMode;
        }


        /// <summary>
        /// 绘制笔画到主题内容
        /// </summary>
        public void DrawGlyph()
        {
            PrintCmd(bmp);
            Enstack();
        }

        /// <summary>
        /// 入栈
        /// </summary>
        private void Enstack()
        {
            if (!UseStack) return;
            Undoable = BmpStack.Count != 0;
            BmpStack.Push((Bitmap)bitmap.Clone());
        }

        /// <summary>
        /// 返回上一次编辑状态
        /// </summary>
        public void Undo()
        {
            if (!UseStack) return;
            if (BmpStack.Count > 0)
                bitmap = BmpStack.Pop();
            if (BmpStack.Count == 0)
                Enstack();
            remapbmp();
        }

        /// <summary>
        /// 清空绘制内容
        /// </summary>
        public void Clear()
        {
            bitmap = new Bitmap(bitmap.Width, bitmap.Height);
            remapbmp();
            bmp.Clear(Color.Transparent);
            Enstack();
        }

        /// <summary>
        /// 刷新graphic数据(re ref)
        /// </summary>
        private void remapbmp()
        {
            bmp = Graphics.FromImage(bitmap);
            bmp.SmoothingMode = SmoothingMode;
        }

        /// <summary>
        /// 绘制内容到笔画
        /// </summary>
        /// <param name="Cmd"></param>
        /// <param name="isDown"></param>
        public void Handle(IPainterCmd Cmd, bool isDown = true)
        {
            if (!Cmd.CouldHandle) return;
            switch (Cmd.Type)
            {
                case PaintType.Rectangle:
                    var tpdr = Cmd as PCmdData_2p;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g =>
                        g.DrawRectangle(tpdr.Pen, getRectangle(tpdr.Start, tpdr.End));
                    break;
                case PaintType.Ellipse:
                    var tpdc = Cmd as PCmdData_2p;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g => g.DrawEllipse(tpdc.Pen, getRectangleF(tpdc.Start, tpdc.End));
                    break;
                case PaintType.Arrow:
                    var tpda = Cmd as PCmdData_2p;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g => g.DrawLine(tpda.Pen, tpda.Start, tpda.End);
                    break;
                case PaintType.StraightLine:
                    var tpds = Cmd as PCmdData_2p;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g => g.DrawLine(tpds.Pen, tpds.Start, tpds.End);
                    break;
                case PaintType.Dashed:
                    tpds = Cmd as PCmdData_2p;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g => g.DrawLine(tpds.Pen, tpds.Start, tpds.End);
                    break;
                case PaintType.Lines:
                    var tpdls = Cmd as PCmdData_Lines;
                    glyph.Clear(Color.Transparent);
                    PrintCmd = g => g.DrawCurve(tpdls.Pen, tpdls.Points);
                    break;
                default:
                    throw new Exception("error.." + Cmd.Type);
            }
            PrintCmd(glyph);
            glyph.Flush();

        }

        private Rectangle getRectangle(PointF lineStart, PointF lineEnd)
        {
            return new Rectangle((int)Math.Min(lineStart.X, lineEnd.X), (int)Math.Min(lineStart.Y, lineEnd.Y),
                   (int)Math.Abs(lineStart.X - lineEnd.X), (int)Math.Abs(lineStart.Y - lineEnd.Y));
        }

        private RectangleF getRectangleF(PointF lineStart, PointF lineEnd)
        {
            return new RectangleF(Math.Min(lineStart.X, lineEnd.X), Math.Min(lineStart.Y, lineEnd.Y),
                Math.Abs(lineStart.X - lineEnd.X), Math.Abs(lineStart.Y - lineEnd.Y));
        }
    }
}