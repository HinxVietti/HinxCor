using System;
using System.Collections.Generic;

public interface IIPCServer
{
    int Port { get; }
    void SetException(IExceptionHandler handler);
    bool RegisterCmdHandle(ICmdHandler handler);
    bool UnRegisterCmdHandle(ICmdHandler handler);
    bool ExistCmdHandler(ICmdHandler handler);
}
