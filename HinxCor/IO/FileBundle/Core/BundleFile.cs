using System;
using System.Collections.Generic;
using System.IO;

namespace HinxCor.IO
{
    /// <summary>
    /// 专注文件读写的bundle
    /// </summary>
    public class BundleFile : IDisposable
    {
        Stream stream;
        bool Poping = false;

        public BundleFile(Stream stream)
        {
            this.stream = stream;
        }

        ~BundleFile()
        {
            this.Dispose();
        }

        public BundleFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            this.stream = new FileStream(fileName, FileMode.CreateNew);
        }

        public void PushEntry<T>(IDataEntry<T> entry)
        {
            stream.Write(getData((int)entry.DataType), 0, 4);
            stream.Write(getData(entry.DataSize), 0, 4);
            stream.Write(entry.data, 0, entry.data.Length);
        }

        public IDataEntry PopEntry()
        {
            if (!Poping) throw new InvalidOperationException("请先调用StarPop方法准备");
            byte[] buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            var t = (BundleType)getInt(buffer);
            stream.Read(buffer, 0, 4);
            var size = getInt(buffer);
            buffer = new byte[size];
            stream.Read(buffer, 0, size);

            switch (t)
            {
                case BundleType.File:
                    break;
                case BundleType.Text:
                    return new StringEntry(buffer);
                case BundleType.Int:
                    break;
                case BundleType.Float:
                    break;
                case BundleType.Double:
                    break;
                case BundleType.fPNG:
                    return new PNGFileEntry(buffer);
                case BundleType.fTEXT:
                    return new TxtFileEntry(buffer);
                default:
                    break;
            }
            throw new Exception("");
        }


        private int getInt(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        private byte[] getData(int value)
        {
            return BitConverter.GetBytes(value);
        }

        public void StartPush()
        {
            EndPop();
        }

        public void EndPop()
        {
            stream.Position = stream.Length;
            Poping = false;
        }

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
