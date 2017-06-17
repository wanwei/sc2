namespace com.wer.sc.verify
{
    partial class FormMain
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
            this.btFormView = new System.Windows.Forms.Button();
            this.btFormReader = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btFormView
            // 
            this.btFormView.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btFormView.Location = new System.Drawing.Point(105, 93);
            this.btFormView.Name = "btFormView";
            this.btFormView.Size = new System.Drawing.Size(173, 41);
            this.btFormView.TabIndex = 1;
            this.btFormView.Text = "查看数据文件";
            this.btFormView.UseVisualStyleBackColor = true;
            this.btFormView.Click += new System.EventHandler(this.btFormView_Click);
            // 
            // btFormReader
            // 
            this.btFormReader.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btFormReader.Location = new System.Drawing.Point(341, 93);
            this.btFormReader.Name = "btFormReader";
            this.btFormReader.Size = new System.Drawing.Size(173, 41);
            this.btFormReader.TabIndex = 2;
            this.btFormReader.Text = "读取器";
            this.btFormReader.UseVisualStyleBackColor = true;
            this.btFormReader.Click += new System.EventHandler(this.btFormReader_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 507);
            this.Controls.Add(this.btFormReader);
            this.Controls.Add(this.btFormView);
            this.Name = "FormMain";
            this.Text = "测试工程";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btFormView;
        private System.Windows.Forms.Button btFormReader;
    }
}