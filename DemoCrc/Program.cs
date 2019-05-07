using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HinxCor;
using HinxCor.Compiler;

namespace DemoCrc
{
    class Program
    {
        static void Main(string[] args)
        {
            var res1 = EnvironmentTools.AddPath(Compiler.cscEnv);
            Console.WriteLine(res1);
            string code = "using System; " +
"class App                            " +
"{                                    " +
"    static void Main(string[] args)  " +
"    {                                " +
"        Console.WriteLine(\"成功：02\");" +
"        Console.ReadKey();             " +
"    }                                  " +
"}                                      ";


            string fname = "code.cs";
            File.WriteAllText(fname, code);

            Windows.ExecuteCommand("csc -out:demo.exe code.cs");

            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "demo.exe";
            Process.Start(p);
            Console.ReadKey();
        }
    }
}



//