using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Webview
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            return;

            if (args == null || args.Length == 0)
                args = new string[] { "http://www.focusky.com.cn/client/webpage/meiyan-promotion.html" };
            IWin32Window parent = null;
            if (args.Length >= 2)
            {
                if (int.TryParse(args?[1], out var ptr))
                    parent = new ForeWin32(ptr);
            }
            if (parent == null)
                parent = new ForeWin32();

            var hm = new HomePage(args[0]);
            hm.ShowDialog(parent);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        private class ForeWin32 : IWin32Window
        {
            private static IntPtr forg => GetForegroundWindow();
            public IntPtr Handle { get; private set; }

            public ForeWin32(int intptr)
            {
                if (intptr <= 0)
                    Handle = forg;
                else
                    Handle = new IntPtr(intptr);
            }

            public ForeWin32()
            {
                Handle = forg;
            }
        }
    }
}
