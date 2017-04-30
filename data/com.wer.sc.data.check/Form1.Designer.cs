namespace com.wer.sc.data.check
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
            this.btDataBrowser = new System.Windows.Forms.Button();
            this.btTestGen = new System.Windows.Forms.Button();
            this.btLoadReal = new System.Windows.Forms.Button();
            this.btDataNavigate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btDataBrowser
            // 
            this.btDataBrowser.Location = new System.Drawing.Point(166, 122);
            this.btDataBrowser.Name = "btDataBrowser";
            this.btDataBrowser.Size = new System.Drawing.Size(75, 23);
            this.btDataBrowser.TabIndex = 0;
            this.btDataBrowser.Text = "数据浏览";
            this.btDataBrowser.UseVisualStyleBackColor = true;
            this.btDataBrowser.Click += new System.EventHandler(this.btDataBrowser_Click);
            // 
            // btTestGen
            // 
            this.btTestGen.Location = new System.Drawing.Point(271, 122);
            this.btTestGen.Name = "btTestGen";
            this.btTestGen.Size = new System.Drawing.Size(75, 23);
            this.btTestGen.TabIndex = 1;
            this.btTestGen.Text = "测试生成";
            this.btTestGen.UseVisualStyleBackColor = true;
            this.btTestGen.Click += new System.EventHandler(this.btTestGen_Click);
            // 
            // btLoadReal
            // 
            this.btLoadReal.Location = new System.Drawing.Point(166, 162);
            this.btLoadReal.Name = "btLoadReal";
            this.btLoadReal.Size = new System.Drawing.Size(75, 23);
            this.btLoadReal.TabIndex = 2;
            this.btLoadReal.Text = "分时数据";
            this.btLoadReal.UseVisualStyleBackColor = true;
            this.btLoadReal.Click += new System.EventHandler(this.btLoadReal_Click);
            // 
            // btDataNavigate
            // 
            this.btDataNavigate.Location = new System.Drawing.Point(271, 162);
            this.btDataNavigate.Name = "btDataNavigate";
            this.btDataNavigate.Size = new System.Drawing.Size(75, 23);
            this.btDataNavigate.TabIndex = 3;
            this.btDataNavigate.Text = "数据导航";
            this.btDataNavigate.UseVisualStyleBackColor = true;
            this.btDataNavigate.Click += new System.EventHandler(this.btDataNavigate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 351);
            this.Controls.Add(this.btDataNavigate);
            this.Controls.Add(this.btLoadReal);
            this.Controls.Add(this.btTestGen);
            this.Controls.Add(this.btDataBrowser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btDataBrowser;
        private System.Windows.Forms.Button btTestGen;
        private System.Windows.Forms.Button btLoadReal;
        private System.Windows.Forms.Button btDataNavigate;
    }
}

