using System;
using System.IO;
using System.Threading;
using HinxCor.Compression.net45;

namespace HinxCor.Compiler
{
    /// <summary>
    /// 编译辅助
    /// </summary>
    public class CompilerHelper
    {
        /// <summary>
        /// 打包加密MHX数据
        /// </summary>
        /// <param name="args"></param>
        public static void Build_MHX_DATA(string[] args)
        {
            string passwd = "F41E6-F565-41F1F-C1DR5-6QW";
            var ZipFile = "data.mhx";
            ZipHelper.CompressFilesAndFolder(args, ZipFile, ZipHelper.CompressionLevel.Deflated, passwd);
        }

        /// <summary>
        /// 异步加密打包 MHX 包,传入Files
        /// </summary>
        /// <param name="files">文件+文件夹</param>
        /// <returns></returns>
        public static AsyncOperate ASyncBuild_MHX_DATA(string[] files, Action<Exception> OnError = null)
        {

            var ao = new AsyncOperator();
            AsyncOperate op = new AsyncOperate(ao);
            op.Start();
            try
            {

                Action<string> sync_log = log => { ao.log = log; };
                Action<float> sync_process = log => { ao.process = log; };
                Action<bool> sync_state = log => { ao.isdone = log; };

                string passwd = "F41E6-F565-41F1F-C1DR5-6QW";
                var ZipFile = "data.mhx";
                new Thread(() =>
                {
                    ZipHelper.ASyncCompressFilesAndFolder(sync_process, sync_log, sync_state, OnError, files, ZipFile, 5, passwd);
                })
                {
                    IsBackground = true,
                    Name = "_async_build_mhx_data"
                }.Start();
            }
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(e);
                //op.isDone = true;
                ao.log = e.ToString();
                ao.isdone = true;
                if (File.Exists("data.mhx"))
                    File.Delete("data.mhx");
            }
            return op;
        }


    }
}

