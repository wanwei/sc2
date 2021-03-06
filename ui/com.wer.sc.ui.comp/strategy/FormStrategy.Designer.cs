﻿namespace com.wer.sc.ui.comp.strategy
{
    partial class FormStrategy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStrategy));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.btChangeStrategy = new System.Windows.Forms.ToolStripButton();
            this.btStrategyDescription = new System.Windows.Forms.ToolStripButton();
            this.btStrategyDataPackage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btRunState = new System.Windows.Forms.ToolStripButton();
            this.btStrategyReport = new System.Windows.Forms.ToolStripButton();
            this.btStrategyResult = new System.Windows.Forms.ToolStripButton();
            this.btExecutor = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.compParameters1 = new com.wer.sc.graphic.param.CompParameters();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.compCodePackage1 = new com.wer.sc.ui.comp.CompCodePackage();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btRefresh,
            this.btChangeStrategy,
            this.btStrategyDescription,
            this.btStrategyDataPackage,
            this.toolStripSeparator1,
            this.btRunState,
            this.btStrategyReport,
            this.btStrategyResult});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(782, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btRefresh
            // 
            this.btRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btRefresh.Image")));
            this.btRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(28, 28);
            this.btRefresh.Text = "刷新策略";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btChangeStrategy
            // 
            this.btChangeStrategy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btChangeStrategy.Image = ((System.Drawing.Image)(resources.GetObject("btChangeStrategy.Image")));
            this.btChangeStrategy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btChangeStrategy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btChangeStrategy.Name = "btChangeStrategy";
            this.btChangeStrategy.Size = new System.Drawing.Size(28, 28);
            this.btChangeStrategy.Text = "切换策略";
            this.btChangeStrategy.Click += new System.EventHandler(this.btChangeStrategy_Click);
            // 
            // btStrategyDescription
            // 
            this.btStrategyDescription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btStrategyDescription.Image = ((System.Drawing.Image)(resources.GetObject("btStrategyDescription.Image")));
            this.btStrategyDescription.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btStrategyDescription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStrategyDescription.Name = "btStrategyDescription";
            this.btStrategyDescription.Size = new System.Drawing.Size(28, 28);
            this.btStrategyDescription.Text = "策略信息";
            this.btStrategyDescription.Click += new System.EventHandler(this.btStrategyDescription_Click);
            // 
            // btStrategyDataPackage
            // 
            this.btStrategyDataPackage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btStrategyDataPackage.Image = ((System.Drawing.Image)(resources.GetObject("btStrategyDataPackage.Image")));
            this.btStrategyDataPackage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btStrategyDataPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStrategyDataPackage.Name = "btStrategyDataPackage";
            this.btStrategyDataPackage.Size = new System.Drawing.Size(28, 28);
            this.btStrategyDataPackage.Text = "数据包";
            this.btStrategyDataPackage.Visible = false;
            this.btStrategyDataPackage.Click += new System.EventHandler(this.btStrategyDataPackage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btRunState
            // 
            this.btRunState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRunState.Image = ((System.Drawing.Image)(resources.GetObject("btRunState.Image")));
            this.btRunState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRunState.Name = "btRunState";
            this.btRunState.Size = new System.Drawing.Size(24, 28);
            this.btRunState.Text = "toolStripButton1";
            // 
            // btStrategyReport
            // 
            this.btStrategyReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btStrategyReport.Image = ((System.Drawing.Image)(resources.GetObject("btStrategyReport.Image")));
            this.btStrategyReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btStrategyReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStrategyReport.Name = "btStrategyReport";
            this.btStrategyReport.Size = new System.Drawing.Size(28, 28);
            this.btStrategyReport.Text = "回测报告";
            this.btStrategyReport.Click += new System.EventHandler(this.btStrategyReport_Click);
            // 
            // btStrategyResult
            // 
            this.btStrategyResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btStrategyResult.Image = ((System.Drawing.Image)(resources.GetObject("btStrategyResult.Image")));
            this.btStrategyResult.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btStrategyResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btStrategyResult.Name = "btStrategyResult";
            this.btStrategyResult.Size = new System.Drawing.Size(28, 28);
            this.btStrategyResult.Text = "搜索结果";
            this.btStrategyResult.Click += new System.EventHandler(this.btStrategyResult_Click);
            // 
            // btExecutor
            // 
            this.btExecutor.Dock = System.Windows.Forms.DockStyle.Right;
            this.btExecutor.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btExecutor.Location = new System.Drawing.Point(582, 0);
            this.btExecutor.Name = "btExecutor";
            this.btExecutor.Size = new System.Drawing.Size(100, 37);
            this.btExecutor.TabIndex = 11;
            this.btExecutor.Text = "执行";
            this.btExecutor.UseVisualStyleBackColor = true;
            this.btExecutor.Click += new System.EventHandler(this.btExecutor_Click);
            // 
            // btCancel
            // 
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.Location = new System.Drawing.Point(682, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 37);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(582, 37);
            this.progressBar1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 481);
            this.panel1.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(782, 481);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.compParameters1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(774, 452);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // compParameters1
            // 
            this.compParameters1.BackColor = System.Drawing.Color.Transparent;
            this.compParameters1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compParameters1.Location = new System.Drawing.Point(3, 3);
            this.compParameters1.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.compParameters1.Name = "compParameters1";
            this.compParameters1.Parameters = null;
            this.compParameters1.Size = new System.Drawing.Size(768, 446);
            this.compParameters1.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.compCodePackage1);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(774, 447);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(774, 447);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "配置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.btExecutor);
            this.splitContainer1.Panel2.Controls.Add(this.btCancel);
            this.splitContainer1.Size = new System.Drawing.Size(782, 522);
            this.splitContainer1.SplitterDistance = 481;
            this.splitContainer1.TabIndex = 11;
            // 
            // compCodePackage1
            // 
            this.compCodePackage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compCodePackage1.Location = new System.Drawing.Point(3, 3);
            this.compCodePackage1.Name = "compCodePackage1";
            this.compCodePackage1.Size = new System.Drawing.Size(768, 441);
            this.compCodePackage1.TabIndex = 0;
            // 
            // FormStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStrategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "策略";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.ToolStripButton btChangeStrategy;
        private System.Windows.Forms.Button btExecutor;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripButton btStrategyReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btStrategyDescription;
        private System.Windows.Forms.ToolStripButton btStrategyDataPackage;
        private System.Windows.Forms.ToolStripButton btStrategyResult;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private graphic.param.CompParameters compParameters1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private CompCodePackage compCodePackage1;
        private System.Windows.Forms.ToolStripButton btRunState;
    }
}