using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update2
{
    class DataTransfer_Tick2KLine2
    {
        public static IKLineData Transfer(List<TickData> data, KLinePeriod targetPeriod, List<double[]> opentime)
        {
            List<IKLineData> klineDataList = new List<IKLineData>();
            KLineData lastData = null;
            for (int i = 0; i < data.Count; i++)
            {
                float lastPrice = lastData == null ? -1 : lastData.arr_end[lastData.Length - 1];
                KLineData klinedata = Transfer(data[i], targetPeriod, opentime, lastPrice);
                klineDataList.Add(klinedata);
                lastData = klinedata;
            }
            //DataTransfer_Tick2KLine transfer = new DataTransfer_Tick2KLine(data, targetPeriod, opentime, yesterdayEndPrice);
            //return transfer.GetKLineData();
            return KLineData.Merge(klineDataList);
        }

        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime)
        {
            return Transfer(data, targetPeriod, opentime, -1);
        }

        public static KLineData Transfer(TickData data, KLinePeriod targetPeriod, List<double[]> opentime, float yesterdayEndPrice)
        {
            DataTransfer_Tick2KLine2 transfer = new DataTransfer_Tick2KLine2(data, targetPeriod, opentime, yesterdayEndPrice);
            return transfer.GetKLineData();
        }

        private TickData ticks;
        private KLinePeriod targetPeriod;
        private List<double> timePeriods;
        private float yesterdayEndPrice = -1;

        public DataTransfer_Tick2KLine2(TickData ticks, KLinePeriod targetPeriod, List<double[]> opentime, float yesterdayEndPrice)
        {
            this.ticks = ticks;
            this.targetPeriod = targetPeriod;
            this.timePeriods = TimeUtils.GetKLineTimes(opentime, targetPeriod);
            this.yesterdayEndPrice = yesterdayEndPrice;
        }

        public List<KLineChart> GetKLineCharts()
        {
            return KLineChartGen.GenerateCharts(ticks, timePeriods, yesterdayEndPrice);
        }

        public KLineData GetKLineData()
        {
            List<KLineChart> charts = KLineChartGen.GenerateCharts(ticks, timePeriods, yesterdayEndPrice);

            return GetCharts(charts);
        }

        public static KLineData GetCharts(List<KLineChart> charts)
        {
            KLineData data = new KLineData(charts.Count);
            for (int i = 0; i < charts.Count; i++)
            {
                KLineChart chart = charts[i];
                data.arr_time[i] = chart.time;
                data.arr_start[i] = chart.start;
                data.arr_high[i] = chart.high;
                data.arr_low[i] = chart.low;
                data.arr_end[i] = chart.end;
                data.arr_mount[i] = chart.mount;
                data.arr_money[i] = chart.money;
                data.arr_hold[i] = chart.hold;
            }
            return data;
        }
    }

    public class KLineChartGen
    {
        private int currentTickIndex = 0;

        private int currentPeriodIndex = 0;

        private int currentHold = 0;

        private TickData tickData;

        private List<double> periods;

        private float yesterdayEndPrice;

        public KLineChartGen(TickData data, List<double> timePeriods, float yesterdayEndPrice)
        {
            this.tickData = data;
            this.yesterdayEndPrice = yesterdayEndPrice;
            //传入的timePeriods是没有日期的，下面函数将日期加入
            this.periods = GetTodayTimePeriods(data, timePeriods);
        }

        private List<double> GetTodayTimePeriods(TickData data, List<double> timePeriods)
        {
            int startDate = GetDate(0);
            int endDate = GetDate(data.Length - 1);

            List<double> periods = new List<double>(timePeriods.Count);
            if (startDate == endDate)
            {
                for (int i = 0; i < timePeriods.Count; i++)
                    periods.Add(Math.Round(startDate + timePeriods[i], 6));
            }
            else
            {
                bool isEndDate = false;
                for (int i = 0; i < timePeriods.Count; i++)
                {
                    if (isEndDate)
                        periods.Add(Math.Round(endDate + timePeriods[i], 6));
                    else
                    {
                        int date = startDate;
                        if (i != 0)
                        {
                            if (timePeriods[i] < timePeriods[i - 1])
                            {
                                isEndDate = true;
                                date = endDate;
                            }
                        }
                        periods.Add(Math.Round(date + timePeriods[i], 6));
                    }
                }
            }
            return periods;
        }

        private int GetDate(int index)
        {
            return (int)tickData.arr_time[index];
        }

        public KLineChart NextChart()
        {
            KLineChart chart;
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

        private KLineChart GetEmptyChart()
        {
            KLineChart chart = new KLineChart();
            chart.time = periods[currentPeriodIndex];
            float lastPrice = currentTickIndex == 0 ? yesterdayEndPrice : tickData.arr_price[currentTickIndex - 1];
            if (lastPrice < 0)
                lastPrice = tickData.arr_price[0];
            chart.start = lastPrice;
            chart.high = lastPrice;
            chart.low = lastPrice;
            chart.end = lastPrice;
            chart.mount = 0;
            chart.hold = currentHold;
            return chart;
        }

        private KLineChart GetChart(int startTickIndex, int endTickIndex)
        {
            //if (endTickIndex < startTickIndex)
            //    return GetEmptyChart();
            KLineChart chart = new KLineChart();
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
            chart.time = periods[currentPeriodIndex];
            chart.start = tickData.arr_price[startTickIndex];
            chart.high = high;
            chart.low = low;
            chart.end = tickData.arr_price[endTickIndex];
            chart.mount = mount;
            chart.money = money;
            chart.hold = currentHold;
            return chart;
        }

        public bool HasNext()
        {
            return currentPeriodIndex != periods.Count;
        }

        public static List<KLineChart> GenerateCharts(TickData tickData, List<double> timePeriods, float yesterdayEndPrice)
        {
            KLineChartGen gen = new KLineChartGen(tickData, timePeriods, yesterdayEndPrice);
            List<KLineChart> charts = new List<KLineChart>();
            while (gen.HasNext())
            {
                charts.Add(gen.NextChart());
            }
            return charts;
        }
    }

    public class KLineChart
    {
        public double time;
        public float start;
        public float high;
        public float low;
        public float end;
        public int mount;
        public float money;
        public int hold;

        override
        public String ToString()
        {
            return time + "," + start + "," + high + "," + low + "," + end + "," + mount + "," + money + "," + hold;
        }
    }
}