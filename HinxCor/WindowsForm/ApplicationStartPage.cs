using HinxCor.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HinxCor.WindowsForm
{
    /// <summary>
    /// 可以显示日志的载入窗口
    /// </summary>
    public partial class ApplicationStartPage : Form
    {
        action_aa logHandle;
        action close;
        action_a changeColor;

        private bool initfinished = false;
        public ApplicationStartPage()
        {
            InitializeComponent();
            string iconName = "icon.ico";
            if (File.Exists(iconName)) this.Icon = new Icon(iconName);

            logHandle = (p, m) =>
            {
                DrawLogss((Point)p, (string)m);
            };
            close = () =>
            {
                Close();
                Dispose();
            };
            changeColor = color =>
            {
                var c = (Color)color;
                if (c != null)
                    logLabel.ForeColor = c;
            };
            initfinished = true;
        }

        
        /// <summary>
        /// 从ALProfile读取
        /// </summary>
        public void SetImage()
        {
            var rundataFileName = "ALProfiles";
            FileStream fs = new FileStream(rundataFileName, FileMode.Open);
            BundleFile bf = new BundleFile(fs);

            bf.StartPop();
            var entry1 = bf.PopEntry() as TxtFileEntry;
            var entry2 = bf.PopEntry() as PNGFileEntry;
            CenterToScreen();
            this.BackgroundImage = entry2.GetImage();
            this.Size = BackgroundImage.Size;
            CenterToScreen();
        }

        public void SetImage(Image image)
        {
            this.BackgroundImage = image;
            this.Size = image.Size;
            this.CenterToScreen();
        }

        public void ChangeTextColor(Color color)
        {
            if (!initfinished) return;
            this.Invoke(changeColor, color);
        }

        public void ClosePanel()
        {
            if (!initfinished) return;
            this.Invoke(close);
        }

        public void DrawLog(Point offset, object log)
        {
            if (!initfinished) return;
            this.Invoke(logHandle, offset, log.ToString());
        }


        private void DrawLogss(Point offset, string log)
        {
            this.logLabel.Location = offset;
            this.logLabel.Text = log;
        }


    }
}
