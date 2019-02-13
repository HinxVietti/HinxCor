using HinxCor;
using HinxCor.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFRRP
{
    class Program
    {

        static void Main(string[] args)
        {
            //用BAT打包可执行文件

            //解析cs源码

            string key = "HinxCor.EncrytoPass";
            string csFile = "code.depub";
            var data = File.ReadAllBytes(csFile);//加密了的数据
            string csname = "code.depub.decode";

            using (var rc4 = new RC4(Encoding.UTF8.GetBytes(key)))
            {
                string cedetail = rc4.Decrypt(data);
                File.WriteAllText(csname, cedetail);

                var processInfo = new ProcessStartInfo("tool_RegisterPath.exe");
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                // *** Redirect the output ***
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;
                var p = Process.Start(processInfo);
                p.WaitForExit();
                Thread.Sleep(100);

                string batcmd = string.Format(@"csc -out:{0} {1} -win32icon:icon.ico -resource:{2} -resource:{3} -resource:{4} -resource:{5}"
        , args[0], csname, "DCARE.exe", "HinxCor.CompressionDot45.dll", "ICSharpCode.SharpZipLib.dll", "data.mhx");
                Windows.ExecuteCommand(batcmd);
                Console.WriteLine("batch:" + batcmd);
                File.Delete(csname);
                Console.ReadKey();
            }
        }
    }
}
