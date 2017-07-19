namespace com.wer.sc.ui.comp.test
{
    partial class FormMain
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
            this.compMain1 = new com.wer.sc.ui.comp.CompMain();
            this.SuspendLayout();
            // 
            // compMain1
            // 
            this.compMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compMain1.Location = new System.Drawing.Point(0, 0);
            this.compMain1.Name = "compMain1";
            this.compMain1.Size = new System.Drawing.Size(699, 516);
            this.compMain1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 516);
            this.Controls.Add(this.compMain1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private CompMain compMain1;
    }
}