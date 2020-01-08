using System;
using System.Collections.Generic;
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
            var c = new IpcClient();
            c.Initialize(12345);

            Console.WriteLine("Started client.");

            var rep = c.Send("Hello");
            Console.WriteLine("Received: " + rep);
            Console.WriteLine("client end..");
            Console.ReadKey();
        }
    }
}
