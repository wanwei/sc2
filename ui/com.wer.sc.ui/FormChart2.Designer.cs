using com.wer.sc.ui.comp;

namespace com.wer.sc.ui
{
    partial class FormChart2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChart2));
            com.wer.sc.data.KLinePeriod kLinePeriod1 = new com.wer.sc.data.KLinePeriod();
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
            this.tb_ForwardSetting = new System.Windows.Forms.ToolStripButton();
            this.tb_BackwordTime = new System.Windows.Forms.ToolStripButton();
            this.tb_Play = new System.Windows.Forms.ToolStripButton();
            this.tb_ForwordTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_CodeList = new System.Windows.Forms.ToolStripButton();
            this.btStrategy = new System.Windows.Forms.ToolStripButton();
            this.bt_Log = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据中心ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.compStrategyTree1 = new com.wer.sc.ui.comp.CompStrategyTree();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.compMain1 = new com.wer.sc.ui.comp.CompMain();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.tb_ChangeTime,
            this.tb_ForwardSetting,
            this.tb_BackwordTime,
            this.tb_Play,
            this.tb_ForwordTime,
            this.toolStripSeparator2,
            this.tb_CodeList,
            this.btStrategy,
            this.bt_Log});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1063, 45);
            this.toolStrip1.TabIndex = 3;
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
            this.tb_Refresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Refresh.Name = "tb_Refresh";
            this.tb_Refresh.Size = new System.Drawing.Size(28, 42);
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
            this.tb_ChangeTime.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_ChangeTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_ChangeTime.Name = "tb_ChangeTime";
            this.tb_ChangeTime.Size = new System.Drawing.Size(28, 42);
            this.tb_ChangeTime.Text = "修改时间";
            this.tb_ChangeTime.Click += new System.EventHandler(this.tb_ChangeTime_Click);
            // 
            // tb_ForwardSetting
            // 
            this.tb_ForwardSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_ForwardSetting.Image = ((System.Drawing.Image)(resources.GetObject("tb_ForwardSetting.Image")));
            this.tb_ForwardSetting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_ForwardSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_ForwardSetting.Name = "tb_ForwardSetting";
            this.tb_ForwardSetting.Size = new System.Drawing.Size(28, 42);
            this.tb_ForwardSetting.Text = "前进后退周期设定";
            this.tb_ForwardSetting.Click += new System.EventHandler(this.tb_ForwardSetting_Click);
            // 
            // tb_BackwordTime
            // 
            this.tb_BackwordTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_BackwordTime.Image = ((System.Drawing.Image)(resources.GetObject("tb_BackwordTime.Image")));
            this.tb_BackwordTime.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_BackwordTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_BackwordTime.Name = "tb_BackwordTime";
            this.tb_BackwordTime.Size = new System.Drawing.Size(28, 42);
            this.tb_BackwordTime.Text = "指定周期后退";
            this.tb_BackwordTime.Click += new System.EventHandler(this.tb_BackwordTime_Click);
            // 
            // tb_Play
            // 
            this.tb_Play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_Play.Image = ((System.Drawing.Image)(resources.GetObject("tb_Play.Image")));
            this.tb_Play.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_Play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Play.Name = "tb_Play";
            this.tb_Play.Size = new System.Drawing.Size(28, 42);
            this.tb_Play.Text = "播放";
            this.tb_Play.Click += new System.EventHandler(this.tb_Play_Click);
            // 
            // tb_ForwordTime
            // 
            this.tb_ForwordTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_ForwordTime.Image = ((System.Drawing.Image)(resources.GetObject("tb_ForwordTime.Image")));
            this.tb_ForwordTime.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_ForwordTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_ForwordTime.Name = "tb_ForwordTime";
            this.tb_ForwordTime.Size = new System.Drawing.Size(28, 42);
            this.tb_ForwordTime.Text = "指定周期前进";
            this.tb_ForwordTime.Click += new System.EventHandler(this.tb_ForwordTime_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // tb_CodeList
            // 
            this.tb_CodeList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_CodeList.Image = ((System.Drawing.Image)(resources.GetObject("tb_CodeList.Image")));
            this.tb_CodeList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_CodeList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_CodeList.Name = "tb_CodeList";
            this.tb_CodeList.Size = new System.Drawing.Size(28, 42);
            this.tb_CodeList.Text = "股票列表";
            this.tb_CodeList.Click += new System.EventHandler(this.tb_CodeList_Click);
            // 
            // btStrategy
            // 
            this.btStrategy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btStrategy.Image = ((System.Drawing.Image)(resources.GetObject("btStrategy.Image")));
            this.btStrategy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btStrategy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStrategy.Name = "btStrategy";
            this.btStrategy.Size = new System.Drawing.Size(28, 42);
            this.btStrategy.Text = "策略";
            this.btStrategy.Click += new System.EventHandler(this.btStrategy_Click);
            // 
            // bt_Log
            // 
            this.bt_Log.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_Log.Image = ((System.Drawing.Image)(resources.GetObject("bt_Log.Image")));
            this.bt_Log.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.bt_Log.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_Log.Name = "bt_Log";
            this.bt_Log.Size = new System.Drawing.Size(28, 42);
            this.bt_Log.Text = "日志";
            this.bt_Log.Click += new System.EventHandler(this.bt_Log_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1063, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 数据ToolStripMenuItem
            // 
            this.数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据中心ToolStripMenuItem});
            this.数据ToolStripMenuItem.Name = "数据ToolStripMenuItem";
            this.数据ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.数据ToolStripMenuItem.Text = "数据";
            // 
            // 数据中心ToolStripMenuItem
            // 
            this.数据中心ToolStripMenuItem.Name = "数据中心ToolStripMenuItem";
            this.数据中心ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.数据中心ToolStripMenuItem.Text = "数据中心";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 601);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1063, 25);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbTime
            // 
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(45, 20);
            this.lbTime.Text = "Time";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 73);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.compStrategyTree1);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.compMain1);
            this.splitContainer1.Size = new System.Drawing.Size(1063, 528);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // compStrategyTree1
            // 
            this.compStrategyTree1.ContextMenuStrip = this.contextMenuStrip1;
            this.compStrategyTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compStrategyTree1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.compStrategyTree1.ForeColor = System.Drawing.Color.Yellow;
            this.compStrategyTree1.Location = new System.Drawing.Point(0, 0);
            this.compStrategyTree1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.compStrategyTree1.Name = "compStrategyTree1";
            this.compStrategyTree1.Size = new System.Drawing.Size(271, 100);
            this.compStrategyTree1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExecute,
            this.menuItemParameters,
            this.menuItemRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 82);
            // 
            // menuItemExecute
            // 
            this.menuItemExecute.Name = "menuItemExecute";
            this.menuItemExecute.Size = new System.Drawing.Size(114, 26);
            this.menuItemExecute.Text = "执行";
            this.menuItemExecute.Click += new System.EventHandler(this.menuItemExecute_Click);
            // 
            // menuItemParameters
            // 
            this.menuItemParameters.Name = "menuItemParameters";
            this.menuItemParameters.Size = new System.Drawing.Size(114, 26);
            this.menuItemParameters.Text = "参数";
            this.menuItemParameters.Click += new System.EventHandler(this.menuItemParameters_Click);
            // 
            // menuItemRefresh
            // 
            this.menuItemRefresh.Name = "menuItemRefresh";
            this.menuItemRefresh.Size = new System.Drawing.Size(114, 26);
            this.menuItemRefresh.Text = "刷新";
            // 
            // compMain1
            // 
            this.compMain1.Code = "m1605";
            this.compMain1.DataCenterUri = "E:\\scdata\\cnfutures\\";
            this.compMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compMain1.KLineBlockWidth = 5F;
            kLinePeriod1.PeriodType = com.wer.sc.data.KLineTimeType.MINUTE;
            this.compMain1.KlinePeriod = kLinePeriod1;
            this.compMain1.Location = new System.Drawing.Point(0, 0);
            this.compMain1.Name = "compMain1";
            this.compMain1.Size = new System.Drawing.Size(1063, 528);
            this.compMain1.TabIndex = 8;
            this.compMain1.Time = 20151230.093D;
            // 
            // FormChart2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 626);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormChart2";
            this.Text = "测试图表控件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tb_KLine1;
        private System.Windows.Forms.ToolStripButton tb_KLine5;
        private System.Windows.Forms.ToolStripButton tb_KLine15;
        private System.Windows.Forms.ToolStripButton tb_KLine1H;
        private System.Windows.Forms.ToolStripButton tb_KLine1Day;
        private System.Windows.Forms.ToolStripButton tb_KLine5S;
        private System.Windows.Forms.ToolStripButton tb_KLine15S;
        private System.Windows.Forms.ToolStripButton tb_KLineBackward;
        private System.Windows.Forms.ToolStripButton tb_KLineForward;
        private System.Windows.Forms.ToolStripButton tb_SwitchTimeLine;
        private System.Windows.Forms.ToolStripButton tb_SwitchKLine;
        private System.Windows.Forms.ToolStripButton tb_SwitchTick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tb_ChangeTime;
        private System.Windows.Forms.ToolStripButton tb_Refresh;
        private System.Windows.Forms.ToolStripButton tb_CodeList;
        private System.Windows.Forms.ToolStripButton tb_BackwordTime;
        private System.Windows.Forms.ToolStripButton tb_Play;
        private System.Windows.Forms.ToolStripButton tb_ForwordTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据中心ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tb_ForwardSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private CompMain compMain1;
        private System.Windows.Forms.ToolStripStatusLabel lbTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CompStrategyTree compStrategyTree1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemExecute;
        private System.Windows.Forms.ToolStripMenuItem menuItemParameters;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefresh;
        private System.Windows.Forms.ToolStripButton bt_Log;
        private System.Windows.Forms.ToolStripButton btStrategy;
    }
}

