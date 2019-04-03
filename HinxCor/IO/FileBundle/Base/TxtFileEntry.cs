using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HinxCor.IO
{

    public class TxtFileEntry : IDataEntry<string>
    {

        public BundleType DataType => BundleType.fTEXT;

        public string @object { get { return Encoding.UTF8.GetString(data); } private set { data = Encoding.UTF8.GetBytes(value); } }

        public int DataSize { get { return data.Length; } }

        public byte[] data { get; private set; }

        public string GetText()
        {
            return @object;
        }

        public TxtFileEntry(string fileName)
        {
            if (File.Exists(fileName))
            {
                @object = File.ReadAllText(fileName);
            }
            else
            {
                throw new FileNotFoundException(fileName);
            }
        }

        public TxtFileEntry(byte[] data)
        {
            this.data = data;
        }
    }
}

