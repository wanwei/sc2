using com.wer.sc.data;
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

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_Ana3 : Form
    {
        public FrmGraphicDrawer_Ana3()
        {
            InitializeComponent();

            anaComponent1.DataPath = @"D:\SCDATA\CNFUTURES";
            anaComponent1.Drawer.Show("m13", 20100101, 20150101, new KLinePeriod(KLinePeriod.TYPE_DAY, 1));

            cbPeriod.Items.Add(new CbItem(0, "秒"));
            cbPeriod.Items.Add(new CbItem(1, "分钟"));
            cbPeriod.Items.Add(new CbItem(2, "小时"));
            cbPeriod.Items.Add(new CbItem(3, "天"));
            cbPeriod.Items.Add(new CbItem(4, "周"));
            cbPeriod.SelectedIndex = 3;
        }

        private Plugin_KLineModel model;

        private void btModel_Click(object sender, EventArgs e)
        {
            FrmModel frmMode = new FrmModel();
            DialogResult result = frmMode.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.model = frmMode.Model;
                lbModel.Text = this.model.GetType().Name;
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            String code = tbCode.Text;
            int startDate = int.Parse(tbStart.Text);
            int endDate = int.Parse(tbEnd.Text);
            KLinePeriod period = new KLinePeriod(cbPeriod.SelectedIndex, int.Parse(tbPeriod.Text));
            if (this.model == null) { 
                this.anaComponent1.Drawer.Show(code, startDate, endDate, period);                
            }
            else
                this.anaComponent1.Drawer.Run(code, startDate, endDate, period, model);
        }
    }

    class CbItem
    {
        int index;

        String text;
        public CbItem(int index, String text)
        {
            this.index = index;
            this.text = text;
        }
        public override String ToString()
        {
            return text;
        }
    }
}
