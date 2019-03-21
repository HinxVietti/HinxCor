using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace HinxCor.Network
{

    /// <summary>
    /// 网络环境
    /// </summary>
    public class NetworkEnv
    {
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
    }
}

