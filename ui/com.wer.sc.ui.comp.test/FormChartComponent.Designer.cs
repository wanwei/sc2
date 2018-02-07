namespace com.wer.sc.ui.comp.test
{
    partial class FormChartComponent
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
            this.menuComponent1 = new com.wer.sc.ui.comp.ToolStripComponent();
            this.chartComponent1 = new com.wer.sc.ui.comp.ChartComponent();
            this.SuspendLayout();
            // 
            // menuComponent1
            // 
            this.menuComponent1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuComponent1.Location = new System.Drawing.Point(0, 0);
            this.menuComponent1.Name = "menuComponent1";
            this.menuComponent1.Size = new System.Drawing.Size(623, 43);
            this.menuComponent1.TabIndex = 0;
            // 
            // chartComponent1
            // 
            this.chartComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartComponent1.Location = new System.Drawing.Point(0, 43);
            this.chartComponent1.Name = "chartComponent1";
            this.chartComponent1.Size = new System.Drawing.Size(623, 434);
            this.chartComponent1.TabIndex = 1;
            // 
            // FormChartComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 477);
            this.Controls.Add(this.chartComponent1);
            this.Controls.Add(this.menuComponent1);
            this.Name = "FormChartComponent";
            this.Text = "chart组件测试";
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripComponent menuComponent1;
        private ChartComponent chartComponent1;
    }
}