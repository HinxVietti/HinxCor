using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HinxCor.CryptoSer;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        private int round;
        public Form1()
        {
            InitializeComponent();
            Form1_Load();
        }



        private void comboBox1_SelectedIndexChanged()
        {
            string str = this.comboBox1.Text;
            switch (str)
            {
                case "128位加密": round = 16; break;
                case "192位加密": round = 24; break;
                case "256位加密": round = 32; break;
                default: break;
            }
        }

        private void Form1_Load()
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged();
            if (round == 0) round = 16;
            RC6 RC = new RC6(round);
            this.label6.Text = RC.Decrypt(this.textBox2.Text, this.textBox3.Text);
            comboBox1.Enabled = true; comboBox1.ForeColor = SystemColors.WindowText;
            textBox2.Enabled = true; textBox2.ForeColor = SystemColors.WindowText;
            textBox3.Enabled = true; textBox3.ForeColor = SystemColors.WindowText;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "") { MessageBox.Show("请输入加密的明文"); this.textBox1.Focus(); return; }
            if (this.textBox3.Text == "") { MessageBox.Show("请输入加密的密钥"); this.textBox3.Focus(); return; }
            comboBox1_SelectedIndexChanged();
            RC6 RC = new RC6(round);
            //如果手动输入加密向量RC.IV = -1;请使用RC._IV():函数进行验证。

            textBox2.Text = RC.Encrypt(this.textBox1.Text, this.textBox3.Text);
            comboBox1.Enabled = false; comboBox1.ForeColor = SystemColors.WindowText;
            textBox2.Enabled = false; textBox2.ForeColor = SystemColors.WindowText;
            textBox3.Enabled = false; textBox3.ForeColor = SystemColors.WindowText;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }
    }
}