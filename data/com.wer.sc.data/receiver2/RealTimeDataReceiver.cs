using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2
{
    /// <summary>
    /// 实时数据接收器
    /// </summary>
    public class RealTimeDataReceiver
    {
        private IPlugin_Market plugin_Market;

        private DataReceiver dataReceiver;

        private DataReceiver_Tick dataReceiver_Tick;

        private DataReaderFactory2 dataReaderFactory;

        private int lastOpenDate;

        private String[] receiveCodes;

        private List<KLinePeriod> klinePeriods;

        private Dictionary<string, RealTimeDataReceiver_Code> dic_Code_Receive = new Dictionary<string, RealTimeDataReceiver_Code>();

        /// <summary>
        /// 实时数据接收器构造函数
        /// </summary>
        /// <param name="todayDataPath">今天</param>
        /// <param name="plugin_Market"></param>
        /// <param name="plugin_HistoryData"></param>
        /// <param name="receiveCodes"></param>
        /// <param name="klinePeriods"></param>
        public RealTimeDataReceiver(string todayDataPath, IPlugin_Market plugin_Market, IPlugin_HistoryData plugin_HistoryData, String[] receiveCodes, List<KLinePeriod> klinePeriods)
        {
            Init(todayDataPath, plugin_Market, plugin_HistoryData, -1, receiveCodes, klinePeriods);
        }

        public RealTimeDataReceiver(string todayDataPath, IPlugin_Market plugin_Market, IPlugin_HistoryData plugin_HistoryData, int lastOpenDate, String[] receiveCodes, List<KLinePeriod> klinePeriods)
        {
            Init(todayDataPath, plugin_Market, plugin_HistoryData, lastOpenDate, receiveCodes, klinePeriods);
        }

        private void Init(string todayDataPath, IPlugin_Market plugin_Market, IPlugin_HistoryData plugin_HistoryData, int lastOpenDate, string[] receiveCodes, List<KLinePeriod> klinePeriods)
        {
            this.plugin_Market = plugin_Market;
            this.receiveCodes = receiveCodes;
            this.klinePeriods = klinePeriods;
            //初始化历史数据
            this.dataReaderFactory = new DataReaderFactory2("");
            if (lastOpenDate < 0)
                this.lastOpenDate = this.dataReaderFactory.OpenDateReader.LastTradingDay;
            else
                this.lastOpenDate = lastOpenDate;
            //初始化数据接收器
            this.dataReceiver = new DataReceiver(plugin_Market, todayDataPath);
            this.dataReceiver.Subscribe(receiveCodes);
            this.dataReceiver.OnReceiverPrepared = this.onReceiverPrepared;
        }

        private void onReceiverPrepared(object sender, DataReceiver_Tick dataReceiver_Tick)
        {
            this.clearCurrentDataReceiver();

            this.dataReceiver_Tick = dataReceiver_Tick;
            this.dataReceiver_Tick.OnTickDataReceived = onTickDataReceived;

            int date = dataReceiver_Tick.Date;
            foreach (string code in receiveCodes)
            {
                dic_Code_Receive.Add(code, new RealTimeDataReceiver_Code(code, date, dataReaderFactory, lastOpenDate, klinePeriods, plugin_Market.MarketData.GetTradingSession(code, date)));
            }
        }

        private void clearCurrentDataReceiver()
        {
            if (this.dataReceiver_Tick != null)
            {
                this.dataReceiver_Tick.OnTickDataReceived = null;
                this.dataReceiver_Tick = null;
            }
            this.dic_Code_Receive.Clear();
        }

        private void onTickDataReceived(object sender, ITickData tickData, ITickBar tickBar)
        {
            RealTimeDataReceiver_Code receiver;
            bool b = dic_Code_Receive.TryGetValue(tickData.Code, out receiver);
            if (b)
            {
                receiver.Receive(tickData, tickBar);
            }
        }

        private DelegateOnRealTimeDataProceed onRealTimeDataProceed;

        public DelegateOnRealTimeDataProceed OnRealTimeDataProceed
        {
            get
            {
                return onRealTimeDataProceed;
            }

            set
            {
                onRealTimeDataProceed = value;
            }
        }
    }

    public delegate void DelegateOnRealTimeDataProceed(object sender, string code, IRealTimeDataReader currentData);
}
