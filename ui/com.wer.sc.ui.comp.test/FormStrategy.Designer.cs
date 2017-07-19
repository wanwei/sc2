namespace com.wer.sc.ui.comp.test
{
    partial class FormStrategy
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
            this.compStrategyTree1 = new com.wer.sc.ui.comp.CompStrategyTree();
            this.SuspendLayout();
            // 
            // compStrategyTree1
            // 
            this.compStrategyTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compStrategyTree1.Location = new System.Drawing.Point(0, 0);
            this.compStrategyTree1.Name = "compStrategyTree1";
            this.compStrategyTree1.Size = new System.Drawing.Size(598, 494);
            this.compStrategyTree1.TabIndex = 0;
            // 
            // FormStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 494);
            this.Controls.Add(this.compStrategyTree1);
            this.Name = "FormStrategy";
            this.Text = "FormStrategy";
            this.ResumeLayout(false);

        }

        #endregion

        private CompStrategyTree compStrategyTree1;
    }
}