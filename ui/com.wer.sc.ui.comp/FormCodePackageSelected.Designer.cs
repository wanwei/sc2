namespace com.wer.sc.ui.comp
{
    partial class FormCodePackageSelected
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
            this.cbChooseType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.codeTree1 = new com.wer.sc.ui.comp.CompCodeTree();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.codeTree1);
            this.splitContainer1.Panel1.Controls.Add(this.cbChooseType);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btOK);
            this.splitContainer1.Size = new System.Drawing.Size(525, 516);
            this.splitContainer1.SplitterDistance = 466;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 49;
            // 
            // cbChooseType
            // 
            this.cbChooseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChooseType.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbChooseType.FormattingEnabled = true;
            this.cbChooseType.Items.AddRange(new object[] {
            "直接选取",
            "按类别选取",
            "选择主合约"});
            this.cbChooseType.Location = new System.Drawing.Point(170, 21);
            this.cbChooseType.Name = "cbChooseType";
            this.cbChooseType.Size = new System.Drawing.Size(189, 31);
            this.cbChooseType.TabIndex = 50;
            this.cbChooseType.SelectedIndexChanged += new System.EventHandler(this.cbChooseType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 49;
            this.label1.Text = "选择合约";
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.Location = new System.Drawing.Point(287, 7);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(153, 34);
            this.btCancel.TabIndex = 35;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.Location = new System.Drawing.Point(84, 7);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(154, 34);
            this.btOK.TabIndex = 34;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // codeTree1
            // 
            this.codeTree1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.codeTree1.Location = new System.Drawing.Point(0, 66);
            this.codeTree1.MultiSelect = false;
            this.codeTree1.Name = "codeTree1";
            this.codeTree1.SelectCatelog = false;
            this.codeTree1.Size = new System.Drawing.Size(525, 400);
            this.codeTree1.TabIndex = 51;
            // 
            // FormCodePackageSelected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 516);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormCodePackageSelected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合约选取";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbChooseType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private CompCodeTree codeTree1;
    }
}