using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HinxCor.WindowsForm
{
    public partial class PickColorDialog : Form
    {
        public PickColorDialog()
        {
            InitializeComponent();
            InitializeProperties();

        }
        ScreenCapture Capture = new ScreenCapture();

        private void InitializeProperties()
        {
            this.BackgroundImage = Capture.CaptureScreen();

            MouseMove += PickColorDialog_MouseMove;
        }

        private void PickColorDialog_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Location.X > 100 && e.Location.Y > 100)
            {
                this.button1.Text = string.Format("e:{0},{1}", e.Location.X, e.Location.Y);
                Size size = new Size(30, 30);
                Size size2 = new Size(30, 30);
                this.Lines.BackgroundImage = ((Bitmap)this.BackgroundImage).Clone(new Rectangle(e.Location - size, size2), BackgroundImage.PixelFormat);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
