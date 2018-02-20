using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.ui.comp
{
    public partial class CompCodePackage : UserControl
    {
        private CodePeriodPackageInfo codePackageInfo;

        public CodePeriodPackageInfo CodePackage
        {
            get
            {
                this.codePackageInfo.Start = int.Parse(tbStart.Text);
                this.codePackageInfo.End = int.Parse(tbEnd.Text);
                return codePackageInfo;
            }
        }

        public CompCodePackage()
        {
            InitializeComponent();
        }

        public void Init(CodePeriodPackageInfo codePackageInfo)
        {
            this.codePackageInfo = codePackageInfo;
            this.tbStart.Text = this.codePackageInfo.Start.ToString();
            this.tbEnd.Text = this.codePackageInfo.End.ToString();
            this.RefreshGrid(codePackageInfo);
        }

        private void RefreshGrid(CodePeriodPackageInfo codePackageInfo)
        {
            string title = "合约";
            if (codePackageInfo.CodeChooseMethod == CodeChooseMethod.Maincontract)
                title = "主合约品种";
            else if (codePackageInfo.CodeChooseMethod == CodeChooseMethod.Catelog)
                title = "品种";
            this.gridCodes.Columns[0].HeaderText = title;
            this.gridCodes.Rows.Clear();
            for (int i = 0; i < codePackageInfo.Codes.Count; i++)
            {
                this.gridCodes.Rows.Add(codePackageInfo.Codes[i]);
            }
        }

        private void btModifyCodes_Click(object sender, EventArgs e)
        {
            FormCodePackageSelected form = new FormCodePackageSelected(codePackageInfo);
            DialogResult result = form.ShowDialog();
            if(result == DialogResult.OK)
            {
                RefreshGrid(this.codePackageInfo);
            }
        }
    }
}
