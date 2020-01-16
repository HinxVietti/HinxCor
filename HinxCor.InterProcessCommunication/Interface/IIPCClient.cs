using System;
using System.Collections.Generic;

public interface IIPCClient
{
    string SendCmd(string cmd);
    string SendCmd(string cmd, params string[] args);
}
