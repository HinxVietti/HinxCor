using System;
using System.Collections.Generic;

namespace HinxCor.Win32
{
    using System.Drawing;
    using System.Windows.Forms;
    using static User32;

    public class WindowsCat
    {
        private IntPtr windowsIntptr;

        private Rectangle NorScreen;

        public IntPtr Handle => windowsIntptr;

        public int X { get => NorScreen.X; set { NorScreen.X = value; } }
        public int Y { get => NorScreen.Y; set { NorScreen.Y = value; } }
        public int Width { get => NorScreen.Width; set { NorScreen.Width = value; } }
        public int Height { get => NorScreen.Height; set { NorScreen.Height = value; } }

        public void SWP()
        {
            ShowWindow(windowsIntptr, 1);
            /*=>*/
            SetWindowPos(windowsIntptr, new IntPtr(0), X, Y, Width, Height, SWP_SHOWWINDOW);
        }

        public WindowsCat(IntPtr @int)
        {
            this.windowsIntptr = @int;
            GetWindowRect(@int, ref NorScreen);
            Width -= X;
            Height -= Y;
        }

        public void SetPosition(int left, int top)
        {
            this.X = left;
            this.Y = top;
            SWP();
        }


        /// <summary>
        /// 重设大小并且居中
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="hei"></param>
        public void SetSizeAndMidCenter(int wid, int hei)
        {
            this.Width = wid;
            this.Height = hei;
            var freew = SystemInformation.WorkingArea.Width - Width;
            var freeh = SystemInformation.WorkingArea.Height - Height;
            this.X = freew / 2;
            this.Y = freeh / 2;
            SWP();
        }

        /// <summary>
        /// 重新设置窗口大小
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="hei"></param>
        public void SetSize(int wid, int hei)
        {
            this.Width = wid;
            this.Height = hei;
            SWP();
        }

        /// <summary>
        /// 居中屏幕
        /// </summary>
        public void MidCenter()
        {
            var freew = SystemInformation.WorkingArea.Width - Width;
            var freeh = SystemInformation.WorkingArea.Height - Height;
            this.X = freew / 2;
            this.Y = freeh / 2;
            SWP();
        }

        /// <summary>
        /// 普通窗口化
        /// </summary>
        public void Normalize() => SWP();

        /// <summary>
        /// 工作区全屏
        /// </summary>
        public void FullWorkScreen() => SetFullWorkScreen();
        public void SetFullWorkScreen()
        {
            SetWindowPos(windowsIntptr, new IntPtr(0), 0, 0, SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height, SWP_SHOWWINDOW);
            //SetWindowPos(windowsIntptr, new IntPtr(-1), 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 40, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// 设置真全屏
        /// </summary>
        public void RealFullScreen() => SetRealFullScreen();
        public void SetRealFullScreen()
        {
            SetWindowPos(windowsIntptr, new IntPtr(0), 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// 把窗体设为无边框模式
        /// </summary>
        public void SetNoFrame()
        {
            SetWindowLong(windowsIntptr, GWL_STYLE, WS_Normal_None);
        }

        /// <summary>
        /// 把窗体设为Resize窗体模式
        /// </summary>
        public void SetNormal()
        {
            SetWindowLong(windowsIntptr, GWL_STYLE, WS_Normal_None);
        }


    }

}
