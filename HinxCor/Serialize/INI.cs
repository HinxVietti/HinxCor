using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile   // revision 11
{
    public string this[string key, string section] { get { return Read(key, section); } }
    public string this[string key] { get { return Read(key); } }

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