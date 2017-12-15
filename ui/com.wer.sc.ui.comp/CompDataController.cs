using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件的数据控制器
    /// </summary>
    public class CompDataController
    {
        private CompData prevCompData;

        private CompData compData;

        private IDataNavigate currentNavigater;

        public CompDataController(IDataNavigate dataNavigater, CompData compData)
        {
            this.compData = compData;
            this.prevCompData = (CompData)compData.Clone();
            this.currentNavigater = dataNavigater;
            this.currentNavigater.OnNavigateTo += CurrentNavigater_OnNavigateTo;
        }

        public CompData CompData
        {
            get { return compData; }
        }

        /// <summary>
        /// 切换数据包，并切换时间
        /// </summary>
        /// <param name="dataPackage"></param>
        /// <param name="time"></param>
        public void Change(IDataPackage_Code dataPackage, double time)
        {
            this.compData.Change(dataPackage.Code, time);
            this.currentNavigater.ChangeByDataPackage(dataPackage, time);
        }

        private void CurrentNavigater_OnNavigateTo(object sender, data.navigate.DataNavigateEventArgs e)
        {
            this.prevCompData.CopyFrom(this.compData);
            this.compData.Change(e.Code, e.Time);
            this.compData.ChangeKLineIndex(currentNavigater.GetKLineData(compData.KlinePeriod).BarPos);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new CompDataChangeArgument(prevCompData, compData));
        }

        public void Change(string code, double time, KLinePeriod period)
        {
            bool isChanged = this.compData.Change(code, time, period);
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
            bool isChanged = this.compData.Change(code);
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
            bool isChanged = this.compData.Change(time);
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
            bool isChanged = this.compData.Change(code, time);
            if (!isChanged)
                return;
            this.currentNavigater.Change(code, time);
        }

        public void ChangeKLinePeriod(KLinePeriod klinePeriod)
        {
            this.prevCompData.CopyFrom(this.compData);
            this.compData.Change(klinePeriod);
            this.compData.ChangeKLineIndex(this.currentNavigater.GetKLineData(klinePeriod).BarPos);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new CompDataChangeArgument(prevCompData, compData));
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
            this.prevCompData.CopyFrom(this.compData);
            this.compData.Change(chartType);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new CompDataChangeArgument(prevCompData, compData));
        }

        /// <summary>
        /// 视图向前进或向后退，只在K线上有用
        /// 该方法不会改变当前时间，只会改变当前显示的K线
        /// </summary>
        /// <param name="cnt"></param>
        public void ForwardView(int cnt)
        {
            if (compData.ChartType != ChartType.KLine)
                return;

            IKLineData klineData = this.currentNavigater.GetKLineData(compData.KlinePeriod);

            int currentIndex = compData.ShowKLineIndex + cnt;
            if (currentIndex < 0)
                currentIndex = 0;
            else if (currentIndex >= klineData.BarPos)
                currentIndex = klineData.BarPos;

            this.prevCompData.CopyFrom(this.compData);
            this.compData.ChangeKLineIndex(currentIndex);
            if (this.OnDataChanged != null)
                this.OnDataChanged(this, new CompDataChangeArgument(prevCompData, compData));
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

        public IRealTimeDataReader_Code CurrentRealTimeDataReader
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

        public event DelegateOnCompDataChanged OnDataChanged;
    }

    public delegate void DelegateOnCompDataChanged(object sender, CompDataChangeArgument arg);

    public class CompDataChangeArgument
    {
        private CompData prevCompData;

        private CompData currentCompData;

        public CompData PrevCompData
        {
            get
            {
                return prevCompData;
            }

        }

        public CompData CurrentCompData
        {
            get
            {
                return currentCompData;
            }
        }

        public CompDataChangeArgument(CompData prevCompData, CompData currentCompData)
        {
            this.prevCompData = prevCompData;
            this.currentCompData = currentCompData;
        }
    }
}