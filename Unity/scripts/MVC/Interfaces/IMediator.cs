namespace HinxCor.MVC.Interfaces
{

    public interface IMediator:IObserver,INotifier
    {
        string mediatorName { get; set; }
        string[] listNotificationInterests();

    }
}
