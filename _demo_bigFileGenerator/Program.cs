using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _demo_bigFileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            int Times = int.Parse(args[1]);

            byte[] array = new byte[1024];
            for (int i = 0; i < 1024; i++)
                array[i] = (byte)(i % 255);
            using (var fs = new FileStream(fileName, FileMode.CreateNew))
            {
                for (int i = 0; i < Times; i++)
                    fs.Write(array, 0, array.Length);
                fs.Flush();
            }
        }
    }
}
