/*
 * 由VS2017创建。
 * 用户： guyue
 * 日期: 2018/8/4
 * 时间: 14:53
 * 
 */
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Teigha.Geometry;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using Dwg2Json.Util;
using System.Data;
using Microsoft.VisualBasic;

namespace Dwg2Json
{
    public partial class FormMain : Form
    {
        //定义原图像归零参数
        private double x, y;
        //定义实际图像归零参数
        private double a, b;
        //定义图像缩放系数
        private int mul;  
        private List<JsonPolyline> finjp = new List<JsonPolyline>();
        //归零后巷道信息
        public List<JsonPolyline> tmpjp;
        //归零后的图像信息
        public List<JsonLine> ll = new List<JsonLine>();
        public static Boolean changehd = false;
        private List<Point3d> point = new List<Point3d>();
        private Boolean changeLine = false;
        private double lx, ly, la, lb;
        Boolean hd = false;
        public FormMain()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);          
            Init();
            ReadOldInfo();
        }
        /// <summary>
        /// 初始化参数信息
        /// </summary>
        void Init()
        {
            Helper.Llinit();
            Helper.Lpinit();
            this.colorlist.Items.Clear();
            this.pictureBox1.MouseDown += pictureBox1_MouseDown;
            this.pictureBox1.MouseMove += pictureBox1_MouseMove;
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            this.opencadT.Click += OpenCAD;
            this.openjsonT.Click += OpenJSON;
            this.savejsonT.Click += SaveJSON;
            this.showjsonT.Click += ShowJSON;
            this.hxt.Click += HX;
            this.createjsonT.Click += CreateJSON;
            this.changejsonT.Click += ChangeJSON;
            this.goreturnT.Click += GoReturn;
            this.emptyT.Click += Empty;
            this.layshow.Click+= LayeringShow;
            this.ActiveControl = this.pictureBox1;
            this.colorlist.Hide();
            this.button7.Hide();
            ls = false;
        }
        /// <summary>
        /// 读取缓存信息
        /// </summary>
        void ReadOldInfo()
        {
            string fname = Path.Combine(Directory.GetCurrentDirectory(), "listjsonpyline.txt");
            Helper.Json2List(fname, 1,null);
            fname = Path.Combine(Directory.GetCurrentDirectory(), "listjsonline.txt");
            Helper.Json2List(fname, 0, this.colorlist);
            if (Helper.GetListLine().Count != 0)
                InitZero();
        }
        /// <summary>
        /// 初始化归零参数
        /// </summary>
        void InitZero()
        {
            mul = 1;
            x = Helper.GetListLine()[0].x1;
            y = Helper.GetListLine()[0].y1;
            a = x; b = y;
            ll = Helper.ComputeLl(Helper.GetListLine(), a, b);           
            tmpjp = Helper.ComputeLl(Helper.GetlistPolyline(), a, b);            
            Draw(ll, tmpjp);
        }  /// <summary>
        /// 打开cad事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCAD(object sender, EventArgs e)
        {
            if (!this.wj.Enabled)
            {
                return;
            }
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                Init();
                string fname = openFileDlg.FileName;
                Helper.ReadCAD(fname,this.colorlist);               
            }
            if (Helper.GetListLine().Count != 0)
                InitZero();
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 画出解析完的线段
        /// </summary>
        /// <param name="list">线段集合</param>
        private void Draw(List<JsonLine> list, List<JsonPolyline> listjp)
        {
            Bitmap bmp = new Bitmap(800, 800);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Color mycolor;
            //创建画笔(颜色)
            Pen npen;
            if (list != null)
            {
                foreach (var tmp in list)
                {
                    //int index = this.colorlist.Items.IndexOf(tmp.color.ToString());
                    //if (index == -1) continue;
                    //if (!this.colorlist.GetItemChecked(index)) continue;
                    mycolor = ColorTranslator.FromHtml(tmp.color);
                    npen = new Pen(mycolor);
                    if (tmp.enable)
                        npen = new Pen(Color.LightGray, 3);
                    Point n1 = new Point((int)tmp.x1, (int)tmp.y1);
                    Point n2 = new Point((int)tmp.x2, (int)tmp.y2);
                    try { g.DrawLine(npen, n1, n2); }
                    catch (System.Exception e)
                    {
                        continue;
                    }
                }
            }
            if (listjp != null)
            {
                foreach (var tmp in listjp)
                {
                    mycolor = Color.Gray;
                    npen = new Pen(mycolor, 5);
                    GraphicsPath path = new GraphicsPath();
                    for (int i = 0; i < tmp.coordinates.Length; i++)
                    {
                        if (i < tmp.coordinates.Length - 1)
                        {
                            Point p1 = new Point((int)tmp.coordinates[i].X, (int)tmp.coordinates[i].Y);
                            Point p2 = new Point((int)tmp.coordinates[i + 1].X, (int)tmp.coordinates[i + 1].Y);
                            path.AddLine(p1, p2);//使每个点都用直线连接
                        }
                    }
                    if (path.PointCount > 1)
                    {
                        g.DrawPath(npen, path);
                        g.DrawPath(new Pen(Color.Black, 1), path);
                    }
                }
            }
            this.pictureBox1.CreateGraphics().DrawImage(bmp, 0, 0);
            g.Dispose();
        }
        /// <summary>
        /// 保存json图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveJSON(object sender, EventArgs e)
        {
            if (!this.wj.Enabled)
            {
                return;
            }
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                string fname = saveFileDlg.FileName;
                string str = Helper.List2Json(Helper.GetlistPolyline());
                //str = str.Replace("#", "\r\n\t");
                File.WriteAllText(fname, str);
                MessageBox.Show("保存成功！");
            }
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 鼠标移动事件(显示坐标）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double i = this.PointToClient(Control.MousePosition).X - this.pictureBox1.Location.X;
            double j = this.PointToClient(Control.MousePosition).Y - this.pictureBox1.Location.Y;
            i += a;
            j += b;            
            textBox1.Text = "x:" + i + "    y:" + j;
        }        
        /// <summary>
        /// 鼠标按下事件（描点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown2(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {                
                Point3d p = new Point3d();                
                double sx = this.PointToClient(Control.MousePosition).X - this.pictureBox1.Location.X;
                double sy = this.PointToClient(Control.MousePosition).Y - this.pictureBox1.Location.Y;
                if (Helper.SurPoint(new Point((int)sx, (int)sy), ll) != null)
                    p = (Point3d)Helper.SurPoint(new Point((int)sx, (int)sy), ll);
                else
                    return;
                p = Helper.Goreturn(p, mul, a, b);
                point.Add(p);
                if (point.Count > 0)
                 //   this.button1.Enabled = true;
                SubDraw();
            }
            else
            {
                if (point.Count < 2)
                {
                    MessageBox.Show("必须描两个以上的点");
                    return;
                }
                Form2 from1 = new Form2(new JsonPolyline(point));
                from1.button1.Enabled = false;
                DialogResult dr = from1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    for (int j = 0; j < point.Count - 1; j++)
                    {
                        for (int i = 0; i < ll.Count; i++)
                        {
                            if (ll[i].x1 == point[j].X && ll[i].y1 == point[j].Y && ll[i].x2 == point[j + 1].X && ll[i].y2 == point[j + 1].Y)
                            {
                                ll[i].enable = true;
                                Helper.LlChange(i);
                                break;
                            }
                            if (ll[i].x2 == point[j].X && ll[i].y2 == point[j].Y && ll[i].x1 == point[j + 1].X && ll[i].y1 == point[j + 1].Y)
                            {
                                ll[i].enable = true;
                                Helper.LlChange(i);
                                break;
                            }
                        }
                    }
                    point.Clear();
                   // button1.Enabled = false;
                    Draw(ll, tmpjp);
                }
                else
                {
                    Draw(ll, tmpjp);
                    SubDraw();
                }
            }
        }
        /// <summary>
        /// 鼠标按下画线事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HXclick(object sender, MouseEventArgs e)
        {            
                Point3d p = new Point3d();
                double sx = this.PointToClient(Control.MousePosition).X - this.pictureBox1.Location.X;
                double sy = this.PointToClient(Control.MousePosition).Y - this.pictureBox1.Location.Y;                
                p = new Point3d(sx,sy,0);
                p = Helper.Goreturn(p, mul, a, b);
                point.Add(p);
                SubDraw();
        }
        bool hx = true;
        private void HX(object sender, EventArgs e)
        {
            if (!changeLine)
            {
                return;
            }
            if (hx)
            {
                hx = false;
                //this.hxt.Text = "1";
                this.pictureBox1.MouseClick -= pictureBox1_MouseDown2;
                this.pictureBox1.MouseClick += HXclick;
            }
            else
            {
                hx = true;
                this.pictureBox1.MouseClick += pictureBox1_MouseDown2;
                this.pictureBox1.MouseClick -= HXclick;
                //this.hxt.Text = "2";
            }
        }
        /// <summary>
        /// 描点画线
        /// </summary>
        private void SubDraw()
        {
            if (point.Count < 1)
            {
                return;
            }
            Graphics g = this.pictureBox1.CreateGraphics();
            //point = Helper.GoCom(point,a,b);
            Color mycolor;
            //创建画笔(颜色)
            mycolor = ColorTranslator.FromHtml("#f00");
            Pen npen = new Pen(mycolor, 3);
            Brush bush = new SolidBrush(mycolor);//填充的颜色
            GraphicsPath path = new GraphicsPath();
            for (int i = 0; i < point.Count; i++)
            {
                g.FillEllipse(bush, (float)point[i].X, (float)point[i].Y, 2, 2);
                if (i < point.Count - 1)
                {
                    Point p1, p2;
                    if (mul > 0)
                    {
                        p1 = new Point((int)(point[i].X / mul), (int)(point[i].Y / mul));
                        p2 = new Point((int)(point[i + 1].X / mul), (int)(point[i + 1].Y / mul));
                    }
                    else
                    {
                        int tmp = Math.Abs(mul - 1);
                        p1 = new Point((int)(point[i].X * tmp), (int)(point[i].Y * tmp));
                        p2 = new Point((int)(point[i + 1].X * tmp), (int)(point[i + 1].Y * tmp));
                    }
                    p1 = new Point((int)(p1.X - a), (int)(p1.Y - b));
                    p2 = new Point((int)(p2.X - a), (int)(p2.Y - b));
                    path.AddLine(p1, p2);//使每个点都用直线连接
                }
            }
            if (path.PointCount > 1)
            {
                g.DrawPath(npen, path);
            }
        }        
        /// <summary>
        /// 描点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateJSON(object sender, EventArgs e)
        {
            if(!this.createjsonT.Enabled)
            {
                return;
            }
            if (changeLine)
            {                
                this.pictureBox1.MouseClick -= pictureBox1_MouseDown2;
                this.ActiveControl = this.pictureBox1;              
                this.createjsonT.Text = "描点(Ctrl+E)";               
                changeLine = false;
                this.wj.Enabled = true;
                this.changejsonT.Enabled = true;
                point.Clear();
                hx = true;
            }
            else
            {
                if (ll.Count < 2)
                {
                    MessageBox.Show("没有足够可描的点");
                    return;
                }               
                this.pictureBox1.MouseClick += pictureBox1_MouseDown2;                         
                this.createjsonT.Text = "返回(Ctrl+E)";             
                changeLine = true;
                this.wj.Enabled = false;
                this.changejsonT.Enabled = false;
            }
            this.ActiveControl = this.pictureBox1;
        }
        void CtrlD(object sender, EventArgs e)
        {
            //取消描点状态
            this.pictureBox1.MouseDown += pictureBox1_MouseDown;
            this.pictureBox1.MouseClick -= pictureBox1_MouseDown2;
            this.changejsonT.Enabled = true;
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;           
            this.createjsonT.Text = "描点(Ctrl+E)";
            changeLine = false;
            //取消编辑状态
            this.createjsonT.Enabled = true;
            this.pictureBox1.MouseClick -= ChangeJPMouseDown;            
            this.ActiveControl = this.pictureBox1;
            this.changejsonT.Text = "更改(Ctrl+T)";
            changehd = false;
            this.wj.Enabled = true;
            point.Clear();
            hx = true;
        }
        /// <summary>
        /// 描点后退事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoReturn(object sender, EventArgs e)
        {
            if (point.Count == 0)
            {
                return;                
            }
            point.RemoveAt(point.Count - 1);
           
            Draw(ll, tmpjp);
            SubDraw();
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 描点清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Empty(object sender, EventArgs e)
        {
           // this.button1.Enabled = false;
            point.Clear();
            Draw(ll, tmpjp);
            SubDraw();
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 显示巷道信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowJSON(object sender, EventArgs e)
        {
            if (hd)
            {
                finjp.Clear();
               this.showjsonT.Text = "显示巷道(Ctrl+V)";
                hd = false;
            }
            else
            {
                finjp = Helper.GetlistPolyline();
                this.showjsonT.Text = "隐藏巷道(Ctrl+V)";
                hd = true;
            }
            tmpjp = Helper.Sf(finjp, mul);
            tmpjp = Helper.ComputeLl(tmpjp, a, b);
            Draw(ll, tmpjp);
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 打开json文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenJSON(object sender, EventArgs e)
        {
            if (!this.wj.Enabled)
            {
                return;
            }
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;
                Helper.Json2List(fname, 1,null);
            }
            InitZero();
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 编辑事件
        /// </summary>        
        private void ChangeJSON(object sender, EventArgs e)
        {
            if (!this.changejsonT.Enabled)
            {
                return;
            }
           else if (changehd)
            {                
                this.pictureBox1.MouseClick -= ChangeJPMouseDown;               
                this.ActiveControl = this.pictureBox1;
                this.changejsonT.Text = "更改(Ctrl+T)";              
                changehd = false;
                this.wj.Enabled = true;
                this.createjsonT.Enabled = true;
            }
            else
            {
                
                this.pictureBox1.MouseClick += ChangeJPMouseDown;
                
                this.changejsonT.Text = "返回(Ctrl+T)";
                this.createjsonT.Enabled = false;
                changehd = true;
                this.wj.Enabled = false;
            }
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 鼠标按下编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeJPMouseDown(object sender, MouseEventArgs e)
        {
            double x1 = this.PointToClient(Control.MousePosition).X - this.pictureBox1.Location.X + a;
            double y1 = this.PointToClient(Control.MousePosition).Y - this.pictureBox1.Location.Y + b;
            if (mul > 0)
            {
                x1 = x1 * mul;
                y1 = y1 * mul;
            }
            else
            {
                int tmp = Math.Abs(mul - 1);
                x1 = x1 / tmp;
                y1 = y1 / tmp;
            }
            Point point = new Point((int)x1, (int)y1);
            JsonPolyline jsonPolyline = Helper.GetPolylineByPoint(point);
            if (jsonPolyline == null)
                return;
            Form2 form2 = new Form2(jsonPolyline);
            DialogResult dr = form2.ShowDialog();
            if (dr == DialogResult.OK)
            {              
            }
        }
        bool color = true;
        private void button7_Click(object sender, EventArgs e)
        {
            if (this.colorlist.Items.Count == 0)
            {
                return;
            }
           else if (color)
            {
                this.button7.Text = "清空";
                for (int i = 0; i < this.colorlist.Items.Count; i++)
                {
                    this.colorlist.SetItemChecked(i, true);
                }
                color = false;
            }
            else
            {
                this.button7.Text = "全选";
                for (int i = 0; i < this.colorlist.Items.Count; i++)
                {
                    this.colorlist.SetItemChecked(i, false);
                }
                color = true;
            }
            this.ActiveControl = this.pictureBox1;
        }
        /// <summary>
        /// 鼠标按下事件（拖动画布）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.pictureBox1.MouseMove -= pictureBox1_MouseMove;
            this.Cursor = Cursors.Cross;
            lx = this.PointToClient(Control.MousePosition).X;
            ly = this.PointToClient(Control.MousePosition).Y;
            la = a;
            lb = b;
            this.pictureBox1.MouseMove += pictureBox1_MouseMove2;
            this.pictureBox1.MouseUp += pictureBox1_MouseUP;
        }
        private void 缓存位置设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(Directory.GetCurrentDirectory(), "listjsonpyline.txt");           
            FileUtil.DeleFile(filename);            
            filename = Path.Combine(Directory.GetCurrentDirectory(), "listjsonline.txt");
            FileUtil.DeleFile(filename);           
        }
        private void 配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = Interaction.InputBox("请输入需要解析的图层，多个值以逗号隔开", "设置图层", "中线", -1, -1);
            string filename = Path.Combine(Directory.GetCurrentDirectory(), "layer.ini");            
            FileUtil.WriteFile(filename, str, Encoding.UTF8, FileMode.Create);
        }
        bool ls;
        private void LayeringShow(object sender, EventArgs e)
        {
            if (ls)
            {
                this.colorlist.Hide();
                this.button7.Hide();
                ls = false;
            }
            else
            {
                this.colorlist.Show();
                this.button7.Show();
                ls = true;
            }
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.K)
            {
                OpenCAD(sender,e);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                SaveJSON(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                OpenJSON(sender, e);
            }
            else if(e.Control && e.KeyCode == Keys.E)
            {
                CreateJSON(sender,e);
            }
            else if(e.Control && e.KeyCode == Keys.T)
            {
                ChangeJSON(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                GoReturn(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                Empty(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                CtrlD(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                ShowJSON(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                HX(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                LayeringShow(sender, e);
            }

        }

        private void 文件模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("文件模块分三部分，打开CAD,打开JSON（巷道）,保存JSON（巷道）\r\n打开CAD会使当前的图片信息全部丢失，包括缓存信息和已编辑的信息。\r\n打开JSON文件必须保证该JSON文件是在该图片下编辑的，如果CAD和JAON不同步则可能会出错。\r\n保存JSON即保存编辑好的巷道信息。");
        }

        private void 视图模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("巷道显示可显示已绘制的巷道轨迹，再次按下会隐藏掉。\r\n分层显示会打开图片分层信息，可根据需要选择需要显示的层图，再次按下隐藏该分层信息。");
        }

        private void 描点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("按下描点按钮可对图片进行描点编辑操作，具体操作如下：\r\n①：鼠标左键选择点，系统会根据点击位置来寻找最近的点，多点后可连成线段。\r\n②：鼠标右键可对已描的点进行编辑（巷道信息)。\r\n③：描点过程中可进行画线操作，按下画线按钮后鼠标左键将不再是寻找最近的点，而是选择该点进行绘制，再次点击可返回描点操作，注意（画线功能只有在描点时才会启动，画完线编辑时需切换到描点状态才可使用右键编辑功能。）\r\n④：描点时按后退可撤销当前所绘的点，按清空可清空所有描点信息（此操作不可还原）。\r\n⑤：描点时文件功能将不可用，更改功能也将不可用，需再次点击描点按钮退出描点状态后方可使用其他功能（退出描点状态后所有的描点信息将会丢失）。");
        }

        private void 更改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("更改功能可对已编辑的巷道信息进行更改操作，具体如下：\r\n鼠标左键按下后系统会选择一条离点击点最近的巷道并弹出巷道编辑页，若鼠标点击位置没有巷道信息，则不会反应。\r\n更改项有巷道所有信息可编辑更改，包括坐标点，其操作不可回滚，若填错可点击取消按钮。\r\n点击更改按钮文件功能和描点功能将不可用，再次点击退出后更改状态后其他功能才可使用。");
        }

        private void 取消编辑状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("取消编辑状态按钮可取消掉所有的编辑状态，当然描点信息也会被清除掉。");
        }

        private void 设置模块ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("设置功能暂不可用，等后期版本更新。");
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filename = Path.Combine(Directory.GetCurrentDirectory(), "listjsonpyline.txt");
            string str = Helper.List2Json(Helper.GetlistPolyline());
            FileUtil.WriteFile(filename, str, Encoding.UTF8, FileMode.Create);
            filename = Path.Combine(Directory.GetCurrentDirectory(), "listjsonline.txt");
            str = Helper.List2Json(Helper.GetListLine());
            FileUtil.WriteFile(filename, str, Encoding.UTF8, FileMode.Create);
        }
        /// <summary>
        /// 鼠标移动事件（拖动画布）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove2(object sender, MouseEventArgs e)
        {
            double fx = this.PointToClient(Control.MousePosition).X;
            double fy = this.PointToClient(Control.MousePosition).Y;
            a = la + (lx - fx);
            b = lb + (ly - fy);
            ll = Helper.Sf(Helper.GetListLine(), mul,colorlist);
            ll = Helper.ComputeLl(ll, a, b);
            tmpjp = Helper.Sf(finjp, mul);
            tmpjp = Helper.ComputeLl(tmpjp, a, b);
            Draw(ll, tmpjp);
            SubDraw();
        }
        /// <summary>
        /// 鼠标弹起事件（拖动画布）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUP(object sender, MouseEventArgs e)
        {
            if (mul > 0)
            {
                x = a * mul;
                y = b * mul;
            }
            else
            {
                int tmpmul = Math.Abs(mul - 1);
                x = a / tmpmul;
                y = b / tmpmul;
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.MouseMove -= pictureBox1_MouseMove2;
            this.pictureBox1.MouseMove += pictureBox1_MouseMove;
            this.pictureBox1.MouseUp -= pictureBox1_MouseUP;
            //this.pictureBox1.MouseWheel
        }
        /// <summary>
        /// 鼠标滚轮事件（缩放画布）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double li = this.PointToClient(Control.MousePosition).X - this.pictureBox1.Location.X;
            double lj = this.PointToClient(Control.MousePosition).Y - this.pictureBox1.Location.Y;
            int lm = mul;
            if (e.Delta > 0)
                mul -= 1;
            else
                mul += 1;
            if (mul > 20)
            {
                mul = 20;
                return;
            }
            if (mul < -9)
            {
                mul = -9;
                return;
            }
            if (mul > 0)
            {
                if (lm > 0)
                {
                    a = (((a + li) * lm) / mul) - li;
                    b = (((b + lj) * lm) / mul) - lj;
                }
                else
                {
                    int tmp = Math.Abs(lm - 1);
                    a = (((a + li) / tmp) / mul) - li;
                    b = (((b + lj) / tmp) / mul) - lj;
                }
                x = a * mul;
                y = b * mul;
            }
            else
            {
                int tmpmul = Math.Abs(mul - 1);
                if (lm > 0)
                {
                    a = (((a + li) / lm) * tmpmul) - li;
                    b = (((b + lj) / lm) * tmpmul) - lj;
                }
                else
                {
                    int tmp = Math.Abs(lm - 1);
                    a = (((a + li) / tmp) * tmpmul) - li;
                    b = (((b + lj) / tmp) * tmpmul) - lj;
                }
                x = a / tmpmul;
                y = b / tmpmul;
            }
            ll = Helper.Sf(Helper.GetListLine(), mul,colorlist);
            ll = Helper.ComputeLl(ll, a, b);
            tmpjp = Helper.Sf(finjp, mul);
            tmpjp = Helper.ComputeLl(tmpjp, a, b);
            Draw(ll, tmpjp);
            SubDraw();
        }
		void ColorlistSelectedIndexChanged(object sender, EventArgs e)
		{
            ll = Helper.Sf(Helper.GetListLine(), mul, colorlist);
            ll = Helper.ComputeLl(ll, a, b);
            tmpjp = Helper.Sf(finjp, mul);
            tmpjp = Helper.ComputeLl(tmpjp, a, b);
            Draw(ll, tmpjp);
            SubDraw();
            this.ActiveControl = this.pictureBox1;
		}
    }
}
