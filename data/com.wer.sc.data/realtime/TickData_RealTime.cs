using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    public class TickData_RealTime : ITickData_Extend
    {
        private ITickData_Extend tickData;

        private int barPos;

        public TickData_RealTime(ITickData_Extend tickData)
        {
            this.tickData = tickData;
        }

        public int TradingDay
        {
            get
            {
                return tickData.TradingDay;
            }
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }

            set
            {
                this.barPos = value;
            }
        }

        public int Length
        {
            get
            {
                return tickData.Length;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return tickData.Arr_Time;
            }
        }

        public IList<float> Arr_Price
        {
            get
            {
                return tickData.Arr_Price;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return tickData.Arr_Mount;
            }
        }

        public IList<int> Arr_TotalMount
        {
            get
            {
                return tickData.Arr_TotalMount;
            }
        }

        public IList<int> Arr_Add
        {
            get
            {
                return tickData.Arr_Add;
            }
        }

        public IList<float> Arr_BuyPrice
        {
            get
            {
                return tickData.Arr_BuyPrice;
            }
        }

        public IList<int> Arr_BuyMount
        {
            get
            {
                return tickData.Arr_BuyMount;
            }
        }

        public IList<float> Arr_SellPrice
        {
            get
            {
                return tickData.Arr_SellPrice;
            }
        }

        public IList<int> Arr_SellMount
        {
            get
            {
                return tickData.Arr_SellMount;
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

        public string Code
        {
            get
            {
                return tickData.Code;
            }
        }

        public double Time
        {
            get
            {
                return tickData.Arr_Time[barPos];
            }
        }

        public float Price
        {
            get
            {
                return tickData.Arr_Price[barPos];
            }
        }

        public int Mount
        {
            get
            {
                return tickData.Arr_Mount[barPos];
            }
        }

        public int TotalMount
        {
            get
            {
                return tickData.Arr_TotalMount[barPos];
            }
        }

        public int Add
        {
            get
            {
                return tickData.Arr_Add[barPos];
            }
        }

        public float BuyPrice
        {
            get
            {
                return tickData.Arr_BuyPrice[barPos];
            }
        }

        public int BuyMount
        {
            get
            {
                return tickData.Arr_BuyMount[barPos];
            }
        }

        public float SellPrice
        {
            get
            {
                return tickData.Arr_SellPrice[barPos];
            }
        }

        public int SellMount
        {
            get
            {
                return tickData.Arr_SellMount[barPos];
            }
        }

        public int Hold
        {
            get
            {
                return tickData.Arr_Hold[barPos];
            }
        }

        public bool IsBuy
        {
            get
            {
                return tickData.Arr_IsBuy[barPos];
            }
        }

        public IList<int> TradingTimeEndBarPoses
        {
            get
            {
                return tickData.TradingTimeEndBarPoses;
            }
        }

        public ITickBar GetCurrentBar()
        {
            return this;
        }

        public ITickBar GetBar(int index)
        {
            return tickData.GetBar(index);
        }

        public string ToString(int index)
        {
            return tickData.ToString(index);
        }

        public override string ToString()
        {
            return tickData.ToString(barPos);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            return tickData.IsTradingTimeStart(barPos);
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            return tickData.IsTradingTimeEnd(barPos);
        }
    }
}
