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
    public partial class Progressbar : Form
    {
        action_sf setString;
        action close;

        public Progressbar()
        {
            InitializeComponent();
            setString = (s, f) =>
            {
                this.Message.Text = s;
                if (f < 0) f = 0;
                if (f > 1) f = 1;
                this.progressBar1.Value = (int)(f * 10000);
            };
            close = Close;
            this.Shown += Progressbar_Shown;
        }

        private void Progressbar_Shown(object sender, EventArgs e)
        {
            isReady = true;
        }

        public bool isReady { get; set; }
        public void _EX_CLOSE() => this.Invoke(close);
        public void setMessage(string message, float value) => this.Invoke(setString, message, value);
    }
}
