namespace HinxCor.InterProcessCommunication.DataStruct
{
    public class ReceivedRequestEventArgs
    {
        private ZetaIpc.Runtime.Server.ReceivedRequestEventArgs instance;

        public ReceivedRequestEventArgs(string request)
        {
            this.instance = new ZetaIpc.Runtime.Server.ReceivedRequestEventArgs(request);
        }

        public ReceivedRequestEventArgs(ZetaIpc.Runtime.Server.ReceivedRequestEventArgs request)
        {
            this.instance = request;
        }

        public ZetaIpc.Runtime.Server.ReceivedRequestEventArgs GetRequest() => instance;

        public string Request { get => instance?.Request; }
        public string Response { get => instance?.Response; set => instance.Response = value; }
        public bool Handled { get => instance.Handled; set => instance.Handled = value; }
    }
}
