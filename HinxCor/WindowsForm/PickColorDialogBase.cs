using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HinxCor.WindowsForm
{
    public partial class PickColorDialogBase : Form
    {
        private delegate void SyncScreen(Point mouseLocation);

        Color color;
        private int MaxWid;
        private int MaxHei;

        public PickColorDialogBase()
        {
            InitializeComponent();
            this.BackgroundImage = new ScreenCapture().CaptureScreen();
            //MouseMove += PickColorDialogBase_MouseMove;
            MouseDown += PickColorDialogBase_MouseDown;
            MaxWid = Screen.PrimaryScreen.Bounds.Width;
            MaxHei = Screen.PrimaryScreen.Bounds.Height;
            Load += PickColorDialogBase_Load;

        }
        Thread pickColor;
        private void PickColorDialogBase_Load(object sender, EventArgs e)
        {
            SyncScreen Worker = PickColorDialogBase_MouseMove;
            pickColor = new Thread(() =>
           {
               while (true)
               {
                   this.Invoke(Worker, Cursor.Position);
                   Thread.Sleep(20);
               }
           })
            {
                IsBackground = true
            };
            pickColor.Start();
        }

        private void PickColorDialogBase_MouseMove(Point e)
        {
            this.panel1.Location = GetLocation(e);
            this.pictureBox1.BackColor = ((Bitmap)this.BackgroundImage).GetPixel(e.X, e.Y);
            this.label1.Text = GetColorString(this.pictureBox1.BackColor);
        }

        private string GetColorString(Color backColor)
        {
            return string.Format("#{0}{1}{2}{3}", System.Convert.ToString(backColor.R, 16),
                System.Convert.ToString(backColor.G, 16),
                System.Convert.ToString(backColor.B, 16),
                System.Convert.ToString(backColor.A, 16)).ToUpper();
        }

        readonly Size offset = new Size(20, 15);

        private Point GetLocation(Point location)
        {
            if (location.X > MaxWid - 50)
                location.X -= offset.Width + panel1.Width;
            /*if (location.X < 50) */
            else location.X += offset.Width;
            if (location.Y > MaxHei - 50)
                location.Y -= offset.Height + panel1.Height;
            /*if (location.Y < 50) */
            else location.Y += offset.Height;
            return location;
        }

        private void PickColorDialogBase_MouseDown(object sender, MouseEventArgs e)
        {
            color = ((Bitmap)this.BackgroundImage).GetPixel(e.Location.X, e.Location.Y);
            pickColor.Abort();
            this.Close();
        }

        public Color ShowAndBlock()
        {
            this.ShowDialog();
            return color;
        }

    }
}
