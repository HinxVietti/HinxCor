using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Svg;

namespace HinxCor.SVG
{
    public class SVGUtil
    {
        public static Bitmap ReadSvg(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            //var svg = SvgDocument.Open("filename");
            var svg = SvgDocument.Open(document);
            return svg.Draw();
        }


        public static MemoryStream SVG2Stream(string svgXml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(svgXml);
            var svg = SvgDocument.Open(document);
            var bmp = svg.Draw();
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms;
        }

        public static byte[] SVG2Png(string svgXml)
        {
            return SVG2Stream(svgXml).ToArray();
        }

        public static void ReadSvg(string svgXml, Action<byte[]> saver)
        {
            Task.Run(() =>
            {
                var bdata = SVG2Png(svgXml);
                saver?.Invoke(bdata);
            });
        }
    }
}
