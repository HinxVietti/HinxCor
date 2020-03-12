using System;
using System.Collections.Generic;

/// <summary>
/// 建议是用ICPUtil获取示例。
/// </summary>
public interface IIPCClient
{
    string SendCmd(string cmd);
    string SendCmd(string cmd, params string[] args);
}
