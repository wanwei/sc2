using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.ana
{
    public class KLineTradeFee
    {
        //代码
        private String code;

        //每手数量
        private int handCount;

        //最小的价格变化
        private float minPriceChange;

        //买入手续费
        private float buyFee;

        //卖出手续费
        private float sellFee;

        //手续费是否按照百分比收
        private bool isPercent = false;

        //保证金比率
        private float depositPercent;

        public KLineTradeFee()
        {
        }

        public KLineTradeFee(String contract, int handCount, float minPriceChange, float buyFee, float sellFeee,
                bool isPercent, float depositPercent)
        {
            this.code = contract;
            this.handCount = handCount;
            this.minPriceChange = minPriceChange;
            this.buyFee = buyFee;
            this.sellFee = sellFeee;
            this.isPercent = isPercent;
            this.depositPercent = depositPercent;
        }

        public String getContract()
        {
            return code;
        }

        public void setContract(String contract)
        {
            this.code = contract;
        }

        public int getHandCount()
        {
            return handCount;
        }

        public void setHandCount(int handCount)
        {
            this.handCount = handCount;
        }

        public float getMinPriceChange()
        {
            return minPriceChange;
        }

        public void setMinPriceChange(float minPriceChange)
        {
            this.minPriceChange = minPriceChange;
        }

        public float getBuyFee()
        {
            return buyFee;
        }

        public void setBuyFee(float buyFee)
        {
            this.buyFee = buyFee;
        }

        public float getSellFee()
        {
            return sellFee;
        }

        public void setSellFee(float sellFee)
        {
            this.sellFee = sellFee;
        }

        public bool isPercents()
        {
            return isPercent;
        }

        public void setPercent(bool isPercent)
        {
            this.isPercent = isPercent;
        }

        public float getDepositPercent()
        {
            return depositPercent;
        }

        public void setDepositPercent(float depositPercent)
        {
            this.depositPercent = depositPercent;
        }

        public float calcMoney(int cnt, float price)
        {
            return cnt * (price * getHandCount() * (getDepositPercent() / 100));
        }

        public float calcMoney(bool isOpen, int cnt, float price)
        {
            float handFee = this.getBuyFee();
            if (isOpen)
                return cnt * (price * getHandCount() * (getDepositPercent() / 100) + handFee);
            else
                return cnt * (price * getHandCount() * (getDepositPercent() / 100) - handFee);
        }

        public void saveToXml(XmlElement elem)
        {
            elem.SetAttribute("code", code);
            elem.SetAttribute("handCount", handCount + "");
            elem.SetAttribute("minPriceChange", minPriceChange + "");
            elem.SetAttribute("buyFee", buyFee + "");
            elem.SetAttribute("sellFee", sellFee + "");
            elem.SetAttribute("isPercent", isPercent + "");
            elem.SetAttribute("depositPercent", depositPercent + "");
        }

        public void loadFromXml(XmlElement elem)
        {
            this.code = elem.GetAttribute("code");
            this.handCount = int.Parse(elem.GetAttribute("handCount"));
            this.minPriceChange = float.Parse(elem.GetAttribute("minPriceChange"));
            this.buyFee = float.Parse(elem.GetAttribute("buyFee"));
            this.sellFee = float.Parse(elem.GetAttribute("sellFee"));
            this.isPercent = bool.Parse(elem.GetAttribute("isPercent"));
            this.depositPercent = float.Parse(elem.GetAttribute("depositPercent"));
        }
    }
}
