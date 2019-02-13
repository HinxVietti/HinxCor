using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using HinxCor.Compression.net45;
using HinxCor.Security;
using Windows = HinxCor.Windows;


public class HVAppCompress
{

    //if there is icon/icon.ico, it will be use into public target.exe
    //-exe target.exe -dir AppFolder -run ToExcuteExe.exe -args arg1 arg2 ..
    public static void Main(string[] args)
    {
        var ZipFile = "data.mhx";
        string key = "HinxCor.EncrytoPass";
        string tmpCsFile = "se.cs";
        string passwd = "F41E6-F565-41F1F-C1DR5-6QW";

        ZipHelper.CompressFolder(args[3], ZipFile, 5, passwd);
        Thread.Sleep(20);

        string csFile = "code.depub";
        var bytes = File.ReadAllBytes(csFile);
        string csCode = "";

        var coder = Encoding.UTF8.GetBytes(key);
        using (var decrytor = new RC4(coder))
        {
            var decodeData = decrytor.Decrypt((RC4.ByteArray)bytes);
            csCode = Encoding.UTF8.GetString(decodeData);
        }
        csCode = Regex.Replace(csCode, "exeFile", args[5]);
        csCode = Regex.Replace(csCode, "passwd", passwd);
        File.WriteAllText(tmpCsFile, csCode);
        new FileInfo(tmpCsFile).Attributes = FileAttributes.Hidden;
        Thread.Sleep(25);

        string batCmd = string.Format(@"@echo off csc -out:{0} {1}  -win32icon:{2}  -resource:{3},{4}", args[1], tmpCsFile, "icon/icon.ico", "HinxCor.CompressionDot45.dll", ZipFile);

        Windows.ExecuteCommand(batCmd);

    }
}


public class 单个exe文件
{
   
}

//HinxCor.CompressionDot45.dll
//HinxCor.Security.dll
//HinxCor.dll

//-exe ToExcuteExe.exe -folder folderName -args arg1 arg2 ..
//-exe ToExcuteExe.exe -args arg1 arg2 ..

public class DeCompress
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
        WriteResourceToFile(ZipFile, ZipFile);
        ZipHelper.UnCompressionFile(ZipFile, dirname, "passwd");

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

    //private static string GetArgument(List<string> vargs)
    //{
    //    StringBuilder sb = new StringBuilder(1024);
    //    foreach (var arg in vargs)
    //        sb.Append(arg + " ");
    //    string args = sb.ToString();
    //    if (!string.IsNullOrEmpty(args) && !string.IsNullOrWhiteSpace(args))
    //        return args.Remove(args.Length - 1, 1);
    //    return "";
    //}
}

//namespace DynamicSugar
//{
//    /// <summary>
//    /// Dynamic Sharp Helper Class, dedicated methods to work with text resource file
//    /// </summary>
//    public static partial class Embed
//    {

//        public static class Resources
//        {
//            /// <summary>
//            /// Return the fully qualified name of the resource file
//            /// </summary>
//            /// <param name="resourceFileName">File name of the resource</param>
//            /// <returns></returns>
//            private static string GetResourceFullName(string resourceFileName, Assembly assembly)
//            {

//                foreach (var resource in assembly.GetManifestResourceNames())
//                    if (resource.EndsWith(resourceFileName))
//                        return resource;

//                throw new System.ApplicationException(string.Format("Resource '{0}' not find in assembly '{1}'", resourceFileName, Assembly.GetExecutingAssembly().FullName));
//            }
//            /// <summary>
//            /// Return the content of a file embed as a resource.
//            /// The function takes care of finding the fully qualify name, in the current
//            /// assembly.
//            /// </summary>
//            /// <param name="resourceFileName"></param>
//            /// <param name="assembly"></param>
//            /// <returns></returns>
//            public static byte[] GetBinaryResource(string resourceFileName, Assembly assembly)
//            {
//                var resourceFullName = GetResourceFullName(resourceFileName, assembly);
//                var stream = assembly.GetManifestResourceStream(resourceFullName);
//                byte[] data = new Byte[stream.Length];
//                stream.Read(data, 0, (int)stream.Length);
//                return data;
//            }
//            /// <summary>
//            /// Save a buffer of byte into a file
//            /// </summary>
//            /// <param name="byteArray"></param>
//            /// <param name="fileName"></param>
//            /// <returns></returns>
//            private static bool SaveByteArrayToFile(byte[] byteArray, string fileName)
//            {
//                try
//                {
//                    using (Stream fileStream = File.Create(fileName))
//                    {
//                        fileStream.Write(byteArray, 0, byteArray.Length);
//                        fileStream.Close();
//                        return true;
//                    }
//                }
//                catch
//                {
//                    return false;
//                }
//            }
//            /// <summary>
//            /// Save a resource as a local file
//            /// </summary>
//            /// <param name="resourceFileName">Resource name and filename</param>
//            /// <param name="assembly">Assembly where to get the resource</param>
//            /// <param name="path">Local folder</param>
//            /// <returns></returns>
//            public static string SaveBinaryResourceAsFile(Assembly assembly, string path, string resourceFileName)
//            {

//                var outputFileName = System.IO.Path.Combine(path, resourceFileName);
//                if (System.IO.File.Exists(outputFileName))
//                {
//                    System.IO.File.Delete(outputFileName);
//                }

//                if (!System.IO.Directory.Exists(path))
//                    System.IO.Directory.CreateDirectory(path);

//                var buffer = GetBinaryResource(resourceFileName, assembly);

//                SaveByteArrayToFile(buffer, outputFileName);
//                return outputFileName;
//            }
//        }
//    }
//}


