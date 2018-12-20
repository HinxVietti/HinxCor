using System.IO;
using System.Threading;


/*
 * 切割 主指令 @
 * 切割 结果和 地址 #
 * 切割 多路径 ~
 * 切割 多格式 $
 * S 多文件
 * F 多格式
 */


public class Windows
{
    /// <summary>
    /// 在Windows资源管理器上打开该文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void OpenInExplorer(string path)
    {
        path = path.Replace('/', '\\');
        System.Diagnostics.Process.Start("explorer.exe", path);
    }

    public static void ShowInExplorer(string path)
    {
        OpenInExplorer(path);
    }

    public static string SaveFile(string title, string fileType)
    {
        return io_cmd("OpenFile", string.Format("{0}#{1}", title, fileType));
    }

    public static string SaveFileIgnoreException(string title, string ft)
    {
        return SaveFile(title, ft).Split('#')[1];
    }

    public static string OpenFile(string title, string ftype)
    {
        return SaveFile(title, ftype);
    }
    public static string OpenFileIgnoreException(string title, string ftype)
    {
        return OpenFile(title, ftype).Split('#')[1];
    }

    public static string OpenFilefIgnoreException(string title, string desc, params string[] fts)
    {
        return OpenFilef(title, desc, fts).Split('#')[1];
    }
    public static string OpenFilef(string title, string desc, params string[] fts)
    {
        string fileType = "";
        foreach (var ty in fts)
        {
            fileType += ty + '$';
        }
        fileType = fileType.Remove(fileType.Length - 1);

        return io_cmd("OpenFileF", string.Format("{0}#{1}#{2}", title, fileType, desc));
    }

    public static string[] OpenFilefsIgnoreException(string title, string desc, params string[] fts)
    {
        string res = OpenFilefs(title, desc, fts);
        return res.Split('#')[1].Split('~');
    }

    public static string OpenFilefs(string title, string desc, params string[] fts)
    {
        string fileType = "";
        foreach (var ty in fts)
        {
            fileType += ty + '$';
        }
        fileType = fileType.Remove(fileType.Length - 1);

        return io_cmd("OpenFileFS", string.Format("{0}#{1}#{2}", title, fileType, desc));

    }

    public static string[] OpenFilesIgnoreException(string title, string ft)
    {
        return OpenFiles(title, ft).Split('#')[1].Split('~');
    }

    public static string OpenFiles(string title, string fileType)
    {
        return io_cmd("OpenFileS", string.Format("{0}#{1}", title, fileType));

    }
    public static string SelectFolder()
    {
        return io_cmd("SelectFolder");
    }
    public static string SelectFolderIgnoreException()
    {
        return io_cmd("SelectFolder").Split('#')[1];
    }

    private static string io_cmd(string cmd, string io = null)
    {
        string tmpp = "_tmp_f_" + UidGenerator.GetShortId();
        if (File.Exists(tmpp)) File.Delete(tmpp);
        string argument = string.Format("{0}@{1}#{2}", cmd, tmpp, io);
        System.Diagnostics.Process.Start("FolderBrowserDialog.exe", argument);
        while (File.Exists(tmpp) == false) Thread.Sleep(200);
        var reader = new StreamReader(tmpp);
        var result = reader.ReadLine();
        reader.Dispose();
        reader.Close();
        File.Delete(tmpp);
        return result;
    }
}

