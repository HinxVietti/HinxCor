using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

//namespace Vision.Update
//{
//    class Program
//    {
//        const string host = "http://files.focusky.com.cn/vision3d/";
//        public const string updateJson = "update.json";
//        static readonly string main_json_file_sync_time = "_last_remote_update_time";

//        public static bool LocalVerify { get; private set; } = false;

//        public static void Main()
//        {
//            Action<string> log = Console.WriteLine;
//            var dir_Data = new DirectoryInfo("Data");
//            var dir_Download = new DirectoryInfo("Download");
//            if (!dir_Data.Exists) dir_Data.Create();
//            if (!dir_Download.Exists) dir_Download.Create();

//            log("ready handle update");
//            DownloadRemoteAssets(log);
//            log("download assets finished. going to veryfy local assets.");
//            UpdateLocalFiles(log, dir_Data, dir_Download);
//            log("Queue finished");
//            LocalVerify = true;

//            while (true)
//            {
//                Thread.Sleep(5000);//5s 校验一次本地文件
//                UpdateLocalFiles(log, dir_Data, dir_Download);
//            }
//        }



//        static int DownloadRemoteAssets(Action<string> log)
//        {
//            //download remote files.
//            bool mainJsonFinish = false;

//            while (!DownloadHelper.DownloadJsonAsync(host + updateJson, mfjson =>
//            {
//                //success downloaded json is mf json.
//                var mf = LitJson.JsonMapper.ToObject<jsonFiles>(mfjson);

//                if (SQLDAO.TryReadconfig(main_json_file_sync_time, out string _last_update_time_str))
//                {
//                    if (DateTime.TryParse(_last_update_time_str, out var t_local))
//                    {
//                        if ((t_local.Equals(mf.UpdateTime)))
//                        {
//                            log("文件已经是最新的 了，不需要另外更新");
//                            mainJsonFinish = true;
//                            return;
//                        }
//                    }
//                }

//                SQLDAO.WriteConfig(main_json_file_sync_time, mf.UpdateTime.ToString());

//                log("update local databese");


//                //里面有若干的Json',都是新的(服务器)文件列表
//                foreach (var jsonf in mf.Jsons)
//                {
//                    if (DownloadHelper.DownloadJsonAsync(host + jsonf, json =>
//                    {
//#if UNITY_EDITOR
//                     File.WriteAllText(jsonf, json);
//#endif
//                        //大Json列表
//                        log("downloaded jhost json:" + jsonf + "##");
//                        RemoteResourceData RRD = LitJson.JsonMapper.ToObject<RemoteResourceData>(json);
//                        float index = 0;


//                        foreach (var bundleitem in RRD.Items)
//                        {
//                            index++;
//                            try
//                            {
//                                log("handle:" + bundleitem.Name);

//                                var bi = bundleitem;
//                                string filter = string.Format(" where {0} = '{1}' ", "name", bi.Name);//name请指定唯一, 无论任何操作
//                                var query = string.Format("select * from {0} {1}", "ManagedPack", filter);
//                                using (SQLiteConnection conn = SQLDAO.GetConnection())
//                                {
//                                    conn.Open();
//                                    using (var cmd = new SQLiteCommand(query, conn))
//                                    {
//                                        using (var reader = cmd.ExecuteReader())
//                                        {
//                                            if (reader.Read())
//                                                query = string.Format("update ManagedPack set sha_1='{0}',version = {1},tags= '{2}', access = {3},type={4},thumbnail='{5}',append='{6}' {7}",
//                                                    bi.SHA_1, bi.Version, bi.Tags, bi.Access, bi.Type, bi.Thumbnail, bi.Append, filter);
//                                            else
//                                                query = string.Format("insert into ManagedPack(name,sha_1,request_url,version,type,tags,access,uid,thumbnail,append)" +
//                                                    " values('{0}','{1}','{2}',{3},{4},'{5}',{6},'{7}','{8}','{9}') "
//                                                    , bi.Name, bi.SHA_1, bi.RequestURL, bi.Version, bi.Type, bi.Tags, bi.Access, bi.uid, bi.Thumbnail, bi.Append);
//                                            reader.Close();
//                                        }
//                                        cmd.CommandText = query;
//                                        cmd.ExecuteNonQuery();
//                                    }

//                                    conn.Close();
//                                }
//                            }
//                            catch (Exception e)
//                            {
//                                log(e.ToString());
//                            }
//                        }

//                        log("update download jsons finished.");
//                    }))
//                    {

//                    }
//                    else
//                    {
//                        log("Unable download json:" + jsonf);
//                    }
//                }

//                mainJsonFinish = true;

//            }))
//            {
//                log("Downloader helper not ready , wait 5s");
//                Thread.Sleep(5000);//未能下载服务器的Json列表,5s后重试
//            }

//            while (mainJsonFinish == false)
//            {
//                log("wait download finished , wait 5s");

//                Thread.Sleep(500);
//            }

//            using (var conn = SQLDAO.GetConnection())
//            {
//                conn.Open();
//                using (var cmd = new SQLiteCommand())
//                {
//                    cmd.Connection = conn;
//                    cmd.CommandText = "select request_url from skybox";//所有的远程skybox
//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            //string name = reader.GetString(0);
//                            string uri = reader.GetString(0);
//                            uri = Regex.Replace(uri, ".vskf", ".pskinf");

//                            //远程拥有或者本地可用
//                            if (LocalResourceManager.FileAvailable(uri, out string localDir))
//                            {
//                                if (LocalResourceManager.LocalAvailable(uri, out localDir)) continue;
//                                bool downloadFinished = false;

//                                log(string.Format("start download from remotr {0}", localDir));
//                                RemoteResourceManager.DownloadToLocalAsync(uri, dir =>
//                                {
//                                    if (File.Exists(dir) == false) log(string.Format("检查到服务器可用,但是无法下载:{0}", uri));
//                                    else log(string.Format("success check --{0}", uri));
//                                    downloadFinished = true;
//                                });
//                                while (!downloadFinished)
//                                    Thread.Sleep(20);
//                            }
//                            else
//                            {
//                                log(string.Format("<color=orange>absolute file {0}</color>", uri));
//                            }

//                        }
//                    }
//                }
//                conn.Close();
//            }

//            //下载所有的pprl
//            using (var conn = SQLDAO.GetConnection())
//            {
//                conn.Open();
//                using (var cmd = new SQLiteCommand())
//                {
//                    cmd.CommandText = "select request_url from pprl";//bundle preview dats
//                    cmd.Connection = conn;
//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            string uri = reader.GetString(0);
//                            if (LocalResourceManager.LocalAvailable(uri, out string _dir)) continue;
//                            else if (LocalResourceManager.RemoteAvailable(uri, out _dir))
//                            {
//                                //本地没有服务器有, 下载他
//                                bool downloadFinished = false;

//                                RemoteResourceManager.DownloadToLocalAsync(uri, dir =>
//                                {
//                                    if (File.Exists(dir) == false) log(string.Format("检查到服务器可用,但是无法下载:{0}", uri));
//                                    else log(string.Format("success download from server --{0}", uri));
//                                    downloadFinished = true;
//                                });

//                                while (!downloadFinished)
//                                    Thread.Sleep(20);
//                            }
//                            else
//                            {
//                                log(string.Format("<color=orange>absolute file {0}</color>", uri));
//                            }
//                        }
//                    }
//                }
//                conn.Close();
//            }

//            return 0;
//        }

//        static List<string> UpdatedFiles = new List<string>();

//        /// <summary>
//        /// 刷新检测本地文件, 同时更新数据库
//        /// </summary>
//        /// <param name="log"></param>
//        /// <param name="dir_Data"></param>
//        /// <param name="dir_Download"></param>
//        private static void UpdateLocalFiles(Action<string> log, DirectoryInfo dir_Data, DirectoryInfo dir_Download)
//        {
//            using (var conn = SQLDAO.GetConnection())
//            {
//                conn.Open();
//                using (var cmd = new SQLiteCommand(conn))
//                {
//                    cmd.CommandText = string.Format("select path from localbundlefiles");
//                    List<string> toRemoveFiles = new List<string>();
//                    using (var reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            string path = reader.GetString(0);
//                            if (!File.Exists(path))
//                            {
//                                toRemoveFiles.Add(path);
//                            }
//                        }
//                    }
//                    int count = 0;
//                    foreach (var filepath in toRemoveFiles)
//                    {
//                        log("清理:" + filepath);
//                        cmd.CommandText = string.Format("delete from localbundlefiles where path = '{0}'", filepath);
//                        count += cmd.ExecuteNonQuery();
//                    }
//                    if (count > 0)
//                        log(string.Format("remove dont exist file {0}", count));
//                }
//                conn.Close();
//            }

//            var files = new List<FileInfo>();
//            files.AddRange(dir_Data.GetFiles("*", SearchOption.AllDirectories));
//            files.AddRange(dir_Download.GetFiles("*", SearchOption.AllDirectories));

//            files.RemoveAll(file => UpdatedFiles.Contains(file.FullName));

//            if (files.Count > 0)
//                log(string.Format("Has success find file {0}/{1}", files.Count, UpdatedFiles.Count));

//            using (var conn = SQLDAO.GetConnection())
//            {
//                conn.Open();
//                using (var cmd = new SQLiteCommand(conn))
//                {
//                    int count = 0;
//                    foreach (var file in files)
//                    {
//                        if (file.Length == 0)
//                        {
//                            //log("Found empty file and delete,");
//                            file.Delete();//可能是在下载中的
//                            continue;
//                        }
//                        string name = file.Name;
//                        string path = file.FullName;
//                        long size = file.Length;
//                        //string sha_1 = RRPD_Tools.GetSHA_1(path);
//                        string sha_1 = string.Empty;

//                        //查找数据库,如果有就更新,没有就添加
//                        string query = string.Format("select size,sha_1 from localbundlefiles where name = '{0}'", name);
//                        cmd.CommandText = query;
//                        bool exist = false;
//                        bool hasNew = false;
//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                var nsize = reader.GetInt64(0);
//                                if (nsize == size)
//                                {
//                                    hasNew = false;
//                                    sha_1 = reader.GetString(1);
//                                }
//                                else
//                                {
//                                    hasNew = true;
//                                    sha_1 = RRPD_Tools.GetSHA_1(path);
//                                }
//                                exist = true;
//                            }
//                            else sha_1 = RRPD_Tools.GetSHA_1(path);
//                        }
//                        if (hasNew || !exist)//内容刷新了,或者不存在
//                        {
//                            if (exist)
//                            {
//                                query = string.Format("update localbundlefiles values('{0}','{1}',{2},'{3}') where name = '{4}'", name, path, size, sha_1, name);
//                            }
//                            else
//                            {
//                                query = string.Format("insert into localbundlefiles values('{0}','{1}',{2},'{3}')", name, path, size, sha_1);
//                            }
//                            try
//                            {
//                                log("更新:" + path);
//                                cmd.CommandText = query;
//                                count += cmd.ExecuteNonQuery();
//                            }
//                            catch (SQLiteException e)
//                            {
//                                log(string.Format("Unable excite query:<color=red> {0} \n{1}</color>", query, e));
//                            }

//                        }

//                        UpdatedFiles.Add(file.FullName);
//                    }
//                    if (count > 0)
//                    {
//                        log(string.Format("Update/Insert {0} items", count));
//                        log("Finished update local files info. at " + System.DateTime.Now.ToString());
//                    }
//                }
//                conn.Close();

//            }
//        }


//    }
//}
