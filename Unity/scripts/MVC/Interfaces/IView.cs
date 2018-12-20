namespace HinxCor.MVC.Interfaces
{
    /// <summary>
    /// manager imediator
    /// 传递者，中介物
    /// </summary>
    public interface IView
    {
        //注册IMediator
        void RegisterMediator(IMediator mediator);
        //获取IMediator
        IMediator RetrieveMediator(string mediatorName);
        //移除(取出)IMediator
        IMediator RemoveMediator(string mediatorName);
    }
}
