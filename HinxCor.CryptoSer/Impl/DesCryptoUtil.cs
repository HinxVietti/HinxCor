using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Client.Impl
{
    /// <summary>
    /// DES加密
    /// </summary>
    public class DesCryptoUtil : IDesCryptoUtil
    {
        /// <summary>
        /// 秘钥长度8位;
        /// https://www.random.org/strings/
        /// </summary>
        private static readonly byte[] Key = Encoding.ASCII.GetBytes("0e3Nl9Z9");

        /// <summary>
        /// IV长度8位;
        /// https://www.random.org/strings/
        /// </summary>
        private static readonly byte[] Iv = Encoding.ASCII.GetBytes("62EcX79F");

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainBytes)
        {
            using (var provider = new DESCryptoServiceProvider())
            {
                provider.Key = Key;
                provider.IV = Iv;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] encryptedBytes)
        {
            using (var provider = new DESCryptoServiceProvider())
            {
                provider.Key = Key;
                provider.IV = Iv;
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    return memoryStream.ToArray();
                }
            }
        }
    }
}