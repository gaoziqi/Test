namespace InstConnection
{
    partial class Form3
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
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("原理图1");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("原理图2");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("原理图2");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("原理图2");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("科目一", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("科目二");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("科目三");
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.label1.TabIndex = 20;
            this.label1.Text = "内场测试设备连接控制台";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 32);
            this.label2.TabIndex = 25;
            this.label2.Text = "科目列表";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 106);
            this.treeView1.Name = "treeView1";
            treeNode8.Name = "节点3";
            treeNode8.Text = "原理图1";
            treeNode9.Name = "节点7";
            treeNode9.Text = "原理图2";
            treeNode10.Name = "节点8";
            treeNode10.Text = "原理图2";
            treeNode11.Name = "节点9";
            treeNode11.Text = "原理图2";
            treeNode12.Name = "节点0";
            treeNode12.Text = "科目一";
            treeNode13.Name = "节点1";
            treeNode13.Text = "科目二";
            treeNode14.Name = "节点2";
            treeNode14.Text = "科目三";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14});
            this.treeView1.Size = new System.Drawing.Size(229, 362);
            this.treeView1.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(14, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 30);
            this.button1.TabIndex = 26;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(402, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(231, 98);
            this.button2.TabIndex = 26;
            this.button2.Text = "开始考核";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Name = "Form3";
            this.Text = "考核模式";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}