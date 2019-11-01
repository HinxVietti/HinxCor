using System;
using System.Collections.Generic;

/// <summary>
/// byte array decode
/// </summary>
public interface IBytesDecode
{
    /// <summary>
    /// decode
    /// </summary>
    /// <param name="plain"></param>
    /// <param name="codePwd"></param>
    /// <returns></returns>
    byte[] Decode(byte[] plain, ICodePwdProvider codePwd);
}

