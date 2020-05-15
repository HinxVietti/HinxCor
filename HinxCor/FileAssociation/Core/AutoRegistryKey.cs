using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public class AutoRegistryKey
{
    public RegistryKey Key => m_reference;
    public IList<string> Subkeys => m_subKeys;


    private RegistryKey m_reference;
    private List<string> m_subKeys;

    public AutoRegistryKey(RegistryKey key)
    {
        this.m_reference = key;
        m_subKeys = new List<string>();
        m_subKeys.AddRange(key.GetSubKeyNames());
    }


    public void Flush()
    {
        Key.Flush();
        m_subKeys.Clear();
        m_subKeys.AddRange(Key.GetSubKeyNames());
    }

    public AutoRegistryKey OpenSubKey(string name)
    {
        return GetSubKey(name);
    }
    public AutoRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
    {
        return GetSubKey(name, permissionCheck);
    }
    public AutoRegistryKey OpenSubKey(string name, bool writable)
    {
        return GetSubKey(name, writable);
    }

    public AutoRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
    {
        return GetSubKey(name, permissionCheck, rights);
    }

    public AutoRegistryKey CreateSubKey(string name)
    {
        return GetSubKey(name);
    }

    public AutoRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    {
        return GetSubKey(subkey, permissionCheck, options);
    }

    public AutoRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
    {
        return GetSubKey(subkey, permissionCheck);
    }


    private AutoRegistryKey GetSubKey(string keyName, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
    {
        RegistryKey key = null;
        m_subKeys.Contains(keyName);
        if (m_subKeys.Contains(keyName))
            key = this.m_reference.OpenSubKey(keyName, permissionCheck);
        else
            key = this.m_reference.CreateSubKey(keyName, permissionCheck, options);
        return new AutoRegistryKey(key);
    }

    private AutoRegistryKey GetSubKey(string keyName, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
    {
        RegistryKey key = null;
        m_subKeys.Contains(keyName);
        if (m_subKeys.Contains(keyName))
            key = this.m_reference.OpenSubKey(keyName, permissionCheck, rights);
        else
            key = this.m_reference.CreateSubKey(keyName, permissionCheck);
        return new AutoRegistryKey(key);
    }

    private AutoRegistryKey GetSubKey(string keyName, RegistryKeyPermissionCheck permissionCheck)
    {
        RegistryKey key = null;
        m_subKeys.Contains(keyName);
        if (m_subKeys.Contains(keyName))
            key = this.m_reference.OpenSubKey(keyName, permissionCheck);
        else
            key = this.m_reference.CreateSubKey(keyName, permissionCheck);
        return new AutoRegistryKey(key);
    }

    private AutoRegistryKey GetSubKey(string keyName, bool writeable)
    {
        RegistryKey key = null;
        m_subKeys.Contains(keyName);
        if (m_subKeys.Contains(keyName))
            key = this.m_reference.OpenSubKey(keyName, writeable);
        else
            key = this.m_reference.CreateSubKey(keyName);

        return new AutoRegistryKey(key);
    }

    private AutoRegistryKey GetSubKey(string keyName)
    {
        RegistryKey key = null;
        m_subKeys.Contains(keyName);
        if (m_subKeys.Contains(keyName))
            key = this.m_reference.OpenSubKey(keyName);
        else
            key = this.m_reference.CreateSubKey(keyName);
        return new AutoRegistryKey(key);
    }

    

    #region Static methods

    public static readonly AutoRegistryKey CurrentUser = Registry.CurrentUser;
    public static readonly AutoRegistryKey LocalMachine = Registry.LocalMachine;
    public static readonly AutoRegistryKey ClassesRoot = Registry.ClassesRoot;
    public static readonly AutoRegistryKey Users = Registry.Users;
    public static readonly AutoRegistryKey PerformanceData = Registry.PerformanceData;
    public static readonly AutoRegistryKey CurrentConfig = Registry.CurrentConfig;
    public static readonly AutoRegistryKey DynData = Registry.DynData;


    public static AutoRegistryKey GetFileType(string type)
    {
        if (!type.StartsWith("."))
            type = "." + type;

        return ClassesRoot.GetSubKey(type);
    }

    public static implicit operator AutoRegistryKey(RegistryKey key)
    {
        if (key != null)
            return new AutoRegistryKey(key);
        return null;
    }

    #endregion

}
