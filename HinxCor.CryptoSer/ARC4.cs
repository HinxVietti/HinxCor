using System;
using System.Collections.Generic;
using System.Text;


/*
* RC4
* 
* An ActionScript 3 implementation of RC4
* Copyright (c) 2007 Henri Torgemane
* 
* Derived from:
* 		The jsbn library, Copyright (c) 2003-2005 Tom Wu
* 
* See LICENSE.txt for full license information.
*/
namespace HinxCor.Security
{
    /// <summary>
    ///RC4加密算法;
    ///Please enter your massage supported Encoring. Default is UTF-8
    /// </summary>
    public class RC4 : IDisposable
    {
        private int i = 0;
        private int j = 0;
        private ByteArray S;
        private ByteArray key;
        private const uint psize = 256;

        private string keygen = "HinxCor.Encrypt:";
        private Encoding Encoding = Encoding.UTF8;

        /// <summary>
        ///  实例化
        /// </summary>
        public RC4()
        {
            Construct(Encoding, keygen);
        }

        /// <summary>
        ///  实例化
        /// </summary>
        /// <param name="encoding">编码</param>
        public RC4(Encoding encoding)
        {
            Construct(encoding, keygen);
        }

        /// <summary>
        ///  实例化
        /// </summary>
        /// <param name="keyg">秘钥</param>
        public RC4(string keyg)
        {
            Construct(Encoding, keyg);
        }

        /// <summary>
        ///  实例化
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="keyg">秘钥</param>
        public RC4(Encoding encoding, string keyg)
        {
            Construct(encoding, keyg);
        }

        private void Construct(Encoding encoding, string keyg)
        {
            this.Encoding = encoding;
            this.keygen = keyg;
            S = new ByteArray();
            var keygendata = Encoding.GetBytes(keygen);
            init(keygendata);
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] Encrypt(string message)
        {
            return encrypt(Encoding.GetBytes(message));
        }

        /// <summary>
        /// 解密为字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Decrypt(byte[] data)
        {
            return Encoding.GetString(encrypt(data));
        }

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="key">Keygen</param>
        public RC4(ByteArray key = null)
        {
            S = new ByteArray();
            if (key != null)
                init(key);
        }

        /// <summary>
        /// PSize
        /// </summary>
        /// <returns></returns>
        public uint getPoolSize()
        {
            return psize;
        }

        /// <summary>
        /// INIT
        /// </summary>
        /// <param name="key"></param>
        public void init(ByteArray key)
        {

            this.key = key; //keep a copy, as we need to reset our state for every encrypt/decrypt call.

            int i;
            int j;
            int t;


            for (i = 0; i < 256; ++i)
            {
                S[i] = (byte)i;
            }

            j = 0;
            for (i = 0; i < 256; ++i)
            {
                j = (j + S[i] + key[i % key.Count]) & 255;
                t = S[i];
                S[i] = S[j];
                S[j] = (byte)t;
            }

            this.i = 0;

            this.j = 0;
        }

        /// <summary>
        /// NEXT
        /// </summary>
        /// <returns></returns>
        public uint next()
        {
            int t;
            i = (i + 1) & 255;
            j = (j + S[i]) & 255;
            t = S[i];
            S[i] = S[j];
            S[j] = (byte)t;
            return S[(t + S[i]) & 255];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public uint getBlockSize()
        {
            return 1;
        }

        /// <summary>
        /// 加密C2C
        /// </summary>
        /// <param name="block"></param>
        public byte[] encrypt(ByteArray block)
        {
            init(key);
            int i = 0;
            while (i < block.Count)
            {
                block[i++] ^= (byte)next();
            }
            return block;
        }
        /// <summary>
        /// 解密C2C
        /// </summary>
        /// <param name="block"></param>
        public byte[] Decrypt(ByteArray block)
        {
            return encrypt(block); // the beauty of XOR.
        }


        /// <summary>
        /// STR
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return "rc4";
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {

            int i = 0;
            Random random = new Random();
            for (i = 0; i < S.Count; i++)
            {
                S[i] = (byte)(random.Next() * 256);
            }
            for (i = 0; i < key.Count; i++)
            {
                key[i] = (byte)(random.Next() * 256);
            }

            //S.Length = 0;
            //key.Length = 0;
            S = null;
            key = null;

            this.i = 0;

            this.j = 0;

            GC.Collect();
        }


        /// <summary>
        /// 诡异的字节数组
        /// </summary>
        public class ByteArray : Dictionary<int, byte>
        {

            /// <summary>
            /// 隐式转换byte[]为ByteArray
            /// </summary>
            /// <param name="data"></param>
            public static implicit operator ByteArray(byte[] data)
            {
                ByteArray byt = new ByteArray();
                for (int i = 0; i < data.Length; i++)
                    byt[i] = data[i];
                return byt;
            }
            /// <summary>
            /// 隐式转换ByteArray为byte[]
            /// </summary>
            /// <param name="array"></param>
            public static implicit operator byte[] (ByteArray array)
            {
                byte[] data = new byte[array.Count];
                foreach (var dt in array)
                    data[dt.Key] = dt.Value;
                return data;
            }
        }
    }

}
