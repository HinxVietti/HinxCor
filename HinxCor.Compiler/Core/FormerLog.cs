using System;
using System.Collections.Generic;
using System.Text;

namespace HinxCor
{
    /// <summary>
    /// 格式化日志
    /// </summary>
    public class FormerLog
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// normal log
            /// </summary>
            Log,
            /// <summary>
            /// some wrong not so important.
            /// </summary>
            Warning,
            /// <summary>
            /// knowable error
            /// </summary>
            Error,
            /// <summary>
            /// Special System Exceptions
            /// </summary>
            SystemException,
        }

        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// CONSTRUCT
        /// </summary>
        /// <param name="t"></param>
        /// <param name="s"></param>
        public FormerLog(LogType t, string s)
        {
            this.Type = t;
            AppendLine(s);
        }

        /// <summary>
        /// CONSTRUCT
        /// </summary>
        public FormerLog()
        {
            this.Type = LogType.Log;
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType Type { get; set; }
        /// <summary>
        /// SB
        /// </summary>
        public StringBuilder Loger { get { return sb; } protected set { sb = value; } }
        /// <summary>
        /// Log
        /// </summary>
        public string Log { get { return Loger.ToString(); } }

        /// <summary>
        /// 追加内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public StringBuilder Append(string message)
        {
            return Loger.Append(message);
        }
        /// <summary>
        /// 追加字符
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public StringBuilder Append(char message)
        {
            return Loger.Append(message);
        }
        /// <summary>
        /// 追加行
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public StringBuilder AppendLine(string message)
        {
            return Loger.AppendLine(message);
        }
        /// <summary>
        /// 追加格式日志
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            return Loger.AppendFormat(provider, format, args);
        }
        /// <summary>
        /// 追加格式文本
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return Loger.AppendFormat(format, args);
        }

    }

}
