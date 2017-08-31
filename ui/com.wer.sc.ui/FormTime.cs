using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public partial class FormTime : Form
    {
        private double time;

        public FormTime(double time)
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            this.time = time;
            this.textBox1.Text = time.ToString();
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }                
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double s = double.Parse(textBox1.Text);
                    this.time = s;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FormTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
