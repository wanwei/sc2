namespace com.wer.sc.ui.comp.trade
{
    partial class FormTradeBak
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
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.gridTrade = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btCurrentPrice = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gridPosition = new System.Windows.Forms.DataGridView();
            this.columnPositionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionCurrentMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionEarn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPositionEarnPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAsset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridOrder = new System.Windows.Forms.DataGridView();
            this.columnOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCanCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btCurrentPrice);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.tbPrice);
            this.splitContainer1.Panel1.Controls.Add(this.tbMount);
            this.splitContainer1.Panel1.Controls.Add(this.tbCode);
            this.splitContainer1.Panel1.Controls.Add(this.btClose);
            this.splitContainer1.Panel1.Controls.Add(this.btOrderDown);
            this.splitContainer1.Panel1.Controls.Add(this.btOrderUp);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1202, 409);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(25, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "价格";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(25, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "代码";
            // 
            // tbPrice
            // 
            this.tbPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPrice.Location = new System.Drawing.Point(93, 101);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(144, 30);
            this.tbPrice.TabIndex = 8;
            // 
            // tbMount
            // 
            this.tbMount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMount.Location = new System.Drawing.Point(93, 61);
            this.tbMount.Name = "tbMount";
            this.tbMount.Size = new System.Drawing.Size(158, 30);
            this.tbMount.TabIndex = 7;
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.Location = new System.Drawing.Point(93, 23);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(199, 30);
            this.tbCode.TabIndex = 6;
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClose.ForeColor = System.Drawing.Color.Blue;
            this.btClose.Location = new System.Drawing.Point(250, 152);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(102, 58);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "平仓";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btClose_MouseUp);
            // 
            // btOrderDown
            // 
            this.btOrderDown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOrderDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btOrderDown.Location = new System.Drawing.Point(135, 152);
            this.btOrderDown.Name = "btOrderDown";
            this.btOrderDown.Size = new System.Drawing.Size(102, 58);
            this.btOrderDown.TabIndex = 4;
            this.btOrderDown.Text = "卖空";
            this.btOrderDown.UseVisualStyleBackColor = true;
            this.btOrderDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btOrderDown_MouseUp);
            // 
            // btOrderUp
            // 
            this.btOrderUp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOrderUp.ForeColor = System.Drawing.Color.Red;
            this.btOrderUp.Location = new System.Drawing.Point(19, 152);
            this.btOrderUp.Name = "btOrderUp";
            this.btOrderUp.Size = new System.Drawing.Size(102, 58);
            this.btOrderUp.TabIndex = 3;
            this.btOrderUp.Text = "买多";
            this.btOrderUp.UseVisualStyleBackColor = true;
            this.btOrderUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btOrderUp_MouseUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 409);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(819, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "持仓";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.gridTrade);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(819, 380);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "成交";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // gridTrade
            // 
            this.gridTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTrade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.gridTrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrade.Location = new System.Drawing.Point(3, 3);
            this.gridTrade.MultiSelect = false;
            this.gridTrade.Name = "gridTrade";
            this.gridTrade.ReadOnly = true;
            this.gridTrade.RowHeadersVisible = false;
            this.gridTrade.RowTemplate.Height = 27;
            this.gridTrade.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrade.Size = new System.Drawing.Size(813, 374);
            this.gridTrade.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "代码";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "方向";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "数量";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "可用";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "均价";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "盈利";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "价值";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // btCurrentPrice
            // 
            this.btCurrentPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCurrentPrice.ForeColor = System.Drawing.Color.Black;
            this.btCurrentPrice.Location = new System.Drawing.Point(250, 101);
            this.btCurrentPrice.Name = "btCurrentPrice";
            this.btCurrentPrice.Size = new System.Drawing.Size(83, 28);
            this.btCurrentPrice.TabIndex = 12;
            this.btCurrentPrice.Text = "当前价";
            this.btCurrentPrice.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridPosition);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridOrder);
            this.splitContainer2.Size = new System.Drawing.Size(813, 374);
            this.splitContainer2.SplitterDistance = 187;
            this.splitContainer2.TabIndex = 5;
            // 
            // gridPosition
            // 
            this.gridPosition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPosition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPositionCode,
            this.columnPositionDirection,
            this.columnPositionMount,
            this.columnPositionCurrentMount,
            this.columnPositionPrice,
            this.columnPositionEarn,
            this.columnPositionEarnPercent,
            this.columnAsset});
            this.gridPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPosition.Location = new System.Drawing.Point(0, 0);
            this.gridPosition.MultiSelect = false;
            this.gridPosition.Name = "gridPosition";
            this.gridPosition.ReadOnly = true;
            this.gridPosition.RowHeadersVisible = false;
            this.gridPosition.RowTemplate.Height = 27;
            this.gridPosition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPosition.Size = new System.Drawing.Size(813, 187);
            this.gridPosition.TabIndex = 5;
            // 
            // columnPositionCode
            // 
            this.columnPositionCode.HeaderText = "代码";
            this.columnPositionCode.Name = "columnPositionCode";
            this.columnPositionCode.ReadOnly = true;
            // 
            // columnPositionDirection
            // 
            this.columnPositionDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnPositionDirection.HeaderText = "多空";
            this.columnPositionDirection.Name = "columnPositionDirection";
            this.columnPositionDirection.ReadOnly = true;
            this.columnPositionDirection.Width = 66;
            // 
            // columnPositionMount
            // 
            this.columnPositionMount.HeaderText = "总仓";
            this.columnPositionMount.Name = "columnPositionMount";
            this.columnPositionMount.ReadOnly = true;
            // 
            // columnPositionCurrentMount
            // 
            this.columnPositionCurrentMount.HeaderText = "可用";
            this.columnPositionCurrentMount.Name = "columnPositionCurrentMount";
            this.columnPositionCurrentMount.ReadOnly = true;
            // 
            // columnPositionPrice
            // 
            this.columnPositionPrice.HeaderText = "均价";
            this.columnPositionPrice.Name = "columnPositionPrice";
            this.columnPositionPrice.ReadOnly = true;
            // 
            // columnPositionEarn
            // 
            this.columnPositionEarn.HeaderText = "盈利";
            this.columnPositionEarn.Name = "columnPositionEarn";
            this.columnPositionEarn.ReadOnly = true;
            // 
            // columnPositionEarnPercent
            // 
            this.columnPositionEarnPercent.HeaderText = "盈利比例";
            this.columnPositionEarnPercent.Name = "columnPositionEarnPercent";
            this.columnPositionEarnPercent.ReadOnly = true;
            // 
            // columnAsset
            // 
            this.columnAsset.HeaderText = "价值";
            this.columnAsset.Name = "columnAsset";
            this.columnAsset.ReadOnly = true;
            // 
            // gridOrder
            // 
            this.gridOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOrderID,
            this.columnOrderTime,
            this.columnOrderCode,
            this.columnOrderState,
            this.columnOrderDirection,
            this.columnOpenClose,
            this.columnOrderPrice,
            this.columnOrderMount,
            this.columnCanCancelCount,
            this.columnTradeCount,
            this.columnCancelCount});
            this.gridOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrder.Location = new System.Drawing.Point(0, 0);
            this.gridOrder.MultiSelect = false;
            this.gridOrder.Name = "gridOrder";
            this.gridOrder.ReadOnly = true;
            this.gridOrder.RowHeadersVisible = false;
            this.gridOrder.RowTemplate.Height = 27;
            this.gridOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrder.Size = new System.Drawing.Size(813, 183);
            this.gridOrder.TabIndex = 6;
            // 
            // columnOrderID
            // 
            this.columnOrderID.HeaderText = "委托ID";
            this.columnOrderID.Name = "columnOrderID";
            this.columnOrderID.ReadOnly = true;
            this.columnOrderID.Visible = false;
            // 
            // columnOrderTime
            // 
            this.columnOrderTime.HeaderText = "时间";
            this.columnOrderTime.Name = "columnOrderTime";
            this.columnOrderTime.ReadOnly = true;
            this.columnOrderTime.Width = 80;
            // 
            // columnOrderCode
            // 
            this.columnOrderCode.HeaderText = "代码";
            this.columnOrderCode.Name = "columnOrderCode";
            this.columnOrderCode.ReadOnly = true;
            this.columnOrderCode.Width = 80;
            // 
            // columnOrderState
            // 
            this.columnOrderState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnOrderState.HeaderText = "状态";
            this.columnOrderState.Name = "columnOrderState";
            this.columnOrderState.ReadOnly = true;
            this.columnOrderState.Width = 66;
            // 
            // columnOrderDirection
            // 
            this.columnOrderDirection.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnOrderDirection.HeaderText = "买卖";
            this.columnOrderDirection.Name = "columnOrderDirection";
            this.columnOrderDirection.ReadOnly = true;
            this.columnOrderDirection.Width = 66;
            // 
            // columnOpenClose
            // 
            this.columnOpenClose.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnOpenClose.HeaderText = "开平";
            this.columnOpenClose.Name = "columnOpenClose";
            this.columnOpenClose.ReadOnly = true;
            this.columnOpenClose.Width = 66;
            // 
            // columnOrderPrice
            // 
            this.columnOrderPrice.HeaderText = "委托价";
            this.columnOrderPrice.Name = "columnOrderPrice";
            this.columnOrderPrice.ReadOnly = true;
            // 
            // columnOrderMount
            // 
            this.columnOrderMount.HeaderText = "委托量";
            this.columnOrderMount.Name = "columnOrderMount";
            this.columnOrderMount.ReadOnly = true;
            // 
            // columnCanCancelCount
            // 
            this.columnCanCancelCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnCanCancelCount.HeaderText = "可撤";
            this.columnCanCancelCount.Name = "columnCanCancelCount";
            this.columnCanCancelCount.ReadOnly = true;
            this.columnCanCancelCount.Width = 66;
            // 
            // columnTradeCount
            // 
            this.columnTradeCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnTradeCount.HeaderText = "已成交";
            this.columnTradeCount.Name = "columnTradeCount";
            this.columnTradeCount.ReadOnly = true;
            this.columnTradeCount.Width = 81;
            // 
            // columnCancelCount
            // 
            this.columnCancelCount.HeaderText = "已撤单";
            this.columnCancelCount.Name = "columnCancelCount";
            this.columnCancelCount.ReadOnly = true;
            // 
            // FormTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 409);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "FormTrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交易";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btOrderDown;
        private System.Windows.Forms.Button btOrderUp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.TextBox tbMount;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView gridTrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.Button btCurrentPrice;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gridPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionCurrentMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionEarn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionEarnPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAsset;
        private System.Windows.Forms.DataGridView gridOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderState;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOpenClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCanCancelCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCancelCount;
    }
}