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

namespace com.wer.sc.ui.comp.test
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
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
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Minute;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_5Minute;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_15Minute;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Hour;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_1Hour;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine5S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_5Second;
            this.compChart1.ChartType = ChartType.KLine;
            this.compChart1.PaintChart();
        }

        private void tb_KLine15S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = KLinePeriod.KLinePeriod_15Second;
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

        private bool isPlaying = false;

        private void tb_Play_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                Image img = Image.FromHbitmap(Properties.Resources.play.ToBitmap().GetHbitmap());
                tb_Play.Image = img;
                this.isPlaying = false;
            }
            else
            {
                Image img = Image.FromHbitmap(Properties.Resources.pause.ToBitmap().GetHbitmap());
                tb_Play.Image = img;
                this.isPlaying = true;
            }
        }

        private void tb_Pause_Click(object sender, EventArgs e)
        {

        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {

        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {

        }

        private void tb_ForwardSetting_Click(object sender, EventArgs e)
        {

        }
    }
}
