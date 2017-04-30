namespace com.wer.sc.data.market
{
    partial class FormTestReceiveTickData
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
            this.btSubscribe = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSubscribeCode = new System.Windows.Forms.TextBox();
            this.tbTickReceived = new System.Windows.Forms.TextBox();
            this.btUnSubscribe = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSubscribe
            // 
            this.btSubscribe.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSubscribe.Location = new System.Drawing.Point(470, 128);
            this.btSubscribe.Name = "btSubscribe";
            this.btSubscribe.Size = new System.Drawing.Size(75, 35);
            this.btSubscribe.TabIndex = 32;
            this.btSubscribe.Text = "订阅";
            this.btSubscribe.UseVisualStyleBackColor = true;
            this.btSubscribe.Click += new System.EventHandler(this.btSubscribe_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(30, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "代码";
            // 
            // tbSubscribeCode
            // 
            this.tbSubscribeCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSubscribeCode.Location = new System.Drawing.Point(85, 130);
            this.tbSubscribeCode.Name = "tbSubscribeCode";
            this.tbSubscribeCode.Size = new System.Drawing.Size(360, 30);
            this.tbSubscribeCode.TabIndex = 30;
            this.tbSubscribeCode.Text = "m05,rb05";
            // 
            // tbTickReceived
            // 
            this.tbTickReceived.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTickReceived.Location = new System.Drawing.Point(23, 192);
            this.tbTickReceived.Multiline = true;
            this.tbTickReceived.Name = "tbTickReceived";
            this.tbTickReceived.Size = new System.Drawing.Size(660, 320);
            this.tbTickReceived.TabIndex = 33;
            // 
            // btUnSubscribe
            // 
            this.btUnSubscribe.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUnSubscribe.Location = new System.Drawing.Point(551, 128);
            this.btUnSubscribe.Name = "btUnSubscribe";
            this.btUnSubscribe.Size = new System.Drawing.Size(118, 35);
            this.btUnSubscribe.TabIndex = 34;
            this.btUnSubscribe.Text = "取消订阅";
            this.btUnSubscribe.UseVisualStyleBackColor = true;
            this.btUnSubscribe.Click += new System.EventHandler(this.btUnSubscribe_Click);
            // 
            // btConnect
            // 
            this.btConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btConnect.Location = new System.Drawing.Point(85, 61);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 35);
            this.btConnect.TabIndex = 35;
            this.btConnect.Text = "连接";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDisconnect.Location = new System.Drawing.Point(181, 61);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 35);
            this.btDisconnect.TabIndex = 36;
            this.btDisconnect.Text = "断开";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // FormTestReceiveTickData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 524);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.btUnSubscribe);
            this.Controls.Add(this.tbTickReceived);
            this.Controls.Add(this.btSubscribe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSubscribeCode);
            this.Name = "FormTestReceiveTickData";
            this.Text = "测试数据接收";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSubscribe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSubscribeCode;
        private System.Windows.Forms.TextBox tbTickReceived;
        private System.Windows.Forms.Button btUnSubscribe;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
    }
}