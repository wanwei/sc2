namespace com.wer.sc.data.generator
{
    partial class FormPrepareHistoryData
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.controlDataProceed1 = new com.wer.sc.utils.ui.update.ControlDataUpdate();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 41);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // controlDataProceed1
            // 
            this.controlDataProceed1.DataProceed = null;
            this.controlDataProceed1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlDataProceed1.Location = new System.Drawing.Point(0, 41);
            this.controlDataProceed1.Margin = new System.Windows.Forms.Padding(5);
            this.controlDataProceed1.Name = "controlDataProceed1";
            this.controlDataProceed1.Size = new System.Drawing.Size(715, 65);
            this.controlDataProceed1.TabIndex = 5;
            // 
            // FormPrepareHistoryData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 106);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.controlDataProceed1);
            this.Name = "FormPrepareHistoryData";
            this.Text = "准备历史数据";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private sc.utils.ui.update.ControlDataUpdate controlDataProceed1;
    }
}