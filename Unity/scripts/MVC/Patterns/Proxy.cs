using HinxCor.MVC.Core;
using HinxCor.MVC.Interfaces;

namespace HinxCor.MVC.Patterns
{
    public class Proxy : IProxy
    {
        public const string NAME = "Proxy";

        public string proxyName { get; set; }

        public void SendNotification(string name, object data = null)
        {
            Facade.instance.SendNotification(name, data);
        }
    }
}
