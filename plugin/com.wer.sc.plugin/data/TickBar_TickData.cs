using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// ITickBar接口的实现类
    /// 该类是只读的，用来从TickData里获取数据
    /// </summary>
    public class TickBar_TickData : TickBar_Abstract, ITickBar
    {
        private ITickData data;

        private int index;

        public TickBar_TickData(ITickData data, int index)
        {
            this.data = data;
            this.index = index;
        }

        public override string Code
        {
            get { return data.Code; }
            set { throw new NotImplementedException(); }
        }

        public override int Add
        {
            get
            {
                return data.Arr_Add[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int BuyMount
        {
            get
            {
                return data.Arr_BuyMount[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float BuyPrice
        {
            get
            {
                return data.Arr_BuyPrice[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Hold
        {
            get
            {
                return data.Arr_Hold[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override bool IsBuy
        {
            get
            {
                return data.Arr_IsBuy[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Mount
        {
            get
            {
                return data.Arr_Mount[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Price
        {
            get
            {
                return data.Arr_Price[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int SellMount
        {
            get
            {
                return data.Arr_SellMount[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float SellPrice
        {
            get
            {
                return data.Arr_SellPrice[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override double Time
        {
            get
            {
                return data.Arr_Time[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int TotalMount
        {
            get
            {
                return data.Arr_TotalMount[index];
            }
            set { throw new NotImplementedException(); }
        }
    }
}
