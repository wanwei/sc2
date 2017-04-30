namespace com.wer.sc.data.market
{
    partial class FormTestRealTimeDataBuilder
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
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.btUnSubscribe = new System.Windows.Forms.Button();
            this.tbKLineData = new System.Windows.Forms.TextBox();
            this.btSubscribe = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSubscribeCode = new System.Windows.Forms.TextBox();
            this.tbTimeLineData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btDisconnect
            // 
            this.btDisconnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDisconnect.Location = new System.Drawing.Point(171, 12);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 35);
            this.btDisconnect.TabIndex = 43;
            this.btDisconnect.Text = "断开";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // btConnect
            // 
            this.btConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btConnect.Location = new System.Drawing.Point(75, 12);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 35);
            this.btConnect.TabIndex = 42;
            this.btConnect.Text = "连接";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btUnSubscribe
            // 
            this.btUnSubscribe.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUnSubscribe.Location = new System.Drawing.Point(541, 79);
            this.btUnSubscribe.Name = "btUnSubscribe";
            this.btUnSubscribe.Size = new System.Drawing.Size(118, 35);
            this.btUnSubscribe.TabIndex = 41;
            this.btUnSubscribe.Text = "取消订阅";
            this.btUnSubscribe.UseVisualStyleBackColor = true;
            this.btUnSubscribe.Click += new System.EventHandler(this.btUnSubscribe_Click);
            // 
            // tbKLineData
            // 
            this.tbKLineData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbKLineData.Location = new System.Drawing.Point(13, 168);
            this.tbKLineData.Multiline = true;
            this.tbKLineData.Name = "tbKLineData";
            this.tbKLineData.Size = new System.Drawing.Size(422, 358);
            this.tbKLineData.TabIndex = 40;
            // 
            // btSubscribe
            // 
            this.btSubscribe.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSubscribe.Location = new System.Drawing.Point(460, 79);
            this.btSubscribe.Name = "btSubscribe";
            this.btSubscribe.Size = new System.Drawing.Size(75, 35);
            this.btSubscribe.TabIndex = 39;
            this.btSubscribe.Text = "订阅";
            this.btSubscribe.UseVisualStyleBackColor = true;
            this.btSubscribe.Click += new System.EventHandler(this.btSubscribe_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "代码";
            // 
            // tbSubscribeCode
            // 
            this.tbSubscribeCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSubscribeCode.Location = new System.Drawing.Point(75, 81);
            this.tbSubscribeCode.Name = "tbSubscribeCode";
            this.tbSubscribeCode.Size = new System.Drawing.Size(360, 30);
            this.tbSubscribeCode.TabIndex = 37;
            this.tbSubscribeCode.Text = "m05,rb05";
            // 
            // tbTimeLineData
            // 
            this.tbTimeLineData.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTimeLineData.Location = new System.Drawing.Point(473, 168);
            this.tbTimeLineData.Multiline = true;
            this.tbTimeLineData.Name = "tbTimeLineData";
            this.tbTimeLineData.Size = new System.Drawing.Size(422, 358);
            this.tbTimeLineData.TabIndex = 44;
            // 
            // FormTestRealTimeDataBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 523);
            this.Controls.Add(this.tbTimeLineData);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.btUnSubscribe);
            this.Controls.Add(this.tbKLineData);
            this.Controls.Add(this.btSubscribe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSubscribeCode);
            this.Name = "FormTestRealTimeDataBuilder";
            this.Text = "FormTestRealTimeDataBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btUnSubscribe;
        private System.Windows.Forms.TextBox tbKLineData;
        private System.Windows.Forms.Button btSubscribe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSubscribeCode;
        private System.Windows.Forms.TextBox tbTimeLineData;
    }
}