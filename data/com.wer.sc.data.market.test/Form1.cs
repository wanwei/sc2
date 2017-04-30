using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.market
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTestReceiveTickData frm = new FormTestReceiveTickData();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormTestTragger_Writer frm = new FormTestTragger_Writer();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormTestRealTimeDataBuilder frm = new FormTestRealTimeDataBuilder();
            frm.ShowDialog();
        }
    }
}
