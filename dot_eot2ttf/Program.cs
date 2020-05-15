using System;
using System.Runtime.InteropServices; // 用 DllImport 需用此 命名空间
using System.Reflection; // 使用 Assembly 类需用此 命名空间
using System.Reflection.Emit; // 使用 ILGenerator 需用此 命名空间


namespace run_eot2ttf
{
    class Program
    {

        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong CSharp_eot2ttf_init(string eot_file);

        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_apply", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSharp_eot2ttf_apply(ulong file);
        //CSharp_eot2ttf_free
        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_free", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSharp_eot2ttf_free(ulong aa);
        //CSharp_eot2ttf_set_json_path
        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_set_json_path", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSharp_eot2ttf_set_json_path(ulong aa, string file);
        //CSharp_eot2ttf_set_message_fun
        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_set_message_fun", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSharp_eot2ttf_set_message_fun(ulong aa, string file);
        //CSharp_eot2ttf_set_ttf_path
        [DllImport("eot2ttf.dll", EntryPoint = "CSharp_eot2ttf_set_ttf_path", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSharp_eot2ttf_set_ttf_path(ulong aa, string file);

        static void Main(string[] args)
        {
            ulong aa = (ulong)CSharp_eot2ttf_init("E:\\test\\fonts\\font1.fntdata");
            CSharp_eot2ttf_set_ttf_path(aa, "E:\\test\\fonts\\font1.ttf");
            CSharp_eot2ttf_set_json_path(aa, "E:\\test\\fonts\\font1.json");
            CSharp_eot2ttf_apply(aa);
            CSharp_eot2ttf_free(aa);
            Console.WriteLine("Hello World!");
        }
    }
}
