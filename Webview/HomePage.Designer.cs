namespace Webview
{
    partial class HomePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.web = new System.Windows.Forms.WebBrowser();
            this.DialogContainer = new System.Windows.Forms.SplitContainer();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.closePicBack = new System.Windows.Forms.PictureBox();
            this.closepic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DialogContainer)).BeginInit();
            this.DialogContainer.Panel1.SuspendLayout();
            this.DialogContainer.Panel2.SuspendLayout();
            this.DialogContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closePicBack)).BeginInit();
            this.closePicBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closepic)).BeginInit();
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web.Location = new System.Drawing.Point(0, 0);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(1006, 574);
            this.web.TabIndex = 0;
            // 
            // DialogContainer
            // 
            this.DialogContainer.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.DialogContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.DialogContainer.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DialogContainer.IsSplitterFixed = true;
            this.DialogContainer.Location = new System.Drawing.Point(0, 0);
            this.DialogContainer.Margin = new System.Windows.Forms.Padding(0);
            this.DialogContainer.Name = "DialogContainer";
            this.DialogContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // DialogContainer.Panel1
            // 
            this.DialogContainer.Panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.DialogContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(231)))));
            this.DialogContainer.Panel1.Controls.Add(this.TitleLabel);
            this.DialogContainer.Panel1.Controls.Add(this.closePicBack);
            this.DialogContainer.Panel1.MouseDown += MoveWindow;
            //this.DialogContainer.Panel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            // 
            // DialogContainer.Panel2
            // 
            this.DialogContainer.Panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.DialogContainer.Panel2.Controls.Add(this.web);
            this.DialogContainer.Size = new System.Drawing.Size(1006, 600);
            this.DialogContainer.SplitterDistance = 25;
            this.DialogContainer.SplitterWidth = 1;
            this.DialogContainer.TabIndex = 1;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("新宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(5, 5);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(215, 15);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Welcome Dotnet web browser";
            // 
            // closePicBack
            // 
            this.closePicBack.BackColor = System.Drawing.Color.White;
            this.closePicBack.Controls.Add(this.closepic);
            this.closePicBack.Location = new System.Drawing.Point(983, 2);
            this.closePicBack.Name = "closePicBack";
            this.closePicBack.Size = new System.Drawing.Size(21, 21);
            this.closePicBack.TabIndex = 2;
            this.closePicBack.TabStop = false;
            // 
            // closepic
            // 
            this.closepic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closepic.Image = global::Webview.Properties.Resources.close;
            this.closepic.Location = new System.Drawing.Point(4, 4);
            this.closepic.Name = "closepic";
            this.closepic.Size = new System.Drawing.Size(15, 15);
            this.closepic.TabIndex = 1;
            this.closepic.TabStop = false;
            this.closepic.Click += new System.EventHandler(this.closepic_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 600);
            this.Controls.Add(this.DialogContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomePage";
            this.DialogContainer.Panel1.ResumeLayout(false);
            this.DialogContainer.Panel1.PerformLayout();
            this.DialogContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DialogContainer)).EndInit();
            this.DialogContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closePicBack)).EndInit();
            this.closePicBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closepic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser web;
        private System.Windows.Forms.SplitContainer DialogContainer;
        private System.Windows.Forms.PictureBox closepic;
        private System.Windows.Forms.PictureBox closePicBack;
        private System.Windows.Forms.Label TitleLabel;
    }
}