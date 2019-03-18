using System.Security.Cryptography;

namespace Crypto.Client.Impl
{
    /// <summary>
    /// MD5单项加密
    /// </summary>
    public class Md5CryptoUtil : IMd5CryptoUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainBytes)
        {
            using (var md5 = MD5.Create())
            {
                var encryptedBytes = md5.ComputeHash(plainBytes);
                return encryptedBytes;
            }
        }
    }
}