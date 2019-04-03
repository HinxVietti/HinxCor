using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HinxCor.IO
{

    public class PNGFileEntry : ByteArrayEntry
    {

        public override BundleType DataType { get { return BundleType.fPNG; } }

        public PNGFileEntry(string FileName) : base(FileName) { }

        public PNGFileEntry(byte[] data) : base(data) { }


        public Image GetImage()
        {
            var ms = new MemoryStream(data);
            return Image.FromStream(ms);

        }
    }
}

