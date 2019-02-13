using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

public static class Program
{

    /// <summary>
    /// singlePackToRun..
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        string exe = "~anonymous.exe";
        bool usearg = false;

        if (args.Length < 1)
        {
            usearg = false;
            var cAssembly = Assembly.GetExecutingAssembly();
            var fileNames = cAssembly.GetManifestResourceNames();
            for (int i = 0; i < fileNames.Length; i++)
                fileNames[i] = WriteResourceToFileInAssembly(cAssembly, fileNames[i]);
            args = fileNames;
        }
        else
        {
            usearg = true;
            WriteResourceToFile(exe);
            for (int i = 0; i < args.Length; i++)
                WriteResourceToFile(args[i]);
        }

        Thread.Sleep(100);
        GC.Collect();

        Thread.Sleep(100);
        var p = Process.Start(exe);

        while (p.HasExited == false) Thread.Sleep(100);
        Console.WriteLine("Run Finished.. AnyKey to esc.");

        var AssemFs = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        //ManifestResourceInfo
        for (int i = 0; i < AssemFs.Length; i++)
        {
            Console.WriteLine("Deleting " + AssemFs[i]);
        }

        if (usearg)
            File.Delete(exe);
        for (int i = 0; i < args.Length; i++)
            File.Delete(args[i]);

        Console.ReadKey();
    }

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


    public static void WriteResourceToFile(string name)
    {
        WriteResourceToFile(name, name);
    }

    /// <summary>
    /// decompression files
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="fileName"></param>
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



//public class SetSysEnvironmentVariable
//{
//    [DllImport("Kernel32.DLL ", SetLastError = true)]
//    public static extern bool SetEnvironmentVariable(string lpName, string lpValue);

//    public static void SetPath(string pathValue)
//    {
//        string pathlist;
//        pathlist = SysEnvironment.GetSysEnvironmentByName("PATH");
//        string[] list = pathlist.Split(';');
//        bool isPathExist = false;

//        foreach (string item in list)
//        {
//            if (item == pathValue)
//                isPathExist = true;
//        }
//        if (!isPathExist)
//        {
//            SetEnvironmentVariable("PATH", pathlist + pathValue + ";");

//        }
//    }
//}


//class SysEnvironment
//{
//    /// <summary>
//    /// 获取系统环境变量
//    /// </summary>
//    /// <param name="name"></param>
//    /// <returns></returns>
//    public static string GetSysEnvironmentByName(string name)
//    {
//        string result = string.Empty;
//        try
//        {
//            result = OpenSysEnvironment().GetValue(name).ToString();//读取
//        }
//        catch (Exception)
//        {

//            return string.Empty;
//        }
//        return result;

//    }

//    /// <summary>
//    /// 打开系统环境变量注册表
//    /// </summary>
//    /// <returns>RegistryKey</returns>
//    private static RegistryKey OpenSysEnvironment()
//    {
//        RegistryKey regLocalMachine = Registry.LocalMachine;
//        RegistryKey regSYSTEM = regLocalMachine.OpenSubKey("SYSTEM", true);//打开HKEY_LOCAL_MACHINE下的SYSTEM 
//        RegistryKey regControlSet001 = regSYSTEM.OpenSubKey("ControlSet001", true);//打开ControlSet001 
//        RegistryKey regControl = regControlSet001.OpenSubKey("Control", true);//打开Control 
//        RegistryKey regManager = regControl.OpenSubKey("Session Manager", true);//打开Control 

//        RegistryKey regEnvironment = regManager.OpenSubKey("Environment", true);
//        return regEnvironment;
//    }

//    /// <summary>
//    /// 设置系统环境变量
//    /// </summary>
//    /// <param name="name">变量名</param>
//    /// <param name="strValue">值</param>
//    public static void SetSysEnvironment(string name, string strValue)
//    {
//        OpenSysEnvironment().SetValue(name, strValue);

//    }

//    /// <summary>
//    /// 检测系统环境变量是否存在
//    /// </summary>
//    /// <param name="name"></param>
//    /// <returns></returns>
//    public bool CheckSysEnvironmentExist(string name)
//    {
//        if (!string.IsNullOrEmpty(GetSysEnvironmentByName(name)))
//            return true;
//        else
//            return false;
//    }

//    /// <summary>
//    /// 添加到PATH环境变量（会检测路径是否存在，存在就不重复）
//    /// </summary>
//    /// <param name="strPath"></param>
//    public static void SetPathAfter(string strHome)
//    {
//        string pathlist;
//        pathlist = GetSysEnvironmentByName("PATH");
//        //检测是否以;结尾
//        if (pathlist.Substring(pathlist.Length - 1, 1) != ";")
//        {
//            SetSysEnvironment("PATH", pathlist + ";");
//            pathlist = GetSysEnvironmentByName("PATH");
//        }
//        string[] list = pathlist.Split(';');
//        bool isPathExist = false;

//        foreach (string item in list)
//        {
//            if (item == strHome)
//                isPathExist = true;
//        }
//        if (!isPathExist)
//        {
//            SetSysEnvironment("PATH", pathlist + strHome + ";");
//        }

//    }

//    public static void SetPathBefore(string strHome)
//    {

//        string pathlist;
//        pathlist = GetSysEnvironmentByName("PATH");
//        string[] list = pathlist.Split(';');
//        bool isPathExist = false;

//        foreach (string item in list)
//        {
//            if (item == strHome)
//                isPathExist = true;
//        }
//        if (!isPathExist)
//        {
//            SetSysEnvironment("PATH", strHome + ";" + pathlist);
//        }

//    }

//    public static void SetPath(string strHome)
//    {

//        string pathlist;
//        pathlist = GetSysEnvironmentByName("PATH");
//        string[] list = pathlist.Split(';');
//        bool isPathExist = false;

//        foreach (string item in list)
//        {
//            if (item == strHome)
//                isPathExist = true;
//        }
//        if (!isPathExist)
//        {
//            SetSysEnvironment("PATH", pathlist + strHome + ";");

//        }

//    }


//}
