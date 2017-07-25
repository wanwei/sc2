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
            //this.compChart1.Code = "RB1710";
            //this.compChart1.Time = 20170531.210011;
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.OnDataRefresh += CompChart1_OnDataRefresh;
            this.SetLbTime(this.compChart1.Time);
            this.compChart1.PaintChart();
        }

        private void CompChart1_OnDataRefresh(object sender, DataRefreshArgument arg)
        {
            this.compChart1.PaintChart();
            //等待异步    
            SetLbTime(this.compChart1.Time);
        }

        private delegate void SetLabelText(object txt);//代理

        private void SetLbTime(object text)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new SetLabelText(SetLbTime), text);//通过代理调用刷新方法
            }
            else
            {
                this.lbTime.Text = text.ToString();
            }
        }

        private void tb_SwitchTimeLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.TimeLine;
        }

        private void tb_SwitchKLine_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_SwitchTick_Click(object sender, EventArgs e)
        {
            this.compChart1.ChartType = ChartType.Tick;
        }

        private void tb_Refresh_Click(object sender, EventArgs e)
        {
            if (this.compChart1.ChartType == ChartType.KLine)
                this.compChart1.PaintChart();
        }

        private void tb_KLine1_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine5_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 5;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine15_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 15;
            this.compChart1.KlineTimeType = data.KLineTimeType.MINUTE;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine1H_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.HOUR;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine1Day_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 1;
            this.compChart1.KlineTimeType = data.KLineTimeType.DAY;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine5S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 5;
            this.compChart1.KlineTimeType = data.KLineTimeType.SECOND;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLine15S_Click(object sender, EventArgs e)
        {
            this.compChart1.KlinePeriod = 15;
            this.compChart1.KlineTimeType = data.KLineTimeType.SECOND;
            this.compChart1.ChartType = ChartType.KLine;
        }

        private void tb_KLineBackward_Click(object sender, EventArgs e)
        {
            this.compChart1.Backward(50);
        }

        private void tb_KLineForward_Click(object sender, EventArgs e)
        {
            this.compChart1.Forward(50);
        }

        private void tb_ChangeTime_Click(object sender, EventArgs e)
        {
            FormTime ft = new FormTime(this.compChart1.Time);
            ft.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = ft.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.compChart1.Time = ft.Time;
            }
        }

        private void tb_ForwardSetting_Click(object sender, EventArgs e)
        {
            FormForwardSetting frm = new FormForwardSetting();
            frm.ForwardPeriod = this.compChart1.ForwardPeriod;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.compChart1.ForwardPeriod = frm.ForwardPeriod;
            }
        }

        private void tb_ForwordTime_Click(object sender, EventArgs e)
        {
            this.compChart1.ForwardTime();
        }

        private void tb_BackwordTime_Click(object sender, EventArgs e)
        {
            this.compChart1.BackwardTime();
        }

        private bool isPlaying;

        private void tb_Play_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                this.compChart1.Pause();
                isPlaying = false;
                tb_ForwardSetting.Enabled = true;
                tb_ForwordTime.Enabled = true;
                tb_BackwordTime.Enabled = true;
                tb_ChangeTime.Enabled = true;
            }
            else
            {
                this.compChart1.Play();
                isPlaying = true;
                tb_ForwardSetting.Enabled = false;
                tb_ForwordTime.Enabled = false;
                tb_BackwordTime.Enabled = false;
                tb_ChangeTime.Enabled = false;
            }
        }
    }
}