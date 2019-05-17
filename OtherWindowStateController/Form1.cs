using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using HinxCor.Win32;

namespace OtherWindowStateController
{
    using System.Drawing;
    using static User32;

    public partial class 窗体工具 : Form
    {
        IntPtr toapply { get { return cat.Handle; } set { cat = new WindowsCat(value); } }

        public 窗体工具()
        {
            InitializeComponent();
            TopMost = true;
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


        private void button7_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            toapply = GetForegroundWindow();
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

        private void button8_Click(object sender, EventArgs e)
        {
            //SetWindowLong(toapply,GWL_STYLE,WS_POPUP);
            //var sty = GetWindowLong(toapply, GWL_STYLE);//获取当前得样式
            //sty &= ~WS_BORDER;
            //sty &= ~WS_THICKFRAME;
            var sr = 0x16010000;

            //0x10000000
            SetWindowLong(toapply, GWL_STYLE, sr);//回去设置属性
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var sty = GetWindowLong(toapply, GWL_STYLE);//获取当前得样式
            var sr = 0x16cf0000;
            SetWindowLong(toapply, GWL_STYLE, sr);//回去设置属性

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetWindowPos(toapply, new IntPtr(-1), int.Parse(x.Text), int.Parse(y.Text), int.Parse(w.Text), int.Parse(h.Text), SWP_SHOWWINDOW);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Size OutTaskBarSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);

            Size ScreenSize = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            Size TaskBarSize;

            TaskBarSize = new Size(
                            (ScreenSize.Width - (ScreenSize.Width - OutTaskBarSize.Width)),
                            (ScreenSize.Height - OutTaskBarSize.Height)
                            );

            System.Windows.Forms.MessageBox.Show("任务栏大小：" + TaskBarSize.Width + "," + TaskBarSize.Height);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //当前的屏幕除任务栏外的工作域大小
            string currentScreenSize_OutTaskBar = SystemInformation.WorkingArea.Width.ToString() + "," + SystemInformation.WorkingArea.Height.ToString();
            System.Windows.Forms.MessageBox.Show("当前的屏幕除任务栏外的工作域大小为:" + currentScreenSize_OutTaskBar);

            //当前的屏幕包括任务栏的工作域大小
            string currentScreenSize = Screen.PrimaryScreen.Bounds.Width.ToString() + "," + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height.ToString();
            System.Windows.Forms.MessageBox.Show("当前的屏幕包括任务栏的工作域大小为:" + currentScreenSize);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            cat.SetFullWorkScreen();
            //SetWindowLong(toapply, GWL_STYLE, WS_Normal_None);
            //SetWindowPos(toapply, new IntPtr(-1), 0, 0, SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height, SWP_SHOWWINDOW);
        }

        WindowsCat cat;

        private void button14_Click(object sender, EventArgs e)
        {
            new WindowsCat(toapply);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            cat.Normalize();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            cat.RealFullScreen();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            cat.SetSizeAndMidCenter(int.Parse(w.Text), int.Parse(h.Text));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = GetWindowLong(toapply, GWL_STYLE).ToString();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = GetWindowLong(toapply, GWL_EXSTYLE).ToString();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = GetWindowLong(toapply, GWL_HWNDPARENT).ToString();
        }
    }
}
