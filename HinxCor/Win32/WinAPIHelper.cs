using System;
using System.Collections.Generic;
using System.IO;

public static class WinAPIHelper
{
    /// <summary>
    /// 获取文件的唯一标识
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static ulong GetUnipueFileID(string fileName)
    {
        if (File.Exists(fileName) == false) return 0;
        WinAPI.BY_HANDLE_FILE_INFORMATION objectFileInfo = new WinAPI.BY_HANDLE_FILE_INFORMATION();

        FileInfo fi = new FileInfo(fileName);
        FileStream fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        WinAPI.GetFileInformationByHandle(fs.Handle, out objectFileInfo);

        fs.Close();

        ulong fileIndex = ((ulong)objectFileInfo.FileIndexHigh << 32) + (ulong)objectFileInfo.FileIndexLow;

        return fileIndex;
    }

}

