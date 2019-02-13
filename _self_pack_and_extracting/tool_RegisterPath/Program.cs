using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tool_RegisterPath
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                string pth = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";

                const string name = "PATH";
                string pathvar = Environment.GetEnvironmentVariable(name);

                var pths = pathvar.Split(';');
                Console.WriteLine(pth);
                bool hasF = false;
                for (int i = 0; i < pths.Length; i++)
                    hasF = hasF || pths[i] == pth;

                if (!hasF)
                {
                    var value = pathvar + @";" + pth;
                    var target = EnvironmentVariableTarget.Machine;
                    Environment.SetEnvironmentVariable(name, value, target);
                    Console.WriteLine("success add to path..");
                }
                else
                    Console.WriteLine("已经存在配置路径:" + pth);
            }
            else
            {
                Console.WriteLine("To register paths");
                foreach (var arg in args)
                {
                    Console.WriteLine(arg);
                }

            }

            Console.WriteLine("Finished...Any key to esc.");
            //Console.ReadKey();
        }
    }
}
