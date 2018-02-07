namespace com.wer.sc.ui
{
    partial class FormChart2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChart2));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainComponent1 = new com.wer.sc.ui.comp.MainComponent();
            this.menuComponent1 = new com.wer.sc.ui.comp.ToolStripComponent();
            this.statusStrip1.SuspendLayout();
            this.menuComponent1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 449);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(623, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbTime
            // 
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(59, 20);
            this.lbTime.Text = "lbTime";
            // 
            // mainComponent1
            // 
            this.mainComponent1.Location = new System.Drawing.Point(12, 141);
            this.mainComponent1.Name = "mainComponent1";
            this.mainComponent1.Size = new System.Drawing.Size(623, 421);
            this.mainComponent1.TabIndex = 2;
            // 
            // menuComponent1
            // 
            this.menuComponent1.Controls.Add(this.mainComponent1);
            this.menuComponent1.Location = new System.Drawing.Point(140, 219);
            this.menuComponent1.Name = "menuComponent1";
            this.menuComponent1.Size = new System.Drawing.Size(623, 43);
            this.menuComponent1.TabIndex = 6;
            // 
            // FormChart2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 474);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormChart2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SC交易策略";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuComponent1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbTime;
        private comp.MainComponent mainComponent1;
        private comp.ToolStripComponent menuComponent1;
    }
}