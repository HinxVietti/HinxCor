using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public class ColorPicker : CommonDialog
    {
        public ColorPicker()
        {
            
            InitializeComponent();
        }


        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        public override void Reset()
        {

        }

        protected override bool RunDialog(IntPtr hwndOwner)
        {
            return true;
        }
    }
}
