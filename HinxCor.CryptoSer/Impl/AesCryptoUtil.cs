using System.Security.Cryptography;
using System.Text;

namespace HinxCor.CryptoSer
{
    /// <summary>
    /// AES加密
    /// </summary>
    public class AesCryptoUtil : IAesCryptoUtil
    {
        /// <summary>
        /// 系统秘钥,长度应该为16,若非请用方法补充到16位或者到网站随机生成;
        /// You can use tool to generate the string on https://www.random.org/strings/ or other website.
        /// </summary>
        private const string SystemKeyPart = "84ImUeBn432oPkqo";

        /// <summary>
        /// 用户自己的秘钥,长度为4-16
        /// </summary>
        private const string UserKeyPart = "AecCrypto";

        /// <summary>
        /// 组合秘钥,(用户自己的秘钥会自己补充到16位)
        /// </summary>
        private static readonly byte[] Key = Encoding.ASCII.GetBytes(UserKeyPart.PadRight(16, '#') + SystemKeyPart);

        /// <summary>
        /// The Initialization Vector;
        /// 长度必须为16;
        /// </summary>
        private static readonly byte[] Iv = Encoding.ASCII.GetBytes("bCNtStALc7bRqREq");

        /// <summary>
        /// 加密明文
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainBytes)
        {
            return Encrypt(plainBytes, CipherMode.CBC, PaddingMode.PKCS7);
        }

        /// <summary>
        /// 解密密文
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] encryptedBytes)
        {
            return Decrypt(encryptedBytes, CipherMode.CBC, PaddingMode.PKCS7);
        }

        private static byte[] Encrypt(byte[] plainBytes, CipherMode cipher, PaddingMode padding)
        {
            using (var aes = Rijndael.Create())
            {
                aes.Mode = cipher;
                aes.Padding = padding;

                using (var transform = aes.CreateEncryptor(Key, Iv))
                {
                    var encryptedBytes = transform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return encryptedBytes;
                }
            }
        }

        private static byte[] Decrypt(byte[] encryptedBytes, CipherMode cipher, PaddingMode padding)
        {
            using (var aes = Rijndael.Create())
            {
                aes.Mode = cipher;
                aes.Padding = padding;

                using (var transform = aes.CreateDecryptor(Key, Iv))
                {
                    var plainBytes = transform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return plainBytes;
                }
            }
        }
    }
}