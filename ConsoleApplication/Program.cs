using HinxCor.Network;
using HinxCor.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LitJson;
using ICSharpCode.SharpZipLib.Zip;

namespace ConsoleApplication
{
    class Program
    {
        static string source = "Assets/city-prefabs/23-12-1125-19 \u690D\u88AB \u704C\u6728.prefab";

        const string p1 = "  ";
        const string p2 = "    ";
        const string p3 = "- ";

        delegate void act();

        [STAThread]
        static void Main(string[] args)
        {
            string zipName = "testZip";

            Console.WriteLine("请输入CMD");
            Console.WriteLine("1.添加文件、\n2.查看列表 ");


            string cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    OpenFileDialog dlg = new OpenFileDialog();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (ZipFile zip = new ZipFile(zipName))
                        {
                            zip.BeginUpdate();
                            zip.Add(dlg.FileName);
                           // zip.CommitUpdate();
                        }
                    }
                    //using (ZipOutputStream zip = new ZipOutputStream(new FileStream(zipName, FileMode.OpenOrCreate)))
                    //{
                    //    OpenFileDialog dlg = new OpenFileDialog();
                    //    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //    {
                    //        var cfile = new FileInfo(dlg.FileName);
                    //        var entry = new ZipEntry(cfile.Name);
                    //        zip.PutNextEntry(entry);
                    //        long writeSize = 0;
                    //        int buffSize = 2048;
                    //        int readSize = 0;
                    //        byte[] buffer = new byte[buffSize];
                    //        using (FileStream fs = new FileStream(cfile.FullName, FileMode.Open))
                    //        {
                    //            do
                    //            {
                    //                readSize = fs.Read(buffer, 0, buffSize);// 0 是buffer的offset
                    //                zip.Write(buffer, 0, readSize);
                    //                writeSize += readSize;
                    //                //sync_process(0.3f + 0.7f * writeSize / size);
                    //            } while (readSize > 0);
                    //        }
                    //        zip.Flush();
                    //    }
                    //}
                    break;
                case "2":
                    ZipFile zipFile = new ZipFile(zipName);
                    for (int i = 0; i < zipFile.Count; i++)
                    {
                        Console.WriteLine(zipFile[i].Name);
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("end.");
            Console.ReadKey();
        }









        private static void dlgTest()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            List<string> words = new List<string>();
            string txt = File.ReadAllText(dlg.FileName);

            txt.Replace('\r', ' ');
            var ls = txt.Split('\n', '、', ' ', '\r');

            //var ls = File.ReadAllLines(dlg.FileName);
            for (int i = 0; i < ls.Length; i++)
            {
                string ss = ls[i];
                if (string.IsNullOrEmpty(ss))
                    continue;

                var arg = ss.Split('|');
                ss = arg[0];
                ss.Trim();
                if (!string.IsNullOrEmpty(ss) && !string.IsNullOrWhiteSpace(ss))
                    if (!words.Contains(ss))
                        words.Add(ss);
            }
            File.WriteAllLines("save", words.ToArray());
            Console.WriteLine("finished");
            Console.ReadKey();
        }

        private static void TaskTest()
        {
            //Task myTask = Task.Factory.StartNew(GetFile);
            Task myTask = new Task(GetFile);
            Console.WriteLine("RunSynchronously");
            myTask.RunSynchronously();
            //myTask.Wait(); // wait for myTask to complete
            Console.WriteLine("Main thread/");

            for (int i = 0; i < m_WordArray.Length; i++)
                Console.Write(m_WordArray[i]);
            Console.WriteLine();
            Console.WriteLine("new end of frame.");
        }

        static string[] m_WordArray;

        internal static async void GetFile()
        {
            Stream stream = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((stream = dialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string temp = reader.ReadToEnd();
                                m_WordArray = temp.Replace
                                    ("\r", string.Empty).Replace("\n", " ")
                                .Replace("\"", string.Empty).Split(' ').ToArray<string>();
                                // Console.Read();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
        }

        private static string GetFile(string filter = "")
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!string.IsNullOrEmpty(filter))
                dlg.Filter = filter;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dlg.FileName;
            //return string.Empty;
            return "Not select any file.";
        }

        private static void ProcessGetFiles()
        {
            var port = NetworkEnv.GetAvailableUdpPort();
            UnicClient client = null;
            Process p = null;
            client = new UnicClient(port, (dat, ep) =>
            {
                var str = Encoding.UTF8.GetString(dat);
                if (str.StartsWith("rev#"))
                {
                    Console.WriteLine("rev\t" + str.Remove(0, 4));
                }
                else if (str.StartsWith("msg#"))
                {
                    Console.WriteLine("msg\t" + str.Remove(0, 4));
                }
                else
                    Console.WriteLine(str);
                if (p?.HasExited == false)
                    p?.Kill();
            });

            //ThreadTag();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "FileBrowser.exe";
            startInfo.Arguments = getOpenFileArgs(true, "|*.png;*.jpg", port);
            p = Process.Start(startInfo);


            //TestOpenFile();
            Console.WriteLine("line1");
            while (p.HasExited == false)
                Thread.Sleep(100);
            client.Dispose();
            Console.WriteLine("finished");
        }

        private static string getOpenFileArgs(bool multipleSelect, string filter, int port)
        {
            return string.Format("{0} {1} {2} 0", port, multipleSelect, filter);
        }

        private static void ThreadTag()
        {
            string fname = string.Empty;
            Thread p = new Thread(() =>
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.ShowDialog();
                fname = openfile.FileName;
            });
            p.SetApartmentState(ApartmentState.STA);
            p.Start();

            while (p.IsAlive)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine(fname);
            Console.WriteLine("finished");
            Console.ReadKey();
        }

        private static void TestOpenFile()
        {
            var port = NetworkEnv.GetAvailableUdpPort();
            IPEndPoint server = null;
            UnicClient client = null;
            client = new UnicClient(port, (dat, ep) =>
            {
                var str = Encoding.UTF8.GetString(dat);
                if (str.StartsWith("reg#"))
                {
                    int _port = int.Parse(str.Remove(0, 4));
                    Console.WriteLine("register: " + _port);
                    server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port);
                    FBCMD cmd = new FBCMD()
                    {
                        cmd = FBCMDString.OpenFile,
                        uid = "f6we541f6we41",
                        parm = new[] { "图像文件|*.png" }
                    };
                    client.SendMessage(server, cmd.ToString());
                }
                else if (str.StartsWith("rev#"))
                {
                    Console.WriteLine("rev\t" + str.Remove(0, 4));
                }
                else if (str.StartsWith("msg#"))
                {
                    Console.WriteLine("msg\t" + str.Remove(0, 4));
                }
                else
                    Console.WriteLine(str);
            });
            Console.WriteLine("Clien has init");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "FileBrowser.exe";
            startInfo.Arguments = port.ToString();
            var p = Process.Start(startInfo);
            //准备工作完成；开始发送cmd
            Console.WriteLine();
            while (p.HasExited == false)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    p.Kill();
                    break;
                }
                Thread.Sleep(10);
            }
            Console.WriteLine();
            Console.WriteLine(p.ExitCode);
            Console.WriteLine("console end");
            Console.ReadKey();
        }

        public class FBCMDString
        {
            public const string OpenFile = "OpenFile";
            public const string receiveRespone = "received";
            public const string result = "received";
            public const string success = "success";
        }

        /// <summary>
        /// 不能包含';'
        /// </summary>
        public class FBCMD
        {
            public string cmd { get; set; }
            public string uid { get; set; }
            public string[] parm { get; set; }

            public const char splitChar = '\t';


            public static FBCMD FromParms(params string[] args)
            {
                if (args.Length < 3)
                    throw new Exception("arg length less 3");
                var pars = new string[args.Length - 2];
                for (int i = 0; i < args.Length - 2; i++)
                {
                    pars[i] = args[i + 2];
                }

                return new FBCMD()
                {
                    cmd = args[0],
                    uid = args[1],
                    parm = pars
                };
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(cmd);
                sb.Append(splitChar);
                sb.Append(uid);
                sb.Append(splitChar);
                for (int i = 0; i < parm.Length; i++)
                {
                    sb.Append(parm[i]);
                    if (i != parm.Length - 1)
                        sb.Append(splitChar);
                }
                return sb.ToString();
            }
        }


        private static Dictionary<string, List<string>> GetDictionary(string origin)
        {
            origin = Regex.Replace(origin, ": ", "\n" + p1);
            var txts = origin.Split('\n');
            var dict = new Dictionary<string, List<string>>();
            List<string> current = null;
            bool lastIsG = false;
            for (int i = 0; i < txts.Length; i++)
            {
                string parm = txts[i];

                if (parm.StartsWith(p3))
                {
                    parm = parm.Trim();
                    parm = Regex.Replace(parm, p3, string.Empty);
                    current.Add(parm);
                    lastIsG = true;
                }
                else if (parm.StartsWith(p2))
                {
                    parm = parm.Trim();
                    parm = Regex.Replace(parm, p2, string.Empty);
                    current.Add(parm);
                    lastIsG = false;
                }
                else if (parm.StartsWith(p1))
                {
                    parm = parm.Trim();
                    parm = Regex.Replace(parm, p1, string.Empty);
                    if (lastIsG)
                        current[current.Count - 1] += parm;
                    else
                        current.Add(parm);
                }
                else
                {
                    //new key;
                    var tk = parm.Split(':');
                    if (dict.ContainsKey(tk[0]) == false)
                    {
                        dict.Add(tk[0], new List<string>());
                        current = dict[tk[0]];
                    }
                }
            }
            return dict;
        }





        static string Unescape(string str)
        {
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            while (true)
            {
                int index = IndexOfBackslashU(str, startIndex);
                if (index == -1)
                    return builder.Append(Regex.Unescape(str.Substring(startIndex))).ToString();
                builder.Append(Regex.Unescape(str.Substring(startIndex, index - startIndex)));
                string number = str.Substring(index + 2, 8);
                builder.Append(char.ConvertFromUtf32(int.Parse(number, NumberStyles.HexNumber)));
                startIndex = index + 10;
            }
        }

        static int IndexOfBackslashU(string str, int startIndex)
        {
            while (true)
            {
                int index = str.IndexOf(@"\U", startIndex);
                if (index == -1)
                    return index;
                bool evenNumberOfPreviousBackslashes = true;
                for (int k = index - 1; k >= 0 && str[k] == '\\'; k--)
                    evenNumberOfPreviousBackslashes = !evenNumberOfPreviousBackslashes;
                if (evenNumberOfPreviousBackslashes)
                    return index;
                startIndex = index + 2;
            }
        }


        private static void 净化Json()
        {

        }


        private static void GenerateJsonFile()
        {
            FolderBrowserDialog openFolder = new FolderBrowserDialog();
            openFolder.RootFolder = Environment.SpecialFolder.MyComputer;
            openFolder.ShowDialog();
            string rootDir = openFolder.SelectedPath;
            DirectoryInfo root = new DirectoryInfo(rootDir);

            var files = new List<string>(Directory.GetFiles(rootDir, "*", SearchOption.AllDirectories));
            Console.WriteLine(files.Count);
            files.RemoveAll(file => file.EndsWith(".manifest"));
            Console.WriteLine(files.Count);
            files.RemoveAll(file => file.EndsWith(".png"));
            Console.WriteLine(files.Count);
            string trim = "D:\\Workspace\\vision_3d_standalone\\Assets\\";
            var l = trim.Length;
            for (int i = 0; i < files.Count; i++)
            {
                files[i] = files[i].Remove(0, l);
            }

            string json = LitJson.JsonMapper.ToJson(files);
            File.WriteAllText(rootDir + "/save.json", json);
            Console.ReadKey();
        }

        private static void ReadFileBloomberg()
        {
            string fileName = "core.dat";
            using (var reader = File.OpenRead(fileName))
            {

            }
        }

        private static void SaveFileBloomberg()
        {
            string txtFileContent = "hello hinx.";
            string saveName = "save.dat";
            File.WriteAllText(saveName, txtFileContent);

            var vNameDat = Encoding.UTF8.GetBytes(saveName);
            var vdat = File.ReadAllBytes(saveName);

            int byteCount = vNameDat.Length;
            var start0 = byteCount + 4;

            var lengthByteData = BitConverter.GetBytes(byteCount);

            using (FileStream saveFile = new FileStream("core.dat", FileMode.Create))
            {
                saveFile.Write(lengthByteData, 0, lengthByteData.Length);
                saveFile.Write(vNameDat, 0, vNameDat.Length);
                saveFile.Write(vdat, 0, vdat.Length);
                saveFile.Flush();
            }
        }



    }
}
