using HinxCor.Wins.Forms;
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
    public partial class ApplicationLoadout : TransparencyForm
    //public partial class ApplicationLoadout : Form
    {
        public ApplicationLoadout(Image background)
        {
            InitializeComponent();
            this.BackgroundImage = background;
            SetBitmap((Bitmap)this.BackgroundImage, 255);
        }
        public void CenterScreen() => CenterToScreen();
    }
}
