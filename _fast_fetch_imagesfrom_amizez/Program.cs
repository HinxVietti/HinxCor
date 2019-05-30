using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace _fast_fetch_imagesfrom_amizez
{
    class Program
    {
        static void Main(string[] args)
        {
            爬网络素材资源();
        }

        static WebClient clientRoot;

        private static void 爬网络素材资源()
        {
            //http://www.animiz.cn/client/resource/hand-painted

            // string jsonURL = @"http://www.animiz.cn/client/resource/hand-painted?search=&pagesize=30&page=1&catid=3";


            clientRoot = new WebClient();

            List<Thread> threads = new List<Thread>();
            bool islaststarted = false;

            for (int tcatid = 1; tcatid < 30; tcatid++)
            {
                var thr = new Thread(() =>
                {
                    int catid = tcatid;
                    islaststarted = true;
                    try
                    {
                        var client = new WebClient();
                        int page = 0;
                        nextpage: page++;
                        Console.WriteLine();
                        Console.WriteLine("开始下载Part:" + page + ",CID: " + catid);
                        string jsonRequest = string.Format(@"http://www.animiz.cn/client/resource/hand-painted?search=&pagesize=60&page={1}&catid={0}", catid, page);
                        var json = client.DownloadString(jsonRequest);
                        var res = LitJson.JsonMapper.ToObject<ImageList>(json);
                        if (res.data.Length > 0 && page < 10)
                        {
                            for (int i = 0; i < res.data.Length; i++)
                                DownLoadImageItem(res.data[i], client, catid);
                            goto nextpage;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(catid + " error:" + e);
                    }
                    Console.WriteLine(catid + " has exit");
                    Console.WriteLine();
                })
                { IsBackground = true };
                threads.Add(thr);
                thr.Start();
                while (islaststarted == false) Thread.Sleep(10);
                islaststarted = false;
            }



            while (threads.Count > 0)
            {
                var todel = new List<Thread>();
                for (int i = 0; i < threads.Count; i++)
                    if (threads[i].IsAlive == false)
                        todel.Add(threads[i]);
                for (int i = 0; i < todel.Count; i++)
                    threads.Remove(todel[i]);
                Console.WriteLine("CLC:" + threads.Count);
                if (threads.Count == 0) break;
                Thread.Sleep(3000);
            }

            Console.WriteLine();
            Console.WriteLine("FINIEHED");
            Console.ReadKey();
        }

        private static void DownLoadImageItem(ImageItem imageItem, WebClient client, int catid)
        {
            string folderName = "Download/" + catid + "/";
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
            string fname = imageItem.title + "--" + imageItem.thumbnail.Remove(0, imageItem.thumbnail.LastIndexOf('/') + 2);
            //FileInfo desfile = new FileInfo(folderName + fname);
            //if (desfile.Directory.Exists == false) desfile.Directory.Create();
            //client.DownloadFile(imageItem.thumbnail, folderName + fname);
            Console.WriteLine("下载...: " + imageItem.thumbnail);
            //client.DownloadFile(imageItem.url, folderName + fname + ".svg");
            Console.WriteLine("下载...:" + imageItem.url);
        }

        private class ImageList
        {
            public object status { get; set; }
            public accessitem[] access { get; set; }
            public ImageItem[] data { get; set; }

        }

        private class accessitem
        {
            public object access { get; set; }
            public string type { get; set; }
        }

        private class ImageItem
        {
            public string title { get; set; }
            public string url { get; set; }
            public string thumbnail { get; set; }
            public string access { get; set; }
        }

    }
}
