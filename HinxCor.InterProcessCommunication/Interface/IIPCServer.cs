using System;
using System.Collections.Generic;

/// <summary>
/// 建议是用ICPUtil获取示例。
/// </summary>
public interface IIPCServer
{
    int Port { get; }
    void SetException(IExceptionHandler handler);
    bool RegisterCmdHandle(ICmdHandler handler);
    bool UnRegisterCmdHandle(ICmdHandler handler);
    bool ExistCmdHandler(ICmdHandler handler);
}
