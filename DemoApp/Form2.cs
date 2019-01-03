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
            this.textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text == string.Empty) return;
            refresh();
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
            StringFormat fffm = StringFormat.GenericDefault;
            fffm.Alignment = StringAlignment.Center;
            //var bmp = BitmapGenerator.DrawString(textBox1.Text, font, Rectangle.Empty, 0, 0, Color.Gray, Color.Transparent, System.Drawing.Text.TextRenderingHint.AntiAlias);
            var bmp = BitmapGenerator.DrawText(textBox1.Text, font, Rectangle.Empty, StringFormat.GenericDefault, 0, 0, new SolidBrush(Color.Gray), Color.Transparent, System.Drawing.Text.TextRenderingHint.AntiAlias);
            DrawstringBox.Image = bmp;
            DrawstringBox.Size = bmp.Size;
        }

        private void refresh()
        {
            StringFormat fffm = StringFormat.GenericDefault;
            fffm.Alignment = StringAlignment.Center;
            var bmp = BitmapGenerator.DrawText(textBox1.Text, font, Rectangle.Empty, StringFormat.GenericDefault, 0, 0, new SolidBrush(Color.Gray), Color.Transparent, System.Drawing.Text.TextRenderingHint.AntiAlias);
            DrawstringBox.Image = bmp;
            DrawstringBox.Size = bmp.Size;
            bmp = BitmapGenerator.GetBitmap(textBox1.Text, font, Rectangle.Empty, Color.Gray, Color.Transparent, format, System.Drawing.Text.TextRenderingHint.AntiAlias);
            GetBmpBox.Image = bmp;
            GetBmpBox.Size = bmp.Size;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var bmp = BitmapGenerator.GetBitmap(textBox1.Text, font, Rectangle.Empty, Color.Gray, Color.Transparent, format, System.Drawing.Text.TextRenderingHint.AntiAlias);
            GetBmpBox.Image = bmp;
            GetBmpBox.Size = bmp.Size;
        }


    }
}
