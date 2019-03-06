using HinxCor.Wins.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ApplicationLauncher
{
    public partial class Loadout : TransparencyForm
    {
        private delegate void Vfunc();

        public Loadout()
        {
            InitializeComponent();
            StartRunableThread();
        }


        private void StartRunableThread()
        {
            try
            {
                string ename = "ALProfiles/execute.ini";
                //string filename = File.ReadAllText("ALProfiles/execute.ini");
                using (StreamReader reader = new StreamReader(ename))
                {
                    //var l1 = reader.ReadLine();
                    //string exename = 
                    List<string> = new List<string>();

                }



            }
            catch
            {
                this.Close();
                return;
            }


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
                if (File.Exists(EndName))
                {
                    File.Delete(EndName);
                    break;
                }
            }

            var closef = new Vfunc(Close);
            Invoke(closef);
            //this.Close();
        }

    }
}
