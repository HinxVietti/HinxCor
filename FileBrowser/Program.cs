using HinxCor.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DialogResult = System.Windows.Forms.DialogResult;

namespace FileBrowser
{
    static class Program
    {
        static UnicClient client;
        static IPEndPoint server;
        static bool exit = false;



        class ForegroundWindow : IWin32Window
        {
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            static ForegroundWindow obj = null;
            public static ForegroundWindow CurrentWindow
            {
                get
                {
                    if (obj == null)
                        obj = new ForegroundWindow();
                    return obj;
                }
            }
            public IntPtr Handle
            {
                get { return GetForegroundWindow(); }
            }
        }


        //port multiple_select  filter maincmd
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            if (!int.TryParse(args[0], out var port))
            {
                var lst = new List<string>(args);
                lst.Add("error open file browser.");
                File.WriteAllLines("fb_error, error parse port number", lst);
                return -1;
            }

            server = new IPEndPoint(localIp, port);
            port = NetworkEnv.GetAvailableUdpPort();
            client = new UnicClient(port, (msg, ep) =>
            { });

            bool multipleSelect = bool.Parse(args[1]);
            string filter = args[2];
            int mainCmd = int.Parse(args[3]);
            switch (mainCmd)
            {
                case saveFile:
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = filter;
                    switch (sf.ShowDialog(ForegroundWindow.CurrentWindow))
                    {
                        case DialogResult.OK:
                            client.SendMessage(server, sf.FileName);
                            break;
                        case DialogResult.None:
                        case DialogResult.Cancel:
                        case DialogResult.Abort:
                        case DialogResult.Retry:
                        case DialogResult.Ignore:
                        case DialogResult.Yes:
                        case DialogResult.No:
                        default:
                            client.SendMessage(server, "cancel");
                            break;
                    }
                    break;
                case OpenFolder:
                    FolderBrowserDialog fb = new FolderBrowserDialog();
                    fb.RootFolder = Environment.SpecialFolder.MyComputer;
                    switch (fb.ShowDialog(ForegroundWindow.CurrentWindow))
                    {
                        case DialogResult.OK:
                            client.SendMessage(server, fb.SelectedPath);
                            break;
                        case DialogResult.None:
                        case DialogResult.Cancel:
                        case DialogResult.Abort:
                        case DialogResult.Retry:
                        case DialogResult.Ignore:
                        case DialogResult.Yes:
                        case DialogResult.No:
                        default:
                            client.SendMessage(server, "cancel");
                            break;
                    }
                    break;
                default:
                    OpenFileDialog openfile = new OpenFileDialog();
                    openfile.Multiselect = multipleSelect;
                    openfile.Filter = filter;
                    switch (openfile.ShowDialog(ForegroundWindow.CurrentWindow))
                    {
                        case DialogResult.OK:
                            string ret = string.Empty;
                            if (multipleSelect)
                            {
                                StringBuilder sb = new StringBuilder();
                                for (int i = 0; i < openfile.FileNames.Length; i++)
                                {
                                    sb.Append(openfile.FileNames[i]);
                                    if (i != openfile.FileNames.Length - 1)
                                        sb.Append('|');
                                }
                                ret = sb.ToString();
                            }
                            else ret = openfile.FileName;
                            client.SendMessage(server, ret);
                            break;
                        case DialogResult.None:
                        case DialogResult.Cancel:
                        case DialogResult.Abort:
                        case DialogResult.Retry:
                        case DialogResult.Ignore:
                        case DialogResult.Yes:
                        case DialogResult.No:
                        default:
                            client.SendMessage(server, "cancel");
                            break;
                    }
                    break;
            }

            client.SendMessage(server, "browserExit");
            return 0;
        }

        const int saveFile = 1;
        const int OpenFolder = 2;

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
}



/*
 
        private static int M1(string[] args)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            string fileName = openFile.FileName;
            var localIp = IPAddress.Parse("127.0.0.1");
            if (!int.TryParse(args[0], out var port))
            {
                var lst = new List<string>(args);
                lst.Add("error open file browser.");
                File.WriteAllLines("fb_error, error parse port number", lst);
                return -1;
            }
            server = new IPEndPoint(localIp, port);
            port = NetworkEnv.GetAvailableUdpPort();
            client = new UnicClient(port, MainMethod);
            client.SendMessage(server, "reg#" + port.ToString());
            client.SendMessage(server, fileName);
            while (!exit)
                Thread.Sleep(10);
            ;
            return 0;
        }

        private static void MainMethod(byte[] data, IPEndPoint ep)
        {
            try
            {
                string source = Encoding.UTF8.GetString(data);

                client.SendMessage(server, "rev#" + source);

                var cmd = FBCMD.FromParms(source.Split(FBCMD.splitChar));

                client.SendMessage(server, new FBCMD()
                {
                    cmd = FBCMDString.receiveRespone,
                    uid = cmd.uid,
                    parm = new[] { FBCMDString.success }
                }.ToString());

                HandleCmd(cmd);
            }
            catch { }

        }

        private static void HandleCmd(FBCMD cmd)
        {
            switch (cmd.cmd)
            {
                case FBCMDString.OpenFile:
                    FBCMD response = OpenFile(cmd);
                    client.SendMessage(server, response.ToString());
                    break;
                default:
                    break;
            }

        }

        private static FBCMD OpenFile(FBCMD cmd)
        {
            client.SendMessage(server, "msg#" + "openfile dialog.");
            OpenFileDialog openFile = new OpenFileDialog();
            string filter = cmd?.parm?[0];
            filter = filter == null ? "" : filter;
            client.SendMessage(server, "msg#" + "openfile continue." + filter);
            openFile.Filter = filter;
            var res = openFile.ShowDialog();
            client.SendMessage(server, "msg#" + "openfile finished.");
            return new FBCMD()
            {
                cmd = FBCMDString.result,
                uid = cmd.uid,
                parm = new[] {res.ToString(),
                openFile.FileName}
            };
        }
     */
