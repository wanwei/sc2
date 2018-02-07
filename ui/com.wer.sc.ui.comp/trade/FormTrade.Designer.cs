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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.numberMount = new com.wer.sc.ui.comp.NumberUpDown();
            this.numberPrice = new com.wer.sc.ui.comp.NumberUpDown();
            this.btCurrentPrice = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btOrderDown = new System.Windows.Forms.Button();
            this.btOrderUp = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.gridWaitingOrder = new System.Windows.Forms.DataGridView();
            this.columnOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderCanCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderTradeCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnOrderCancelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridTrade = new System.Windows.Forms.DataGridView();
            this.columnTradeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeMount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnTradeOpenClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWaitingOrder)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.numberMount);
            this.splitContainer1.Panel1.Controls.Add(this.numberPrice);
            this.splitContainer1.Panel1.Controls.Add(this.btCurrentPrice);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
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
            // numberMount
            // 
            this.numberMount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.numberMount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberMount.IsInputState = false;
            this.numberMount.Location = new System.Drawing.Point(92, 85);
            this.numberMount.Margin = new System.Windows.Forms.Padding(4);
            this.numberMount.MaxValue = 1.7976931348623157E+308D;
            this.numberMount.MinPeriod = 0D;
            this.numberMount.MinValue = -1.7976931348623157E+308D;
            this.numberMount.Name = "numberMount";
            this.numberMount.NormalText = null;
            this.numberMount.Size = new System.Drawing.Size(144, 27);
            this.numberMount.TabIndex = 14;
            this.numberMount.Value = 0D;
            // 
            // numberPrice
            // 
            this.numberPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.numberPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberPrice.IsInputState = false;
            this.numberPrice.Location = new System.Drawing.Point(92, 126);
            this.numberPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numberPrice.MaxValue = 1.7976931348623157E+308D;
            this.numberPrice.MinPeriod = 0D;
            this.numberPrice.MinValue = -1.7976931348623157E+308D;
            this.numberPrice.Name = "numberPrice";
            this.numberPrice.NormalText = null;
            this.numberPrice.Size = new System.Drawing.Size(144, 27);
            this.numberPrice.TabIndex = 13;
            this.numberPrice.Value = 0D;
            // 
            // btCurrentPrice
            // 
            this.btCurrentPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCurrentPrice.ForeColor = System.Drawing.Color.Black;
            this.btCurrentPrice.Location = new System.Drawing.Point(260, 120);
            this.btCurrentPrice.Name = "btCurrentPrice";
            this.btCurrentPrice.Size = new System.Drawing.Size(96, 39);
            this.btCurrentPrice.TabIndex = 12;
            this.btCurrentPrice.Text = "当前价";
            this.btCurrentPrice.UseVisualStyleBackColor = true;
            this.btCurrentPrice.Click += new System.EventHandler(this.btCurrentPrice_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(24, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "价格";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "代码";
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCode.Location = new System.Drawing.Point(92, 48);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(199, 30);
            this.tbCode.TabIndex = 6;
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClose.ForeColor = System.Drawing.Color.Blue;
            this.btClose.Location = new System.Drawing.Point(243, 210);
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
            this.btOrderDown.Location = new System.Drawing.Point(128, 210);
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
            this.btOrderUp.Location = new System.Drawing.Point(12, 210);
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
            this.tabControl1.Controls.Add(this.tabPage3);
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
            this.splitContainer2.Panel2.Controls.Add(this.gridWaitingOrder);
            this.splitContainer2.Size = new System.Drawing.Size(813, 374);
            this.splitContainer2.SplitterDistance = 187;
            this.splitContainer2.TabIndex = 5;
            // 
            // gridPosition
            // 
            this.gridPosition.AllowUserToAddRows = false;
            this.gridPosition.AllowUserToResizeRows = false;
            this.gridPosition.BackgroundColor = System.Drawing.Color.White;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPosition.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.gridPosition.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridPosition_MouseClick);
            this.gridPosition.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridPosition_MouseDoubleClick);
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
            this.columnPositionMount.Width = 80;
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
            // gridWaitingOrder
            // 
            this.gridWaitingOrder.AllowUserToAddRows = false;
            this.gridWaitingOrder.AllowUserToResizeRows = false;
            this.gridWaitingOrder.BackgroundColor = System.Drawing.Color.White;
            this.gridWaitingOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWaitingOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnOrderID,
            this.columnOrderTime,
            this.columnOrderCode,
            this.columnOrderState,
            this.columnOrderDirection,
            this.columnOpenClose,
            this.columnOrderPrice,
            this.columnOrderMount,
            this.columnOrderCanCancelCount,
            this.columnOrderTradeCount,
            this.columnOrderCancelCount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridWaitingOrder.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridWaitingOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridWaitingOrder.Location = new System.Drawing.Point(0, 0);
            this.gridWaitingOrder.MultiSelect = false;
            this.gridWaitingOrder.Name = "gridWaitingOrder";
            this.gridWaitingOrder.ReadOnly = true;
            this.gridWaitingOrder.RowHeadersVisible = false;
            this.gridWaitingOrder.RowTemplate.Height = 27;
            this.gridWaitingOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridWaitingOrder.Size = new System.Drawing.Size(813, 183);
            this.gridWaitingOrder.TabIndex = 6;
            this.gridWaitingOrder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gridOrder_MouseDoubleClick);
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
            // columnOrderCanCancelCount
            // 
            this.columnOrderCanCancelCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnOrderCanCancelCount.HeaderText = "可撤";
            this.columnOrderCanCancelCount.Name = "columnOrderCanCancelCount";
            this.columnOrderCanCancelCount.ReadOnly = true;
            this.columnOrderCanCancelCount.Width = 66;
            // 
            // columnOrderTradeCount
            // 
            this.columnOrderTradeCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.columnOrderTradeCount.HeaderText = "已成交";
            this.columnOrderTradeCount.Name = "columnOrderTradeCount";
            this.columnOrderTradeCount.ReadOnly = true;
            this.columnOrderTradeCount.Width = 81;
            // 
            // columnOrderCancelCount
            // 
            this.columnOrderCancelCount.HeaderText = "已撤单";
            this.columnOrderCancelCount.Name = "columnOrderCancelCount";
            this.columnOrderCancelCount.ReadOnly = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridTrade);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(819, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "成交";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.gridTrade.Size = new System.Drawing.Size(813, 374);
            this.gridTrade.TabIndex = 3;
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
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWaitingOrder)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btOrderDown;
        private System.Windows.Forms.Button btOrderUp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCurrentPrice;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gridPosition;
        private System.Windows.Forms.DataGridView gridWaitingOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionCurrentMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionEarn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnPositionEarnPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAsset;
        private NumberUpDown numberPrice;
        private NumberUpDown numberMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderState;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOpenClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderCanCancelCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderTradeCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnOrderCancelCount;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gridTrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeMount;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTradeOpenClose;
    }
}