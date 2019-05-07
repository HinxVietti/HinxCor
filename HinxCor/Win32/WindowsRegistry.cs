using System;
using System.Collections.Generic;
using Microsoft.Win32;

/// <summary>
/// 感知类型
/// </summary>
public enum PerceivedType
{
    empty = -1,
    text,
    video,
    audio,
    document,
    compressed,
    vision_mod
}


public static class WindowsRegistry
{

    public class ContentType
    {
        public const string PlainText = @"text/plain";
        public const string HtmlText = @"text/html";
        public const string XComponentText = @"text/x-component";
    }

    /// <summary>
    /// 打开或者创建name子集
    /// </summary>
    /// <param name="root"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private static RegistryKey OpenOrCreate(this RegistryKey root, string name)
    {
        RegistryKey result;
        if ((result = root.OpenSubKey(name, true)) == null)
            result = root.CreateSubKey(name);
        return result;
    }

    /// <summary>
    /// -1 拓展名不合法(false), 1 不含感知类型(true) ,2 不含默认打开路径(true), 3 不含 CT(true) , 0  normal true
    /// </summary>
    /// <param name="extensionName"></param>
    /// <param name="iconpath"></param>
    /// <param name="type"></param>
    /// <param name="defaultappName"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    public static int RegisterDefaultApp(string extensionName, string iconpath, PerceivedType type = PerceivedType.empty, string defaultappName = "", string contentType = "")
    {
        if (extensionName.Contains("."))
        {
            bool isExtNameLeagl = extensionName.LastIndexOf('.') == 0;
            if (!isExtNameLeagl)
                return -1;
        }
        else extensionName = "." + extensionName;

        RegistryKey root = Registry.ClassesRoot;
        var k = root.OpenOrCreate(extensionName);

        string folderName = extensionName.Remove(0, 1);
        k.SetValue(string.Empty, "Mobiano\\" + folderName);

        var kmobiano = root.OpenOrCreate("Mobiano");
        var ext = kmobiano.OpenOrCreate(folderName);
        var defaultIcon = ext.OpenOrCreate("DefaultIcon");
        defaultIcon.SetValue(string.Empty, iconpath);

        if (type == PerceivedType.empty) return 1;
        k.SetValue("PreceivedType", type.ToString());

        if (string.IsNullOrEmpty(defaultappName))
            return 2;
        var shell = ext.OpenOrCreate("shell");
        var open = shell.OpenOrCreate("open");
        var cmd = open.OpenOrCreate("command");
        cmd.SetValue(string.Empty, defaultappName);

        if (string.IsNullOrEmpty(contentType))
            return 3;
        k.SetValue("Content Type", contentType);
        return 0;
    }
}

