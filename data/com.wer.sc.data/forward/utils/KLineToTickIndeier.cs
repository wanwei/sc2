using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    public class KLineToTickIndeier
    {
        private ITickData tickData;

        private IKLineData_Extend klineData;

        private Dictionary<int, int> dic_TickIndex_KLineIndex = new Dictionary<int, int>();

        private Dictionary<int, int> dic_KLineStart_KLineEnd = new Dictionary<int, int>();

        public KLineToTickIndeier(ITickData tickData, IKLineData_Extend klineData)
        {
            this.tickData = tickData;
            this.klineData = klineData;
            Index();
        }

        private void Index()
        {
            this.dic_TickIndex_KLineIndex.Clear();
            this.dic_KLineStart_KLineEnd.Clear();
            int tradingDay = tickData.TradingDay;
            int startIndex = klineData.GetTradingDayStartIndex(tradingDay);
            int endIndex = klineData.GetTradingDayEndIndex(tradingDay);
            int barPos = startIndex;

            double endKLineTime = GetEndTime(klineData, barPos);
            double lastTime = 0;
            for (int i = 0; i < tickData.Length; i++)
            {
                double time = tickData.Arr_Time[i];
                if (lastTime < endKLineTime && time >= endKLineTime)
                {
                    dic_TickIndex_KLineIndex.Add(i - 1, barPos);
                    barPos++;
                    if (barPos >= klineData.Length - 1)
                    {
                        dic_TickIndex_KLineIndex.Add(tickData.Length - 1, barPos);
                        return;
                    }
                    endKLineTime = GetEndTime(klineData, barPos);
                    if (endKLineTime < time)
                    {
                        int startBarPos = barPos;
                        while (endKLineTime < time)
                        {
                            barPos++;
                            if (barPos >= klineData.Length - 1)
                            {
                                dic_TickIndex_KLineIndex.Add(tickData.Length - 1, barPos - 1);
                                return;
                            }
                            endKLineTime = GetEndTime(klineData, barPos);
                        }
                        int currentBarPos = barPos - 1;
                        if (endKLineTime == time)
                        {
                            currentBarPos++;
                            barPos++;
                            endKLineTime = GetEndTime(klineData, barPos);
                        }
                        dic_KLineStart_KLineEnd.Add(startBarPos - 1, currentBarPos);
                    }
                }
                lastTime = time;
            }
            dic_TickIndex_KLineIndex.Add(tickData.Length - 1, barPos);
        }

        private static double GetEndTime(IKLineData_Extend klineData, int barPos)
        {
            if (klineData.Period.PeriodType >= KLineTimeType.DAY)
                return klineData.Time;
            double endTime = klineData.GetEndTime(barPos);
            if (barPos < klineData.Length - 1 && klineData.IsTradingTimeEnd(barPos))
            {
                endTime = (endTime + klineData.Arr_Time[barPos + 1]) / 2;
            }
            return endTime;
        }

        public void ChangeTradingDay(ITickData tickData)
        {
            this.tickData = tickData;
            Index();
        }

        public void ChangeTradingDay(ITickData tickData, IKLineData_Extend klineData)
        {
            this.tickData = tickData;
            this.klineData = klineData;
            Index();
        }

        public int GetKLineBarPosIfFinished(int tickBarPos, out int lastBarPos)
        {
            lastBarPos = -1;
            if (dic_TickIndex_KLineIndex.ContainsKey(tickBarPos))
            {
                int klineIndex = dic_TickIndex_KLineIndex[tickBarPos];
                if (dic_KLineStart_KLineEnd.ContainsKey(klineIndex))
                {
                    lastBarPos = dic_KLineStart_KLineEnd[klineIndex];
                }
                else
                    lastBarPos = klineIndex;
                return klineIndex;
            }
            return -1;
        }
    }
}
