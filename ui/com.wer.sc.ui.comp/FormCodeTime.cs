using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    public partial class FormCodeTime : Form
    {
        private string code;

        private double time;

        public FormCodeTime(string code, double time)
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            this.code = code;
            this.time = time;
            this.tbCode.Text = code;
            this.tbTime.Text = time.ToString();
        }

        public string Code
        {
            get
            {
                if (!code.Equals(tbCode.Text.Trim()))
                    code = tbCode.Text.Trim();
                return code;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        private void tbCode_KeyDown(object sender, KeyEventArgs e)
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
                    this.code = tbCode.Text;
                    this.tbTime.Focus();
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

        private void tbTime_KeyDown(object sender, KeyEventArgs e)
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
                    double s = double.Parse(tbTime.Text);
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
    }
}
