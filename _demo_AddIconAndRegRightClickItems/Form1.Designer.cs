namespace FileUploadAssistant
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewFolder = new System.Windows.Forms.ListView();
            this.buttonSelFolder = new System.Windows.Forms.Button();
            this.buttonDelFiles = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFolder
            // 
            this.listViewFolder.AllowDrop = true;
            this.listViewFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFolder.Location = new System.Drawing.Point(3, 63);
            this.listViewFolder.Name = "listViewFolder";
            this.listViewFolder.Size = new System.Drawing.Size(794, 384);
            this.listViewFolder.TabIndex = 1;
            this.listViewFolder.UseCompatibleStateImageBehavior = false;
            this.listViewFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFolder_DragDrop);
            this.listViewFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFolder_DragEnter);
            // 
            // buttonSelFolder
            // 
            this.buttonSelFolder.Location = new System.Drawing.Point(25, 18);
            this.buttonSelFolder.Name = "buttonSelFolder";
            this.buttonSelFolder.Size = new System.Drawing.Size(133, 21);
            this.buttonSelFolder.TabIndex = 2;
            this.buttonSelFolder.Text = "1.选择实例所在文件夹";
            this.buttonSelFolder.UseVisualStyleBackColor = true;
            this.buttonSelFolder.Click += new System.EventHandler(this.buttonSelFolder_Click);
            // 
            // buttonDelFiles
            // 
            this.buttonDelFiles.Location = new System.Drawing.Point(181, 18);
            this.buttonDelFiles.Name = "buttonDelFiles";
            this.buttonDelFiles.Size = new System.Drawing.Size(150, 21);
            this.buttonDelFiles.TabIndex = 3;
            this.buttonDelFiles.Text = "2.删除上传过的本地文件";
            this.buttonDelFiles.UseVisualStyleBackColor = true;
            this.buttonDelFiles.Click += new System.EventHandler(this.buttonDelFiles_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listViewFolder, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonSelFolder);
            this.panel1.Controls.Add(this.buttonDelFiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 54);
            this.panel1.TabIndex = 0;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(356, 18);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(133, 21);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "3.清空列表(重新选择)";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "提示：将要检测的文件拖拽至如下列表即可检测";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "重复文件检测工具";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFolder;
        private System.Windows.Forms.Button buttonSelFolder;
        private System.Windows.Forms.Button buttonDelFiles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonReset;
    }
}

