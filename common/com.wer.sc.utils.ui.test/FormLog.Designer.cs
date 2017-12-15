namespace com.wer.sc.utils.ui
{
    partial class FormLog
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
            this.btPrintLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btPrintLog
            // 
            this.btPrintLog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btPrintLog.Location = new System.Drawing.Point(67, 43);
            this.btPrintLog.Name = "btPrintLog";
            this.btPrintLog.Size = new System.Drawing.Size(143, 50);
            this.btPrintLog.TabIndex = 0;
            this.btPrintLog.Text = "输出日志";
            this.btPrintLog.UseVisualStyleBackColor = true;
            this.btPrintLog.Click += new System.EventHandler(this.btPrintLog_Click);
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 492);
            this.Controls.Add(this.btPrintLog);
            this.Name = "FormLog";
            this.Text = "FormLog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btPrintLog;
    }
}