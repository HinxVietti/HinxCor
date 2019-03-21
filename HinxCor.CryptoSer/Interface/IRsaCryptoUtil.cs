namespace HinxCor.CryptoSer
{
    /// <summary>
    /// RSA加密; 
    /// 验证方式, 使用私钥加明文生成签名, 然后使用签名和密文加公钥验证
    /// </summary>
    public interface IRsaCryptoUtil
    {
        /// <summary>
        /// 生成密匙
        /// </summary>
        /// <returns></returns>
        RsaKey GenerateKeys();

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        byte[] Sign(byte[] bytes, string privateKey);

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="signature"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        bool Verify(byte[] bytes, byte[] signature, string publicKey);

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] plainBytes, string publicKey);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] encryptedBytes, string privateKey);
    }
}