using HinxCor.Security;
using System;
using System.Collections.Generic;

/// <summary>
/// 加密解密内容
/// </summary>
public class NanaCode
{
    private const string pwd = "nanacode_0x5af20c7f7a";
    private static RC4 rc4 = new RC4(System.Text.Encoding.ASCII, pwd);

    /// <summary>
    /// 加密内容
    /// </summary>
    /// <param name="source"></param>
    /// <param name="encoder"></param>
    /// <param name="codePwd"></param>
    /// <returns></returns>
    public static byte[] PushCode(byte[] source, IBytesEncode encoder, ICodePwdProvider codePwd)
    {
        var encoded = encoder.Encode(source, codePwd);
        string en_b64 = Convert.ToBase64String(encoded);
        return rc4.Encrypt(en_b64);
    }

    /// <summary>
    /// 提取内容
    /// </summary>
    /// <param name="encoded"></param>
    /// <param name="decoder"></param>
    /// <param name="codePwd"></param>
    /// <returns></returns>
    public static byte[] RequestCode(byte[] encoded, IBytesDecode decoder, ICodePwdProvider codePwd)
    {
        var decoded_ = rc4.Decrypt(encoded);
        var b_array = Convert.FromBase64String(decoded_);
        return decoder.Decode(b_array, codePwd);
    }

}

