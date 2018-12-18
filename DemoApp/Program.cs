using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HinxCor.Drawing;
using System.Drawing.Imaging;
using System.Drawing;

namespace DemoApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //DemoMethod5d3f();
        }



        private static void DemoMethod546ad2()
        {
            
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

        private static void DemoApp23ef()
        {
            Console.WriteLine("This is an console demo app, any key to exit.");
            Console.ReadKey();
        }
    }
}
