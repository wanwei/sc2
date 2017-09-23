using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.strategy
{
    class StrategyTradeFee_Code : IXmlExchange
    {
        //代码
        private String code;

        //每手数量
        private int handCount;

        //最小的价格变化
        private double minPriceChange;

        //买入手续费
        private double buyFee;

        //卖出手续费
        private double sellFee;

        //手续费是否按照百分比收
        private Boolean isPercent = false;

        //保证金比率
        private double depositPercent;

        public string Code
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

        public int HandCount
        {
            get
            {
                return handCount;
            }

            set
            {
                handCount = value;
            }
        }

        public double MinPriceChange
        {
            get
            {
                return minPriceChange;
            }

            set
            {
                minPriceChange = value;
            }
        }

        public double BuyFee
        {
            get
            {
                return buyFee;
            }

            set
            {
                buyFee = value;
            }
        }

        public double SellFee
        {
            get
            {
                return sellFee;
            }

            set
            {
                sellFee = value;
            }
        }

        public bool IsPercent
        {
            get
            {
                return isPercent;
            }

            set
            {
                isPercent = value;
            }
        }

        public double DepositPercent
        {
            get
            {
                return depositPercent;
            }

            set
            {
                depositPercent = value;
            }
        }

        public StrategyTradeFee_Code()
        {
        }

        public StrategyTradeFee_Code(String contract, int handCount, double minPriceChange, double buyFee,
                double sellFeee, Boolean isPercent, double depositPercent)
        {
            this.code = contract;
            this.handCount = handCount;
            this.minPriceChange = minPriceChange;
            this.buyFee = buyFee;
            this.sellFee = sellFeee;
            this.isPercent = isPercent;
            this.depositPercent = depositPercent;
        }

        public void Save(XmlElement elem)
        {
            elem.SetAttribute("code", Code);
            elem.SetAttribute("handCount", HandCount + "");
            elem.SetAttribute("minPriceChange", MinPriceChange + "");
            elem.SetAttribute("buyFee", BuyFee + "");
            elem.SetAttribute("sellFee", SellFee + "");
            elem.SetAttribute("isPercent", IsPercent + "");
            elem.SetAttribute("depositPercent", DepositPercent + "");
        }

        public void Load(XmlElement elem)
        {
            this.code = elem.GetAttribute("code");
            this.handCount = int.Parse(elem.GetAttribute("handCount"));
            this.minPriceChange = Double.Parse(elem.GetAttribute("minPriceChange"));
            this.buyFee = Double.Parse(elem.GetAttribute("buyFee"));
            this.sellFee = Double.Parse(elem.GetAttribute("sellFee"));
            this.isPercent = Boolean.Parse(elem.GetAttribute("isPercent"));
            this.depositPercent = Double.Parse(elem.GetAttribute("depositPercent"));
        }
    }
}
