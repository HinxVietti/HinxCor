namespace DemoApp
{
    partial class Form4
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.SolidBrush = new System.Windows.Forms.Button();
            this.TextureBrush = new System.Windows.Forms.Button();
            this.HatchBrush = new System.Windows.Forms.Button();
            this.LinearGradientBrush = new System.Windows.Forms.Button();
            this.PathGradientBrush = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(387, 426);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // SolidBrush
            // 
            this.SolidBrush.Location = new System.Drawing.Point(453, 40);
            this.SolidBrush.Name = "SolidBrush";
            this.SolidBrush.Size = new System.Drawing.Size(259, 29);
            this.SolidBrush.TabIndex = 1;
            this.SolidBrush.Text = "SolidBrush";
            this.SolidBrush.UseVisualStyleBackColor = true;
            this.SolidBrush.Click += new System.EventHandler(this.SolidBrush_Click);
            // 
            // TextureBrush
            // 
            this.TextureBrush.Location = new System.Drawing.Point(453, 93);
            this.TextureBrush.Name = "TextureBrush";
            this.TextureBrush.Size = new System.Drawing.Size(259, 29);
            this.TextureBrush.TabIndex = 2;
            this.TextureBrush.Text = "TextureBrush";
            this.TextureBrush.UseVisualStyleBackColor = true;
            this.TextureBrush.Click += new System.EventHandler(this.TextureBrush_Click);
            // 
            // HatchBrush
            // 
            this.HatchBrush.Location = new System.Drawing.Point(453, 153);
            this.HatchBrush.Name = "HatchBrush";
            this.HatchBrush.Size = new System.Drawing.Size(259, 29);
            this.HatchBrush.TabIndex = 3;
            this.HatchBrush.Text = "HatchBrush";
            this.HatchBrush.UseVisualStyleBackColor = true;
            this.HatchBrush.Click += new System.EventHandler(this.HatchBrush_Click);
            // 
            // LinearGradientBrush
            // 
            this.LinearGradientBrush.Location = new System.Drawing.Point(453, 210);
            this.LinearGradientBrush.Name = "LinearGradientBrush";
            this.LinearGradientBrush.Size = new System.Drawing.Size(259, 29);
            this.LinearGradientBrush.TabIndex = 4;
            this.LinearGradientBrush.Text = "LinearGradientBrush";
            this.LinearGradientBrush.UseVisualStyleBackColor = true;
            this.LinearGradientBrush.Click += new System.EventHandler(this.LinearGradientBrush_Click);
            // 
            // PathGradientBrush
            // 
            this.PathGradientBrush.Location = new System.Drawing.Point(453, 270);
            this.PathGradientBrush.Name = "PathGradientBrush";
            this.PathGradientBrush.Size = new System.Drawing.Size(259, 29);
            this.PathGradientBrush.TabIndex = 5;
            this.PathGradientBrush.Text = "PathGradientBrush";
            this.PathGradientBrush.UseVisualStyleBackColor = true;
            this.PathGradientBrush.Click += new System.EventHandler(this.PathGradientBrush_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PathGradientBrush);
            this.Controls.Add(this.LinearGradientBrush);
            this.Controls.Add(this.HatchBrush);
            this.Controls.Add(this.TextureBrush);
            this.Controls.Add(this.SolidBrush);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form4";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button SolidBrush;
        private System.Windows.Forms.Button TextureBrush;
        private System.Windows.Forms.Button HatchBrush;
        private System.Windows.Forms.Button LinearGradientBrush;
        private System.Windows.Forms.Button PathGradientBrush;
    }
}