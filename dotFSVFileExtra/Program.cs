using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace dotFSVFileExtra
{
    class Program
    {

        private const string passwd = "cVYAzGYSkmfK4M6UK1CoebMsEPB0KAkD";

        static void Main(string[] args)
        {

            Console.WriteLine("Start..");
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    HandleFsvFile(args[i], i + 1);
                }
                Console.SetCursorPosition(0, args.Length + 1);
                Console.WriteLine("end");
                Console.ReadKey();
            }
        }

        private static int CurrentLine = 0;
        private static string CurrentName = string.Empty;
        private static h5Coder stringParser = new h5Coder();


        private static void HandleFsvFile(string fileName, int line)
        {
            Console.SetCursorPosition(0, line);
            if (File.Exists(fileName))
            {
                try
                {
                    FileInfo file = new FileInfo(fileName);
                    if (file.Extension.Equals(".fsv"))
                    {
                        CurrentLine = line;
                        CurrentName = fileName;

                        ZipFile zip = new ZipFile(file.FullName);
                        zip.Password = passwd;

                        //foreach (var entry in zip.Entries)
                        var entries = new List<ZipEntry>(zip.Entries);
                        for (int i = 0; i < entries.Count; i++)
                            entries[i].FileName = stringParser.Decode(entries[i].FileName);

                        DirectoryInfo directory = new DirectoryInfo(zip.Name.Remove(zip.Name.LastIndexOf('.')));
                        if (!directory.Exists) directory.Create();
                        zip.ExtractProgress += Zip_ExtractProgress;
                        zip.ExtractAll(directory.FullName);
                    }
                    else
                    {
                        Console.Write(string.Format("File not fsv file:{0}", fileName));
                    }

                }
                catch (Exception e)
                {
                    string errorMsg = e.ToString().Split('\n')[0];
                    Console.Write(errorMsg);
                }
            }
            else
            {
                Console.Write(string.Format("File not exist:{0}", fileName));
            }
        }

        private static void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            Console.SetCursorPosition(0, CurrentLine);
            float p = 100;
            if (e.EntriesTotal != 0)
                p = e.EntriesExtracted * 100f / e.EntriesTotal;
            if (p > 100 || p < 0) p = 100;
            Console.Write(string.Format("Extra:[{0}%] {1}", (int)p, CurrentName));
        }

        public class h5Coder
        {
            public string Decode(string encodedString)
            {
                return HttpUtility.UrlDecode(encodedString);
            }

            public string Encode(string plainString)
            {
                return HttpUtility.UrlEncode(plainString);
            }
        }

    }
}
