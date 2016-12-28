namespace InstConnection
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
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("测试连接图");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("互调抑制比测试连接图");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("无线电罗盘原理图", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("塔康系统测量框图");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("发射峰脉冲值功率测量框图");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("射频脉冲频谱测量框图");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("同波道抑制性能测量框图");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("塔康机载设备原理图", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("测试连接图");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("抗交叉调制干扰测试连接图");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("带内干扰测试连接图");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("带外干扰测试连接图");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("组合接收设备原理图", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25});
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(151, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(482, 49);
            this.label1.TabIndex = 19;
            this.label1.Text = "内场测试设备连接控制台";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(245, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(525, 32);
            this.label3.TabIndex = 22;
            this.label3.Text = "原理图";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 32);
            this.label2.TabIndex = 23;
            this.label2.Text = "科目列表";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 104);
            this.treeView1.Name = "treeView1";
            treeNode14.Name = "节点0";
            treeNode14.Text = "测试连接图";
            treeNode15.Name = "节点1";
            treeNode15.Text = "互调抑制比测试连接图";
            treeNode16.Name = "节点0";
            treeNode16.Text = "无线电罗盘原理图";
            treeNode17.Name = "节点0";
            treeNode17.Text = "塔康系统测量框图";
            treeNode18.Name = "节点1";
            treeNode18.Text = "发射峰脉冲值功率测量框图";
            treeNode19.Name = "节点2";
            treeNode19.Text = "射频脉冲频谱测量框图";
            treeNode20.Name = "节点3";
            treeNode20.Text = "同波道抑制性能测量框图";
            treeNode21.Name = "节点1";
            treeNode21.Text = "塔康机载设备原理图";
            treeNode22.Name = "节点0";
            treeNode22.Text = "测试连接图";
            treeNode23.Name = "节点1";
            treeNode23.Text = "抗交叉调制干扰测试连接图";
            treeNode24.Name = "节点2";
            treeNode24.Text = "带内干扰测试连接图";
            treeNode25.Name = "节点3";
            treeNode25.Text = "带外干扰测试连接图";
            treeNode26.Name = "节点2";
            treeNode26.Text = "组合接收设备原理图";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode21,
            treeNode26});
            this.treeView1.Size = new System.Drawing.Size(229, 409);
            this.treeView1.TabIndex = 21;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(247, 104);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(525, 295);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(14, 519);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 30);
            this.button1.TabIndex = 24;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(249, 406);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(523, 143);
            this.textBox1.TabIndex = 25;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "教学模式";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}