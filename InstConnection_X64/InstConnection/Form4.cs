using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;
using 高子奇;
using System.Collections.ObjectModel;

namespace InstConnection
{
    public partial class Form4 : Form
    {
        public class 接口 
        {
            public Int32 类型;
            public PointF 位置;
            public String 图像地址; //图像名称
            public string connectName; //连接线缆名
            public 接口(Int32 类型, float x, float y, string url)
            {
                this.类型 = new Int32();
                this.位置 = new PointF();           
                this.类型 = 类型;
                this.位置.X = x;
                this.位置.Y = y;
                this.图像地址 = url;
                this.connectName = "";
            }
        }

        public class 设备 : PictureBox
        {
            public static int 部件 = 0;
            public static int 线缆 = 1;
            public static int 接口头 = 2;

            public int 类型;
            public string name;
            //部件属性
            public Collection<接口> _接口 = new Collection<接口>();
            //线缆属性
            public Point groupCenter;
            //接口头属性
            public string groupName;
            public 设备 连接设备;
            public PointF 连接设备位置;
            public 设备(string name, string groupName = "")
            {
                base.Name = name;
                this.name = name;
                SQLiteDBHelper db = new SQLiteDBHelper(@"sql.db");
                DataTable dt = sqlite_gzq.SelectData(db, "interface", "name", name);
                if (dt.Rows.Count>0)
                {
                    this.类型 = Convert.ToInt32(dt.Rows[0]["type"]);
                    foreach (DataRow row in dt.Rows)
                        _接口.Add(new 接口(Convert.ToInt32(row["interface_type"]), Convert.ToSingle(row["position_x"]), Convert.ToSingle(row["position_y"]), row["picture_url"].ToString()));
                }
                this.groupName = groupName;
                if (groupName != "") this.类型 = 设备.接口头;
                this.groupCenter = new Point();
                this.连接设备 = null;
            }
        }

        public void show(string msg) 
        {
            label_state.Text = "状态：" + msg;
        }

        //图片显示框
        Dictionary<string, 设备> pic = new Dictionary<string, 设备>();


        public Form4()
        {
            InitializeComponent();

            //获取路径下所有图片
            DirectoryInfo di1 = new DirectoryInfo(@"附件及电缆");
            DirectoryInfo di2 = new DirectoryInfo(@"测试设备");
            DirectoryInfo di3 = new DirectoryInfo(@"机载设备");


            int imagelistindex = 0;


            //载入附件及电缆


            listView1.Items.Clear();
            listView1.LargeImageList = imageList1;
            listView1.View = View.LargeIcon;

            listView1.BeginUpdate();

            foreach (FileInfo fi in di1.GetFiles())
            {
                if (fi.Name.ToUpper().EndsWith(".BMP") || fi.Name.ToUpper().EndsWith(".PNG") || fi.Name.ToUpper().EndsWith(".JPG"))
                {
                    imageList1.Images.Add(Image.FromFile(fi.FullName));
                    imageList1.Images.SetKeyName(imagelistindex,fi.Name);   //用图片名称作为key

                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = imagelistindex;
                    lvi.Name = fi.FullName; //图片路径作为list列表项名字
                    lvi.Text = fi.Name;     //图片名称作为list显示文字
                    listView1.Items.Add(lvi);
                    imagelistindex++;
                }
            }
            listView1.EndUpdate();

            /////////////////////////////////////载入测试设备//////////////////////////////
            imagelistindex = 0;

            listView2.Items.Clear();
            listView2.LargeImageList = imageList2;
            listView2.View = View.LargeIcon;
            listView2.BeginUpdate();

            foreach (FileInfo fi in di2.GetFiles())
            {
                if (fi.Name.ToUpper().EndsWith(".BMP") || fi.Name.ToUpper().EndsWith(".PNG") || fi.Name.ToUpper().EndsWith(".JPG"))
                {
                    imageList2.Images.Add(Image.FromFile(fi.FullName));
                    imageList2.Images.SetKeyName(imagelistindex, fi.Name);   //用图片路径作为key


                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = imagelistindex;
                    lvi.Name = fi.FullName; //图片路径作为list列表项名字
                    lvi.Text = fi.Name;     //图片名称作为list显示文字
                    listView2.Items.Add(lvi);

                    imagelistindex++;
                }
            }

            listView2.EndUpdate();

            /////////////////////////////////////载入机载设备////////////////////////////////////
            imagelistindex = 0;
            listView3.Items.Clear();
            listView3.LargeImageList = imageList3;
            listView3.View = View.LargeIcon;
            listView3.BeginUpdate();
            foreach (FileInfo fi in di3.GetFiles())
            {
                if (fi.Name.ToUpper().EndsWith(".BMP") || fi.Name.ToUpper().EndsWith(".PNG") || fi.Name.ToUpper().EndsWith(".JPG"))
                {
                    imageList3.Images.Add(Image.FromFile(fi.FullName));
                    imageList3.Images.SetKeyName(imagelistindex, fi.Name);   //用图片路径作为key

                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = imagelistindex;
                    lvi.Name = fi.FullName; //图片路径作为list列表项名字
                    lvi.Text = fi.Name;     //图片名称作为list显示文字
                    listView3.Items.Add(lvi);

                    imagelistindex++;
                }
            }

            listView3.EndUpdate();

            //listView1
            imageList1.ImageSize = new Size(64, 64);
            imageList2.ImageSize = new Size(64, 64);
            imageList3.ImageSize = new Size(64, 64);

            panel1.AllowDrop = true;    //设置panel允许拖动

        }

        /// <summary>
        /// 列表窗口
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        public void 添加设备(设备 temppic, bool show = true)
        {
            //添加图片
            int i = 0;
            string str = temppic.Name + i;
            while (pic.ContainsKey(str))
            {
                str = temppic.Name + ++i;
            }
            temppic.Name = str;

            if (show)
            {
                temppic.Visible = false;
                temppic.SizeMode = PictureBoxSizeMode.StretchImage;
                temppic.Width = temppic.Image.Width;
                temppic.Height = temppic.Image.Height;
                //panel1.
                temppic.Parent = panel1;
                //this.Controls.Add(temppic);   //添加到窗体
            }

            pic.Add(temppic.Name, temppic);
        }

        public void 显示设备(设备 sb, Point center)
        {
            show("添加 " + sb.Name);

            sb.Left = center.X - sb.Width / 2;
            sb.Top = center.Y - sb.Height / 2;

            sb.Parent = panel1;

            sb.MouseDown += new System.Windows.Forms.MouseEventHandler(pic_MouseDown);
            sb.MouseMove += new System.Windows.Forms.MouseEventHandler(pic_MouseMove);
            sb.MouseClick += new System.Windows.Forms.MouseEventHandler(pic_MouseClick);
            sb.MouseWheel += new System.Windows.Forms.MouseEventHandler(pic_MouseWheel);
        }

        ListView lisview;
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {

            lisview = (ListView)sender;

            if (lisview.Items.Count == 0) return;
            int index = lisview.SelectedItems[0].Index;

            设备 temppic = new 设备(lisview.SelectedItems[0].Text);
            if (temppic.类型 == 设备.部件)
            {
                temppic.Image = Image.FromFile(lisview.SelectedItems[0].Name);
                添加设备(temppic);
            }
            else if (temppic.类型 == 设备.线缆)
            {              
                添加设备(temppic, false);
                foreach (接口 ii in temppic._接口) 
                {
                    设备 temppic1 = new 设备(ii.图像地址, temppic.Name);
                    temppic1.Image = Image.FromFile(ii.图像地址);
                    添加设备(temppic1);
                }
            }

            lisview.DoDragDrop(lisview.SelectedItems, DragDropEffects.Move);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标
            Point formPoint = this.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
            formPoint.X -= panel1.Location.X;
            formPoint.Y -= panel1.Location.Y;
            int i = 0;
            string str = lisview.SelectedItems[0].Text + i;
            while (pic.ContainsKey(str))
            {
                str = lisview.SelectedItems[0].Text + ++i;
            }
            
            string dragname = lisview.SelectedItems[0].Text + --i;
            try
            {
                pic[dragname].Visible = true;
            }
            catch
            {
                Console.WriteLine(dragname + "is not found.");
                return;
            }
            if (pic[dragname].类型 == 设备.部件)
                显示设备(pic[dragname], formPoint);
            else if (pic[dragname].类型 == 设备.线缆)
            {
                pic[dragname].groupCenter = formPoint;
                Point[] centers = new Point[pic[dragname]._接口.Count];
                int dist = 60;
                //添加初始坐标,目前只添加两个接口的情况
                if (pic[dragname]._接口.Count == 2) 
                {
                    centers[0].X = formPoint.X - dist;
                    centers[1].X = formPoint.X + dist;
                    centers[0].Y = formPoint.Y;
                    centers[1].Y = formPoint.Y;
                }
                i = 0;
                foreach (string k in pic.Keys)
                {
                    if (pic[k].groupName == dragname)
                    {
                        pic[k].Visible = true;
                        显示设备(pic[k], centers[i++]);
                    }
                }
            }
        }


        ///////////////////////////图片操作窗口////////////////////////
        int mouseX;
        int mouseY;
        int picX;
        int picY;
        int picRatio;



        private void pic_MouseDown(object sender,MouseEventArgs e)
        {
            PictureBox picbox = (PictureBox)sender;
            show("移动 " + picbox.Name);
            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
            picX = picbox.Left;
            picY = picbox.Top;
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            设备 picbox=(设备)sender;
            if (picbox.连接设备 != null)
                return;
            int y = Cursor.Position.Y - mouseY + picY;
            int x = Cursor.Position.X - mouseX + picX;
            if (e.Button == MouseButtons.Left)
            {
                picbox.Top = y;
                picbox.Left = x;              
            }
        }
        private void pic_MouseWheel(object sender, MouseEventArgs e)
        {
            PictureBox picbox = (PictureBox)sender;
            picRatio = picbox.Width / picbox.Height;

            int wheelspeed = e.Delta / 5;  //设置滚轮速度


            //if (picbox.Width < 30) 
            if (picbox.Height < 30)
            {
                picbox.Height = 30;
                picbox.Width = 30 * picRatio;
            }

            picbox.Width += wheelspeed;
            picbox.Height += wheelspeed/picRatio;

            picbox.Top -= wheelspeed /(2* picRatio);
            picbox.Left -= wheelspeed / 2;
        }

        public void 移除设备(string name)
        {
            if (pic[name].类型 == 设备.部件)
            {
                foreach (string k in pic.Keys)
                {
                    if (pic[k].连接设备 != null && pic[k].连接设备.Name == name)
                    {
                        pic[k].连接设备 = null;
                    }
                }
            }
            else if (pic[name].类型 == 设备.接口头)
            {
                if (pic[name].连接设备 != null) 
                {
                    foreach (接口 ii in pic[name].连接设备._接口)
                    {
                        if (ii.connectName == name)
                        {
                            ii.connectName = "";
                            break;
                        }
                    }
                }
            }
            pic[name].Dispose();
            pic.Remove(name);
            GC.Collect();
        }

        string connectKey = "";
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            设备 picbox = (设备)sender;
            if (e.Button==MouseButtons.Right)
            {
                //this.Controls.Remove(pic[picbox.Name]);
                if (picbox.groupName == "")
                    移除设备(picbox.Name);
                else
                {
                    List<string> name = new List<string>();
                    name.Add(picbox.groupName);
                    foreach (string k in pic.Keys)
                    {
                        if (pic[k].groupName == picbox.groupName)
                            name.Add(k);
                    }
                    foreach (string k in name)
                        移除设备(k);
                }          
            }
            if (e.Button == MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (connectKey == "")
                    {
                        picbox.BorderStyle = BorderStyle.Fixed3D;
                        connectKey = picbox.Name;
                    }
                    else
                    {
                        设备 线缆;
                        设备 部件;
                        pic[connectKey].BorderStyle = BorderStyle.None;
                        if (picbox.groupName == "" && pic[connectKey].groupName != "")
                        {
                            部件 = picbox;
                            线缆 = pic[connectKey];
                        }
                        else if (picbox.groupName == "" && pic[connectKey].groupName != "")
                        {
                            线缆 = picbox;
                            部件 = pic[connectKey];
                        }
                        else
                        {
                            show("错误 " + picbox.Name + " 和 " + pic[connectKey].name + " 同类型,不能连接");
                            connectKey = "";
                            return;
                        }
                        int 线缆类型=0;
                        bool connect=false;
                        foreach (接口 ii in pic[线缆.groupName]._接口)
                        {
                            if(ii.图像地址 == 线缆.name)
                                线缆类型 = ii.类型;
                        }
                        foreach (接口 ii in 部件._接口)
                        {
                            if (ii.类型 == 线缆类型)
                            {
                                if (ii.connectName!="")
                                {
                                    show("错误 " + picbox.Name + " 和 " + pic[connectKey].name + " 接口已占用,不能连接");
                                    connectKey = "";
                                    return;
                                }
                                ii.connectName = 线缆.Name;
                                线缆.连接设备位置 = ii.位置;
                                connect = true;
                                break;
                            }
                        }
                        if (connect)
                        {
                            show(picbox.Name + " 和 " + pic[connectKey].name + " 连接成功");
                            线缆.Location = new Point(
                                (int)(部件.Location.X + 部件.Size.Width * 线缆.连接设备位置.X),
                                (int)(部件.Location.Y + 部件.Size.Height * 线缆.连接设备位置.Y));
                            线缆.连接设备 = 部件;
                            线缆.BringToFront();
                        }
                        else
                            show("错误 " + picbox.Name + " 和 " + pic[connectKey].name + " 接口不符,不能连接");
                        connectKey = "";
                    }          
                }
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.Refresh();

            Panel panel = (Panel)sender;
            Pen p = new Pen(Color.Blue, 10);//设置笔的粗细为,颜色为蓝色
            Graphics g = Graphics.FromHwnd(panel.Handle);

            g.Clear(this.BackColor);
            p.DashStyle = DashStyle.Solid;//定义线条的样式
            //画实现
            Dictionary<string,bool> name = new Dictionary<string, bool>();
            foreach (string k in pic.Keys)
            {
                if (pic[k].groupName != "")
                {
                    //中点计算，卡顿去掉                 
                    if (!name.ContainsKey(pic[k].groupName))
                    {
                        name.Add(pic[k].groupName, true);
                        Size center = new Size(0,0);
                        foreach (string k1 in pic.Keys)
                        {
                            if (pic[k1].groupName == pic[k].groupName)
                            {
                                center += (Size)pic[k1].Location;
                                center.Width += pic[k1].Size.Width / 2;
                                center.Height += pic[k1].Size.Height / 2;
                            }
                        }
                        pic[pic[k].groupName].groupCenter.X = center.Width / pic[pic[k].groupName]._接口.Count;
                        pic[pic[k].groupName].groupCenter.Y = center.Height / pic[pic[k].groupName]._接口.Count;
                    }
                    //中点计算，卡顿去掉
                    g.DrawLine(p, new Point(pic[k].Location.X + pic[k].Size.Width / 2, pic[k].Location.Y + pic[k].Size.Height / 2), pic[pic[k].groupName].groupCenter);
                    if (pic[k].连接设备 != null) 
                    {
                        pic[k].Location = Location = new Point(
                            (int)(pic[k].连接设备.Location.X + pic[k].连接设备.Size.Width * pic[k].连接设备位置.X),
                            (int)(pic[k].连接设备.Location.Y + pic[k].连接设备.Size.Height * pic[k].连接设备位置.Y));
                    }
                }
            }
            g.Dispose();
            p.Dispose();

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach(var pp in pic)
            {
                pp.Value.BorderStyle = BorderStyle.None;
            }
        }
    }
}
