using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InstConnection
{
    public partial class Form2 : Form
    {
        TreeNode mySeletedNode;

        string[,] path=new string[3,4];

        public Form2()
        {
            InitializeComponent();

            path[0,0] = @"测试设备\04.jpg";
            path[0,1] = @"测试设备\01.jpg";
            path[0,2] = @"";
            path[0,3] = @"";

            path[1,0] = @"测试设备\05.jpg";
            path[1,1] = @"测试设备\01.jpg";
            path[1,2] = @"测试设备\02.jpg";
            path[1,3] = @"测试设备\03.jpg";

            path[2,0] = @"测试设备\06.jpg";
            path[2,1] = @"测试设备\01.jpg";
            path[2,2] = @"测试设备\02.jpg";
            path[2,3] = @"测试设备\03.jpg";

            pictureBox1.SizeMode=PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Owner.Show();
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Close(); 
            this.Owner.Show();
                       
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //treeView1.GetNodeAt()

            //if(pictureBox1.Image != null)
            //{
            //    //pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            //    //pictureBox1.Image = 400;





            //}
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            mySeletedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();    //释放图片缓存
            }
            textBox1.Text = "";
            if (mySeletedNode == null) return;

            //目前原理图比较少,暂时使用下面方法，若原理图比较多，则使用数据库进行管理
            if (mySeletedNode.Parent == null)
            {
                if (mySeletedNode.Name.ToString() == "节点0")
                {
                    pictureBox1.Image = Image.FromFile(path[0, 0]);

                    textBox1.Text = "无线电罗盘原理图说明";

                    return;
                }
                if (mySeletedNode.Name.ToString() == "节点1")
                {
                    pictureBox1.Image = Image.FromFile(path[1, 0]);
                    textBox1.Text = "塔康机载设备原理图说明";
                    return;
                }
                if (mySeletedNode.Name.ToString() == "节点2")
                {
                    pictureBox1.Image = Image.FromFile(path[2, 0]);
                    textBox1.Text = "组合接收设备原理图说明";
                    return;
                }
                return;
            }
            else
            {

                if (mySeletedNode.Parent.Name.ToString() == "节点0")
                {

                    if (mySeletedNode.Name.ToString() == "节点0")
                    {
                        pictureBox1.Image = Image.FromFile(path[0, 0]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点1")
                    {
                        pictureBox1.Image = Image.FromFile(path[0, 1]);
                        return;
                    }
                    return;
                }

                if (mySeletedNode.Parent.Name.ToString() == "节点1")
                {
                    if (mySeletedNode.Name.ToString() == "节点0")
                    {
                        pictureBox1.Image = Image.FromFile(path[1, 0]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点1")
                    {
                        pictureBox1.Image = Image.FromFile(path[1, 1]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点2")
                    {
                        pictureBox1.Image = Image.FromFile(path[1, 2]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点3")
                    {
                        pictureBox1.Image = Image.FromFile(path[1, 3]);
                        return;
                    }
                    return;
                }

                if (mySeletedNode.Parent.Name.ToString() == "节点2")
                {
                    if (mySeletedNode.Name.ToString() == "节点0")
                    {
                        pictureBox1.Image = Image.FromFile(path[2, 0]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点1")
                    {
                        pictureBox1.Image = Image.FromFile(path[2, 1]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点2")
                    {
                        pictureBox1.Image = Image.FromFile(path[2, 2]);
                        return;
                    }
                    if (mySeletedNode.Name.ToString() == "节点3")
                    {
                        pictureBox1.Image = Image.FromFile(path[2, 3]);
                        return;
                    }
                    return;
                }
            }
        }
    }
}
