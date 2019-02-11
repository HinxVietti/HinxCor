using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HinxCor.Compression.net45
{
    public class ZipHelper
    {

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



        ///// <summary>
        ///// 多个文件打包
        ///// </summary>
        ///// <param name="files"></param>
        ///// <param name="zipFilePath"></param>
        ///// <returns></returns>
        //public static bool CompressZip(string[] files, string zipFilePath)
        //{
        //    bool success = false;
        //    try
        //    {
        //        using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
        //        {
        //            s.SetLevel(AppConfig.CompressedLevel); // 压缩级别 0-9
        //            byte[] buffer = new byte[4096]; //缓冲区大小
        //            foreach (string file in files)
        //            {
        //                ZipEntry entry = new ZipEntry(Path.GetFileName(file));
        //                entry.DateTime = System.DateTime.Now;
        //                s.PutNextEntry(entry);
        //                using (FileStream fs = File.OpenRead(file))
        //                {
        //                    int sourceBytes;
        //                    do
        //                    {
        //                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
        //                        s.Write(buffer, 0, sourceBytes);
        //                    } while (sourceBytes > 0);
        //                }
        //            }
        //            success = true;
        //            s.Finish();
        //            s.Close();
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        success = false;
        //        Debug.Log("Exception during processing " + ex);
        //    }
        //    return success;
        //}



        public static void UnCompressionFile(string fileName, string destinateDir, string password = "")
        {
            FileInfo zipFile = new FileInfo(fileName);
            if (zipFile.Exists == false) throw new FileNotFoundException("找不到压缩文件:" + zipFile.FullName);

            using (ZipInputStream zip = new ZipInputStream(zipFile.Open(FileMode.Open)))
            {
                if (!string.IsNullOrEmpty(password))
                    zip.Password = password;

                ZipEntry entery;
                ZipEntryFactory factory;
                //ZipFile
                //Action UnZipFac

                while ((entery = zip.GetNextEntry()) != null)
                {

                }

            }

        }


        public static void Demo()
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Multiselect = true;
            open.Filter = "d|*.zip";
            open.ShowDialog();

            //SaveFileDialog save = new SaveFileDialog();
            //save.Filter = "压缩文件|*.zip";
            //save.ShowDialog();
            ////ZipFile zip = ZipFile.Create(save.FileName);
            ////zip.Password = "123";
            ////zip.BeginUpdate();

            ////var files = open.FileNames;

            ////foreach (var file in files)
            ////    zip.Add(file);

            ////zip.CommitUpdate();
            ////zip.Close();

            //FolderBrowserDialog gf = new FolderBrowserDialog();
            //gf.RootFolder = Environment.SpecialFolder.MyComputer;
            //gf.ShowDialog();
            //CompressFolder(gf.SelectedPath, save.FileName, 5);

            ZipFile file = new ZipFile(open.FileName);
            string estName = "ext\\";
            foreach (ZipEntry entery in file)
            {
                if (entery != null)
                {
                    string fname = estName + entery.Name;
                    FileInfo f = new FileInfo(fname);
                    if (!f.Directory.Exists) f.Directory.Create();
                    var fs = File.Create(fname);
                    var buffer = entery.ExtraData;
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                    fs.Close();
                }
            }

        }


        //public static string[] UnZipFile(string zipFilePath, string rootFolder = "")
        //{
        //    //string rootFolder = VisionHelper.CachePath;
        //    if (!File.Exists(zipFilePath))
        //    {
        //        Debug.LogWarning("Cannot find file " + zipFilePath);
        //        return null;
        //    }

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
    }
}

