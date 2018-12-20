using HinxCor.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

public class INI : HinxSingleton<INI>, IINI
{
    /// <summary>
    /// 请传递相对路径
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public Dictionary<string, string> ReadIni(string filename)
    {

        filename = AppConfig.AppPath + "/" + filename;
        return ReadInif(filename);
    }

    /// <summary>
    /// 请传递相对路径;
    /// end with '/'
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="massages"></param>
    public void WriteIni(string filename, Dictionary<string, string> massages)
    {
        filename = AppConfig.AppPath + "/" + filename;
        WriteInif(filename, massages);
    }

    /// <summary>
    /// 绝对路径读入
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Dictionary<string, string> ReadInif(string path)
    {
        var dic = new Dictionary<string, string>();
        FileInfo info = new FileInfo(path);
        if (info.Directory.Exists == false) info.Directory.Create();
        using (var reader = info.OpenText())
        {
            string str = "";
            do
            {
                str = reader.ReadLine();
                if (string.IsNullOrEmpty(str)) break;
                string[] arr = str.Split('=');
                if (arr.Length == 2)
                {
                    dic.Add(arr[0], arr[1]);
                }
            }
            while (string.IsNullOrEmpty(str) == false);

        }
        return dic;
    }

    /// <summary>
    /// 绝对路径写入
    /// </summary>
    /// <param name="path"></param>
    /// <param name="massage"></param>
    public void WriteInif(string path, Dictionary<string, string> massage)
    {

        FileInfo info = new FileInfo(path);
        if (info.Directory.Exists == false) info.Directory.Create();
        using (StreamWriter writer = info.CreateText())
        {
            foreach (var item in massage)
            {
                string str = item.Key + "=" + item.Value;
                writer.WriteLine(str);
            }
            writer.Flush();
        }
    }
    /// <summary>
    /// 绝对路径写入
    /// </summary>
    /// <param name="path"></param>
    /// <param name="massage"></param>
    public void WriteInif(string path, Dictionary<string, object> massage)
    {

        FileInfo info = new FileInfo(path);
        if (info.Directory.Exists == false) info.Directory.Create();
        using (StreamWriter writer = info.CreateText())
        {
            foreach (var item in massage)
            {
                string str = item.Key + "=" + item.Value;
                writer.WriteLine(str);
            }
            writer.Flush();
        }
    }
}

public class IniFile   // revision 11
{
    string Path;
    string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(string IniPath = null)
    {
        Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
    }

    public string Read(string Key, string Section = null)
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
        return RetVal.ToString();
    }

    /// <summary>
    /// read config and set source value
    /// </summary>
    /// <typeparam name="T">the property type</typeparam>
    /// <param name="Key">this key in ini file </param>
    /// <param name="obj">this property to set</param>
    /// <param name="converter">how to convet string to value </param>
    /// <param name="section">this parm in the ini file</param>
    public void Read<T>(string Key, ref T obj, Func<string, T> converter, string section = null)
    {
        string res = Read(Key, section);
        obj = converter(res);
    }
    

    public void Write(string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
    }
    public void Write(string Key, object Value, string Section = null)
    {
        WritePrivateProfileString(Section ?? EXE, Key, Value.ToString(), Path);
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section).Length > 0;
    }
}