using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HinxCor.FFmpeg
{
    class Program
    {
        static void Main(string[] args)
        {
            //string cmd = "ffmpeg -i {0} {1}";
            string cmd = "-i {0} {1}";
            string inputName = "input.mp4";
            string outputName = "output.mp4";
            cmd = string.Format(cmd, inputName, outputName);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = cmd,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };

            process.Start();

            string processOutput = null;
            while ((processOutput = process.StandardError.ReadLine()) != null)
            {
                // do something with processOutput
                //Debug.WriteLine(processOutput);
                Console.WriteLine(processOutput);
            }
            Console.WriteLine("Finished.");
            Console.ReadKey();
        }



    }
}
