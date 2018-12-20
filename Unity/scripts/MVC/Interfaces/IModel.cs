namespace HinxCor.MVC.Interfaces
{
    public interface IModel
    {
        void RegisterProxy(IProxy proxy);

        IProxy RetrieveProxy(string proxyName);

        IProxy RemoveProxy(string proxyName);
    }
}
