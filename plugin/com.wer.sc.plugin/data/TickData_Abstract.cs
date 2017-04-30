using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// Tick数据的抽象实现类
    /// </summary>
    public abstract class TickData_Abstract : ITickData
    {
        private String code;

        protected int tradingDay = -1;

        private int barPos;

        public virtual int TradingDay
        {
            get
            {
                if (this.tradingDay > 0)
                    return this.tradingDay;
                this.tradingDay = (int)this.Arr_Time[this.Arr_Time.Count - 1];
                return tradingDay;
            }
        }

        public ITickBar GetCurrentBar()
        {
            return this;
        }

        public ITickBar GetBar(int index)
        {
            return new TickBar_TickData(this, index);
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }

            set
            {
                barPos = value;
            }
        }

        public int Length
        {
            get { return Arr_Time.Count; }
        }


        #region 实现ITickBar

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public double Time
        {
            get { return Arr_Time[BarPos]; }
        }

        public float Price
        {
            get { return Arr_Price[BarPos]; }
        }

        public int Mount
        {
            get { return Arr_Mount[BarPos]; }
        }

        public int TotalMount
        {
            get { return Arr_TotalMount[BarPos]; }
        }

        public int Add
        {
            get { return Arr_Add[BarPos]; }
        }

        public int Hold
        {
            get { return Arr_Hold[BarPos]; }
        }

        public float BuyPrice
        {
            get { return Arr_BuyPrice[BarPos]; }
        }

        public int BuyMount
        {
            get { return Arr_BuyMount[BarPos]; }
        }

        public float SellPrice
        {
            get { return Arr_SellPrice[BarPos]; }
        }

        public int SellMount
        {
            get { return Arr_SellMount[BarPos]; }
        }

        public Boolean IsBuy
        {
            get { return Arr_IsBuy[BarPos]; }
        }

        #endregion

        #region 完整数据

        public abstract IList<double> Arr_Time { get; }

        public abstract IList<float> Arr_Price { get; }

        public abstract IList<int> Arr_Mount { get; }

        public abstract IList<int> Arr_TotalMount { get; }

        public abstract IList<int> Arr_Add { get; }

        public abstract IList<float> Arr_BuyPrice { get; }

        public abstract IList<int> Arr_BuyMount { get; }

        public abstract IList<float> Arr_SellPrice { get; }

        public abstract IList<int> Arr_SellMount { get; }

        public abstract IList<int> Arr_Hold { get; }

        public abstract IList<Boolean> Arr_IsBuy { get; }

        #endregion

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Time).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(TotalMount).Append(",");
            sb.Append(Add).Append(",");
            sb.Append(BuyPrice).Append(",");
            sb.Append(BuyMount).Append(",");
            sb.Append(SellPrice).Append(",");
            sb.Append(SellMount).Append(",");
            sb.Append(IsBuy ? 1 : 0);
            return sb.ToString();
        }

        public String ToString(int i)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Arr_Time[i]).Append(",");
            sb.Append(Arr_Price[i]).Append(",");
            sb.Append(Arr_Mount[i]).Append(",");
            sb.Append(Arr_TotalMount[i]).Append(",");
            sb.Append(Arr_Add[i]).Append(",");
            sb.Append(Arr_BuyPrice[i]).Append(",");
            sb.Append(Arr_BuyMount[i]).Append(",");
            sb.Append(Arr_SellPrice[i]).Append(",");
            sb.Append(Arr_SellMount[i]).Append(",");
            sb.Append(Arr_IsBuy[i] ? 1 : 0);
            return sb.ToString();
        }
    }
}
