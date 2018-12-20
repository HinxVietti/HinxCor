using HinxCor.MVC.Interfaces;
using System;
using System.Collections.Generic;

namespace HinxCor.MVC.Core
{
    public class Model : IModel
    {
        #region Singleton

        private static Model _instance;
        public static Model instance
        {
            get
            {
                if (_instance == null) _instance = new Model();
                return _instance;
            }
        }

        public Model()
        {
            if (_instance != null)
                throw new Exception();
            _instance = this;
            proxyMap = new Dictionary<string, IProxy>();
        }

        [Obsolete("请使用instance代替")]
        public static Model I()
        {
            return instance;
        }

        #endregion

        private Dictionary<string, IProxy> proxyMap;


        public void RegisterProxy(IProxy proxy)
        {
            proxyMap[proxy.proxyName] = proxy;
        }

        public IProxy RemoveProxy(string proxyName)
        {
            var result = proxyMap.ContainsKey(proxyName) ? proxyMap[proxyName] : null;
            if (result != null) proxyMap[proxyName] = null;
            return result;
        }

        public IProxy RetrieveProxy(string proxyName)
        {
            return proxyMap.ContainsKey(proxyName) ? proxyMap[proxyName] : null;
        }
    }
}
