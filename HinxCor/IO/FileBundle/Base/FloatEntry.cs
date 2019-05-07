using System;
using System.Collections.Generic;

namespace HinxCor.IO
{
    /// <summary>
    /// entry of bundle file
    /// </summary>
    public class FloatEntry : IDataEntry<float>
    {
        public FloatEntry(float value)
        {
            this.@object = value;
        }

        public FloatEntry(byte[] data)
        {
            this.data = data;
        }

        public float @object
        {
            get { return BitConverter.ToSingle(data, 0); }
            set { data = BitConverter.GetBytes(value); }
        }

        public BundleType DataType => BundleType.Float;

        public int DataSize => 4;

        public byte[] data { get; protected set; }
    }
}

