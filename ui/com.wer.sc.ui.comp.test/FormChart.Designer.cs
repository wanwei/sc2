namespace com.wer.sc.ui.comp.test
{
    partial class FormChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChart));
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
            this.compChart1 = new com.wer.sc.ui.comp.CompChart();
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
            this.tb_ChangeTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(906, 45);
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
            // compChart1
            // 
            this.compChart1.Code = "m1601";
            this.compChart1.DataCenterUri = "E:\\scdata\\cnfutures\\";
            this.compChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compChart1.KLineBlockWidth = 4F;
            this.compChart1.Location = new System.Drawing.Point(0, 45);
            this.compChart1.Name = "compChart1";
            this.compChart1.Size = new System.Drawing.Size(906, 579);
            this.compChart1.TabIndex = 4;
            this.compChart1.Time = 20150626.093D;
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 624);
            this.Controls.Add(this.compChart1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormChart";
            this.Text = "测试图表控件";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private CompChart compChart1;
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
    }
}

