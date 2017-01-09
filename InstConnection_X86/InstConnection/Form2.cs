using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 高子奇;
using System.Xml.Linq;

namespace InstConnection
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            treeView1.LoadXml(@"科目列表/test1.xml");
            pictureBox1.SizeMode=PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();               
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;
            Image image = null;
            if (node.Tag != null)
            {
                foreach (XAttribute a in node.Tag as IEnumerable<XAttribute>)
                {
                    if (a.Name.ToString() == "picture")
                    {
                        image = Image.FromFile(a.Value);
                        break;
                    }
                }
            }
            if(pictureBox1.Image!=null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = image;
        }
    }
}
