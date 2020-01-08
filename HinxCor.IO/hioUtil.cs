using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HinxCor.IO
{
    public class hioUtil
    {

        void test()
        {
            FileBundle fb = new FileBundle();
            int size = 0;
            while ((size = fb.NextInt()) != 0)
            {
                string fName = fb.NextString(size);
                size = fb.NextInt();
                byte[] data = fb.NextArray(size);

            }
        }
    }
}
