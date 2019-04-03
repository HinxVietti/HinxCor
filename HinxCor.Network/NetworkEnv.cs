using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace HinxCor.Network
{

    /// <summary>
    /// 网络环境
    /// </summary>
    public class NetworkEnv
    {
        private static readonly IPEndPoint DefaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, port: 0);
        /// <summary>
        /// 全局IP信息
        /// </summary>
        public static IPGlobalProperties IPGlobalProperties { get { return IPGlobalProperties.GetIPGlobalProperties(); } }
        /// <summary>
        /// 激活的Tcp连接信息
        /// </summary>
        public static TcpConnectionInformation[] ActiveTcpConnections { get { return IPGlobalProperties.GetActiveTcpConnections(); } }
        /// <summary>
        /// 全局使用了的UDP终端信息
        /// </summary>
        public static IPEndPoint[] GlobalActiveUdpListeners { get { return IPGlobalProperties.GetActiveUdpListeners(); } }

        /// <summary>
        /// 检测端口是否被占用了(校测激活的TCP端口和已经监听了的UDP端口)
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPortUsed(int port)
        {
            var atcs = ActiveTcpConnections;
            foreach (var ccs in atcs)
                if (ccs.LocalEndPoint.Port == port) return true;

            var udpuse = GlobalActiveUdpListeners;
            foreach (var ep in udpuse)
                if (ep.Port == port) return true;

            return false;
        }


        /// <summary>
        /// 获取一个可用的端口
        /// </summary>
        /// <returns></returns>
        public static int GetAvailableTcpPort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(DefaultLoopbackEndpoint);
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
        }

        /// <summary>
        /// 获取一个可用的端口
        /// </summary>
        /// <returns></returns>
        public static int GetAvailableUdpPort()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Bind(DefaultLoopbackEndpoint);
                return ((IPEndPoint)socket.LocalEndPoint).Port;
            }
        }

    }
}

