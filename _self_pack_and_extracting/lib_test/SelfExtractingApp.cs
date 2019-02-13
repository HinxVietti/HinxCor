using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

public class SelfExtractingApp
{
    /// <summary>
    /// Run no exe
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var ZipFile = "data.chv";
        string dirname = "~tmpdata_" + DateTime.Now.GetHashCode();
        DirectoryInfo dir = new DirectoryInfo(dirname);
        dir.Create();
        dir.Attributes = FileAttributes.Hidden;
        Thread.Sleep(8);
        //WriteResourceToFile(ZipFile, ZipFile);
        DeCompressFromRes(ZipFile, dirname, "passwd");

        string exeName = "exeFile";


        var progress = Process.Start(exeName);
        while (progress.HasExited == false)
            Thread.Sleep(20);
        //wait gc.
        Thread.Sleep(1000);
        ClearFolder(dir);
        dir = null;
    }


    public static void WriteResourceToFile(string resourceName, string fileName)
    {
        using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                resource.CopyTo(file);
            }
        }
    }

    private static void DeCompressFromRes(string fileName, string destinateDir, string v)
    {
        var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

        ZipFile file = new ZipFile(resource);
        if (!string.IsNullOrEmpty(v))
            file.Password = v;
        string estName = string.IsNullOrEmpty(destinateDir) ? "ExtractData\\" : destinateDir;
        if (estName.EndsWith("\\") == false) estName += "\\";

        foreach (ZipEntry entery in file)
        {
            if (entery != null)
            {
                string fname = estName + entery.Name;
                FileInfo f = new FileInfo(fname);
                if (!f.Directory.Exists) f.Directory.Create();
                var fs = File.Create(fname);

                var stream = file.GetInputStream(entery);
                stream.CopyTo(fs);
                fs.Flush();
                fs.Close();
            }
        }
    }

    private static void ClearFolder(DirectoryInfo dir)
    {
        var dirs = dir.GetDirectories();
        var files = dir.GetFiles();
        foreach (var ddir in dirs)
            ClearFolder(dir);
        foreach (var ffile in files)
            ffile.Delete();
        dir.Delete();
    }
}

