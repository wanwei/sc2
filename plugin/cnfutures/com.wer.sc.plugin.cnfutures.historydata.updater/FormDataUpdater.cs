using com.wer.sc.utils.ui.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.plugin.cnfutures.historydata.datapreparer
{
    public partial class FormDataUpdater : Form
    {
        public FormDataUpdater()
        {
            InitializeComponent();

            this.tbSrcPath.Text = @"E:\FUTURES\CSV\TICK";
            this.tbAdjustedPath.Text = @"E:\FUTURES\CSV\TICKADJUSTED";
            this.tbDataCenterUri.Text = @"FILE:D:\SCDATA\CNFUTURES\";
            this.rb_New.Checked = true;
            controlDataProceed1.BeforeProceedStart += ControlDataProceed1_BeforeProceedStart;
        }

        private void ControlDataProceed1_BeforeProceedStart()
        {
            string srcDataPath = tbSrcPath.Text;
            string pluginSrcDataPath = tbAdjustedPath.Text;
            //StepPreparer preparer = new StepPreparer(srcDataPath, pluginSrcDataPath);
            //List<IStep> steps = preparer.GetAllSteps();
            DataProceed_CnFuturesData proceed = new DataProceed_CnFuturesData(srcDataPath, pluginSrcDataPath, "", !this.rb_New.Checked);
            controlDataProceed1.DataProceed = proceed;
        }
    }
}
