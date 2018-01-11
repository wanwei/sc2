using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data;
using com.wer.sc.data.reader;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentController
    {
        private ChartComponentData prevChartComponentData;

        private ChartComponentData chartComponentData;

        private IDataNavigate currentNavigater;

        public ChartComponentController(IDataNavigate dataNavigater, ChartComponentData ChartComponentData)
        {
            this.chartComponentData = ChartComponentData;
            this.prevChartComponentData = (ChartComponentData)ChartComponentData.Clone();
            this.currentNavigater = dataNavigater;
            this.currentNavigater.OnNavigateTo += CurrentNavigater_OnNavigateTo;
        }

        public ChartComponentData ChartComponentData
        {
            get { return chartComponentData; }
        }

        /// <summary>
        /// 切换数据包，并切换时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        public void Change(IDataPackage_Code dataPackage, double time)
        {
            this.ChartComponentData.Change(dataPackage.Code, time);
            this.currentNavigater.ChangeByDataPackage(dataPackage, time);
        }

        private void CurrentNavigater_OnNavigateTo(object sender, data.navigate.DataNavigateEventArgs e)
        {
            this.prevChartComponentData.CopyFrom(this.chartComponentData);
            this.ChartComponentData.Change(e.Code, e.Time);
            this.ChartComponentData.ChangeKLineIndex(currentNavigater.GetKLineData(ChartComponentData.KlinePeriod).BarPos);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new ChartComponentDataChangeArgument(prevChartComponentData, ChartComponentData));
        }

        public void Change(string code, double time, KLinePeriod period)
        {
            bool isChanged = this.ChartComponentData.Change(code, time, period);
            if (!isChanged)
                return;
            this.currentNavigater.Change(code, time);
        }

        /// <summary>
        /// 修改图中显示的品种
        /// </summary>
        /// <param name="code"></param>
        public void Change(string code)
        {
            bool isChanged = this.ChartComponentData.Change(code);
            if (!isChanged)
                return;
            this.currentNavigater.Change(code);
        }

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        public void Change(double time)
        {
            bool isChanged = this.ChartComponentData.Change(time);
            if (!isChanged)
                return;
            this.currentNavigater.NavigateTo(time);
        }

        /// <summary>
        /// 修改图中显示的品种和当前时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        public void Change(String code, double time)
        {
            bool isChanged = this.ChartComponentData.Change(code, time);
            if (!isChanged)
                return;
            this.currentNavigater.Change(code, time);
        }

        public void ChangeKLinePeriod(KLinePeriod klinePeriod)
        {
            this.prevChartComponentData.CopyFrom(this.ChartComponentData);
            this.ChartComponentData.Change(klinePeriod);
            this.ChartComponentData.ChangeKLineIndex(this.currentNavigater.GetKLineData(klinePeriod).BarPos);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new ChartComponentDataChangeArgument(prevChartComponentData, ChartComponentData));
        }

        /// <summary>
        /// 视图向前进
        /// </summary>
        /// <param name="forwardPeriod"></param>
        public void ForwardTime(KLinePeriod forwardPeriod)
        {
            this.currentNavigater.Forward(forwardPeriod);
        }

        public void BackwardTime(KLinePeriod forwardPeriod)
        {
            this.currentNavigater.Backward(forwardPeriod);
        }

        /// <summary>
        /// 切换图中显示的图形，K线、分时线或tick线
        /// </summary>
        /// <param name="chartType"></param>
        public void ChangeChartType(ChartType chartType)
        {
            this.prevChartComponentData.CopyFrom(this.ChartComponentData);
            this.ChartComponentData.Change(chartType);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new ChartComponentDataChangeArgument(prevChartComponentData, ChartComponentData));
        }

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间，只会改变当前显示的K线
        /// </summary>
        /// <param name="cnt"></param>
        public void ForwardView(int cnt)
        {
            if (ChartComponentData.ChartType != ChartType.KLine)
                return;

            IKLineData klineData = this.currentNavigater.GetKLineData(ChartComponentData.KlinePeriod);

            int currentIndex = ChartComponentData.ShowKLineIndex + cnt;
            if (currentIndex < 0)
                currentIndex = 0;
            else if (currentIndex >= klineData.BarPos)
                currentIndex = klineData.BarPos;

            this.prevChartComponentData.CopyFrom(this.ChartComponentData);
            this.ChartComponentData.ChangeKLineIndex(currentIndex);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new ChartComponentDataChangeArgument(prevChartComponentData, ChartComponentData));
        }

        /// <summary>
        /// 刷新图形，如果是K线，则返回当前时间显示的K线
        /// </summary>
        public void Refresh()
        {

        }
        public bool IsPlayIng
        {
            get { return currentNavigater.IsPlaying; }
        }
        /// <summary>
        /// 自动前进，会自动改变当前时间，模拟真实交易场景
        /// </summary>
        public void Play()
        {
            this.currentNavigater.Play();
        }

        /// <summary>
        /// 停止自动前进
        /// </summary>
        public void Pause()
        {
            this.currentNavigater.Pause();
        }

        public IRealTimeData_Code CurrentRealTimeDataReader
        {
            get
            {
                return this.currentNavigater;
            }
        }

        public IDataNavigate CurrentNavigater
        {
            get { return this.currentNavigater; }
        }

        public event DelegateOnChartComponentDataChanged OnDataChanged;
    }

    public delegate void DelegateOnChartComponentDataChanged(object sender, ChartComponentDataChangeArgument arg);

    public class ChartComponentDataChangeArgument
    {
        private ChartComponentData prevChartComponentData;

        private ChartComponentData currentChartComponentData;

        public ChartComponentData PrevChartComponentData
        {
            get
            {
                return prevChartComponentData;
            }

        }

        public ChartComponentData CurrentChartComponentData
        {
            get
            {
                return currentChartComponentData;
            }
        }

        public ChartComponentDataChangeArgument(ChartComponentData prevChartComponentData, ChartComponentData currentChartComponentData)
        {
            this.prevChartComponentData = prevChartComponentData;
            this.currentChartComponentData = currentChartComponentData;
        }
    }
}
