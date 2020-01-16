using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo_webBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string url = "https://www.baidu.com/";
            this.web.DocumentCompleted += Web_DocumentCompleted;
            this.web.Navigate(url);

            Text = "欢迎使用魅演3D";
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Text = web.DocumentTitle;
        }
    }
}
