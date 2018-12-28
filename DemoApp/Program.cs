using System;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
//using HinxCor.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using HinxCor.Rendering;

namespace DemoApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //DemoColorPicker();
            //DemoFontTxt();
            //DemoMethod564a6c();
            //DemoMethodsd453();
            DemoMethod2d5f8a();

            //Bitmap bitmap = new Bitmap(1, 1);
            //Font f = GetFont();
            //Graphics g = Graphics.FromImage(bitmap);
            //float a1 = g.MeasureString(" ", f).Width;
            //float a2 = g.MeasureString("\r", f).Width;
            //float a3 = g.MeasureString("\n", f).Width;
            //float a4 = g.MeasureString("\t", f).Width;
            //float a5 = g.MeasureString("\n\r", f).Width;
            //Console.ReadKey();

        }

        /// <summary>
        /// 测试段落生成工具
        /// </summary>
        private static void DemoMethod2d5f8a()
        {
            string str = "你好,中国" + "\n" +
                "这是新的内容哦" + "\n" +
                "hello world" + "\n" +
                "GG bold" + "\r" +
                "狗狗狗" + "\n\r" +
                "TTTTTTTTTTT" + "\r\n"
                +"H啊哈哈哈";


            Console.WriteLine("请输入段落文本,输入Q + Enter 结束");
            //string str = "";
            //string enline;
            //while (!(enline = Console.ReadLine()).Equals("Q"))
            //{
            //    str += enline + "\n";
            //}
            Console.WriteLine("输入完成. 开始写入(1000次)");
            var font = GetFont();
            StringFormat format = new StringFormat();

            Func<string, Bitmap> drawStr = mstr =>
             {
                 return BitmapGenerator.DrawString(str, font, default(Rectangle), 0, 0, Color.Gray, Color.White, format, TextRenderingHint.AntiAlias);
             };
            Func<string, Bitmap> getBmp = mstr =>
           {
               return BitmapGenerator.GetBitmap(str, font, default(Rectangle), Color.Gray, Color.White, format, TextRenderingHint.AntiAlias);
           };

            var t = System.DateTime.Now;
            for (int i = 0; i < 1000; i++)
                drawStr(str);
            Console.WriteLine(string.Format("耗时:{0} ms,写入完成. ", (System.DateTime.Now - t).TotalMilliseconds));

            t = System.DateTime.Now;
            for (int i = 0; i < 1000; i++)
                getBmp(str);
            Console.WriteLine(string.Format("耗时:{0} ms,写入完成. ", (System.DateTime.Now - t).TotalMilliseconds));

            var bmp = drawStr(str);
            string pth = GetPath();
            bmp.Save(pth, ImageFormat.Png);
            int p = pth.LastIndexOf('.');
            pth = pth.Insert(p - 1, " 1");
            bmp = getBmp(str);
            bmp.Save(pth, ImageFormat.Png);
            OpenFileInExplorer(pth);
            Console.WriteLine("OK");
            Console.ReadKey();
        }


        /// <summary>
        /// 测试不同情况下渲染结果的优劣
        /// </summary>
        private static void DemoMethodsd453()
        {
            var font = GetFont();
            string folder = GetFolder();

            Rectangle rect = new Rectangle();
            int lh = (int)(font.Size + font.Height);
            Bitmap bmp = new Bitmap(1550, lh * 6);
            Graphics.FromImage(bmp).Clear(bgColor);
            for (int i = 0; i < 6; i++)
            {
                var hint = (TextRenderingHint)i;
                rect.Y = i * lh;
                bmp = GetBitmap(bmp, font, rect, hint);
            }
            var pth = GetPath();
            bmp.Save(pth, ImageFormat.Png);
            OpenFileInExplorer(pth);

            // ShowInExplorer(pth);
        }

        private static Color fontColor = Color.Gray;
        private static Color bgColor = Color.White;
        private static StringFormat format = new StringFormat();

        private static Bitmap GetBitmap(Bitmap bmp, Font font, Rectangle rect, TextRenderingHint renderinghint)
        {
            string text = renderinghint.ToString();
            return GetBitmap(bmp, text, font, rect, fontColor, bgColor, format, renderinghint);
        }

        private static Bitmap GetBitmap(Bitmap bmp, string text, Font font, Rectangle rect, Color fontColor, Color backColor, StringFormat format, TextRenderingHint renderingHint)
        {
            //Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            var size = g.MeasureString(text, font);

            //设置背景
            //g.Clear(backColor);

            // 蚂蚁线填充边框
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // The interpolation mode determines how intermediate values between two endpoints are calculated.
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // This one is important
            g.TextRenderingHint = renderingHint;

            g.DrawString(text, font, new SolidBrush(fontColor), rect, format);

            g.Flush();
            return bmp;
        }


        private static string GetFolder()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            while (folder.ShowDialog() != DialogResult.OK) ;
            return folder.SelectedPath;
        }

        /// <summary>
        /// 测试遍历写入和 一次性绘制的时间差异
        /// </summary>
        private static void DemoMethod564a6c()
        {
            Bitmap bmp = new Bitmap(1, 1);
            string str = "hello world,this is an test program.\n 这是一个测试用的程序,你好,世界.";
            var strArray = str.ToCharArray();
            Graphics g = Graphics.FromImage(bmp);
            Font f = GetFont();

            var size = g.MeasureString(str, f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            Brush b = new SolidBrush(Color.Black);

            int cercleCount = 2000;
            int timemul = 1;
            double[] ta = new double[50];
            double[] tb = new double[50];
            var t = System.DateTime.Now;
            for (int i = 0; i < 50; i++)
            {
                for (int o = 0; o < cercleCount; o++)
                {
                    g.DrawString(str, f, b, 0, 0);
                    g.Flush();
                }
                ta[i] = (System.DateTime.Now - t).TotalMilliseconds * timemul;
                //Console.WriteLine("A" + i + " 用时:" + (ta).ToString("F3"));
                t = System.DateTime.Now;
                for (int o = 0; o < cercleCount; o++)
                {
                    int num = strArray.Length;
                    int currentpx = 0;
                    int currentpy = 0;
                    for (int j = 0; j < num; j++)
                    {
                        var ss = g.MeasureString(strArray[i].ToString(), f);
                        g.DrawString(strArray[i].ToString(), f, b, currentpx, currentpy);
                        currentpx += (int)ss.Width;
                        currentpy += (int)ss.Height;
                    }
                    g.Flush();
                }
                tb[i] = (System.DateTime.Now - t).TotalMilliseconds * timemul;
                //Console.WriteLine("B" + i + " 用时:" + (tb).ToString("F3"));
                //Console.WriteLine(string.Format("时间差: {0} ms", tb - ta));
                //Console.WriteLine();
                //Console.WriteLine();
                Console.Write("*");
                t = System.DateTime.Now;
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(string.Format("A{0}:{1}, \tB{0}:{2}, \t时间差:{3} ms", i, ta[i], tb[i], tb[i] - ta[i]));
                Console.WriteLine();
            }

            Console.ReadKey();
        }


        private static Font GetFont()
        {
            FontDialog f = new FontDialog();
            f.ShowEffects = true;
            f.ShowColor = true;
            f.ShowApply = true;
            f.ShowDialog();
            return f.Font;
        }

        public static void DemoFontTxt()
        {

            FontDialog f = new FontDialog();
            f.ShowDialog();
            Font font = f.Font;
            //FontFamily family = new FontFamily("");
            //StringFormat format;

            Console.WriteLine("writeImage");
            var str = Console.ReadLine();
            var bmp = new Bitmap(1, 1);
            //var bmp = Helper.CreateBitmapImage(str, font);
            var save = new SaveFileDialog();
            save.Filter = ".png|*.png";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ph = save.FileName;
                bmp.Save(ph, ImageFormat.Png);
                Console.WriteLine("OK");
                //ShowInExplorer(ph);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Abort.");
                Console.ReadKey();
            }

        }

        public static void DemoColorPicker()
        {
            //OpenFileDialog open = new OpenFileDialog();
            //while (open.ShowDialog() != DialogResult.OK) ;
            //var fname = open.FileName;
            //HinxCor.Drawing.ColorPickerDisplayer pp = new HinxCor.Drawing.ColorPickerDisplayer();
            //pp.ShowPicker(fname);
            //Console.WriteLine(pp.color);
            //Console.ReadKey();
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
            var bmp = new Bitmap(1, 1);
            //var bmp = Helper.CreateBitmapImage(str);
            var save = new SaveFileDialog();
            save.Filter = ".png|*.png";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string ph = save.FileName;
                bmp.Save(ph, ImageFormat.Png);
                Console.WriteLine("OK");
                //ShowInExplorer(ph);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Abort.");
                Console.ReadKey();
            }
        }


        [Obsolete]
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

        [Obsolete]
        private static void DemoTxt()
        {
            string infoString = "";  // enough space for one line of output
            int ascent;             // font family ascent in design units
            float ascentPixel;      // ascent converted to pixels
            int descent;            // font family descent in design units
            float descentPixel;     // descent converted to pixels
            int lineSpacing;        // font family line spacing in design units
            float lineSpacingPixel; // line spacing converted to pixels

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               16, FontStyle.Regular,
               GraphicsUnit.Pixel);
            PointF pointF = new PointF(10, 10);
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            // Display the font size in pixels.
            infoString = "font.Size returns " + font.Size + ".";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the font family em height in design units.
            infoString = "fontFamily.GetEmHeight() returns " +
               fontFamily.GetEmHeight(FontStyle.Regular) + ".";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down two lines.
            pointF.Y += 2 * font.Height;

            // Display the ascent in design units and pixels.
            ascent = fontFamily.GetCellAscent(FontStyle.Regular);

            // 14.484375 = 16.0 * 1854 / 2048
            ascentPixel =
               font.Size * ascent / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The ascent is " + ascent + " design units, " + ascentPixel +
               " pixels.";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the descent in design units and pixels.
            descent = fontFamily.GetCellDescent(FontStyle.Regular);

            // 3.390625 = 16.0 * 434 / 2048
            descentPixel =
               font.Size * descent / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The descent is " + descent + " design units, " +
               descentPixel + " pixels.";
            //e.Graphics.DrawString(infoString, font, solidBrush, pointF);

            // Move down one line.
            pointF.Y += font.Height;

            // Display the line spacing in design units and pixels.
            lineSpacing = fontFamily.GetLineSpacing(FontStyle.Regular);

            // 18.398438 = 16.0 * 2355 / 2048
            lineSpacingPixel =
            font.Size * lineSpacing / fontFamily.GetEmHeight(FontStyle.Regular);
            infoString = "The line spacing is " + lineSpacing + " design units, " +
               lineSpacingPixel + " pixels.";
            // e.Graphics.DrawString(infoString, font, solidBrush, pointF);
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
