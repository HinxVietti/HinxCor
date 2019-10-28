using HinxCor.IO;
using HinxCor.Serialize;
using HinxCor.Wins.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public partial class HybridTransparentBorder : TransparencyForm
{

    public HybridTransparentBorder()
    {
        InitializeComponent();//初始化组建
        CenterToScreen();//更新显示方案
        StartRunableThread();//启动监听
    }

    private void StartRunableThread()
    {
        var rundataFileName = "ALProfiles";
        FileStream fs = new FileStream(rundataFileName, FileMode.Open);
        BundleFile bf = new BundleFile(fs);

        bf.StartPop();
        var entry1 = bf.PopEntry() as TxtFileEntry;
        var entry2 = bf.PopEntry() as PNGFileEntry;
        CenterToScreen();
        Display.Image = entry2.GetImage();
        this.Size = Display.Image.Size;
        var bitmap = (Bitmap)Display.Image;
        SetBitmap(bitmap, 255);
        //TopMost = true;
        CenterToScreen();

    }

}
partial class HybridTransparentBorder
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Display = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.Display)).BeginInit();
        this.SuspendLayout();
        // 
        // Display
        // 
        this.Display.BackColor = System.Drawing.Color.Transparent;
        this.Display.Dock = System.Windows.Forms.DockStyle.Fill;
        this.Display.Location = new System.Drawing.Point(0, 0);
        this.Display.Name = "Display";
        this.Display.Size = new System.Drawing.Size(244, 261);
        this.Display.TabIndex = 0;
        this.Display.TabStop = false;
        // 
        // Loadout
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(244, 261);
        this.ControlBox = false;
        this.Controls.Add(this.Display);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "Loadout";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Loadout";
        ((System.ComponentModel.ISupportInitialize)(this.Display)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox Display;
}