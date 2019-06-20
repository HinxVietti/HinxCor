using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class StringFile : ICollection<string>, IEnumerator<string>
{
    public const short _TypeINT_Length = 4;
    public const string _SPLIT_STRING = @"&#Eaa;-R#";


    public List<string> FileNames { get; set; }
    public Dictionary<string, byte[]> FileDatas { get; private set; }
    private Dictionary<string, string> FileStringDatas { get; set; }

    public int Count { get; private set; }

    public string this[string key] => FileStringDatas.ContainsKey(key) ? FileStringDatas[key] : Convert.ToBase64String(FileDatas[key]);
    public byte[] this[int key] => FileDatas[FileNames[key]];

    public bool IsReadOnly => false;

    public string Current => this[FileNames[_ienumerator_index]];
    private int _ienumerator_index;
    object IEnumerator.Current => Current;

    public StringFile()
    {
        //TableEnd = 4;
        FileNames = new List<string>();
        FileDatas = new Dictionary<string, byte[]>();
        FileStringDatas = new Dictionary<string, string>();
        Count = 0;
        _ienumerator_index = 0;
    }

    /// <summary>
    /// Add Plain will not generate byte array
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="plain"></param>
    public StringFile(string txt, bool plain = false)
    {
        FileNames = new List<string>();
        FileDatas = new Dictionary<string, byte[]>();
        FileStringDatas = new Dictionary<string, string>();
        Count = 0;
        _ienumerator_index = 0;

        if (string.IsNullOrEmpty(txt))
            return;

        var args = Regex.Split(txt, System.Environment.NewLine);
        for (int i = 0; i < args.Length; i++)
        {
            var kvp = Regex.Split(args[i], _SPLIT_STRING);
            if (kvp.Length == 2)
                if (plain)
                    AddPlain(kvp[0], kvp[1]);
                else
                    Add(kvp[0], kvp[1]);
        }
    }

    /// <summary>
    /// 仅仅添加外部b64数据
    /// </summary>
    /// <param name="item"></param>
    /// <param name="b64data"></param>
    public void AddPlain(string item, string b64data)
    {
        if (FileNames.Contains(item) == false)
        {
            Count++;
            FileNames.Add(item);
            FileDatas.Add(item, null);
        }
        if (FileStringDatas.ContainsKey(item) == false)
            FileStringDatas.Add(item, b64data);
        else
            FileStringDatas[item] = (b64data);
    }

    public void Add(string item)
    {
        if (getData(item, out byte[] data))
        {
            if (FileNames.Contains(item) == false)
            {
                Count++;
                FileNames.Add(item);
                FileDatas.Add(item, null);
            }
            FileDatas[item] = data;
            //renewTableEnd();
        }
    }



    public void Add(string item, byte[] data)
    {
        if (FileNames.Contains(item) == false)
        {
            Count++;
            FileNames.Add(item);
            FileDatas.Add(item, null);
        }
        FileDatas[item] = data;
    }

    public void Add(string item, string b64data)
    {
        if (FileNames.Contains(item) == false)
        {
            Count++;
            FileNames.Add(item);
            FileDatas.Add(item, null);
        }
        FileDatas[item] = Convert.FromBase64String(b64data);
    }


    public bool Remove(string item)
    {
        if (FileNames.Contains(item))
        {
            var dat = FileDatas[item];
            var res = FileNames.Remove(item) & FileDatas.Remove(item);

            if (res)
                Count--;
            else
            {
                if (!FileNames.Contains(item)) FileNames.Add(item);
                if (!FileDatas.ContainsKey(item)) FileDatas.Add(item, dat);
            }
            return res;
        }
        return false;
    }


    private bool getData(string item, out byte[] data)
    {
        if (File.Exists(item))
        {
            data = File.ReadAllBytes(item);
            return true;
        }
        data = null;
        return false;
        //throw new NotImplementedException();
    }

    public void Clear()
    {
        //TableEnd = 4;
        FileNames = new List<string>();
        FileDatas = new Dictionary<string, byte[]>();
        FileStringDatas = new Dictionary<string, string>();
        Count = 0;
        _ienumerator_index = 0;
    }

    public bool Contains(string item) => FileNames.Contains(item);

    public void CopyTo(string[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
            array[i + arrayIndex] = FileNames[i];
    }

    public IEnumerator<string> GetEnumerator()
    {
        return this;
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    public void Dispose()
    {
        Clear();
    }

    public bool MoveNext()
    {
        _ienumerator_index++;
        return _ienumerator_index < FileNames.Count;
    }

    public void Reset()
    {
        _ienumerator_index = 0;
    }

    /// <summary>
    /// 将数据变为B64的KVP
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < FileNames.Count; i++)
        {
            string message = FileNames[i] + _SPLIT_STRING + this[FileNames[i]];
            if (i != FileNames.Count - 1)
                sb.AppendLine(message);
            else sb.Append(message);
        }
        return sb.ToString();
    }
}

