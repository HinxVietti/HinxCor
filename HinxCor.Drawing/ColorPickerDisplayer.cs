using System;
using System.Drawing;
using System.Windows.Forms;

namespace HinxCor.Drawing
{
    /// <summary>
    /// 拾取色彩展示窗口
    /// </summary>
    public partial class ColorPickerDisplayer : Form
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ColorPickerDisplayer()
        {
            InitializeComponent();
            timer1.Tick += Timer1_Tick;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        private Color pixColor = Color.Green;
        /// <summary>
        /// 拾色器当前的颜色
        /// </summary>
        public Color color;
        const int magnification = 3;//倍率，调节放大倍数,可由TrackBar控制调节  
        const int imgWidth = 120;//放大后图片的宽度
        const int imgHeight = 120;//放大后图片的高度
        Point offset = new Point(25, 25);
        Point startPosition = new Point(Cursor.Position.X - imgWidth / (2 * magnification), Cursor.Position.Y - imgHeight / (2 * magnification));

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public DialogResult ShowPicker(string fname)
        {
            Image img = Image.FromFile(fname);
            this.pictureBox2.Image = img;
            StartTimer();
            return this.ShowDialog();
        }

        /// <summary>
        /// 设置当前颜色
        /// </summary>
        /// <param name="c"></param>
        public void SetColor(Color c)
        {
            this.pixColor = c;
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        public void StartTimer()
        {
            timer1.Start();
        }

        /// <summary>
        /// 关闭计时器
        /// </summary>
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

            for (int i = 0; i < rigin.Width; i++)
                rigin.SetPixel(i, rigin.Height / 2, pixColor);
            for (int i = 0; i < rigin.Height; i++)
                rigin.SetPixel(rigin.Width / 2, i, pixColor);

            //for (int i = 0; i < 3; i++)
            //    for (int j = 0; j < rigin.Width; j++)
            //        rigin.SetPixel(j, Clamp(i * rigin.Height / 2, rigin.Height), pixColor);

            //for (int i = 0; i < 3; i++)
            //    for (int j = 0; j < rigin.Height; j++)
            //        rigin.SetPixel(Clamp(i * rigin.Width / 2, rigin.Width), j, pixColor);

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
            this.pictureBox2.Image = null;
            this.Hide();
            this.Dispose();
            this.Close();
        }
    }
}
