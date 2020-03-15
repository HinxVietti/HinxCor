using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Video.ConvertT
{

    class Program
    {
        static string videoFilter = "视频文件|*.avi;*.mov;*.rmvb;*.rm;*.flv;*.mp4;*.3gp";

        [STAThread]
        static void Main(string[] args)
        {
            if (args != null)
            {
                if (args.Length > 0)
                {
                    if (!ContinueWith(args[0]))
                        return;
                    foreach (var videoFile in args)
                        ConvertVideoFile(videoFile);
                }
                else
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = videoFilter;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        ConvertVideoFile(dlg.FileName);
                    }
                }

            }

            Console.WriteLine();
            Console.WriteLine("Video Convert Finished. Any key esc.");
            Console.ReadKey();
        }

        /// <summary>
        /// 处理第一个治理
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool ContinueWith(string v)
        {
            switch (v)
            {
                case "-v":
                    OutPrintVersionOfVideoConverter();
                    return false;
                default:
                    return true;
            }
        }

        private static void OutPrintVersionOfVideoConverter()
        {
            Console.WriteLine("This version of video converter is 1.0 bate.");
            Console.WriteLine("Auther is Hinx Viett. @ hinxvietti@gmail.com");
            Console.WriteLine();
        }

        static string res = "1920*1080";


        //AVI、mov、rmvb、rm、FLV、mp4、3GP
        private static void ConvertVideoFile(string videoFile)
        {
            if (!File.Exists(videoFile)) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = videoFilter;
            dlg.Title = string.Format("保存{0}的转换结果", new FileInfo(videoFile).Name);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string SaveName = dlg.FileName;
                if (File.Exists(SaveName))
                {
                    switch (MessageBox.Show("目标文件已经存在，删除？\n（是）删除（覆盖）文件；（否）备份文件\n（取消）不做任何操作!", caption: "文件已存在", buttons: MessageBoxButtons.YesNoCancel))
                    {
                        case DialogResult.Yes:
                            File.Delete(SaveName);
                            break;
                        case DialogResult.No:
                            MoveFileToBak(SaveName);
                            break;
                        //case DialogResult.Cancel:
                        default:
                            break;

                    }
                }
                string cmdt = "ffmpeg";
                string cmd = string.Format("-i \"{0}\" -s {1} \"{2}\"", videoFile, res, SaveName);
                Console.WriteLine(cmd);
                var startInfo = new ProcessStartInfo()
                {
                    FileName = cmdt,
                    Arguments = cmd,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
                var process = Process.Start(startInfo);
                new Thread(() =>
                {
                    string line = string.Empty;
                    while ((line = process.StandardError.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                })
                {
                    IsBackground = true
                }.Start();
                new Thread(() =>
                {
                    string line = string.Empty;
                    while ((line = process.StandardOutput.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                })
                {
                    IsBackground = true
                }.Start();
                //Process.Start(cmdt, cmd);
            }
        }

        private static void MoveFileToBak(string saveName)
        {
            if (!File.Exists(saveName + ".bak"))
            {
                File.Move(saveName, saveName + ".bak");
                return;
            }
            for (int i = 1; i < 9999; i++)
            {
                string sname = string.Format("{0}_{1}.bak", saveName, i);
                if (File.Exists(sname) == false)
                {
                    File.Move(saveName, sname);
                    return;
                }
            }
            File.Delete(saveName + ".bak");
            File.Move(saveName, saveName + ".bak");
        }
    }
}
