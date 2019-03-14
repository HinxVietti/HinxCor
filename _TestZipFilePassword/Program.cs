using System;
using System.Diagnostics;
using System.IO;

namespace _TestZipFilePassword
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = System.DateTime.Now;
            ProcessStartInfo exe = new ProcessStartInfo();
            exe.FileName = @"C:\Program Files\WinRAR\WinRar.exe";
            exe.CreateNoWindow = true;
            exe.UseShellExecute = false;

            string archiveName = args[0];

            StreamReader reader = new StreamReader("codepage.txt");
            int index = 0;
            while (true)
            {
                index++;
                string cmd = reader.ReadLine();
                if (string.IsNullOrEmpty(cmd)) break;
                exe.Arguments = GetCMD(cmd, archiveName);
                var p = Process.Start(exe);
                p.WaitForExit();
                if (p.ExitCode == 0)
                {
                    Console.WriteLine("passwd:" + cmd);
                    Console.WriteLine(index + " times Test .USE:" + (DateTime.Now - t).TotalSeconds + " S");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("NO PASSWD");
            Console.WriteLine(index + " times Test .USE:" + (DateTime.Now - t).TotalSeconds + " S");
            Console.ReadKey();
            //Windows.ExecuteCommand("unrar");
        }

        private static string GetCMD(string passwd, string filename)
        {
            return string.Format(@"T -p{0} -y {1}", passwd, filename);
        }


    }
}
