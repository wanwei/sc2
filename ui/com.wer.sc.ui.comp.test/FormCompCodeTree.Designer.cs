namespace com.wer.sc.ui.comp.test
{
    partial class FormCompCodeTree
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
            this.cbMultiSelect = new System.Windows.Forms.CheckBox();
            this.cbShowCatelog = new System.Windows.Forms.CheckBox();
            this.compCodeTree1 = new com.wer.sc.ui.comp.CompCodeTree();
            this.btCatelogs = new System.Windows.Forms.Button();
            this.btCode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btCode);
            this.splitContainer1.Panel1.Controls.Add(this.btCatelogs);
            this.splitContainer1.Panel1.Controls.Add(this.cbShowCatelog);
            this.splitContainer1.Panel1.Controls.Add(this.cbMultiSelect);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.compCodeTree1);
            this.splitContainer1.Size = new System.Drawing.Size(694, 434);
            this.splitContainer1.SplitterDistance = 96;
            this.splitContainer1.TabIndex = 0;
            // 
            // cbMultiSelect
            // 
            this.cbMultiSelect.AutoSize = true;
            this.cbMultiSelect.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMultiSelect.Location = new System.Drawing.Point(47, 30);
            this.cbMultiSelect.Name = "cbMultiSelect";
            this.cbMultiSelect.Size = new System.Drawing.Size(80, 28);
            this.cbMultiSelect.TabIndex = 5;
            this.cbMultiSelect.Text = "多选";
            this.cbMultiSelect.UseVisualStyleBackColor = true;
            this.cbMultiSelect.CheckedChanged += new System.EventHandler(this.cbMultiSelect_CheckedChanged);
            // 
            // cbShowCatelog
            // 
            this.cbShowCatelog.AutoSize = true;
            this.cbShowCatelog.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbShowCatelog.Location = new System.Drawing.Point(168, 30);
            this.cbShowCatelog.Name = "cbShowCatelog";
            this.cbShowCatelog.Size = new System.Drawing.Size(152, 28);
            this.cbShowCatelog.TabIndex = 6;
            this.cbShowCatelog.Text = "只选择类别";
            this.cbShowCatelog.UseVisualStyleBackColor = true;
            this.cbShowCatelog.CheckedChanged += new System.EventHandler(this.cbShowCatelog_CheckedChanged);
            // 
            // compCodeTree1
            // 
            this.compCodeTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compCodeTree1.Location = new System.Drawing.Point(0, 0);
            this.compCodeTree1.MultiSelect = false;
            this.compCodeTree1.Name = "compCodeTree1";
            this.compCodeTree1.SelectCatelog = false;
            this.compCodeTree1.Size = new System.Drawing.Size(694, 334);
            this.compCodeTree1.TabIndex = 0;
            // 
            // btCatelogs
            // 
            this.btCatelogs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCatelogs.Location = new System.Drawing.Point(343, 23);
            this.btCatelogs.Name = "btCatelogs";
            this.btCatelogs.Size = new System.Drawing.Size(145, 44);
            this.btCatelogs.TabIndex = 9;
            this.btCatelogs.Text = "选中类别";
            this.btCatelogs.UseVisualStyleBackColor = true;
            this.btCatelogs.Click += new System.EventHandler(this.btCatelogs_Click);
            // 
            // btCode
            // 
            this.btCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCode.Location = new System.Drawing.Point(494, 23);
            this.btCode.Name = "btCode";
            this.btCode.Size = new System.Drawing.Size(145, 44);
            this.btCode.TabIndex = 10;
            this.btCode.Text = "选中Code";
            this.btCode.UseVisualStyleBackColor = true;
            this.btCode.Click += new System.EventHandler(this.btCode_Click);
            // 
            // FormCompCodeTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 434);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormCompCodeTree";
            this.Text = "FormCompCodeTree";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CompCodeTree compCodeTree1;
        private System.Windows.Forms.CheckBox cbMultiSelect;
        private System.Windows.Forms.CheckBox cbShowCatelog;
        private System.Windows.Forms.Button btCatelogs;
        private System.Windows.Forms.Button btCode;
    }
}