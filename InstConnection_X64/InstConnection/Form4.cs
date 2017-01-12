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
            public Size origin;
            //部件属性
            public Collection<接口> _接口 = new Collection<接口>();
            //线缆属性
            public Point groupCenter;
            public string colorName;
            //接口头属性
            public string groupName;
            public 设备 连接设备;
            public PointF 连接设备位置;
            public 设备(SQLiteDBHelper db,string name, string groupName = "")
            {
                base.Name = name;
                this.name = name;
                DataTable dt = sqlite_gzq.SelectData(db, "interface", "name", name);
                if (dt.Rows.Count>0)
                {
                    this.类型 = Convert.ToInt32(dt.Rows[0]["type"]);
                    this.colorName = dt.Rows[0]["remark"].ToString();
                    foreach (DataRow row in dt.Rows)
                        _接口.Add(new 接口(Convert.ToInt32(row["interface_type"]), Convert.ToSingle(row["position_x"]), Convert.ToSingle(row["position_y"]), row["picture_url"].ToString()));
                }
                this.groupName = groupName;
                if (groupName != "") this.类型 = 设备.接口头;
                this.groupCenter = new Point();
                this.连接设备 = null;
                this.BackColor = Color.Transparent;
                this.origin = this.Size;
            }
        }

        public void show(string msg) 
        {
            label_state.Text = "状态：" + msg;
        }

        //图片显示框
        Dictionary<string, 设备> pic = new Dictionary<string, 设备>();

        SQLiteDBHelper db = new SQLiteDBHelper(@"sql.db");

        //表名
        String t_Name = "ceshi1";
        
        //缩放
        Size origin_panel1;
        double[] 缩放倍数=new double[]{0.4, 0.5, 0.6};
        int 当前倍数 = 1;
        int 之前倍数 = 1;
        public Form4()
        {
            InitializeComponent();
            画笔 = new Pen(Color.Blue, 10);
            画笔.DashStyle = DashStyle.Solid;//定义线条的样式
            origin_panel1 = panel1.Size;
            panel1.MouseWheel+=new MouseEventHandler(panel1_MouseWheel);
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

        void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            Panel p = sender as Panel; 
            if (e.Delta > 0)
            {
                当前倍数++;
                if (当前倍数 < 缩放倍数.Length)
                    缩放设备(缩放倍数[当前倍数]);
                else
                    当前倍数 = 缩放倍数.Length-1;
                之前倍数 = 当前倍数;
            }
            else
            {
                当前倍数--;
                if (当前倍数 > -1)
                    缩放设备(缩放倍数[当前倍数]);
                else
                    当前倍数 = 0;
                之前倍数 = 当前倍数;
            }
        }

        public Size SP_缩放(Size s, double 倍数)
        {
            return new Size((int)(s.Width * 倍数), (int)(s.Height * 倍数));
        }

        public Point SP_缩放(Point s, double 倍数)
        {
            return new Point((int)(s.X * 倍数), (int)(s.Y * 倍数));
        }

        public Point SP_缩放(Point s, Point Location ,double 倍数)
        {
            return new Point((int)((s.X + Location.X) * 倍数) - Location.X,
                (int)((s.Y + Location.Y) * 倍数) - Location.Y);
            //return new Point((int)(s.X * 倍数), (int)(s.Y * 倍数));
        }

        public void 缩放设备(double 倍数)
        {
            foreach (string k1 in pic.Keys)
            {
                pic[k1].Size = SP_缩放(pic[k1].origin, 倍数);
                double t = 倍数 / 缩放倍数[之前倍数];
                if (pic[k1].Parent == panel1)
                {
                    pic[k1].groupCenter = SP_缩放(pic[k1].groupCenter, pic[k1].Parent.Location, t);
                    pic[k1].Location = SP_缩放(pic[k1].Location, pic[k1].Parent.Location, t);
                }
                else if (pic[k1].Parent != null)
                {
                    pic[k1].groupCenter = SP_缩放(pic[k1].groupCenter, t);
                    pic[k1].Location = SP_缩放(pic[k1].Location, t);
                }
                else 
                {
                    pic[k1].groupCenter = SP_缩放(pic[k1].groupCenter, panel1.Location, t);
                }
            }
        }

        public Padding Panel1自适应(Point Location, Size Size, bool mode = true)
        {
            Padding result = new Padding();
            if (Location.X > -2 && Location.Y > -2)
                Location = new Point(-2, -2);
            else if (Location.Y > -2)
                Location = new Point(panel1.Location.X, -2);
            else if (Location.X > -2)
                Location = new Point(-2, Location.Y);
            int x = Location.X + Size.Width - panel2.Width + 2;
            int y = Location.Y + Size.Height - panel2.Height + 2;
            Size s = new Size(0, 0);
            if (x < 0 && y < 0)
                s += new Size(-x, -y);
            else if (y < 0)
                s += new Size(0, -y);
            else if (x < 0)
                s += new Size(-x, 0);
            if (mode)
                Size += s;
            else
                Location += s;
            result.Top = Location.Y;
            result.Left = Location.X;
            result.Bottom = Location.Y + Size.Height;
            result.Right = Location.X + Size.Width;
            return result;
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
                temppic.origin = new Size(temppic.Image.Width, temppic.Image.Height);
                temppic.Width = (int)(temppic.origin.Width * 缩放倍数[当前倍数]);
                temppic.Height = (int)(temppic.origin.Height * 缩放倍数[当前倍数]);
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

            设备 temppic = new 设备(db,SelectedItems[0].Text);
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
                    设备 temppic1 = new 设备(db,ii.图像地址, temppic.Name);
                    temppic1.Image = Image.FromFile(ii.图像地址);
                    添加设备(temppic1);
                }
            }

            string dragname = temppic.Name;

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
                int i = 0;
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
        string picName = "";

        模拟鼠标操作 鼠标 = new 模拟鼠标操作();
        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                设备 picbox = (设备)sender;
                picName = "";
                if (connectKey != "" && picbox.类型 == 设备.接口头)
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
                else if (connectKey!="" && connectState == 2 && picbox.类型 == 设备.部件)
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
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                设备 picbox = (设备)sender;
                if (picbox.连接设备 != null)
                    return;
                mouseX = Cursor.Position.X;
                mouseY = Cursor.Position.Y;
                picX = picbox.Left;
                picY = picbox.Top;
                picName = picbox.Name;
                if (connectState == 0 && picbox.类型 == 设备.接口头)
                {
                    picbox.BringToFront();
                    picbox.BorderStyle = BorderStyle.Fixed3D;
                    connectKey = picbox.Name;
                    connectState++;
                }
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
            if (e.Button == MouseButtons.Left)
            {
                设备 picbox = (设备)sender;
                if (picbox.连接设备 != null)
                    return;
                if (picbox.Name != picName)
                    return;
                picbox.Top = Cursor.Position.Y - mouseY + picY;
                picbox.Left = Cursor.Position.X - mouseX + picX;
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
                p.Color = Color.FromName(pic[pic1.groupName].colorName);
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
                            if (pic[k].连接设备 == null)
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
            this.panel1.Focus();
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
                    p.Color = Color.FromName(pic[pic[k].groupName].colorName);
                    g.DrawLine(p, point, pic[pic[k].groupName].groupCenter);
                }
            }
            g.Dispose();
            p.Dispose();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left&& ModifierKeys==Keys.Control) 
            {
                Point p = Control.MousePosition;
                p.Offset(panel1XY.X - panel2.Location.X, panel1XY.Y - panel2.Location.Y);
                Padding p1 = Panel1自适应(this.PointToClient(p), panel1.Size, false);
                panel1.Location = new Point(p1.Left,p1.Top);
            }
        }

        private Point panel1XY;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {
                panel1XY = new Point(-e.X, -e.Y);
            }
        }

        //匹配
        public class check设备
        {
            public string realname;
            public string name;
            public string connectrealname;
            public check设备(string realname, string name, string connectrealname)
            {
                this.realname = realname;
                this.name = name;
                this.connectrealname = connectrealname;
            }
        }
        List<check设备> checklist = new List<check设备>();
        Dictionary<check设备, string> check = new Dictionary<check设备, string>();
        Dictionary<string, bool> check_used = new Dictionary<string, bool>();
        private bool check_match()
        {
            string str = "";
            foreach (string k in check_used.Keys)
            {
                if (!check_used[k])
                {
                    str = k;
                    break;
                }
            }
            if (str == "")
                return true;
            foreach (check设备 k in checklist)
            {
                //类型相同
                if (check[k] == "" && k.name == pic[str].name)
                {
                    //无连接设备
                    if (k.connectrealname == "")
                    {
                        if (pic[str].连接设备 != null)
                            continue;
                        check[k] = str;
                        check_used[str] = true;
                        if (check_match())
                            return true;
                        check[k] = "";
                        check_used[str] = false;
                    }
                    //有连接设备
                    else
                    {
                        if (pic[str].连接设备 == null)
                            continue;
                        foreach (check设备 k1 in checklist)
                        {
                            if (k1.realname == k.connectrealname)
                            {
                                //check未匹配
                                if (check[k1] == "")
                                {
                                    check[k] = str;
                                    check_used[str] = true;
                                    check[k1] = pic[str].连接设备.Name;
                                    check_used[pic[str].连接设备.Name] = true;
                                    if (check_match())
                                        return true;
                                    check[k] = "";
                                    check_used[str] = false;
                                    check[k1] = "";
                                    check_used[pic[str].连接设备.Name] = false;
                                }
                                //check已匹配
                                else
                                {
                                    if (check[k1] == pic[str].连接设备.Name)
                                    {
                                        check[k] = str;
                                        check_used[str] = true;
                                        if (check_match())
                                            return true;
                                        check[k] = "";
                                        check_used[str] = false;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //清空
            checklist.Clear(); check.Clear(); check_used.Clear();
            List<string> type_name = new List<string>();
            Dictionary<string, int> type_num = new Dictionary<string, int>();
            foreach (string k in pic.Keys)
            {
                if (!type_name.Contains(pic[k].name))
                {
                    type_name.Add(pic[k].name);
                    DataTable dt = sqlite_gzq.SelectData(db, t_Name, "name", pic[k].name);
                    type_num.Add(pic[k].name, dt.Rows.Count - 1);
                    foreach (DataRow row in dt.Rows)
                    {
                        check设备 t = new check设备
                            (row["realname"].ToString(),
                            row["name"].ToString(),
                            row["connectrealname"].ToString());
                        checklist.Add(t);
                        check.Add(t, "");
                    }
                }
                else
                {
                    type_num[pic[k].name]--;
                }
                if(type_num[pic[k].name]<0)
                {
                    MessageBox.Show("错误："+pic[k].name+"数量过多");
                    return;
                }
                check_used.Add(pic[k].Name, false);
            }
            if (pic.Count!=sqlite_gzq.ReadData(db, t_Name).Rows.Count)
            {
                MessageBox.Show("错误：设备或线缆数量不正确");
                return;
            }
            if(check_match())
                MessageBox.Show("恭喜连接正确");
            else
                MessageBox.Show("错误：连接方式不正确");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String sql = String.Format(@"Drop table if exists '{0}';
                CREATE TABLE '{0}'(
                'realname' CHAR(50), 
                'name' CHAR(50), 
                'type' SMALLINT,
                'connectrealname' CHAR(50),
                'connectname' CHAR(50), 
                'remark' TEXT);", t_Name);
            try
            {
                db.ExecuteNonQuery(sql, null);
                foreach (string k in pic.Keys)
                {
                    sql=@"Insert into '{0}' values ('{1}','{2}',{3},'{4}','{5}','{6}')";
                    if(pic[k].类型 == 设备.部件)
                    {
                        sql=String.Format(sql, t_Name, pic[k].Name, pic[k].name, pic[k].类型, null, null,null);
                    }
                    else if(pic[k].类型 == 设备.接口头)
                    {
                        if(pic[k].连接设备==null)
                            sql=String.Format(sql, t_Name, pic[k].Name, pic[k].name, pic[k].类型, null, null,null);
                        else
                            sql = String.Format(sql, t_Name, pic[k].Name, pic[k].name, pic[k].类型, pic[k].连接设备.Name, pic[k].连接设备.name, null);
                    }
                    else if(pic[k].类型 == 设备.线缆)
                    {
                        sql = String.Format(sql, t_Name, pic[k].Name, pic[k].name, pic[k].类型, null, null, null);
                    }
                    db.ExecuteNonQuery(sql, null);
                }
            }
            catch
            {
                MessageBox.Show("数据库异常，保存失败");
            }
        }
    }
}
