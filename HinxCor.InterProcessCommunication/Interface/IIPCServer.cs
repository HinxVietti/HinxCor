using System;
using System.Collections.Generic;

public interface IIPCServer
{
    void SetException(IExceptionHandler handler);
    bool RegisterCmdHandle(ICmdHandler handler);
    bool UnRegisterCmdHandle(ICmdHandler handler);
    bool ExistCmdHandler(ICmdHandler handler);
}
