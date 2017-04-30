using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class KLineDataGetter_FromTick
    {
        private DataPathUtils utils;
        private DataReaderFactory dataReaderFactory;
        private Plugin_DataProvider dataProvider;

        public KLineDataGetter_FromTick(DataReaderFactory dataReaderFactory, Plugin_DataProvider dataProvider)
        {
            this.utils = new DataPathUtils(dataProvider.GetDataPath());
            this.dataReaderFactory = dataReaderFactory;
            this.dataProvider = dataProvider;
        }

        public IKLineData GetKLineData(string code, int startDate, int endDate, KLinePeriod period)
        {
            ITradingDayReader openDateReader = dataReaderFactory.OpenDateReader;

            IList<int> openDates = openDateReader.GetOpenDates(startDate, endDate);
            int prevOpenDate = openDateReader.GetPrevOpenDate(startDate);
            IKLineData lastKLineData = dataReaderFactory.KLineDataReader.GetData(code, prevOpenDate, prevOpenDate, period);
            List<IKLineData> klineDataList = new List<IKLineData>();            
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                TickData tickdata = dataReaderFactory.TickDataReader.GetTickData(code, openDate);
                if (tickdata != null)
                {
                    List<double[]> openTimes = dataProvider.GetOpenTime(code, openDate);
                    OpenTimeUtilsArgs args = new OpenTimeUtilsArgs(openDate, openDateReader, openTimes, period);
                    KLineData klineData = DataTransfer_Tick2KLine2.Transfer(tickdata, lastKLineData, args);
                    klineDataList.Add(klineData);
                }
            }
            if (klineDataList.Count == 0)
                return null;
            return KLineData.Merge(klineDataList);
        }
    }
}
