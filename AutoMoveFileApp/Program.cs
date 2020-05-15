using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMoveFileApp
{
    class Program
    {
        static Dictionary<string, string> maps;


        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                args = File.ReadAllLines("config");
            }
            else if (args.Length == 1)
            {
                args = File.ReadAllLines(args[0]);
            }

            maps = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                var a2 = args[i].Split('|');
                if (a2.Length > 1)
                {
                    if (!maps.ContainsKey(a2[0]))
                    {
                        maps.Add(a2[0], a2[1]);
                    }
                }
            }
            foreach (var kvp in maps)
                Console.WriteLine("Add listen:" + kvp.Key);
            Console.WriteLine();

            new Thread(() =>
            {
                Console.WriteLine("thread start");
                Console.WriteLine();
                while (true)
                {
                    Thread.Sleep(300);

                    foreach (var kvp in maps)
                    {
                        try
                        {
                            if (File.Exists(kvp.Key))
                            {
                                Console.WriteLine();
                                File.Copy(kvp.Key, kvp.Value, true);
                                File.Delete(kvp.Key);
                                Console.WriteLine("move2: " + kvp.Value);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error");
                        }
                    }

                }

            })
            {
                IsBackground = true //kill with main thread
            }.Start();

            Console.WriteLine();
            Console.WriteLine("后台已经启动，按下 esc 退出");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Thread.Sleep(20);
            Console.WriteLine("quit");

        }
    }
}
