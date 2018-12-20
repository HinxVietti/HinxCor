namespace HinxCor.MVC.Interfaces
{
    public interface INotifier
    {
        void SendNotification(string name, object data);
    }
}
