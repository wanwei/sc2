using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DayMinuteKLineDataGetter
    {
        private IKLineData klineData;

        private List<int> openDates = new List<int>();
        private Dictionary<int, int[]> dicDateStartEnd = new Dictionary<int, int[]>();

        private ITradingDayReader openDateReader;

        public DayMinuteKLineDataGetter(IKLineData klineData, ITradingDayReader openDateReader)
        {
            this.klineData = klineData;
            this.openDateReader = openDateReader;
            this.initIndex();
        }

        private void initIndex()
        {
            //KLineTimeGetter timeGetter = new KLineTimeGetter(klineData);

            //List<SplitterResult> splitResults = DaySplitter.Split(timeGetter, openDateReader);
            //for (int i = 0; i < splitResults.Count; i++)
            //{
            //    SplitterResult result = splitResults[i];
            //    openDates.Add(result.Date);
            //    int start = result.Index;
            //    int end = (i == splitResults.Count - 1) ? timeGetter.Count - 1 : splitResults[i + 1].Index;
            //    dicDateStartEnd.Add(result.Date, new int[] { start, end });
            //}
        }

        public IKLineData GetMinuteKLineData(int date)
        {
            if (!dicDateStartEnd.ContainsKey(date))
                return null;
            int[] startEnd = dicDateStartEnd[date];
            return new KLineData_Sub(klineData, startEnd[0], startEnd[1]);
        }

        public float LastEndPrice(int date)
        {
            if (!dicDateStartEnd.ContainsKey(date))
                return -1;
            int[] startEnd = dicDateStartEnd[date];
            int lastEndIndex = startEnd[0];
            int index = lastEndIndex - 1;
            if (index < 0)
                return klineData.Arr_Start[0];
            return klineData.Arr_End[index];
        }

        public List<int> OpenDates
        {
            get
            {
                return openDates;
            }
        }
    }
}
