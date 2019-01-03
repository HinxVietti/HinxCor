namespace DemoApp
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DrawstringBox = new System.Windows.Forms.PictureBox();
            this.GetBmpBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawstringBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetBmpBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GetBmpBox);
            this.panel1.Location = new System.Drawing.Point(12, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 666);
            this.panel1.TabIndex = 0;
            // 
            // DrawstringBox
            // 
            this.DrawstringBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DrawstringBox.Location = new System.Drawing.Point(12, 280);
            this.DrawstringBox.Name = "DrawstringBox";
            this.DrawstringBox.Size = new System.Drawing.Size(616, 370);
            this.DrawstringBox.TabIndex = 0;
            this.DrawstringBox.TabStop = false;
            // 
            // GetBmpBox
            // 
            this.GetBmpBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GetBmpBox.Location = new System.Drawing.Point(-3, 0);
            this.GetBmpBox.Name = "GetBmpBox";
            this.GetBmpBox.Size = new System.Drawing.Size(619, 272);
            this.GetBmpBox.TabIndex = 0;
            this.GetBmpBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "设置字体";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(637, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 132);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(637, 179);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "DrawString";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(637, 208);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "GetBmp";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 662);
            this.Controls.Add(this.DrawstringBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DrawstringBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetBmpBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox DrawstringBox;
        private System.Windows.Forms.PictureBox GetBmpBox;
    }
}