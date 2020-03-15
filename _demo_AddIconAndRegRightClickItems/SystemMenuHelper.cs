using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileUploadAssistant
{
    /// <summary>
    /// 系统菜单帮助类
    /// </summary>
    public class SystemMenuHelper
    {
        /// <summary>
        /// 增加文件右键菜单
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="associatedProgramFullPath"></param>
        public void AddFileContextMenuItem(string itemName, string associatedProgramFullPath, string menuDesciption, string iconPath = "")
        {
            try
            {
                //创建项：shell 
                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                if (shellKey == null)
                {
                    shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                }

                //创建项：右键显示的菜单名称
                RegistryKey rightCommondKey = shellKey.CreateSubKey(itemName);
                rightCommondKey.SetValue("", menuDesciption);
                if (!string.IsNullOrEmpty(iconPath)) rightCommondKey.SetValue("Icon", iconPath, RegistryValueKind.ExpandString);
                RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

                //创建默认值：关联的程序
                associatedProgramKey.SetValue(string.Empty, associatedProgramFullPath);

                //刷新到磁盘并释放资源
                associatedProgramKey.Close();
                rightCommondKey.Close();
                shellKey.Close();
            }
            catch { }
        }
        /// <summary>
        /// 是否存在此快捷键
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool ExitsFileContextMenuItem(string itemName)
        {
            try
            {
                //创建项：shell 
                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                if (shellKey == null) return false;

                //创建项：右键显示的菜单名称
                RegistryKey rightCommondKey = shellKey.OpenSubKey(itemName);
                var isExists = rightCommondKey != null;
                rightCommondKey.Close();
                shellKey.Close();
                return isExists;
            }
            catch {
                return false;
            }

        }

        /// <summary>
        /// 增加文件夹右键菜单
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="associatedProgramFullPath"></param>
        public void AddDirectoryContextMenuItem(string itemName, string associatedProgramFullPath, string menuDesciption)
        {
            //创建项：shell 
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"directory\shell", true);
            if (shellKey == null)
            {
                shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
            }

            //创建项：右键显示的菜单名称
            RegistryKey rightCommondKey = shellKey.CreateSubKey(itemName);
            rightCommondKey.SetValue("", menuDesciption);
            RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

            //创建默认值：关联的程序
            associatedProgramKey.SetValue("", associatedProgramFullPath);


            //刷新到磁盘并释放资源
            associatedProgramKey.Close();
            rightCommondKey.Close();
            shellKey.Close();
        }
        /// <summary>
        /// 删除文件右键菜单
        /// </summary>
        /// <param name="itemName"></param>
        public void DelFileContextMenuItem(string itemName)
        {
            //创建项：shell 
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
            if (shellKey != null)
            {
                shellKey.DeleteSubKeyTree(itemName);
            }
            shellKey.Close();
        }
        /// <summary>
        /// 删除文件夹右键菜单
        /// </summary>
        /// <param name="itemName"></param>
        public void DelDirectoryContextMenuItem(string itemName)
        {
            //创建项：shell 
            RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"directory\shell", true);
            if (shellKey != null)
            {
                shellKey.DeleteSubKeyTree(itemName);
            }
            shellKey.Close();
        }

    }
}
