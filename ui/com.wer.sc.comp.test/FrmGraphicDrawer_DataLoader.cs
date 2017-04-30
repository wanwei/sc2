using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_DataLoader : Form
    {
        public FrmGraphicDrawer_DataLoader()
        {
            InitializeComponent();

            cbPeriod.Items.Add(new CbItem((int)KLineTimeType.SECOND, "秒"));
            cbPeriod.Items.Add(new CbItem((int)KLineTimeType.MINUTE, "分钟"));
            cbPeriod.Items.Add(new CbItem((int)KLineTimeType.HOUR, "小时"));
            cbPeriod.Items.Add(new CbItem((int)KLineTimeType.DAY, "天"));
            cbPeriod.Items.Add(new CbItem((int)KLineTimeType.WEEK, "周"));
            cbPeriod.SelectedIndex = 3;

            anaComponent1.DataPath = @"D:\SCDATA\CNFUTURES";

            this.show();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            this.show();
        }

        private void show()
        {
            String code = tbCode.Text;
            int startDate = int.Parse(tbStart.Text);
            int endDate = int.Parse(tbEnd.Text);
            KLinePeriod period = new KLinePeriod((KLineTimeType)Enum.ToObject(typeof(KLineTimeType), cbPeriod.SelectedIndex), int.Parse(tbPeriod.Text));
            this.anaComponent1.Drawer.Show(code, startDate, endDate, period);
            this.anaComponent1.Refresh();    
        }
    }
}
