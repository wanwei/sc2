namespace com.wer.sc.data.checker
{
    partial class Form1
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
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.controlDataUpdate1 = new com.wer.sc.utils.ui.update.ControlDataUpdate();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPath.Location = new System.Drawing.Point(162, 123);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(391, 30);
            this.tbPath.TabIndex = 0;
            this.tbPath.Text = "E:\\FUTURES\\CSV\\DATACENTERSOURCE\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "暂存数据路径";
            // 
            // controlDataUpdate1
            // 
            this.controlDataUpdate1.DataProceed = null;
            this.controlDataUpdate1.Location = new System.Drawing.Point(29, 160);
            this.controlDataUpdate1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.controlDataUpdate1.Name = "controlDataUpdate1";
            this.controlDataUpdate1.Size = new System.Drawing.Size(736, 65);
            this.controlDataUpdate1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 577);
            this.Controls.Add(this.controlDataUpdate1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Name = "Form1";
            this.Text = "数据检测";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private sc.utils.ui.update.ControlDataUpdate controlDataUpdate1;
    }
}

