using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HinxCor.Rendering.GDI
{
    using Math = System.Math;
    /// <summary>
    /// 
    /// </summary>
    public partial class ScreenPainter : Form
    {
        const char ESC = (char)27;
        private static PaintType paintType = PaintType.Arrow;

        private Stack<Image> OperateStack = new Stack<Image>();
        private Graphics backPrinter;
        private Graphics painter; List<PointF> points = new List<PointF>();
        Pen pen;
        PointF lineStart, lineEnd;
        /// <summary>
        /// 
        /// </summary>
        public int PenAlpha { get; set; }
        private bool mouseLeft { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScreenPainter()
        {
            InitializeComponent();
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            //全屏
            var sc = Screen.PrimaryScreen.Bounds;
            this.Size = sc.Size;
            this.WindowState = FormWindowState.Maximized;

            var screen = new ScreenCapture();
            this.BackgroundImage = screen.CaptureScreen();
            OperateStack.Push((Image)BackgroundImage.Clone());

            this.printer.Image = new Bitmap(sc.Width, sc.Height);
            painter = Graphics.FromImage(printer.Image);

            //painter.

            this.KeyPress += Drawer_KeyPress;
            printer.MouseDown += Printer_MouseDown;
            printer.MouseUp += Printer_MouseUp;
            printer.MouseMove += Printer_MouseMove;

            pen = new Pen(Color.RoyalBlue, 5);

            PointF p1 = new PointF(0, 0);
            PointF p2 = new PointF(100, 100);

            //LinearGradientBrush gradientBrush = new LinearGradientBrush(p1, p2, Color.OrangeRed, Color.Blue);
            //PathGradientBrush pathGradientBrush = new PathGradientBrush(new[] { new PointF(0, 0), new PointF(0, 100), new PointF(100, 100), new PointF(100, 0) });
            //pathGradientBrush.CenterColor = Color.Red;
            //pathGradientBrush.CenterPoint = new PointF(50, 50);

            //*******************************************************************

            //Blend blend = new Blend();
            //// Create point and positions arrays
            //float[] factArray = { 0.0f, 0.3f, 0.5f, 1.0f };
            //float[] posArray = { 0.0f, 0.2f, 0.6f, 1.0f };
            //// Set Factors and Positions properties of Blend
            //blend.Factors = factArray;
            //blend.Positions = posArray;

            //// Create path and add a rectangle
            //GraphicsPath path = new GraphicsPath();
            //Rectangle rect = new Rectangle(10, 20, 200, 200);
            //path.AddRectangle(rect);
            //// Create path gradient brush
            //PathGradientBrush rgBrush =
            //    new PathGradientBrush(path);
            //// Set Blend and FocusScales properties
            //rgBrush.Blend = blend;
            //rgBrush.FocusScales = new PointF(0.6f, 0.2f);
            //Color[] colors = { Color.Green };
            //// Set CenterColor and SurroundColors properties
            //rgBrush.CenterColor = Color.Red;
            //rgBrush.SurroundColors = colors;

            //******************************************************************

            PenAlpha = 125;
            pen = new Pen(Color.FromArgb(120, 255, 0, 0), 5);
        }


        private void Printer_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLeft)
            {
                //draw something
                DrawSomething(e);
            }
        }


        private void DrawSomething(MouseEventArgs e)
        {
            painter.Clear(Color.Transparent);
            switch (paintType)
            {
                case PaintType.Rectangle:
                    lineEnd = e.Location;
                    painter.DrawRectangle(pen, getRectangle(lineStart, lineEnd));
                    break;
                case PaintType.Ellipse:
                    lineEnd = e.Location;
                    painter.DrawEllipse(pen, getRectangleF(lineStart, lineEnd));
                    break;
                case PaintType.Arrow:
                    lineEnd = e.Location;
                    painter.DrawLine(getArrowPen(pen), lineStart, lineEnd);
                    break;
                case PaintType.StraightLine:
                    lineEnd = e.Location;
                    painter.DrawLine(pen, lineStart, lineEnd);
                    break;
                case PaintType.Dashed:
                    lineEnd = e.Location;
                    painter.DrawLine(getDashPen(pen), lineStart, lineEnd);
                    break;
                case PaintType.Lines:
                    points.Add(e.Location);
                    if (points.Count >= 2)
                    {
                        painter.Clear(Color.Transparent);
                        painter.DrawCurve(pen, points.ToArray());
                    }
                    break;
            }

            painter.Flush();
            Refresh();
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

        private Pen getDashPen(Pen pend)
        {
            var pen = new Pen(Color.FromArgb(PenAlpha, pend.Color), pend.Width);
            pen.DashStyle = DashStyle.DashDot;
            return pen;
        }




        private Pen getArrowPen(Pen pend)
        {
            var pen = new Pen(Color.FromArgb(PenAlpha, pend.Color), 10);
            pen.DashStyle = DashStyle.Solid;
            pen.DashCap = DashCap.Round;
            AdjustableArrowCap bigArrow = new AdjustableArrowCap(2, 2);
            pen.CustomEndCap = bigArrow;
            return pen;
        }




        private void StartDrawSomething(MouseEventArgs e)
        {
            switch (paintType)
            {
                case PaintType.Rectangle:
                    lineStart = e.Location;
                    break;
                case PaintType.Ellipse:
                    lineStart = e.Location;
                    break;
                case PaintType.Arrow:
                    lineStart = e.Location;
                    break;
                case PaintType.StraightLine:
                    lineStart = e.Location;
                    break;
                case PaintType.Dashed:
                    lineStart = e.Location;
                    break;
                case PaintType.Lines:
                    points.Add(e.Location);
                    break;
            }

        }


        /// <summary>
        /// 绘制终止
        /// </summary>
        private void EndDrawSomething()
        {
            backPrinter = Graphics.FromImage(BackgroundImage);
            switch (paintType)
            {
                case PaintType.Rectangle:
                    backPrinter.DrawRectangle(pen, getRectangle(lineStart, lineEnd));
                    break;
                case PaintType.Ellipse:
                    backPrinter.DrawEllipse(pen, getRectangleF(lineStart, lineEnd));
                    break;
                case PaintType.Arrow:
                    backPrinter.DrawLine(getArrowPen(pen), lineStart, lineEnd);
                    break;
                case PaintType.StraightLine:
                    backPrinter.DrawLine(pen, lineStart, lineEnd);
                    break;
                case PaintType.Dashed:
                    backPrinter.DrawLine(getDashPen(pen), lineStart, lineEnd);
                    break;
                case PaintType.Lines:
                    if (points.Count >= 2)
                        backPrinter.DrawCurve(pen, points.ToArray());
                    break;
            }

            painter.Clear(Color.Transparent);
            backPrinter.Flush();
            var img = (Image)BackgroundImage.Clone();
            OperateStack.Push(img);
            points = new List<PointF>();
        }


        private void Printer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLeft = false;
                EndDrawSomething();
            }
        }


        private void Printer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLeft = true;
                StartDrawSomething(e);
            }
        }


        private void Drawer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ESC) Quit();
            else if (e.KeyChar == 'z') Undo();
            switch (e.KeyChar)
            {
                case 'q':
                    paintType = PaintType.Rectangle;
                    break;
                case 'w':
                    paintType = PaintType.Ellipse;
                    break;
                case 'e':
                    paintType = PaintType.StraightLine;
                    break;
                case 'r':
                    paintType = PaintType.Dashed;
                    break;
                case 't':
                    paintType = PaintType.Arrow;
                    break;
                case 'y':
                    paintType = PaintType.Lines;
                    break;

            }
        }

        private void Undo()
        {
            if (OperateStack.Count > 0)
                BackgroundImage = OperateStack.Pop();
            if (OperateStack.Count == 0)
                OperateStack.Push((Image)BackgroundImage.Clone());
        }

        private void Quit()
        {
            this.Close();
        }
    }
}
