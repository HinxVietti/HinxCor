
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace FileUploadAssistant
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            InitListView(this.listViewFolder);
            if (args != null && args.Length > 0) ListFolder(new string[] { args[0]});
        }
        private void InitListView(ListView listView)
        {
            listView.SmallImageList = new ImageList();
            listView.LargeImageList = new ImageList();

            listView.View = View.Details;
            listView.AllowDrop = true;
            
        }

        private void buttonSelFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                String[] fileList = System.IO.Directory.GetFiles(folderBrowserDialog.SelectedPath);
                ListFolder(fileList);
            }
        }
        /// <summary>
        /// List files in the folder
        /// </summary>
        /// <param name="directory">the directory of the folder</param>
        private void ListFolder(String[] fileList)
        {

            listViewFolder.Columns.Add("Name", 300);
            listViewFolder.Columns.Add("Size", 100);
            listViewFolder.Columns.Add("Md5", 200);
            listViewFolder.Columns.Add("IsExists", 100);
            var md5s = new List<string>();
            foreach (string fileName in fileList)
            {
                //Show file name
                ListViewItem itemName = new ListViewItem(System.IO.Path.GetFileName(fileName));
                itemName.Tag = fileName;

                //Show file icon
                IconImageProvider iconImageProvider = new IconImageProvider(listViewFolder.SmallImageList, listViewFolder.LargeImageList);
                itemName.ImageIndex = iconImageProvider.GetIconImageIndex(fileName);

                //Show file size
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                long size = fileInfo.Length;

                String strSize;
                if (size < 1024)
                {
                    strSize = size.ToString();
                }
                else if (size < 1024 * 1024)
                {
                    strSize = String.Format("{0:###.##}KB", (float)size / 1024);
                }
                else if (size < 1024 * 1024 * 1024)
                {
                    strSize = String.Format("{0:###.##}MB", (float)size / (1024 * 1024));
                }
                else
                {
                    strSize = String.Format("{0:###.##}GB", (float)size / (1024 * 1024 * 1024));
                }

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = strSize;
                subItem.Tag = size;
                itemName.SubItems.Add(subItem);

                //Show file time
                subItem = new ListViewItem.ListViewSubItem();
                //DateTime fileTime = System.IO.File.GetLastWriteTime(fileName);
                //subItem.Text = (string)fileTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"); ;
                //subItem.Tag = fileTime;

                //Show file md5
                string md5 = "test";
                subItem.Text = md5;
                subItem.Tag = md5;
                subItem.Name = "Md5";
                itemName.SubItems.Add(subItem);

                //Show check result
                subItem = new ListViewItem.ListViewSubItem();
                subItem.Text = ""; ;
                subItem.Tag = "";
                subItem.Name = "IsExists";
                itemName.SubItems.Add(subItem);

                if (!string.IsNullOrEmpty(md5))
                {
                    md5s.Add(md5);
                    itemName.Name = md5;
                    listViewFolder.Items.Add(itemName);
                }
                
            }
            //var dicResult = new Dictionary<string, string>();

            //for (var i = 0; i < this.listViewFolder.Items.Count; i++)
            //{
            //    var md5 = this.listViewFolder.Items[i].Name;
            //    if (dicResult.ContainsKey(md5))
            //    {
            //        this.listViewFolder.Items[i].SubItems["IsExists"].Text = (string.IsNullOrEmpty(dicResult[md5]) ? "不存在" : "已存在");
            //        if (!string.IsNullOrEmpty(dicResult[md5]))
            //        {
            //            this.listViewFolder.Items[i].BackColor = Color.Red;
            //            this.listViewFolder.Items[i].ForeColor = Color.White;
            //        }
            //    }
            //}
            ////MessageBox.Show(this.listViewFolder.Items[md5s[0]].SubItems["Md5"].Text);
        }

        private void listViewFolder_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
                ListFolder(files);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            listViewFolder.Items.Clear();
            listViewFolder.Columns.Clear();
        }

        private void listViewFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void buttonDelFiles_Click(object sender, EventArgs e)
        {
            for (var i = this.listViewFolder.Items.Count-1; i >=0; i--)
            {
                var md5 = this.listViewFolder.Items[i].Name;
                if (this.listViewFolder.Items[i].SubItems["IsExists"].Text=="已存在")
                {
                    var filePath = this.listViewFolder.Items[i].Tag.ToString();
                    File.Delete(filePath);
                    this.listViewFolder.Items[i].Remove();
                }
            }
        }
    }
}
