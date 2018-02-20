using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.datapackage;

namespace com.wer.sc.ui.comp
{
    public partial class FormCodePackageSelected : Form
    {
        private CodePeriodPackageInfo codePackageInfo;

        public FormCodePackageSelected(CodePeriodPackageInfo codePackageInfo)
        {
            InitializeComponent();
            this.codeTree1.MultiSelect = true;
            this.codeTree1.Init();
            this.codePackageInfo = codePackageInfo;
            if (this.codePackageInfo.CodeChooseMethod == CodeChooseMethod.Maincontract)
                this.cbChooseType.SelectedIndex = 2;
            else if (this.codePackageInfo.CodeChooseMethod == CodeChooseMethod.Catelog)
                this.cbChooseType.SelectedIndex = 1;
            else
                this.cbChooseType.SelectedIndex = 0;
        }

        private void cbChooseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectType(this.cbChooseType.SelectedIndex);
        }

        private void SelectType(int type)
        {
            if (type == 0)
                this.codeTree1.SelectCatelog = false;
            else
                this.codeTree1.SelectCatelog = true;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.codePackageInfo.Codes.Clear();
            if (this.cbChooseType.SelectedIndex == 0)
            {
                this.codePackageInfo.CodeChooseMethod = CodeChooseMethod.Normal;
                this.codePackageInfo.Codes.AddRange(codeTree1.SelectedCodes);
            }
            else if (this.cbChooseType.SelectedIndex == 1)
            {
                this.codePackageInfo.CodeChooseMethod = CodeChooseMethod.Catelog;
                this.codePackageInfo.Codes.AddRange(codeTree1.SelectedCatelogs);
            }
            else
            {
                this.codePackageInfo.CodeChooseMethod = CodeChooseMethod.Maincontract;
                this.codePackageInfo.Codes.AddRange(codeTree1.SelectedCatelogs);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}