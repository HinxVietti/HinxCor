using System;
using System.Collections.Generic;

/// <summary>
/// byte array encode
/// </summary>
public interface IBytesEncode
{
    /// <summary>
    /// encode
    /// </summary>
    /// <param name="plain"></param>
    /// <returns></returns>
    byte[] Encode(byte[] plain, ICodePwdProvider codePwd);
}

