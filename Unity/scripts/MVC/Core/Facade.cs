using HinxCor.MVC.Interfaces;
using System;

namespace HinxCor.MVC.Core
{
    public class Facade : IFacade
    {
        #region 单例外观模式

        protected static Facade _instance;
        public static Facade instance
        {
            get
            {
                if (_instance == null) _instance = new Facade();
                return _instance;
            }
        }
        /// <summary>
        /// 如果别人构造了该外观接口，则抛出异常
        /// </summary>
        public Facade()
        {
            if (_instance != null)
                throw new Exception();
            _instance = this;
        }

        #endregion

        #region View 管理 Mediator的三个功能

        public void RegisterMediator(IMediator mediator)
        {
            View.I().RegisterMediator(mediator);
        }

        public IMediator RemoveMediator(string mediatorName)
        {
            return View.I().RemoveMediator(mediatorName);
        }


        public IMediator RetrieveMediator(string mediatorName)
        {
            return View.I().RetrieveMediator(mediatorName);
        }
        #endregion

        #region Model 管理 Proxy的三个功能
        public void RegisterProxy(IProxy proxy)
        {
            Model.instance.RegisterProxy(proxy);
        }


        public IProxy RemoveProxy(string proxyName)
        {
            return Model.instance.RemoveProxy(proxyName);
        }

        public IProxy RetrieveProxy(string proxyName)
        {
            return Model.instance.RetrieveProxy(proxyName);
        }
        #endregion

        /// <summary>
        /// 发送信息功能
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SendNotification(string name, object data = null)
        {
            NotificationCenter.instance.SendNotification(name, data);
        }
    }
}
