using HinxCor.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// 请不要添加超过2G大小的内容,数据写在内存流中
/// <para>理论支持最大240G的文件,但是我估计没哪个人才用这个打包200多G的单个文件吧;</para>
/// </summary>
public class UncompressedFileBundle : IFileBundle, IEnumerable<byte[]>, IEnumerator<byte[]>
{
    /// <summary>
    /// 单步写入数量
    /// </summary>
    //public const int BufferSize = 4096;
    public const int BufferSize = 512;

    Stream stream = new MemoryStream();


    private List<byte> _data = new List<byte>();
    public byte[] data
    {
        get { return _data.ToArray(); }
        protected set
        {
            _data = new List<byte>();
            _data.AddRange(value);
        }
    }

    public byte[] Current { get; private set; }

    object IEnumerator.Current { get { return Current; } }

    //public UncompressedFileBundle

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<byte[]> GetEnumerator()
    {
        //throw new NotImplementedException();
        return this;
    }

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public int PushByteArray(byte[] data)
    {
        try
        {
            var size = data.Length;
            stream.Write(BitConverter.GetBytes(size), 0, 4);//4byte 32 bit
            int offset = 0;
            int writedown = 0;
            while (size > 0)
            {
                writedown = size > BufferSize ? BufferSize : size;
                stream.Write(data, offset, writedown);
                size -= writedown;
            }
            return 0;
        }
        catch
        {
            return -1;
        }

    }

    public int PushNextFile(string fileName)
    {
        if (!File.Exists(fileName)) return -2;
        var data = File.ReadAllBytes(fileName);
        return PushByteArray(data);
    }

    public int PushNextString(string text)
    {
        if (string.IsNullOrEmpty(text)) return -2;
        var data = Encoding.UTF8.GetBytes(text);
        return PushByteArray(data);
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        //throw new NotImplementedException();
        return this;
    }
}

