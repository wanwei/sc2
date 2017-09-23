namespace com.wer.sc.ui.strategy
{
    partial class FormStrategyDescription
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbStrategyDesc = new System.Windows.Forms.Label();
            this.lbStrategyName = new System.Windows.Forms.Label();
            this.lbStrategyId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbStrategyDesc);
            this.groupBox1.Controls.Add(this.lbStrategyName);
            this.groupBox1.Controls.Add(this.lbStrategyId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 406);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前策略信息";
            // 
            // lbStrategyDesc
            // 
            this.lbStrategyDesc.AutoSize = true;
            this.lbStrategyDesc.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStrategyDesc.Location = new System.Drawing.Point(169, 139);
            this.lbStrategyDesc.MaximumSize = new System.Drawing.Size(500, 0);
            this.lbStrategyDesc.Name = "lbStrategyDesc";
            this.lbStrategyDesc.Size = new System.Drawing.Size(106, 24);
            this.lbStrategyDesc.TabIndex = 12;
            this.lbStrategyDesc.Text = "策略描述";
            // 
            // lbStrategyName
            // 
            this.lbStrategyName.AutoSize = true;
            this.lbStrategyName.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStrategyName.Location = new System.Drawing.Point(169, 92);
            this.lbStrategyName.Name = "lbStrategyName";
            this.lbStrategyName.Size = new System.Drawing.Size(106, 24);
            this.lbStrategyName.TabIndex = 11;
            this.lbStrategyName.Text = "策略名称";
            // 
            // lbStrategyId
            // 
            this.lbStrategyId.AutoSize = true;
            this.lbStrategyId.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStrategyId.Location = new System.Drawing.Point(169, 50);
            this.lbStrategyId.Name = "lbStrategyId";
            this.lbStrategyId.Size = new System.Drawing.Size(82, 24);
            this.lbStrategyId.TabIndex = 10;
            this.lbStrategyId.Text = "策略ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(28, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "策略描述";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(28, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "策略名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "策略ID";
            // 
            // FormStrategyDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 406);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "FormStrategyDescription";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "策略信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbStrategyDesc;
        private System.Windows.Forms.Label lbStrategyName;
        private System.Windows.Forms.Label lbStrategyId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}