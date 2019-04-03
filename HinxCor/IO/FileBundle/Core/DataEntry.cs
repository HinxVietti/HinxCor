using HinxCor.IO;
using System;
using System.Collections.Generic;

[System.Obsolete]
public abstract class DataEntry<T> : IDataEntry<T> // T File ByteArray String Int and Etc
{
    public int Length { get { return data.Length + 8; } }

    public abstract BundleType DataType { get; protected set; }

    public abstract int DataSize { get; protected set; }

    public abstract byte[] data { get; protected set; }

    public abstract T @object { get; protected set; }

    //BundleType IDataEntry.DataType => throw new NotImplementedException();
}

