using System;
using System.Linq;
using System.Text;

namespace Crypto.Client.Impl
{
    /// <summary>
    /// 字节工具包
    /// </summary>
    public class BytesUtil : IBytesUtil
    {
        /// <summary>
        /// 字符串转为byte[]; 编码utf-8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] FromString(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// base64 到 byte[];
        /// Convert.fromBase64
        /// </summary>
        /// <param name="base64Text"></param>
        /// <returns></returns>
        public byte[] FromBase64(string base64Text)
        {
            return Convert.FromBase64String(base64Text);
        }

        /// <summary>
        /// byte[] 到字符串 utf-8
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// byte[] 到字符串 base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// byte到HexString
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ToHex(byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.AppendFormat("{0:X2}", b);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 字节组拼
        /// </summary>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public byte[] Combine(params byte[][] arrays)
        {
            var result = new byte[arrays.Sum(a => a.Length)];

            var offset = 0;

            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }
    }
}