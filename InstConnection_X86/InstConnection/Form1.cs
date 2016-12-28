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
    public partial class Form1 : Form
    {


        Form4 form4 = new Form4();
        public Form1()
        {
            InitializeComponent();

            form4.Show();
            form4.Owner = this;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            form2.Left = this.Left;
            form2.Top = this.Top;
            form2.Width = this.Width;
            form2.Height = this.Height;
            form2.Owner = this;
            form2.Show();
            //form2.backtoform1 += new BackToForm1();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Left = this.Left;
            form3.Top = this.Top;
            form3.Width = this.Width;
            form3.Height = this.Height;
            form3.Owner = this;
            form3.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
