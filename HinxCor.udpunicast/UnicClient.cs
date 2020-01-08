using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class UnicClient : IDisposable
{
    UdpClient client;
    MessageReceiveHandler OnMessageReceive;
    IPEndPoint receivefrom = null;
    Thread p;
    public UnicClient(int port, MessageReceiveHandler onMessageReceive)
    {
        client = new UdpClient(port);
        OnMessageReceive = onMessageReceive;
        p = new Thread(Run)/* { IsBackground = true }*/;
        p.SetApartmentState(ApartmentState.STA);
        p.Start();
    }

    public void Dispose()
    {
        if (p.IsAlive)
            p?.Abort();
    }

    public void SendMessage(IPEndPoint destinate, string message)
    {
        var bdata = Encoding.UTF8.GetBytes(message);
        client.Send(bdata, bdata.Length, destinate);
    }

    private void Run()
    {
        while (update()) ;
    }

    private bool update()
    {
        if (client == null)
            return true;
        var data = client.Receive(ref receivefrom);
        OnMessageReceive?.Invoke(data, receivefrom);
        return true;
    }
}


public delegate void MessageReceiveHandler(byte[] data, IPEndPoint recieveFrom);