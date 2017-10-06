using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class KLineData_DaySplitter
    {

        private int currentIndex = -1;

        private List<SplitterResult> results;

        public KLineData_DaySplitter(IKLineData klineData, ITradingTimeReader_Code tradingSessionReader)
        {
            this.results = DaySplitter.Split(klineData, tradingSessionReader);
        }

        private bool isEnd = false;

        public bool NextDay()
        {
            if (isEnd)
                return false;
            currentIndex++;
            if (currentIndex >= results.Count - 1)
            {
                isEnd = true;
            }
            return true;
        }

        public int CurrentDay
        {
            get { return results[currentIndex].Date; }
        }

        public int CurrentDayKLineIndex
        {
            get
            {
                return results[currentIndex].Index;
            }
        }
    }
}