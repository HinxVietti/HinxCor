using HinxCor.Security;
using System.Text;

/// <summary>
/// 
/// </summary>
public class NanaCodeUtil : ICodePwdProvider, IBytesEncode, IBytesDecode
{
    RC4 rC4;
    private static readonly byte[] Iv = Encoding.ASCII.GetBytes("6sT5e8DF");
    private string passwd;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pwd"></param>
    public NanaCodeUtil(string pwd)
    {
        this.passwd = pwd;
        rC4 = new RC4(Encoding.UTF8, pwd);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plain"></param>
    /// <param name="codePwd"></param>
    /// <returns></returns>
    public byte[] Decode(byte[] plain, ICodePwdProvider codePwd)
    {
        return rC4.encrypt(plain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="plain"></param>
    /// <param name="codePwd"></param>
    /// <returns></returns>
    public byte[] Encode(byte[] plain, ICodePwdProvider codePwd)
    {
        return rC4.encrypt(plain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public byte[] GetBytesPwd()
    {
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string GetStringPwd()
    {
        return string.Empty;
    }
}

