namespace Crypto.Client
{

    /// <summary>
    /// DES加密接口
    /// </summary>
    public interface IDesCryptoUtil
    {
        /// <summary>
        /// 加密明文
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] plainBytes);

        /// <summary>
        /// 解密密文
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptedBytes);
    }
}