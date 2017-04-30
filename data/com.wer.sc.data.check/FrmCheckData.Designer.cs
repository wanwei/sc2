namespace com.wer.sc.data.check
{
    partial class FrmCheckData
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.btShowData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "品种";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(95, 40);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(100, 21);
            this.tbCode.TabIndex = 1;
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(306, 34);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(100, 21);
            this.tbDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "日期";
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(21, 90);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(559, 310);
            this.tbData.TabIndex = 4;
            // 
            // btShowData
            // 
            this.btShowData.Location = new System.Drawing.Point(445, 31);
            this.btShowData.Name = "btShowData";
            this.btShowData.Size = new System.Drawing.Size(75, 23);
            this.btShowData.TabIndex = 5;
            this.btShowData.Text = "显示";
            this.btShowData.UseVisualStyleBackColor = true;
            this.btShowData.Click += new System.EventHandler(this.btShowData_Click);
            // 
            // FrmCheckData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 412);
            this.Controls.Add(this.btShowData);
            this.Controls.Add(this.tbData);
            this.Controls.Add(this.tbDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label1);
            this.Name = "FrmCheckData";
            this.Text = "FrmCheckData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Button btShowData;
    }
}