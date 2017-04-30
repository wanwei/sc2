using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;

namespace com.wer.sc.data.update
{
    public class DataTransfer_Tick2KLineGenerator
    {
        private int currentTickIndex = 0;

        private int currentPeriodIndex = 0;

        private int currentHold = 0;

        private TickData tickData;

        private List<double> periods;

        private float yesterdayEndPrice;

        public DataTransfer_Tick2KLineGenerator(TickData data, List<double[]> openTime, KLinePeriod targetPeriod, float yesterdayEndPrice)
        {
            this.tickData = data;
            this.yesterdayEndPrice = yesterdayEndPrice;
            //传入的timePeriods是没有日期的，下面函数将日期加入
            this.periods = GetTodayTimePeriods(data, openTime, targetPeriod);
        }

        private static List<double> GetTodayTimePeriods(TickData data, List<double[]> openTime, KLinePeriod targetPeriod)
        {
            List<double> timePeriods = TimeUtils.GetKLineTimes(openTime, targetPeriod);
            int startDate = GetDate(data, 0);
            int endDate = GetDate(data, data.Length - 1);            

            List<double> periods = new List<double>(timePeriods.Count);
            if (startDate == endDate)
            {
                for (int i = 0; i < timePeriods.Count; i++)
                    periods.Add(Math.Round(startDate + timePeriods[i], 6));
            }
            else
            {
                /*
                 * 四种情况：
                 * 1.无夜盘，直接生成即可：如20100105090000、...
                 * 2.有夜盘，但夜盘不过夜：如20160329210000、...、20160329233000、...、20160330090000
                 * 3.有夜盘，要过夜，不是周末：如20150324210000、...、20150325023000、...、20150325090000
                 * 4.有夜盘，要过夜，而且是周末：如20141226210000、...、20141227023000、...、20141229090000
                 */
                int date = startDate;
                bool dayChanged = false;
                for (int i = 0; i < timePeriods.Count; i++)
                {
                    if (i != 0 && date != endDate)
                    {
                        double currentTime = timePeriods[i];
                        if (isDayChange(timePeriods[i - 1], currentTime))
                        {
                            if (dayChanged || currentTime > 0.06)
                                date = endDate;
                            else
                            {
                                date = (int)TimeUtils.AddDays(startDate, 1);
                                if (date == endDate)
                                    date = endDate;
                            }
                            dayChanged = true;
                        }
                    }
                    periods.Add(Math.Round(date + timePeriods[i], 6));
                }
            }
            return periods;
        }

        private static bool isDayChange(double time1, double time2)
        {
            if (time2 < time1)
                return true;
            if (time1 < 0.06 && time2 > 0.08)
                return true;
            return false;
        }

        private static int GetDate(ITickData tickData, int index)
        {
            return (int)tickData.Arr_Time[index];
        }

        public KLineBar NextChart()
        {
            KLineBar chart;
            //已经没有tick数据了，说明最后几个周期没有交易
            if (currentTickIndex >= tickData.Length)
            {
                chart = GetEmptyChart();
                currentPeriodIndex++;
                return chart;
            }
            //double endTime = currentPeriodIndex == periods.Count - 1 ? double.MaxValue : periods[currentPeriodIndex + 1];
            double endTime = GetEndTime();
            double currentTickTime = GetTickTime(currentTickIndex);
            //如果当前tick时间大于该周期的结束时间，说明这一个时间段没有交易
            if (currentTickTime >= endTime)
            {
                chart = GetEmptyChart();
                currentPeriodIndex++;
                return chart;
            }
            int startTickIndex = currentTickIndex;
            int endTickIndex = GetEndTickIndex(endTime);
            chart = GetChart(startTickIndex, endTickIndex);
            currentTickIndex = endTickIndex + 1;
            currentPeriodIndex++;
            return chart;
        }

        private double GetEndTime()
        {
            if (currentPeriodIndex == periods.Count - 1)
                return double.MaxValue;
            double nextPeriod = periods[currentPeriodIndex + 1];
            double period = periods[currentPeriodIndex];
            TimeSpan span = TimeUtils.Substract(nextPeriod, period);
            double endTime = nextPeriod;
            if (span.Minutes > 5)
                endTime = TimeUtils.AddMinutes(period, 5);

            return endTime;
        }

        private double GetTickTime(int tickIndex)
        {
            return tickData.arr_time[tickIndex];
        }

        private int GetEndTickIndex(double endTime)
        {
            if (currentPeriodIndex == periods.Count - 1)
                return tickData.Length - 1;

            double startTime = periods[currentPeriodIndex];

            int tickIndex = currentTickIndex;
            double tickTime = GetTickTime(tickIndex);
            //if (tickTime < startTime && currentPeriodIndex != 0)
            //    tickTime += 1;

            while (tickTime < endTime)
            {
                tickIndex++;
                if (tickIndex >= tickData.Length)
                    return tickIndex - 1;

                tickTime = GetTickTime(tickIndex);
                if (tickTime < startTime && currentPeriodIndex != 0)
                    tickTime += 1;
            }
            return tickIndex - 1;
        }

        /// <summary>
        /// 是否是周期结束
        /// 规则：
        /// 1.如果该周期不是过夜周期，直接根据时间判断
        /// 2.如果该周期是过夜周期，那么
        /// </summary>
        /// <param name="periodTimeOverNight">该周期过夜，比如20150104235900-20150105000000，算周期过夜</param>
        /// <param name="tickOverNight">tick数据过夜了，如该tick数据正好是20150105000005，前一个tick时间是20150104235958</param>
        /// <param name="endTime">该周期的结束时间</param>
        /// <param name="tickTime">现在的tick时间</param>
        /// <returns></returns>
        private Boolean IsPeriodEnd(bool periodTimeOverNight, bool tickOverNight, double endTime, double tickTime)
        {
            if (!periodTimeOverNight)
                return tickTime >= endTime;

            if (!periodTimeOverNight || tickOverNight)
                return tickTime < endTime;
            return true;
        }

        private KLineBar GetEmptyChart()
        {
            KLineBar chart = new KLineBar();
            chart.Code = tickData.Code;
            chart.Time = periods[currentPeriodIndex];
            float lastPrice = currentTickIndex == 0 ? yesterdayEndPrice : tickData.arr_price[currentTickIndex - 1];
            if (lastPrice < 0)
                lastPrice = tickData.arr_price[0];
            chart.Start = lastPrice;
            chart.High = lastPrice;
            chart.Low = lastPrice;
            chart.End = lastPrice;
            chart.Mount = 0;
            chart.Hold = currentHold;
            return chart;
        }

        private KLineBar GetChart(int startTickIndex, int endTickIndex)
        {
            //if (endTickIndex < startTickIndex)
            //    return GetEmptyChart();
            KLineBar chart = new KLineBar();
            float high = 0;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startTickIndex; i <= endTickIndex; i++)
            {
                int currentMount = tickData.arr_mount[i];
                float price = tickData.arr_price[i];
                high = high < price ? price : high;
                low = low > price ? price : low;
                mount += currentMount;
                //money += currentMount * price;
                currentHold += tickData.arr_add[i];
            }
            chart.Code = tickData.Code;
            chart.Time = periods[currentPeriodIndex];
            chart.Start = tickData.arr_price[startTickIndex];
            chart.High = high;
            chart.Low = low;
            chart.End = tickData.arr_price[endTickIndex];
            chart.Mount = mount;
            chart.Money = money;
            chart.Hold = currentHold;
            return chart;
        }

        public bool HasNext()
        {
            return currentPeriodIndex != periods.Count;
        }

        public static List<KLineBar> GenerateCharts(TickData tickData, List<double[]> openTime, KLinePeriod targetPeriod, float yesterdayEndPrice)
        {
            DataTransfer_Tick2KLineGenerator gen = new DataTransfer_Tick2KLineGenerator(tickData, openTime, targetPeriod, yesterdayEndPrice);
            List<KLineBar> charts = new List<KLineBar>();
            while (gen.HasNext())
            {
                charts.Add(gen.NextChart());
            }
            return charts;
        }
    }
}
