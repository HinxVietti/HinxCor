using System;
using System.Collections.Generic;
using System.IO;

namespace HinxCor.IO
{
    public abstract class ByteArrayEntry : IDataEntry<byte[]>
    {
        public byte[] @object { get; protected set; }

        public abstract BundleType DataType { get; }

        public int DataSize { get { return data.Length; } }

        public byte[] data => @object;

        public ByteArrayEntry(string FileName)
        {
            if (!File.Exists(FileName)) throw new FileNotFoundException(FileName);
            @object = File.ReadAllBytes(FileName);
        }

        public ByteArrayEntry(byte[] data)
        {
            @object = data;
        }
    }
}

