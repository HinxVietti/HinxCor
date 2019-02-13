using HinxCor.Compression.net45;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

public class DRARE
{

    /// <summary>
    /// extract and Run exe 
    /// -3 clearFolder error
    /// -2; folder has no exe file
    /// -1; unconpression fail.
    /// </summary>
    /// <returns></returns>

    static int Main()
    {
        string passwd = "F41E6-F565-41F1F-C1DR5-6QW";
        Random rm = new Random((int)DateTime.Now.ToFileTimeUtc());
        string folderName = "~rundata_" + rm.Next(0, 9999);
        try
        {
            ZipHelper.UnCompressionFile("data.mhx", folderName, passwd);
        }
        catch
        {
            return -1;
        }
        string[] exels = new[] { "~anonymous.exe", "buildin.exe", "index.exe" };
        DirectoryInfo dir = new DirectoryInfo(folderName);
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
        var p = Process.Start(folderName + "\\" + exename);
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

