using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.plugin.data;

namespace com.wer.sc.plugin.mock.historydata
{
    [Plugin("MOCK.HISTORYDATA.SINA", "模拟历史数据，新浪数据", "模拟历史数据，模拟取新浪数据，测试专用", MarketType.CnStock)]
    public class Plugin_HistoryData_Mock_Sina : IPlugin_HistoryData
    {
        public List<CodeInfo> GetInstruments()
        {
            throw new NotImplementedException();
        }

        public string GetDataCenterUri()
        {
            throw new NotImplementedException();
        }

        public List<TradingSession> GetTradingSessions(string code)
        {
            throw new NotImplementedException();
        }

        public IKLineData GetKLineData(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            throw new NotImplementedException();
        }

        public NeedsToUpdate GetNeedsToUpdate()
        {
            throw new NotImplementedException();
        }

        public List<int> GetTradingDays()
        {
            throw new NotImplementedException();
        }

        public ITickData GetTickData(string code, int date)
        {
            throw new NotImplementedException();
        }

        public TradingTime GetDefaultTradingTime()
        {
            throw new NotImplementedException();
        }

        public TradingTime GetTradingTime(string code, int tradingDay)
        {
            throw new NotImplementedException();
        }

        public IList<TradingTime> GetTradingTime(string code)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetTickDataDays(string code)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetKLineDataDays(string code)
        {
            throw new NotImplementedException();
        }

        public IList<MainContractInfo> GetMainContractInfos()
        {
            throw new NotImplementedException();
        }
    }
}
