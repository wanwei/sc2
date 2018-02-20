using com.wer.sc.data;

namespace com.wer.sc.ui.comp
{
    partial class ToolStripComponent
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolStripComponent));
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
            this.tb_ChangeCode = new System.Windows.Forms.ToolStripButton();
            this.tb_ForwardSetting = new System.Windows.Forms.ToolStripButton();
            this.tb_BackwordTime = new System.Windows.Forms.ToolStripButton();
            this.tb_Play = new System.Windows.Forms.ToolStripButton();
            this.tb_ForwordTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_Strategy = new System.Windows.Forms.ToolStripButton();
            this.btRunState = new System.Windows.Forms.ToolStripButton();
            this.tb_StrategyDataPackage = new System.Windows.Forms.ToolStripButton();
            this.tb_StrategyReport = new System.Windows.Forms.ToolStripButton();
            this.tb_StrategyResult = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tb_Connect = new System.Windows.Forms.ToolStripButton();
            this.tb_Trade = new System.Windows.Forms.ToolStripButton();
            this.tb_AccountAna = new System.Windows.Forms.ToolStripButton();
            this.btLogout = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
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
            this.tb_ChangeCode,
            this.tb_ForwardSetting,
            this.tb_BackwordTime,
            this.tb_Play,
            this.tb_ForwordTime,
            this.toolStripSeparator2,
            this.tb_Strategy,
            this.btRunState,
            this.tb_StrategyDataPackage,
            this.tb_StrategyReport,
            this.tb_StrategyResult,
            this.toolStripSeparator3,
            this.tb_Connect,
            this.tb_Trade,
            this.tb_AccountAna,
            this.btLogout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(937, 45);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "交易";
            // 
            // tb_SwitchTimeLine
            // 
            this.tb_SwitchTimeLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchTimeLine.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchTimeLine.Image")));
            this.tb_SwitchTimeLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchTimeLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchTimeLine.Name = "tb_SwitchTimeLine";
            this.tb_SwitchTimeLine.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchTimeLine.Tag = com.wer.sc.ui.comp.ChartType.TimeLine;
            this.tb_SwitchTimeLine.Text = "分时线";
            this.tb_SwitchTimeLine.Click += new System.EventHandler(this.tb_SwitchChartType_Click);
            // 
            // tb_SwitchKLine
            // 
            this.tb_SwitchKLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchKLine.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchKLine.Image")));
            this.tb_SwitchKLine.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchKLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchKLine.Name = "tb_SwitchKLine";
            this.tb_SwitchKLine.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchKLine.Tag = com.wer.sc.ui.comp.ChartType.KLine;
            this.tb_SwitchKLine.Text = "K线";
            this.tb_SwitchKLine.Click += new System.EventHandler(this.tb_SwitchChartType_Click);
            // 
            // tb_SwitchTick
            // 
            this.tb_SwitchTick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_SwitchTick.Image = ((System.Drawing.Image)(resources.GetObject("tb_SwitchTick.Image")));
            this.tb_SwitchTick.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_SwitchTick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_SwitchTick.Name = "tb_SwitchTick";
            this.tb_SwitchTick.Size = new System.Drawing.Size(28, 42);
            this.tb_SwitchTick.Tag = com.wer.sc.ui.comp.ChartType.Tick;
            this.tb_SwitchTick.Text = "闪电线";
            this.tb_SwitchTick.Visible = false;
            this.tb_SwitchTick.Click += new System.EventHandler(this.tb_SwitchChartType_Click);
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
            this.tb_KLine1.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine5.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine15.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine1H.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine1Day.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine5S.Click += new System.EventHandler(this.tb_KLine_Click);
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
            this.tb_KLine15S.Click += new System.EventHandler(this.tb_KLine_Click);
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
            // tb_ChangeCode
            // 
            this.tb_ChangeCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_ChangeCode.Image = ((System.Drawing.Image)(resources.GetObject("tb_ChangeCode.Image")));
            this.tb_ChangeCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_ChangeCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_ChangeCode.Name = "tb_ChangeCode";
            this.tb_ChangeCode.Size = new System.Drawing.Size(28, 42);
            this.tb_ChangeCode.Text = "修改代码";
            this.tb_ChangeCode.Visible = false;
            this.tb_ChangeCode.Click += new System.EventHandler(this.tb_ChangeCode_Click);
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
            this.tb_BackwordTime.Text = "后退1分钟";
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
            this.tb_ForwordTime.Text = "前进1分钟";
            this.tb_ForwordTime.Click += new System.EventHandler(this.tb_ForwordTime_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // tb_Strategy
            // 
            this.tb_Strategy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_Strategy.Image = ((System.Drawing.Image)(resources.GetObject("tb_Strategy.Image")));
            this.tb_Strategy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_Strategy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Strategy.Name = "tb_Strategy";
            this.tb_Strategy.Size = new System.Drawing.Size(28, 42);
            this.tb_Strategy.Text = "策略";
            this.tb_Strategy.Click += new System.EventHandler(this.btStrategy_Click);
            // 
            // btRunState
            // 
            this.btRunState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRunState.Image = ((System.Drawing.Image)(resources.GetObject("btRunState.Image")));
            this.btRunState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btRunState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRunState.Name = "btRunState";
            this.btRunState.Size = new System.Drawing.Size(28, 42);
            this.btRunState.Text = "策略执行状态";
            // 
            // tb_StrategyDataPackage
            // 
            this.tb_StrategyDataPackage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_StrategyDataPackage.Image = ((System.Drawing.Image)(resources.GetObject("tb_StrategyDataPackage.Image")));
            this.tb_StrategyDataPackage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_StrategyDataPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_StrategyDataPackage.Name = "tb_StrategyDataPackage";
            this.tb_StrategyDataPackage.Size = new System.Drawing.Size(28, 42);
            this.tb_StrategyDataPackage.Text = "数据包";
            this.tb_StrategyDataPackage.Visible = false;
            this.tb_StrategyDataPackage.Click += new System.EventHandler(this.btStrategyDataPackage_Click);
            // 
            // tb_StrategyReport
            // 
            this.tb_StrategyReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_StrategyReport.Image = ((System.Drawing.Image)(resources.GetObject("tb_StrategyReport.Image")));
            this.tb_StrategyReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_StrategyReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_StrategyReport.Name = "tb_StrategyReport";
            this.tb_StrategyReport.Size = new System.Drawing.Size(28, 42);
            this.tb_StrategyReport.Text = "回测报告";
            this.tb_StrategyReport.Visible = false;
            // 
            // tb_StrategyResult
            // 
            this.tb_StrategyResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_StrategyResult.Image = ((System.Drawing.Image)(resources.GetObject("tb_StrategyResult.Image")));
            this.tb_StrategyResult.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_StrategyResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_StrategyResult.Name = "tb_StrategyResult";
            this.tb_StrategyResult.Size = new System.Drawing.Size(28, 42);
            this.tb_StrategyResult.Text = "策略结果";
            this.tb_StrategyResult.Click += new System.EventHandler(this.btStrategyResult_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            this.toolStripSeparator3.Visible = false;
            // 
            // tb_Connect
            // 
            this.tb_Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_Connect.Image = ((System.Drawing.Image)(resources.GetObject("tb_Connect.Image")));
            this.tb_Connect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Connect.Name = "tb_Connect";
            this.tb_Connect.Size = new System.Drawing.Size(28, 42);
            this.tb_Connect.Text = "连接服务器";
            this.tb_Connect.Visible = false;
            // 
            // tb_Trade
            // 
            this.tb_Trade.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_Trade.Image = ((System.Drawing.Image)(resources.GetObject("tb_Trade.Image")));
            this.tb_Trade.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_Trade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_Trade.Name = "tb_Trade";
            this.tb_Trade.Size = new System.Drawing.Size(28, 42);
            this.tb_Trade.Text = "交易";
            this.tb_Trade.Click += new System.EventHandler(this.tb_Trade_Click);
            // 
            // tb_AccountAna
            // 
            this.tb_AccountAna.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tb_AccountAna.Image = ((System.Drawing.Image)(resources.GetObject("tb_AccountAna.Image")));
            this.tb_AccountAna.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tb_AccountAna.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tb_AccountAna.Name = "tb_AccountAna";
            this.tb_AccountAna.Size = new System.Drawing.Size(28, 42);
            this.tb_AccountAna.Text = "账户分析";
            this.tb_AccountAna.Click += new System.EventHandler(this.tb_AccountAna_Click);
            // 
            // btLogout
            // 
            this.btLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btLogout.Image = ((System.Drawing.Image)(resources.GetObject("btLogout.Image")));
            this.btLogout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btLogout.Name = "btLogout";
            this.btLogout.Size = new System.Drawing.Size(28, 42);
            this.btLogout.Text = "登出";
            this.btLogout.Click += new System.EventHandler(this.btLogout_Click);
            // 
            // ToolStripComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "ToolStripComponent";
            this.Size = new System.Drawing.Size(937, 43);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.ToolStripButton tb_ForwardSetting;
        private System.Windows.Forms.ToolStripButton tb_BackwordTime;
        private System.Windows.Forms.ToolStripButton tb_Play;
        private System.Windows.Forms.ToolStripButton tb_ForwordTime;
        private System.Windows.Forms.ToolStripButton tb_Strategy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tb_StrategyResult;
        private System.Windows.Forms.ToolStripButton tb_StrategyDataPackage;
        private System.Windows.Forms.ToolStripButton tb_StrategyReport;
        private System.Windows.Forms.ToolStripButton tb_ChangeCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tb_Connect;
        private System.Windows.Forms.ToolStripButton tb_Trade;
        private System.Windows.Forms.ToolStripButton btLogout;
        private System.Windows.Forms.ToolStripButton tb_AccountAna;
        private System.Windows.Forms.ToolStripButton btRunState;
    }
}
