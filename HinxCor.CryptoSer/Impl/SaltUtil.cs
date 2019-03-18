using System.Security.Cryptography;

namespace Crypto.Client.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class SaltUtil : ISaltUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GenerateSalt(int size)
        {
            //using (var rng = new RNGCryptoServiceProvider())
            var rng = new RNGCryptoServiceProvider();
            {
                var salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}