using com.wer.sc.data.datacenter;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    /// <summary>
    /// 实时数据接收器
    /// 数据接收器可以完成以下工作
    /// 1.接收tick数据
    /// 2.提供IRealTimeData的侦听，供策略执行使用
    /// 3.将数据持久化到硬盘上，并且能够快速获取已经持久化的数据
    /// 4.收盘后将数据更新到数据中心
    /// 
    /// 以上除了接收tick数据都可以自行选择是否执行。
    /// 
    /// 一个问题：
    /// 数据接收后持久化到硬盘的工作不应该影响主体的数据接收工作
    /// 要多考虑下持久化的容错性
    /// </summary>
    public class RealDataReceiver
    {
        private bool isRunning = false;

        private IMarket market;
        private MarketType marketType;
        private ConnectionInfo marketDataConnectionInfo;
        private ConnectionInfo marketTraderConnectionInfo;

        private bool enableRealTimeReader;
        private DataCenter realTimeReader_DataCenter;
        private List<KLinePeriod> realTimeReader_KlinePeriods;
        private MarketDataReceiveTragger_RealTimeBuilder realTimeReader_Builder;

        private bool enableDataPersistent;
        //private MarketDataReceiveTragger_Writer dataPersistentWriter;
        private string dataPersistentWriter_Path;
        private int dataPersistentWriter_WriteInterval;
        private RealDataPersistent realDataPersistent;

        public RealDataReceiver(MarketType marketType, ConnectionInfo marketDataConnectionInfo)
        {
            this.marketType = marketType;
            this.marketDataConnectionInfo = marketDataConnectionInfo;
        }

        public RealDataReceiver(MarketType marketType, ConnectionInfo marketDataConnectionInfo, ConnectionInfo marketTraderConnectionInfo)
        {
            this.marketType = marketType;
            this.marketDataConnectionInfo = marketDataConnectionInfo;
            this.marketTraderConnectionInfo = marketTraderConnectionInfo;
        }

        /// <summary>
        /// 运行接收器
        /// </summary>
        public void Start()
        {
            if (isRunning)
                return;
            isRunning = true;

            MarketFactory fac = new MarketFactory(marketType);
            market = fac.CreateMarket();
            market.MarketData.DataReceived += MarketData_DataReceived;

            if (enableRealTimeReader)
            {
                this.realTimeReader_Builder = new MarketDataReceiveTragger_RealTimeBuilder(realTimeReader_DataCenter.DataReader, realTimeReader_KlinePeriods);
                this.realTimeReader_Builder.RealTimeDataChanged += RealTimeReader_Builder_RealTimeDataChanged;
                this.market.MarketData.Traggers.Add(realTimeReader_Builder);
            }
            if (enableDataPersistent)
            {
                this.realDataPersistent = new RealDataPersistent(market, dataPersistentWriter_Path, dataPersistentWriter_WriteInterval);
                this.realDataPersistent.Start();
            }
            market.MarketData.Connect(marketDataConnectionInfo);
            if (this.marketTraderConnectionInfo != null)
            {
                this.market.MarketTrader.Connect(marketTraderConnectionInfo);
            }
        }

        private void RealTimeReader_Builder_RealTimeDataChanged(object sender, IRealTimeDataReader realTimeDataReader)
        {
            if (RealTimeDataChanged != null)
                RealTimeDataChanged(this, realTimeDataReader);
        }

        private void MarketData_DataReceived(object sender, ITickData tickData)
        {
            if (TickDataReceived != null)
                TickDataReceived(this, tickData);
        }

        public void Stop()
        {
            if (!isRunning)
                return;
            if (market != null)
            {
                market.MarketData.DisConnect();
                market.MarketData.Traggers.Clear();
                if (this.realDataPersistent != null)
                    this.realDataPersistent.Stop();
            }
            isRunning = false;
        }

        #region 开关

        /// <summary>
        /// 使接收器可以接收RealTimeData数据，通过事件RealTimeDataChanged
        /// 该方法必须在开始接收数据前调用
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <param name="klinePeriods"></param>
        public void EnableRealTimeReader(DataCenter dataCenter, List<KLinePeriod> klinePeriods)
        {
            this.realTimeReader_DataCenter = dataCenter;
            this.realTimeReader_KlinePeriods = klinePeriods;
            this.enableRealTimeReader = true;
        }

        public void DisableRealTimeReader()
        {
            this.realTimeReader_DataCenter = null;
            this.realTimeReader_KlinePeriods = null;
            this.enableRealTimeReader = false;
        }

        /// <summary>
        /// 使接收器可以将接收到的数据持久化
        /// 该方法必须在开始接收数据前调用
        /// </summary>
        /// <param name="path"></param>
        public void EnableDataPersistent(string path, int writerInterval)
        {
            this.dataPersistentWriter_Path = path;
            this.dataPersistentWriter_WriteInterval = writerInterval;
            this.enableDataPersistent = true;
        }

        public void DisableDataPersistent()
        {
            this.dataPersistentWriter_Path = null;
            this.dataPersistentWriter_WriteInterval = -1;
            this.enableDataPersistent = false;
        }

        //private bool enableAutoUpdate;

        //private string updateDataCenterUri;

        ///// <summary>
        ///// 使
        ///// </summary>
        ///// <param name="dataCenterUri"></param>
        //public void EnableAutoUpdate(string dataCenterUri)
        //{
        //    this.enableAutoUpdate = true;
        //    this.updateDataCenterUri = dataCenterUri;
        //}

        //public void DisableAutoUpdate()
        //{
        //    this.enableAutoUpdate = false;
        //}

        #endregion

        /// <summary>
        /// tick数据接收事件
        /// </summary>
        public event DelegateOnDataReceived TickDataReceived;

        /// <summary>
        /// 实时数据变化事件
        /// </summary>
        public event DelegateOnRealTimeDataChanged RealTimeDataChanged;
    }

    /// <summary>
    /// 持久化保存实时接收的数据
    /// 保存路径：
    /// PATH
    ///     --20170108
    ///         instrument_20170108.csv
    ///         --tick
    ///             m01.csv
    ///             m03.csv
    ///             m05.csv
    ///             ......
    ///     --20170109
    ///     ......
    /// </summary>
    public class RealDataPersistent
    {
        private IMarket market;

        private int tradingDay;

        private MarketDataReceiveTragger_TickWriter dataPersistentWriter;
        private string dataPersistentWriter_Path;
        private int dataPersistentWriter_WriteInterval;

        public RealDataPersistent(IMarket market, string path, int writeInterval)
        {
            this.market = market;
            this.dataPersistentWriter_Path = path;
            this.dataPersistentWriter_WriteInterval = writeInterval;
            this.dataPersistentWriter = new MarketDataReceiveTragger_TickWriter(dataPersistentWriter_Path, dataPersistentWriter_WriteInterval);
        }

        public void Start()
        {
            this.market.MarketData.Traggers.Add(dataPersistentWriter);
            this.market.MarketTrader.ConnectionStatusChanged += MarketTrader_ConnectionStatusChanged;
            this.market.MarketTrader.InstrumentsReturned += MarketTrader_InstrumentsReturned;
        }

        public void Stop()
        {
            this.market.MarketData.Traggers.Remove(dataPersistentWriter);
            this.market.MarketTrader.ConnectionStatusChanged -= MarketTrader_ConnectionStatusChanged;
            this.market.MarketTrader.InstrumentsReturned -= MarketTrader_InstrumentsReturned;
        }

        private void MarketTrader_ConnectionStatusChanged(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            if (status == ConnectionStatus.Logined)
            {
                this.tradingDay = userLogin.TradingDay;
                this.market.MarketTrader.QueryInstruments();
            }
        }

        private void MarketTrader_InstrumentsReturned(object sender, ref List<InstrumentInfo> instruments)
        {
            string path = this.dataPersistentWriter_Path + "\\" + tradingDay + "\\instrument_" + tradingDay + ".csv";
            CsvUtils_Instrument.Save(path, instruments);
        }
    }
}