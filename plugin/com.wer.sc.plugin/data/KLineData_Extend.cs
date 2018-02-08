using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineData_Extend : IKLineData_Extend
    {
        private IKLineDataTradingTimeInfo_Periods currentTradingPeriods;

        private IKLineData klineData;

        private IList<ITradingTime> tradingTimes;

        private Dictionary<int, ITradingTime> dic_TradingDay_TradingTime = new Dictionary<int, ITradingTime>();

        private KLineDataTradingTimeInfo klineTimeInfo;

        private List<int> tradingDayEndBarPoses;

        private List<int> tradingPeriodEndBarPoses;

        public KLineData_Extend(IKLineData klineData, IList<ITradingTime> tradingTimes)
        {
            this.klineData = klineData;
            this.tradingTimes = tradingTimes;
            for (int i = 0; i < tradingTimes.Count; i++)
            {
                ITradingTime tradingTime = tradingTimes[i];
                dic_TradingDay_TradingTime.Add(tradingTime.TradingDay, tradingTime);
            }
            this.klineTimeInfo = new KLineDataTradingTimeInfo(klineData, tradingTimes);
            this.currentTradingPeriods = klineTimeInfo.GetTradingPeriodsByBarPos(klineData.BarPos);
        }

        #region IKLineData的实现

        public IList<float> Arr_BlockHeight
        {
            get
            {
                return klineData.Arr_BlockHeight;
            }
        }

        public IList<float> Arr_BlockHeightPercent
        {
            get
            {
                return klineData.Arr_BlockHeightPercent;
            }
        }

        public IList<float> Arr_BlockHigh
        {
            get
            {
                return klineData.Arr_BlockHigh;
            }
        }

        public IList<float> Arr_BlockLow
        {
            get
            {
                return klineData.Arr_BlockLow;
            }
        }

        public IList<float> Arr_End
        {
            get
            {
                return klineData.Arr_End;
            }
        }

        public IList<float> Arr_Height
        {
            get
            {
                return klineData.Arr_Height;
            }
        }

        public IList<float> Arr_HeightPercent
        {
            get
            {
                return klineData.Arr_HeightPercent;
            }
        }

        public IList<float> Arr_High
        {
            get
            {
                return klineData.Arr_High;
            }
        }

        public IList<int> Arr_Hold
        {
            get
            {
                return klineData.Arr_Hold;
            }
        }

        public IList<float> Arr_Low
        {
            get
            {
                return klineData.Arr_Low;
            }
        }

        public IList<float> Arr_Money
        {
            get
            {
                return klineData.Arr_Money;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return klineData.Arr_Mount;
            }
        }

        public IList<float> Arr_Start
        {
            get
            {
                return klineData.Arr_Start;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return klineData.Arr_Time;
            }
        }

        public IList<float> Arr_UpPercent
        {
            get
            {
                return klineData.Arr_UpPercent;
            }
        }

        public int BarPos
        {
            get
            {
                return klineData.BarPos;
            }

            set
            {
                if (value < 0 || value >= klineData.Length)
                    return;
                klineData.BarPos = value;
                if (this.currentTradingPeriods == null
                    || (this.currentTradingPeriods.StartPos > value
                    || this.currentTradingPeriods.EndPos < value))
                    this.currentTradingPeriods = GetTradingPeriodsByBarPos(value);
            }
        }

        public float BlockHeight
        {
            get
            {
                return klineData.BlockHeight;
            }
        }

        public float BlockHeightPercent
        {
            get
            {
                return klineData.BlockHeightPercent;
            }
        }

        public float BlockHigh
        {
            get
            {
                return klineData.BlockHigh;
            }
        }

        public float BlockLow
        {
            get
            {
                return klineData.BlockLow;
            }
        }

        public float BlockMiddle
        {
            get
            {
                return klineData.BlockMiddle;
            }
        }

        public float BottomShadow
        {
            get
            {
                return klineData.BottomShadow;
            }
        }

        public string Code
        {
            get
            {
                return klineData.Code;
            }
        }

        public float End
        {
            get
            {
                return klineData.End;
            }
        }

        public float Height
        {
            get
            {
                return klineData.Height;
            }
        }

        public float HeightPercent
        {
            get
            {
                return klineData.HeightPercent;
            }
        }

        public float High
        {
            get
            {
                return klineData.High;
            }
        }

        public int Hold
        {
            get
            {
                return klineData.Hold;
            }
        }

        public int Length
        {
            get
            {
                return klineData.Length;
            }
        }

        public float Low
        {
            get
            {
                return klineData.Low;
            }
        }

        public float Middle
        {
            get
            {
                return klineData.Middle;
            }
        }

        public float Money
        {
            get
            {
                return klineData.Money;
            }
        }

        public int Mount
        {
            get
            {
                return klineData.Mount;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return klineData.Period;
            }

            set
            {
                klineData.Period = value;
            }
        }

        public float Start
        {
            get
            {
                return klineData.Start;
            }
        }

        public double Time
        {
            get
            {
                return klineData.Time;
            }
        }

        public float TopShadow
        {
            get
            {
                return klineData.TopShadow;
            }
        }

        public IKLineBar GetAggrKLineBar(int startPos, int endPos)
        {
            return klineData.GetAggrKLineBar(startPos, endPos);
        }

        public IKLineBar GetBar(int index)
        {
            return klineData.GetBar(index);
        }

        public IKLineBar GetCurrentBar()
        {
            return klineData.GetCurrentBar();
        }

        public IKLineData GetRange(int startPos, int endPos)
        {
            return klineData.GetRange(startPos, endPos);
        }

        public int IndexOfTime(double time)
        {
            return klineData.IndexOfTime(time);
        }

        public bool isRed()
        {
            return klineData.isRed();
        }

        public IKLineData Sub(int startPos, int endPos)
        {
            return klineData.Sub(startPos, endPos);
        }

        public string ToString(int index)
        {
            return klineData.ToString(index);
        }

        public override string ToString()
        {
            return klineData.ToString();
        }

        #endregion

        #region IKLineData_Extend Day部分的实现

        private IKLineDataTradingTimeInfo_Periods GetTradingPeriodsByBarPos(int barPos)
        {
            return klineTimeInfo.GetTradingPeriodsByBarPos(barPos);
        }

        /// <summary>
        /// 得到当前barPos对应的交易日
        /// </summary>
        public int TradingDay
        {
            get
            {
                return this.currentTradingPeriods.KlineTimeInfo_Day.TradingDay;
            }
        }

        /// <summary>
        /// 得到该K线的所有交易日
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllTradingDays()
        {
            return this.klineTimeInfo.TradingDays;
        }

        /// <summary>
        /// 得到指定交易日在K线上开始位置的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        public int GetDayStartBarPosByTradingDay(int tradingDay)
        {
            return this.klineTimeInfo.GetKLineTimeInfo_Day(tradingDay).StartPos;
        }

        /// <summary>
        /// 得到指定交易日在K线上结束位置的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        public int GetDayEndBarPosByTradingDay(int tradingDay)
        {
            return this.klineTimeInfo.GetKLineTimeInfo_Day(tradingDay).EndPos;
        }

        /// <summary>
        /// 得到当前交易日的所有barCount
        /// </summary>
        /// <returns></returns>
        public int GetDayBarCount()
        {
            return currentTradingPeriods.KlineTimeInfo_Day.BarCount;
        }

        /// <summary>
        /// 获得当前barpos所在交易日的第一个K线的BarPos
        /// </summary>
        /// <returns></returns>
        public int GetDayStartBarPos()
        {
            return currentTradingPeriods.KlineTimeInfo_Day.StartPos;
        }

        /// <summary>
        /// 获得指定barpos所在交易日的第一个K线的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        public int GetDayStartBarPos(int tradingDay)
        {
            return klineTimeInfo.GetKLineTimeInfo_Day(tradingDay).StartPos;
        }

        /// <summary>
        /// 获得当前barpos所在交易日的最后一根K线的BarPos
        /// </summary>
        /// <returns></returns>
        public int GetDayEndBarPos()
        {
            return currentTradingPeriods.KlineTimeInfo_Day.EndPos;
        }

        /// <summary>
        /// 获得指定barpos所在交易日的最后一根K线的BarPos
        /// </summary>
        /// <param name="tradingDay"></param>
        /// <returns></returns>
        public int GetDayEndBarPos(int tradingDay)
        {
            return klineTimeInfo.GetKLineTimeInfo_Day(tradingDay).EndPos;
        }

        /// <summary>
        /// 得到当前BarPos所在交易日的交易时间
        /// </summary>
        /// <returns></returns>
        public ITradingTime GetTradingTime()
        {
            int tradingDay = TradingDay;
            if (dic_TradingDay_TradingTime.ContainsKey(tradingDay))
                return dic_TradingDay_TradingTime[tradingDay];
            return null;
        }

        /// <summary>
        /// 得到指定BarPos所在交易日的交易时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public ITradingTime GetTradingTime(int barPos)
        {
            int tradingDay = GetTradingPeriodsByBarPos(barPos).KlineTimeInfo_Day.TradingDay;
            if (dic_TradingDay_TradingTime.ContainsKey(tradingDay))
                return dic_TradingDay_TradingTime[tradingDay];
            return null;
        }

        /// <summary>
        /// 当前BarPos是否是一天的开始
        /// </summary>
        /// <returns></returns>
        public bool IsDayStart()
        {
            if (currentTradingPeriods.PeriodIndex != 0)
                return false;
            return currentTradingPeriods.StartPos == BarPos && (currentTradingPeriods.StartPos == currentTradingPeriods.KlineTimeInfo_Day.StartPos);
        }

        /// <summary>
        /// 指定的barPos是否是一天的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsDayStart(int barPos)
        {
            IKLineDataTradingTimeInfo_Periods tradingPeriods = GetTradingPeriodsByBarPos(barPos);
            if (tradingPeriods.PeriodIndex != 0)
                return false;
            return tradingPeriods.StartPos == barPos && (tradingPeriods.StartPos == tradingPeriods.KlineTimeInfo_Day.StartPos);
        }

        /// <summary>
        /// 当前的
        /// </summary>
        /// <returns></returns>
        public bool IsDayEnd()
        {
            if (currentTradingPeriods.PeriodIndex != currentTradingPeriods.KlineTimeInfo_Day.TradingPeriods.Count - 1)
                return false;
            return currentTradingPeriods.EndPos == BarPos && (currentTradingPeriods.EndPos == currentTradingPeriods.KlineTimeInfo_Day.EndPos);
        }

        /// <summary>
        /// 是否是一天结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsDayEnd(int barPos)
        {
            IKLineDataTradingTimeInfo_Periods tradingPeriods = GetTradingPeriodsByBarPos(barPos);
            if (tradingPeriods.PeriodIndex != tradingPeriods.KlineTimeInfo_Day.TradingPeriods.Count - 1)
                return false;
            return tradingPeriods.EndPos == barPos && (tradingPeriods.EndPos == tradingPeriods.KlineTimeInfo_Day.EndPos);
        }

        /// <summary>
        /// 当前BarPos对应的交易日离前一个交易日相差几天
        /// </summary>
        /// <returns></returns>
        public int HolidayDayCount()
        {
            return 0;
        }

        /// <summary>
        /// 指定barPos对应的交易日离之前交易日相差几个交易日
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public int HolidayDayCount(int barPos)
        {
            return 0;
        }

        /// <summary>
        /// 停牌日数量
        /// </summary>
        /// <returns></returns>
        public int HaltDayCount()
        {
            return 0;
        }

        /// <summary>
        /// 停牌日数量
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public int HaltDayCount(int barPos)
        {
            return 0;
        }

        /// <summary>
        /// 得到所有交易日结束的barpos
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllTradingDayEndBarPoses()
        {
            if (tradingDayEndBarPoses == null)
            {
                tradingDayEndBarPoses = new List<int>(this.klineTimeInfo.TradingDayInfos.Count);
                for (int i = 0; i < this.klineTimeInfo.TradingDayInfos.Count; i++)
                {
                    IKLineDataTradingTimeInfo_Day timeInfo_Day = this.klineTimeInfo.TradingDayInfos[i];
                    tradingDayEndBarPoses.Add(timeInfo_Day.EndPos);
                }
            }
            return tradingDayEndBarPoses;
        }

        #endregion

        #region TradingPeriod

        /// <summary>
        /// 得到当前BarPos所在的交易周期
        /// </summary>
        public double[] GetTradingPeriods()
        {
            ITradingTime tradingTime = GetTradingTime();
            return tradingTime.GetPeriodTime(currentTradingPeriods.PeriodIndex);
        }

        /// <summary>
        /// 得到指定BarPos所在的交易周期
        /// </summary>
        public double[] GetTradingPeriods(int barPos)
        {
            ITradingTime tradingTime = GetTradingTime(barPos);
            int periodIndex = GetTradingPeriodsByBarPos(barPos).PeriodIndex;
            return tradingTime.TradingPeriods[periodIndex];
        }

        /// <summary>
        /// 得到当前交易周期的bar的数量
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsBarCount()
        {
            return currentTradingPeriods.BarCount;
        }

        /// <summary>
        /// 得到当前交易周期的bar的数量
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsBarCount(int barPos)
        {
            return GetTradingPeriodsByBarPos(barPos).BarCount;
        }

        /// <summary>
        /// 获得当前barpos所在交易时段的第一个K线的BarPos
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsStartBarPos()
        {
            return currentTradingPeriods.StartPos;
        }

        /// <summary>
        /// 获得指定barpos所在交易时段的第一个K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public int GetTradingPeriodsStartBarPos(int barPos)
        {
            return GetTradingPeriodsByBarPos(barPos).StartPos;
        }

        /// <summary>
        /// 获得当前barpos所在交易时段的最后一根K线的BarPos
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsEndBarPos()
        {
            return currentTradingPeriods.EndPos;
        }

        /// <summary>
        /// 获得指定barpos所在交易时段的最后一根K线的BarPos
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public int GetTradingPeriodsEndBarPos(int barPos)
        {
            return GetTradingPeriodsByBarPos(barPos).EndPos;
        }

        /// <summary>
        /// 得到当前BarPos在交易时段的Index
        /// </summary>
        /// <returns></returns>
        public int GetIndexInTradingPeriods()
        {
            int startBarPos = currentTradingPeriods.StartPos;
            return BarPos - startBarPos;
        }

        /// <summary>
        /// 得到指定BarPos在交易时段的Index
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public int GetIndexInTradingPeriods(int barPos)
        {
            IKLineDataTradingTimeInfo_Periods tradingPeriods = GetTradingPeriodsByBarPos(barPos);
            int startBarPos = tradingPeriods.StartPos;
            return barPos - startBarPos;
        }

        /// <summary>
        /// 得到当前barPos所在交易时段是交易日的第几个交易时段
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsIndexInTradingDay()
        {
            return currentTradingPeriods.PeriodIndex;
        }

        /// <summary>
        /// 得到指定barPos所在交易时段是交易日的第几个交易时段
        /// </summary>
        /// <returns></returns>
        public int GetTradingPeriodsIndexInTradingDay(int barPos)
        {
            return GetTradingPeriodsByBarPos(barPos).PeriodIndex;
        }

        /// <summary>
        /// 得到当前BarPos是否是一个开盘周期的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsTradingPeriodStart()
        {
            return currentTradingPeriods.StartPos == BarPos;
        }

        /// <summary>
        /// 得到指定barpos是否是一个开盘周期的开始
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsTradingPeriodStart(int barPos)
        {
            IKLineDataTradingTimeInfo_Periods tradingPeriods = GetTradingPeriodsByBarPos(barPos);
            return tradingPeriods.StartPos == barPos;
        }

        /// <summary>
        /// 得到当前BarPos是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsTradingPeriodEnd()
        {
            return currentTradingPeriods.EndPos == BarPos;
        }

        /// <summary>
        /// 得到指定BarPos是否是一个开盘周期结束
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public bool IsTradingPeriodEnd(int barPos)
        {
            IKLineDataTradingTimeInfo_Periods tradingPeriods = GetTradingPeriodsByBarPos(barPos);
            return tradingPeriods.EndPos == barPos;
        }

        /// <summary>
        /// 得到所有交易时间结束的barpos
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllTradingTimeEndBarPoses()
        {
            if (tradingPeriodEndBarPoses == null)
            {
                tradingPeriodEndBarPoses = new List<int>();
                for (int i = 0; i < klineTimeInfo.TradingDayInfos.Count; i++)
                {
                    IKLineDataTradingTimeInfo_Day timeInfo_Day = klineTimeInfo.TradingDayInfos[i];
                    for (int j = 0; j < timeInfo_Day.TradingPeriods.Count; j++)
                    {
                        tradingPeriodEndBarPoses.Add(timeInfo_Day.TradingPeriods[j].EndPos);
                    }
                }
            }
            return tradingPeriodEndBarPoses;
        }

        #endregion

        #region KLinePeriod

        /// <summary>
        /// 得到距离这个bar结束的时间
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTimeToKLinePeriodEnd()
        {
            return default(TimeSpan);
        }

        /// <summary>
        /// 得到这个bar的结束时间
        /// </summary>
        /// <param name="barPos"></param>
        /// <returns></returns>
        public double GetKLinePeriodEndTime(int barPos)
        {
            double time = Arr_Time[barPos];
            return TimeUtils.AddTime(time, this.Period.Period, this.Period.PeriodType);
        }

        #endregion
    }
}