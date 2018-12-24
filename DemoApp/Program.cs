using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HinxCor.Drawing;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security.Cryptography;

namespace DemoApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DemoColorPicker();

        }


        public static void DemoColorPicker()
        {
            OpenFileDialog open = new OpenFileDialog();
            while (open.ShowDialog() != DialogResult.OK) ;
            var fname = open.FileName;
            HinxCor.Drawing.ColorPickerDisplayer pp = new HinxCor.Drawing.ColorPickerDisplayer();
            pp.ShowPicker(fname);
            Console.WriteLine(pp.color);
            Console.ReadKey();
        }

        public static void DemoMethodTestMousePos()
        {
            int t = 5;
            while (t-- > 0)
            {
                Point p;
                de.GetCursorPos(out p);
                Console.WriteLine(p.X + "#" + p.Y);
                Thread.Sleep(500);
            }
            Console.ReadKey();
        }

        private static void DemoMethod546ad2()
        {
            ScreenCapture sc = new ScreenCapture();
            Image img = sc.CaptureScreen();
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "png(图片)|*.png";
            if (sav.ShowDialog() == DialogResult.OK)
                img.Save(sav.FileName);


            // capture entire screen, and save it to a file
            // display image in a Picture control named imageDisplay
            ////this.imageDisplay.Image = img;
            //this.pictureBox1.Image = img;
            //// capture this window, and save it
            //sc.CaptureWindowToFile(this.Handle, "C:\\temp2.jpg", ImageFormat.Jpeg);
        }

        private static string GetPath(string filter = "png图片|*.png")
        {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = filter;
            while (sav.ShowDialog() != DialogResult.OK)
                sav.Title = "直到你选择保存位置为止";
            return sav.FileName;
        }

        /// <summary>
        /// 测试
        /// </summary>
        private static void DemoMethod5d3f()
        {
            Console.WriteLine("writeImage");
            var str = Console.ReadLine();
            var bmp = Helper.CreateBitmapImage(str);
            var save = new SaveFileDialog();
            save.Filter = ".png|*.png";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ph = save.FileName;
                bmp.Save(ph, ImageFormat.Png);
                Console.WriteLine("OK");
                ShowInExplorer(ph);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Abort.");
                Console.ReadKey();
            }
        }


        private static void ShowInExplorer(string fileName)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select," + fileName);
        }

        /// <summary>
        /// 在Windows资源管理器上打开该文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void OpenInExplorer(string path)
        {
            path = path.Replace('/', '\\');
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        public static void OpenFileInExplorer(string fileName)
        {
            OpenInExplorer(new FileInfo(fileName).Directory.FullName);
        }

        private static void DemoApp23ef()
        {
            Console.WriteLine("This is an console demo app, any key to exit.");
            Console.ReadKey();
        }
    }
}


class de
{

    //using System.Runtime.InteropServices;
    Point p;
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point pt);
    private void timer1_Tick(object sender, EventArgs e)
    {
        GetCursorPos(out p);
        //label1.Text = p.X.ToString();//X坐标
        //label2.Text = p.Y.ToString();//Y坐标
    }
}
