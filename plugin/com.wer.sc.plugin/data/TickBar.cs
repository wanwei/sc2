using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TickBar :TickBar_Abstract, ITickBar
    {
        private string code;

        private int add;

        private int buyMount;

        private float buyPrice;

        private int hold;

        private bool isBuy;

        private int mount;

        private float price;

        private int sellMount;

        private float sellPrice;

        private double time;

        private int totalMount;

        public override string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public override int Add
        {
            get
            {
                return add;
            }

            set
            {
                add = value;
            }
        }

        public override int BuyMount
        {
            get
            {
                return buyMount;
            }

            set
            {
                buyMount = value;
            }
        }

        public override float BuyPrice
        {
            get
            {
                return buyPrice;
            }

            set
            {
                buyPrice = value;
            }
        }

        public override int Hold
        {
            get
            {
                return hold;
            }

            set
            {
                hold = value;
            }
        }

        public override bool IsBuy
        {
            get
            {
                return isBuy;
            }

            set
            {
                isBuy = value;
            }
        }

        public override int Mount
        {
            get
            {
                return mount;
            }

            set
            {
                mount = value;
            }
        }

        public override float Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public override int SellMount
        {
            get
            {
                return sellMount;
            }

            set
            {
                sellMount = value;
            }
        }

        public override float SellPrice
        {
            get
            {
                return sellPrice;
            }

            set
            {
                sellPrice = value;
            }
        }

        public override double Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public override int TotalMount
        {
            get
            {
                return totalMount;
            }

            set
            {
                totalMount = value;
            }
        }
    }
}
