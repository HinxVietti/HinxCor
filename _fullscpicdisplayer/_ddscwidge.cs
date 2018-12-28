using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _fullscpicdisplayer
{
    public partial class _ddscwidge : Form
    {
        public _ddscwidge()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            pictureBox1.MouseDoubleClick += CloseTap;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            if (e.Button == MouseButtons.Left)
            {
                MouseHold = false;
                pos = default(Point);
            }
            base.OnMouseUp(e);
        }

        private bool MouseHold = false;
        private Point pos;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            if (e.Button == MouseButtons.Left)
            {
                MouseHold = true;
                pos = MousePosition;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!pos.Equals(default(Point)) && MouseHold)
            {
                var d = reduce(MousePosition, pos);
                pos = MousePosition;
                Location = new Point(d.X + Location.X, Location.Y + d.Y);
            }
        }


        private Point reduce(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        private void CloseTap(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MinimizeBox = false;
            MaximizeBox = false;
            //ControlBox = false;
            if (FormBorderStyle == FormBorderStyle.None) FormBorderStyle = FormBorderStyle.FixedSingle;
            else FormBorderStyle = FormBorderStyle.None;
            //this.Close();
            //this.Hide();
            //this.Dispose();
        }
    }
}
