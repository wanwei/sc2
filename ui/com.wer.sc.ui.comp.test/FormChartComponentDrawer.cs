using com.wer.sc.data;
using com.wer.sc.data.navigate;
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
    public partial class FormChartComponentDrawer : Form
    {
        ChartComponentController controller;
        public FormChartComponentDrawer()
        {
            InitializeComponent();

            string code = "rb1710";
            double time = 20170601.093055;
            KLinePeriod klinePeriod = KLinePeriod.KLinePeriod_1Minute;
            IDataNavigate dataNavigater = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);
            IKLineData klineData = dataNavigater.GetKLineData(klinePeriod);
            int showKLineIndex = klineData.BarPos;
            ChartComponentData compData = new ChartComponentData(code, time, klinePeriod, showKLineIndex);
            controller = new ChartComponentController(dataNavigater, compData);

            ChartComponentDrawer drawer = new ChartComponentDrawer(this, controller);
            //drawer.RePaint();
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            controller.ForwardView(-20);
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            controller.ForwardView(20);
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_15Minute);
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_5Minute);
        }

        private void tb_KLine1_Click(object sender, EventArgs e)
        {
            controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Minute);
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Hour);
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Day);
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            string code = "rb1710";
            double time = 20170501.093055;
            controller.Change(code, time);
        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {

        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {

        }

        private void tb_ForwardSetting_Click(object sender, EventArgs e)
        {

        }
    }
}
