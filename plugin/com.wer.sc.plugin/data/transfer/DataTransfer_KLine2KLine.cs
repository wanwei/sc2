using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    /// <summary>
    /// K线数据转换，一般是用短周期K线转换成长周期K线
    /// 如将1分钟K线转成5分钟K线
    /// </summary>
    public class DataTransfer_KLine2KLine
    {
        /// <summary>
        /// 将源k线数据转换成目标k线数据
        /// 源只可能是1分钟、1小时、1日
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targetPeriod"></param>
        /// <param name="openTimeList"></param>
        /// <returns></returns>
        public static IKLineData Transfer(IKLineData data, KLinePeriod targetPeriod, ITradingSessionReader_Instrument tradingSessionReader)
        {
            if (targetPeriod.PeriodType == KLineTimeType.DAY)
                return Transfer_Day(data, targetPeriod, tradingSessionReader);
            KLinePeriod sourcePeriod = data.Period;
            if (sourcePeriod.PeriodType == KLineTimeType.MINUTE)
            {
                if (targetPeriod.PeriodType == KLineTimeType.HOUR)
                    targetPeriod = new KLinePeriod(sourcePeriod.PeriodType, targetPeriod.Period * 60);
                return Transfer_SrcIs1Minute(data, targetPeriod, tradingSessionReader);
            }
            if (sourcePeriod.PeriodType == KLineTimeType.HOUR)
            {
                return Transfer_SrcIs1Minute(data, targetPeriod, tradingSessionReader);
            }
            return null;
        }

        /// <summary>
        /// 将k线转换成日线
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targetPeriod"></param>
        /// <param name="timeSplit"></param>
        /// <returns></returns>
        public static IKLineData Transfer_Day(IKLineData data, KLinePeriod targetPeriod, ITradingSessionReader_Instrument startTimeReader)
        {
            List<SplitterResult> results = DaySplitter.Split(data, startTimeReader);

            List<KLineBar> charts = new List<KLineBar>(results.Count);
            for (int i = 0; i < results.Count; i++)
            {
                int startIndex = results[i].Index;
                int endIndex = (i == results.Count - 1) ? data.Length - 1 : results[i + 1].Index - 1;
                charts.Add(GetChart_Day(data, startIndex, endIndex));
            }

            return GetKLineData(data.Code, charts);
        }

        private static KLineBar GetChart_Day(IKLineData data, int startIndex, int endIndex)
        {
            KLineBar chart = GetChart(data, startIndex, endIndex);
            chart.Time = (int)data.Arr_Time[endIndex];
            return chart;
        }

        private static IKLineData Transfer_SrcIs1Minute(IKLineData data, KLinePeriod targetPeriod, ITradingSessionReader_Instrument startTimeReader)
        {
            KLinePeriod sourcePeriod = data.Period;
            if (sourcePeriod.PeriodType != targetPeriod.PeriodType)
                return Transfer_DifferentPeriod(data, targetPeriod, startTimeReader);

            List<KLineBar> charts = new List<KLineBar>();
            int period = targetPeriod.Period;

            int startIndex = 0;
            int endIndex = startIndex + period - 1;
            endIndex = FindRealLastIndex_1Minute(data, startIndex, endIndex);

            while (startIndex < data.Length && endIndex < data.Length)
            {
                charts.Add(GetChart(data, startIndex, endIndex));
                startIndex = endIndex + 1;
                endIndex = startIndex + period - 1;

                endIndex = FindRealLastIndex_1Minute(data, startIndex, endIndex);
            }

            return GetKLineData(data.Code, charts);
        }

        private static int FindRealLastIndex_1Minute(IKLineData data, int startIndex, int endIndex)
        {
            if (endIndex >= data.Length)
                return data.Length - 1;
            if (startIndex >= data.Length)
                return endIndex;
            double between = data.Arr_Time[endIndex] - data.Arr_Time[startIndex];
            if (between < 0.04)
                return endIndex;

            while (between > 0.04)
            {
                endIndex--;
                between = data.Arr_Time[endIndex] - data.Arr_Time[startIndex];
            }
            return endIndex;
        }

        private static KLineBar GetChart(IKLineData data, int startIndex, int endIndex)
        {
            //KLineChart chart = new KLineChart();
            KLineBar chart = new KLineBar();
            chart.Time = data.Arr_Time[startIndex];
            chart.Start = data.Arr_Start[startIndex];
            chart.End = data.Arr_End[endIndex];
            chart.Hold = data.Arr_Hold[endIndex];

            float high = float.MinValue;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                float chigh = data.Arr_High[i];
                float clow = data.Arr_Low[i];
                high = high < chigh ? chigh : high;
                low = low > clow ? clow : low;
                mount += data.Arr_Mount[i];
                money += data.Arr_Money[i];
            }
            chart.High = high;
            chart.Low = low;
            chart.Mount = mount;
            chart.Money = money;
            return chart;
        }

        private static KLineData GetKLineData(string code, List<KLineBar> charts)
        {
            KLineData data = new KLineData(charts.Count);
            data.Code = code;
            for (int i = 0; i < charts.Count; i++)
            {
                KLineBar chart = charts[i];
                data.arr_time[i] = chart.Time;
                data.arr_start[i] = chart.Start;
                data.arr_high[i] = chart.High;
                data.arr_low[i] = chart.Low;
                data.arr_end[i] = chart.End;
                data.arr_mount[i] = chart.Mount;
                data.arr_money[i] = chart.Money;
                data.arr_hold[i] = chart.Hold;
            }
            return data;
        }

        private static IKLineData Transfer_DifferentPeriod(IKLineData data, KLinePeriod targetPeriod, ITradingSessionReader_Instrument startTimeReader)
        {
            KLinePeriod srcPeriod = data.Period;
            if (targetPeriod.PeriodType == KLineTimeType.HOUR && srcPeriod.PeriodType == KLineTimeType.MINUTE)
            {
                KLinePeriod p = new KLinePeriod(srcPeriod.PeriodType, targetPeriod.Period * 60);
                return Transfer(data, p, startTimeReader);
            }
            return null;
        }

        //private static KLineData Transfer_Minute(KLineData data, KLinePeriod targetPeriod, List<double[]> openTimeList)
        //{
        //    //TimeUtils.GetKLineTimes()
        //    return null;
        //}
    }
}
