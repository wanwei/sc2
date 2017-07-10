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
    public partial class FrmGraphicDrawer_DataNavigate2 : Form
    {
        private IDataNavigate2 dataNavigate;

        public FrmGraphicDrawer_DataNavigate2()
        {
            InitializeComponent();

            cbPeriod.Items.Add(new CbItem(0, "秒"));
            cbPeriod.Items.Add(new CbItem(1, "分钟"));
            cbPeriod.Items.Add(new CbItem(2, "小时"));
            cbPeriod.Items.Add(new CbItem(3, "天"));
            cbPeriod.Items.Add(new CbItem(4, "周"));
            cbPeriod.SelectedIndex = 3;
        }

        private void tabContent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btRead_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            String code = GetCode();
            double time = GetTime();

            if (dataNavigate == null)
            {
                DataReaderFactory fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
                DataNavigateMgr mgr = new DataNavigateMgr(fac);
                dataNavigate = mgr.CreateNavigate(code, time);
                dataNavigate.OnDataChangeHandler += DataNavigate_OnDataChangeHandler;
            }

            int index = tabContent.SelectedIndex;
            if (index == 0)
            {
                CalcKLineText();
            }
            else if (index == 1)
            {
                CalcRealText();
            }
            else
            {
                CalcTickText();
            }
        }

        private void DataNavigate_OnDataChangeHandler(object source, DataChangeEventArgs e)
        {
            tbChangeInfo.Text = e.ToString();
        }

        private void CalcKLineText()
        {
            KLinePeriod period = GetPeriods();
            IKLineData klineData = dataNavigate.GetKLineData(period);
            //tbKLine.AppendText(klineData.PrintAll());
        }

        private void CalcRealText()
        {
            ITimeLineData realData = dataNavigate.GetRealData();
            //tbReal.AppendText(realData.PrintAll());
        }

        private void CalcTickText()
        {
            ITickData tickData = dataNavigate.GetTickData();
            //tbTick.AppendText(tickData.PrintAll());
        }

        private string GetCode()
        {
            return tbCode.Text.Trim();
        }

        private KLinePeriod GetPeriods()
        {
            return new KLinePeriod(cbPeriod.SelectedIndex, int.Parse(tbPeriod.Text));
        }

        private int GetLength()
        {
            return int.Parse(tbPeriod.Text);
        }

        private int GetStartDate()
        {
            return int.Parse(tbStart.Text);
        }

        private int GetEndDate()
        {
            return int.Parse(tbEnd.Text);
        }

        private double GetTime()
        {
            return double.Parse(tbTime.Text);
        }

        private void btForward_Click(object sender, EventArgs e)
        {
            if (dataNavigate == null)
            {
                MessageBox.Show("请先加载数据");
                return;
            }
            dataNavigate.Forward(GetPeriods(), GetLength());
        }

        private void btTickForward_Click(object sender, EventArgs e)
        {
            if (dataNavigate == null)
            {
                MessageBox.Show("请先加载数据");
                return;
            }
            dataNavigate.ForwardTick(GetLength());
        }
    }
}
