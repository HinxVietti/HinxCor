using System.Security.Cryptography;

namespace HinxCor.CryptoSer
{
    /// <summary>
    /// RSA加密
    /// </summary>
    public class RsaCryptoUtil : IRsaCryptoUtil
    {
        /// <summary>
        /// 生成秘钥
        /// </summary>
        /// <returns></returns>
        public RsaKey GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                var key = new RsaKey
                {
                    Private = rsa.ToXmlString(true),
                    Public = rsa.ToXmlString(false)
                };

                return key;
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public byte[] Sign(byte[] bytes, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                var signature = rsa.SignData(bytes, new MD5CryptoServiceProvider());
                return signature;
            }
        }

        /// <summary>
        /// 检验
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="signature"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public bool Verify(byte[] bytes, byte[] signature, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                return rsa.VerifyData(bytes, new MD5CryptoServiceProvider(), signature);
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainBytes, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                var encryptedBytes = rsa.Encrypt(plainBytes, false);
                return encryptedBytes;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] encryptedBytes, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                var decryptedBytes = rsa.Decrypt(encryptedBytes, false);
                return decryptedBytes;
            }
        }
    }
}