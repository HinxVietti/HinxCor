using HinxCor.MVC.Interfaces;
using HinxCor.MVC.Patterns;
using System;
using System.Collections.Generic;

namespace HinxCor.MVC.Core
{
    public class NotificationCenter
    {
        #region 单例模式
        private static NotificationCenter _instance;
        public static NotificationCenter instance
        {
            get
            {
                if (_instance == null) _instance = new NotificationCenter();
                return _instance;
            }
        }

        public static NotificationCenter I()
        {
            return instance;
        }

        public NotificationCenter()
        {
            if (_instance != null)
                throw new Exception();
            _instance = this;
            observerMap = new Dictionary<string, List<IObserver>>();
        }
        #endregion

        private Dictionary<string, List<IObserver>> observerMap;

        public void AddObserver(string name,IObserver observer)
        {
            if (!observerMap.ContainsKey(name))
                observerMap[name] = new List<IObserver>();
            observerMap[name].Add(observer);
        }

        public void RemoveObserver(string name,IObserver observer)
        {
            if (!observerMap.ContainsKey(name)) return;
            if (!observerMap[name].Contains(observer)) return;
            observerMap[name].Remove(observer);
        }

        public void SendNotification(string name,object data = null)
        { 
            // 如果没有监听这个消息,就直接返回
            if (!observerMap.ContainsKey(name)) return;
            // 找到List
            List<IObserver> list = observerMap[name];
            // 遍历List
            foreach (IObserver observer in list)
                observer.HandleNotification(new Notification(name, data));
            // observer.HandleNotification()

        }


        /// <summary>
        /// 查看所有消息监听对象
        /// </summary>
        public void View()
        {
            string s = "----------------------Start View Observers----------------------\n";
            foreach (string name in observerMap.Keys)
            {
                s += name + "  : [ ";
                List<IObserver> list = observerMap[name];
                for (int i = 0; i < list.Count; i++)
                {
                    s += list[i];
                    if (i != list.Count - 1) s += ",";
                }

                s += " ]\n";
            }

            s += "----------------------End View Observers----------------------";
            UnityEngine.Debug.Log(s);
        }
    }
}
