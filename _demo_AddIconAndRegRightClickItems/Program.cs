
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileUploadAssistant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var isExist = File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload_to_haolizi.txt"));
            //xp/win2000/win2003
            if (Environment.OSVersion.Version.Major < 6|| isExist)
            {
                //增加右键菜单
                new SystemMenuHelper().AddFileContextMenuItem("upload_to_haolizi", Application.ExecutablePath + " \"%1\" \"AddToFastLink\"", "上传到好例子网", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favicon.ico"));
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload_to_haolizi.txt"),"success");
                Application.Run(new Form1(args));
                return;
            }
            //var j = 0;
            //var i = 100/j;
            try
            {
                //下为: Vista/win7/win8/win10 on up

                /** 
             * 当前用户是管理员的时候，直接启动应用程序 
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行 
             */
                //获得当前登录的Windows用户标示  
                System.Security.Principal.WindowsIdentity identity =
                    System.Security.Principal.WindowsIdentity.GetCurrent();
                //创建Windows用户主题  
                Application.EnableVisualStyles();

                System.Security.Principal.WindowsPrincipal principal =
                    new System.Security.Principal.WindowsPrincipal(identity);
                //判断当前登录用户是否为管理员  
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //增加右键菜单
                    new SystemMenuHelper().AddFileContextMenuItem("upload_to_haolizi", Application.ExecutablePath + " \"%1\" \"AddToFastLink\"", "上传到好例子网", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favicon.ico"));
                    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload_to_haolizi.txt"), "success");
                    //如果是管理员，则直接运行  
                    Application.Run(new Form1(args));
                }
                else
                {
                    //创建启动对象  
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //设置运行文件  
                    startInfo.FileName = Application.ExecutablePath;
                    //设置启动参数  
                    startInfo.Arguments = String.Join(" ", args);
                    //设置启动动作,确保以管理员身份运行  
                    startInfo.Verb = "runas";
                    //如果不是管理员，则启动UAC  
                    System.Diagnostics.Process.Start(startInfo);
                    //退出  
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception start_ex)
            {
                MessageBox.Show(start_ex.Message);
            }



            
        }
    }
}
