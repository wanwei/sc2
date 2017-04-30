using com.wer.sc.utils.ui.test.proceed;
using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.utils.ui.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmProceed frm = new FrmProceed();
            frm.ShowDialog();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLog form = new FormLog();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmMultiUpdate frm = new FrmMultiUpdate();
            frm.ShowDialog();
        }
    }
}
