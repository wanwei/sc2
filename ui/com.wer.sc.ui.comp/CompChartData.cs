using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ui.comp
{
    /// <summary>
    /// 组件数据提供类
    /// </summary>
    public class CompChartData:IDataPackageOwner
    {
        private IDataPackage dataPackage;

        private IDataReader dataReader;

        private bool isDataRefresh;

        private string code;

        private string dataCenterUri;

        private ChartType chartType;

        private double time;

        private float kLineBlockWidth;

        private int klinePeriod;

        private KLineTimeType klineTimeType;

        public string DataCenterUri
        {
            get
            {
                return dataCenterUri;
            }

            set
            {
                if (dataCenterUri == value)
                    return;
                dataCenterUri = value;
                this.dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
                this.IsDataRefresh = true;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                if (code == value)
                    return;
                code = value;
                this.IsDataRefresh = true;
            }
        }

        public ChartType ChartType
        {
            get
            {
                return chartType;
            }

            set
            {
                if (chartType == value)
                    return;
                chartType = value;
                this.IsDataRefresh = true;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }

            set
            {
                if (time == value)
                    return;
                time = value;
                this.IsDataRefresh = true;
            }
        }

        public float KLineBlockWidth
        {
            get
            {
                return kLineBlockWidth;
            }

            set
            {
                if (kLineBlockWidth == value)
                    return;
                kLineBlockWidth = value;
                this.IsDataRefresh = true;
            }
        }

        public int KlinePeriod
        {
            get
            {
                return klinePeriod;
            }

            set
            {
                if (klinePeriod == value)
                    return;
                klinePeriod = value;
                this.IsDataRefresh = true;
            }
        }

        public KLineTimeType KlineTimeType
        {
            get
            {
                return klineTimeType;
            }

            set
            {
                if (klineTimeType == value)
                    return;
                klineTimeType = value;
                this.IsDataRefresh = true;
            }
        }

        private IDataNavigate_Code dataNavigate_Code;

        private IHistoryDataForward_Code historyDataForward_CodePlaying;

        public IRealTimeDataReader CurrentRealTimeDataReader
        {
            get
            {
                if (isPlaying)
                    return GetHistoryDataForward_Playing();
                return GetDataNavigate_Code();
            }
        }

        public bool CheckData()
        {
            if (dataCenterUri == null)
                return false;
            if (time == 0)
                return false;
            if (code == null)
                return false;
            if (klinePeriod <= 0)
                return false;
            if (dataReader == null)
                return false;
            return true;
        }

        private IDataNavigate_Code GetDataNavigate_Code()
        {
            if (!CheckData())
                return null;
            if (dataNavigate_Code == null)
                dataNavigate_Code = DataNavigateFactory.CreateDataNavigate(dataReader, code, time);
            else
            {
                if (dataNavigate_Code.Code != code)
                    dataNavigate_Code = DataNavigateFactory.CreateDataNavigate(dataReader, code, time);
            }
            dataNavigate_Code.NavigateTo(time);
            this.dataPackage = dataNavigate_Code.DataPackage;
            return dataNavigate_Code;
        }

        private IHistoryDataForward_Code GetHistoryDataForward_Playing()
        {
            if (this.dataReader == null)
                return null;
            if (historyDataForward_CodePlaying == null)
            {
                StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
                referedPeriods.UseTickData = true;
                referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
                referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

                ForwardPeriod fp = new ForwardPeriod(true, KLinePeriod.KLinePeriod_1Minute);

                this.historyDataForward_CodePlaying = HistoryDataForwardFactory.CreateHistoryDataForward_Code(this.DataPackage, referedPeriods, fp);
                //HistoryDataForwardArguments args = new HistoryDataForwardArguments();
                //int date = dataReader.CreateTradingSessionReader(code).GetTradingDay(time);
                //args.StartDate = date;
                //args.EndDate = date;
                //args.IsTickForward = true;
                //args.ReferedPeriods = new strategy.StrategyReferedPeriods();
                //args.ReferedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
                //args.ReferedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);

                //this.historyDataForward_CodePlaying = HistoryDataForwardFactory.CreateHistoryDataForward_Code(dataReader, code, args);
                this.historyDataForward_CodePlaying.NavigateTo(time);
                this.historyDataForward_CodePlaying.OnTick += HistoryDataForward_Code_OnTick;                
            }
            else
            {
                if (!isPlaying)
                    this.historyDataForward_CodePlaying.NavigateTo(time);
            }
            return historyDataForward_CodePlaying;
        }

        private void HistoryDataForward_Code_OnTick(object sender, ITickData tickData, int index)
        {
            this.IsDataRefresh = true;
            if (tickData != null)
                this.time = tickData.Time;
        }

        private bool isPlaying;

        public void Play()
        {
            if (isPlaying)
                return;
            IHistoryDataForward_Code historyDataForward = GetHistoryDataForward_Playing();
            historyDataForward.Play();
            isPlaying = true;
        }

        public void Pause()
        {
            if (!isPlaying)
                return;
            IHistoryDataForward_Code historyDataForward = GetHistoryDataForward_Playing();
            historyDataForward.Pause();
            isPlaying = false;
        }

        private ForwardPeriod forwardPeriod = new ForwardPeriod(false, KLinePeriod.KLinePeriod_1Minute);

        public void ForwardTime()
        {
            IDataNavigate_Code nav = GetDataNavigate_Code();
            bool canForward = nav.Forward(forwardPeriod.KlineForwardPeriod);
            if (canForward)
                this.Time = nav.Time;
            //IsDataRefresh = true;
        }

        public void BackwardTime()
        {
            IDataNavigate_Code nav = GetDataNavigate_Code();
            bool canBackward = nav.Backward(forwardPeriod.KlineForwardPeriod);
            if (canBackward)
                this.Time = nav.Time;
            //IsDataRefresh = true;
        }

        //private bool isForwarding;

        //private IHistoryDataForward_Code GetHistoryDataForward_CodeForward()
        //{
        //    return historyDataForward_CodeForward;
        //}

        public ForwardPeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }

            set
            {
                if (forwardPeriod == value)
                    return;
                forwardPeriod = value;
            }
        }

        public bool IsDataRefresh
        {
            get { return isDataRefresh; }
            set
            {
                this.isDataRefresh = value;
                if (this.isDataRefresh && OnDataRefresh != null)
                    OnDataRefresh(this, new DataRefreshArgument());
            }
        }

        public event DelegateOnDataRefresh OnDataRefresh;

        public IGraphicData_CurrentInfo CurrentInfo
        {
            get { return null; }
        }

        public IDataPackage DataPackage
        {
            get
            {
                return dataPackage;
            }
        }
    }

    public delegate void DelegateOnDataRefresh(object sender, DataRefreshArgument arg);

    public class DataRefreshArgument
    {

    }
}