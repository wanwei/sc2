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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btTestDrawer_Click(object sender, EventArgs e)
        {
            FormChartComponentDrawer drawer = new FormChartComponentDrawer();
            drawer.ShowDialog();
        }

        private void btControl_Click(object sender, EventArgs e)
        {
            FormChartComponent form = new FormChartComponent();
            form.ShowDialog();
        }

        private void btCurrentInfoComponent_Click(object sender, EventArgs e)
        {
            FormCurrentInfoComponent form = new FormCurrentInfoComponent();
            form.ShowDialog();
        }

        private void btMainComponent_Click(object sender, EventArgs e)
        {
            FormMainComponent form = new FormMainComponent();
            form.ShowDialog();
        }

        private void btStrategyTreeComponent_Click(object sender, EventArgs e)
        {
            FormStrategyTreeComponent form = new FormStrategyTreeComponent();
            form.ShowDialog();
        }
    }
}
