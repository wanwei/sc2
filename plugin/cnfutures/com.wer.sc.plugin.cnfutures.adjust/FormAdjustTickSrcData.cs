using com.wer.sc.data;
using com.wer.sc.plugin.cnfutures.config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.plugin.cnfutures.adjust
{
    public partial class FormAdjustTickSrcData : Form
    {
        //string srcPath = @"E:\FUTURES\CSV\TICKADJUSTED\";
        //string targetPath = @"E:\FUTURES\CSV\DATACENTERSOURCE\";

        public FormAdjustTickSrcData()
        {
            InitializeComponent();
            //this.controlDataUpdate1.DataProceed = new AdjustTickStepGetter();
            //this.controlDataUpdate1.DataProceed = new AdjustDataCenterTickData();
            //this.controlDataUpdate1.DataProceed = new AdjustIndexTick();
            this.controlDataUpdate1.DataProceed = new DeleteZZKLine_20160429();
            
        }
    }
}
