using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.check
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btDataBrowser_Click(object sender, EventArgs e)
        {
            FrmDataBrowser frm = new FrmDataBrowser();
            frm.ShowDialog();
        }

        private void btTestGen_Click(object sender, EventArgs e)
        {
            FrmTestGenData frm = new FrmTestGenData();
            frm.ShowDialog();
        }

        private void btLoadReal_Click(object sender, EventArgs e)
        {
            FrmLoadRealData frm = new FrmLoadRealData();
            frm.ShowDialog();
        }

        private void btDataNavigate_Click(object sender, EventArgs e)
        {
            FrmDataNavigate frm = new FrmDataNavigate();
            frm.ShowDialog();
        }
    }
}
