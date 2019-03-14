using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace _ffpixable
{
    class Program
    {
        /// <summary>
        /// -1: arg = nil
        /// -2: nothing to do
        /// -3: args error
        /// -4: file not exist
        /// -5: unknow error
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            //string logName = "D:\\sr.log";
            //File.AppendAllText(logName, "\n" + DateTime.Now + " Start log+\n");
            //File.AppendAllLines(logName, args); 

            //Action<string> appendLog = log =>
            //{
            //    File.AppendAllText(logName, "\n" + DateTime.Now + "\n" + log + "\n\n");
            //};

            try
            {

                if (args.Length < 1)
                {
                    //appendLog("参数为空");
                    return -1;
                }

                if (File.Exists(args[0]))
                {
                    ReplaceAsThumbnail(args[0], 100, 100);
                    return 1;
                }

                var dic = new Dictionary<string, string>();
                try
                {

                    for (int i = 0; i < args.Length; i++)
                    {
                        var ag = args[i].Split(':');
                        if (ag[0] == "-f")
                            dic.Add("-f", args[i].Remove(0, 3));
                        else
                            dic.Add(ag[0], ag[1]);
                    }
                }
                catch
                {
                    //appendLog("拆解参数错误");
                    return -31;
                }
                if (dic.Count >= 3)
                {
                    try
                    {
                        if (!dic.ContainsKey("-f"))
                        {
                            //appendLog("无法读取文件名参数");
                            return -3;
                        }
                        if (!dic.ContainsKey("-width"))
                        {
                            //appendLog("无法读取宽参数");
                            return -3;
                        }
                        if (!dic.ContainsKey("-height"))
                        {
                            //appendLog("无法读取高参数");
                            return -3;
                        }
                        int w = Convert.ToInt32(dic["-width"]);
                        int h = Convert.ToInt32(dic["-height"]);
                        string fName = dic["-f"];
                        var ff = new FileInfo(fName);
                        if (ff.Exists == false)
                        {
                            //appendLog("找不到文件:" + ff.FullName);
                            return -4;
                        }
                        ReplaceAsThumbnail(dic["-f"], w, h);
                        return 0;
                    }
                    catch (Exception e)
                    {
                        //appendLog(e.ToString());
                        return -5;
                    }
                }
                //appendLog("参数不足");
                return -2;
            }
            catch (Exception e)
            {
                //appendLog(e.ToString());
                //File.AppendAllText(logName, "ERROR:\n" + e.ToString());
                return -0xf;
            }
        }


        static void ReplaceAsThumbnail(string filename, int w, int h)
        {
            Image.GetThumbnailImageAbort bort = Abort;
            Image bmp = Image.FromFile(filename);
            var bmp2 = bmp.GetThumbnailImage(w, h, Abort, IntPtr.Zero);
            bmp.Dispose();
            File.Delete(filename);
            bmp2.Save(filename);
        }

        private static bool Abort()
        {
            return false;
        }

    }
}
