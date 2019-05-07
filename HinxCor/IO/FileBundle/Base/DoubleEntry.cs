using System;
using System.Collections.Generic;


namespace HinxCor.IO
{
    /// <summary>
    /// entry of bundle file
    /// </summary>
    public class DoubleEntry : IDataEntry<double>
    {

        public DoubleEntry(double value)
        {
            this.@object = value;
        }

        public DoubleEntry(byte[] data)
        {
            this.data = data;
        }


        public double @object
        {
            get { return BitConverter.ToDouble(data, 0); }
            set { data = BitConverter.GetBytes(value); }
        }

        public BundleType DataType => BundleType.Double;

        public int DataSize => 8;

        public byte[] data { get; protected set; }
    }
}

