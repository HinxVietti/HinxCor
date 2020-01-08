using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileBrowser
{
    public partial class fb : TransparencyForm
    {
        public fb()
        {
            //InitializeComponent();
            markp();
        }

        private bool mouseDown = false;

        public void markp()
        {
            InitializeComponent();
            MouseMove += ImageDisplayer_MouseMove;
            MouseDown += ImageDisplayer_MouseDown;
            MouseUp += ImageDisplayer_MouseUp;
            //MouseDoubleClick += ImageDisplayer_MouseDoubleClick;

            //DragDrop += ImageDisplayer_DragDrop;
            //this.Height = img.Height;
            //this.BackgroundImage = img;
            this.Width = BackgroundImage.Width;
            this.Height = BackgroundImage.Height;
            SetBitmap((Bitmap)BackgroundImage, 005);
            CenterToScreen();
        }

        private void ImageDisplayer_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void ImageDisplayer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ImageDisplayer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        private void ImageDisplayer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseDown = true;
        }

        private void ImageDisplayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
                DragMove();
        }

        void DragMove()
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, MOUSE_MOVE, IntPtr.Zero);
            SendMessage(Handle, 0x0202, 0, IntPtr.Zero);
        }



        private const int MOUSE_MOVE = 0xF012;
        [DllImport("user32")] public static extern int ReleaseCapture();
        [DllImport("user32")] public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
    }

    /// <summary>
    /// 透明窗口
    /// </summary>
    public class TransparencyForm : Form
    {
        /// <summary>
        /// INSTN
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                //  WS_EX_LAYERED 扩展样式
                return cp;
            }
        }

        /// <summary>
        /// 让该Bitmap可以使用透明通道在Windows桌面上
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="opacity"></param>
        public void SetBitmap(Bitmap bitmap, byte opacity)
        {

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("位图必须是32位包含alpha 通道");

            IntPtr screenDc = _Win32.GetDC(IntPtr.Zero);
            IntPtr memDc = _Win32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));   // 创建GDI位图句柄，效率较低
                oldBitmap = _Win32.SelectObject(memDc, hBitmap);
                _Win32.Size size = new _Win32.Size(bitmap.Width, bitmap.Height);
                _Win32.Point pointSource = new _Win32.Point(0, 0);
                _Win32.Point topPos = new _Win32.Point(Left, Top);
                _Win32.BLENDFUNCTION blend = new _Win32.BLENDFUNCTION();
                blend.BlendOp = _Win32.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = _Win32.AC_SRC_ALPHA;
                _Win32.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, _Win32.ULW_ALPHA);
            }
            finally
            {
                _Win32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    _Win32.SelectObject(memDc, oldBitmap);
                    _Win32.DeleteObject(hBitmap);
                }
                _Win32.DeleteDC(memDc);
            }
        }


    }
}


internal class _Win32
{
    public enum Bool

    {

        False = 0,

        True

    };


    [StructLayout(LayoutKind.Sequential)]

    public struct Point
    {

        public Int32 x;

        public Int32 y;



        public Point(Int32 x, Int32 y)

        { this.x = x; this.y = y; }

    }





    [StructLayout(LayoutKind.Sequential)]

    public struct Size

    {

        public Int32 cx;

        public Int32 cy;



        public Size(Int32 cx, Int32 cy)

        { this.cx = cx; this.cy = cy; }

    }





    [StructLayout(LayoutKind.Sequential, Pack = 1)]

    struct ARGB

    {

        public byte Blue;

        public byte Green;

        public byte Red;

        public byte Alpha;

    }





    [StructLayout(LayoutKind.Sequential, Pack = 1)]

    public struct BLENDFUNCTION

    {

        public byte BlendOp;

        public byte BlendFlags;

        public byte SourceConstantAlpha;

        public byte AlphaFormat;

    }





    public const Int32 ULW_COLORKEY = 0x00000001;

    public const Int32 ULW_ALPHA = 0x00000002;

    public const Int32 ULW_OPAQUE = 0x00000004;



    public const byte AC_SRC_OVER = 0x00;

    public const byte AC_SRC_ALPHA = 0x01;



    [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
    public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);


    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]

    public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);



    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]

    public static extern IntPtr GetDC(IntPtr hWnd);



    [DllImport("user32.dll", ExactSpelling = true)]

    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);



    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);



    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

    public static extern Bool DeleteDC(IntPtr hdc);



    [DllImport("gdi32.dll", ExactSpelling = true)]

    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);



    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

    public static extern Bool DeleteObject(IntPtr hObject);



    [DllImport("user32.dll", EntryPoint = "SendMessage")]

    public static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

    [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]



    public static extern int ReleaseCapture();

    public const int WM_SysCommand = 0x0112;

    public const int SC_MOVE = 0xF012;



    public const int SC_MAXIMIZE = 61488;

    public const int SC_MINIMIZE = 61472;

}


