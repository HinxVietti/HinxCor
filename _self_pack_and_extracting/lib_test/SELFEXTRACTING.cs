using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

public class SELFEXTRACTING
{
    public static void Main()
    {
        var ca = Assembly.GetExecutingAssembly();

        var handle = GetConsoleWindow();
        ShowWindow(handle, SW_HIDE);

        //提取解压的exe
        WriteResourceToFileInAssembly(ca, "DCARE.exe");
        //提取dll
        WriteResourceToFileInAssembly(ca, "HinxCor.CompressionDot45.dll");
        WriteResourceToFileInAssembly(ca, "ICSharpCode.SharpZipLib.dll");
        //提取DATA
        WriteResourceToFileInAssembly(ca, "data.mhx");

        //运行解压程序(内部挑选exe执行)
        Thread.Sleep(100);

        ProcessStartInfo pinfo = new ProcessStartInfo("DCARE.exe");
        pinfo.CreateNoWindow = true;
        pinfo.UseShellExecute = false;

        var exp = Process.Start(pinfo);
        //while (!exp.HasExited) Thread.Sleep(100);
        exp.WaitForExit();

        //switch (exp.ExitCode)
        //{
        //    case 0:
        //        Console.WriteLine("正常结束");
        //        break;
        //    case -1:
        //        Console.WriteLine("ERROR-1:解压失败");
        //        break;
        //    case -2:
        //        Console.WriteLine("ERROR-2:目录没有exe文件");
        //        break;
        //    case -3:
        //        Console.WriteLine("ERROR-3:运行正确,但是未能清理文件夹");
        //        break;
        //    default:
        //        Console.WriteLine("未知错误:" + exp.ExitCode);
        //        break;
        //}
        Thread.Sleep(100);
        //监测结束-- 删除提取的文件
        File.Delete("DCARE.exe");
        //delete dlls
        File.Delete("HinxCor.CompressionDot45.dll");
        File.Delete("ICSharpCode.SharpZipLib.dll");
        File.Delete("data.mhx");

        //Console.WriteLine();
        //Console.WriteLine("Run finished. any key to exit...");
        //Console.ReadKey();
    }

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    public static string WriteResourceToFileInAssembly(Assembly assembly, string fileName)
    {
        using (var resource = assembly.GetManifestResourceStream(fileName))
        {
            fileName = PrecheckFile(fileName);
            PrecheckPath(fileName);
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                resource.CopyTo(file);
            }
            File.SetAttributes(fileName, FileAttributes.Hidden);
        }
        return fileName;
    }

    private static void PrecheckPath(string fileName)
    {
        FileInfo f = new FileInfo(fileName);
        if (f.Directory.Exists == false) f.Directory.Create();
    }

    private static string PrecheckFile(string filename, bool delete = false)
    {
        if (File.Exists(filename))
        {
            if (delete)
            {
                File.Delete(filename);
                return filename;
            }
            else
            {
                filename += "_new";
                return PrecheckFile(filename);
            }
        }
        return filename;
    }

}

