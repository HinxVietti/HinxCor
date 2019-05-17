namespace HinxCor.WindowsForm
{
    partial class PickColorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickColorDialog));
            this.button1 = new System.Windows.Forms.Button();
            this.PreviewPanel = new System.Windows.Forms.Panel();
            this.Lines = new System.Windows.Forms.PictureBox();
            this.PreviewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lines)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PreviewPanel
            // 
            this.PreviewPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PreviewPanel.Controls.Add(this.Lines);
            this.PreviewPanel.Location = new System.Drawing.Point(337, 135);
            this.PreviewPanel.Name = "PreviewPanel";
            this.PreviewPanel.Size = new System.Drawing.Size(116, 116);
            this.PreviewPanel.TabIndex = 99;
            // 
            // Lines
            // 
            this.Lines.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Lines.Image = ((System.Drawing.Image)(resources.GetObject("Lines.Image")));
            this.Lines.Location = new System.Drawing.Point(0, 0);
            this.Lines.Name = "Lines";
            this.Lines.Size = new System.Drawing.Size(116, 116);
            this.Lines.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Lines.TabIndex = 1;
            this.Lines.TabStop = false;
            // 
            // PickColorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.PreviewPanel);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PickColorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PickColorDialog";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PreviewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Lines)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel PreviewPanel;
        private System.Windows.Forms.PictureBox Lines;
    }
}