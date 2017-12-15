using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    public class CompChartModifier
    {
        private CompChart compChart;

        private CompChartData compChartData;

        public CompChartModifier(CompChart compChart)
        {
            this.compChart = compChart;
            this.compChartData = compChart.CompChartData;
        }

        /// <summary>
        /// 切换数据包，并切换时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        public void Change(IDataPackage_Code dataPackage, double time)
        {

        }

        public void Change(string code, double time, KLinePeriod period)
        {

        }

        /// <summary>
        /// 修改图中显示的品种
        /// </summary>
        /// <param name="code"></param>
        public void Change(string code)
        {
            if (code == compChartData.Code)
                return;
            //compChartData.
            //this.oldChartDataState = GetChartDataState();
            //code = value;
        }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        public void Change(double time) { }

        /// <summary>
        /// 修改图中显示的品种和当前时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public void Change(String code, double time) { }

        public void ChangeKLinePeriod(KLinePeriod klinePeriod)
        {

        }

        /// <summary>
        /// 视图向前进或后退，
        /// </summary>
        /// <param name="forwardPeriod"></param>
        public void ForwardTime(ForwardPeriod forwardPeriod) { }

        /// <summary>
        /// 切换图中显示的图形，K线、分时线或tick线
        /// </summary>
        /// <param name="chartType"></param>
        public void ChangeChartType(ChartType chartType) { }

        /// <summary>
        /// 修改K线每一个bar显示的宽度
        /// </summary>
        /// <param name="width"></param>
        public void ChangeBarWidth(double width) { }

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间，只会改变当前显示的K线
        /// </summary>
        /// <param name="cnt"></param>
        public void ForwardView(int cnt) { }


        /// <summary>
        /// 刷新图形，如果是K线，则返回当前时间显示的K线
        /// </summary>
        public void Refresh() { }

        /// <summary>
        /// 自动前进，会自动改变当前时间，模拟真实交易场景
        /// </summary>
        public void Play() { }

        /// <summary>
        /// 停止自动前进
        /// </summary>
        public void Pause() { }
    }
}
