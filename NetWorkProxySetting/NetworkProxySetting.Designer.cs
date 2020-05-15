namespace NetWorkProxySetting
{
    partial class NetworkProxySetting
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
            this.m_ProxyStatusLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_ProxyStatusLabel
            // 
            this.m_ProxyStatusLabel.AutoSize = true;
            this.m_ProxyStatusLabel.Location = new System.Drawing.Point(12, 9);
            this.m_ProxyStatusLabel.Name = "m_ProxyStatusLabel";
            this.m_ProxyStatusLabel.Size = new System.Drawing.Size(131, 12);
            this.m_ProxyStatusLabel.TabIndex = 0;
            this.m_ProxyStatusLabel.Text = "当前系统代理设置 打开";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打开/关闭代理";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // NetworkProxySetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 63);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_ProxyStatusLabel);
            this.Name = "NetworkProxySetting";
            this.Text = "NetworkProxySetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_ProxyStatusLabel;
        private System.Windows.Forms.Button button1;
    }
}