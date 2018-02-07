namespace com.wer.sc.ui.comp.test
{
    partial class FormNumberUpDown
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
            this.button1 = new System.Windows.Forms.Button();
            this.numberMount = new com.wer.sc.ui.comp.NumberUpDown();
            this.numberPrice = new com.wer.sc.ui.comp.NumberUpDown();
            this.btShowValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(276, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "现价";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numberMount
            // 
            this.numberMount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.numberMount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberMount.IsInputState = false;
            this.numberMount.Location = new System.Drawing.Point(59, 173);
            this.numberMount.Margin = new System.Windows.Forms.Padding(4);
            this.numberMount.MaxValue = 1.7976931348623157E+308D;
            this.numberMount.MinPeriod = 1D;
            this.numberMount.MinValue = -1.7976931348623157E+308D;
            this.numberMount.Name = "numberMount";
            this.numberMount.NormalText = null;
            this.numberMount.Size = new System.Drawing.Size(192, 30);
            this.numberMount.TabIndex = 2;
            this.numberMount.Value = 0D;
            // 
            // numberPrice
            // 
            this.numberPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.numberPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numberPrice.IsInputState = false;
            this.numberPrice.Location = new System.Drawing.Point(59, 227);
            this.numberPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numberPrice.MaxValue = 1.7976931348623157E+308D;
            this.numberPrice.MinPeriod = 1D;
            this.numberPrice.MinValue = -1.7976931348623157E+308D;
            this.numberPrice.Name = "numberPrice";
            this.numberPrice.NormalText = null;
            this.numberPrice.Size = new System.Drawing.Size(192, 30);
            this.numberPrice.TabIndex = 0;
            this.numberPrice.Value = 0D;
            // 
            // btShowValue
            // 
            this.btShowValue.Location = new System.Drawing.Point(59, 337);
            this.btShowValue.Name = "btShowValue";
            this.btShowValue.Size = new System.Drawing.Size(116, 39);
            this.btShowValue.TabIndex = 3;
            this.btShowValue.Text = "现价";
            this.btShowValue.UseVisualStyleBackColor = true;
            this.btShowValue.Click += new System.EventHandler(this.btShowValue_Click);
            // 
            // FormNumberUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 399);
            this.Controls.Add(this.btShowValue);
            this.Controls.Add(this.numberMount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numberPrice);
            this.Name = "FormNumberUpDown";
            this.Text = "FormNumberUpDown";
            this.ResumeLayout(false);

        }

        #endregion

        private NumberUpDown numberPrice;
        private System.Windows.Forms.Button button1;
        private NumberUpDown numberMount;
        private System.Windows.Forms.Button btShowValue;
    }
}