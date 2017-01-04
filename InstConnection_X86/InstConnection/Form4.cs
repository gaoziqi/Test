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
using System.Runtime.InteropServices;

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
                this.BackColor = Color.Transparent;
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
            画笔 = new Pen(Color.Blue, 10);
            画笔.DashStyle = DashStyle.Solid;//定义线条的样式

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
            sb.Left = center.X - sb.Width / 2;
            sb.Top = center.Y - sb.Height / 2;

            sb.BackColor = Color.Transparent;
            sb.Parent = panel1;

            sb.MouseDown += new System.Windows.Forms.MouseEventHandler(pic_MouseDown);
            sb.MouseMove += new System.Windows.Forms.MouseEventHandler(pic_MouseMove);
            sb.MouseClick += new System.Windows.Forms.MouseEventHandler(pic_MouseClick);
            sb.MouseUp += new System.Windows.Forms.MouseEventHandler(pic_MouseUp);
            if (sb.类型 == 设备.接口头)
                sb.Paint += new System.Windows.Forms.PaintEventHandler(pic_Paint);
        }

        ListView.SelectedListViewItemCollection SelectedItems = null;
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListView lisview = (ListView)sender;

            if (lisview.Items.Count == 0) return;
            SelectedItems = lisview.SelectedItems;

            lisview.DoDragDrop(lisview.SelectedItems, DragDropEffects.Move);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (SelectedItems == null) return;
            Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标
            Point formPoint = this.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标
            formPoint.X -= panel1.Location.X;
            formPoint.Y -= panel1.Location.Y;

            设备 temppic = new 设备(SelectedItems[0].Text);
            if (temppic.类型 == 设备.部件)
            {
                temppic.Image = Image.FromFile(SelectedItems[0].Name);
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

            int i = 0;
            string str = SelectedItems[0].Text + i;
            while (pic.ContainsKey(str))
            {
                str = SelectedItems[0].Text + ++i;
            }

            string dragname = SelectedItems[0].Text + --i;

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

            SelectedItems = null;
        }


        ///////////////////////////图片操作窗口////////////////////////
        int mouseX;
        int mouseY;
        int picX;
        int picY;
        string picName;

        模拟鼠标操作 鼠标 = new 模拟鼠标操作();
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            设备 picbox = (设备)sender;
            if (e.Button == MouseButtons.Left && picbox.类型 == 设备.接口头)
            {
                if (connectState == 1)
                {
                    picbox.SendToBack();
                    鼠标.模拟鼠标_Click(模拟鼠标操作.鼠标.左键, 鼠标.鼠标位置);
                    connectState++;
                }
                else
                {
                    connectState = 0;
                    pic[connectKey].BorderStyle = BorderStyle.None;
                    connectKey = "";
                }
            }
            if (connectState == 2 && e.Button == MouseButtons.Left && picbox.类型 == 设备.部件)
            {
                设备 线缆 = pic[connectKey];
                设备 部件 = picbox;
                int 线缆类型 = 0;
                bool connect = false;
                foreach (接口 ii in pic[线缆.groupName]._接口)
                {
                    if (ii.图像地址 == 线缆.name)
                        线缆类型 = ii.类型;
                }
                foreach (接口 ii in 部件._接口)
                {
                    if (ii.类型 == 线缆类型)
                    {
                        if (ii.connectName != "")
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
                    线缆.连接设备 = 部件;
                    线缆.Parent = 部件;
                    线缆.Location = new Point((int)(部件.Size.Width * 线缆.连接设备位置.X), (int)(部件.Size.Height * 线缆.连接设备位置.Y));
                    重新计算中点(线缆.groupName);
                }
                else
                    show("错误 " + picbox.Name + " 和 " + pic[connectKey].name + " 接口不符,不能连接");
                connectState = 0;
                pic[connectKey].BorderStyle = BorderStyle.None;
                connectKey = "";
            }
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            设备 picbox = (设备)sender;
            picbox.BringToFront();
            mouseX = Cursor.Position.X;
            mouseY = Cursor.Position.Y;
            picX = picbox.Left;
            picY = picbox.Top;
            picName = picbox.Name;
            if (e.Button == MouseButtons.Left && connectState == 0 && picbox.类型 == 设备.接口头)
            {
                picbox.BorderStyle = BorderStyle.Fixed3D;
                connectKey = picbox.Name;
                connectState++;
            }

        }

        void 重新计算中点(string groupName)
        {
            Size center = new Size(0, 0);
            foreach (string k1 in pic.Keys)
            {
                if (pic[k1].groupName == groupName)
                {
                    Size s = (Size)pic[k1].Location;
                    if (pic[k1].连接设备 != null)
                        s += (Size)pic[k1].连接设备.Location;
                    center += s;
                    center.Width += pic[k1].Size.Width / 2;
                    center.Height += pic[k1].Size.Height / 2;
                }
            }
            pic[groupName].groupCenter.X = center.Width / pic[groupName]._接口.Count;
            pic[groupName].groupCenter.Y = center.Height / pic[groupName]._接口.Count;

            foreach (string k1 in pic.Keys)
            {
                if (pic[k1].groupName == groupName)
                {
                    if (pic[k1].连接设备 != null)
                        pic[k1].连接设备.Invalidate();
                }
            }

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            设备 picbox=(设备)sender;
            if (picbox.连接设备 != null)
                return;
            if (picName != picbox.Name)
            {
                return;
            }
            int y = Cursor.Position.Y - mouseY + picY;
            int x = Cursor.Position.X - mouseX + picX;
            if (e.Button == MouseButtons.Left)
            {
                picbox.Top = y;
                picbox.Left = x;
                if (picbox.类型 == 设备.部件)
                {
                    foreach (接口 ii in picbox._接口)
                    {
                        if (ii.connectName != "")
                        {
                            重新计算中点(pic[ii.connectName].groupName);
                        }
                    }
                }
                else if (picbox.类型 == 设备.接口头)
                {
                    重新计算中点(picbox.groupName);
                }
            }
        }

        public void 移除设备(string name)
        {
            if (pic[name].类型 == 设备.部件)
            {
                foreach (string k in pic.Keys)
                {
                    if (pic[k].连接设备 != null && pic[k].连接设备.Name == name)
                    {
                        pic[k].Location = Point.Add(pic[k].连接设备.Location, (Size)pic[k].Location);

                        pic[k].连接设备 = null;
                        pic[k].Parent = panel1;
                    }
                }
            }
            else if (pic[name].类型 == 设备.接口头)
            {
                if (pic[name].连接设备 != null) 
                {
                    pic[name].连接设备.Invalidate();
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
        int connectState=0;//0接入之前，1第一次接入

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;   

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            设备 picbox = (设备)sender;
            if (e.Button==MouseButtons.Right)
            {
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
                    SendMessage(panel1.Handle, WM_SETREDRAW, 0, IntPtr.Zero); 
                    foreach (string k in name)
                        移除设备(k);
                    SendMessage(panel1.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                    panel1.Invalidate();
                }          
            }
            
        }

        public Pen 画笔;

        private void pic_Paint(object sender, PaintEventArgs e) 
        {
            设备 pic1 = (设备)sender;
            if (pic1.连接设备 != null)
            {
                Pen p = (Pen)画笔.Clone();
                Graphics g = Graphics.FromHwnd(pic1.连接设备.Handle);
                Point point = pic[pic1.groupName].groupCenter;
                point.X -= pic1.连接设备.Location.X;
                point.Y -= pic1.连接设备.Location.Y;
                if (pic1.连接设备.Bounds.Contains(pic[pic1.groupName].groupCenter))
                {
                    foreach (string k in pic.Keys)
                    {
                        if (pic[k].groupName == pic1.groupName)
                        {
                            if (pic[k].连接设备==null)
                                g.DrawLine(p,
                                new Point(pic[k].Location.X - pic1.连接设备.Location.X + (int)pic[k].Size.Width / 2,
                                    pic[k].Location.Y - pic1.连接设备.Location.Y + (int)pic[k].Size.Height / 2), point);
                            else
                                g.DrawLine(p,
                                new Point(pic[k].Location.X + pic[k].连接设备.Location.X - pic1.连接设备.Location.X + (int)pic[k].Size.Width / 2,
                                    pic[k].Location.Y + pic[k].连接设备.Location.Y - pic1.连接设备.Location.Y + (int)pic[k].Size.Height / 2), point);
                        }
                    }
                }
                else
                    g.DrawLine(p, new Point(pic1.Location.X + (int)pic1.Size.Width / 2, pic1.Location.Y + (int)pic1.Size.Height / 2), point);

                

                g.Dispose();
                p.Dispose();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.Refresh();
            show(connectState.ToString());
            Panel panel = (Panel)sender;
            Pen p = (Pen)画笔.Clone();
            Graphics g = Graphics.FromHwnd(panel.Handle);
            g.Clear(this.BackColor);
            
            //画实现
            foreach (string k in pic.Keys)
            {
                if (pic[k].groupName != "")
                {
                    Point point = pic[k].Location;
                    point.X += pic[k].Size.Width / 2;
                    point.Y += pic[k].Size.Height / 2;
                    if (pic[k].连接设备 != null)
                    {
                        point.X += pic[k].连接设备.Location.X;
                        point.Y += pic[k].连接设备.Location.Y;
                    }
                    g.DrawLine(p, point, pic[pic[k].groupName].groupCenter);
                }
            }
            g.Dispose();
            p.Dispose();
        }
    }
}
