using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _demo_NetMQ_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new ResponseSocket("tcp://localhost:5556"))
            {
                Console.WriteLine("Server start.");
                string m1 = server.ReceiveFrameString();
                Console.WriteLine("From Client: {0}", m1);

                server.SendFrame("Hi Back");
            }

            Console.WriteLine();
            Console.WriteLine("Finished..");
            Console.ReadKey();
        }
    }
}
