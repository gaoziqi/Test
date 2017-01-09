using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 高子奇;

namespace InstConnection
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            treeView1.LoadXml(@"科目列表/test2.xml");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("是否进行该科目的考核","提示",MessageBoxButtons.OKCancel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Hide();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }
    }
}
