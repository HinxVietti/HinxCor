using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtherWindowStateController
{
    public partial class 窗体工具 : Form
    {
        IntPtr toapply;

        public 窗体工具()
        {
            InitializeComponent();
            this.textBox1.KeyPress += txtbox1_KeyPress;
        }

        private void txtbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toapply = GetProcessWnd();
            Display();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            toapply = GetProcessWnd();
            Display();
        }

        private void Display()
        {
            this.textBox1.Text = toapply.ToString();
            this.label1.Text = this.textBox1.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.toapply = new IntPtr(Convert.ToInt32(this.textBox1.Text));
            Display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowWindow(toapply, WindowShowStyle.ShowMinimized);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowWindow(toapply, WindowShowStyle.Maximize);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowWindow(toapply, WindowShowStyle.Restore);
        }

        #region Hide

        [DllImport("user32.dll", SetLastError = true)] public static extern IntPtr GetParent(IntPtr hwnd);
        [DllImport("user32")] public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref uint lpdwProcessId);
        [DllImport("kernel32")] public static extern void SetLastError(int dwErrCode);
        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);
        [DllImport("user32")] public static extern IntPtr GetForegroundWindow();

        public static IntPtr GetProcessWnd()
        {
            IntPtr ptrWnd = IntPtr.Zero;
            var currentProcess = Process.GetCurrentProcess();
            uint pid = (uint)currentProcess.Id;  // 当前进程 ID
            bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
            {
                uint id = 0;

                if (GetParent(hwnd) == IntPtr.Zero)
                {
                    GetWindowThreadProcessId(hwnd, ref id);
                    if (id == lParam)    // 找到进程对应的主窗口句柄
                    {
                        ptrWnd = hwnd;   // 把句柄缓存起来
                                         //SetLastError(0);    // 设置无错误
                        SetLastError(0);
                        return false;   // 返回 false 以终止枚举窗口
                    }
                }

                return true;

            }), pid);

            return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
        }


        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        private enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }
        #endregion

    }
}
