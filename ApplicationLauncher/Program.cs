﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ApplicationLauncher
{
    static class Program
    {


        [STAThread]
        static void Main(string[] args)
        {
            string logName = "app_launcher_args.err.log";
            string configName = "startinf.ali";
            if (args != null && args.Length > 0)
            {
                var CMD1 = args[0];
                switch (CMD1)
                {
                    case "admin":
                        return;
                    default:
                        break;
                }
                File.AppendAllLines(logName, args);
            }
            else
            {
                string[] cmd = File.ReadAllLines(configName);
                if (File.Exists(configName) == false)
                {
                    File.AppendAllText(logName, string.Format("\n{0}:{1}{2}", DateTime.Now, "file not found ", configName));
                    return;
                }
                if (!(cmd?.Length > 0))
                {
                    File.AppendAllText(logName, string.Format("\n{0}:{1}{2}", DateTime.Now, "could not get cmd from ", configName));
                    return;
                }
                if (File.Exists(cmd[0]))
                {
                    File.AppendAllText(logName, string.Format("\n{0}:{1} , {2}", DateTime.Now, "File Not Exist, ", cmd[0]));
                    return;
                }
                try
                {
                    ProcessStartInfo startinfo = new ProcessStartInfo();
                    startinfo.FileName = cmd[0];
                    startinfo.Arguments = GetArgument(cmd);
                    Process.Start(startinfo);
                }
                catch (Exception e)
                {
                    File.AppendAllText(logName, string.Format("\n{0}:{1} , {2}", DateTime.Now, "start cmd error while, ", e));
                }
            }

        }

        private static string GetArgument(string[] cmd)
        {
            string str = string.Empty;
            for (int i = 1; i < cmd.Length; i++)
            {
                str += string.Format("\"{0}\" ", cmd[i]);
            }
            return str.Remove(str.Length - 1);
        }

        [STAThread]
        static void Main1()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "Focusky Vision 2019.exe";
            Process.Start(info);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Loadout());
            //Application.Run(new HybridTransparentBorder());
        }
    }
}
