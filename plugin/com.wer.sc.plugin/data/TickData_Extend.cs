using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TickData_Extend : ITickData_Extend
    {
        private ITickData tickData;

        private IList<int> tradingTimeEndBarPoses = new List<int>();

        private ISet<int> set_TradingTimeEndBar = new HashSet<int>();

        public TickData_Extend(ITickData tickData, ITradingTime tradingTime)
        {
            this.tickData = tickData;
            for (int i = 0; i < tradingTime.PeriodCount - 1; i++)
            {
                int barPos = FindEndBarPos(tickData, tradingTime, i);
                tradingTimeEndBarPoses.Add(barPos);
                set_TradingTimeEndBar.Add(barPos);
            }
            tradingTimeEndBarPoses.Add(tickData.Length - 1);
            set_TradingTimeEndBar.Add(tickData.Length - 1);
        }

        private int FindEndBarPos(ITickData tickData, ITradingTime tradingTime, int periodIndex)
        {
            double endTime = tradingTime.GetPeriodTime(periodIndex)[1];
            double nextStartTime = tradingTime.GetPeriodTime(periodIndex + 1)[0];
            endTime = (endTime + nextStartTime) / 2;
            return TimeIndeierUtils.IndexOfTime_Tick(tickData, endTime);
        }

        public int Add
        {
            get
            {
                return tickData.Add;
            }
        }

        public IList<int> Arr_Add
        {
            get
            {
                return tickData.Arr_Add;
            }
        }

        public IList<int> Arr_BuyMount
        {
            get
            {
                return tickData.Arr_BuyMount;
            }
        }

        public IList<float> Arr_BuyPrice
        {
            get
            {
                return tickData.Arr_BuyPrice;
            }
        }

        public IList<int> Arr_Hold
        {
            get
            {
                return tickData.Arr_Hold;
            }
        }

        public IList<bool> Arr_IsBuy
        {
            get
            {
                return tickData.Arr_IsBuy;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return tickData.Arr_Mount;
            }
        }

        public IList<float> Arr_Price
        {
            get
            {
                return tickData.Arr_Price;
            }
        }

        public IList<int> Arr_SellMount
        {
            get
            {
                return tickData.Arr_SellMount;
            }
        }

        public IList<float> Arr_SellPrice
        {
            get
            {
                return tickData.Arr_SellPrice;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return tickData.Arr_Time;
            }
        }

        public IList<int> Arr_TotalMount
        {
            get
            {
                return tickData.Arr_TotalMount;
            }
        }

        public int BarPos
        {
            get
            {
                return tickData.BarPos;
            }

            set
            {
                tickData.BarPos = value;
            }
        }

        public int BuyMount
        {
            get
            {
                return tickData.BuyMount;
            }
        }

        public float BuyPrice
        {
            get
            {
                return tickData.BuyPrice;
            }
        }

        public string Code
        {
            get
            {
                return tickData.Code;
            }
        }

        public int Hold
        {
            get
            {
                return tickData.Hold;
            }
        }

        public bool IsBuy
        {
            get
            {
                return tickData.IsBuy;
            }
        }

        public int Length
        {
            get
            {
                return tickData.Length;
            }
        }

        public int Mount
        {
            get
            {
                return tickData.Mount;
            }
        }

        public float Price
        {
            get
            {
                return tickData.Price;
            }
        }

        public int SellMount
        {
            get
            {
                return tickData.SellMount;
            }
        }

        public float SellPrice
        {
            get
            {
                return tickData.SellPrice;
            }
        }

        public double Time
        {
            get
            {
                return tickData.Time;
            }
        }

        public int TotalMount
        {
            get
            {
                return tickData.TotalMount;
            }
        }

        public int TradingDay
        {
            get
            {
                return tickData.TradingDay;
            }
        }

        public ITickBar GetBar(int index)
        {
            return tickData.GetBar(index);
        }

        public ITickBar GetCurrentBar()
        {
            return tickData.GetCurrentBar();
        } 

        public IList<int> TradingTimeEndBarPoses
        {
            get { return tradingTimeEndBarPoses; }
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            return set_TradingTimeEndBar.Contains(barPos);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            if (barPos == 0)
                return true;
            return IsTradingTimeEnd(barPos - 1);
        }

        public string ToString(int index)
        {
            return tickData.ToString(index);
        }

        public override string ToString()
        {
            return tickData.ToString();
        }
    }
}