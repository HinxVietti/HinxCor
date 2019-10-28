using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgsDebuger
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null) return;
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine(args[i]);
            Console.WriteLine();
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
