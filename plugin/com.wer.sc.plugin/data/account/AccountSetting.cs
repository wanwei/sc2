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
        /// <summary>
        /// 成交自动过滤，该参数是为了简化交易模型设计的，方便固定手数买入卖出
        /// 规则如下：
        /// 1.开仓没有成交则下次再开仓时会撤掉之前开仓
        /// 2.成功开仓后下次开仓会被过滤掉直到完成平仓
        /// </summary>
        private bool autoFilter;

        /// <summary>
        /// 账号成交方式
        /// </summary>
        private AccountTradeType tradeType;

        /// <summary>
        /// 延时成交，比如延时3秒才开始成交
        /// </summary>
        private double delayTime;

        /// <summary>
        /// 延迟tick成交
        /// </summary>
        private int delayTick;

        /// <summary>
        /// 滑点成交方式
        /// </summary>
        private AccountSlipType slipType;

        /// <summary>
        /// 最小价格滑点
        /// </summary>
        private int slipMinPrice;

        /// <summary>
        /// 滑点成交，百分比，比如每次成交默认按照价格
        /// </summary>
        private double slipPerccent;

        /// <summary>
        /// 滑点成交
        /// </summary>
        private double slipPrice;

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

        public AccountSlipType SlipType
        {
            get
            {
                return slipType;
            }

            set
            {
                slipType = value;
            }
        }

        public int SlipMinPrice
        {
            get
            {
                return slipMinPrice;
            }

            set
            {
                slipMinPrice = value;
            }
        }

        public double SlipPrice
        {
            get
            {
                return slipPrice;
            }

            set
            {
                slipPrice = value;
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
            }
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("autoFilter", this.autoFilter.ToString());
            xmlElem.SetAttribute("tradeType", this.tradeType.ToString());
            xmlElem.SetAttribute("delayTick", this.delayTick.ToString());
            xmlElem.SetAttribute("delayTime", this.delayTime.ToString());

            xmlElem.SetAttribute("slipType", this.slipType.ToString());            
            xmlElem.SetAttribute("slipMinPrice", this.slipMinPrice.ToString());
            xmlElem.SetAttribute("slipPerccent", this.slipPerccent.ToString());
            xmlElem.SetAttribute("slipPrice", this.slipPrice.ToString());            
        }

        public void Load(XmlElement xmlElem)
        {
            this.autoFilter = bool.Parse(xmlElem.GetAttribute("autoFilter"));
            this.tradeType = (AccountTradeType)Enum.Parse(typeof(AccountTradeType), xmlElem.GetAttribute("tradeType"));
            this.delayTick = int.Parse(xmlElem.GetAttribute("delayTick"));
            this.delayTime = double.Parse(xmlElem.GetAttribute("delayTime"));

            this.slipType = (AccountSlipType)Enum.Parse(typeof(AccountSlipType), xmlElem.GetAttribute("slipType"));            
            this.slipMinPrice = int.Parse(xmlElem.GetAttribute("slipMinPrice"));
            this.slipPerccent = double.Parse(xmlElem.GetAttribute("slipPerccent"));
            this.slipPrice = double.Parse(xmlElem.GetAttribute("slipPrice"));         
        }
    }
}