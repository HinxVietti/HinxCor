using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace HinxCor
{
    /// <summary>
    /// windows 辅助工具
    /// </summary>
    public static class Windows
    {
        /// <summary>
        /// dos执行batcher命令
        /// </summary>
        /// <param name="command">bat命令</param>
        public static void ExecuteCommandConsole(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");

            process.Close();
        }

        /// <summary>
        /// dos执行batcher命令
        /// </summary>
        /// <param name="command">bat命令</param>
        /// <param name="exitcode"></param>
        public static void ExecuteCommand(string command, out int exitcode)
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitcode = process.ExitCode;

            process.Close();
        }
        /// <summary>
        /// dos执行batcher命令
        /// </summary>
        /// <param name="command">bat命令</param>
        /// <param name="exif">INFO</param>
        public static void ExecuteCommand(string command, out CommandExitInfo exif)
        {
            exif = ExecuteCommand(command);
        }
        /// <summary>
        /// dos执行batcher命令
        /// </summary>
        /// <param name="command">bat命令</param>
        /// <returns>CMD INFO</returns>
        public static CommandExitInfo ExecuteCommand(string command)
        {
            var exif = new CommandExitInfo(0);

            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            exif.ExitCode = process.ExitCode;
            exif.Error = process.StandardError.ReadToEnd();
            exif.Output = process.StandardOutput.ReadToEnd();

            //Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            //Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            //Console.WriteLine("ExitCode: " + exitcode.ToString(), "ExecuteCommand");

            process.Close();
            return exif;
        }


        /// <summary>
        /// 在Windows资源管理器上打开该文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void OpenInExplorer(string path)
        {
            path = path.Replace('/', '\\');
            System.Diagnostics.Process.Start("explorer.exe", Directory.Exists(path) ? path : System.Environment.CurrentDirectory + "\\" + path);
        }
        /// <summary>
        /// 在文件夹打开
        /// </summary>
        /// <param name="path"></param>
        public static void ShowInExplorer(string path)
        {
            OpenInExplorer(path);
        }
        /// <summary>
        /// 在浏览器打开
        /// </summary>
        /// <param name="url"></param>
        public static void OpenInBrowser(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
        /// <summary>
        /// 打开保存文件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string SaveFile(string title, string fileType)
        {
            return io_cmd("OpenFile", string.Format("{0}#{1}", title, fileType));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public static string SaveFileIgnoreException(string title, string ft)
        {
            return SaveFile(title, ft).Split('#')[1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ftype"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string OpenFile(string title, string ftype)
        {
            return SaveFile(title, ftype);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ftype"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string OpenFileIgnoreException(string title, string ftype)
        {
            return OpenFile(title, ftype).Split('#')[1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="fts"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string OpenFilefIgnoreException(string title, string desc, params string[] fts)
        {
            return OpenFilef(title, desc, fts).Split('#')[1];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="fts"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="fts"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string[] OpenFilefsIgnoreException(string title, string desc, params string[] fts)
        {
            string res = OpenFilefs(title, desc, fts);
            return res.Split('#')[1].Split('~');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="fts"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string[] OpenFilesIgnoreException(string title, string ft)
        {
            return OpenFiles(title, ft).Split('#')[1].Split('~');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string OpenFiles(string title, string fileType)
        {
            return io_cmd("OpenFileS", string.Format("{0}#{1}", title, fileType));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
        public static string SelectFolder()
        {
            return io_cmd("SelectFolder");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("try another way please", true)]
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

        private static bool io_cmd(string cmd, out string path, string io = null)
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
            path = result;
            return !string.IsNullOrEmpty(path);
        }
    }
}

