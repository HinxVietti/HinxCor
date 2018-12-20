using HinxCor.Common;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ZipHelper
{


    /// <summary>
    /// 打包指定文件夹下的文件，并在指定路径创建zip文件
    /// </summary>
    /// <param name="filesPath">指定待打包的文件夹</param>
    /// <param name="zipFilePath">创建的zip文件完全路径</param>
    /// <returns>是否成功生成</returns>
    public static bool CreateZipFile(string filesPath, string zipFilePath)
    {
        bool success = false;
        if (!Directory.Exists(filesPath))
        {
            Debug.Log("Cannot find directory " + filesPath);
            return false;
        }
        try
        {
            string[] filenames = Directory.GetFiles(filesPath);
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
            {

                s.SetLevel(AppConfig.CompressedLevel); // 压缩级别 0-9
                //s.Password = "123";
                byte[] buffer = new byte[4096]; //缓冲区大小
                foreach (string file in filenames)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                    entry.DateTime = System.DateTime.Now;
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                success = true;
                s.Finish();
                s.Close();
            }
        }
        catch (System.Exception ex)
        {
            success = false;
            Debug.Log("Exception during zip processing " + ex);
        }
        finally
        {

        }
        return success;
    }

    /// <summary>
    /// 多个文件打包
    /// </summary>
    /// <param name="files"></param>
    /// <param name="zipFilePath"></param>
    /// <returns></returns>
    public static bool CompressZip(string[] files, string zipFilePath)
    {
        bool success = false;
        try
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
            {
                s.SetLevel(AppConfig.CompressedLevel); // 压缩级别 0-9
                byte[] buffer = new byte[4096]; //缓冲区大小
                foreach (string file in files)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                    entry.DateTime = System.DateTime.Now;
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                success = true;
                s.Finish();
                s.Close();
            }
        }
        catch (System.Exception ex)
        {
            success = false;
            Debug.Log("Exception during processing " + ex);
        }
        return success;
    }

    /// <summary>
    /// 解压并且返回第一个符合筛选拓展名的文件
    /// </summary>
    /// <param name="zipFilePath"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static string UnZipAndGetSpecialFile(string zipFilePath, string filter)
    {
        var array = UnZipAndReturnFiles(zipFilePath);
        foreach (var path in array)
        {
            int index = path.LastIndexOf('.');
            var est = path.Remove(0, index + 1);
            if (est.Equals(filter)) return path;

        }
        return "";
    }

    public static string[] UnZipAndReturnFiles(string zipFilePath)
    {
        return UnZipFile(zipFilePath, VisionHelper.CachePath);
    }

    public static string[] UnZipFile(string zipFilePath, string rootFolder = "")
    {
        //string rootFolder = VisionHelper.CachePath;
        if (!File.Exists(zipFilePath))
        {
            Debug.LogWarning("Cannot find file " + zipFilePath);
            return null;
        }

        List<string> args = new List<string>();

        using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
        {

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {

                string directoryName = Path.GetDirectoryName(theEntry.Name);
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                if (fileName != string.Empty)
                {
                    string filename = rootFolder + theEntry.Name;
                    args.Add(filename);
                    FileInfo finfo = new FileInfo(filename);
                    if (finfo.Directory.Exists == false) finfo.Directory.Create();
                    //
                    using (FileStream streamWriter = File.Create(filename))
                    {
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                                streamWriter.Write(data, 0, size);
                            else
                                break;
                        }
                        streamWriter.Flush();
                    }
                }
            }
        }
        return args.ToArray();
    }

    public static TextTemplate UnzipTextTemplate(string fname)
    {
        //string rootFolder = VisionHelper.CachePath;
        if (!File.Exists(fname))
        {
            Debug.LogWarning("Cannot find file " + fname);
            return null;
        }

        TextTemplate result = new TextTemplate();
        int step = 0;

        using (ZipInputStream s = new ZipInputStream(File.OpenRead(fname)))
        {

            ZipEntry theEntry;
            byte[] buffer;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(theEntry.Name);
                switch (fileName)
                {
                    case "config.json":
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int size = 2048;
                            buffer = new byte[size];
                            while (true)
                            {
                                size = s.Read(buffer, 0, buffer.Length);
                                if (size > 0)
                                    ms.Write(buffer, 0, size);
                                else
                                    break;
                            }
                            ms.Flush();
                            ms.Position = 0;
                            buffer = new byte[ms.Length];
                            ms.Read(buffer, 0, buffer.Length);
                            string json = Encoding.Default.GetString(buffer, 0, buffer.Length);
                            //Debug.Log("Json:" + json);// the json is right answer
                            result.textData = LitJson.JsonMapper.ToObject<VisionTextData>(json);
                            step++;
                        }
                        break;
                    case "thumbnail.png":
                        buffer = new byte[s.Length];
                        s.Read(buffer, 0, buffer.Length);
                        Texture2D t = new Texture2D(1, 1);
                        t.LoadImage(buffer);
                        result.Thumbnail = t;
                        step++;
                        break;
                }

            }
        }
        result.TemplateName = new FileInfo(fname).Name.RemoveExtension();
        if (step < 2) Debug.LogWarning("Something wrong while unpack txt template files");
        return result;
    }

    public static bool ZipTextTemplate(TextTemplate template, string outputPath)
    {
        string fname = outputPath + "/" + template.TemplateName + ".vtxt";
        try
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(fname)))
            {
                s.SetLevel(AppConfig.CompressedLevel); // 压缩级别 0-9

                ZipEntry entry = new ZipEntry("config.json");
                entry.DateTime = System.DateTime.Now;
                s.PutNextEntry(entry);
                var json = LitJson.JsonMapper.ToJson(template.textData);
                var buffer = Encoding.Default.GetBytes(json);
                s.Write(buffer, 0, buffer.Length);

                entry = new ZipEntry("thumbnail.png");
                entry.DateTime = System.DateTime.Now;
                s.PutNextEntry(entry);
                var img = template.Thumbnail;
                buffer = (img as Texture2D).EncodeToPNG();
                s.Write(buffer, 0, buffer.Length);

                s.Finish();
                s.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;

    }

}