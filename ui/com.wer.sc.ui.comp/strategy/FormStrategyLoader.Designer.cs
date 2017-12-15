namespace com.wer.sc.ui.comp.strategy
{
    partial class FormStrategyLoader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStrategyLoader));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.compStrategyTree1 = new com.wer.sc.ui.comp.strategy.StrategyTreeComponent();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(478, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btRefresh
            // 
            this.btRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btRefresh.Image")));
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(24, 24);
            this.btRefresh.Text = "刷新策略树";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // compStrategyTree1
            // 
            this.compStrategyTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compStrategyTree1.Location = new System.Drawing.Point(0, 27);
            this.compStrategyTree1.Name = "compStrategyTree1";
            this.compStrategyTree1.Size = new System.Drawing.Size(478, 377);
            this.compStrategyTree1.TabIndex = 2;
            // 
            // FormStrategyLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 404);
            this.Controls.Add(this.compStrategyTree1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormStrategyLoader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private StrategyTreeComponent compStrategyTree1;
    }
}