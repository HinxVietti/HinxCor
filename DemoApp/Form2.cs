using HinxCor.Rendering;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.font = new Font("微软雅黑", 14);
        }

        public Font font;

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new FontDialog();
            f.ShowDialog();
            font = f.Font;
            this.Text = font.ToString();
        }
        StringFormat format = StringFormat.GenericDefault;
        private void button2_Click(object sender, EventArgs e)
        {
            var bmp =
            BitmapGenerator.DrawString(textBox1.Text, font, Rectangle.Empty, 0, 0, Color.Gray, Color.Transparent, format, System.Drawing.Text.TextRenderingHint.AntiAlias);
            DrawstringBox.Image = bmp;
            DrawstringBox.Size = bmp.Size;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var bmp =
            BitmapGenerator.GetBitmap(textBox1.Text, font, Rectangle.Empty, Color.Gray, System.Drawing.Color.Transparent, format, System.Drawing.Text.TextRenderingHint.AntiAlias);
            GetBmpBox.Image = bmp;
            GetBmpBox.Size = bmp.Size;
        }


    }
}
