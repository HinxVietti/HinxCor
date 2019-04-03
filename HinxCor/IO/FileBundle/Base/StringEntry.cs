using System;
using System.Collections.Generic;
using System.Text;

namespace HinxCor.IO
{

    public sealed class StringEntry : IDataEntry<string>
    {
        public StringEntry(string text)
        {
            this.@object = text;
        }

        public StringEntry(byte[] data)
        {
            this.data = data;
        }

        public string @object { get { return Encoding.UTF8.GetString(data); } private set { data = Encoding.UTF8.GetBytes(value); } }

        public BundleType DataType { get { return BundleType.Text; } }

        public int DataSize { get { return data.Length; } }

        public byte[] data { get; private set; }
    }
}

