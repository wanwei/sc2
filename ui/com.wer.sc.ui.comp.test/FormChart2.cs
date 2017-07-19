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
    public partial class FormChart2 : Form
    {
        private CompChart compChart1;

        public FormChart2()
        {
            InitializeComponent();
            this.compChart1 = compMain1.CompChart1;
            this.compChart1.PaintChart();
        }

        private void tb_SwitchTimeLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.TimeLine;
            this.compChart1.PaintChart();
        }

        private void tb_SwitchKLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_SwitchTick_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.Tick;
            this.compChart1.PaintChart();
        }

        private void tb_KLine1_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 5;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 15;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.HOUR;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.DAY;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine5S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 5;
            this.compChart1.KlineTimeType = data.KLineTimeType.SECOND;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine15S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 15;
            this.compChart1.KlineTimeType = data.KLineTimeType.SECOND;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            this.compChart1.Backward(50);
            this.compChart1.PaintChart();
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            this.compChart1.Forward(50);
            this.compChart1.PaintChart();
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            FormTime ft = new FormTime(this.compChart1.Time);
            ft.StartPosition = FormStartPosition.CenterParent;
            
            DialogResult result = ft.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.compChart1.Time = ft.Time;
                this.compChart1.PaintChart();
            }
        }

        private void tb_Refresh_Click(object sender, EventArgs e)
        {
            //if (this.compChart1.ChartType == ChartType.KLine)
            //    this.compChart1.kl
        }
    }
}
