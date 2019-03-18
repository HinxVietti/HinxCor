using System.Security.Cryptography;

namespace Crypto.Client.Impl
{
    /// <summary>
    /// SHA1单项加密
    /// </summary>
    public class Sha1CryptoUtil : ISha1CryptoUtil
    {
        /// <summary>
        /// SHA1单向加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainBytes)
        {
            using (var sha1 = SHA1.Create())
            {
                var encryptedBytes = sha1.ComputeHash(plainBytes);
                return encryptedBytes;
            }
        }
    }
}