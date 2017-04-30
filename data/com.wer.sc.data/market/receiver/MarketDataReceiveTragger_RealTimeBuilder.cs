using com.wer.sc.data.datacenter;
using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    public class MarketDataReceiveTragger_RealTimeBuilder : IMarketDataReceiveTragger
    {
        private Dictionary<string, RealTimeDataReceiver_Code> dic_Code_RealTimeData = new Dictionary<string, RealTimeDataReceiver_Code>();

        private List<KLinePeriod> klinePeriods;

        private IDataReader dataReader;

        private int currentTradingDay;

        public MarketDataReceiveTragger_RealTimeBuilder(IDataReader dataReader, List<KLinePeriod> klinePeriods)
        {
            this.klinePeriods = klinePeriods;
            this.dataReader = dataReader;
        }

        public void TickDataReceived(object sender, ITickData tickData)
        {
            string code = tickData.Code;

            RealTimeDataReceiver_Code realTimeDataReceiver;
            if (dic_Code_RealTimeData.ContainsKey(code))
            {
                realTimeDataReceiver = dic_Code_RealTimeData[code];
                realTimeDataReceiver.Receive(tickData);
            }
            else
            {
                IMarketData marketData = ((IMarketData)sender);
                List<double[]> openTime = marketData.GetTradingSession(code, currentTradingDay);
                if (openTime == null)
                    throw new ApplicationException(code + "-" + currentTradingDay + "没有配置开盘时间");
                realTimeDataReceiver = new RealTimeDataReceiver_Code(code, currentTradingDay, dataReader, klinePeriods, openTime);
                dic_Code_RealTimeData.Add(code, realTimeDataReceiver);
            }
            if (RealTimeDataChanged != null)
                RealTimeDataChanged(this, realTimeDataReceiver);
        }

        public void TradingDayChanged(int currentTradingDay)
        {
            dic_Code_RealTimeData.Clear();
            this.currentTradingDay = currentTradingDay;
        }

        public IRealTimeDataReader GetRealTimeDataReader(string code)
        {
            RealTimeDataReceiver_Code realTimeData;
            if (dic_Code_RealTimeData.TryGetValue(code, out realTimeData))
                return realTimeData;
            return null;
        }

        public event DelegateOnRealTimeDataChanged RealTimeDataChanged;
    }

    /// <summary>
    /// 返回合约信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="instruments"></param>
    public delegate void DelegateOnRealTimeDataChanged(object sender, IRealTimeDataReader realTimeDataReader);
}
