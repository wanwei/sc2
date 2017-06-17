using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    /// <summary>
    /// 当日K线数据
    /// </summary>
    public class KLineData_Present : KLineData
    {
        private IKLineData historyKLineData;

        private bool isFirstReceive = true;
        private int presentBarPos;

        private IList<double> list_Time;
        private IList<float> list_Start;
        private IList<float> list_High;
        private IList<float> list_Low;
        private IList<float> list_End;
        private IList<int> list_Mount;
        private IList<float> list_Money;
        private IList<int> list_Hold;

        public KLineData_Present(IKLineData historyKLineData, List<double> todayKlineTimeList, KLinePeriod period) : base(todayKlineTimeList.Count)
        {
            this.Period = period;
            int length = todayKlineTimeList.Count;
            this.arr_time = todayKlineTimeList.ToArray();
            this.arr_start = new float[length];
            this.arr_high = new float[length];
            this.arr_low = new float[length];
            this.arr_end = new float[length];
            this.arr_mount = new int[length];
            this.arr_money = new float[length];
            this.arr_hold = new int[length];

            this.historyKLineData = historyKLineData;

            this.list_Time = new ReadOnlyList_Merge<double>(historyKLineData.Arr_Time, arr_time);
            this.list_Start = new ReadOnlyList_Merge<float>(historyKLineData.Arr_Start, arr_start);
            this.list_High = new ReadOnlyList_Merge<float>(historyKLineData.Arr_High, arr_high);
            this.list_Low = new ReadOnlyList_Merge<float>(historyKLineData.Arr_Low, arr_low);
            this.list_End = new ReadOnlyList_Merge<float>(historyKLineData.Arr_End, arr_end);
            this.list_Mount = new ReadOnlyList_Merge<int>(historyKLineData.Arr_Mount, arr_mount);
            this.list_Money = new ReadOnlyList_Merge<float>(historyKLineData.Arr_Money, arr_money);
            this.list_Hold = new ReadOnlyList_Merge<int>(historyKLineData.Arr_Hold, arr_hold);
            this.BarPos = historyKLineData.Length;
        }

        public void Receive(ITickBar tickBar)
        {
            if (isFirstReceive)
            {
                InitBar(tickBar);
                isFirstReceive = false;
                return;
            }

            if (PresentBarPos == arr_time.Length - 1)
            {
                AddTickToBar(tickBar);
                return;
            }

            double nextTime = arr_time[PresentBarPos + 1];
            if (tickBar.Time >= nextTime)
            {
                PresentBarPos++;
                InitBar(tickBar);
            }
            else
            {
                AddTickToBar(tickBar);
            }
        }

        private void InitBar(ITickBar tickBar)
        {
            arr_start[PresentBarPos] = tickBar.Price;
            arr_high[PresentBarPos] = tickBar.Price;
            arr_low[PresentBarPos] = tickBar.Price;
            arr_end[PresentBarPos] = tickBar.Price;

            arr_mount[PresentBarPos] = tickBar.Mount;
            //arr_money[PresentBarPos] = tickBar.mon
            arr_hold[PresentBarPos] = tickBar.Hold;
        }

        private void AddTickToBar(ITickBar tickBar)
        {
            if (arr_high[PresentBarPos] < tickBar.Price)
                arr_high[PresentBarPos] = tickBar.Price;
            if (arr_low[PresentBarPos] > tickBar.Price)
                arr_low[PresentBarPos] = tickBar.Price;
            arr_end[PresentBarPos] = tickBar.Price;
            arr_mount[PresentBarPos] += tickBar.Mount;
            arr_hold[PresentBarPos] = tickBar.Hold;
        }

        public int PresentBarPos
        {
            get
            {
                return presentBarPos;
            }

            set
            {
                presentBarPos = value;
                BarPos = historyKLineData.Length + presentBarPos;
            }
        }

        #region 得到完整数据        

        public override IList<double> Arr_Time
        {
            get
            {
                return list_Time;
            }
        }

        public override IList<float> Arr_Start
        {
            get
            {
                return list_Start;
            }
        }

        public override IList<float> Arr_High
        {
            get
            {
                return list_High;
            }
        }

        public override IList<float> Arr_Low
        {
            get
            {
                return list_Low;
            }
        }

        public override IList<float> Arr_End
        {
            get
            {
                return list_End;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return list_Mount;
            }
        }

        public override IList<float> Arr_Money
        {
            get
            {
                return list_Money;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return list_Hold;
            }
        }
        
        #endregion
    }
}