using HinxCor.Compression.net45;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DCARE
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            string passwd = "F41E6-F565-41F1F-C1DR5-6QW";
            Random rm = new Random((int)DateTime.Now.ToFileTimeUtc());
            string folderName = "~rundata_" + rm.Next(0, 9999);
            try
            {
                var win = new ExtractingBox();
                new Thread(() => { Application.Run(win); }) { IsBackground = true }.Start();

                int contine = 0;
                //ZipHelper.UnCompressionFile("data.mhx", folderName, passwd);
                Action<float> on_Progress = progress =>
                {
                    win.SetValue(progress);
                };
                Action on_Finished = () =>
                {
                    contine = 1;
                };
                Action<Exception> on_Error = e =>
                {
                    contine = -1;
                };
                new Thread(() =>
                {
                    ZipHelper.AsyncUnCompressionFile(on_Progress, on_Finished, "data.mhx", on_Error, folderName, passwd);
                })
                {
                    IsBackground = true
                }.Start();
                while (contine == 0) Thread.Sleep(100);
                win.DoClose();
                if (contine == -1) return -11;//解压出错

            }
            catch
            {
                return -1;
            }
            string[] exels = new[] { "~anonymous.exe", "buildin.exe", "index.exe" };
            DirectoryInfo dir = new DirectoryInfo(folderName);
            dir.Attributes = FileAttributes.Hidden;
            var exes = dir.GetFiles("*.exe");
            if (exes.Length < 1) return -2;

            string exename = "";
            for (int i = 0; i < exels.Length; i++)
                for (int j = 0; j < exes.Length; j++)
                {
                    if (exels[i] == exes[j].Name)
                    {
                        exename = exels[i];
                        goto exe;
                    }
                }

            exename = exes[0].Name;
            exe:
            FileInfo exefile = new FileInfo(folderName + @"\" + exename);
            ProcessStartInfo startinfo = new ProcessStartInfo(exefile.FullName);
            startinfo.WorkingDirectory = exefile.DirectoryName;
            var p = Process.Start(startinfo);
            while (p.HasExited == false) Thread.Sleep(100);
            //清理目录
            try
            {
                ClearFolder(dir);
            }
            catch
            {
                return -3;
            }
            return 0;
        }

        private static void ClearFolder(DirectoryInfo dir)
        {
            var dirs = dir.GetDirectories();
            var files = dir.GetFiles();
            for (int i = 0; i < dirs.Length; i++)
                ClearFolder(dirs[i]);
            for (int i = 0; i < files.Length; i++)
                files[i].Delete();
            dir.Delete();
        }
        
    }
}
