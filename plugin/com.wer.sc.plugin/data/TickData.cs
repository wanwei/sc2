using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// Tick数据实现类，表示一天的Tick数据
    /// 
    /// 当Tick数据从数据中心获取后，会初始化成该类
    /// </summary>
    public class TickData : TickData_Abstract
    {
        // 交易时间
        public double[] arr_time;

        // 交易价格
        public float[] arr_price;

        // 交易量
        public int[] arr_mount;

        // 到现在为止总成交量
        public int[] arr_totalMount;

        // 持仓增减
        public int[] arr_add;

        // 买价
        public float[] arr_buyPrice;

        // 买量
        public int[] arr_buyMount;

        // 卖价
        public float[] arr_sellPrice;

        // 卖量
        public int[] arr_sellMount;

        // 买OR卖
        public Boolean[] arr_isBuy;

        private int[] arr_hold;
        
        public TickData(int length)
        {
            this.arr_time = new double[length];
            this.arr_price = new float[length];
            this.arr_mount = new int[length];
            this.arr_totalMount = new int[length];
            this.arr_add = new int[length];
            this.arr_buyPrice = new float[length];
            this.arr_buyMount = new int[length];
            this.arr_sellPrice = new float[length];
            this.arr_sellMount = new int[length];
            this.arr_isBuy = new bool[length];
        }

        public TickData SubData(int start, int end)
        {
            TickData data = this;
            TickData d1 = new TickData(end - start + 1);
            for (int i = start; i <= end; i++)
            {
                d1.arr_time[i - start] = data.arr_time[i];
                d1.arr_price[i - start] = data.arr_price[i];
                d1.arr_mount[i - start] = data.arr_mount[i];
                d1.arr_totalMount[i - start] = data.arr_totalMount[i];
                d1.arr_add[i - start] = data.arr_add[i];
                d1.arr_buyPrice[i - start] = data.arr_buyPrice[i];
                d1.arr_buyMount[i - start] = data.arr_buyMount[i];
                d1.arr_sellPrice[i - start] = data.arr_sellPrice[i];
                d1.arr_sellMount[i - start] = data.arr_sellMount[i];
                d1.arr_isBuy[i - start] = data.arr_isBuy[i];
            }
            return d1;
        }

        #region 完整数据

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
                if (arr_hold != null)
                    return arr_hold;
                this.arr_hold = new int[Length];
                int currentHold = 0;
                for (int i = 0; i < arr_add.Length; i++)
                {
                    currentHold += arr_add[i];
                    arr_hold[i] = currentHold;
                }
                return arr_hold;
            }
        }

        public override IList<Boolean> Arr_IsBuy { get { return arr_isBuy; } }

        #endregion
    }
}