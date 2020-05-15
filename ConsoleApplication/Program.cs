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
//using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using IronPython.Hosting;
using NetMQ.Sockets;
using NetMQ;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Collections;
using HinxCor.Serialization;

class ssr
{
    public string name = "RRE";
}

namespace ConsoleApplication
{
    class girl
    {
        public class man
        {
            public string name = "lili";
            [ARWIgnore]
            public int age = 23;
            public int sex = 26;
            public bool ddd { get; } = false;

            public string location { get; set; } = "127.0.0.1";

            public man(string name)
            {
                this.name = name;
            }
        }

    }

    class Program
    {
        static string source = "Assets/city-prefabs/23-12-1125-19 \u690D\u88AB \u704C\u6728.prefab";

        const string p1 = "  ";
        const string p2 = "    ";
        const string p3 = "- ";

        delegate void act();

        public static string passwd = "cVYAzGYSkmfK4M6UK1CoebMsEPB0KAkD";


        [STAThread]
        static void Main(string[] args)
        {
            var lst = new List<int>(new[] { 1 });

            lst.Insert(2, 3);


            Console.WriteLine(lst);
            //a a = new a();
            //string json = JsonMapper.ToJson(a);
            //var b = JsonMapper.ToObject<b>(json);
            Console.WriteLine("end");
            Console.ReadKey();

        }


        private class a
        {

            public string sa;
            public string sb;
            public bool C { get; } = true;
        }

        private class b
        {

            public string sa;
            public string sb;
            public bool C
            {
                set
                {
                    Console.WriteLine(value);
                }
            }

        }




        // Simple business object.
        public class Person
        {
            public Person(string fName, string lName)
            {
                this.firstName = fName;
                this.lastName = lName;
            }

            public string firstName;
            public string lastName;
        }

        // Collection of Person objects. This class
        // implements IEnumerable so that it can be used
        // with ForEach syntax.
        public class People : IEnumerable
        {
            private Person[] _people;
            public People(Person[] pArray)
            {
                _people = new Person[pArray.Length];

                for (int i = 0; i < pArray.Length; i++)
                {
                    _people[i] = pArray[i];
                }
            }

            // Implementation for the GetEnumerator method.
            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public PeopleEnum GetEnumerator()
            {
                return new PeopleEnum(_people);
            }
        }

        // When you implement IEnumerable, you must also implement IEnumerator.
        public class PeopleEnum : IEnumerator
        {
            public Person[] _people;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public PeopleEnum(Person[] list)
            {
                _people = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _people.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Person Current
            {
                get
                {
                    try
                    {
                        return _people[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }


        private class testEnumerator : ICollection<string>, IEnumerator
        {
            private List<string> source;

            public testEnumerator(params string[] args)
            {
                this.source = new List<string>();
                source.AddRange(args);
            }

            public int Count => source.Count;

            public bool IsReadOnly => false;

            public object Current => source[index];

            private int index = 0;

            public void Add(string item) => source.Add(item);

            public void Clear() => source.Clear();

            public bool Contains(string item) => source.Contains(item);

            public void CopyTo(string[] array, int arrayIndex) => source.CopyTo(array, arrayIndex);

            public IEnumerator<string> GetEnumerator() => source.GetEnumerator();

            public bool MoveNext()
            {
                index++;
                return index < source.Count;
            }

            public bool Remove(string item) => source.Remove(item);

            public void Reset()
            {
                index = 0;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this;
            }
        }


        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private static void GenerateBigFile()
        {
            FileStream fs = new FileStream("438.kk", FileMode.CreateNew);

            var array = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            //Console.WriteLine('_' + i);
            int gig = 1;

            for (int i = 0; i < 1024 * gig / 8; i++)//8G
            {
                for (int j = 0; j < 1024; j++)//8m
                {
                    for (int k = 0; k < 1024; k++)//8k
                    {
                        fs.Write(array, 0, 8);//8b
                    }
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(string.Format("{0} MBytes", i * 8));
            }
            Console.WriteLine();
            Console.WriteLine("done, flush wait.");
            fs.FlushAsync().Wait();
            Console.WriteLine("Finished");
        }

        private static void startp1()
        {
            string exeName = "MixingAudio.exe";
            Console.WriteLine("开始添加音频 ESC:开始合成）");
            var clips = new List<_M_AudioClip>();
            OpenFileDialog mp3dlg = new OpenFileDialog();
            mp3dlg.Filter = "|*.mp3";
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                if (mp3dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var audioClip = new _M_AudioClip();
                    audioClip.url = mp3dlg.FileName;
                    Console.WriteLine("请输入时间");
                    while (!float.TryParse(Console.ReadLine(), out audioClip.start_time))
                        Console.WriteLine("无法获取到输入的时间，请重试");
                    clips.Add(audioClip);
                    Console.WriteLine("Add:" + audioClip.url);
                }
                else
                    Console.WriteLine("Cancel openfile , esc end add audio file");
            }
            Console.WriteLine("开始合成");

            string cfgFile = "audiocfg";
            StringBuilder sb = new StringBuilder();
            foreach (var audioClip in clips)
                sb.Append(audioClip.ToString());
            File.WriteAllText(cfgFile, sb.ToString());

            string saveFile = "mixMp3.mp3";

            //string cmd = " --ws=\"%cd%\audio\" --txt=\"%cd%\\audio\\config.txt\" --fwid=\"fw-123456789-1\" --af=\"%cd%\test-1.mp3\"";
            string cmd = " --ws=\"%cd%\audio\" --txt=\"{0}\" --fwid=\"fw-123456789-1\" --af=\"{1}\"";
            cmd = string.Format(cmd, cfgFile, saveFile);

            ProcessStartInfo startp = new ProcessStartInfo();
            startp.UseShellExecute = false;
            startp.RedirectStandardOutput = true;
            startp.RedirectStandardError = true;
            startp.FileName = exeName;
            startp.Arguments = cmd;
            //var p = new Process();
            //p.StartInfo = startp;
            //p.output
            var p = Process.Start(startp);
            Task.Run(() =>
            {
                while (p.HasExited == false)
                    Console.WriteLine(p.StandardOutput.ReadLine());
            });

            Task.Run(() =>
            {
                while (p.HasExited == false)
                    Console.WriteLine(p.StandardError.ReadLine());
            });


            Console.WriteLine("Prepared.");
            Console.WriteLine();

            while (p.HasExited == false)
                Thread.Sleep(10);
            Console.WriteLine("Finished");

            var openf = new FileInfo(saveFile);
            HinxCor.Windows.OpenInExplorer(openf.FullName);
        }

        private class mAudio
        {
            public string fileName;

            public override string ToString()
            {
                //return "url=app-storage:/475EDFFA11EEC94156351E17F7EDA27E.mp3|start_time=0.0000|end_time=4.9680|offset=0.0000|duration=4.9680|volume=1|speed=1;";
                return string.Format("url={0}|start_time=0.0000|end_time=-1|offset=0.0000|duration=-1|volume=1|speed=1;");
            }
        }

        private class _M_AudioClip
        {
            public string url;      // 路径
            public float start_time = 0;// 秒
            public float end_time = -1;  // 秒
            public float offset = 0;    // 秒
            public float duration = 30;  // 秒
            public float fade_in = -1;   // 秒
            public float fade_out = -1;  // 秒
            public float volume = 1;    // 0~1 
            public float speed = 1;     // 1
            public int loop = -1;        // -1 或者 0 

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}=\"{1}\"|", nameof(url), url));
                sb.Append(string.Format("{0}={1}|", nameof(start_time), start_time));
                sb.Append(string.Format("{0}={1}|", nameof(end_time), end_time));
                sb.Append(string.Format("{0}={1}|", nameof(offset), offset));
                sb.Append(string.Format("{0}={1}|", nameof(duration), duration));
                //sb.Append(string.Format("{0}={1}|", nameof(fade_in), fade_in));
                //sb.Append(string.Format("{0}={1}|", nameof(fade_out), fade_out));
                sb.Append(string.Format("{0}={1}|", nameof(volume), volume));
                sb.Append(string.Format("{0}={1}|", nameof(speed), speed));
                sb.Append(string.Format("{0}={1};", nameof(loop), loop));

                //sb.Append(nameof(start_time) + "=" + start_time);
                //sb.Append('|');
                //sb.Append(nameof(end_time) + "=" + end_time);
                //sb.Append('|');
                //sb.Append(nameof(offset) + "=" + offset);
                //sb.Append('|');
                //sb.Append(nameof(duration) + "=" + duration);
                //sb.Append('|');
                //sb.Append(nameof(fade_in) + "=" + fade_in);
                //sb.Append('|');
                //sb.Append(nameof(fade_out) + "=" + fade_out);
                //sb.Append('|');
                //sb.Append(nameof(volume) + "=" + volume);
                //sb.Append('|');
                //sb.Append(nameof(speed) + "=" + speed);
                //sb.Append('|');
                //sb.Append(nameof(loop) + "=" + loop);
                //sb.Append(';');
                return sb.ToString();

                //return string.Format("{0}=");
            }
        }

        private static void AudioMixingCustom()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "|*.mp4";
            dlg.ShowDialog();
            string videoFile = dlg.FileName;
            dlg.Filter = "|*.mp3";
            dlg.ShowDialog();
            string audioFile = dlg.FileName;

            SaveFileDialog sdlg = new SaveFileDialog();
            //            sdlg.Filter 
            sdlg.ShowDialog();
            string saveFile = sdlg.FileName;


            string cmd = "ffmpeg -i \"{0}\" -i \"{1}\" -c:v copy -filter_complex \"[0:a]aformat = fltp:44100:stereo,apad[0a];[1] aformat=fltp:44100:stereo,volume=1.5[1a];[0a] [1a] amerge[a]\" -map 0:v -map \"[a]\" -ac 2 \"{2}\"";
            cmd = "ffmpeg.exe -i {0} -i {1} -filter_complex \"[1:0]volume = 0.5[a1];[0:a] [a1] amix=inputs=2:duration=first\" -map 0:v:0 {2}";
            cmd = string.Format(cmd, videoFile, audioFile, saveFile);
            Console.WriteLine(cmd);
            HinxCor.Windows.ExecuteCommand(cmd);
        }

        // Return True if the internet settings has ProxyEnable = true.
        private static bool IsInternetProxyEnabled()
        {
            //Registry.CurrentUser.OpenSubKey
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
            string[] keys = key.GetValueNames();
            bool result = (int)key.GetValue("ProxyEnable", 0) != 0;
            key.Close();

            return result;
        }


        private static void SetInternetProxyEnabled(bool value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            string[] keys = key.GetValueNames();
            key.SetValue("ProxyEnable", value ? 1 : 0);
            key.Close();
        }


        private static void OpenSvgAndSaveToPng()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "svg file|*.svg";
            dlg.ShowDialog();
            string xml = File.ReadAllText(dlg.FileName);
            var pngData = HinxCor.SVG.SVGUtil.SVG2Png(xml);

            SaveFileDialog sdlg = new SaveFileDialog();
            sdlg.Filter = "png图像|*.png";
            sdlg.ShowDialog();
            File.WriteAllBytes(sdlg.FileName, pngData);
            HinxCor.Windows.OpenInExplorer(sdlg.FileName);
        }

        static int[] grans = new[] { 1, 2, 3, 4, 3, 2, 1, 4, 3, 13, 13, 13, 13, 23 };
        static int gran = 0;

        private static void HandleRef(ref int i)
        {
            gran++;
            i = grans[gran];
        }

        static string titleTxt = "-->右键测试<-\\1080";

        private static void UnRegisterRightClick()
        {
            RegistryKey shell = Registry.ClassesRoot.OpenSubKey("*", true).OpenSubKey("shell", true);
            if (shell != null)
                shell.DeleteSubKeyTree(titleTxt);

            shell = Registry.ClassesRoot.OpenSubKey("directory", true).OpenSubKey("shell", true);
            if (shell != null)
                shell.DeleteSubKeyTree(titleTxt);

            shell.Close();

            System.Windows.Forms.MessageBox.Show("反注册成功!", "提示");
        }

        private static void RegisterRightClick(int index)
        {
            if (titleTxt.Length == 0)
                return;

            // 注册到文件
            if (/*this.ckRegToFile.Checked*/ true)
            {
                string destiType = "*";
                //var ns = new List<string>(Registry.ClassesRoot.GetSubKeyNames());
                RegistryKey pdbKey = Registry.ClassesRoot.OpenSubKey(destiType, true);
                //if (ns.Contains(destiType) == false)
                if (pdbKey == null)
                    pdbKey = Registry.ClassesRoot.CreateSubKey(destiType);
                pdbKey.SetValue("Icon", GetIconPath(), RegistryValueKind.ExpandString);

                RegistryKey shell = pdbKey.OpenSubKey("shell", true);
                if (shell == null)
                    shell = pdbKey.CreateSubKey("shell");

                RegistryKey custome = shell.CreateSubKey("转换为1080P");
                RegistryKey cmd = custome.CreateSubKey("command");
                cmd.SetValue("", "to1080p" + " %1");
                Console.WriteLine(string.Format("Key:{0}", Application.ExecutablePath));
                cmd.Close();
                custome.Close();
                shell.Close();
                pdbKey.Close();
            }

            // 注册到文件夹
            if (/*this.ckRegToDir.Checked*/ false)
            {
                RegistryKey shell = Registry.ClassesRoot.OpenSubKey("directory", true).OpenSubKey("shell", true);
                if (shell == null)
                    shell = Registry.ClassesRoot.OpenSubKey("directory", true).CreateSubKey("shell");
                RegistryKey custome = shell.CreateSubKey(titleTxt);
                RegistryKey cmd = custome.CreateSubKey("command");
                cmd.SetValue("", Application.ExecutablePath + " %1");
                cmd.Close();
                custome.Close();
                shell.Close();
            }
            Console.WriteLine("Reg key Finished.");
        }

        private static object GetIconPath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ico file|*.ico";
            while (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                ;
            return dlg.FileName;
        }

        private class foregroundWindow : IWin32Window
        {
            public IntPtr Handle { get; private set; }

            internal static foregroundWindow current
            {
                get
                {
                    return new foregroundWindow()
                    {
                        Handle = User32.GetForegroundWindow()
                    };
                }
            }
        }

        private static void TestNetMq()
        {
            using (var server = new ResponseSocket())
            {
                server.Bind("tcp://*:5555");
                string msg = server.ReceiveFrameString();
                Console.WriteLine("From Client: {0}", msg);
                server.SendFrame("World");
            }
        }

        private static void NewMethod()
        {
            OpenFileDialog openf = new OpenFileDialog();
            openf.Filter = "|*.fsv";
            openf.ShowDialog();
            ZipFile zip = new ZipFile(openf.FileName);
            zip.Password = passwd;
            foreach (var entry in zip.Entries)
            {
                if (entry.FileName.EndsWith(".mf"))
                {
                    var ms = new MemoryStream();
                    entry.Extract(ms);
                    ms.Position = 0;
                    var data = ms.ToArray();
                    var content = Encoding.UTF8.GetString(data);
                    Console.WriteLine(System.Web.HttpUtility.UrlDecode(entry.FileName));
                    Console.WriteLine(content);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("finished. any key esc.");
            Console.ReadKey();
        }

        private static void zipTest()
        {
            string zipName = "save.zip";
            ZipFile zips = new ZipFile(zipName);

        CMD:
            Console.WriteLine("1=add file, 2=displayNames; 3=clear names");
            Console.Write("请输入：");
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "1":
                    OpenFileDialog dlg = new OpenFileDialog();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //zips.Add(dlg.FileName);
                        var ets = zips.AddFile(dlg.FileName);
                        ets.FileName = inname(new FileInfo(dlg.FileName).Name);
                        Console.WriteLine("Add file:" + ets.FileName);
                    }
                    Console.WriteLine();
                    goto CMD;
                case "2":
                    var ns = zips.EntryFileNames;
                    foreach (var name in ns)
                    {
                        Console.WriteLine(outname(name));
                    }
                    Console.WriteLine();
                    goto CMD;
                case "3":
                    var entrys = new List<ZipEntry>(zips.Entries);
                    for (int i = 0; i < entrys.Count; i++)
                    {
                        var entry = entrys[i];
                        string ename = entry.FileName;
                        entry.FileName = new FileInfo(ename).Name;
                    }
                    Console.WriteLine("Clear finished,");
                    goto CMD;
                case "4":
                    SaveFileDialog saveFile = new SaveFileDialog();
                    if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        zips.SaveSelfExtractor(saveFile.FileName, SelfExtractorFlavor.ConsoleApplication);
                    }


                    Console.WriteLine("Clear finished,");
                    goto CMD;
                default:
                    break;
            }
            zips.SaveProgress += Zips_SaveProgress;
            zips.Save();
            Console.WriteLine();
            Console.WriteLine("Finished .");
            Console.ReadKey();
        }

        private static string inname(string source)
        {
            return System.Web.HttpUtility.UrlEncode(source);
        }
        private static string outname(string source)
        {
            return System.Web.HttpUtility.UrlDecode(source);
        }

        private static void Zips_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            Console.Write(".");
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
