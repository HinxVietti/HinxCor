using HinxCor;
using System;
using System.Collections.Generic;

/// <summary>
/// 异步工具
/// </summary>
public class AsyncOperator : IAsyncOperator
{
    /// <summary>
    /// 是否完成
    /// </summary>
    public bool isdone { get; internal set; }
    /// <summary>
    /// 日志
    /// </summary>
    public string log { get; internal set; }
    /// <summary>
    /// 进度
    /// </summary>
    public float process { get; internal set; }
    /// <summary>
    /// 格式日志
    /// </summary>
    public FormerLog hlog { get; internal set; }

    /// <summary>
    /// 构造
    /// </summary>
    public AsyncOperator()
    {
        isdone = false;
        log = "start";
        process = 0;
        hlog = new FormerLog()
        {
            Type = FormerLog.LogType.Log
        };
        hlog.AppendLine(log);
    }
    /// <summary>
    /// 获取Log
    /// </summary>
    /// <returns></returns>
    public FormerLog GetHLog()
    {
        return hlog;
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <returns></returns>
    public bool GetIsDoneState()
    {
        return isdone;
    }
    /// <summary>
    /// 获取日志
    /// </summary>
    /// <returns></returns>
    public string GetLog()
    {
        return log;
    }
    /// <summary>
    /// 获取进度
    /// </summary>
    /// <returns></returns>
    public float GetProcess()
    {
        return process;
    }
}

