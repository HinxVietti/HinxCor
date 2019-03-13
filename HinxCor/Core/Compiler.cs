using HinxCor.Security;
using System;
using System.IO;
using System.Text;
using System.Threading;


namespace HinxCor
{
    /// <summary>
    /// require Windows dotNet 4.0
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// dotNet 4.0 路劲
        /// </summary>
        public const string cscEnv = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";

        internal const string crcKey = "HinxCor.EncrytoPass";
        /// <summary>
        /// CSC CS FILE NAME
        /// </summary>
        public const string csFileName = "code.depub";

        /// <summary>
        /// 尝试编译
        /// </summary>
        /// <param name="outName"></param>
        /// <param name="key"></param>
        /// <param name="csFile"></param>
        public static void TryComplire(string outName, string key = crcKey, string csFile = csFileName)
        {
            var data = File.ReadAllBytes(csFile);//加密了的数据
            string csname = "code.depub.decode";

            using (var rc4 = new RC4(Encoding.UTF8.GetBytes(key)))
            {
                string cedetail = rc4.Decrypt(data);
                File.WriteAllText(csname, cedetail);

                if (EnvironmentTools.AddPath(cscEnv) == false) throw new Exception("系统不知错,错误 -05");

                string batcmd = string.Format(@"csc -out:{0} {1} -win32icon:icon.ico -resource:{2} -resource:{3} -resource:{4} -resource:{5}"
        , outName, csname, "DCARE.exe", "HinxCor.CompressionDot45.dll", "ICSharpCode.SharpZipLib.dll", "data.mhx");
                Windows.ExecuteCommand(batcmd);
                File.Delete(csname);
                File.Delete("data.mhx");
            }
        }

        /// <summary>
        /// 自解压程序; dCare 需要 dll1,dll2,data,mhx
        /// 需要 DCARE.exe; HinxCor.CompressionDot45.dll; ICSharpCode.SharpZipLib.dll; data.mhx;
        /// require Windows dotNet 4.0
        /// </summary>
        /// <param name="outName"></param>
        /// <param name="key"></param>
        /// <param name="csFile"></param>
        public static AsyncOperate AsyncTryComplire(string outName, string key = crcKey, string csFile = csFileName)
        {
            var ao = new AsyncOperator();
            AsyncOperate op = new AsyncOperate(ao);
            op.Start();
            //proc =;
            new Thread(() =>
            {
                ao.process = 0.01f;
                ao.log = "准备中";
                ao.hlog = new FormerLog(FormerLog.LogType.Log, ao.log);
                var data = File.ReadAllBytes(csFile);//加密了的数据
                ao.process = 0.08f;
                ao.log = "加载加密码";
                string csname = "code.depub.decode";

                using (var rc4 = new RC4(Encoding.UTF8.GetBytes(key)))
                {
                    ao.process = 0.09f;
                    ao.log = "准备解密";
                    string cedetail = rc4.Decrypt(data);
                    ao.process = 0.15f;
                    ao.log = "写入钥文";
                    File.WriteAllText(csname, cedetail);

                    ao.process = 0.20f;
                    ao.log = "环境监测";
                    if (Directory.Exists(cscEnv) == false)
                    {
                        ao.process = 0.23f;
                        ao.hlog = new FormerLog(FormerLog.LogType.Error, "需要.net 4.0");
                        return;
                    }
                    if (!EnvironmentTools.AddPath(cscEnv))
                    {
                        ao.process = 0.33f;
                        ao.hlog = new FormerLog(FormerLog.LogType.Warning, "无法正确加入环境变量");
                        //return;
                    }
                    ao.process = 0.30f;
                    ao.log = "准备OK";
                    Thread.Sleep(100);
                    ao.process = 0.40f;

                    ao.log = "编译中";
                    string batcmd = string.Format(@"csc -out:{0} {1} -win32icon:icon.ico -resource:{2} -resource:{3} -resource:{4} -resource:{5}"
            , outName, csname, "DCARE.exe", "HinxCor.CompressionDot45.dll", "ICSharpCode.SharpZipLib.dll", "data.mhx");
                    ao.process = 0.50f;
                    ao.log = "编译中.";
                    ao.log = "编译中..";
                    ao.log = "编译中...";
                    Windows.ExecuteCommand(batcmd);
                    ao.log = "编译结束";
                    ao.process = 0.80f;

                    ao.log = "清理文件";
                    File.Delete(csname);
                    ao.log = "清理文件";
                    ao.process = 0.90f;
                    File.Delete("data.mhx");
                    ao.process = 1f;
                    ao.log = "完成:无异常";
                }
            })
            {
                IsBackground = true,
                Name = "_complair"
            }.Start();
            return op;
        }

        /// <summary>
        /// 自解压程序; dCare 需要 dll1,dll2,data,mhx
        /// 需要 DCARE.exe; HinxCor.CompressionDot45.dll; ICSharpCode.SharpZipLib.dll; data.mhx;
        /// require Windows dotNet 4.0
        /// </summary>
        /// <param name="outName">输出路径</param>
        /// <param name="resFiles">资源文件列表</param>
        /// <param name="icopth">ICO文件路径</param>
        /// <param name="key">解密秘钥(可默认)</param>
        /// <param name="csFile">加密的CS文件路劲</param>
        public static AsyncOperate AsyncTryComplire(string outName, string[] resFiles, string icopth, string key = crcKey, string csFile = csFileName)
        {
            var ao = new AsyncOperator();
            AsyncOperate op = new AsyncOperate(ao);
            op.Start();
            //proc =;
            new Thread(() =>
            {
                ao.process = 0.01f;
                ao.log = "准备中";
                ao.hlog = new FormerLog(FormerLog.LogType.Log, ao.log);
                var data = File.ReadAllBytes(csFile);//加密了的数据
                ao.process = 0.08f;
                ao.log = "加载加密码";
                string csname = "code.depub.decode";

                using (var rc4 = new RC4(Encoding.UTF8.GetBytes(key)))
                {
                    try
                    {

                        ao.process = 0.09f;
                        ao.log = "准备解密";
                        string cedetail = rc4.Decrypt(data);
                        ao.process = 0.15f;
                        ao.log = "写入钥文";
                        File.WriteAllText(csname, cedetail);

                        ao.process = 0.20f;
                        ao.log = "环境监测";
                        if (Directory.Exists(cscEnv) == false)
                        {
                            ao.process = 0.23f;
                            ao.hlog = new FormerLog(FormerLog.LogType.Error, "需要.net 4.0");
                            return;
                        }
                        if (!EnvironmentTools.AddPath(cscEnv))
                        {
                            ao.process = 0.33f;
                            ao.hlog = new FormerLog(FormerLog.LogType.Warning, "无法正确加入环境变量");
                            //return;
                        }
                        ao.process = 0.30f;
                        ao.log = "准备OK";
                        Thread.Sleep(100);
                        ao.process = 0.40f;

                        ao.log = "编译中";
                        StringBuilder sb = new StringBuilder();
                        sb.Append("csc");
                        sb.Append(' ');
                        sb.Append("-out:");
                        sb.Append(outName);
                        sb.Append(' ');
                        sb.Append(csname);
                        sb.Append(' ');
                        sb.Append("-win32icon:");
                        sb.Append(icopth);
                        sb.Append(' ');
                        for (int i = 0; i < resFiles.Length; i++)
                        {
                            sb.Append("-resource:");
                            sb.Append(resFiles[i]);
                            sb.Append(' ');
                        }

                        //        string batcmd = string.Format(@"csc -out:{0} {1} -win32icon:icon.ico -resource:{2} -resource:{3} -resource:{4} -resource:{5}"
                        //, outName, csname, "DCARE.exe", "HinxCor.CompressionDot45.dll", "ICSharpCode.SharpZipLib.dll", "data.mhx");
                        ao.process = 0.50f;
                        ao.log = "编译中.";
                        ao.log = "编译中..";
                        ao.log = "编译中...";
                        ao.hlog = new FormerLog(FormerLog.LogType.Error, ao.log);
                        Windows.ExecuteCommand(sb.ToString());
                        ao.log = "编译结束";
                        ao.process = 0.80f;
                        ao.hlog = new FormerLog(FormerLog.LogType.Error, ao.log);

                        ao.log = "清理文件";
                        File.Delete(csname);
                        ao.log = "清理文件";
                        ao.process = 0.90f;
                        File.Delete("data.mhx");
                        ao.process = 1f;
                        ao.log = "完成:无异常";
                        ao.isdone = true;
                    }
                    catch (Exception e)
                    {
                        ao.log = "失败:" + e.ToString();
                        ao.hlog = new FormerLog(FormerLog.LogType.Error, ao.log);
                        ao.isdone = true;
                    }
                }
            })
            {
                IsBackground = true,
                Name = "_complair"
            }.Start();
            return op;
        }
    }
}
