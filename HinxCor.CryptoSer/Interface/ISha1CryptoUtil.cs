namespace HinxCor.CryptoSer
{
    /// <summary>
    /// SHA1加密
    /// </summary>
    public interface ISha1CryptoUtil
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] plainBytes);
    }
}