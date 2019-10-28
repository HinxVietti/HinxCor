using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Config
{
    public ConfigFile this[string fileName]
    {
        get
        {
            CheckKey(fileName);
            return data[fileName];
        }
        set
        {
            CheckKey(fileName);
            data[fileName] = value;
        }
    }

    private void CheckKey(string fileName)
    {
        if (data.ContainsKey(fileName) == false) data.Add(fileName, new ConfigFile(fileName));
    }

    Dictionary<string, ConfigFile> data;

    public Config()
    {
        data = new Dictionary<string, ConfigFile>();
    }

    /// <summary>
    /// 将所有配置属性写入本地
    /// </summary>
    public void Flush()
    {
        foreach (var configFile in data.Values)
        {
            configFile.Flush();
        }
    }

    /// <summary>
    /// 将所有配置异步属性写入本地
    /// </summary>
    public void AsyncFlush(Action OnFinished = null)
    {
        Task.Run(() =>
        {
            Flush();
            OnFinished?.Invoke();
        });
    }
}


public class ConfigValue
{
    object value;


    public ConfigValue(object obj)
    {
        value = obj;
    }

    public void Set(object val)
    {
        this.value = val;
    }

    public int ToInt() => int.TryParse(this, out int res) ? res : 0;
    public float ToSingle() => float.TryParse(this, out float res) ? res : 0f;
    public double ToDouble() => double.TryParse(this, out double res) ? res : 0d;

    public override string ToString()
    {
        return this;
    }

    public static implicit operator string(ConfigValue value)
    {
        return value.value.ToString();
    }

}

public class ConfigSection
{
    private readonly IniFile iniFile;
    private readonly string sectionName;

    public ConfigValue this[string propertyName]
    {
        get
        {
            CheckKey(propertyName);
            return data[propertyName];
        }
        set
        {
            CheckKey(propertyName);
            data[propertyName] = value;
        }
    }

    Dictionary<string, ConfigValue> data;

    private void CheckKey(string keyname)
    {
        if (data.ContainsKey(keyname) == false)
        {
            data.Add(keyname, new ConfigValue(0));
            if (iniFile.KeyExists(keyname, sectionName))
                data[keyname].Set(iniFile.Read(keyname, sectionName));
            else
                iniFile.Write(keyname, 0, sectionName);
        }
    }

    public ConfigSection(IniFile iniFile, string propertyName)
    {
        this.iniFile = iniFile;
        this.sectionName = propertyName;
        data = new Dictionary<string, ConfigValue>();
    }

    internal void Flush()
    {
        foreach (var kvp in data)
            iniFile.Write(kvp.Key, kvp.Value.ToString(), sectionName);
    }
}


public class ConfigFile
{
    public const string ConfigFileExtension = @".cfg";
    /// <summary>
    /// ***请勿获取此项
    /// </summary>
    private readonly string configFileName;
    private readonly IniFile iniFile;
    public string ConfigFileName => configFileName + ConfigFileExtension;

    public ConfigSection this[string sectionName]
    {
        get
        {
            CheckKey(sectionName);
            return data[sectionName];
        }
        set
        {
            CheckKey(sectionName);
            data[sectionName] = value;
        }
    }

    private void CheckKey(string propertyName)
    {
        if (data.ContainsKey(propertyName) == false) data.Add(propertyName, new ConfigSection(iniFile, propertyName));
    }

    Dictionary<string, ConfigSection> data;

    public ConfigFile(string fileName)
    {
        configFileName = fileName;
        iniFile = new IniFile(ConfigFileName);
        data = new Dictionary<string, ConfigSection>();
    }

    /// <summary>
    /// 将当前配置的属性写入本地
    /// </summary>
    public void Flush()
    {
        foreach (var section in data.Values)
            section.Flush();
    }

}



