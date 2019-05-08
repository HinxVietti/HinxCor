using HinxCor.IO;
using HinxCor.Network;
using HinxCor.Serialize;
using HinxCor.Wins.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ApplicationLauncher
{
    public partial class Loadout : TransparencyForm
    {
        private delegate void Vfunc();
        private UdpClient client;
        private int Port;

        public Loadout()
        {
            InitializeComponent();//初始化组建
            precheck();//预处理
            CenterToScreen();//更新显示方案
            StartRunableThread();//启动监听
        }

        private void precheck()
        {

            Port = NetworkEnv.GetAvailableUdpPort();
            client = new UdpClient(Port);
        }

        private void StartRunableThread()
        {
            var rundataFileName = "ALProfiles";
            FileStream fs = new FileStream(rundataFileName, FileMode.Open);
            BundleFile bf = new BundleFile(fs);

            bf.StartPop();
            var entry1 = bf.PopEntry() as TxtFileEntry;
            var entry2 = bf.PopEntry() as PNGFileEntry;
            Arguments args = Arguments.CreateFrom(entry1.GetText());
            string exeName = args.GetArgument("-exe");
            var deargs = args.GetArgumentList("-arg");
            deargs.Add("cmdport:" + Port);

            if (!string.IsNullOrEmpty(exeName))
            {
                var exefile = new FileInfo(exeName);
                if (exefile.Exists)
                {
                    ProcessStartInfo runexe = new ProcessStartInfo(exefile.FullName);
                    runexe.WorkingDirectory = exefile.Directory.FullName;

                    runexe.Arguments = Arguments.PackArguments(deargs);
                    Process p = Process.Start(runexe);
                    p.Exited += P_Exited;
                }
                else
                {
                    File.WriteAllText("log.txt", "Progarm not start cause app not found:" + exefile);
                    //Close();
                }
            }

            CenterToScreen();
            Display.Image = entry2.GetImage();
            this.Size = Display.Image.Size;
            var bitmap = (Bitmap)Display.Image;
            SetBitmap(bitmap, 255);
            CenterToScreen();
            Thread thr = new Thread(Entry);
            thr.Start();

            //try
            //{
            //    string ename = "ALProfiles/execute.ini";
            //    string exename = string.Empty;
            //    List<string> args = new List<string>();

            //    using (StreamReader reader = new StreamReader(ename))
            //    {
            //        List<string> cmds = new List<string>();
            //        string str;
            //        while (!string.IsNullOrEmpty(str = reader.ReadLine()))
            //            cmds.Add(str);

            //        foreach (var cmdline in cmds)
            //        {
            //            //var kvp = cmdline.Split(':');
            //            var kvp = Arguments.GetKeyValuePare(cmdline);
            //            switch (kvp.Key)
            //            {
            //                case "-exe":
            //                    exename = kvp.Value;
            //                    break;
            //                case "-arg":
            //                    args.Add(kvp.Value);
            //                    break;
            //                default:
            //                    throw new ArgumentException(string.Format("参数:{0}, 无效", kvp));
            //            }
            //        }
            //    }

            //    args.Add("cmdport:" + Port);

            //    if (!string.IsNullOrEmpty(exename))
            //    {
            //        var exefile = new FileInfo(exename);
            //        if (exefile.Exists)
            //        {
            //            ProcessStartInfo runexe = new ProcessStartInfo(exefile.FullName);
            //            runexe.WorkingDirectory = exefile.Directory.FullName;

            //            runexe.Arguments = Arguments.PackArguments(args);
            //            Process p = Process.Start(runexe);
            //            p.Exited += P_Exited;
            //        }
            //    }

            //}
            //catch
            //{
            //    this.Close();
            //    return;
            //}

            //CenterToScreen();
            //Display.Image = Image.FromFile("ALProfiles/loadout.png");
            //this.Size = Display.Image.Size;
            ////var bd = screen
            //var bitmap = (Bitmap)Display.Image;
            //SetBitmap(bitmap, 255);
            //CenterToScreen();
            //Thread thr = new Thread(Entry);
            //thr.Start();
        }

        private void P_Exited(object sender, EventArgs e)
        {
            this.Close();
        }

        //TcpClient client;

        private unsafe void Entry()
        {
            var closef = new Vfunc(Close);
            IPEndPoint IP = new IPEndPoint(0, 0);
            //string EndName = "~startok.mr";
            while (true)
            {
                var dgram = client.Receive(ref IP);
                var message = Encoding.UTF8.GetString(dgram);
                if (message.Equals("exit"))
                    break;
            }
            Invoke(closef);
        }
    }
}
