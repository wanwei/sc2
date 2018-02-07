using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TimeLineData_Extend : ITimeLineData_Extend
    {
        private ITimeLineData timeLineData;

        private IList<double[]> startEndInfo;

        private IList<int> tradingTimeStartBarPos;

        private IList<int> tradingTimeEndBarPos;

        private ISet<int> set_TradingTimeStartBarPos;

        public TimeLineData_Extend(ITimeLineData timeLineData, IList<double[]> tradingTimeStartEndInfo)
        {
            this.timeLineData = timeLineData;
            this.startEndInfo = tradingTimeStartEndInfo;

            this.tradingTimeStartBarPos = new List<int>();
            this.tradingTimeEndBarPos = new List<int>();
            this.set_TradingTimeStartBarPos = new HashSet<int>();
            ISet<double> set_TradingTimeStart = GetSet_TradingTimeStart(tradingTimeStartEndInfo);
            for (int i = 0; i < timeLineData.Length; i++)
            {
                double time = timeLineData.Arr_Time[i];
                if (set_TradingTimeStart.Contains(time))
                {
                    this.tradingTimeStartBarPos.Add(i);
                    this.set_TradingTimeStartBarPos.Add(i);
                    if (i != 0)
                        tradingTimeEndBarPos.Add(i - 1);
                }
            }
            tradingTimeEndBarPos.Add(timeLineData.Length - 1);
        }

        private ISet<double> GetSet_TradingTimeStart(IList<double[]> tradingTimeStartEndInfo)
        {
            ISet<double> set = new HashSet<double>();
            for (int i = 0; i < tradingTimeStartEndInfo.Count; i++)
            {
                set.Add(tradingTimeStartEndInfo[i][0]);
            }
            return set;
        }

        public IList<int> Arr_Hold
        {
            get
            {
                return timeLineData.Arr_Hold;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return timeLineData.Arr_Mount;
            }
        }

        public IList<float> Arr_Price
        {
            get
            {
                return timeLineData.Arr_Price;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return timeLineData.Arr_Time;
            }
        }

        public IList<float> Arr_UpPercent
        {
            get
            {
                return timeLineData.Arr_UpPercent;
            }
        }

        public IList<float> Arr_UpRange
        {
            get
            {
                return timeLineData.Arr_UpRange;
            }
        }

        public int BarPos
        {
            get
            {
                return timeLineData.BarPos;
            }

            set
            {
                timeLineData.BarPos = value;
            }
        }

        public string Code
        {
            get
            {
                return timeLineData.Code;
            }
        }

        public int Date
        {
            get
            {
                return timeLineData.Date;
            }
        }

        public int Hold
        {
            get
            {
                return timeLineData.Hold;
            }
        }

        public int Length
        {
            get
            {
                return timeLineData.Length;
            }
        }

        public int Mount
        {
            get
            {
                return timeLineData.Mount;
            }
        }

        public float Price
        {
            get
            {
                return timeLineData.Price;
            }
        }

        public double Time
        {
            get
            {
                return timeLineData.Time;
            }
        }

        public float UpPercent
        {
            get
            {
                return this.timeLineData.UpPercent;
            }
        }

        public float UpRange
        {
            get
            {
                return this.timeLineData.UpRange;
            }
        }

        public float YesterdayEnd
        {
            get
            {
                return this.timeLineData.YesterdayEnd;
            }
        }

        public ITimeLineBar GetBar(int index)
        {
            return this.timeLineData.GetBar(index);
        }

        public ITimeLineBar GetCurrentBar()
        {
            return this.timeLineData.GetCurrentBar();
        }

        public int IndexOfTime(double time)
        {
            return this.timeLineData.IndexOfTime(time);
        }

        public void SetBarPosByTime(double time)
        {
            this.timeLineData.SetBarPosByTime(time);
        }
        public string ToString(int index)
        {
            return this.timeLineData.ToString(index);
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            if (barPos == timeLineData.Length - 1)
                return true;
            return IsTradingTimeStart(barPos + 1);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            return set_TradingTimeStartBarPos.Contains(barPos);
        }

        public IList<int> TradingTimeEndBarPoses
        {
            get
            {
                return tradingTimeEndBarPos ;
            }
        }
    }
}
