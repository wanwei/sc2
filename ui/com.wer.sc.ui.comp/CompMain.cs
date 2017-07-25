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
    public partial class CompMain : UserControl
    {
        public CompMain()
        {
            InitializeComponent();
            this.compCurrentInfo1.CompChartData = CompChart1.CompChartData;
        }

        public CompChart CompChart1
        {
            get { return compChart1; }
        }


        [Browsable(true), DisplayName("数据中心"), Description("数据中心"), Category("自定义属性"), DefaultValue(null)]
        public string DataCenterUri
        {
            get
            {
                return CompChart1.DataCenterUri;
            }

            set
            {
                this.compChart1.DataCenterUri = value;
            }
        }

        [Browsable(true), DisplayName("合约或股票代码"), Description("合约或股票代码"), Category("自定义属性"), DefaultValue(null)]
        public string Code
        {
            get
            {
                return compChart1.Code;
            }

            set
            {
                this.compChart1.Code = value;
            }
        }

        [Browsable(true), DisplayName("图表类型"), Description("图表类型"), Category("自定义属性"), DefaultValue(ChartType.KLine)]
        public ChartType ChartType
        {
            get
            {
                return compChart1.ChartType;
            }

            set
            {
                this.compChart1.ChartType = value;
            }
        }

        [Browsable(true), DisplayName("当前时间"), Description("当前时间"), Category("自定义属性"), DefaultValue(20150107.093005)]
        public double Time
        {
            get
            {
                return compChart1.Time;
            }

            set
            {
                this.compChart1.Time = value;
            }
        }

        [Browsable(true), DisplayName("K线柱子宽度"), Description("显示K线数量"), Category("自定义属性"), DefaultValue(5)]
        public float KLineBlockWidth
        {
            get
            {
                return compChart1.KLineBlockWidth;
            }

            set
            {
                this.compChart1.KLineBlockWidth = value;
            }
        }

        [Browsable(true), DisplayName("K线周期"), Description("K线周期"), Category("自定义属性"), DefaultValue(1)]
        public int KlinePeriod
        {
            get
            {
                return compChart1.KlinePeriod;
            }

            set
            {
                this.compChart1.KlinePeriod = value;
            }
        }

        [Browsable(true), DisplayName("K线种类"), Description("K线种类"), Category("自定义属性"), DefaultValue(KLineTimeType.MINUTE)]
        public KLineTimeType KlineTimeType
        {
            get
            {
                return compChart1.KlineTimeType;
            }

            set
            {
                this.compChart1.KlineTimeType = value;
            }
        }

    }
}
