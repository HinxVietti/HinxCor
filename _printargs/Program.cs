using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _printargs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("无ARG");
                Console.ReadKey();
                return;
            }
            var arg = new HinxCor.Serialize.Arguments(args);
            Console.WriteLine(arg);
            Console.ReadKey();
            try
            {
                if (arg.HasKey("cmdport"))
                {
                    int port = int.Parse(arg.GetArgument("cmdport"));
                    var client = new UdpClient();
                    var startapp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                    string message = "exit";
                    var dgram = Encoding.UTF8.GetBytes(message);
                    client.Send(dgram, dgram.Length, startapp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }
    }
}
