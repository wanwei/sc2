﻿namespace com.wer.sc.ui.comp
{
    partial class CompStrategyTree
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
            this.treeStrategy = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeStrategy
            // 
            this.treeStrategy.BackColor = System.Drawing.Color.Black;
            this.treeStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeStrategy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeStrategy.ForeColor = System.Drawing.Color.Yellow;
            this.treeStrategy.Location = new System.Drawing.Point(0, 0);
            this.treeStrategy.Name = "treeStrategy";
            this.treeStrategy.Size = new System.Drawing.Size(489, 436);
            this.treeStrategy.TabIndex = 0;
            // 
            // CompStrategyTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeStrategy);
            this.Name = "CompStrategyTree";
            this.Size = new System.Drawing.Size(489, 436);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeStrategy;
    }
}
