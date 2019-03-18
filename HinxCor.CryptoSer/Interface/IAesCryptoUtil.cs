namespace Crypto.Client
{
    /// <summary>
    /// AES工具包
    /// </summary>
    public interface IAesCryptoUtil
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