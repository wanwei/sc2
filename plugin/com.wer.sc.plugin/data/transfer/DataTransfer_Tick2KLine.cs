using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    /// <summary>
    /// 将tick数据转换成K线数据
    /// </summary>
    public class DataTransfer_Tick2KLine
    {
        /// <summary>
        /// 将一天的Tick数据转换成K线数据
        /// </summary>
        /// <param name="tickData">一天的tick数据</param>
        /// <param name="klineTimeList">k线时间队列，如1分钟K线，那么需要将</param>
        /// <param name="yesterdayEndPrice">昨日的收盘价</param>
        /// <param name="yesterdayEndHold">昨日的持仓</param>
        /// <returns></returns>
        public static IKLineData Transfer(ITickData tickData, IList<double> klineTimeList, float yesterdayEndPrice, int yesterdayEndHold)
        {
            DataTransfer_Tick2KLine transfer = new DataTransfer_Tick2KLine(tickData, klineTimeList, yesterdayEndPrice, yesterdayEndHold);
            return transfer.CalcKLineData();
        }

        public static IKLineData Transfer(ITickData[] tickDataArray, IList<double>[] klineTimeListArray, float lastEndPrice, int lastEndHold)
        {
            if (tickDataArray.Length != klineTimeListArray.Length)
                throw new Exception("");
            List<IKLineData> klineDataList = new List<IKLineData>(tickDataArray.Length);
            for (int i = 0; i < tickDataArray.Length; i++)
            {
                ITickData tickData = tickDataArray[i];
                IList<double> klineTimeList = klineTimeListArray[i];
                IKLineData klineData = Transfer(tickData, klineTimeList, lastEndPrice, lastEndHold);
                lastEndPrice = klineData.Arr_End[klineData.Length - 1];
                lastEndHold = klineData.Arr_Hold[klineData.Length - 1];
                klineDataList.Add(klineData);
            }
            return KLineData.Merge(klineDataList);
        }

        private ITickData tickData;
        private IList<double> klineTimes;

        private float lastEndPrice;
        private int lastEndHold;
        private KLineData klineData;
        private int currentHold = 0;

        private Object lockObj = new object();

        /// <summary>
        /// 数据转换器
        /// </summary>
        /// <param name="data">待转换的tick数据</param>
        /// <param name="klineTimes">目标K线时间队列，需要传入完整时间，如5分钟K线，要传入20140106090000,20140106090500......</param>
        /// <param name="lastEndPrice"></param>
        /// <param name="lastEndHold"></param>
        public DataTransfer_Tick2KLine(ITickData data, IList<double> klineTimes, float lastEndPrice, int lastEndHold)
        {
            this.tickData = data;
            this.klineTimes = klineTimes;
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;
        }

        public KLineData CalcKLineData()
        {
            lock (lockObj)
            {
                if (this.klineData != null)
                    return klineData;

                if (this.tickData == null)
                {
                    this.klineData = GetEmptyKLineData(this.klineTimes, lastEndPrice, lastEndHold);
                    return this.klineData;
                }

                this.klineData = new KLineData(this.klineTimes.Count);

                int startTickIndex = 0;
                int endTickIndex = 0;
                KLineBar klineBar;
                for (int i = 0; i < klineTimes.Count - 1; i++)
                {
                    endTickIndex = CalcCurrentTickEndIndex(startTickIndex, klineTimes[i + 1]);
                    klineBar = CalcKLineBar(i, startTickIndex, endTickIndex);
                    klineBar.Copy2KLineData(klineData, i);
                    startTickIndex = endTickIndex + 1;
                }

                klineBar = CalcKLineBar(klineData.Length - 1, startTickIndex, tickData.Length - 1);
                klineBar.Copy2KLineData(klineData, klineData.Length - 1);

                return this.klineData;
            }
        }

        private int CalcCurrentTickEndIndex(int startTickIndex, double endTime)
        {
            if (startTickIndex >= tickData.Arr_Time.Count)
                return tickData.Arr_Time.Count - 1;
            double tickTime = tickData.Arr_Time[startTickIndex];
            while (tickTime < endTime)
            {
                startTickIndex++;
                if (startTickIndex >= tickData.Length)
                    return startTickIndex - 1;

                tickTime = tickData.Arr_Time[startTickIndex];
            }
            return startTickIndex - 1;
        }

        private KLineBar CalcKLineBar(int klineIndex, int startTickIndex, int endTickIndex)
        {
            if (endTickIndex < startTickIndex)
            {
                return CalcCurrentChart_EmptyChart(klineIndex);
            }
            KLineBar klineBar = new KLineBar();
            float high = 0;
            float low = float.MaxValue;
            int mount = 0;
            float money = 0;
            for (int i = startTickIndex; i <= endTickIndex; i++)
            {
                int currentMount = tickData.Arr_Mount[i];
                float price = tickData.Arr_Price[i];
                high = high < price ? price : high;
                low = low > price ? price : low;
                mount += currentMount;
                //money += currentMount * price;
                currentHold += tickData.Arr_Add[i];
            }
            klineBar.Code = tickData.Code;
            klineBar.Time = klineTimes[klineIndex];
            klineBar.Start = tickData.Arr_Price[startTickIndex];
            klineBar.High = high;
            klineBar.Low = low;
            klineBar.End = tickData.Arr_Price[endTickIndex];
            klineBar.Mount = mount;
            klineBar.Money = money;
            klineBar.Hold = currentHold;
            return klineBar;
        }

        private KLineBar CalcCurrentChart_EmptyChart(int currentKLineIndex)
        {
            KLineBar klineBar = new KLineBar();
            klineBar.Time = klineTimes[currentKLineIndex];
            float lastPrice = 0;
            if (currentKLineIndex == 0)
            {
                if (lastEndPrice < 0)
                    lastPrice = tickData.Arr_Price[0];
                else
                    lastPrice = lastEndPrice;
            }
            else
                lastPrice = klineData.Arr_End[currentKLineIndex - 1];

            int lastHold = 0;
            if (currentKLineIndex == 0)
            {
                if (lastEndHold < 0)
                    lastHold = tickData.Arr_Hold[0];
                else
                    lastHold = lastEndHold;
            }
            else
                lastHold = klineData.Arr_Hold[currentKLineIndex - 1];

            klineBar.Start = lastPrice;
            klineBar.High = lastPrice;
            klineBar.Low = lastPrice;
            klineBar.End = lastPrice;
            klineBar.Mount = 0;
            klineBar.Money = 0;
            klineBar.Hold = lastHold;
            return klineBar;
        }

        private static KLineData GetEmptyKLineData(IList<double> klineTimes, float price, int hold)
        {
            KLineData klineData = new KLineData(klineTimes.Count);
            for (int i = 0; i < klineTimes.Count; i++)
            {
                klineData.arr_time[i] = klineTimes[i];
                klineData.arr_start[i] = price;
                klineData.arr_high[i] = price;
                klineData.arr_low[i] = price;
                klineData.arr_end[i] = price;
                klineData.arr_mount[i] = 0;
                klineData.arr_money[i] = 0;
                klineData.arr_hold[i] = hold;
            }
            return klineData;
        }
    }
}