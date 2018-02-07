using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
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
    public partial class FormCodePackage : Form
    {
        private CodePackageInfo codePackageInfo;

        public FormCodePackage(CodePackageInfo codePackageInfo)
        {
            InitializeComponent();
            this.codePackageInfo = codePackageInfo;
            this.Init(codePackageInfo);
        }

        private void Init(CodePackageInfo dataPackageInfo)
        {
            this.compCodePackage1.Init(dataPackageInfo);           
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.codePackageInfo = this.compCodePackage1.CodePackage;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
    }
}
