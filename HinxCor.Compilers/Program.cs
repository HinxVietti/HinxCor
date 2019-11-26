using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HinxCor.Compilers
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            XmlDocument document = new XmlDocument();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "|*.svg";
            openFile.ShowDialog();
            document.Load(openFile.FileName);
            Svg.SvgDocument svg = Svg.SvgDocument.Open(document);
            var bmp = svg.Draw();
            bmp.Save(openFile.FileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
