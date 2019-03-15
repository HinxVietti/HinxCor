using HinxCor;
using HinxCor.Compiler;
using System;
using System.Threading;

namespace demo_CompilerByHinxCor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("any key start");
            Console.ReadKey();
            var AO1 = CompilerHelper.ASyncBuild_MHX_DATA(new[] { "demo.exe" });
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            int index = 0;
            while (AO1.isDone == false)
            {
                //Console.SetCursorPosition(index, 2);
                Console.Write(".");
                index++;
                index = HMath.Clamp(index, 0, 100);
                //Console.SetCursorPosition(0, 3);
                Console.WriteLine(AO1.Log);
                Thread.Sleep(500);
            }
            Console.WriteLine();
            Console.WriteLine("FINISHED 1");
            var AO2 = Compiler.AsyncTryComplire("输出.exe");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            index = 0;
            while (AO2.isDone == false)
            {
                Console.Write(".");
                index++;
                index = HMath.Clamp(index, 0, 100);
                Console.WriteLine(AO1.Log);
                Thread.Sleep(500);
            }

            Console.WriteLine();
            Console.WriteLine("ALL FINISHED..");
            Console.WriteLine("Any key esc.");
            Console.ReadKey();
        }
    }
}
