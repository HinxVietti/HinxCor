using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _fullscpicdisplayer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main1(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var imgf = new _ddscwidge();
            imgf.WindowState = FormWindowState.Maximized;
            imgf.ControlBox = false;
            //OpenFileDialog openf = new OpenFileDialog();
            //while (openf.ShowDialog() != DialogResult.OK) ;
            //imgf.SetImage(openf.FileName);
            try
            {
                imgf.SetImage(args[0]);
                imgf.TopMost = true;
                Application.Run(imgf);
            }
            catch { }
        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var imgf = new _ddscwidge();
            //imgf.WindowState = FormWindowState.Maximized;
            //imgf.ControlBox = false;
            //OpenFileDialog openf = new OpenFileDialog();
            //while (openf.ShowDialog() != DialogResult.OK) ;
            //imgf.SetImage(openf.FileName);
            try
            {
                imgf.SetImage(args[0], true);
                imgf.TopMost = true;
                Application.Run(imgf);
            }
            catch { }
        }
    }
}
