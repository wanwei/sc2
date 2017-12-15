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

namespace com.wer.sc.ui.comp.strategy
{
    public partial class FormStrategyDataPackage : Form
    {
        public FormStrategyDataPackage(IDataPackage_Code dataPackage)
        {
            InitializeComponent();

            this.lbCode.Text = dataPackage.Code;
            this.lbStart.Text = dataPackage.StartDate.ToString();
            this.lbEnd.Text = dataPackage.EndDate.ToString();
        }
    }
}
