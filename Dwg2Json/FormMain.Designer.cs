namespace Dwg2Json
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorlist = new System.Windows.Forms.CheckedListBox();
            this.button7 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wj = new System.Windows.Forms.ToolStripMenuItem();
            this.opencadT = new System.Windows.Forms.ToolStripMenuItem();
            this.openjsonT = new System.Windows.Forms.ToolStripMenuItem();
            this.savejsonT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.showjsonT = new System.Windows.Forms.ToolStripMenuItem();
            this.layshow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.createjsonT = new System.Windows.Forms.ToolStripMenuItem();
            this.changejsonT = new System.Windows.Forms.ToolStripMenuItem();
            this.goreturnT = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyT = new System.Windows.Forms.ToolStripMenuItem();
            this.hxt = new System.Windows.Forms.ToolStripMenuItem();
            this.取消编辑状态CtrlDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.缓存位置设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.描点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消编辑状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDlg
            // 
            this.openFileDlg.DefaultExt = "dwg";
            this.openFileDlg.FileName = "openFileDialog";
            this.openFileDlg.Filter = "DWG文件|*.dwg";
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.DefaultExt = "conf";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(151, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 800);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(490, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "坐标:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(544, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 26);
            this.textBox1.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "CONF文件|*.conf";
            // 
            // colorlist
            // 
            this.colorlist.FormattingEnabled = true;
            this.colorlist.Items.AddRange(new object[] {
            "dfsdf",
            "dsdgdfg"});
            this.colorlist.Location = new System.Drawing.Point(12, 209);
            this.colorlist.Name = "colorlist";
            this.colorlist.Size = new System.Drawing.Size(133, 193);
            this.colorlist.TabIndex = 17;
            this.colorlist.SelectedIndexChanged += new System.EventHandler(this.ColorlistSelectedIndexChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(25, 408);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(96, 32);
            this.button7.TabIndex = 19;
            this.button7.Text = "全选";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wj,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1146, 25);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wj
            // 
            this.wj.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opencadT,
            this.openjsonT,
            this.savejsonT});
            this.wj.Name = "wj";
            this.wj.Size = new System.Drawing.Size(44, 21);
            this.wj.Text = "文件";
            // 
            // opencadT
            // 
            this.opencadT.Name = "opencadT";
            this.opencadT.Size = new System.Drawing.Size(180, 22);
            this.opencadT.Text = "打开CAD(Ctrl+K)";
            // 
            // openjsonT
            // 
            this.openjsonT.Name = "openjsonT";
            this.openjsonT.Size = new System.Drawing.Size(180, 22);
            this.openjsonT.Text = "打开JSON(Ctrl+O)";
            // 
            // savejsonT
            // 
            this.savejsonT.Name = "savejsonT";
            this.savejsonT.Size = new System.Drawing.Size(180, 22);
            this.savejsonT.Text = "保存JSON(Ctrl+S)";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showjsonT,
            this.layshow});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem2.Text = "视图";
            // 
            // showjsonT
            // 
            this.showjsonT.Name = "showjsonT";
            this.showjsonT.Size = new System.Drawing.Size(180, 22);
            this.showjsonT.Text = "显示巷道(Ctrl+V)";
            // 
            // layshow
            // 
            this.layshow.Name = "layshow";
            this.layshow.Size = new System.Drawing.Size(180, 22);
            this.layshow.Text = "分层显示(Ctrl+Y)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createjsonT,
            this.changejsonT,
            this.goreturnT,
            this.emptyT,
            this.hxt,
            this.取消编辑状态CtrlDToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem3.Text = "编辑";
            // 
            // createjsonT
            // 
            this.createjsonT.Name = "createjsonT";
            this.createjsonT.Size = new System.Drawing.Size(194, 22);
            this.createjsonT.Text = "描点(Ctrl+E)";
            // 
            // changejsonT
            // 
            this.changejsonT.Name = "changejsonT";
            this.changejsonT.Size = new System.Drawing.Size(194, 22);
            this.changejsonT.Text = "更改(Ctrl+T)";
            // 
            // goreturnT
            // 
            this.goreturnT.Name = "goreturnT";
            this.goreturnT.Size = new System.Drawing.Size(194, 22);
            this.goreturnT.Text = "后退(Ctrl+Z)";
            // 
            // emptyT
            // 
            this.emptyT.Name = "emptyT";
            this.emptyT.Size = new System.Drawing.Size(194, 22);
            this.emptyT.Text = "清除(Ctrl+F)";
            // 
            // hxt
            // 
            this.hxt.Name = "hxt";
            this.hxt.Size = new System.Drawing.Size(194, 22);
            this.hxt.Text = "画线(Ctrl+P)";
            // 
            // 取消编辑状态CtrlDToolStripMenuItem
            // 
            this.取消编辑状态CtrlDToolStripMenuItem.Name = "取消编辑状态CtrlDToolStripMenuItem";
            this.取消编辑状态CtrlDToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.取消编辑状态CtrlDToolStripMenuItem.Text = "取消编辑状态(Ctrl+D)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.缓存位置设置ToolStripMenuItem,
            this.配置文件ToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem4.Text = "设置";
            // 
            // 缓存位置设置ToolStripMenuItem
            // 
            this.缓存位置设置ToolStripMenuItem.Name = "缓存位置设置ToolStripMenuItem";
            this.缓存位置设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.缓存位置设置ToolStripMenuItem.Text = "清除缓存";
            this.缓存位置设置ToolStripMenuItem.Click += new System.EventHandler(this.缓存位置设置ToolStripMenuItem_Click);
            // 
            // 配置文件ToolStripMenuItem
            // 
            this.配置文件ToolStripMenuItem.Name = "配置文件ToolStripMenuItem";
            this.配置文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.配置文件ToolStripMenuItem.Text = "设置图层";
            this.配置文件ToolStripMenuItem.Click += new System.EventHandler(this.配置文件ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件模块ToolStripMenuItem,
            this.视图模块ToolStripMenuItem,
            this.编辑模块ToolStripMenuItem,
            this.设置模块ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 文件模块ToolStripMenuItem
            // 
            this.文件模块ToolStripMenuItem.Name = "文件模块ToolStripMenuItem";
            this.文件模块ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.文件模块ToolStripMenuItem.Text = "文件模块";
            this.文件模块ToolStripMenuItem.Click += new System.EventHandler(this.文件模块ToolStripMenuItem_Click);
            // 
            // 视图模块ToolStripMenuItem
            // 
            this.视图模块ToolStripMenuItem.Name = "视图模块ToolStripMenuItem";
            this.视图模块ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.视图模块ToolStripMenuItem.Text = "视图模块";
            this.视图模块ToolStripMenuItem.Click += new System.EventHandler(this.视图模块ToolStripMenuItem_Click);
            // 
            // 编辑模块ToolStripMenuItem
            // 
            this.编辑模块ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.描点ToolStripMenuItem,
            this.更改ToolStripMenuItem,
            this.取消编辑状态ToolStripMenuItem});
            this.编辑模块ToolStripMenuItem.Name = "编辑模块ToolStripMenuItem";
            this.编辑模块ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.编辑模块ToolStripMenuItem.Text = "编辑模块";
            // 
            // 描点ToolStripMenuItem
            // 
            this.描点ToolStripMenuItem.Name = "描点ToolStripMenuItem";
            this.描点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.描点ToolStripMenuItem.Text = "描点";
            this.描点ToolStripMenuItem.Click += new System.EventHandler(this.描点ToolStripMenuItem_Click);
            // 
            // 更改ToolStripMenuItem
            // 
            this.更改ToolStripMenuItem.Name = "更改ToolStripMenuItem";
            this.更改ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.更改ToolStripMenuItem.Text = "更改";
            this.更改ToolStripMenuItem.Click += new System.EventHandler(this.更改ToolStripMenuItem_Click);
            // 
            // 取消编辑状态ToolStripMenuItem
            // 
            this.取消编辑状态ToolStripMenuItem.Name = "取消编辑状态ToolStripMenuItem";
            this.取消编辑状态ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.取消编辑状态ToolStripMenuItem.Text = "取消编辑状态";
            this.取消编辑状态ToolStripMenuItem.Click += new System.EventHandler(this.取消编辑状态ToolStripMenuItem_Click);
            // 
            // 设置模块ToolStripMenuItem
            // 
            this.设置模块ToolStripMenuItem.Name = "设置模块ToolStripMenuItem";
            this.设置模块ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置模块ToolStripMenuItem.Text = "设置模块";
            this.设置模块ToolStripMenuItem.Click += new System.EventHandler(this.设置模块ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 881);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.colorlist);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "tunnels factory ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckedListBox colorlist;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wj;
        private System.Windows.Forms.ToolStripMenuItem opencadT;
        private System.Windows.Forms.ToolStripMenuItem openjsonT;
        private System.Windows.Forms.ToolStripMenuItem savejsonT;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem showjsonT;
        private System.Windows.Forms.ToolStripMenuItem createjsonT;
        private System.Windows.Forms.ToolStripMenuItem changejsonT;
        private System.Windows.Forms.ToolStripMenuItem goreturnT;
        private System.Windows.Forms.ToolStripMenuItem emptyT;
        private System.Windows.Forms.ToolStripMenuItem 缓存位置设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layshow;
        private System.Windows.Forms.ToolStripMenuItem hxt;
        private System.Windows.Forms.ToolStripMenuItem 取消编辑状态CtrlDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 描点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消编辑状态ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置模块ToolStripMenuItem;
    }
}

