using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 
/// </summary>
public interface TExeExtReader
{
    /*private*/
    string FFileName { get; set; }
    /*private*/
    int FSectionCount { get; set; }
    /*private*/
    int[] FScetions { get; set; }
    /*private*/
    int FBeginOffset { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /*public*/
    int SectionCount { get; }

    //TExeExtReader(string fileName);
    string Section(int index);
    void GetSection(int index, Stream stream);
    string GetSectionString(int index);

}


public interface TExeExtWriter
{
    string FBaseFileName { get; set; }
    List<string> FFiles { get; set; }
    bool FValContent { get; set; }
    void SetValContent(bool value);

    bool ValContent { get; }

    void AddFile(string FileName);
    void AddString(string S);
    void Save(string FileName = " ");

}

