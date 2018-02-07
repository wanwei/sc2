namespace com.wer.sc.ui.comp.trade
{
    partial class FormNewAccount
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
            this.tbSlipPrice = new System.Windows.Forms.TextBox();
            this.tbSlipPercent = new System.Windows.Forms.TextBox();
            this.tbSlipMinPrice = new System.Windows.Forms.TextBox();
            this.cbSlipType = new System.Windows.Forms.ComboBox();
            this.tbLateTickTrading = new System.Windows.Forms.TextBox();
            this.tbAccountName = new System.Windows.Forms.TextBox();
            this.tbMoney = new System.Windows.Forms.TextBox();
            this.tbLateTrading = new System.Windows.Forms.TextBox();
            this.cbTradeType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbSlipPrice
            // 
            this.tbSlipPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSlipPrice.Location = new System.Drawing.Point(193, 292);
            this.tbSlipPrice.Name = "tbSlipPrice";
            this.tbSlipPrice.Size = new System.Drawing.Size(224, 30);
            this.tbSlipPrice.TabIndex = 98;
            // 
            // tbSlipPercent
            // 
            this.tbSlipPercent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSlipPercent.Location = new System.Drawing.Point(193, 257);
            this.tbSlipPercent.Name = "tbSlipPercent";
            this.tbSlipPercent.Size = new System.Drawing.Size(224, 30);
            this.tbSlipPercent.TabIndex = 90;
            // 
            // tbSlipMinPrice
            // 
            this.tbSlipMinPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSlipMinPrice.Location = new System.Drawing.Point(193, 222);
            this.tbSlipMinPrice.Name = "tbSlipMinPrice";
            this.tbSlipMinPrice.Size = new System.Drawing.Size(224, 30);
            this.tbSlipMinPrice.TabIndex = 91;
            // 
            // cbSlipType
            // 
            this.cbSlipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlipType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSlipType.FormattingEnabled = true;
            this.cbSlipType.Items.AddRange(new object[] {
            "不滑点",
            "最小价格滑点",
            "百分比价格滑点",
            "绝对价格滑点"});
            this.cbSlipType.Location = new System.Drawing.Point(193, 187);
            this.cbSlipType.Name = "cbSlipType";
            this.cbSlipType.Size = new System.Drawing.Size(224, 28);
            this.cbSlipType.TabIndex = 97;
            // 
            // tbLateTickTrading
            // 
            this.tbLateTickTrading.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLateTickTrading.Location = new System.Drawing.Point(193, 152);
            this.tbLateTickTrading.Name = "tbLateTickTrading";
            this.tbLateTickTrading.Size = new System.Drawing.Size(224, 30);
            this.tbLateTickTrading.TabIndex = 92;
            // 
            // tbAccountName
            // 
            this.tbAccountName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAccountName.Location = new System.Drawing.Point(193, 12);
            this.tbAccountName.Name = "tbAccountName";
            this.tbAccountName.Size = new System.Drawing.Size(224, 30);
            this.tbAccountName.TabIndex = 93;
            // 
            // tbMoney
            // 
            this.tbMoney.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMoney.Location = new System.Drawing.Point(193, 47);
            this.tbMoney.Name = "tbMoney";
            this.tbMoney.Size = new System.Drawing.Size(224, 30);
            this.tbMoney.TabIndex = 95;
            // 
            // tbLateTrading
            // 
            this.tbLateTrading.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLateTrading.Location = new System.Drawing.Point(193, 117);
            this.tbLateTrading.Name = "tbLateTrading";
            this.tbLateTrading.Size = new System.Drawing.Size(224, 30);
            this.tbLateTrading.TabIndex = 94;
            // 
            // cbTradeType
            // 
            this.cbTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTradeType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTradeType.FormattingEnabled = true;
            this.cbTradeType.Items.AddRange(new object[] {
            "立即成交",
            "市价成交",
            "延时成交",
            "延迟tick成交"});
            this.cbTradeType.Location = new System.Drawing.Point(193, 82);
            this.cbTradeType.Name = "cbTradeType";
            this.cbTradeType.Size = new System.Drawing.Size(224, 28);
            this.cbTradeType.TabIndex = 96;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(40, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 20);
            this.label9.TabIndex = 89;
            this.label9.Text = "绝对价格滑点";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(19, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 20);
            this.label8.TabIndex = 87;
            this.label8.Text = "百分比价格滑点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(40, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 20);
            this.label7.TabIndex = 88;
            this.label7.Text = "最小价格滑点";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(82, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 82;
            this.label6.Text = "滑点方式";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(38, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 83;
            this.label5.Text = "延迟tick成交";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(82, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 84;
            this.label4.Text = "延迟成交";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(82, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 85;
            this.label3.Text = "交易方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(103, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 81;
            this.label1.Text = "账号名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 86;
            this.label2.Text = "账号初始金额";
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.Location = new System.Drawing.Point(225, 349);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(184, 35);
            this.btCancel.TabIndex = 102;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.Location = new System.Drawing.Point(23, 349);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(184, 35);
            this.btOK.TabIndex = 101;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // FormNewAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 399);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbSlipPrice);
            this.Controls.Add(this.tbSlipPercent);
            this.Controls.Add(this.tbSlipMinPrice);
            this.Controls.Add(this.cbSlipType);
            this.Controls.Add(this.tbLateTickTrading);
            this.Controls.Add(this.tbAccountName);
            this.Controls.Add(this.tbMoney);
            this.Controls.Add(this.tbLateTrading);
            this.Controls.Add(this.cbTradeType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "FormNewAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建账号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSlipPrice;
        private System.Windows.Forms.TextBox tbSlipPercent;
        private System.Windows.Forms.TextBox tbSlipMinPrice;
        private System.Windows.Forms.ComboBox cbSlipType;
        private System.Windows.Forms.TextBox tbLateTickTrading;
        private System.Windows.Forms.TextBox tbAccountName;
        private System.Windows.Forms.TextBox tbMoney;
        private System.Windows.Forms.TextBox tbLateTrading;
        private System.Windows.Forms.ComboBox cbTradeType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
    }
}