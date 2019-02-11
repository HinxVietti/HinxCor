using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _showTxtOnConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WelCome");
            Console.WriteLine();
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine("ARGS: \t" + args[i]);
            Console.WriteLine("");
            string str = File.ReadAllText("1.txt");
            Console.WriteLine(str);
            
            Console.ReadKey();
        }
    }
}
