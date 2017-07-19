namespace com.wer.sc.ui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.本地数据中心ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tb_SwitchTimeLine = new System.Windows.Forms.ToolStripButton();
            this.tb_SwitchKLine = new System.Windows.Forms.ToolStripButton();
            this.tb_SwitchTick = new System.Windows.Forms.ToolStripButton();
            this.tb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine1 = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine5 = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine15 = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine1H = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine1Day = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine5S = new System.Windows.Forms.ToolStripButton();
            this.tb_KLine15S = new System.Windows.Forms.ToolStripButton();
            this.tb_KLineBackward = new System.Windows.Forms.ToolStripButton();
            this.tb_KLineForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_ChangeTime = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.compStrategyTree1 = new com.wer.sc.ui.comp.CompStrategyTree();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.compChart1 = new com.wer.sc.ui.comp.CompChart();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.账户ToolStripMenuItem,
            this.查看ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "数据";
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.本地数据中心ToolStripMenuItem});
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.连接ToolStripMenuItem.Text = "本地数据中心";
            // 
            // 本地数据中心ToolStripMenuItem
            // 
            this.本地数据中心ToolStripMenuItem.Name = "本地数据中心ToolStripMenuItem";
            this.本地数据中心ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.本地数据中心ToolStripMenuItem.Text = "连接";
            // 
            // 账户ToolStripMenuItem
            // 
            this.账户ToolStripMenuItem.Name = "账户ToolStripMenuItem";
            this.账户ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.账户ToolStripMenuItem.Text = "账户";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 568);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(823, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tb_SwitchTimeLine,
            this.tb_SwitchKLine,
            this.tb_SwitchTick,
            this.tb_Refresh,
            this.tb_KLine1,
            this.tb_KLine5,
            this.tb_KLine15,
            this.tb_KLine1H,
            this.tb_KLine1Day,
            this.tb_KLine5S,
            this.tb_KLine15S,
            this.tb_KLineBackward,
            this.tb_KLineForward,
            this.toolStripSeparator1,
            this.tb_ChangeTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(823, 45);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tb_SwitchTimeLine
            // 
            this.tb_SwitchTimeLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchTimeLine.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchTimeLine.Image")));
            this.tb_SwitchTimeLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchTimeLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchTimeLine.Name = "tb_SwitchTimeLine";
            this.tb_SwitchTimeLine.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchTimeLine.Text = "分时线";
            this.tb_SwitchTimeLine.Click += new System.EventHandler(this.tb_SwitchTimeLine_Click);
            // 
            // tb_SwitchKLine
            // 
            this.tb_SwitchKLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchKLine.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchKLine.Image")));
            this.tb_SwitchKLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchKLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchKLine.Name = "tb_SwitchKLine";
            this.tb_SwitchKLine.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchKLine.Text = "K线";
            this.tb_SwitchKLine.Click += new System.EventHandler(this.tb_SwitchKLine_Click);
            // 
            // tb_SwitchTick
            // 
            this.tb_SwitchTick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchTick.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchTick.Image")));
            this.tb_SwitchTick.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchTick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchTick.Name = "tb_SwitchTick";
            this.tb_SwitchTick.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchTick.Text = "闪电线";
            this.tb_SwitchTick.Click += new System.EventHandler(this.tb_SwitchTick_Click);
            // 
            // tb_Refresh
            // 
            this.tb_Refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tb_Refresh.Image")));
            this.tb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Refresh.Name = "tb_Refresh";
            this.tb_Refresh.Size = new System.Drawing.Size(24, 42);
            this.tb_Refresh.Text = "刷新";
            this.tb_Refresh.Click += new System.EventHandler(this.tb_Refresh_Click);
            // 
            // tb_KLine1
            // 
            this.tb_KLine1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine1.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine1.Image")));
            this.tb_KLine1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine1.Name = "tb_KLine1";
            this.tb_KLine1.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine1.Text = "1分钟";
            this.tb_KLine1.Click += new System.EventHandler(this.tb_KLine1_Click);
            // 
            // tb_KLine5
            // 
            this.tb_KLine5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine5.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine5.Image")));
            this.tb_KLine5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine5.Name = "tb_KLine5";
            this.tb_KLine5.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine5.Text = "5分钟";
            this.tb_KLine5.Click += new System.EventHandler(this.tb_KLine5_Click);
            // 
            // tb_KLine15
            // 
            this.tb_KLine15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine15.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine15.Image")));
            this.tb_KLine15.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine15.Name = "tb_KLine15";
            this.tb_KLine15.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine15.Text = "15分钟";
            this.tb_KLine15.Click += new System.EventHandler(this.tb_KLine15_Click);
            // 
            // tb_KLine1H
            // 
            this.tb_KLine1H.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine1H.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine1H.Image")));
            this.tb_KLine1H.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine1H.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine1H.Name = "tb_KLine1H";
            this.tb_KLine1H.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine1H.Text = "1小时";
            this.tb_KLine1H.Click += new System.EventHandler(this.tb_KLine1H_Click);
            // 
            // tb_KLine1Day
            // 
            this.tb_KLine1Day.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine1Day.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine1Day.Image")));
            this.tb_KLine1Day.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine1Day.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine1Day.Name = "tb_KLine1Day";
            this.tb_KLine1Day.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine1Day.Text = "日线";
            this.tb_KLine1Day.Click += new System.EventHandler(this.tb_KLine1Day_Click);
            // 
            // tb_KLine5S
            // 
            this.tb_KLine5S.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine5S.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine5S.Image")));
            this.tb_KLine5S.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine5S.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine5S.Name = "tb_KLine5S";
            this.tb_KLine5S.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine5S.Text = "5秒";
            this.tb_KLine5S.Click += new System.EventHandler(this.tb_KLine5S_Click);
            // 
            // tb_KLine15S
            // 
            this.tb_KLine15S.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLine15S.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLine15S.Image")));
            this.tb_KLine15S.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLine15S.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLine15S.Name = "tb_KLine15S";
            this.tb_KLine15S.Size = new System.Drawing.Size(28, 42);
            this.tb_KLine15S.Text = "15秒";
            this.tb_KLine15S.Click += new System.EventHandler(this.tb_KLine15S_Click);
            // 
            // tb_KLineBackward
            // 
            this.tb_KLineBackward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLineBackward.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLineBackward.Image")));
            this.tb_KLineBackward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLineBackward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLineBackward.Name = "tb_KLineBackward";
            this.tb_KLineBackward.Size = new System.Drawing.Size(28, 42);
            this.tb_KLineBackward.Text = "后退";
            this.tb_KLineBackward.Click += new System.EventHandler(this.tb_KLineBackward_Click);
            // 
            // tb_KLineForward
            // 
            this.tb_KLineForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_KLineForward.Image = ((System.Drawing.Image)(resources.GetObject("tb_KLineForward.Image")));
            this.tb_KLineForward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_KLineForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_KLineForward.Name = "tb_KLineForward";
            this.tb_KLineForward.Size = new System.Drawing.Size(28, 42);
            this.tb_KLineForward.Text = "前进";
            this.tb_KLineForward.Click += new System.EventHandler(this.tb_KLineForward_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // tb_ChangeTime
            // 
            this.tb_ChangeTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_ChangeTime.Image = ((System.Drawing.Image)(resources.GetObject("tb_ChangeTime.Image")));
            this.tb_ChangeTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_ChangeTime.Name = "tb_ChangeTime";
            this.tb_ChangeTime.Size = new System.Drawing.Size(24, 42);
            this.tb_ChangeTime.Text = "修改时间";
            this.tb_ChangeTime.Click += new System.EventHandler(this.tb_ChangeTime_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.compChart1);
            this.splitContainer1.Size = new System.Drawing.Size(823, 495);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(190, 495);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.compStrategyTree1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(182, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "策略";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // compStrategyTree1
            // 
            this.compStrategyTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compStrategyTree1.Location = new System.Drawing.Point(3, 3);
            this.compStrategyTree1.Name = "compStrategyTree1";
            this.compStrategyTree1.Size = new System.Drawing.Size(176, 460);
            this.compStrategyTree1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(182, 466);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // compChart1
            // 
            this.compChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compChart1.KLineBlockWidth = 5F;
            this.compChart1.Location = new System.Drawing.Point(0, 0);
            this.compChart1.Name = "compChart1";
            this.compChart1.Size = new System.Drawing.Size(629, 495);
            this.compChart1.TabIndex = 0;
            this.compChart1.Time = 0D;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 590);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "SC";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tb_SwitchTimeLine;
        private System.Windows.Forms.ToolStripButton tb_SwitchKLine;
        private System.Windows.Forms.ToolStripButton tb_SwitchTick;
        private System.Windows.Forms.ToolStripButton tb_Refresh;
        private System.Windows.Forms.ToolStripButton tb_KLine1;
        private System.Windows.Forms.ToolStripButton tb_KLine5;
        private System.Windows.Forms.ToolStripButton tb_KLine15;
        private System.Windows.Forms.ToolStripButton tb_KLine1H;
        private System.Windows.Forms.ToolStripButton tb_KLine1Day;
        private System.Windows.Forms.ToolStripButton tb_KLine5S;
        private System.Windows.Forms.ToolStripButton tb_KLine15S;
        private System.Windows.Forms.ToolStripButton tb_KLineBackward;
        private System.Windows.Forms.ToolStripButton tb_KLineForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tb_ChangeTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private comp.CompStrategyTree compStrategyTree1;
        private System.Windows.Forms.TabPage tabPage2;
        private comp.CompChart compChart1;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 本地数据中心ToolStripMenuItem;
    }
}

