using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    /// <summary>
    /// 
    /// </summary>
    public class KLineToKLineIndeier
    {
        private IKLineData_Extend mainKLine;

        private IList<IKLineData_Extend> indexKLines;

        private Dictionary<KLinePeriod, int> dic_Period_CurrentIndex = new Dictionary<KLinePeriod, int>();

        private Dictionary<KLinePeriod, Dictionary<int, int>> dic_Period_PosIndex = new Dictionary<KLinePeriod, Dictionary<int, int>>();

        public KLineToKLineIndeier(IKLineData_Extend mainKLine, IList<IKLineData_Extend> indexKLines)
        {
            this.mainKLine = mainKLine;
            this.indexKLines = indexKLines;
            this.Index();
        }

        private void Index()
        {
            for (int i = mainKLine.BarPos + 1; i < mainKLine.Length; i++)
            {
                if (i == mainKLine.Length - 1)
                {
                    IndexEndBarPos();
                    continue;
                }
                double time = mainKLine.Arr_Time[i];
                IndexKLineData(i, time);
            }
        }

        private void IndexEndBarPos()
        {
            foreach (IKLineData_Extend klineData in indexKLines)
            {
                if (klineData.Equals(mainKLine))
                    continue;
                AddOnBarData(mainKLine.Length - 1, klineData.Period, klineData.Length - 1);
            }
        }

        private void IndexKLineData(int mainIndex, double time)
        {
            foreach (IKLineData kline in indexKLines)
            {
                if (kline.Equals(mainKLine))
                    continue;
                KLinePeriod period = kline.Period;
                if (period.PeriodType >= KLineTimeType.DAY)
                {
                    if (period.Period == 1)
                    {
                        //if (mainKLine.IsDayEnd(mainIndex))
                        if (mainKLine.IsDayStart(mainIndex))
                        {
                            int currentDayIndex = GetCurrentIndex(period);
                            if (currentDayIndex == 0)
                                currentDayIndex = kline.BarPos;
                            AddOnBarData(mainIndex - 1, period, currentDayIndex);
                            dic_Period_CurrentIndex[period] = currentDayIndex + 1;
                        }
                    }
                    //TODO
                    continue;
                }

                int barIndex = FindBarIndex(kline, time, GetCurrentIndex(period));
                if (barIndex >= kline.BarPos)
                {
                    int endBarMainKLineIndex = mainIndex - 1;
                    AddOnBarData(endBarMainKLineIndex, period, barIndex);
                    dic_Period_CurrentIndex[period] = barIndex;
                }
            }
        }

        private void IndexKLine_Day()
        {
            //if (period.Period == 1)
            //{
            //    if (mainKLine.IsDayEnd(mainIndex))
            //        if (mainKLine.IsDayStart(mainIndex))
            //        {
            //            int currentDayIndex = GetCurrentIndex(kline);
            //            AddOnBarData(mainIndex - 1, kline, currentDayIndex);
            //            dic_Period_CurrentIndex[kline] = currentDayIndex + 1;
            //        }
            //}
        }

        private int GetCurrentIndex(KLinePeriod period)
        {
            if (!dic_Period_CurrentIndex.ContainsKey(period))
                return 0;
            return dic_Period_CurrentIndex[period];
        }

        public static int FindBarIndex(IKLineData klineData, double time, int currentIndex)
        {
            double klineTime;
            while (currentIndex < klineData.Length)
            {
                klineTime = klineData.Arr_Time[currentIndex];
                //if (klineTime >= time)
                //    return currentIndex - 1;
                if (klineTime == time)
                    return currentIndex - 1;
                if (klineTime > time)
                    return -1;
                currentIndex++;
            }
            return -1;
        }

        private void AddOnBarData(int mainBarPos, KLinePeriod klinePeriod, int klineBarPos)
        {
            if (dic_Period_PosIndex.ContainsKey(klinePeriod))
            {
                dic_Period_PosIndex[klinePeriod].Add(mainBarPos, klineBarPos);
            }
            else
            {
                Dictionary<int, int> dic_MainPos_KLinePos = new Dictionary<int, int>();
                dic_MainPos_KLinePos.Add(mainBarPos, klineBarPos);
                dic_Period_PosIndex.Add(klinePeriod, dic_MainPos_KLinePos);
            }
        }

        public int GetOtherKLineBarPosIfFinished(int mainBarPos, KLinePeriod klinePeriod)
        {
            if (!dic_Period_PosIndex.ContainsKey(klinePeriod))
                return -1;
            Dictionary<int, int> indexBarPos = dic_Period_PosIndex[klinePeriod];
            if (indexBarPos.ContainsKey(mainBarPos))
                return indexBarPos[mainBarPos];
            return -1;
        }
    }
}
