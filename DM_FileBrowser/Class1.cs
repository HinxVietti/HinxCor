using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM_FileBrowser
{
    public class Class1
    {


        public static string GetSaveName()
        {
            OpenFileDialog openf = new OpenFileDialog();
            openf.ShowDialog();
            return openf.FileName;
        }
    }
}
