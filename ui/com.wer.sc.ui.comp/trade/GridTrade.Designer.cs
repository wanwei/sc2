namespace com.wer.sc.ui.comp.trade
{
    partial class GridTrade
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
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnEarn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAsset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOrderID,
            this.columnCode,
            this.columnDirection,
            this.columnMount,
            this.columnAvailable,
            this.columnPrice,
            this.columnEarn,
            this.columnAsset});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(522, 397);
            this.dataGridView1.TabIndex = 2;
            // 
            // columnOrderID
            // 
            this.columnOrderID.HeaderText = "委托ID";
            this.columnOrderID.Name = "columnOrderID";
            this.columnOrderID.ReadOnly = true;
            this.columnOrderID.Visible = false;
            // 
            // columnCode
            // 
            this.columnCode.HeaderText = "代码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            // 
            // columnDirection
            // 
            this.columnDirection.HeaderText = "方向";
            this.columnDirection.Name = "columnDirection";
            this.columnDirection.ReadOnly = true;
            // 
            // columnMount
            // 
            this.columnMount.HeaderText = "数量";
            this.columnMount.Name = "columnMount";
            this.columnMount.ReadOnly = true;
            // 
            // columnAvailable
            // 
            this.columnAvailable.HeaderText = "可用";
            this.columnAvailable.Name = "columnAvailable";
            this.columnAvailable.ReadOnly = true;
            // 
            // columnPrice
            // 
            this.columnPrice.HeaderText = "均价";
            this.columnPrice.Name = "columnPrice";
            this.columnPrice.ReadOnly = true;
            // 
            // columnEarn
            // 
            this.columnEarn.HeaderText = "盈利";
            this.columnEarn.Name = "columnEarn";
            this.columnEarn.ReadOnly = true;
            // 
            // columnAsset
            // 
            this.columnAsset.HeaderText = "价值";
            this.columnAsset.Name = "columnAsset";
            this.columnAsset.ReadOnly = true;
            // 
            // GridTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "GridTrade";
            this.Size = new System.Drawing.Size(522, 397);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnEarn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAsset;
    }
}
