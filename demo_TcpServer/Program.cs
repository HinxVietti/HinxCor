using HinxCor.InterProcessCommunication;
using System;
using System.Threading;
using System.Windows.Forms;
using ZetaIpc.Runtime.Server;

namespace demo_TcpServer
{
    class Program
    {
        [STAThread]
        static void Main(string[] argse)
        {
            //Console.WriteLine(OpenFile());

            var server = IPCUtil.GetServer();
            server.RegisterCmdHandle(new onRevHandler());
            server.RegisterCmdHandle(new cmdHandler());
            server.RegisterCmdHandle(new cmdHandler2());
            server.RegisterCmdHandle(new cmdHandler3());
            Console.WriteLine(server.Port);
            Console.WriteLine("Server start.any key esc.");
            Console.ReadKey();
        }

        private class onRevHandler : ICmdHandler
        {
            public string name => "revhandler";

            public ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received)
            {
                Console.WriteLine("___________________________________________RECEIVE CMD_______________________________________");
                return received;
            }
        }

        private class cmdHandler : ICmdHandler
        {
            public string name => "加法";

            public ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received)
            {
                Console.WriteLine("\t\t ---" + name);
                if (received.Handled) return received;

                string cmd = received.GetCmd(out var args);
                if (cmd.Equals("plus"))
                {
                    Console.WriteLine(name + " REV:" + received.Request.Replace('|', ' '));
                    int a = int.Parse(args[0]);
                    int b = int.Parse(args[1]);
                    string result = string.Format("{0}+{1}={2}", a, b, a + b);
                    received.Response = result;
                    Console.WriteLine(result);
                    received.Handled = true;
                }

                return received;
            }
        }

        private class cmdHandler2 : ICmdHandler
        {
            public string name => "乘法";

            public ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received)
            {
                Console.WriteLine("\t\t ---" + name);
                if (received.Handled) return received;

                string cmd = received.GetCmd(out var args);
                if (cmd.Equals("x"))
                {
                    Console.WriteLine(name + " REV:" + received.Request);
                    int a = int.Parse(args[0]);
                    int b = int.Parse(args[1]);
                    string result = string.Format("{0}x{1}={2}", a, b, a * b);
                    received.Response = result;
                    Console.WriteLine(result);
                    received.Handled = true;
                }
                return received;
            }
        }

        private class cmdHandler3 : ICmdHandler
        {
            public string name => "默认";

            public ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received)
            {
                Console.WriteLine("\t\t ---" + name);
                if (received.Handled) return received;

                Console.WriteLine(name + " REV:" + received.Request);
                received.Response = "server has handled. and do not response";
                received.Handled = true;
                return received;
            }
        }

        private class cmdGetFile : ICmdHandler
        {
            public string name => "浏览文件";

            public ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received)
            {
                Console.WriteLine("\t\t ---" + name);
                if (received.Handled) return received;

                var cmd = received.GetCmd(out var args);
                if (cmd.Equals("fdlg"))
                {
                    //if (args != null)
                    //{
                    var th = new Thread(() =>
                    {
                        OpenFileDialog odlg = new OpenFileDialog();
                        if (args.Length > 0)
                        {
                            odlg.Filter = args[0];
                        }
                        if (args.Length > 1)
                        {
                            odlg.Filter = args[0];
                        }

                    });
                    //}
                }
                return received;
            }
        }

        private static string OpenFile()
        {
            bool isdown = false;
            string filename = string.Empty;
            var th = new Thread(() =>
            {
                filename = getString();
                isdown = true;
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            while (isdown == false)
                Thread.Sleep(100);
            return filename;
        }

        static string getString()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            try
            {
                dlg.ShowDialog();
                return "catch file:" + dlg.FileName;
            }
            catch
            {
                return "Error get folder;";
            }
        }
    }
}
