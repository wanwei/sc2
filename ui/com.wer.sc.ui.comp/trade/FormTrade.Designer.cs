namespace com.wer.sc.ui.comp.trade
{
    partial class FormTrade
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.tbMount = new System.Windows.Forms.TextBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btOrderDown = new System.Windows.Forms.Button();
            this.btOrderUp = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgOpenInterest = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgOrder = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgTrade = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenInterest)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrder)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrade)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbPrice);
            this.splitContainer1.Panel1.Controls.Add(this.tbMount);
            this.splitContainer1.Panel1.Controls.Add(this.tbCode);
            this.splitContainer1.Panel1.Controls.Add(this.btClose);
            this.splitContainer1.Panel1.Controls.Add(this.btOrderDown);
            this.splitContainer1.Panel1.Controls.Add(this.btOrderUp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1074, 356);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(264, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "价格";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(145, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "合约";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(267, 92);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(100, 25);
            this.tbPrice.TabIndex = 8;
            // 
            // tbMount
            // 
            this.tbMount.Location = new System.Drawing.Point(148, 92);
            this.tbMount.Name = "tbMount";
            this.tbMount.Size = new System.Drawing.Size(77, 25);
            this.tbMount.TabIndex = 7;
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(29, 92);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(78, 25);
            this.tbCode.TabIndex = 6;
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClose.ForeColor = System.Drawing.Color.Blue;
            this.btClose.Location = new System.Drawing.Point(267, 150);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(90, 66);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "平仓";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            this.btClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btClose_MouseUp);
            // 
            // btOrderDown
            // 
            this.btOrderDown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOrderDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btOrderDown.Location = new System.Drawing.Point(148, 150);
            this.btOrderDown.Name = "btOrderDown";
            this.btOrderDown.Size = new System.Drawing.Size(90, 66);
            this.btOrderDown.TabIndex = 4;
            this.btOrderDown.Text = "卖空";
            this.btOrderDown.UseVisualStyleBackColor = true;
            this.btOrderDown.Click += new System.EventHandler(this.btOrderDown_Click);
            this.btOrderDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btOrderDown_MouseUp);
            // 
            // btOrderUp
            // 
            this.btOrderUp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOrderUp.ForeColor = System.Drawing.Color.Red;
            this.btOrderUp.Location = new System.Drawing.Point(29, 150);
            this.btOrderUp.Name = "btOrderUp";
            this.btOrderUp.Size = new System.Drawing.Size(90, 66);
            this.btOrderUp.TabIndex = 3;
            this.btOrderUp.Text = "买多";
            this.btOrderUp.UseVisualStyleBackColor = true;
            this.btOrderUp.Click += new System.EventHandler(this.btOrderUp_Click);
            this.btOrderUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btOrderUp_MouseUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(670, 356);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgOpenInterest);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(662, 327);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "持仓";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgOpenInterest
            // 
            this.dgOpenInterest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOpenInterest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOpenInterest.Location = new System.Drawing.Point(3, 3);
            this.dgOpenInterest.Name = "dgOpenInterest";
            this.dgOpenInterest.ReadOnly = true;
            this.dgOpenInterest.RowTemplate.Height = 27;
            this.dgOpenInterest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOpenInterest.Size = new System.Drawing.Size(656, 321);
            this.dgOpenInterest.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgOrder);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(662, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "委托";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgOrder
            // 
            this.dgOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOrder.Location = new System.Drawing.Point(3, 3);
            this.dgOrder.Name = "dgOrder";
            this.dgOrder.ReadOnly = true;
            this.dgOrder.RowTemplate.Height = 27;
            this.dgOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOrder.Size = new System.Drawing.Size(656, 321);
            this.dgOrder.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgTrade);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(662, 327);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "成交";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgTrade
            // 
            this.dgTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTrade.Location = new System.Drawing.Point(0, 0);
            this.dgTrade.Name = "dgTrade";
            this.dgTrade.ReadOnly = true;
            this.dgTrade.RowTemplate.Height = 27;
            this.dgTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTrade.Size = new System.Drawing.Size(662, 327);
            this.dgTrade.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(662, 327);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "合约";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // FormTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 356);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormTrade";
            this.Text = "交易";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenInterest)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOrder)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btOrderDown;
        private System.Windows.Forms.Button btOrderUp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgOpenInterest;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.TextBox tbMount;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgOrder;
        private System.Windows.Forms.DataGridView dgTrade;
    }
}