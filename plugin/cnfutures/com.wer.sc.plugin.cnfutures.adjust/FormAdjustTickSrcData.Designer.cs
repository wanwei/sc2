namespace com.wer.sc.plugin.cnfutures.adjust
{
    partial class FormAdjustTickSrcData
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
            this.controlDataUpdate1 = new com.wer.sc.utils.ui.update.ControlDataUpdate();
            this.SuspendLayout();
            // 
            // controlDataUpdate1
            // 
            this.controlDataUpdate1.DataProceed = null;
            this.controlDataUpdate1.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlDataUpdate1.Location = new System.Drawing.Point(0, 0);
            this.controlDataUpdate1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.controlDataUpdate1.Name = "controlDataUpdate1";
            this.controlDataUpdate1.Size = new System.Drawing.Size(752, 65);
            this.controlDataUpdate1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 67);
            this.Controls.Add(this.controlDataUpdate1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private utils.ui.update.ControlDataUpdate controlDataUpdate1;
    }
}

