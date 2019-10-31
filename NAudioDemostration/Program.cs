using System;
using System.IO;
using System.Windows.Forms;

namespace NAudioDemostration
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openf = new OpenFileDialog();
            openf.Filter = "|*.mp3";
            openf.ShowDialog();

            string fileName = openf.FileName;

            var bdat = File.ReadAllBytes(fileName);
            var b64 = Convert.ToBase64String(bdat);
            var ndat = Convert.FromBase64String(b64);

            var ms = new MemoryStream(ndat);

            //FileStream fs = new FileStream(fileName, FileMode.Open);

            NAudioUtility.PlayAudioStream(ms);
            //began:
            //OpenFileDialog openf = new OpenFileDialog();
            //openf.Filter = "Music File|*.MP3";
            //openf.ShowDialog();
            //string audioFileName = openf.FileName;
            //Console.WriteLine(audioFileName + " is playing.");
            //using (var audioFile = new AudioFileReader(audioFileName))
            //using (var outputDevice = new WaveOutEvent())
            //{
            //    outputDevice.Init(audioFile);
            //    outputDevice.Play();

            //    while (true)
            //    {
            //        //if()
            //        var key = Console.ReadKey();
            //        if (key.Key == ConsoleKey.Escape)
            //            break;
            //        else if (key.Key == ConsoleKey.S)
            //            goto began;
            //        Thread.Sleep(1000);
            //    }
            //}

        }
    }
}
