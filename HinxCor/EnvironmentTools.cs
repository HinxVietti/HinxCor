using System;
using System.Collections.Generic;

namespace HinxCor
{
    /// <summary>
    /// 系统环境工具
    /// </summary>
    public class EnvironmentTools
    {
        const string @Path = "PATH";

        /// <summary>
        /// 查询是否存在环境变量( Path项)
        /// </summary>
        /// <param name="pathName">路径名</param>
        /// <returns></returns>
        public static bool HasPath(string pathName)
        {
            string pathvar = Environment.GetEnvironmentVariable(Path);
            var pths = pathvar.Split(';');
            bool hasF = false;
            for (int i = 0; i < pths.Length; i++)
                hasF = hasF || pths[i] == pathName;
            return hasF;
        }

        /// <summary>
        /// 尝试添加路径
        /// </summary>
        /// <param name="pathName">路径名</param>
        /// <param name="ignoreException">是否忽略异常</param>
        /// <returns>true:已存在,成功添加,false:添加失败或者,或者异常</returns>
        public static bool AddPath(string pathName, bool ignoreException = false)
        {
            if (HasPath(pathName)) return true;
            try
            {
                string pathvar = Environment.GetEnvironmentVariable(Path);
                var value = pathvar + @";" + pathName;
                var target = EnvironmentVariableTarget.Machine;
                Environment.SetEnvironmentVariable(Path, value, target);
                return true;
            }
            catch (Exception e)
            {
                if (ignoreException) return false;
                throw e;
            }

        }
    }

}
