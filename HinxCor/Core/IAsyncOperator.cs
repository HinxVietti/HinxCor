using System;
using System.Collections.Generic;

namespace HinxCor
{
    /// <summary>
    /// 异步工具
    /// </summary>
    public interface IAsyncOperator
    {
        /// <summary>
        /// 获取当前日志
        /// </summary>
        /// <returns></returns>
        string GetLog();
        /// <summary>
        /// 获取格式日志
        /// </summary>
        /// <returns></returns>
        FormerLog GetHLog();
        /// <summary>
        /// 获取进度
        /// </summary>
        /// <returns></returns>
        float GetProcess();
        /// <summary>
        /// 获取异步进程状态
        /// </summary>
        /// <returns></returns>
        bool GetIsDoneState();
        //AsyncOperate AO { get; }
        //void SetAO(AsyncOperate ao);
        //void OutLog(out string log);
        //void OutHLog(out FormerLog hlog);
        //void OutProcess(out float process);
        //void OutState(out bool state);
    }
}

