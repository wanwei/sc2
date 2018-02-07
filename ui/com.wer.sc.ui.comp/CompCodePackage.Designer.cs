namespace com.wer.sc.ui.comp
{
    partial class CompCodePackage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.btModifyCodes = new System.Windows.Forms.Button();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.gridCodes = new System.Windows.Forms.DataGridView();
            this.columnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.tbStart);
            this.splitContainer2.Panel1.Controls.Add(this.btModifyCodes);
            this.splitContainer2.Panel1.Controls.Add(this.tbEnd);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridCodes);
            this.splitContainer2.Size = new System.Drawing.Size(403, 463);
            this.splitContainer2.SplitterDistance = 141;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(21, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 24);
            this.label8.TabIndex = 39;
            this.label8.Text = "开始时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 46;
            this.label1.Text = "合约选取";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(21, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 24);
            this.label7.TabIndex = 40;
            this.label7.Text = "结束时间";
            // 
            // tbStart
            // 
            this.tbStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbStart.Location = new System.Drawing.Point(166, 9);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(199, 30);
            this.tbStart.TabIndex = 41;
            // 
            // btModifyCodes
            // 
            this.btModifyCodes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btModifyCodes.Location = new System.Drawing.Point(253, 94);
            this.btModifyCodes.Name = "btModifyCodes";
            this.btModifyCodes.Size = new System.Drawing.Size(113, 34);
            this.btModifyCodes.TabIndex = 32;
            this.btModifyCodes.Text = "修改";
            this.btModifyCodes.UseVisualStyleBackColor = true;
            this.btModifyCodes.Click += new System.EventHandler(this.btModifyCodes_Click);
            // 
            // tbEnd
            // 
            this.tbEnd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbEnd.Location = new System.Drawing.Point(166, 52);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(199, 30);
            this.tbEnd.TabIndex = 42;
            // 
            // gridPosition
            // 
            this.gridCodes.AllowUserToAddRows = false;
            this.gridCodes.AllowUserToResizeRows = false;
            this.gridCodes.BackgroundColor = System.Drawing.Color.White;
            this.gridCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCode});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCodes.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCodes.Location = new System.Drawing.Point(0, 0);
            this.gridCodes.MultiSelect = false;
            this.gridCodes.Name = "gridPosition";
            this.gridCodes.ReadOnly = true;
            this.gridCodes.RowHeadersVisible = false;
            this.gridCodes.RowTemplate.Height = 27;
            this.gridCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCodes.Size = new System.Drawing.Size(403, 321);
            this.gridCodes.TabIndex = 6;
            // 
            // columnCode
            // 
            this.columnCode.HeaderText = "代码";
            this.columnCode.Name = "columnCode";
            this.columnCode.ReadOnly = true;
            this.columnCode.Width = 250;
            // 
            // CompCodePackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "CompCodePackage";
            this.Size = new System.Drawing.Size(403, 463);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Button btModifyCodes;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.DataGridView gridCodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCode;
    }
}
