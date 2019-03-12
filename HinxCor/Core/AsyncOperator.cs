using HinxCor;
using System;
using System.Collections.Generic;

public class AsyncOperator : IAsyncOperator
{
    public bool isdone { get; internal set; }
    public string log { get; internal set; }
    public float process { get; internal set; }
    public FormerLog hlog { get; internal set; }

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

    public FormerLog GetHLog()
    {
        return hlog;
    }

    public bool GetIsDoneState()
    {
        return isdone;
    }

    public string GetLog()
    {
        return log;
    }

    public float GetProcess()
    {
        return process;
    }
}

