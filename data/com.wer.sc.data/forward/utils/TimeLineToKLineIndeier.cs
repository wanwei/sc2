using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    public class TimeLineToKLineIndeier
    {
        private IKLineData klineData;

        private ITimeLineData timeLineData;

        private Dictionary<int, int> dic_KLineIndex_TimeLineIndex = new Dictionary<int, int>();

        public TimeLineToKLineIndeier(IKLineData klineData, ITimeLineData timeLineData)
        {
            this.klineData = klineData;
            this.timeLineData = timeLineData;
            this.Index();
        }

        private void Index()
        {
            dic_KLineIndex_TimeLineIndex.Clear();
            int currentBarPos = klineData.BarPos;
            for (int i = 0; i < timeLineData.Length; i++)
            {
                double time = timeLineData.Arr_Time[i];
                int index = FindBarIndex(klineData, time, currentBarPos);
                if (index < 0)
                    continue;
                currentBarPos = index;
                dic_KLineIndex_TimeLineIndex.Add(currentBarPos, i);
            }
        }

        private int FindBarIndex(IKLineData klineData, double time, int currentIndex)
        {
            double klineTime;
            while (currentIndex < klineData.Length)
            {
                klineTime = klineData.Arr_Time[currentIndex];
                if (klineTime == time)
                    return currentIndex;
                if (klineTime > time)
                    return -1;
                currentIndex++;
            }
            return -1;
        }

        public void ChangeTradingDay(ITimeLineData timeLineData)
        {
            this.timeLineData = timeLineData;
            Index();
        }

        public void ChangeTradingDay(IKLineData klineData, ITimeLineData timeLineData)
        {
            this.klineData = klineData;
            this.timeLineData = timeLineData;
            Index();
        }

        public int GetTimeLineBarPosIfFinished(int klineBarPos)
        {
            if (dic_KLineIndex_TimeLineIndex.ContainsKey(klineBarPos))
                return dic_KLineIndex_TimeLineIndex[klineBarPos];
            return -1;
        }
    }
}