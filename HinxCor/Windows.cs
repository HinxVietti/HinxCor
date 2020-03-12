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
            System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
            //System.Diagnostics.Process.Start("explorer.exe","/select,"+ (Directory.Exists(path) ? path : System.Environment.CurrentDirectory + "\\" + path));
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


        //*************************************************

        //-----------------------------------------------------------------------------
        // Copyright 2012-2018 RenderHeads Ltd.  All rights reserved.
        //-----------------------------------------------------------------------------


        public static class Utils
        {
#if Unity
            /// <summary>
            /// The "main" camera isn't necessarily the one the gets rendered last to screen,
            /// so we sort all cameras by depth and find the one with no render target
            /// </summary>
            public static Camera GetUltimateRenderCamera()
            {
                Camera result = Camera.main;

                {
                    // Iterate all enabled cameras
                    float highestDepth = float.MinValue;
                    Camera[] enabledCameras = Camera.allCameras;
                    for (int cameraIndex = 0; cameraIndex < enabledCameras.Length; cameraIndex++)
                    {
                        Camera camera = enabledCameras[cameraIndex];
                        // Ignore null cameras
                        if (camera != null)
                        {
                            // Ignore cameras that are hidden or have a targetTexture
                            bool isHidden = (camera.hideFlags & HideFlags.HideInHierarchy) == HideFlags.HideInHierarchy;
                            if (!isHidden && camera.targetTexture == null)
                            {
                                // Ignore cameras that render nothing
                                if (camera.pixelRect.width > 0f && camera.pixelHeight > 0f)
                                {
                                    // Keep the one with highest depth
                                    // TODO: handle the case where camera depths are equal - which one is first then?
                                    if (camera.depth >= highestDepth)
                                    {
                                        highestDepth = camera.depth;
                                        result = camera;
                                    }
                                }
                            }
                        }
                    }
                }

                return result;
            }



            public static bool HasContributingCameras(Camera parentCamera)
            {
                bool result = true;

                // If the camera doesn't clear the target completely then it may have other contributing cameras
                if (parentCamera.rect == new Rect(0f, 0f, 1f, 1f))
                {
                    if (parentCamera.clearFlags == CameraClearFlags.Skybox ||
                        parentCamera.clearFlags == CameraClearFlags.Color)
                    {
                        result = false;
                    }
                }

                return result;
            }

            /// <summary>
            /// Returns a list of cameras sorted in render order from first to last that contribute to the rendering to parentCamera
            /// </summary>
            public static Camera[] FindContributingCameras(Camera parentCamera)
            {
                System.Collections.Generic.List<Camera> validCameras = new System.Collections.Generic.List<Camera>(8);
                {
                    // Iterate all enabled/disabled cameras (they may become enabled later on)
                    Camera[] allCameras = (Camera[])Resources.FindObjectsOfTypeAll(typeof(Camera));
                    for (int cameraIndex = 0; cameraIndex < allCameras.Length; cameraIndex++)
                    {
                        Camera camera = allCameras[cameraIndex];
                        // Ignore null cameras and camera that is the parent
                        if (camera != null && camera != parentCamera)
                        {
                            // Only allow cameras with depth less or equal to parent camera
                            if (camera.depth <= parentCamera.depth)
                            {
                                // Ignore cameras that are hidden or have a targetTexture that doesn't match the parent
                                bool isHidden = (camera.hideFlags & HideFlags.HideInHierarchy) == HideFlags.HideInHierarchy;
                                if (!isHidden && camera.targetTexture == parentCamera.targetTexture)
                                {
                                    // Ignore cameras that render nothing
                                    if (camera.pixelRect.width > 0 && camera.pixelHeight > 0)
                                    {
                                        validCameras.Add(camera);
                                    }
                                }
                            }
                        }
                    }
                }

                if (validCameras.Count > 1)
                {
                    // Sort by depth (render order)
                    // TODO: handle the case where camera depths are equal - which one is first then?
                    validCameras.Sort(delegate (Camera a, Camera b)
                    {
                        if (a != b) // Pre .Net 4.6.2 Sort() can compare an elements with itself
                        {
                            if (a.depth < b.depth)
                            {
                                return -1;
                            }
                            else if (a.depth > b.depth)
                            {
                                return 1;
                            }
                            else if (a.depth == b.depth)
                            {
                                UnityEngine.Debug.LogWarning("[AVProMovieCapture] Cameras '" + a.name + "' and '" + b.name + "' have the same depth value - unable to determine render order: " + a.depth);
                            }
                        }
                        return 0;
                    });

                    // Starting from the last camera to render, find the first one that clears the screen completely
                    for (int i = (validCameras.Count - 1); i >= 0; i--)
                    {
                        if (validCameras[i].rect == new Rect(0f, 0f, 1f, 1f))
                        {
                            if (validCameras[i].clearFlags == CameraClearFlags.Skybox ||
                                validCameras[i].clearFlags == CameraClearFlags.Color)
                            {
                                // Remove all cameras before this
                                validCameras.RemoveRange(0, i);
                                break;
                            }
                        }
                    }
                }

                return validCameras.ToArray();
            }


            public static bool ShowInExplorer(string itemPath)
            {
                bool result = false;

#if !UNITY_WEBPLAYER
                itemPath = Path.GetFullPath(itemPath.Replace(@"/", @"\"));   // explorer doesn't like front slashes
                if (File.Exists(itemPath))
                {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
				Process.Start("explorer.exe", "/select," + itemPath);
#endif
                    result = true;
                }
                else if (Directory.Exists(itemPath))
                {
                    // NOTE: We use OpenURL() instead of the explorer process so that it opens explorer inside the folder
                    UnityEngine.Application.OpenURL(itemPath);
                    result = true;
                }

#endif

                return result;
            }

            public static bool OpenInDefaultApp(string itemPath)
            {
                bool result = false;

                itemPath = Path.GetFullPath(itemPath.Replace(@"/", @"\"));
                if (File.Exists(itemPath))
                {
                    UnityEngine.Application.OpenURL(itemPath);
                    result = true;
                }
                else if (Directory.Exists(itemPath))
                {
                    UnityEngine.Application.OpenURL(itemPath);
                    result = true;
                }

                return result;
            }
#endif

            public static long GetFileSize(string filename)
            {
#if UNITY_WEBPLAYER
			return 0;
#else
                System.IO.FileInfo fi = new System.IO.FileInfo(filename);
                return fi.Length;
#endif
            }


#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
														out ulong lpFreeBytesAvailable,
														out ulong lpTotalNumberOfBytes,
														out ulong lpTotalNumberOfFreeBytes);

		public static bool DriveFreeBytes(string folderName, out ulong freespace)
		{
			freespace = 0;
			if (string.IsNullOrEmpty(folderName))
			{
				throw new System.ArgumentNullException("folderName");
			}

			if (!folderName.EndsWith("\\"))
			{
				folderName += '\\';
			}

			ulong free = 0, dummy1 = 0, dummy2 = 0;

			if (GetDiskFreeSpaceEx(folderName, out free, out dummy1, out dummy2))
			{
				freespace = free;
				return true;
			}
			else
			{
				return false;
			}
		}
#endif
        }



        //*************************************************


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

