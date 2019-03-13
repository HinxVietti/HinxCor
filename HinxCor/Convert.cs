using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HinxCor
{
    /// <summary>
    /// 转换
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// 将Byte[]转为string
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ToByteString(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append((array[i] <= 15 ? "0" : "") + System.Convert.ToString((int)array[i], 16));
            }
            return sb.ToString();
        }
        /// <summary>
        /// string 2 byte[]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ignoreholdlength"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string source, bool ignoreholdlength = false)
        {
            if (ignoreholdlength && source.Length % 2 != 0) return null;
            byte[] bits = new byte[source.Length / 2];
            for (int i = 0; i < bits.Length; i++)
                bits[i] = (byte)System.Convert.ToInt32(source[2 * i] + source[2 * i + 1] + "", 16);

            return bits;
        }
        /// <summary>
        /// obj to byte[] , obj mast mark as Serilable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToByteArray<T>(T obj)
        {
            return ToByteArray((object)obj);
        }

        /// <summary>
        /// 转换对象到bytearray
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(object obj)
        {
            //IntPtr des = Marshal.GetIDispatchForObject(go);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 把byteArray 到 object
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object ToObject(byte[] array)
        {
            MemoryStream ms = new MemoryStream(array);
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(ms);
        }
    }

}
