using HinxCor;

/// <summary>
/// 异步工具
/// </summary>
public class AsyncOperator : IAsyncOperator
{
    /// <summary>
    /// 是否完成
    /// </summary>
    public bool isdone { get; set; }
    /// <summary>
    /// 日志
    /// </summary>
    public string log { get; set; }
    /// <summary>
    /// 进度
    /// </summary>
    public float process { get; set; }
    /// <summary>
    /// 格式日志
    /// </summary>
    public FormerLog hlog { get; set; }

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

    public void SetClip(LogClip p)
    {
        this.log = p.Log;
        this.process = p.Process;
        this.isdone = p.State;
        this.hlog = new FormerLog((FormerLog.LogType)p.LogType, p.Log);

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

/// <summary>
/// 日志片段
/// </summary>
public struct LogClip
{
    /// <summary>
    /// 进度
    /// </summary>
    public float Process { get; set; }
    /// <summary>
    /// 日志
    /// </summary>
    public string Log { get; set; }
    /// <summary>
    /// 日志类型 0-normal , 1-warning , 2-error , 3-exception
    /// </summary>
    public int LogType { get; set; }
    /// <summary>
    /// 是否完成
    /// </summary>
    public bool State { get; set; }

    /// <summary>
    /// t [0,4]
    /// </summary>
    /// <param name="p"></param>
    /// <param name="l"></param>
    /// <param name="t"></param>
    /// <param name="s"></param>
    public LogClip(float p, string l, int t, bool s)
    {
        Process = p;
        Log = l;
        LogType = t;
        State = s;
    }

    public static LogClip Create(float process, string log, int logt = 1)
    {
        return new LogClip(process, log, logt, false);
    }

    public static LogClip Finished(string log = "finished", int logt = 1, float process = 1)
    {
        return new LogClip(process, log, logt, true);
    }

}