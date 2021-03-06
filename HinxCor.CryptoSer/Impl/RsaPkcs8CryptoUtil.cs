﻿//using System.IO;
//using System.Security.Cryptography;
//using System.Text;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Security;

/*******************************************************************
 * RSA 非对称可逆加密
 * 此处注释原因:没有需要两步校验内容
 * 公钥:对外开放,给别人加密
 * 私钥:解密内容,给自己看,用于解密别人加密的内容
 * *************************************************************************/

//namespace HinxCor.CryptoSer
//{
//    /// <summary>
//    /// RSA PKCS8 加密
//    /// </summary>
//    public class RsaPkcs8CryptoUtil : IRsaCryptoUtil
//    {
//        /// <summary>
//        /// 生成秘钥
//        /// </summary>
//        /// <returns></returns>
//        public RsaKey GenerateKeys()
//        {
//            using (var rsa = new RSACryptoServiceProvider())
//            {
//                var keyPair = DotNetUtilities.GetRsaKeyPair(rsa);

//                var key = new RsaKey
//                {
//                    Private = GeneratePrivateKey(keyPair.Private),
//                    Public = GeneratePublicKey(keyPair.Public)
//                };

//                return key;
//            }
//        }

//        /// <summary>
//        /// 签名
//        /// </summary>
//        /// <param name="bytes"></param>
//        /// <param name="privateKey"></param>
//        /// <returns></returns>
//        public byte[] Sign(byte[] bytes, string privateKey)
//        {
//            using (var rsa = new RSACryptoServiceProvider())
//            {
//                var key = ParsePrivateKey(privateKey);
//                rsa.ImportParameters(key);
//                var signature = rsa.SignData(bytes, new MD5CryptoServiceProvider());
//                return signature;
//            }
//        }

//        /// <summary>
//        /// 校验
//        /// </summary>
//        /// <param name="bytes"></param>
//        /// <param name="signature"></param>
//        /// <param name="publicKey"></param>
//        /// <returns></returns>
//        public bool Verify(byte[] bytes, byte[] signature, string publicKey)
//        {
//            using (var rsa = new RSACryptoServiceProvider())
//            {
//                var key = ParsePublicKey(publicKey);
//                rsa.ImportParameters(key);
//                return rsa.VerifyData(bytes, new MD5CryptoServiceProvider(), signature);
//            }
//        }

//        /// <summary>
//        /// 加密
//        /// </summary>
//        /// <param name="plainBytes"></param>
//        /// <param name="publicKey"></param>
//        /// <returns></returns>
//        public byte[] Encrypt(byte[] plainBytes, string publicKey)
//        {
//            using (var rsa = new RSACryptoServiceProvider())
//            {
//                var key = ParsePublicKey(publicKey);
//                rsa.ImportParameters(key);
//                var encryptedBytes = rsa.Encrypt(plainBytes, false);
//                return encryptedBytes;
//            }
//        }

//        /// <summary>
//        /// 解密
//        /// </summary>
//        /// <param name="encryptedBytes"></param>
//        /// <param name="privateKey"></param>
//        /// <returns></returns>
//        public byte[] Decrypt(byte[] encryptedBytes, string privateKey)
//        {
//            using (var rsa = new RSACryptoServiceProvider())
//            {
//                var key = ParsePrivateKey(privateKey);
//                rsa.ImportParameters(key);
//                var decryptedBytes = rsa.Decrypt(encryptedBytes, false);
//                return decryptedBytes;
//            }
//        }

//        private static string GeneratePrivateKey(AsymmetricKeyParameter key)
//        {
//            var builder = new StringBuilder();

//            using (var writer = new StringWriter(builder))
//            {
//                var pkcs8Gen = new Pkcs8Generator(key);
//                var pemObj = pkcs8Gen.Generate();

//                var pemWriter = new PemWriter(writer);
//                pemWriter.WriteObject(pemObj);
//            }

//            return builder.ToString();
//        }

//        private static string GeneratePublicKey(AsymmetricKeyParameter key)
//        {
//            var builder = new StringBuilder();

//            using (var writer = new StringWriter(builder))
//            {
//                var pemWriter = new PemWriter(writer);
//                pemWriter.WriteObject(key);
//            }

//            return builder.ToString();
//        }

//        private static RSAParameters ParsePrivateKey(string privateKey)
//        {
//            using (var reader = new StringReader(privateKey))
//            {
//                var pemReader = new PemReader(reader);
//                var key = (RsaPrivateCrtKeyParameters)pemReader.ReadObject();

//                var parameter = new RSAParameters
//                {
//                    Modulus = key.Modulus.ToByteArrayUnsigned(),
//                    Exponent = key.PublicExponent.ToByteArrayUnsigned(),
//                    D = key.Exponent.ToByteArrayUnsigned(),
//                    P = key.P.ToByteArrayUnsigned(),
//                    Q = key.Q.ToByteArrayUnsigned(),
//                    DP = key.DP.ToByteArrayUnsigned(),
//                    DQ = key.DQ.ToByteArrayUnsigned(),
//                    InverseQ = key.QInv.ToByteArrayUnsigned()
//                };

//                return parameter;
//            }
//        }

//        private static RSAParameters ParsePublicKey(string publicKey)
//        {
//            using (var reader = new StringReader(publicKey))
//            {
//                var pemReader = new PemReader(reader);
//                var key = (RsaKeyParameters)pemReader.ReadObject();

//                var parameter = new RSAParameters
//                {
//                    Modulus = key.Modulus.ToByteArrayUnsigned(),
//                    Exponent = key.Exponent.ToByteArrayUnsigned()
//                };

//                return parameter;
//            }
//        }
//    }
//}