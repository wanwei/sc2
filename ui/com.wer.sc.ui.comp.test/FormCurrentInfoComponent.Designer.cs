namespace com.wer.sc.ui.comp.test
{
    partial class FormCurrentInfoComponent
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
            this.currentInfoComponent1 = new com.wer.sc.ui.comp.CurrentInfoComponent();
            this.SuspendLayout();
            // 
            // currentInfoComponent1
            // 
            this.currentInfoComponent1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.currentInfoComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentInfoComponent1.Location = new System.Drawing.Point(0, 0);
            this.currentInfoComponent1.Name = "currentInfoComponent1";
            this.currentInfoComponent1.Size = new System.Drawing.Size(414, 558);
            this.currentInfoComponent1.TabIndex = 0;
            // 
            // FormCurrentInfoComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 558);
            this.Controls.Add(this.currentInfoComponent1);
            this.Name = "FormCurrentInfoComponent";
            this.Text = "FormCurrentInfoComponent";
            this.ResumeLayout(false);

        }

        #endregion

        private CurrentInfoComponent currentInfoComponent1;
    }
}