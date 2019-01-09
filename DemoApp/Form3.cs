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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Uri uri = new Uri(@"http://9453bb.com/thread-118328-1-1.html");
            this.webBrowser1.Url = uri;
            this.webBrowser1.Update();

            //webBrowser1.
        }

        public HtmlElementCollection collect
        {
            get
            {
                return
this.webBrowser1.Document.All;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            HtmlElementCollection alldoc = collect;
            List<HtmlElement> allele = new List<HtmlElement>();

            for (int i = 0; i < alldoc.Count; i++)
            {
                if (alldoc[i].Id != null && alldoc[i].Id.Contains("aimg"))
                    allele.Add(alldoc[i]);
            }
            int a = 0;
        }

        private Image GetWebImage(WebBrowser WebCtl, HtmlElement ImgeTag)
        {
            //ImgeTag.
            
            //HTMLDocument doc = (HTMLDocument)WebCtl.Document.DomDocument;
            //HTMLBody body = (HTMLBody)doc.body;
            //IHTMLControlRange rang = (IHTMLControlRange)body.createControlRange();
            //IHTMLControlElement Img = (IHTMLControlElement)ImgeTag.DomElement; //图片地址

            //Image oldImage = Clipboard.GetImage();
            //rang.add(Img);
            //rang.execCommand("Copy", false, null);  //拷贝到内存
            //Image numImage = Clipboard.GetImage();
            //try
            //{
            //    Clipboard.SetImage(oldImage);
            //}
            //catch
            //{
            //}

            //return numImage;
        }
    }
}
