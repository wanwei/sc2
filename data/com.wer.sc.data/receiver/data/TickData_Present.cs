using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    public class TickData_Present : TickData_Abstract
    {
        // 交易时间
        public List<double> arr_time;

        // 交易价格
        public List<float> arr_price;

        // 交易量
        public List<int> arr_mount;

        // 到现在为止总成交量
        public List<int> arr_totalMount;

        // 持仓增减
        public List<int> arr_add;

        // 买价
        public List<float> arr_buyPrice;

        // 买量
        public List<int> arr_buyMount;

        // 卖价
        public List<float> arr_sellPrice;

        // 卖量
        public List<int> arr_sellMount;

        // 买OR卖
        public List<Boolean> arr_isBuy;

        private List<int> arr_hold;

        public TickData_Present(int capacity)
        {
            this.arr_time = new List<double>(capacity);
            this.arr_price = new List<float>(capacity);
            this.arr_mount = new List<int>(capacity);
            this.arr_totalMount = new List<int>(capacity);
            this.arr_add = new List<int>(capacity);
            this.arr_buyPrice = new List<float>(capacity);
            this.arr_buyMount = new List<int>(capacity);
            this.arr_sellPrice = new List<float>(capacity);
            this.arr_sellMount = new List<int>(capacity);
            this.arr_isBuy = new List<bool>(capacity);
            this.arr_hold = new List<int>(capacity);
        }

        public void Recieve(ITickBar tickBar)
        {
            this.arr_time.Add(tickBar.Time);
            this.arr_price.Add(tickBar.Price);
            this.arr_mount.Add(tickBar.Mount);
            this.arr_totalMount.Add(tickBar.TotalMount);
            this.arr_add.Add(tickBar.Add);
            this.arr_buyPrice.Add(tickBar.BuyPrice);
            this.arr_buyMount.Add(tickBar.BuyMount);
            this.arr_sellPrice.Add(tickBar.SellPrice);
            this.arr_sellMount.Add(tickBar.SellMount);
            this.arr_isBuy.Add(tickBar.IsBuy);
            if (this.arr_hold.Count == 0)
                this.arr_hold.Add(tickBar.Add);
            else
                this.arr_hold.Add(this.arr_hold[this.arr_hold.Count - 1] + tickBar.Add);
            this.BarPos = this.arr_time.Count - 1;
        }

        public override IList<double> Arr_Time { get { return arr_time; } }

        public override IList<float> Arr_Price { get { return arr_price; } }

        public override IList<int> Arr_Mount { get { return arr_mount; } }

        public override IList<int> Arr_TotalMount { get { return arr_totalMount; } }

        public override IList<int> Arr_Add { get { return arr_add; } }

        public override IList<float> Arr_BuyPrice { get { return arr_buyPrice; } }

        public override IList<int> Arr_BuyMount { get { return arr_buyMount; } }

        public override IList<float> Arr_SellPrice { get { return arr_sellPrice; } }

        public override IList<int> Arr_SellMount { get { return arr_sellMount; } }

        public override IList<int> Arr_Hold
        {
            get
            {
                return arr_hold;
            }
        }

        public override IList<Boolean> Arr_IsBuy { get { return arr_isBuy; } }
    }
}