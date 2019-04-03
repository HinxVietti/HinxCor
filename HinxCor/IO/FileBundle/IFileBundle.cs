using System;
using System.Collections.Generic;


namespace HinxCor.IO
{
    public interface IFileBundle
    {
        byte[] data { get; }
        int PushNextString(string text);
        int PushNextFile(string fileName);
        int PushByteArray(byte[] data);
    }
}

