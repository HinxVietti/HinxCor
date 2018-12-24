using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class ColorPickerDisplayer : Form
    {
        public ColorPickerDisplayer()
        {
            InitializeComponent();
            timer1.Tick += Timer1_Tick;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        public Color color;
        const int magnification = 3;//倍率，调节放大倍数,可由TrackBar控制调节  
        const int imgWidth = 120;//放大后图片的宽度
        const int imgHeight = 120;//放大后图片的高度
        Point offset = new Point(25, 25);
        Point startPosition = new Point(Cursor.Position.X - imgWidth / (2 * magnification), Cursor.Position.Y - imgHeight / (2 * magnification));

        public void ShowPicker(string fname)
        {
            Image img = Image.FromFile(fname);
            this.pictureBox2.Image = img;
            StartTimer();
            this.ShowDialog();
        }

        public void StartTimer()
        {
            timer1.Start();
        }

        public void EndTimer()
        {
            timer1.Stop();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }

        //对定时器添加Tick事件，并设置Enabled为True
        private void timer1_Tick(object sender, EventArgs e)
        {
            //mx = MousePosition.X;
            //my = MousePosition.Y;
            Bitmap bt = null;
            startPosition.X = Cursor.Position.X - imgWidth / (2 * magnification);
            startPosition.Y = Cursor.Position.Y - imgHeight / (2 * magnification);

            bt = CopyPix(startPosition, pictureBox2.Image as Bitmap, bt);
            pictureBox1.Image = bt;
            pictureBox1.Location = GetRightPosition(pictureBox2.Image.Size);

            //pictureBox2.Image = bt.crea
        }

        private Point GetRightPosition(Size source)
        {
            Point p = MousePosition;
            p.X += MousePosition.X > (source.Width - imgWidth) ? (-offset.X - imgWidth) : offset.X;
            p.Y += MousePosition.Y > (source.Height - imgHeight) ? (-offset.Y - imgHeight) : (offset.Y);
            return p;
        }

        private Bitmap CopyPix(Point start, Bitmap source, Bitmap rigin)
        {
            start.X = Clamp(start.X, source.Width - imgWidth / magnification);
            start.Y = Clamp(start.Y, source.Height - imgHeight / magnification);
            rigin = new Bitmap(imgWidth, imgHeight);

            for (int i = 0; i < imgHeight; i++)
                for (int j = 0; j < imgWidth; j++)
                {
                    int px = Clamp(start.X + j / magnification, source.Size.Width);
                    int py = Clamp(start.Y + i / magnification, source.Size.Height);
                    rigin.SetPixel(j, i, source.GetPixel(px, py));
                }

            return rigin;
        }

        private int Clamp(int value, int max)
        {
            if (value < 0)
                value = 0;
            if (value >= max) value = max - 1; return value;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            color = (pictureBox2.Image as Bitmap).GetPixel(MousePosition.X, MousePosition.Y);
            this.Hide();
        }
    }
}
