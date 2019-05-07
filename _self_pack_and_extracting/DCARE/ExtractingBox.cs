using System;
using System.Windows.Forms;

namespace DCARE
{
    public partial class ExtractingBox : Form
    {
        delegate void winformfunc(float value);
        winformfunc SsetProgress;
        winformfunc closef;


        public ExtractingBox()
        {
            InitializeComponent();
            SsetProgress = v =>
            {
                SetProgress(v);
            };
            closef = v =>
            {
                Close();
            };
        }

        internal void SetValue(float v)
        {
            this.Invoke(SsetProgress, v);
        }

        private void SetProgress(float value)
        {
            this.progressBar1.Value = (int)(value * 100);
            this.label1.Text = string.Format("提取中...{0}%,请稍后.", progressBar1.Value);
        }

        internal void DoClose()
        {
            this.Invoke(closef, 0);
        }
    }
}
