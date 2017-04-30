namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
{
    partial class FormDataUpdater
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
            this.controlDataProceed1 = new com.wer.sc.utils.ui.update.ControlDataUpdate();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSrcPath = new System.Windows.Forms.TextBox();
            this.tbAdjustedPath = new System.Windows.Forms.TextBox();
            this.tbDataCenterUri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbFillUp = new System.Windows.Forms.RadioButton();
            this.rb_New = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlDataProceed1
            // 
            this.controlDataProceed1.DataProceed = null;
            this.controlDataProceed1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlDataProceed1.Location = new System.Drawing.Point(0, 167);
            this.controlDataProceed1.Margin = new System.Windows.Forms.Padding(5);
            this.controlDataProceed1.Name = "controlDataProceed1";
            this.controlDataProceed1.Size = new System.Drawing.Size(757, 65);
            this.controlDataProceed1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSrcPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbAdjustedPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbDataCenterUri, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(757, 167);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "更新方式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "数据中心";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(4, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "调整后目录";
            // 
            // tbSrcPath
            // 
            this.tbSrcPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSrcPath.Location = new System.Drawing.Point(178, 4);
            this.tbSrcPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbSrcPath.Name = "tbSrcPath";
            this.tbSrcPath.Size = new System.Drawing.Size(501, 30);
            this.tbSrcPath.TabIndex = 0;
            // 
            // tbAdjustedPath
            // 
            this.tbAdjustedPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbAdjustedPath.Location = new System.Drawing.Point(178, 45);
            this.tbAdjustedPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbAdjustedPath.Name = "tbAdjustedPath";
            this.tbAdjustedPath.Size = new System.Drawing.Size(501, 30);
            this.tbAdjustedPath.TabIndex = 1;
            // 
            // tbDataCenterUri
            // 
            this.tbDataCenterUri.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDataCenterUri.Location = new System.Drawing.Point(178, 86);
            this.tbDataCenterUri.Margin = new System.Windows.Forms.Padding(4);
            this.tbDataCenterUri.Name = "tbDataCenterUri";
            this.tbDataCenterUri.Size = new System.Drawing.Size(501, 30);
            this.tbDataCenterUri.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "源数据目录";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbFillUp);
            this.panel1.Controls.Add(this.rb_New);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(178, 127);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 36);
            this.panel1.TabIndex = 6;
            // 
            // rbFillUp
            // 
            this.rbFillUp.AutoSize = true;
            this.rbFillUp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbFillUp.Location = new System.Drawing.Point(227, 5);
            this.rbFillUp.Margin = new System.Windows.Forms.Padding(4);
            this.rbFillUp.Name = "rbFillUp";
            this.rbFillUp.Size = new System.Drawing.Size(190, 24);
            this.rbFillUp.TabIndex = 1;
            this.rbFillUp.TabStop = true;
            this.rbFillUp.Text = "补充所有遗漏数据";
            this.rbFillUp.UseVisualStyleBackColor = true;
            // 
            // rb_New
            // 
            this.rb_New.AutoSize = true;
            this.rb_New.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rb_New.Location = new System.Drawing.Point(43, 5);
            this.rb_New.Margin = new System.Windows.Forms.Padding(4);
            this.rb_New.Name = "rb_New";
            this.rb_New.Size = new System.Drawing.Size(170, 24);
            this.rb_New.TabIndex = 0;
            this.rb_New.TabStop = true;
            this.rb_New.Text = "从最新数据更新";
            this.rb_New.UseVisualStyleBackColor = true;
            // 
            // FormDataUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 232);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.controlDataProceed1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormDataUpdater";
            this.Text = "中国期货市场数据更新器";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private sc.utils.ui.update.ControlDataUpdate controlDataProceed1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSrcPath;
        private System.Windows.Forms.TextBox tbAdjustedPath;
        private System.Windows.Forms.TextBox tbDataCenterUri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbFillUp;
        private System.Windows.Forms.RadioButton rb_New;
        private System.Windows.Forms.Label label4;
    }
}