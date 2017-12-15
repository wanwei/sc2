using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.strategy;
using com.wer.sc.strategy.draw;
using com.wer.sc.ui.comp;
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
    public partial class FormCompChart : Form
    {
        private CompDataController controller;

        public FormCompChart()
        {
            InitializeComponent();

            string code = "rb1710";
            double time = 20170601.093055;
            this.compChart21.Init(DataCenter.Default, code, time);
            this.controller = compChart21.Controller;
        }

        private void tb_KLine1_Click(object sender, EventArgs e)
        {
            this.controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Minute);
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            this.controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_5Minute);
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            this.controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_15Minute);
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            this.controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Hour);
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            this.controller.ChangeKLinePeriod(KLinePeriod.KLinePeriod_1Day);
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            this.controller.ForwardView(-20);
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            this.controller.ForwardView(20);
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            FormTime ft = new FormTime(this.controller.CompData.Time);
            DialogResult result = ft.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.controller.Change(ft.Time);
            }
        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {
            this.controller.BackwardTime(KLinePeriod.KLinePeriod_1Minute);
        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {
            this.controller.ForwardTime(KLinePeriod.KLinePeriod_1Minute);
        }

        private void btStrategy_Click(object sender, EventArgs e)
        {
            CompStrategyRunner runner = new CompStrategyRunner(this.compChart21);
            //Strategy_MultiMa strategy = new Strategy_MultiMa();
            //runner.Strategy = strategy;
            runner.Run();
        }
    }
}
