using System;
using System.IO;
using System.Reflection;
using System.Threading;

public static class Program
{
    public static void Main(string[] args)
    {
        string exe = "~anonymous.exe";
        WriteResourceToFile(exe);
        for (int i = 0; i < args.Length; i++)
            WriteResourceToFile(args[i]);
        Thread.Sleep(100);
        GC.Collect();
        Thread.Sleep(100);
        var p = System.Diagnostics.Process.Start(exe);
        while (p.HasExited == false) Thread.Sleep(100);
        File.Delete(exe);
        for (int i = 0; i < args.Length; i++)
            File.Delete(args[i]);
    }

    public static void WriteResourceToFile(string name)
    {
        WriteResourceToFile(name, name);
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
}

