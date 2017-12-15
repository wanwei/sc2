using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data;

namespace com.wer.sc.ui.comp
{
    public partial class MainComponent : UserControl
    {
        public MainComponent()
        {
            InitializeComponent();
        }

        public void Init(IDataCenter dataCenter, string code, double time)
        {
            Init(dataCenter, code, time, KLinePeriod.KLinePeriod_1Minute);
        }

        public void Init(IDataCenter dataCenter, string code, double time, KLinePeriod klinePeriod)
        {
            this.chartComponent1.Init(dataCenter, code, time, klinePeriod);
            this.currentInfoComponent1.Init(this.chartComponent1.Controller);
        }

        public ChartComponentController Controller
        {
            get { return this.chartComponent1.Controller; }
        }

        public ChartComponent ChartComponent
        {
            get { return this.chartComponent1; }
        }

        public CurrentInfoComponent CurrentInfoComponent
        {
            get { return this.currentInfoComponent1; }
        }
    }
}
