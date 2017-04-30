namespace com.wer.sc.data.check
{
    partial class FrmDataNavigate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btTickIndeier = new System.Windows.Forms.Button();
            this.btLoadAll = new System.Windows.Forms.Button();
            this.btLoadCurrentKLineChart = new System.Windows.Forms.Button();
            this.cbProvider = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btLoadMinuteKLineChart = new System.Windows.Forms.Button();
            this.cbPeriod = new System.Windows.Forms.ComboBox();
            this.tbPeriod = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btTickIndeier);
            this.splitContainer1.Panel1.Controls.Add(this.btLoadAll);
            this.splitContainer1.Panel1.Controls.Add(this.btLoadCurrentKLineChart);
            this.splitContainer1.Panel1.Controls.Add(this.cbProvider);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.tbTime);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.btLoadMinuteKLineChart);
            this.splitContainer1.Panel1.Controls.Add(this.cbPeriod);
            this.splitContainer1.Panel1.Controls.Add(this.tbPeriod);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.tbEnd);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.tbStart);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.tbCode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbData);
            this.splitContainer1.Size = new System.Drawing.Size(698, 421);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.TabIndex = 2;
            // 
            // btTickIndeier
            // 
            this.btTickIndeier.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btTickIndeier.Location = new System.Drawing.Point(16, 81);
            this.btTickIndeier.Name = "btTickIndeier";
            this.btTickIndeier.Size = new System.Drawing.Size(124, 28);
            this.btTickIndeier.TabIndex = 32;
            this.btTickIndeier.Text = "TICK索引器";
            this.btTickIndeier.UseVisualStyleBackColor = true;
            this.btTickIndeier.Click += new System.EventHandler(this.btIndeier_Click);
            // 
            // btLoadAll
            // 
            this.btLoadAll.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLoadAll.Location = new System.Drawing.Point(484, 81);
            this.btLoadAll.Name = "btLoadAll";
            this.btLoadAll.Size = new System.Drawing.Size(185, 28);
            this.btLoadAll.TabIndex = 31;
            this.btLoadAll.Text = "装载到当前时间所有K线";
            this.btLoadAll.UseVisualStyleBackColor = true;
            this.btLoadAll.Click += new System.EventHandler(this.btLoadAll_Click);
            // 
            // btLoadCurrentKLineChart
            // 
            this.btLoadCurrentKLineChart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLoadCurrentKLineChart.Location = new System.Drawing.Point(320, 81);
            this.btLoadCurrentKLineChart.Name = "btLoadCurrentKLineChart";
            this.btLoadCurrentKLineChart.Size = new System.Drawing.Size(145, 28);
            this.btLoadCurrentKLineChart.TabIndex = 30;
            this.btLoadCurrentKLineChart.Text = "装载当前时间K线";
            this.btLoadCurrentKLineChart.UseVisualStyleBackColor = true;
            this.btLoadCurrentKLineChart.Click += new System.EventHandler(this.btLoadCurrentKLineChart_Click);
            // 
            // cbProvider
            // 
            this.cbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProvider.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbProvider.FormattingEnabled = true;
            this.cbProvider.Location = new System.Drawing.Point(94, 51);
            this.cbProvider.Name = "cbProvider";
            this.cbProvider.Size = new System.Drawing.Size(95, 24);
            this.cbProvider.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(22, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "数据提供";
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTime.Location = new System.Drawing.Point(272, 48);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(167, 26);
            this.tbTime.TabIndex = 13;
            this.tbTime.Text = "20100105.093213";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(203, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "当前时间";
            // 
            // btLoadMinuteKLineChart
            // 
            this.btLoadMinuteKLineChart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLoadMinuteKLineChart.Location = new System.Drawing.Point(149, 81);
            this.btLoadMinuteKLineChart.Name = "btLoadMinuteKLineChart";
            this.btLoadMinuteKLineChart.Size = new System.Drawing.Size(146, 28);
            this.btLoadMinuteKLineChart.TabIndex = 11;
            this.btLoadMinuteKLineChart.Text = "装载当前1分钟k线";
            this.btLoadMinuteKLineChart.UseVisualStyleBackColor = true;
            this.btLoadMinuteKLineChart.Click += new System.EventHandler(this.btLoadMinuteKLineChart_Click);
            // 
            // cbPeriod
            // 
            this.cbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriod.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbPeriod.FormattingEnabled = true;
            this.cbPeriod.Location = new System.Drawing.Point(586, 15);
            this.cbPeriod.Name = "cbPeriod";
            this.cbPeriod.Size = new System.Drawing.Size(87, 24);
            this.cbPeriod.TabIndex = 10;
            // 
            // tbPeriod
            // 
            this.tbPeriod.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPeriod.Location = new System.Drawing.Point(517, 15);
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(63, 26);
            this.tbPeriod.TabIndex = 9;
            this.tbPeriod.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(471, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "周期";
            // 
            // tbEnd
            // 
            this.tbEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbEnd.Location = new System.Drawing.Point(381, 15);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(84, 26);
            this.tbEnd.TabIndex = 5;
            this.tbEnd.Text = "20150101";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(312, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "结束时间";
            // 
            // tbStart
            // 
            this.tbStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbStart.Location = new System.Drawing.Point(206, 15);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(87, 26);
            this.tbStart.TabIndex = 3;
            this.tbStart.Text = "20100101";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(128, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间";
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.Location = new System.Drawing.Point(59, 15);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(63, 26);
            this.tbCode.TabIndex = 1;
            this.tbCode.Text = "M13";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "代码";
            // 
            // tbData
            // 
            this.tbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbData.Location = new System.Drawing.Point(0, 0);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(698, 298);
            this.tbData.TabIndex = 14;
            // 
            // FrmDataNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 421);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmDataNavigate";
            this.Text = "FrmDataNavigate";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btLoadMinuteKLineChart;
        private System.Windows.Forms.ComboBox cbPeriod;
        private System.Windows.Forms.TextBox tbPeriod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.ComboBox cbProvider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btLoadCurrentKLineChart;
        private System.Windows.Forms.Button btLoadAll;
        private System.Windows.Forms.Button btTickIndeier;
    }
}