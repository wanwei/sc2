using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataTransfer_Tick2KLine
    {
        public static IKLineData Transfer(int date, TickData data, TradingDayReader openDateReader, List<double[]> opentime, KLinePeriod targetPeriod, KLineData lastKLineData)
        {
            List<double> klineTimePeriods = OpenTimeUtils.GetKLineTimeList(date, openDateReader, opentime, targetPeriod);
            KLineData klineData = new KLineData(klineTimePeriods.Count);
            for (int i = 0; i < klineTimePeriods.Count; i++)
            {
                klineData.arr_time[i] = klineTimePeriods[i];
            }

            int currentTickindex = 0;
            for (int i = 0; i < klineData.Length; i++)
            {

            }

            return klineData;
        }

        private static int NextTickIndex()
        {
            return -1;
        }

        private static void GetEmptyChart(KLineData klineData, int currentIndex, double time, IKLineData lastKLineData)
        {
            klineData.arr_time[currentIndex] = time;
            float lastPrice = currentIndex == 0 ? lastKLineData.Arr_End[lastKLineData.Length - 1] : klineData.Arr_End[currentIndex - 1];
            int lastHold = currentIndex == 0 ? lastKLineData.Arr_Hold[lastKLineData.Length - 1] : klineData.Arr_Hold[currentIndex - 1];
            klineData.arr_start[currentIndex] = lastPrice;
            klineData.arr_high[currentIndex] = lastPrice;
            klineData.arr_low[currentIndex] = lastPrice;
            klineData.arr_end[currentIndex] = lastPrice;
            klineData.arr_mount[currentIndex] = 0;
            klineData.arr_money[currentIndex] = 0;
            klineData.arr_hold[currentIndex] = lastHold;
        }

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
            DataTransfer_Tick2KLine transfer = new DataTransfer_Tick2KLine(data, targetPeriod, opentime, yesterdayEndPrice);
            return transfer.GetKLineData();
        }

        private TickData ticks;
        private KLinePeriod targetPeriod;
        //private List<double> timePeriods;
        private float yesterdayEndPrice = -1;
        private List<double[]> openTime;

        public DataTransfer_Tick2KLine(TickData ticks, KLinePeriod targetPeriod, List<double[]> openTime, float yesterdayEndPrice)
        {
            this.ticks = ticks;
            this.targetPeriod = targetPeriod;
            this.openTime = openTime;
            //this.timePeriods = TimeUtils.GetKLineTimes(opentime, targetPeriod);
            this.yesterdayEndPrice = yesterdayEndPrice;
        }

        public List<KLineBar> GetKLineCharts()
        {
            return DataTransfer_Tick2KLineGenerator.GenerateCharts(ticks, openTime, targetPeriod, yesterdayEndPrice);
        }

        public KLineData GetKLineData()
        {
            List<KLineBar> charts = DataTransfer_Tick2KLineGenerator.GenerateCharts(ticks, openTime, targetPeriod, yesterdayEndPrice);
            return GetCharts(charts);
        }

        public static KLineData GetCharts(List<KLineBar> charts)
        {
            KLineData data = new KLineData(charts.Count);
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
    }
}