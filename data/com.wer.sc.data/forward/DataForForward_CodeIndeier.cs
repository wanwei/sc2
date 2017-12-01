using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 历史回测数据索引器
    /// 将
    /// </summary>
    public class DataForForward_CodeIndeier
    {
        private DataForForward_Code data;

        private Dictionary<int, List<KLineBarPos>> dic_BarPos_OnBarPeriods = new Dictionary<int, List<KLineBarPos>>();

        private Dictionary<KLinePeriod, int> dic_Period_CurrentIndex = new Dictionary<KLinePeriod, int>();

        public DataForForward_CodeIndeier(DataForForward_Code data)
        {
            this.data = data;
            IList<KLinePeriod> periods = data.ReferedKLinePeriods;
            for (int i = 0; i < periods.Count; i++)
            {
                KLinePeriod period = periods[i];
                if (period.Equals(data.MainKLinePeriod))
                    continue;
                dic_Period_CurrentIndex.Add(period, data.GetKLineData(period).BarPos);
            }
            this.Index();
        }

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="mainBarPos"></param>
        /// <returns></returns>
        public IList<KLineBarPos> GetFinishedBarsRelativeToMainKLine(int mainBarPos)
        {
            if (!dic_BarPos_OnBarPeriods.ContainsKey(mainBarPos))
                return null;
            return dic_BarPos_OnBarPeriods[mainBarPos];
        }

        private void AddOnBarData(int mainBarPos, KLinePeriod klinePeriod, int klineBarPos)
        {
            if (dic_BarPos_OnBarPeriods.ContainsKey(mainBarPos))
            {
                dic_BarPos_OnBarPeriods[mainBarPos].Add(new KLineBarPos(klinePeriod, klineBarPos));
            }
            else
            {
                List<KLineBarPos> onBarPeriods = new List<KLineBarPos>();
                onBarPeriods.Add(new KLineBarPos(klinePeriod, klineBarPos));
                dic_BarPos_OnBarPeriods.Add(mainBarPos, onBarPeriods);
            }
        }

        private void Index()
        {
            IKLineData_RealTime mainKLineData = data.GetMainKLineData();
            IKLineData_Extend klineData = mainKLineData.GetKLineData_Original();
            for (int i = klineData.BarPos + 1; i < klineData.Length; i++)
            {
                if (i == klineData.Length - 1)
                {
                    IndexEndBarPos();
                    continue;
                }
                double time = klineData.Arr_Time[i];
                IndexKLineData(i, time);
            }
        }

        private void IndexEndBarPos()
        {
            foreach (KLinePeriod period in data.ReferedKLinePeriods)
            {
                if (period.Equals(data.MainKLinePeriod))
                    continue;
                AddOnBarData(data.GetMainKLineData().Length - 1, period, data.GetKLineData(period).Length - 1);
            }
        }

        private int GetCurrentIndex(KLinePeriod period)
        {
            if (!dic_Period_CurrentIndex.ContainsKey(period))
                return 0;
            return dic_Period_CurrentIndex[period];
        }

        private void IndexKLineData(int mainIndex, double time)
        {
            foreach (KLinePeriod period in data.ReferedKLinePeriods)
            {
                if (period.Equals(data.MainKLinePeriod))
                    continue;

                if (period.PeriodType >= KLineTimeType.DAY)
                {
                    if (period.Period == 1)
                    {
                        IKLineData_Extend mainKlineData = data.GetMainKLineData();
                        //if (mainKlineData.IsDayEnd(mainIndex))
                        if (mainKlineData.IsDayStart(mainIndex))
                        {
                            int currentDayIndex = GetCurrentIndex(period);
                            AddOnBarData(mainIndex - 1, period, currentDayIndex);
                            dic_Period_CurrentIndex[period] = currentDayIndex + 1;
                        }
                    }
                    continue;
                }

                IKLineData_RealTime klineData = data.GetKLineData(period);
                int barIndex = FindBarIndex(klineData, time, GetCurrentIndex(period));
                if (barIndex >= klineData.BarPos)
                {
                    int endBarMainKLineIndex = mainIndex - 1;
                    AddOnBarData(endBarMainKLineIndex, period, barIndex);
                    dic_Period_CurrentIndex[period] = barIndex;
                }
            }
        }

        private int FindBarIndex(IKLineData klineData, double time, int currentIndex)
        {
            double klineTime;
            while (currentIndex < klineData.Length)
            {
                klineTime = klineData.Arr_Time[currentIndex];
                if (klineTime == time)
                    return currentIndex - 1;
                if (klineTime > time)
                    return -1;
                currentIndex++;
            }
            return -1;
        }
    }

    public class KLineBarPos
    {
        public KLinePeriod KLinePeriod;
        public int BarPos;

        public override string ToString()
        {
            return KLinePeriod.ToString() + "," + BarPos;
        }

        public KLineBarPos()
        {

        }

        public KLineBarPos(KLinePeriod klinePeriod, int barPos)
        {
            this.KLinePeriod = klinePeriod;
            this.BarPos = barPos;
        }
    }
}