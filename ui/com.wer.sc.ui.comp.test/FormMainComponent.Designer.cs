namespace com.wer.sc.ui.comp.test
{
    partial class FormMainComponent
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
            this.menuComponent1 = new com.wer.sc.ui.comp.MenuComponent();
            this.mainComponent1 = new com.wer.sc.ui.comp.MainComponent();
            this.SuspendLayout();
            // 
            // menuComponent1
            // 
            this.menuComponent1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuComponent1.Location = new System.Drawing.Point(0, 0);
            this.menuComponent1.Name = "menuComponent1";
            this.menuComponent1.Size = new System.Drawing.Size(956, 43);
            this.menuComponent1.TabIndex = 0;
            // 
            // mainComponent1
            // 
            this.mainComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainComponent1.Location = new System.Drawing.Point(0, 43);
            this.mainComponent1.Name = "mainComponent1";
            this.mainComponent1.Size = new System.Drawing.Size(956, 516);
            this.mainComponent1.TabIndex = 1;
            // 
            // FormMainComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 559);
            this.Controls.Add(this.mainComponent1);
            this.Controls.Add(this.menuComponent1);
            this.Name = "FormMainComponent";
            this.Text = "FormMainComponent";
            this.ResumeLayout(false);

        }

        #endregion

        private MenuComponent menuComponent1;
        private MainComponent mainComponent1;
    }
}