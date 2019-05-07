using System;
using System.Collections.Generic;

namespace HinxCor.IO
{
    /// <summary>
    /// entry of bundle file
    /// </summary>
    public class Int32Entry : IDataEntry<int>
    {
        public Int32Entry(int value)
        {
            this.@object = value;
        }

        public Int32Entry(byte[] data)
        {
            this.data = data;
        }

        public int @object
        {
            get { return BitConverter.ToInt32(data, 0); }
            set { data = BitConverter.GetBytes(value); }
        }

        public BundleType DataType => BundleType.Int;

        public int DataSize => 4;

        public byte[] data { get; protected set; }
    }
}

