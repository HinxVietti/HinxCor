using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class 测试笔画绘制 : Form
    {
        public 测试笔画绘制()
        {
            InitializeComponent();
            InitialEvents();
        }

        private ScreenPainter_NOGUI SPNGUI;

        private void InitialEvents()
        {
            var bund = Screen.PrimaryScreen.Bounds;
            this.Size = bund.Size;
            this.WindowState = FormWindowState.Maximized;
            SPNGUI = new ScreenPainter_NOGUI(Size.Width, Size.Height);

            KeyPress += 测试笔画绘制_KeyPress;

            printer.MouseDown += Printer_MouseDown;
            printer.MouseUp += Printer_MouseUp;
            printer.MouseMove += Printer_MouseMove;

        }

        private bool mousedown;
        private PaintType type = PaintType.Ellipse;
        PCmdData_Lines cmd = new PCmdData_Lines();
        PCmdData_2p p;

        private void Printer_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                //cmd.add(e.Location);
                p.End = e.Location;
                SPNGUI.Handle(p);
                this.printer.Image = SPNGUI.GetGlyph();
            }
        }

        private void Printer_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
            SPNGUI.DrawGlyph();
            this.BackgroundImage = SPNGUI.GetMainContent();
        }

        private void Printer_MouseDown(object sender, MouseEventArgs e)
        {
            cmd = new PCmdData_Lines();
            cmd.add(e.Location);
            p = new PCmdData_2p(type)
            {
                Start = e.Location
            };
            mousedown = true;
        }

        private void 测试笔画绘制_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 27) Close();

        }
    }
}
