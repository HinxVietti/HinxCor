using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace demo_UDP_Console
{
    class Program
    {
        static void Main(string[] args)
        {


            UdpClient client = new UdpClient(12345);//IP v4
                                                    //IPEndPoint host = new IPEndPoint(IPAddress.Any, 12345);
                                                    //client.Client.Bind(host);
                                                    //var rp = client?.Client?.RemoteEndPoint;
                                                    //var lp = client?.Client?.LocalEndPoint;
            Console.WriteLine("Program initialize.");
            Console.WriteLine();
            //DebugEndPointInfo(rp as IPEndPoint, "Remote EndPoint");
            //DebugEndPointInfo(lp as IPEndPoint, "Local  EndPoint");
            IPEndPoint remoteEndpoint = null;
            while (true)
            {
                var revData = client.Receive(ref remoteEndpoint);
                string msg = Encoding.UTF8.GetString(revData);
                DebugReceiveMessga(remoteEndpoint, msg);
            }

        }

        static void DebugReceiveMessga(IPEndPoint ep, string msg)
        {
            try
            {
                Console.WriteLine(ep.ToString() + ":");
                Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
                Console.WriteLine(e);
            }

            Console.WriteLine();
        }

        static void DebugEndPointInfo(IPEndPoint ep, string eptag)
        {
            if (ep == null) Console.WriteLine(eptag + " is Empty EndPoint.");
            else
            {
                Console.WriteLine(eptag + " ： " + ep.ToString());
            }
        }
    }
}

namespace client
{
    public class UdpClientManager
    {
        //接收数据事件
        public Action<string> recvMessageEvent = null;
        //发送结果事件
        public Action<int> sendResultEvent = null;
        //本地监听端口
        public int localPort = 0;

        private UdpClient udpClient = null;

        public UdpClientManager(int localPort)
        {
            if (localPort < 0 || localPort > 65535)
                throw new ArgumentOutOfRangeException("localPort is out of range");

            this.localPort = localPort;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    udpClient = new UdpClient(localPort, AddressFamily.InterNetwork);//指定本地监听port
                    ReceiveMessage();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                    Thread.Sleep(100);
                }
            }
        }

        private async void ReceiveMessage()
        {
            while (true)
            {
                if (udpClient == null)
                    return;

                try
                {
                    UdpReceiveResult udpReceiveResult = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                    if (recvMessageEvent != null)
                        recvMessageEvent(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }
            }
        }

        //单播
        public async void SendMessageByUnicast(string message, string destHost, int destPort)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message cant not null");
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");
            if (string.IsNullOrEmpty(destHost))
                throw new ArgumentNullException("destHost cant not null");
            if (destPort < 0 || destPort > 65535)
                throw new ArgumentOutOfRangeException("destPort is out of range");

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            int len = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    len = await udpClient.SendAsync(buffer, buffer.Length, new IPEndPoint(IPAddress.Parse(destHost), destPort));
                }
                catch (Exception)
                {
                    len = 0;
                }

                if (len <= 0)
                    Thread.Sleep(100);
                else
                    break;
            }

            if (sendResultEvent != null)
                sendResultEvent(len);
        }

        public void CloseUdpCliend()
        {
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            try
            {
                udpClient.Client.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {
            }
            udpClient.Close();
            udpClient = null;
        }
    }
}

namespace server
{
    public class UdpServiceManager
    {
        private readonly string broadCastHost = "255.255.255.255";
        //接收数据事件
        public Action<string> recvMessageEvent = null;
        //发送结果事件
        public Action<int> sendResultEvent = null;
        //本地host
        private string localHost = "";
        //本地port
        private int localPort = 0;

        private UdpClient udpClient = null;

        public UdpServiceManager(string localHost, int localPort)
        {
            if (string.IsNullOrEmpty(localHost))
                throw new ArgumentNullException("localHost cant not null");
            if (localPort < 0 || localPort > 65535)
                throw new ArgumentOutOfRangeException("localPort is out of range");

            this.localHost = localHost;
            this.localPort = localPort;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(localHost), localPort));//绑定本地host和port
                    ReceiveMessage();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }
        }

        private async void ReceiveMessage()
        {
            while (true)
            {
                if (udpClient == null)
                    return;

                try
                {
                    UdpReceiveResult udpReceiveResult = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                    if (recvMessageEvent != null)
                        recvMessageEvent(message);
                }
                catch (Exception)
                {
                }
            }
        }

        //单播
        public async void SendMessageByUnicast(string message, string destHost, int destPort)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message cant not null");
            if (string.IsNullOrEmpty(destHost))
                throw new ArgumentNullException("destHost cant not null");
            if (destPort < 0 || destPort > 65535)
                throw new ArgumentOutOfRangeException("destPort is out of range");
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            int len = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    len = await udpClient.SendAsync(buffer, buffer.Length, destHost, destPort);
                }
                catch (Exception)
                {
                    len = 0;
                }

                if (len <= 0)
                    Thread.Sleep(100);
                else
                    break;
            }

            if (sendResultEvent != null)
                sendResultEvent(len);
        }

        //广播
        public async void SendMessageByBroadcast(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message cant not null");
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            int len = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    len = await udpClient.SendAsync(buffer, buffer.Length, broadCastHost, localPort);
                }
                catch (Exception ex)
                {
                    len = 0;
                }

                if (len <= 0)
                    Thread.Sleep(100);
                else
                    break;
            }

            if (sendResultEvent != null)
                sendResultEvent(len);
        }

        public void CloseUdpCliend()
        {
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            try
            {
                udpClient.Client.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {
            }
            udpClient.Close();
            udpClient = null;
        }
    }
}

namespace multicast
{
    public class UdpClientManager
    {
        //接收数据事件
        public Action<string> recvMessageEvent = null;
        //发送结果事件
        public Action<int> sendResultEvent = null;
        //本地监听端口
        public int localPort = 0;
        //组播地址
        public string MultiCastHost = "";

        private UdpClient udpClient = null;

        public UdpClientManager(int localPort, string MultiCastHost)
        {
            if (localPort < 0 || localPort > 65535)
                throw new ArgumentOutOfRangeException("localPort is out of range");
            if (string.IsNullOrEmpty(MultiCastHost))
                throw new ArgumentNullException("message cant not null");

            this.localPort = localPort;
            this.MultiCastHost = MultiCastHost;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    udpClient = new UdpClient(localPort, AddressFamily.InterNetwork);//指定本地监听port
                    udpClient.JoinMulticastGroup(IPAddress.Parse(MultiCastHost));
                    ReceiveMessage();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }
        }

        private async void ReceiveMessage()
        {
            while (true)
            {
                if (udpClient == null)
                    return;

                try
                {
                    UdpReceiveResult udpReceiveResult = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                    if (recvMessageEvent != null)
                        recvMessageEvent(message);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async void SendMessageByMulticast(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message cant not null");
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            int len = 0;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    len = await udpClient.SendAsync(buffer, buffer.Length, new IPEndPoint(IPAddress.Parse(MultiCastHost), localPort));
                }
                catch (Exception)
                {
                    len = 0;
                }

                if (len <= 0)
                    Thread.Sleep(100);
                else
                    break;
            }

            if (sendResultEvent != null)
                sendResultEvent(len);
        }

        public void CloseUdpCliend()
        {
            if (udpClient == null)
                throw new ArgumentNullException("udpClient cant not null");

            try
            {
                udpClient.Client.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {
            }
            udpClient.Close();
            udpClient = null;
        }
    }
}
