using System;
using System.Collections.Generic;

namespace HinxCor.IO
{

    public interface IDataEntry<T> : IDataEntry
    {
        T @object { get; }
    }

    public interface IDataEntry
    {
        BundleType DataType { get; }
        int DataSize { get; }
        byte[] data { get; }
    }

}
