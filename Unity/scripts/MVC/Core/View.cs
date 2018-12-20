using HinxCor.MVC.Interfaces;
using System;
using System.Collections.Generic;

namespace HinxCor.MVC.Core
{
    public class View : IView
    {
        #region 单例模式
        private static View _instance;
        public static View instance
        {
            get
            {
                if (_instance == null) _instance = new View();
                return _instance;
            }
        }

        public static View I()
        {
            return instance;
        }
        public View()
        {
            if (_instance != null)
                throw new Exception();
            _instance = this;
            mediatorMap = new Dictionary<string, IMediator>();
        }

        #endregion

        private Dictionary<string, IMediator> mediatorMap;

        public void RegisterMediator(IMediator mediator)
        {
            //Debug.Log("RegisterMediator"+mediator);
            mediatorMap[mediator.mediatorName] = mediator;

            string[] notifications = mediator.listNotificationInterests();

            if (notifications.Length > 0)
            {
                for(int i = 0; i < notifications.Length; i++)
                {
                    NotificationCenter.I().AddObserver(notifications[i], mediator);
                }
            }
        }

        public IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = mediatorMap.ContainsKey(mediatorName) ? mediatorMap[mediatorName] : null;
            if (mediator != null)
            {
                mediatorMap[mediatorName] = null;
                // 移除监听事件
                string[] notifications = mediator.listNotificationInterests();
                // 遍历,移除Observer
                if (notifications.Length > 0) /// 有监听的消息
                {
                    for (int i = 0; i < notifications.Length; i++)
                    {
                        NotificationCenter.I().RemoveObserver(notifications[i],mediator);
                    }
                }
            }
            return mediator;
        }

        public IMediator RetrieveMediator(string mediatorName)
        {
            IMediator mediator = mediatorMap.ContainsKey(mediatorName) ? mediatorMap[mediatorName] : null;
            return mediator;
        }
    }
}
