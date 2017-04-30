namespace com.wer.sc.ui
{
    partial class FormMarket
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridSubscribe = new System.Windows.Forms.DataGridView();
            this.tbQueryInfo = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbConnections = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMarket = new System.Windows.Forms.ComboBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btBuy = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btSubscribe = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.tbSubscribeCode = new System.Windows.Forms.TextBox();
            this.btSell = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMount = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridPosition = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridOrder = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridTrade = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubscribe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridSubscribe);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbQueryInfo);
            this.splitContainer2.Size = new System.Drawing.Size(1431, 316);
            this.splitContainer2.SplitterDistance = 648;
            this.splitContainer2.TabIndex = 2;
            // 
            // gridMarketData
            // 
            this.gridSubscribe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSubscribe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSubscribe.Location = new System.Drawing.Point(0, 0);
            this.gridSubscribe.MultiSelect = false;
            this.gridSubscribe.Name = "gridMarketData";
            this.gridSubscribe.ReadOnly = true;
            this.gridSubscribe.RowTemplate.Height = 27;
            this.gridSubscribe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSubscribe.Size = new System.Drawing.Size(648, 316);
            this.gridSubscribe.TabIndex = 1;
            this.gridSubscribe.SelectionChanged += new System.EventHandler(this.gridMarketData_SelectionChanged);
            // 
            // tbQueryInfo
            // 
            this.tbQueryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQueryInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbQueryInfo.Location = new System.Drawing.Point(0, 0);
            this.tbQueryInfo.Multiline = true;
            this.tbQueryInfo.Name = "tbQueryInfo";
            this.tbQueryInfo.Size = new System.Drawing.Size(779, 316);
            this.tbQueryInfo.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1431, 636);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(1431, 316);
            this.splitContainer3.SplitterDistance = 646;
            this.splitContainer3.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbCode);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbConnections);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbMarket);
            this.panel1.Controls.Add(this.btConnect);
            this.panel1.Controls.Add(this.btDisconnect);
            this.panel1.Controls.Add(this.btBuy);
            this.panel1.Controls.Add(this.btClose);
            this.panel1.Controls.Add(this.btSubscribe);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbPrice);
            this.panel1.Controls.Add(this.tbSubscribeCode);
            this.panel1.Controls.Add(this.btSell);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbMount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 314);
            this.panel1.TabIndex = 32;
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.Location = new System.Drawing.Point(86, 172);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(125, 30);
            this.tbCode.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(31, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 36;
            this.label6.Text = "代码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(294, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 35;
            this.label5.Text = "连接";
            // 
            // cbConnections
            // 
            this.cbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnections.FormattingEnabled = true;
            this.cbConnections.Location = new System.Drawing.Point(349, 14);
            this.cbConnections.Name = "cbConnections";
            this.cbConnections.Size = new System.Drawing.Size(292, 28);
            this.cbConnections.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(31, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "市场";
            // 
            // cbMarket
            // 
            this.cbMarket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarket.FormattingEnabled = true;
            this.cbMarket.Location = new System.Drawing.Point(86, 14);
            this.cbMarket.Name = "cbMarket";
            this.cbMarket.Size = new System.Drawing.Size(194, 28);
            this.cbMarket.TabIndex = 32;
            this.cbMarket.SelectedIndexChanged += new System.EventHandler(this.cbMarket_SelectedIndexChanged);
            // 
            // btConnect
            // 
            this.btConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btConnect.Location = new System.Drawing.Point(86, 60);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(187, 35);
            this.btConnect.TabIndex = 26;
            this.btConnect.Text = "连接";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDisconnect.Location = new System.Drawing.Point(298, 60);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(187, 35);
            this.btDisconnect.TabIndex = 31;
            this.btDisconnect.Text = "断开";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // btBuy
            // 
            this.btBuy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btBuy.Location = new System.Drawing.Point(86, 227);
            this.btBuy.Name = "btBuy";
            this.btBuy.Size = new System.Drawing.Size(99, 35);
            this.btBuy.TabIndex = 16;
            this.btBuy.Text = "买";
            this.btBuy.UseVisualStyleBackColor = true;
            this.btBuy.Click += new System.EventHandler(this.btBuy_Click);
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClose.Location = new System.Drawing.Point(336, 227);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(99, 35);
            this.btClose.TabIndex = 17;
            this.btClose.Text = "平仓";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btSubscribe
            // 
            this.btSubscribe.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSubscribe.Location = new System.Drawing.Point(452, 119);
            this.btSubscribe.Name = "btSubscribe";
            this.btSubscribe.Size = new System.Drawing.Size(75, 35);
            this.btSubscribe.TabIndex = 29;
            this.btSubscribe.Text = "订阅";
            this.btSubscribe.UseVisualStyleBackColor = true;
            this.btSubscribe.Click += new System.EventHandler(this.btSubscribe_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(31, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "代码";
            // 
            // tbPrice
            // 
            this.tbPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPrice.Location = new System.Drawing.Point(286, 172);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(100, 30);
            this.tbPrice.TabIndex = 19;
            // 
            // tbSubscribeCode
            // 
            this.tbSubscribeCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSubscribeCode.Location = new System.Drawing.Point(86, 121);
            this.tbSubscribeCode.Name = "tbSubscribeCode";
            this.tbSubscribeCode.Size = new System.Drawing.Size(360, 30);
            this.tbSubscribeCode.TabIndex = 27;
            // 
            // btSell
            // 
            this.btSell.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSell.Location = new System.Drawing.Point(211, 227);
            this.btSell.Name = "btSell";
            this.btSell.Size = new System.Drawing.Size(99, 35);
            this.btSell.TabIndex = 20;
            this.btSell.Text = "卖";
            this.btSell.UseVisualStyleBackColor = true;
            this.btSell.Click += new System.EventHandler(this.btSell_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(231, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "价格";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(392, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "量";
            // 
            // tbMount
            // 
            this.tbMount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMount.Location = new System.Drawing.Point(427, 172);
            this.tbMount.Name = "tbMount";
            this.tbMount.Size = new System.Drawing.Size(53, 30);
            this.tbMount.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(779, 314);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridPosition);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(771, 280);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "持仓";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridHold
            // 
            this.gridPosition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPosition.Location = new System.Drawing.Point(3, 3);
            this.gridPosition.Name = "gridHold";
            this.gridPosition.RowTemplate.Height = 27;
            this.gridPosition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPosition.Size = new System.Drawing.Size(765, 274);
            this.gridPosition.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridOrder);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(771, 280);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "委托";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridOrder
            // 
            this.gridOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrder.Location = new System.Drawing.Point(3, 3);
            this.gridOrder.Name = "gridOrder";
            this.gridOrder.RowTemplate.Height = 27;
            this.gridOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrder.Size = new System.Drawing.Size(765, 274);
            this.gridOrder.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridTrade);
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(771, 280);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "成交";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridTrade
            // 
            this.gridTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrade.Location = new System.Drawing.Point(3, 3);
            this.gridTrade.Name = "gridTrade";
            this.gridTrade.RowTemplate.Height = 27;
            this.gridTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrade.Size = new System.Drawing.Size(765, 274);
            this.gridTrade.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(771, 280);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "资金";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 30);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(771, 280);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "合约";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // FormMarket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 636);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMarket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "市场";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubscribe)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbQueryInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btSubscribe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSubscribeCode;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMount;
        private System.Windows.Forms.Button btSell;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btBuy;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView gridPosition;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridOrder;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gridTrade;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMarket;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbConnections;
        private System.Windows.Forms.DataGridView gridSubscribe;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label6;
    }
}