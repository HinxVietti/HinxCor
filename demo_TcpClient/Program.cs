using HinxCor.InterProcessCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZetaIpc.Runtime.Client;

namespace demo_TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string fname = "port";
            int port = 0;
            if (File.Exists(fname))
            {
                string txt = File.ReadAllText(fname);
                int.TryParse(txt, out port);
            }
            Console.Write("请输入服务端口" + (port == 0 ? "" : string.Format("({0})", port)) + ":");
            string portString = Console.ReadLine();
            if (int.TryParse(portString, out var newport))
            {
                if (newport != 0)
                {
                    File.WriteAllText(fname, portString);
                    port = newport;
                }
            }
            else
            { }
            Console.WriteLine("Client start with " + port);
            Console.WriteLine();
            var client = IPCUtil.GetClient(port, 6000);//6S 超时
            string result = client.SendCmd("x", "666", "520");
            Console.WriteLine(result);

            Console.WriteLine("any key esc");
            Console.ReadKey();
        }
    }
}
