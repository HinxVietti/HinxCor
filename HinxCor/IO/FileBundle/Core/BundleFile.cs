using System;
using System.Collections.Generic;
using System.IO;

namespace HinxCor.IO
{
    /// <summary>
    /// 专注文件读写的bundle;
    /// <para>如果使用string构造,将开启文件流,在释放对象的时候,写入对象数据</para>
    /// <para>如果使用流打开的话即仅仅读取数据到内存</para>
    /// <para>开始任意Pop 或者push操作的时候请先调用其Start方法,结束后调用End方法</para>
    /// </summary>
    public class BundleFile : IDisposable
    {
        Stream stream;
        bool Poping = false;
        bool Pushing = false;
        Dictionary<BundleType, Type> reflMap = new Dictionary<BundleType, Type>();

        public BundleFile(Stream stream)
        {
            this.stream = stream;
        }


        ~BundleFile()
        {
            this.Dispose();
        }

        /// <summary>
        /// 制定包含byte[]构造的对象类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool RegisterDataEntry(BundleType type, Type entry)
        {
            if (!reflMap.ContainsKey(type))
            {
                reflMap.Add(type, entry);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 制定包含使用byte[] 构造的对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool RegisterDataEntry<T>(IDataEntry<T> entry)
        {
            if (!reflMap.ContainsKey(entry.DataType))
            {
                reflMap.Add(entry.DataType, entry.GetType());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 打开或者创建FileStream
        /// </summary>
        /// <param name="fileName"></param>
        public BundleFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            this.stream = new FileStream(fileName, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// 打开或者创建Stream
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static BundleFile CreateFrom(Stream sourceStream)
        {
            return new BundleFile(sourceStream);
        }

        /// <summary>
        /// 打开或者创建FileStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static BundleFile CreateFrom(string fileName)
        {
            return new BundleFile(fileName);
        }

        /// <summary>
        /// 将数据复制到流
        /// </summary>
        /// <param name="destinateStream"></param>
        public void CopyTo(Stream destinateStream)
        {
            stream.CopyTo(destinateStream);
        }

        public void PushEntry<T>(IDataEntry<T> entry)
        {
            if (!Pushing)
                throw new InvalidOperationException("请先调用StarPush或者ContinuePush方法准备");
            stream.Write(getData((int)entry.DataType), 0, 4);
            stream.Write(getData(entry.DataSize), 0, 4);
            stream.Write(entry.data, 0, entry.data.Length);
        }

        /// <summary>
        /// int32 , float , double , text , png , string 能够直接返回Entry
        /// </summary>
        /// <returns></returns>
        public IDataEntry PopEntry()
        {
            if (!Poping)
                throw new InvalidOperationException("请先调用StarPop方法准备");
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            var t = (BundleType)getInt(buffer);
            stream.Read(buffer, 0, 4);
            var size = getInt(buffer);
            buffer = new byte[size];
            stream.Read(buffer, 0, size);

            if (reflMap.ContainsKey(t))
            {
                //包含 byte[] 的构造函数
                var rt_constructer = reflMap[t].GetConstructor(new[] { typeof(byte[]) });
                object go = buffer;
                var rt_go = (IDataEntry)rt_constructer.Invoke(new[] { go });
                return rt_go;
            }

            switch (t)
            {
                case BundleType.Int:
                    return new Int32Entry(buffer);
                case BundleType.Float:
                    return new FloatEntry(buffer);
                case BundleType.Double:
                    return new DoubleEntry(buffer);
                case BundleType.Text:
                    return new StringEntry(buffer);
                case BundleType.fPNG:
                    return new PNGFileEntry(buffer);
                case BundleType.fTEXT:
                    return new TxtFileEntry(buffer);
                default:
                    break;
            }
            throw new Exception("无法提取Entry:" + t);
        }


        private int getInt(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        private byte[] getData(int value)
        {
            return BitConverter.GetBytes(value);
        }


        public void ContinuePush()
        {
            Pushing = true;
            EndPop();
        }

        public void StartPush()
        {
            Pushing = true;
            EndPop();
        }

        public void EndPush()
        {
            Pushing = false;

        }


        public void EndPop()
        {
            stream.Position = stream.Length;
            Poping = false;
        }

        /// <summary>
        /// 开始Pop内容
        /// </summary>
        public void StartPop()
        {
            stream.Position = 0;
            Poping = true;
        }

        public void Dispose()
        {
            try
            {
                stream.Flush();
                stream.Dispose();
            }
            catch
            {

            }
        }
    }

}
