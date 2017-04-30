using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    /// <summary>
    /// 实时数据获取器
    /// 该接口负责从服务器获取数据并存入目录
    /// 
    /// </summary>
    public class DataReceiver : IDataReceiver2
    {
        private IPlugin_Market currentMarket;

        private LogUtils logUtils = new LogUtils();

        private DataReceiver_Tick tickDataReceiver;// = new TickBarWriter();

        //当前连接
        private ConnectionInfo currentConnection;

        private List<plugin.market.InstrumentInfo> instruments = new List<plugin.market.InstrumentInfo>();

        private IPlugin_MarketData plugin_MarketData;

        private IPlugin_MarketTrader plugin_MarketTrader;

        private LoginInfo marketDataLoginInfo;

        private LoginInfo marketTraderLoginInfo;

        private string path;

        private string marketPath;

        private int tradingDay;

        private bool subscribeAll = false;

        private DelegateOnReceiverPrepared onReceiverPrepared;

        public LogUtils LogUtils
        {
            get
            {
                return logUtils;
            }
        }

        public DataReceiver_Tick TickDataReceiver
        {
            get
            {
                return tickDataReceiver;
            }
        }

        public DelegateOnReceiverPrepared OnReceiverPrepared
        {
            get
            {
                return onReceiverPrepared;
            }

            set
            {
                onReceiverPrepared = value;
            }
        }

        public DelegateOnConnectionStatus OnMarketDataConnectionStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public DelegateOnConnectionStatus OnMarketTraderConnectionStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 默认保存在D:\SCPRESENT目录里
        /// </summary>
        /// <param name="plugin_Market"></param>
        public DataReceiver(IPlugin_Market plugin_Market) : this(plugin_Market, @"D:\SCPRESENT")
        {

        }

        public DataReceiver(IPlugin_Market plugin_Market, string path)
        {
            this.path = path;
            this.currentMarket = plugin_Market;
            this.plugin_MarketData = plugin_Market.MarketData;
            this.plugin_MarketTrader = plugin_Market.MarketTrader;

            this.plugin_MarketData.OnConnectionStatus = OnConnectionStatus_MarketData;
            this.plugin_MarketData.OnReturnMarketData = OnReturnMarketData;

            plugin_MarketTrader.OnConnectionStatus = OnConnectionStatus_MarketTrader;
            plugin_MarketTrader.OnReturnInstruments = OnReturnInstrument;
        }

        /// <summary>
        /// 构建数据接收器
        /// </summary>
        /// <param name="plugin_Market"></param>
        /// <param name="path"></param>
        /// <param name="subscribeAll"></param>
        public DataReceiver(IPlugin_Market plugin_Market, string path, bool subscribeAll) : this(plugin_Market, path)
        {
            this.subscribeAll = subscribeAll;
        }

        private void OnConnectionStatus_MarketTrader(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            this.marketTraderLoginInfo = userLogin;
            logUtils.WriteLog("登陆交易服务器：" + status);
            logUtils.WriteLog("用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);

            //登陆后访问
            if (status == ConnectionStatus.Logined)
            {
                this.tradingDay = this.marketTraderLoginInfo.TradingDay;
                currentMarket.MarketTrader.QueryInstruments();
            }
        }

        public void OnConnectionStatus_MarketData(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            this.marketDataLoginInfo = userLogin;
            logUtils.WriteLog("登陆数据服务器：" + status);
            logUtils.WriteLog("用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);

            if (status == ConnectionStatus.Logined)
            {
                //登陆成功Beep
                System.Media.SystemSounds.Beep.Play();

                Type type = currentMarket.GetType();
                string[] nameDescArr = PluginAssembly.GetPluginNameDesc(type);
                //如果交易日期改了，需要重新创建一个tickbarwriter，否则程序隔夜跑的时候
                int date = marketDataLoginInfo.TradingDay;
                if (this.tickDataReceiver == null || this.tickDataReceiver.Date != date)
                {
                    //TODO tickbarwriter需要flush一下
                    this.marketPath = path + "\\" + nameDescArr[0] + "\\";
                    this.tickDataReceiver = new DataReceiver_Tick(marketPath, marketDataLoginInfo.TradingDay);
                    if (onReceiverPrepared != null)
                        onReceiverPrepared(this, tickDataReceiver);
                }
            }
        }

        private void OnReturnInstrument(object sender, ref List<plugin.market.InstrumentInfo> instruments)
        {
            WriteInstruments(instruments);
            if (subscribeAll)
                Subscribe(GetCodes(instruments));
        }

        private void WriteInstruments(List<plugin.market.InstrumentInfo> instruments)
        {
            instruments.Sort(delegate (plugin.market.InstrumentInfo a, plugin.market.InstrumentInfo b) { return a.Symbol.CompareTo(b.Symbol); });
            SaveInstruments(instruments);
        }

        private void SaveInstruments(List<plugin.market.InstrumentInfo> instruments)
        {
            JsonUtils_Instrument.Save(marketPath + "Instruments_" + this.tradingDay + ".json", instruments);
        }

        private List<plugin.market.InstrumentInfo> LoadInstruments()
        {
            return JsonUtils_Instrument.Load(marketPath + "Instruments_" + this.tradingDay + ".json");
        }

        private void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            this.tickDataReceiver.Receive(marketData);
        }

        public void Subscribe(string[] codes)
        {
            this.currentMarket.MarketData.Subscribe(codes);
        }

        private string[] GetCodes(List<plugin.market.InstrumentInfo> instruments)
        {
            int subCount = instruments.Count;
            string[] codes = new string[subCount];
            for (int i = 0; i < subCount; i++)
            {
                plugin.market.InstrumentInfo instrument = instruments[i];
                codes[i] = instrument.Symbol;
            }
            return codes;
        }

        public void UnSubscribe(string[] codes)
        {
            this.currentMarket.MarketData.UnSubscribe(codes);
        }

        /// <summary>
        /// 得到所有连接
        /// </summary>
        /// <returns></returns>
        public List<ConnectionInfo> GetAllConnections()
        {
            return this.currentMarket.MarketData.GetAllConnections();
        }

        public void Connect(ConnectionInfo connectionInfo)
        {
            DisConnect();
            this.currentConnection = connectionInfo;
            if (currentMarket != null)
            {
                currentMarket.MarketData.Connect(currentConnection);
                currentMarket.MarketTrader.Connect(currentConnection);
            }
        }

        public void SubscribeAll()
        {
            throw new NotImplementedException();
        }

        public void DisConnect()
        {
            if (currentMarket != null)
            {
                currentMarket.MarketData.DisConnect();
                currentMarket.MarketTrader.DisConnect();
            }
        }

        public void ReConnect()
        {
            if (currentMarket != null)
            {
                currentMarket.MarketData.DisConnect();
                currentMarket.MarketTrader.DisConnect();
                currentMarket.MarketData.Connect(currentConnection);
                currentMarket.MarketTrader.Connect(currentConnection);
            }
        }

    }

    /// <summary>
    /// 委托，接收器准备好
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="dataReceiver_Tick"></param>
    public delegate void DelegateOnReceiverPrepared(object sender, DataReceiver_Tick dataReceiver_Tick);
}
