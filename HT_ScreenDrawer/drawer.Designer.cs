namespace HT_ScreenDrawer
{
    partial class drawer
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
            this.printer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.printer)).BeginInit();
            this.SuspendLayout();
            // 
            // printer
            // 
            this.printer.BackColor = System.Drawing.Color.Transparent;
            this.printer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printer.Location = new System.Drawing.Point(0, 0);
            this.printer.Name = "printer";
            this.printer.Size = new System.Drawing.Size(889, 530);
            this.printer.TabIndex = 0;
            this.printer.TabStop = false;
            // 
            // drawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(889, 530);
            this.Controls.Add(this.printer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "drawer";
            this.Text = "drawer";
            ((System.ComponentModel.ISupportInitialize)(this.printer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox printer;
    }
}