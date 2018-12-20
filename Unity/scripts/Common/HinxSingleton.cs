using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace HinxCor.Common
{
    /// <summary>
    /// 请在构造的时候对_instance赋值，否则 访问单利将重新创建一个实例;
    /// 可以重写Init 代替(无参)构造方法；
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HinxSingleton<T> where T : HinxSingleton<T>, new()
    {
        protected static T _instance;
        public static T singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();

                }
                return _instance;
            }
        }
        public HinxSingleton()
        {
            Init();
        }
        protected virtual void Init() { }
    }
}
