using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _UDPTextSender
{
    class Program
    {
        static UdpClient client;
        static void Main(string[] args)
        {
            client = new UdpClient();
        input:
            Console.WriteLine("请输入目标端口：");
            string port = Console.ReadLine();
            if (!int.TryParse(port, out int _port))
                goto input;
            string input = string.Empty;
            IPEndPoint to = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port);
            do
            {
                input = Console.ReadLine();
                var dgram = getBuffer(input);
                var send = client.Send(dgram, dgram.Length, to);
                Console.WriteLine("send:" + send);
                Console.WriteLine();
            } while (!string.IsNullOrEmpty(input) && !input.Equals("exit"));

            Console.WriteLine("Sender End cause exit key in. any key to esc.");
            Console.ReadKey();
        }

        private static byte[] getBuffer(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
