using System;
using System.Collections.Generic;
using System.IO;

public class FileBundle
{
    public List<string> FileNames { get; set; }
    public List<byte[]> FileDatas { get; set; }
    public string SaveFileName { get; set; }

    private Dictionary<string, byte[]> datas;

    internal int NextInt()
    {
        throw new NotImplementedException();
    }

    internal string NextString(int size)
    {
        throw new NotImplementedException();
    }

    internal byte[] NextArray(int size)
    {
        throw new NotImplementedException();
    }

    public void Flush()
    {



    }


    public void Open(string fileName)
    {
        using (var reader = File.OpenRead(fileName))
        {
            var sizeData = new byte[4];
            reader.Read(sizeData, 0, 4);
            int length = BitConverter.ToInt32(sizeData, 0);


        }


    }

    private struct FileCollect
    {
        public string FileName { get; set; }
        public byte FileDatas { get; set; }
    }

}


