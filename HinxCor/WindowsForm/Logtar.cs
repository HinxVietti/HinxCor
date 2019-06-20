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
    internal partial class Logtar : Form
    {
        public Logtar()
        {
            InitializeComponent();
            string str1 = "这是一个中文说明,等下明显就是要下大雨了,字符超过试试...";
            //总长度30
            //string str2="这是一个中文说明,等下明显就是要下大雨了,字符超过试试自动省略";
            this.BackColor = Color.Bisque;
            this.TransparencyKey = Color.Bisque;
        }

        public const int WM_NCHITTEST = 0x84;
        public const int HTTRANSPARENT = -1;

        public void SetLog(object v) => this.LogLabel.Text = v.ToString();

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (int)WM_NCHITTEST)
            {
                message.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref message);
            }
        }
    }
}
