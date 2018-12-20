using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace HinxCor.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class Debug
    {
        public enum LogLevel
        {
            /// <summary>
            /// 全部记录
            /// </summary>
            All,
            /// <summary>
            /// 包含警告，资源，错误，异常
            /// </summary>
            UpWarning,
            /// <summary>
            /// 包含资源，错误，异常
            /// </summary>
            UpAssert,
            /// <summary>
            /// 包含错误，异常
            /// </summary>
            UpError,
            /// <summary>
            /// 包含异常
            /// </summary>
            UpException,
            /// <summary>
            /// 仅有错误
            /// </summary>
            OnlyError,
            /// <summary>
            /// 仅有异常
            /// </summary>
            OnlyException,
            /// <summary>
            /// 异常和错误
            /// </summary>
            ErrorAndException,
            /// <summary>
            /// 仅有普通日志
            /// </summary>
            OnlyLog
        }

        public static LogLevel logLevel;
        public static bool WriteLog { get; set; }

        private static string logName;
        private static StreamWriter stream;
        private static bool isinit = false;
        private static string logPath = "log";
        private static string logPattern = "log_";
        private static string logFormat = ".log";

        static Debug()
        {
            //Init(false, LogLevel.All);
        }

        public static void Init(bool writelog, LogLevel level)
        {
            if (isinit == true) return;
            isinit = true;
            logName = logPath + "/" + logPattern + Now().Replace(':', '-') + logFormat;

            FileInfo finfo = new FileInfo(logName);
            if (finfo.Directory.Exists == false)
                finfo.Directory.Create();

            //DirectoryInfo dir = new DirectoryInfo(logPath);
            //if (!dir.Exists) dir.Create();
            //if (File.Exists(logName) == false)
            //{
            //    var st = File.Create(logName);
            //    st.Flush();
            //    st.Close();
            //}
            logLevel = level;
            Application.logMessageReceived += HandleLogMsg;
            WriteLog = writelog;
        }

        private static void HandleLogMsg(string condition, string stackTrace, LogType type)
        {
            //System.Windows.Forms.MessageBox.Show(WriteLog + "rev:" + stackTrace + "\n" + condition, type.ToString());
            if (!WriteLog) return;
            switch (type)
            {
                case LogType.Error:
                    HandleError(condition, stackTrace);
                    break;
                case LogType.Assert:
                    HandleAssert(condition, stackTrace);
                    break;
                case LogType.Warning:
                    HandleWarning(condition, stackTrace);
                    break;
                case LogType.Log:
                    HandleLog(condition, stackTrace);
                    break;
                case LogType.Exception:
                    HandleException(condition, stackTrace);
                    break;
            }
        }

        private static void HandleException(string condition, string stackTrace)
        {
            if (logLevel == LogLevel.OnlyLog) return;
            if (logLevel == LogLevel.OnlyError) return;
            Write("ERROR", condition, stackTrace);
        }

        private static void HandleLog(string condition, string stackTrace)
        {
            if (logLevel == LogLevel.OnlyLog || logLevel == LogLevel.All)
                Write("Log", condition, stackTrace);
        }

        private static void HandleWarning(string condition, string stackTrace)
        {

            if (logLevel == LogLevel.UpWarning || logLevel == LogLevel.All)
                Write("WARNING", condition, stackTrace);
        }

        private static void HandleAssert(string condition, string stackTrace)
        {
            if (logLevel == LogLevel.All || logLevel == LogLevel.UpWarning || logLevel == LogLevel.UpAssert)
                Write("ERROR", condition, stackTrace);
        }

        private static void HandleError(string condition, string stackTrace)
        {
            if (logLevel == LogLevel.OnlyException) return;
            if (logLevel == LogLevel.OnlyLog) return;
            if (logLevel == LogLevel.UpException) return;
            Write("ERROR", condition, stackTrace);
        }

        private static void Write(string logName, string condition, string stackTrace)
        {
            using (stream = new StreamWriter(Debug.logName, true))
            {
                stream.WriteLine(Now() + "--> " + logName + ":");
                stream.WriteLine("\t" + condition);
                string res = Regex.Replace(stackTrace, "\n", "\n\t");
                stream.WriteLine("\t" + res);
                stream.Flush();
            }
        }

        static private string Now()
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        }

    }
}

