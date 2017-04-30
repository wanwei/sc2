using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.data
{
    public class TimeLineData_RealTime : TimeLineData_Abstract
    {
        /// <summary>
        /// 全日时间
        /// </summary>
        public double[] arr_time;

        /// <summary>
        /// 全日价格
        /// </summary>
        public float[] arr_price;

        /// <summary>
        /// 全日成交
        /// </summary>
        public int[] arr_mount;

        /// <summary>
        /// 全天持仓数据
        /// </summary>
        public int[] arr_hold;

        private float[] arr_UpPercent;

        private float[] arr_UpRange;

        public TimeLineData_RealTime(float yesterdayEnd, IList<double> timeList)
        {
            this.YesterdayEnd = yesterdayEnd;
            arr_time = new double[timeList.Count];
            arr_price = new float[timeList.Count];
            arr_mount = new int[timeList.Count];
            arr_hold = new int[timeList.Count];
            arr_UpRange = new float[timeList.Count];
            arr_UpPercent = new float[timeList.Count];

            timeList.CopyTo(arr_time, 0);
        }

        private bool isFirstReceive = true;

        public void Receive(ITickBar tickBar)
        {
            if (isFirstReceive)
            {
                InitBar(tickBar);
                isFirstReceive = false;
                return;
            }

            if (BarPos == arr_time.Length - 1)
            {
                AddTickToBar(tickBar);
                return;
            }

            double nextTime = arr_time[BarPos + 1];
            if (tickBar.Time >= nextTime)
            {
                BarPos++;
                InitBar(tickBar);
            }
            else
            {
                AddTickToBar(tickBar);
            }
            int m = 12;
            if (tickBar.Price != Price)
                m++;
        }

        private void InitBar(ITickBar tickBar)
        {
            float price = tickBar.Price;
            arr_price[BarPos] = price;
            arr_mount[BarPos] = tickBar.Mount;
            arr_hold[BarPos] = tickBar.Hold;
            arr_UpRange[BarPos] = (float)Math.Round(price - YesterdayEnd, 2);
            arr_UpPercent[BarPos] = (float)Math.Round((price - YesterdayEnd) / price * 100, 2);
        }

        private void AddTickToBar(ITickBar tickBar)
        {
            float price = tickBar.Price;
            arr_price[BarPos] = price;
            arr_mount[BarPos] += tickBar.Mount;
            arr_hold[BarPos] = tickBar.Hold;
            arr_UpRange[BarPos] = (float)Math.Round(price - YesterdayEnd, 2);
            arr_UpPercent[BarPos] = (float)Math.Round((price - YesterdayEnd) / price * 100, 2);
        }

        public override IList<double> Arr_Time
        {
            get
            {
                return arr_time;
            }
        }

        public override IList<float> Arr_Price
        {
            get
            {
                return arr_price;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return arr_mount;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return arr_hold;
            }
        }

        public override IList<float> Arr_UpPercent
        {
            get
            {
                //if (arr_UpPercent == null)
                //{
                //    arr_UpPercent = new float[Length];
                //    for (int i = 0; i < Length; i++)
                //    {
                //        float p = arr_price[i];
                //        arr_UpPercent[i] = (float)Math.Round((p - YesterdayEnd) / p * 100, 2);
                //    }
                //}
                //return arr_UpPercent;
                return arr_UpPercent;
            }
        }

        public override IList<float> Arr_UpRange
        {
            get
            {
                //if (arr_UpRange == null)
                //{
                //    arr_UpRange = new float[Length];
                //    for (int i = 0; i < Length; i++)
                //    {
                //        float p = arr_price[i];
                //        arr_UpRange[i] = (float)Math.Round(p - YesterdayEnd, 2);
                //    }
                //}
                return arr_UpRange;
            }
        }
    }
}
