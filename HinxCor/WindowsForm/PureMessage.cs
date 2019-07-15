using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HinxCor.WindowsForm
{
    public partial class PureMessage : Form
    {
        private delegate void ApplyText(string title, string msg);
        private delegate void function();

        ApplyText func;
        function centerToScreen, centerToParent, clear;

        public PureMessage()
        {
            InitializeComponent();
            func = (t, m) =>
            {
                this.TitleLabel.Text = t;
                this.message.Text = m;
                this.Refresh();
            };
            centerToParent = CenterToParent;
            centerToScreen = CenterToScreen;
            clear = Close;

            Shown += PureMessage_Shown;
            FormClosed += PureMessage_FormClosed;
        }

        private void PureMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            shown = false;
        }

        public bool shown = false;

        private void PureMessage_Shown(object sender, EventArgs e)
        {
            shown = true;
        }


        /// <summary>
        /// 关闭操作
        /// </summary>
        public void Clear() => this.Invoke(clear);
        /// <summary>
        /// 居中屏幕
        /// </summary>
        public void MidCenter() => this.Invoke(centerToScreen);
        /// <summary>
        /// 居中到父级窗口
        /// </summary>
        public void CenterParent() => this.Invoke(centerToParent);

        public void Set(string title, object message)
        {
            this.Invoke(func, title, message.ToString());
        }
    }
}
