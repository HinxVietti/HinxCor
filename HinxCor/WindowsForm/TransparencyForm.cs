using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HinxCor.Wins.Forms
{
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
