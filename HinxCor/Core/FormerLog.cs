using System;
using System.Collections.Generic;
using System.Text;

namespace HinxCor
{

    public class FormerLog
    {
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

        public FormerLog(LogType t, string s)
        {
            this.Type = t;
            AppendLine(s);
        }

        public FormerLog()
        {
            this.Type = LogType.Log;
        }

        public LogType Type { get; set; }
        public StringBuilder Loger { get { return sb; } protected set { sb = value; } }

        public string Log { get { return Loger.ToString(); } }

        public StringBuilder Append(string message)
        {
            return Loger.Append(message);
        }
        public StringBuilder Append(char message)
        {
            return Loger.Append(message);
        }
        public StringBuilder AppendLine(string message)
        {
            return Loger.AppendLine(message);
        }
        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            return Loger.AppendFormat(provider, format, args);
        }
        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return Loger.AppendFormat(format, args);
        }

    }

}
