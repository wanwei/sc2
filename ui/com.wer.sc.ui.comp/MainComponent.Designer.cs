namespace com.wer.sc.ui.comp
{
    partial class MainComponent
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.currentInfoComponent1 = new com.wer.sc.ui.comp.CurrentInfoComponent();
            this.chartComponent1 = new com.wer.sc.ui.comp.ChartComponent();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartComponent1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.currentInfoComponent1);
            this.splitContainer1.Size = new System.Drawing.Size(793, 567);
            this.splitContainer1.SplitterDistance = 534;
            this.splitContainer1.TabIndex = 1;
            // 
            // currentInfoComponent1
            // 
            this.currentInfoComponent1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;            
            this.currentInfoComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentInfoComponent1.Location = new System.Drawing.Point(0, 0);
            this.currentInfoComponent1.Name = "currentInfoComponent1";
            this.currentInfoComponent1.Size = new System.Drawing.Size(255, 567);
            this.currentInfoComponent1.TabIndex = 0;
            // 
            // chartComponent1
            // 
            this.chartComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartComponent1.Location = new System.Drawing.Point(0, 0);
            this.chartComponent1.Name = "chartComponent1";
            this.chartComponent1.Size = new System.Drawing.Size(534, 567);
            this.chartComponent1.TabIndex = 0;
            // 
            // MainComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainComponent";
            this.Size = new System.Drawing.Size(793, 567);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ChartComponent chartComponent1;
        private CurrentInfoComponent currentInfoComponent1;
    }
}
