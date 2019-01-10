using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HinxCor.Network
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebClient client = new WebClient();
            ////string url = @"http://9453bb.com/thread-118328-1-1.html";
            ////string h5txt = client.DownloadString(url);
            ////File.WriteAllText("simple.html", h5txt);
            //string h5txt = File.ReadAllText("simple.html");

            //var strs = GetHvtImgUrls(h5txt);



            //foreach (var pth in strs)
            //{
            //    if (pth.Contains("http"))
            //    {
            //        string fileName = GetFname(pth);
            //        client = new WebClient();
            //        client.DownloadFile(pth, fileName);
            //        Console.WriteLine("DownLoadFile:" + fileName);
            //    }
            //}
            //Console.WriteLine("Over..");
            //Console.ReadKey();
          //OpenFileDialog 

        }


        private static string GetFname(string pth)
        {
            int index = pth.LastIndexOf('/');
            return pth.Remove(0, index);
            //return "Image_00" + index++ + "";
        }

        //效果 http://tool.hovertree.com/a/zz/img/
        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHvtImgUrls(string sHtmlText)
        {

            Regex m_hvtRegImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            //参考:http://hovertree.com/hvtart/bjae/e4pya1x0.htm

            // 搜索匹配的字符串 
            MatchCollection matches = m_hvtRegImg.Matches(sHtmlText);
            int m_i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[m_i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }


        //public List<string> ImageList;
        //public void GetAllImages()
        //{
        //    // Declaring 'x' as a new WebClient() method
        //    WebClient x = new WebClient();

        //    // Setting the URL, then downloading the data from the URL.
        //    string source = x.DownloadString(@"http://www.google.com");

        //    // Declaring 'document' as new HtmlAgilityPack() method
        //    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

        //    // Loading document's source via HtmlAgilityPack
        //    document.LoadHtml(source);

        //    // For every tag in the HTML containing the node img.
        //    foreach (var link in document.DocumentNode.Descendants("img")
        //                                .Select(i => i.Attributes["src"]))
        //    {
        //        // Storing all links found in an array.
        //        // You can declare this however you want.
        //        ImageList.Add(link.Attribute["src"].Value.ToString());
        //    }
        //}

    }
}
