using HinxCor.Compression.net45;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPRES
{
    class Program
    {
        static void Main(string[] args)
        {
            //ARGS 是所有需要Pack到 data.mhx 的内容
            //pack 密码 
            string passwd = "F41E6-F565-41F1F-C1DR5-6QW";
            var ZipFile = "data.mhx";

            ZipHelper.CompressFilesAndFolder(args, ZipFile, ZipHelper.CompressionLevel.Deflated, passwd);
        }
    }
}
