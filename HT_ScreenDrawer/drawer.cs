﻿using GDIPlus;
using HinxCor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HT_ScreenDrawer
{
    public partial class drawer : Form
    {
        const char ESC = (char)27;
        private static PaintType paintType = PaintType.Arrow;

        private Stack<Image> OperateStack = new Stack<Image>();
        private Graphics backPrinter;
        private Graphics painter; List<PointF> points = new List<PointF>();
        Pen pen;
        PointF lineStart, lineEnd;

        public int PenAlpha { get; set; }
        private bool mouseLeft { get; set; }


        public drawer()
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



#region 弃用的代码

/*

private enum DrawType
{
    Rectangle,
    Ellipse,
    Arrow,
    DirectLine,
    Dashed,
    Custom
}

const char ESC = (char)27;

private void InitializeWindow()
{
    FullScreen();
    RegisterEvent();
    InitialzePainter();
}

Pen pen;

private void InitialzePainter()
{
    pen = new Pen(Color.Indigo, 5);

    var sc = Screen.PrimaryScreen.Bounds;
    Width = sc.Width;
    Height = sc.Height;


    var img = new Bitmap(this.Width, this.Height);
    var g = Graphics.FromImage(img);
    g.Clear(Color.Red);
    g.Flush();
    printer.Image = img;
    //printer.Image = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
    drawor.Image = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

}

private void RegisterEvent()
{
    this.drawor.MouseDown += Drawer_MouseDown;
    this.drawor.MouseMove += Drawer_MouseMove;
    this.drawor.MouseUp += Drawer_MouseUp;

    this.KeyPress += Drawer_KeyPress;
}

private void Drawer_KeyPress(object sender, KeyPressEventArgs e)
{
    if (e.KeyChar == ESC) this.Close();
    else if (e.KeyChar == 'z') Undo();
}

private void Undo()
{
    throw new NotImplementedException();
}

private bool mouseLeft;
private List<PointF> points = new List<PointF>();
private Graphics Graphic, corGraphic;

private void Drawer_MouseMove(object sender, MouseEventArgs e)
{
    if (mouseLeft)
    {
        //鼠标拖拽，绘制内容
        points.Add(e.Location);
        Graphic.Clear(Color.Transparent);
        Graphic.DrawLines(pen, points.ToArray());
        Graphic.Flush();
        Refresh();
    }
}

private void Drawer_MouseUp(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        mouseLeft = false;
        BackUp();
    }

}

Rectangle rect = new Rectangle(0, 0, 1920, 1080);
Stack<Image> Images = new Stack<Image>();

private void BackUp()
{
    Graphic.Clear(Color.Transparent);

    //corGraphic = Graphics.FromImage(printer.Image);
    //corGraphic.DrawLines(pen, points.ToArray());
    //corGraphic.Flush();
    Refresh();
    //B -> A 
    //FastBitmap fb = ((Bitmap)drawor.Image).Clone(rect, PixelFormat.Format24bppRgb);
    //var clone = (Bitmap)drawor.Image.Clone();
    //FastBitmap fbmp = new FastBitmap(clone);
    //var clone2 = (Bitmap)printer.Image.Clone();
    //FastBitmap fbmp2 = new FastBitmap(clone2);

    //fbmp.Lock();
    //fbmp2.Lock();

    //for (int wid = 0; wid < Width; wid++)
    //{
    //    for (int hei = 0; hei < Height; hei++)
    //    {
    //        fbmp2.SetPixel(wid, hei, combine(fbmp2.GetPixel(wid, hei), fbmp2.GetPixel(wid, hei)));
    //    }
    //}

    //fbmp.Unlock();
    //fbmp2.Unlock();

    //printer.Image = fbmp2.Bitmap;
    ////Clear B
    //Graphic.Clear(Color.Transparent);
    //Graphic.Flush();
    ////A -> refresh
    //Refresh();
    ////stack append A

    //Images.Push((Bitmap)printer.Image.Clone());
    points = new List<PointF>();
}

//*********************************************************************************



//private Bitmap CloneFrom(Bitmap bmp)
//{
//    Bitmap _bmp = new Bitmap(bmp.Width, bmp.Height);

//    var vdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
//    IntPtr vdata_addr = vdata.Scan0;

//    int bytes = Math.Abs(vdata.Stride) * bmp.Height;
//    byte[] argaValues = new byte[bytes];
//    Marshal.Copy(vdata_addr, argaValues, 0, bytes);

//    return _bmp;
//}


//*********************************************************************************



private Color combine(Color color1, Color color2)
{
    return Color.FromArgb(Clamp(color1.A + color2.A), Clamp(color1.R + color2.R), Clamp(color1.G + color2.G), Clamp(color1.B + color2.B));
}

private int Clamp(int v)
{
    if (v < 0) return 0;
    if (v > 255) return 255;
    return v;
}

private void Drawer_MouseDown(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        //Graphic = Graphics.FromImage(drawor.Image);
        Graphic = Graphics.FromImage(printer.Image);
        mouseLeft = true;
        points.Add(e.Location);
    }

}


private void FullScreen()
{
    this.WindowState = FormWindowState.Maximized;
    var screen = new ScreenCapture();
    this.BackgroundImage = screen.CaptureScreen();
}



//PictureBoxStack stack = new PictureBoxStack();
//DateTime timer;
//Image img;
//Pen Pen;

//private void InitializeWind0ow()
//{
//    this.WindowState = FormWindowState.Maximized;
//    var scp = new ScreenCapture();
//    img = scp.CaptureScreen();
//    this.BackgroundImage = img;

//    Pen = new Pen(Color.CadetBlue, 3);

//    this.KeyPress += new KeyPressEventHandler(OnKeyDown);

//    this.MouseDown += Drawer_MouseDown;
//    this.MouseUp += Drawer_MouseUp;
//    this.MouseMove += Drawer_MouseMove;
//}

//private List<PointF> points = new List<PointF>();
//private void Drawer_MouseMove(object sender, MouseEventArgs e)
//{
//    if (mouselfet)
//    {
//        points.Add(e.Location);
//        Graphic.Clear(Color.Transparent);
//        Graphic.DrawLines(Pen, points.ToArray());
//        Graphic.Flush();
//        Refresh();
//    }
//}

//private void Drawer_MouseUp(object sender, MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        mouselfet = false;
//        points = new List<PointF>();

//        stack.Current.picture.MouseDown += Drawer_MouseDown;
//        stack.Current.picture.MouseUp += Drawer_MouseUp;
//        stack.Current.picture.MouseMove += Drawer_MouseMove;
//    }
//}

//private bool mouselfet;
//Graphics Graphic;
//Image Img;

//private void Drawer_MouseDown(object sender, MouseEventArgs e)
//{
//    if (e.Button == MouseButtons.Left)
//    {
//        mouselfet = true;
//        points.Add(e.Location);
//        Img = new Bitmap(Size.Width, Size.Height);
//        Graphic = Graphics.FromImage(Img);
//        var picbox = new PictureBox()
//        {
//            Size = this.Size,
//            BackColor = Color.Transparent,
//            Image = Img
//        };
//        this.Controls.Add(picbox);
//        stack.Push(new PictureBoxNode(picbox, stack.Current));
//        timer = DateTime.Now;
//    }
//}

//private void OnKeyDown(object sender, KeyPressEventArgs e)
//{
//    if (e.KeyChar == ESC) this.Close();
//    if (e.KeyChar == 'z') this.Undo();
//}

//private void Undo()
//{
//    var node = stack.Pop();
//    if (node != null)
//    {
//        node.picture.Dispose();
//        node.picture = null;
//        //node.picture.close
//    }
//}

//private class PictureBoxStack
//{
//    public bool hasNext { get { return Current != null; } }
//    public PictureBoxNode Current;

//    public virtual PictureBoxNode Peek()
//    {
//        return Current;
//    }
//    public virtual PictureBoxNode Pop()
//    {
//        var copy = Current;
//        Current = Current.Last;
//        return copy;
//    }
//    public virtual void Push(PictureBoxNode obj)
//    {
//        Current = obj;
//    }
//}

//private class PictureBoxNode
//{
//    public PictureBox picture { get; set; }
//    public PictureBoxNode Last { get; set; }

//    public PictureBoxNode(PictureBox box, PictureBoxNode pre)
//    {
//        this.Last = pre;
//        this.picture = box;
//    }
//}

    */
#endregion