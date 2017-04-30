namespace com.wer.sc.data.market
{
    partial class FormRealDataReceiver
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
            this.tbTimeLineData = new System.Windows.Forms.TextBox();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.tbKLineData = new System.Windows.Forms.TextBox();
            this.cbRealTimeDataReader = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tbDataCenter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbTimeLineData
            // 
            this.tbTimeLineData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTimeLineData.Location = new System.Drawing.Point(462, 164);
            this.tbTimeLineData.Multiline = true;
            this.tbTimeLineData.Name = "tbTimeLineData";
            this.tbTimeLineData.Size = new System.Drawing.Size(422, 358);
            this.tbTimeLineData.TabIndex = 52;
            // 
            // btDisconnect
            // 
            this.btDisconnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDisconnect.Location = new System.Drawing.Point(682, 109);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 35);
            this.btDisconnect.TabIndex = 51;
            this.btDisconnect.Text = "断开";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // btConnect
            // 
            this.btConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btConnect.Location = new System.Drawing.Point(601, 109);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 35);
            this.btConnect.TabIndex = 50;
            this.btConnect.Text = "开始";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // tbKLineData
            // 
            this.tbKLineData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbKLineData.Location = new System.Drawing.Point(2, 164);
            this.tbKLineData.Multiline = true;
            this.tbKLineData.Name = "tbKLineData";
            this.tbKLineData.Size = new System.Drawing.Size(422, 358);
            this.tbKLineData.TabIndex = 48;
            // 
            // cbRealTimeDataReader
            // 
            this.cbRealTimeDataReader.AutoSize = true;
            this.cbRealTimeDataReader.Checked = true;
            this.cbRealTimeDataReader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRealTimeDataReader.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealTimeDataReader.Location = new System.Drawing.Point(601, 14);
            this.cbRealTimeDataReader.Name = "cbRealTimeDataReader";
            this.cbRealTimeDataReader.Size = new System.Drawing.Size(151, 24);
            this.cbRealTimeDataReader.TabIndex = 53;
            this.cbRealTimeDataReader.Text = "接收实时数据";
            this.cbRealTimeDataReader.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(601, 59);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 24);
            this.checkBox1.TabIndex = 54;
            this.checkBox1.Text = "数据持久化";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tbDataCenter
            // 
            this.tbDataCenter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDataCenter.Location = new System.Drawing.Point(349, 11);
            this.tbDataCenter.Name = "tbDataCenter";
            this.tbDataCenter.Size = new System.Drawing.Size(235, 30);
            this.tbDataCenter.TabIndex = 55;
            this.tbDataCenter.Text = "E:\\FUTURES\\MOCKDATACENTER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(204, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "数据中心";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(204, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 60;
            this.label3.Text = "持久化路径";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(349, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(235, 30);
            this.textBox3.TabIndex = 59;
            this.textBox3.Text = "D:\\SCTEST\\SCPRESENT";
            // 
            // FormRealDataReceiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 525);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDataCenter);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbRealTimeDataReader);
            this.Controls.Add(this.tbTimeLineData);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbKLineData);
            this.Name = "FormRealDataReceiver";
            this.Text = "FormRealDataReceiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbTimeLineData;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox tbKLineData;
        private System.Windows.Forms.CheckBox cbRealTimeDataReader;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox tbDataCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
    }
}