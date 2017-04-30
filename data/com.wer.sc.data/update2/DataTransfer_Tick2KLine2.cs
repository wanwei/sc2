using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    /// <summary>
    /// 将tick数据转换成K线数据
    /// </summary>
    public class DataTransfer_Tick2KLine2
    {
        private TickData tickData;
        private IKLineData lastKLineData;
        private List<double> klineTimes;

        private KLineData klineData;
        private int currentHold = 0;

        private Object lockObj = new object();

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="date">指定要transfer的日期</param>
        /// <param name="data">要转换的tick数据</param>
        /// <param name="openDateReader">开盘日读取器，传入该接口是为了得到</param>
        /// <param name="opentime"></param>
        /// <param name="targetPeriod"></param>
        /// <param name="lastKLineData"></param>
        public DataTransfer_Tick2KLine2(TickData data, IKLineData lastKLineData, OpenTimeUtilsArgs openTimeUtilsArgs)
        {
            this.tickData = data;
            this.lastKLineData = lastKLineData;
            this.klineTimes = OpenTimeUtils.GetKLineTimeList(openTimeUtilsArgs);
        }

        public KLineData CalcKLineData()
        {
            lock (lockObj)
            {
                if (this.klineData != null)
                    return klineData;

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
            double tickTime = tickData.arr_time[startTickIndex];
            while (tickTime < endTime)
            {
                startTickIndex++;
                if (startTickIndex >= tickData.Length)
                    return startTickIndex - 1;

                tickTime = tickData.arr_time[startTickIndex];
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
                int currentMount = tickData.arr_mount[i];
                float price = tickData.arr_price[i];
                high = high < price ? price : high;
                low = low > price ? price : low;
                mount += currentMount;
                //money += currentMount * price;
                currentHold += tickData.arr_add[i];
            }
            klineBar.Code = tickData.Code;
            klineBar.Time = klineTimes[klineIndex];
            klineBar.Start = tickData.arr_price[startTickIndex];
            klineBar.High = high;
            klineBar.Low = low;
            klineBar.End = tickData.arr_price[endTickIndex];
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
                if (lastKLineData == null)
                    lastPrice = tickData.Arr_Price[0];
                else
                    lastPrice = lastKLineData.Arr_End[lastKLineData.Length - 1];
            }
            else
                lastPrice = klineData.Arr_End[currentKLineIndex - 1];

            int lastHold = 0;
            if (currentKLineIndex == 0)
            {
                if (lastKLineData == null)
                    lastHold = tickData.Arr_Hold[0];
                else
                    lastHold = lastKLineData.Arr_Hold[lastKLineData.Length - 1];
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

        public static KLineData Transfer(TickData data, IKLineData lastKLineData, OpenTimeUtilsArgs args)
        {
            DataTransfer_Tick2KLine2 transfer = new DataTransfer_Tick2KLine2(data, lastKLineData, args);
            return transfer.CalcKLineData();
        }

        public static KLineData Transfer(int date, TickData data, ITradingDayReader openDateReader, List<double[]> opentime, KLinePeriod targetPeriod, KLineData lastKLineData)
        {
            DataTransfer_Tick2KLine2 transfer = new DataTransfer_Tick2KLine2(data, lastKLineData, new OpenTimeUtilsArgs(date, openDateReader, opentime, targetPeriod));
            return transfer.CalcKLineData();
        }
    }
}