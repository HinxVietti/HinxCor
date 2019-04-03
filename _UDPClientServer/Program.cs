using HinxCor.Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _UDPClientServer
{
    class Program
    {
        static UdpClient client;

        static void Main(string[] args)
        {
            int port = NetworkEnv.GetAvailableUdpPort();

            client = new UdpClient(port);
            //client = new UdpClient();

            IPEndPoint from = new IPEndPoint(0, 0);
            byte[] buffer = new byte[1];
            string message;

            Console.WriteLine("start:" + port);
            Console.WriteLine();

            do
            {
                Console.WriteLine("wait..");
                buffer = client.Receive(ref from);
                message = GetMessage(buffer);
                printIPE(from);
                Console.WriteLine("\t" + message);
                Console.WriteLine();
            }
            while (!string.IsNullOrEmpty(message) && !message.Equals("exit"));
            Console.WriteLine();
            Console.WriteLine("App quit cause exit recived. any key to esc");
            Console.ReadKey();
        }

        private static void printIPE(IPEndPoint ip)
        {
            Console.WriteLine(string.Format("{2}, 来自:{0},#{1}:", ip.Address, ip.Port, DateTime.Now));
        }
        private static void printIPE(string title, IPEndPoint ip)
        {
            if (ip == null) Console.WriteLine(title + ":null");
            else
                Console.WriteLine(string.Format("{2}:{0},#{1}:", ip.Address, ip.Port, title));
        }

        private static string GetMessage(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
