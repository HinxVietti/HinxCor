using HinxCor.Wins.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;

namespace ApplicationLauncher
{
    public partial class Loadout : TransparencyForm
    {
        private delegate void Vfunc();

        public Loadout()
        {
            InitializeComponent();
            CenterToScreen();
            StartRunableThread();
        }


        private void StartRunableThread()
        {
            try
            {
                string ename = "ALProfiles/execute.ini";
                string exename = string.Empty;
                List<string> args = new List<string>();
                //string filename = File.ReadAllText("ALProfiles/execute.ini");
                using (StreamReader reader = new StreamReader(ename))
                {
                    List<string> cmds = new List<string>();
                    string str;
                    while (!string.IsNullOrEmpty(str = reader.ReadLine()))
                        cmds.Add(str);

                    foreach (var cmdline in cmds)
                    {
                        var kvp = cmdline.Split(':');
                        switch (kvp[0])
                        {
                            case "-exe":
                                exename = kvp[1];
                                break;
                            case "-arg":
                                args.Add(kvp[1]);
                                break;
                            default:
                                throw new System.ArgumentException(string.Format("参数:{0}, 无效", kvp));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(exename))
                {
                    var exefile = new FileInfo(exename);
                    if (exefile.Exists)
                    {
                        ProcessStartInfo runexe = new ProcessStartInfo(exefile.FullName);
                        runexe.WorkingDirectory = exefile.Directory.FullName;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < args.Count; i++)
                        {
                            sb.Append(args[i]);
                            sb.Append(' ');
                        }
                        runexe.Arguments = sb.ToString();
                        Process p = Process.Start(runexe);
                    }
                }



            }
            catch
            {
                this.Close();
                return;
            }


            CenterToScreen();
            Display.Image = Image.FromFile("ALProfiles/loadout.png");
            this.Size = Display.Image.Size;
            //var bd = screen
            var bitmap = (Bitmap)Display.Image;
            SetBitmap(bitmap, 255);
            CenterToScreen();
            Thread thr = new Thread(Entry);
            thr.Start();
        }


        private unsafe void Entry()
        {
            string EndName = "~startok.mr";
            while (true)
            {
                Thread.Sleep(200);
                CenterToScreen();
                if (File.Exists(EndName))
                {
                    Thread.Sleep(500);
                    File.Delete(EndName);
                    break;
                }
            }

            var closef = new Vfunc(Close);
            Invoke(closef);
        }

    }
}
