using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class Form4 : Form
    {

        Font f;
        public Form4()
        {
            InitializeComponent();
            f = new Font("微软雅黑", 144);
        }

        private void SolidBrush_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            SizeF size = g.MeasureString("囖", f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            g = Graphics.FromImage(bmp);

            SolidBrush brush = new SolidBrush(Color.Gray);
            g.DrawString("囖", f, brush, 0, 0);
            g.Flush();
            this.pictureBox.Image = bmp;
        }

        private void TextureBrush_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            SizeF size = g.MeasureString("囖", f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            g = Graphics.FromImage(bmp);

            Bitmap image1 = (Bitmap)Image.FromFile(@"D:\2.jpg", true);
            TextureBrush texture = new TextureBrush(image1);
            texture.WrapMode = WrapMode.Tile;

            //Image img = Image.FromFile("D:\\1.jpg");
            //TextureBrush brush = new TextureBrush(img, WrapMode.Tile);
            g.DrawString("囖", f, texture, 0, 0);
            g.Flush();
            this.pictureBox.Image = bmp;
        }

        private void HatchBrush_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            SizeF size = g.MeasureString("囖", f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            g = Graphics.FromImage(bmp);
            Image img = Image.FromFile("D:\\1.jpg");

            HatchBrush brush = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Blue, Color.Orange);

            g.DrawString("囖", f, brush, 0, 0);
            g.Flush();
            this.pictureBox.Image = bmp;
        }

        private void LinearGradientBrush_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            SizeF size = g.MeasureString("囖", f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            g = Graphics.FromImage(bmp);
            Image img = Image.FromFile("D:\\1.jpg");

            LinearGradientBrush brush = new LinearGradientBrush(new PointF(0, 0), new PointF(1f, 1), Color.Green, Color.Yellow);
            g.DrawString("囖", f, brush, 0, 0);
            g.Flush();
            this.pictureBox.Image = bmp;
        }

        private void PathGradientBrush_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            SizeF size = g.MeasureString("囖", f);
            bmp = new Bitmap(bmp, (int)size.Width, (int)size.Height);
            g = Graphics.FromImage(bmp);
            Image img = Image.FromFile("D:\\1.jpg");

            PathGradientBrush brush = new PathGradientBrush(new[] { new PointF(0,0),
            new PointF(0.1f,0.25f),
            new PointF(1,1)});
            g.DrawString("囖", f, brush, 0, 0);
            g.Flush();
            this.pictureBox.Image = bmp;
        }
    }
}
