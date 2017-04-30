namespace com.wer.sc.data.market
{
    partial class FormTestTragger_Writer
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
            this.btWrite = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btStopWrite = new System.Windows.Forms.Button();
            this.tbTickReceived = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btWrite
            // 
            this.btWrite.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btWrite.Location = new System.Drawing.Point(397, 23);
            this.btWrite.Name = "btWrite";
            this.btWrite.Size = new System.Drawing.Size(132, 35);
            this.btWrite.TabIndex = 36;
            this.btWrite.Text = "开始写入";
            this.btWrite.UseVisualStyleBackColor = true;
            this.btWrite.Click += new System.EventHandler(this.btWrite_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(148, 28);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(226, 25);
            this.tbPath.TabIndex = 37;
            this.tbPath.Text = "d:\\sctest\\datawriter\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(48, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 38;
            this.label1.Text = "路径";
            // 
            // btStopWrite
            // 
            this.btStopWrite.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btStopWrite.Location = new System.Drawing.Point(535, 23);
            this.btStopWrite.Name = "btStopWrite";
            this.btStopWrite.Size = new System.Drawing.Size(132, 35);
            this.btStopWrite.TabIndex = 39;
            this.btStopWrite.Text = "停止写入";
            this.btStopWrite.UseVisualStyleBackColor = true;
            this.btStopWrite.Click += new System.EventHandler(this.btStopWrite_Click);
            // 
            // tbTickReceived
            // 
            this.tbTickReceived.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTickReceived.Location = new System.Drawing.Point(28, 77);
            this.tbTickReceived.Multiline = true;
            this.tbTickReceived.Name = "tbTickReceived";
            this.tbTickReceived.Size = new System.Drawing.Size(660, 320);
            this.tbTickReceived.TabIndex = 40;
            // 
            // FormTestTragger_Writer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 409);
            this.Controls.Add(this.tbTickReceived);
            this.Controls.Add(this.btStopWrite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btWrite);
            this.Name = "FormTestTragger_Writer";
            this.Text = "FormTestTragger_Writer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btWrite;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btStopWrite;
        private System.Windows.Forms.TextBox tbTickReceived;
    }
}