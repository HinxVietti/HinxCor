using System;
using System.Collections.Generic;

/// <summary>
/// 提供密码
/// </summary>
public interface ICodePwdProvider
{
    /// <summary>
    /// 获取密码
    /// </summary>
    /// <returns></returns>
    string GetStringPwd();
    /// <summary>
    /// 获取密码
    /// </summary>
    /// <returns></returns>
    byte[] GetBytesPwd();
}

