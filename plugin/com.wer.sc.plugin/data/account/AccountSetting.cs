using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    /// <summary>
    /// 账号设置
    /// </summary>
    public class AccountSetting : IXmlExchange
    {

        private AccountTradeType tradeType;

        /// <summary>
        /// 自动过滤
        /// </summary>
        private bool autoFilter;

        private double delayTime;

        private int delayTick;

        private double slipPrice;

        private double slipPerccent;

        public bool AutoFilter
        {
            get
            {
                return autoFilter;
            }

            set
            {
                autoFilter = value;
            }
        }

        public AccountTradeType TradeType
        {
            get
            {
                return tradeType;
            }

            set
            {
                tradeType = value;
            }
        }

        public double DelayTime
        {
            get
            {
                return delayTime;
            }

            set
            {
                delayTime = value;
            }
        }

        public int DelayTick
        {
            get
            {
                return delayTick;
            }

            set
            {
                delayTick = value;
            }
        }

        //是否滑点
        private bool isTradeSlip;

        public double SlipPrice
        {
            get
            {
                return slipPrice;
            }

            set
            {
                slipPrice = value;
                if (slipPrice != 0)
                    isTradeSlip = true;
                else if (slipPerccent == 0)
                    isTradeSlip = false;
            }
        }

        public double SlipPerccent
        {
            get
            {
                return slipPerccent;
            }

            set
            {
                slipPerccent = value;
                if (slipPerccent != 0)
                    isTradeSlip = true;
                else if (SlipPrice == 0)
                    isTradeSlip = false;
            }
        }

        public bool IsTradeSlip
        {
            get
            {
                return isTradeSlip;
            }
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("autoFilter", this.autoFilter.ToString());
            xmlElem.SetAttribute("delayTick", this.delayTick.ToString());
            xmlElem.SetAttribute("delayTime", this.delayTime.ToString());
            xmlElem.SetAttribute("isTradeSlip", this.isTradeSlip.ToString());
            xmlElem.SetAttribute("slipPerccent", this.slipPerccent.ToString());
            xmlElem.SetAttribute("slipPrice", this.slipPrice.ToString());
            xmlElem.SetAttribute("tradeType", this.tradeType.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            this.autoFilter = bool.Parse(xmlElem.GetAttribute("autoFilter"));
            this.delayTick = int.Parse(xmlElem.GetAttribute("delayTick"));
            this.delayTime = double.Parse(xmlElem.GetAttribute("delayTime"));
            this.isTradeSlip = bool.Parse(xmlElem.GetAttribute("isTradeSlip"));
            this.slipPerccent = double.Parse(xmlElem.GetAttribute("slipPerccent"));
            this.slipPrice = double.Parse(xmlElem.GetAttribute("slipPrice"));
            this.tradeType = (AccountTradeType)Enum.Parse(typeof(AccountTradeType), xmlElem.GetAttribute("tradeType"));
        }
    }
}