namespace com.wer.sc.data.updater
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
            this.controlMultiUpdate1 = new com.wer.sc.utils.ui.update.ControlMultiUpdate();
            this.SuspendLayout();
            // 
            // controlMultiUpdate1
            // 
            this.controlMultiUpdate1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlMultiUpdate1.Location = new System.Drawing.Point(0, 0);
            this.controlMultiUpdate1.MultiUpdater = null;
            this.controlMultiUpdate1.Name = "controlMultiUpdate1";
            this.controlMultiUpdate1.Size = new System.Drawing.Size(757, 297);
            this.controlMultiUpdate1.TabIndex = 0;
            // 
            // FormDataUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 297);
            this.Controls.Add(this.controlMultiUpdate1);
            this.Name = "FormDataUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDataUpdater";
            this.ResumeLayout(false);

        }

        #endregion

        private sc.utils.ui.update.ControlMultiUpdate controlMultiUpdate1;
    }
}