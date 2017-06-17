using com.wer.sc.verify.datareader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.verify
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btFormView_Click(object sender, EventArgs e)
        {
            FormViewData frm = new FormViewData();
            frm.ShowDialog();
        }

        private void btFormReader_Click(object sender, EventArgs e)
        {
            FormDataReader frm = new FormDataReader();
            frm.ShowDialog();
        }
    }
}
