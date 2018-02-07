namespace com.wer.sc.ui.comp.trade
{
    partial class GridOrder
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.columnOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCanCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOrderID,
            this.columnTime,
            this.columnCode,
            this.columnState,
            this.columnDirection,
            this.columnOpenClose,
            this.columnOrderPrice,
            this.columnOrderMount,
            this.columnCanCancelCount,
            this.columnTradeCount,
            this.columnCancelCount});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(511, 440);
            this.dataGridView1.TabIndex = 1;
            // 
            // columnOrderID
            // 
            this.columnOrderID.HeaderText = "委托ID";
            this.columnOrderID.Name = "columnOrderID";
            this.columnOrderID.ReadOnly = true;
            this.columnOrderID.Visible = false;
            // 
            // columnTime
            // 
            this.columnTime.HeaderText = "时间";
            this.columnTime.Name = "columnTime";
            this.columnTime.ReadOnly = true;
            // 
            // columnCode
            // 
            this.columnCode.HeaderText = "代码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            // 
            // columnState
            // 
            this.columnState.HeaderText = "状态";
            this.columnState.Name = "columnState";
            this.columnState.ReadOnly = true;
            // 
            // columnDirection
            // 
            this.columnDirection.HeaderText = "买卖";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            // 
            // columnOpenClose
            // 
            this.columnOpenClose.HeaderText = "开平";
            this.columnOpenClose.Name = "columnOpenClose";
            this.columnOpenClose.ReadOnly = true;
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
            this.columnCanCancelCount.HeaderText = "可撤";
            this.columnCanCancelCount.Name = "columnCanCancelCount";
            this.columnCanCancelCount.ReadOnly = true;
            // 
            // columnTradeCount
            // 
            this.columnTradeCount.HeaderText = "已成交";
            this.columnTradeCount.Name = "columnTradeCount";
            this.columnTradeCount.ReadOnly = true;
            // 
            // columnCancelCount
            // 
            this.columnCancelCount.HeaderText = "已撤单";
            this.columnCancelCount.Name = "columnCancelCount";
            this.columnCancelCount.ReadOnly = true;
            // 
            // GridOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "GridOrder";
            this.Size = new System.Drawing.Size(511, 440);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOpenClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCanCancelCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCancelCount;
    }
}
