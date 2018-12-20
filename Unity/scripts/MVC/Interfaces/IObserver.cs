using HinxCor.MVC.Patterns;

namespace HinxCor.MVC.Interfaces
{
    /// <summary>
    /// 信息处理接口（信息的观察者）
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 信息处理;
        /// 有且仅能收到自己订阅的信息
        /// </summary>
        /// <param name="notification"></param>
        void HandleNotification(Notification notification);
    }
}
