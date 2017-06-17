using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    public class KLineStrategyRunner_History
    {
        private IDataReader dataReaderFactory;

        public KLineStrategyRunner_History(IDataReader dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public void Run(string code, int dataStartDate, int startDate, int endDate, IStrategy strategy)
        {
            //Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

            //RealTimeDataNavigateForward_Tick realTimeDataReader = new RealTimeDataNavigateForward_Tick(dataReaderFactory, code, startDate, endDate, dicKLineData);
            //while (realTimeDataReader.NavigateForward(1))
            //{
            //    strategy.OnBar(realTimeDataReader);
            //}
        }
    }
}
