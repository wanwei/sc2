using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.test
{
    public partial class FormNumberUpDown : Form
    {
        public FormNumberUpDown()
        {
            InitializeComponent();
            numberMount.IsInputState = true;
            numberMount.MinValue = 0;
            numberPrice.IsInputState = false;
            numberPrice.NormalText = "现价";
            numberPrice.OnStateChange += NumberPrice_OnStateChange;
        }

        private void NumberPrice_OnStateChange(object sender, bool state)
        {
            if (state)
                numberPrice.Value = 3215;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numberPrice.NormalText = "现价";
        }

        private void btShowValue_Click(object sender, EventArgs e)
        {
            MessageBox.Show(numberMount.Value.ToString());
            MessageBox.Show(numberPrice.Value.ToString());
        }
    }
}
