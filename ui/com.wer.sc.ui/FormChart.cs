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

namespace com.wer.sc.ui
{
    public partial class FormChart : Form
    {
        public FormChart()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.AppendPrivatePath(@"strategy");
            //string code = "sz000932";
            string code = "rb1805";
            double time = double.Parse(DateTime.Now.ToString("yyyyMMdd.HHmmss"));
            this.mainComponent1.Init(DataCenter.Default, code, time);
            this.menuComponent1.BindChartComponent(this.mainComponent1.ChartComponent);
            this.mainComponent1.ChartComponent.OnChartRefresh += ChartComponent_OnChartRefresh;
        }

        private void ChartComponent_OnChartRefresh(object sender, comp.ChartComponentRefreshArguments arg)
        {
            SetLbTime(arg.CurrentCompData.Time);
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
    }
}
