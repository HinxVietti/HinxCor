using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo_DotNetZip
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("请选择：");
            Console.WriteLine("1. 打开现有Zip\n2. 创建新Zip");
            Console.WriteLine("请输入：");
            ZipFile zipFile = null;
            Func<int> getIndex = null;
            getIndex = () =>
            {
            rin: string code = Console.ReadLine();
                if (int.TryParse(code, out var res))
                    return res;
                else goto rin;
            };

        input: int index = getIndex();
            if (index == 1)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "FZ|*.fz|Zip文档|*.zip";
                dlg.FilterIndex = 1;
                while (dlg.ShowDialog() != DialogResult.OK)
                { }
                zipFile = new ZipFile(dlg.FileName);
            }
            else if (index == 2)
                zipFile = new ZipFile();
            else goto input;


            Console.WriteLine("CMD:\n1: add file, 2.Debug names");
            using (zipFile)
            {

            cmd: Console.WriteLine("请输入命令：");
                index = getIndex();
                switch (index)
                {
                    case 1:
                        OpenFileDialog dlg = new OpenFileDialog();
                        if (dlg.ShowDialog() == DialogResult.OK)
                            zipFile.AddFile(dlg.FileName);
                        Console.WriteLine();
                        goto cmd;
                    case 2:
                        var ets = zipFile.Entries;
                        foreach (var ent in ets)
                            Console.WriteLine(ent.FileName);
                        Console.WriteLine();
                        goto cmd;
                    case 0:
                        break;
                    default:
                        goto cmd;
                }

                SaveFileDialog sdlg = new SaveFileDialog();
                sdlg.Filter = "FZ|*.fz|Zip文档|*.zip";
                while (sdlg.ShowDialog() != DialogResult.OK) ;
                zipFile.Save(sdlg.FileName);
            }
            Console.WriteLine("WEnd");
            Console.ReadKey();
        }
    }

}
