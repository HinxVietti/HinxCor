namespace Crypto.Client
{
    /// <summary>
    /// 加密为MD5 不可逆加密,数据校验
    /// </summary>
    public interface IMd5CryptoUtil
    {
        /// <summary>
        /// ByteArray
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] plainBytes);
    }
}