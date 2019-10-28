using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAudioDemostration
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            began:
            OpenFileDialog openf = new OpenFileDialog();
            openf.Filter = "Music File|*.MP3";
            openf.ShowDialog();
            string audioFileName = openf.FileName;
            Console.WriteLine(audioFileName + " is playing.");
            using (var audioFile = new AudioFileReader(audioFileName))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (true)
                {
                    //if()
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        break;
                    else if (key.Key == ConsoleKey.S)
                        goto began;
                    Thread.Sleep(1000);
                }
            }

        }
    }
}
