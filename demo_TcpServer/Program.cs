using System;
using System.Threading;
using System.Threading.Tasks;
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

            var s = new IpcServer();
            s.Start(12345); // Passing no port selects a free port automatically.

            Console.WriteLine("Started server on port {0}.", s.Port);
            ReceivedRequestEventArgs arg = null;
            s.ReceivedRequest += (sender, args) =>
            {
                Console.WriteLine("Request:" + args.Request);

                args.Response = "openfile";
                args.Response = OpenFile();
                arg = args;
                arg.Handled = true;
            };
            Console.ReadKey();
            Console.WriteLine("End");
            s.Stop();
            Console.ReadKey();
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
