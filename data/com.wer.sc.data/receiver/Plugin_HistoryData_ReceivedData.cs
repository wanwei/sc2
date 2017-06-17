using com.wer.sc.data.market;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    public class Plugin_HistoryData_ReceivedData : IPlugin_HistoryData
    {
        public List<InstrumentInfo> GetInstruments()
        {
            throw new NotImplementedException();
        }

        public string GetDataCenterUri()
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

        public List<TradingSession> GetTradingSessions(string code)
        {
            throw new NotImplementedException();
        }

        List<CodeInfo> IPlugin_HistoryData.GetInstruments()
        {
            throw new NotImplementedException();
        }
    }
}
