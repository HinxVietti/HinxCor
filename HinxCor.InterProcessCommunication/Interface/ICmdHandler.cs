using System;
using System.Collections.Generic;
using ZetaIpc.Runtime.Server;

public interface ICmdHandler
{
    /// <summary>
    /// 控制器的名字
    /// </summary>
    string name { get; }

    /// <summary>
    /// 处理request
    /// </summary>
    /// <param name="received">收到的内容</param>
    /// <returns>返回的结果</returns>
    ReceivedRequestEventArgs HandleRequest(ReceivedRequestEventArgs received);
}
