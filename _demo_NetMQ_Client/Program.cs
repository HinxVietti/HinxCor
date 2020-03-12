using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _demo_NetMQ_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = string.Empty;
            using (var client = new RequestSocket("tcp://localhost:5556"))
            {
                Console.WriteLine("Client start.");
                
                client.SendFrame("Hello");

                string m2 = client.ReceiveFrameString();
                Console.WriteLine("From Server: {0}", m2);
            }

            Console.WriteLine();
            Console.WriteLine("Finished..");
            Console.ReadKey();
        }
    }
}
