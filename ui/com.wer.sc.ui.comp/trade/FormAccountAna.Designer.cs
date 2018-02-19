namespace com.wer.sc.ui.comp.trade
{
    partial class FormAccountAna
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.itemAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEarnLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridTrade = new System.Windows.Forms.DataGridView();
            this.columnTradeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeOpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbEarnPercent = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbEnd = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbStart = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbEarnMoney = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbTradeCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbAsset = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbMoney = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbInitMoney = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAccount});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(895, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // itemAccount
            // 
            this.itemAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemEarnLine});
            this.itemAccount.Name = "itemAccount";
            this.itemAccount.Size = new System.Drawing.Size(51, 24);
            this.itemAccount.Text = "账户";
            // 
            // itemEarnLine
            // 
            this.itemEarnLine.Name = "itemEarnLine";
            this.itemEarnLine.Size = new System.Drawing.Size(181, 26);
            this.itemEarnLine.Text = "收益曲线";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridTrade);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(887, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "交易记录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridTrade
            // 
            this.gridTrade.AllowUserToAddRows = false;
            this.gridTrade.AllowUserToResizeRows = false;
            this.gridTrade.BackgroundColor = System.Drawing.Color.White;
            this.gridTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTrade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTradeID,
            this.columnTradeCode,
            this.columnTradeTime,
            this.columnTradeDirection,
            this.columnTradeMount,
            this.columnTradePrice,
            this.columnTradeOpenClose});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTrade.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrade.Location = new System.Drawing.Point(3, 3);
            this.gridTrade.MultiSelect = false;
            this.gridTrade.Name = "gridTrade";
            this.gridTrade.ReadOnly = true;
            this.gridTrade.RowHeadersVisible = false;
            this.gridTrade.RowTemplate.Height = 27;
            this.gridTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrade.Size = new System.Drawing.Size(881, 502);
            this.gridTrade.TabIndex = 4;
            this.gridTrade.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridTrade_MouseDoubleClick);
            // 
            // columnTradeID
            // 
            this.columnTradeID.HeaderText = "交易ID";
            this.columnTradeID.Name = "columnTradeID";
            this.columnTradeID.ReadOnly = true;
            this.columnTradeID.Visible = false;
            // 
            // columnTradeCode
            // 
            this.columnTradeCode.HeaderText = "代码";
            this.columnTradeCode.Name = "columnTradeCode";
            this.columnTradeCode.ReadOnly = true;
            // 
            // columnTradeTime
            // 
            this.columnTradeTime.HeaderText = "时间";
            this.columnTradeTime.Name = "columnTradeTime";
            this.columnTradeTime.ReadOnly = true;
            // 
            // columnTradeDirection
            // 
            this.columnTradeDirection.HeaderText = "方向";
            this.columnTradeDirection.Name = "columnTradeDirection";
            this.columnTradeDirection.ReadOnly = true;
            // 
            // columnTradeMount
            // 
            this.columnTradeMount.HeaderText = "数量";
            this.columnTradeMount.Name = "columnTradeMount";
            this.columnTradeMount.ReadOnly = true;
            // 
            // columnTradePrice
            // 
            this.columnTradePrice.HeaderText = "价格";
            this.columnTradePrice.Name = "columnTradePrice";
            this.columnTradePrice.ReadOnly = true;
            // 
            // columnTradeOpenClose
            // 
            this.columnTradeOpenClose.HeaderText = "开平仓";
            this.columnTradeOpenClose.Name = "columnTradeOpenClose";
            this.columnTradeOpenClose.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(895, 565);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.lbEarnPercent);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.lbEnd);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.lbStart);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.lbEarnMoney);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.lbTradeCount);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.lbAsset);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.lbMoney);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.lbInitMoney);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(887, 536);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "账户总览";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(206, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 19;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(77, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "盈利比率";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(206, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "label15";
            this.label5.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(77, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "盈利总数";
            this.label7.Visible = false;
            // 
            // lbEarnPercent
            // 
            this.lbEarnPercent.AutoSize = true;
            this.lbEarnPercent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEarnPercent.Location = new System.Drawing.Point(206, 336);
            this.lbEarnPercent.Name = "lbEarnPercent";
            this.lbEarnPercent.Size = new System.Drawing.Size(0, 20);
            this.lbEarnPercent.TabIndex = 15;
            this.lbEarnPercent.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(77, 336);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "盈利比率";
            this.label10.Visible = false;
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEnd.Location = new System.Drawing.Point(206, 53);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(0, 20);
            this.lbEnd.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(77, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 20);
            this.label12.TabIndex = 12;
            this.label12.Text = "当前时间";
            // 
            // lbStart
            // 
            this.lbStart.AutoSize = true;
            this.lbStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStart.Location = new System.Drawing.Point(206, 24);
            this.lbStart.Name = "lbStart";
            this.lbStart.Size = new System.Drawing.Size(0, 20);
            this.lbStart.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(77, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 20);
            this.label14.TabIndex = 10;
            this.label14.Text = "起始时间";
            // 
            // lbEarnMoney
            // 
            this.lbEarnMoney.AutoSize = true;
            this.lbEarnMoney.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbEarnMoney.Location = new System.Drawing.Point(206, 302);
            this.lbEarnMoney.Name = "lbEarnMoney";
            this.lbEarnMoney.Size = new System.Drawing.Size(0, 20);
            this.lbEarnMoney.TabIndex = 9;
            this.lbEarnMoney.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(77, 302);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 20);
            this.label16.TabIndex = 8;
            this.label16.Text = "盈利总数";
            this.label16.Visible = false;
            // 
            // lbTradeCount
            // 
            this.lbTradeCount.AutoSize = true;
            this.lbTradeCount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTradeCount.Location = new System.Drawing.Point(206, 180);
            this.lbTradeCount.Name = "lbTradeCount";
            this.lbTradeCount.Size = new System.Drawing.Size(69, 20);
            this.lbTradeCount.TabIndex = 7;
            this.lbTradeCount.Text = "label5";
            this.lbTradeCount.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(77, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "交易次数";
            this.label6.Visible = false;
            // 
            // lbAsset
            // 
            this.lbAsset.AutoSize = true;
            this.lbAsset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAsset.Location = new System.Drawing.Point(206, 150);
            this.lbAsset.Name = "lbAsset";
            this.lbAsset.Size = new System.Drawing.Size(0, 20);
            this.lbAsset.TabIndex = 5;
            this.lbAsset.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(77, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "当前资产";
            this.label8.Visible = false;
            // 
            // lbMoney
            // 
            this.lbMoney.AutoSize = true;
            this.lbMoney.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMoney.Location = new System.Drawing.Point(206, 121);
            this.lbMoney.Name = "lbMoney";
            this.lbMoney.Size = new System.Drawing.Size(69, 20);
            this.lbMoney.TabIndex = 3;
            this.lbMoney.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(77, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "当前资金";
            // 
            // lbInitMoney
            // 
            this.lbInitMoney.AutoSize = true;
            this.lbInitMoney.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInitMoney.Location = new System.Drawing.Point(206, 91);
            this.lbInitMoney.Name = "lbInitMoney";
            this.lbInitMoney.Size = new System.Drawing.Size(69, 20);
            this.lbInitMoney.TabIndex = 1;
            this.lbInitMoney.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(77, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "初始金额";
            // 
            // FormAccountAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 565);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAccountAna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户分析";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem itemAccount;
        private System.Windows.Forms.ToolStripMenuItem itemEarnLine;
        private System.Windows.Forms.DataGridView gridTrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeOpenClose;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbMoney;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbInitMoney;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbEarnPercent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbEnd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbStart;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbEarnMoney;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbTradeCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbAsset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}