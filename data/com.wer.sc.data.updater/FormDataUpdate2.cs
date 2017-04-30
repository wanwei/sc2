using com.wer.sc.data.datacenter;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.update
{
    public partial class FormDataUpdate : Form
    {
        private IPlugin_HistoryData plugin_HistoryData;

        public FormDataUpdate(IPlugin_HistoryData plugin_HistoryData)
        {
            InitializeComponent();
            this.plugin_HistoryData = plugin_HistoryData;

            //this.tbDataCenter.Text = this.plugin_HistoryData.GetDataCenterUri();
            //DataCenter dataCenter = DataCenterManager.Create()
            //this.controlDataProceed1.DataProceed = new DataProceed_DataUpdate(this.plugin_HistoryData, this.tbDataCenter.Text, !rb_New.Checked);
            this.controlDataProceed1.DataProceed = new DataUpdate(this.plugin_HistoryData, null, false);
            //this.tbDataCenter.Text, !rb_New.Checked);
        }
    }
}
