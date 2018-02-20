using com.wer.sc.data.datapackage;
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

        private void btNumberUpDown_Click(object sender, EventArgs e)
        {
            FormNumberUpDown form = new FormNumberUpDown();
            form.ShowDialog();
        }

        private void btCodePackage_Click(object sender, EventArgs e)
        {
            CodePeriodPackageInfo codePackage = new CodePeriodPackageInfo();
            codePackage.Start = 20160101;
            codePackage.End = 20170101;
            codePackage.CodeChooseMethod = CodeChooseMethod.Maincontract;
            codePackage.Codes.Add("RB");
            FormCodePackage form = new FormCodePackage(codePackage);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show(codePackage.ToString());
            }
        }

        private void btCodeTree_Click(object sender, EventArgs e)
        {
            FormCompCodeTree form = new FormCompCodeTree();
            form.ShowDialog();
        }
    }
}
