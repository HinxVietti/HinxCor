using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZetaIpc.Runtime.Client;
using ZetaIpc.Runtime.Server;

namespace HinxCor.InterProcessCommunication
{
    public static class IPCUtil
    {
        public static IIPCServer GetServer()
        {
            return new IPCServer();
        }

        public static IIPCClient GetClient(int port, int timeout)
        {
            return new IPCClient(port, timeout);
        }

        public static string GetCmd(this ReceivedRequestEventArgs e, out string[] args)
        {
            string cmd = e.Request;
            var ss = cmd.Split('|');
            if (ss.Length > 1)
            {
                args = new string[ss.Length - 1];
                for (int i = 0; i < args.Length; i++)
                    args[i] = ss[i + 1];
            }
            else args = null;

            return ss[0];
        }
    }

    internal class IPCServer : IIPCServer
    {
        IpcServer server;
        public int Port
        {
            get
            {
                if (server != null) return server.Port;
                return 0;
            }
        }
        private IExceptionHandler errorHandler;
        private List<ICmdHandler> handles;
        public IPCServer()
        {
            handles = new List<ICmdHandler>();
            server = new IpcServer();
            server.Start();
            server.ReceivedRequest += Server_ReceivedRequest;
        }

        private void Server_ReceivedRequest(object sender, ReceivedRequestEventArgs e)
        {
            for (int i = 0; i < handles.Count; i++)
            {
                var e1 = e;
                try
                {
                    e = handles[i].HandleRequest(e);
                }
                catch (Exception ee)
                {
                    e = e1;
                    errorHandler?.HandleExcep(ee);
                }
            }
            e.Handled = true;
        }


        public bool RegisterCmdHandle(ICmdHandler handler)
        {
            if (ExistCmdHandler(handler))
                return false;
            handles.Add(handler);
            return true;
        }

        public bool UnRegisterCmdHandle(ICmdHandler handler)
        {
            if (!ExistCmdHandler(handler))
                return false;
            handles.Remove(handler);
            return true;
        }

        public bool ExistCmdHandler(ICmdHandler handler)
        {
            try
            {
                return handles.Exists(handle => handle.name.Equals(handler?.name));
            }
            catch { return false; }
        }

        public void SetException(IExceptionHandler handler)
        {
            this.errorHandler = handler;
        }
    }

    internal class IPCClient : IIPCClient
    {
        IpcClient client;

        public IPCClient(int port, int timeout = 0)
        {
            client = new IpcClient();
            client.Initialize(port, timeout);
        }

        public string SendCmd(string cmd)
        {
            return client.Send(cmd);
        }

        public string SendCmd(string cmd,params string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(cmd);
            if (args != null)
                for (int i = 0; i < args.Length; i++)
                {
                    sb.Append('|');
                    sb.Append(args[i]);
                }
            return SendCmd(sb.ToString());
        }
    }
}
