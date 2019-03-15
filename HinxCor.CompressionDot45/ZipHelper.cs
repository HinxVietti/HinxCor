using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HinxCor.Compression.net45
{
    /// <summary>
    /// 
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public enum CompressionLevel
        {
            ///
            /// 摘要:
            ///     A direct copy of the file contents is held in the archive
            Stored = 0,
            ///
            /// 摘要:
            ///     Common Zip compression method using a sliding dictionary of up to 32KB and secondary
            ///     compression from Huffman/Shannon-Fano trees
            Deflated = 8,
            ///
            /// 摘要:
            ///     An extension to deflate with a 64KB window. Not supported by #Zip currently
            Deflate64 = 9,
            ///
            /// 摘要:
            ///     BZip2 compression. Not supported by #Zip.
            BZip2 = 11,
            ///
            /// 摘要:
            ///     WinZip special for AES encryption, Now supported by #Zip.
            WinZipAES = 99

        }

        /// <summary>
        /// 异步创建压缩文件
        /// </summary>
        /// <param name="sync_process"></param>
        /// <param name="sync_log"></param>
        /// <param name="sync_state"></param>
        /// <param name="onError"></param>
        /// <param name="files">传入的文件夹内容或者文件内容</param>
        /// <param name="zipFile"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="passwd"></param>
        /// <param name="comment"></param>
        public static void ASyncCompressFilesAndFolder(Action<float> sync_process, Action<string> sync_log, Action<bool> sync_state, Action<Exception> onError, string[] files, string zipFile, int compressionLevel = 5, string passwd = "", string comment = "runable achive file.. By.HinxCor.EncrytoPass")
        {
            sync_process(0);
            sync_state(false);
            sync_log("开始查找文件");

            var dict = new Dictionary<string, string>(); // key 是 entry名字 value 是 file全名

            Action<DirectoryInfo, int> CollectDirFiles = null;
            //将所有dir作为root entry 收集起来
            Action<DirectoryInfo> CollectDirFilesAsRoot = rootdirinfo =>
            {
                var trim = rootdirinfo.FullName.Length - rootdirinfo.Name.Length;
                CollectDirFiles(rootdirinfo, trim);
            };

            CollectDirFiles = (dir, trim) =>
            {
                var dirs = dir.GetDirectories();
                var ffs = dir.GetFiles();
                for (int i = 0; i < dirs.Length; i++)
                    CollectDirFiles(dirs[i], trim);
                for (int i = 0; i < ffs.Length; i++)
                    dict.Add(ffs[i].FullName.Remove(0, trim), ffs[i].FullName);
            };

            List<string> toc_files = new List<string>(); //收集到的所有文件
            List<string> toc_dirs = new List<string>();  //收集到的所有文件夹

            //files 分拣 - file or dir
            sync_process(0.05f);
            sync_log("开始分拣");
            foreach (var its in files)
            {
                if (Directory.Exists(its)) toc_dirs.Add(its);
                else if (File.Exists(its)) toc_files.Add(its);
                else
                {
                    onError(new Exception(its + " is not an file or folder.."));
                    return;
                }
            }
            sync_log("分拣完成");
            sync_process(0.1f);
            sync_log("开始收集所有文件");

            //步骤计数器
            int index = 0;

            foreach (var ffile in toc_files)
            {
                var f = new FileInfo(ffile);
                dict.Add(f.Name, f.FullName);
            }

            foreach (var dir in toc_dirs)
            {
                index++;
                CollectDirFilesAsRoot(new DirectoryInfo(dir));
                sync_process(0.1f + 0.1f * index / toc_dirs.Count);// 0.1+0.1(max) = 0.2(max) 
            }

            //FastZip

            //如果要输出的文件已经存在了就删除它
            if (File.Exists(zipFile)) File.Delete(zipFile);

            //收集所有大小
            sync_log("开始计算文件大小");
            long size = 0;
            foreach (var f in dict)
                using (var fs = new FileStream(f.Value, FileMode.Open))
                {
                    size += fs.Length;
                }
            sync_process(0.3f);
            sync_log("计算完成,准备打包数据");

            long writeSize = 0;
            int buffSize = 2048;
            int readSize = 0;
            byte[] buffer = new byte[buffSize];
            using (ZipOutputStream outstr = new ZipOutputStream(new FileStream(zipFile, FileMode.CreateNew)))
            {
                outstr.Password = passwd;
                //ZipEntryFactory factory = new ZipEntryFactory();
                outstr.SetLevel(compressionLevel);
                //k entry name v full name
                foreach (var kvp in dict)
                {
                    //var entry = factory.MakeFileEntry(kvp.Value);
                    var entry = new ZipEntry(kvp.Key);
                    outstr.PutNextEntry(entry);
                    sync_log("packing:" + kvp.Value);
                    //写入data
                    using (FileStream fs = new FileStream(kvp.Value, FileMode.Open))
                    {
                        do
                        {
                            readSize = fs.Read(buffer, 0, buffSize);// 0 是buffer的offset
                            outstr.Write(buffer, 0, readSize);
                            writeSize += readSize;
                            sync_process(0.3f + 0.7f * writeSize / size);
                        } while (readSize > 0);
                    }
                }
            }
            sync_state(true);
            sync_process(1);
            sync_log("finished..success.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sync_p"></param>
        /// <param name="sync_log"></param>
        /// <param name="sync_state"></param>
        /// <param name="args"></param>
        /// <param name="zipFile"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="passwd"></param>
        /// <param name="comment"></param>
        [Obsolete("参数不达标,弃用", true)]
        public static void ASyncCompressFilesAndFolder(Action<float> sync_p, Action<string> sync_log, Action<bool> sync_state, string[] args, string zipFile, CompressionLevel compressionLevel = CompressionLevel.Stored, string passwd = "", string comment = "runable achive file.. By.HinxCor.EncrytoPass")
        {
            sync_p(0);
            sync_state(false);
            sync_log("开始转换文件");

            Dictionary<string, string> dict = new Dictionary<string, string>();

            Action<DirectoryInfo, int> CollectDirFiles = null;
            Action<DirectoryInfo> CollectDirFilesAsRoot = rootdirinfo =>
            {
                var trim = rootdirinfo.FullName.Length - rootdirinfo.Name.Length;
                CollectDirFiles(rootdirinfo, trim);
            };

            CollectDirFiles = (dir, trim) =>
            {
                var dirs = dir.GetDirectories();
                var files = dir.GetFiles();
                for (int i = 0; i < dirs.Length; i++)
                    CollectDirFiles(dirs[i], trim);
                for (int i = 0; i < files.Length; i++)
                    dict.Add(files[i].FullName.Remove(0, trim), files[i].FullName);
            };

            //分拣
            string logname = "extracting.log";
            StringBuilder log;
            List<string> ffs = new List<string>();
            List<string> fdirs = new List<string>();
            for (int i = 0; i < args.Length; i++)
                if (File.Exists(args[i])) ffs.Add(args[i]);
                else if (Directory.Exists(args[i])) fdirs.Add(args[i]);
                else
                {
                    string nlr = string.Format("File {0} was not regular file or folder, add to pack fail.", args[i]);
                    if (File.Exists(logname))
                    {
                        log = new StringBuilder(File.ReadAllText(logname));
                        log.AppendLine(nlr);
                        File.WriteAllText(logname, log.ToString());
                    }
                    else File.WriteAllText(logname, nlr);
                }



            foreach (var ffile in ffs)
            {
                FileInfo f = new FileInfo(ffile);
                dict.Add(f.Name, f.FullName);
            }

            foreach (var dir in fdirs)
                CollectDirFilesAsRoot(new DirectoryInfo(dir));

            sync_p(0.2f);
            sync_log("分拣完成");
            ZipFile zip = ZipFile.Create(zipFile);
            if (!string.IsNullOrEmpty(passwd))
                zip.Password = passwd;

            zip.BeginUpdate();
            zip.SetComment(comment);
            zip.CommitUpdate();

            var clevel = getfromLevel(compressionLevel);
            int index = 0;
            foreach (var item in dict)
            {
                zip.BeginUpdate();
                index++;
                sync_p(0.2f + index * 0.8f / dict.Count);
                sync_log("add.." + item.Value);
                try
                {
                    zip.Add(new private_static_res(item.Value), item.Key, clevel);
                    //zip.CommitUpdate();
                }
                catch (Exception e)
                {
                    string nlr = "ERROR IN COMPRESSION: " + e;
                    if (File.Exists(logname))
                    {
                        log = new StringBuilder(File.ReadAllText(logname));
                        log.AppendLine(nlr);
                        File.WriteAllText(logname, log.ToString());
                    }
                    else File.WriteAllText(logname, nlr);
                }
                zip.CommitUpdate();
            }
            zip.Close();
            //}
            //sync_log("END+ERROR:" + e.ToString());
            //sync_state(true);
            //sync(LogClip.Finished(, 2));

            sync_log("finished.");
            sync_state(true);
            sync_p(1);
            //sync(LogClip.Finished());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="zipFile"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="passwd"></param>
        /// <param name="comment"></param>
        public static void CompressFilesAndFolder(string[] args, string zipFile, CompressionLevel compressionLevel = CompressionLevel.Stored, string passwd = "", string comment = "runable achive file.. By.HinxCor.EncrytoPass")
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();

            Action<DirectoryInfo, int> CollectDirFiles = null;
            Action<DirectoryInfo> CollectDirFilesAsRoot = rootdirinfo =>
            {
                var trim = rootdirinfo.FullName.Length - rootdirinfo.Name.Length;
                CollectDirFiles(rootdirinfo, trim);
            };

            CollectDirFiles = (dir, trim) =>
            {
                var dirs = dir.GetDirectories();
                var files = dir.GetFiles();
                for (int i = 0; i < dirs.Length; i++)
                    CollectDirFiles(dirs[i], trim);
                for (int i = 0; i < files.Length; i++)
                    dict.Add(files[i].FullName.Remove(0, trim), files[i].FullName);
            };

            //分拣
            string logname = "extracting.log";
            StringBuilder log;
            List<string> ffs = new List<string>();
            List<string> fdirs = new List<string>();
            for (int i = 0; i < args.Length; i++)
                if (File.Exists(args[i])) ffs.Add(args[i]);
                else if (Directory.Exists(args[i])) fdirs.Add(args[i]);
                else
                {
                    string nlr = string.Format("File {0} was not regular file or folder, add to pack fail.", args[i]);
                    if (File.Exists(logname))
                    {
                        log = new StringBuilder(File.ReadAllText(logname));
                        log.AppendLine(nlr);
                        File.WriteAllText(logname, log.ToString());
                    }
                    else File.WriteAllText(logname, nlr);
                }

            foreach (var ffile in ffs)
            {
                FileInfo f = new FileInfo(ffile);
                dict.Add(f.Name, f.FullName);
            }
            foreach (var dir in fdirs)
                CollectDirFilesAsRoot(new DirectoryInfo(dir));

            ZipFile zip = ZipFile.Create(zipFile);
            if (!string.IsNullOrEmpty(passwd))
                zip.Password = passwd;
            zip.BeginUpdate();
            zip.SetComment(comment);

            var clevel = getfromLevel(compressionLevel);

            foreach (var item in dict)
            {
                try
                {
                    zip.Add(new private_static_res(item.Value), item.Key, clevel);
                    //zip.CommitUpdate();
                }
                catch (Exception e)
                {
                    string nlr = "ERROR IN COMPRESSION: " + e;
                    if (File.Exists(logname))
                    {
                        log = new StringBuilder(File.ReadAllText(logname));
                        log.AppendLine(nlr);
                        File.WriteAllText(logname, log.ToString());
                    }
                    else File.WriteAllText(logname, nlr);
                }

            }
            zip.CommitUpdate();
            zip.Close();
        }

        private static int getlvfromLevel(CompressionLevel level)
        {
            switch (level)
            {
                case CompressionLevel.Stored:
                    return 5;
                case CompressionLevel.Deflated:
                    return 2;
                case CompressionLevel.Deflate64:
                    return 6;
                case CompressionLevel.BZip2:
                    return 7;
                case CompressionLevel.WinZipAES:
                    return 9;
            }
            return 5;
        }
        private static CompressionMethod getfromLevel(CompressionLevel level)
        {
            switch (level)
            {
                case CompressionLevel.Stored:
                    return CompressionMethod.Stored;
                case CompressionLevel.Deflated:
                    return CompressionMethod.Deflated;
                case CompressionLevel.Deflate64:
                    return CompressionMethod.Deflate64;
                case CompressionLevel.BZip2:
                    return CompressionMethod.BZip2;
                case CompressionLevel.WinZipAES:
                    return CompressionMethod.WinZipAES;
            }
            throw new Exception(level + " could not convert to " + nameof(CompressionMethod));
        }

        /// <summary>
        /// 静态文件资源
        /// </summary>
        private class private_static_res : IStaticDataSource
        {
            private string pth;
            public private_static_res(string path)
            {
                pth = path;
            }
            public Stream GetSource()
            {
                return new FileStream(pth, FileMode.Open);
            }
        }

        /// <summary>
        /// 把目录下的所有文件打包
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="zipPath"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="password"></param>
        /// <param name="fileFilter"></param>
        /// <returns></returns>
        public static bool CompressFolder(string folderName, string zipPath, int compressionLevel, string password = "", string fileFilter = "")
        {
            //clamp compress level;
            compressionLevel = compressionLevel > 9 ? 9 : compressionLevel;
            compressionLevel = compressionLevel < 0 ? 0 : compressionLevel;

            bool result = false;

            if (Directory.Exists(folderName) == false)
                throw new NullReferenceException("找不到目标文件夹,请检查后再试:" + folderName);
            try
            {
                //string[] files = Directory.GetFiles(folderName);
                List<FileInfo> files = new List<FileInfo>();

                Action<DirectoryInfo> fileCollect = null;
                DirectoryInfo root = new DirectoryInfo(folderName);

                fileCollect = dir =>
                {
                    var folders = dir.GetDirectories();
                    if (string.IsNullOrEmpty(fileFilter) == false && string.IsNullOrWhiteSpace(fileFilter) == false)
                        files.AddRange(dir.GetFiles(fileFilter));
                    else
                        files.AddRange(dir.GetFiles());
                    for (int i = 0; i < folders.Length; i++)
                        fileCollect(folders[i]);
                };

                // add all files to files;
                fileCollect(root);
                int trimlength = root.FullName.Length + 1;

                using (ZipOutputStream zip = new ZipOutputStream(File.Create(zipPath)))
                {
                    if (string.IsNullOrEmpty(password) == false && string.IsNullOrWhiteSpace(password) == false)
                        zip.Password = password;
                    zip.SetLevel(compressionLevel);

                    byte[] buffer = new byte[4096]; //4K buffer
                    foreach (var file in files)
                    {
                        string entryName = file.FullName.Remove(0, trimlength);
                        ZipEntry entry = new ZipEntry(entryName);
                        entry.DateTime = file.LastWriteTimeUtc;
                        zip.PutNextEntry(entry);
                        using (FileStream fs = file.Open(FileMode.Open))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                zip.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    result = true;
                    zip.Flush();
                    zip.Finish();
                    zip.Close();
                }

            }
            catch (Exception e)
            {
                //result = false;
                throw e;
            }
            return result;
        }

        /// <summary>
        /// 解压到文件夹
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destinateDir"></param>
        /// <param name="password"></param>
        public static void UnCompressionFile(string fileName, string destinateDir = "", string password = "")
        {
            FileInfo zipFile = new FileInfo(fileName);
            //if(!string.IsNullOrEmpty(password))zipFile.pa
            if (zipFile.Exists == false) throw new FileNotFoundException("找不到压缩文件:" + zipFile.FullName);

            ZipFile file = new ZipFile(fileName);
            string estName = string.IsNullOrEmpty(destinateDir) ? "ExtractData\\" : destinateDir;
            if (estName.EndsWith("\\") == false) estName += "\\";

            if (!string.IsNullOrEmpty(password))
                file.Password = password;

            foreach (ZipEntry entery in file)
            {
                if (entery != null)
                {
                    string fname = estName + entery.Name;
                    FileInfo f = new FileInfo(fname);
                    if (!f.Directory.Exists) f.Directory.Create();
                    var fs = File.Create(fname);

                    var stream = file.GetInputStream(entery);
                    stream.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                }
            }
        }

    }


}



//Obsolute Codes;
//public static void Demo()
//{
//    OpenFileDialog open = new OpenFileDialog();
//    //open.Multiselect = true;
//    open.Filter = "d|*.zip";
//    open.ShowDialog();

//    //UnCompressionFile(open.FileName, "解压测试");

//    //SaveFileDialog save = new SaveFileDialog();
//    //save.Filter = "压缩文件|*.zip";
//    //save.ShowDialog();
//    //ZipFile zip = ZipFile.Create(save.FileName);
//    //zip.Password = "123";
//    //zip.BeginUpdate();
//    //var files = open.FileNames;
//    //foreach (var file in files)
//    //    zip.Add(file);
//    //zip.CommitUpdate();
//    //zip.Close();

//    //FolderBrowserDialog gf = new FolderBrowserDialog();
//    //gf.RootFolder = Environment.SpecialFolder.MyComputer;
//    //gf.ShowDialog();
//    //CompressFolder(gf.SelectedPath, save.FileName, 5);

//    //ZipFile file = new ZipFile(open.FileName);
//    //string estName = "ext\\";

//    ////var stream = file.GetInputStream();

//    ////int BufferSize = 4096;

//    //foreach (ZipEntry entery in file)
//    //{
//    //    if (entery != null)
//    //    {
//    //        string fname = estName + entery.Name;
//    //        FileInfo f = new FileInfo(fname);
//    //        if (!f.Directory.Exists) f.Directory.Create();
//    //        var fs = File.Create(fname);

//    //        var stream = file.GetInputStream(entery);
//    //        stream.CopyTo(fs);
//    //        //var buffer = stream;
//    //        //fs.Write(buffer, 0, buffer.Length);
//    //        fs.Flush();
//    //        fs.Close();
//    //    }
//    //}

//}


//public static string[] UnZipFile(string zipFilePath, string rootFolder = "")
//{
//    //string rootFolder = VisionHelper.CachePath;
//    if (!File.Exists(zipFilePath))
//        throw new NullReferenceException("zip 文件未找到");

//    List<string> args = new List<string>();

//    using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
//    {

//        ZipEntry theEntry;
//        while ((theEntry = s.GetNextEntry()) != null)
//        {

//            string directoryName = Path.GetDirectoryName(theEntry.Name);
//            string fileName = Path.GetFileName(theEntry.Name);
//            // create directory
//            if (directoryName.Length > 0)
//                Directory.CreateDirectory(directoryName);

//            if (fileName != string.Empty)
//            {
//                string filename = rootFolder + theEntry.Name;
//                args.Add(filename);
//                FileInfo finfo = new FileInfo(filename);
//                if (finfo.Directory.Exists == false) finfo.Directory.Create();
//                //
//                using (FileStream streamWriter = File.Create(filename))
//                {
//                    int size = 2048;
//                    byte[] data = new byte[2048];
//                    while (true)
//                    {
//                        size = s.Read(data, 0, data.Length);
//                        if (size > 0)
//                            streamWriter.Write(data, 0, size);
//                        else
//                            break;
//                    }
//                    streamWriter.Flush();
//                }
//            }
//        }
//    }
//    return args.ToArray();
//}

