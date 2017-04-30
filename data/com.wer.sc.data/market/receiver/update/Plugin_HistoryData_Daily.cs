using com.wer.sc.data.datacenter;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.historydata;

namespace com.wer.sc.data.market.receiver
{
    public class Plugin_HistoryData_Daily : IPlugin_HistoryData
    {
        private IDataReader dataReader;

        private string path;

        private int date;

        private TickData_RealReader realReader;

        public Plugin_HistoryData_Daily(DataCenter datacenter, string path, int date)
        {
            this.realReader = new TickData_RealReader(path, date);
            this.dataReader = datacenter.DataReader;
        }

        public List<CodeInfo> GetInstruments()
        {
            throw new NotImplementedException();
        }

        public IKLineData GetKLineData(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            List<double> klineTimeList =new List<double>();
            float yesterdayEndPrice = 0f;
            int yesterdayEndHold = 0;
            return DataTransfer_Tick2KLine.Transfer(GetTickData(code, date), klineTimeList, yesterdayEndPrice, yesterdayEndHold);
        }

        public ITickData GetTickData(string code, int date)
        {
            return this.realReader.ReadTickData(code);
        }

        public List<int> GetTradingDays()
        {
            List<int> tradingDays = new List<int>();
            tradingDays.Add(date);
            return tradingDays;
        }

        public List<TradingSession> GetTradingSessions(string code)
        {
            throw new NotImplementedException();
        }

        public IPlugin_DataUpdater GetHistoryDataPreparer()
        {
            throw new NotImplementedException();
        }
    }
}