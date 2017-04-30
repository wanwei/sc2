using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using com.wer.sc.data.market.data;

namespace com.wer.sc.data.market.impl
{
    /// <summary>
    /// 数据接收器
    /// </summary>
    public class Market : IMarket
    {
        private MarketPluginMgr marketPluginMgr;
        private MarketData marketData;        
        private MarketTrader marketTrader;

        public Market(MarketPluginMgr marketPluginMgr)
        {
            this.marketPluginMgr = marketPluginMgr;
            this.marketData = new MarketData(marketPluginMgr);
            this.marketTrader = new MarketTrader(marketPluginMgr);
        }

        public IMarketData MarketData
        {
            get
            {
                return marketData;
            }
        }

        public IMarketTrader MarketTrader
        {
            get
            {
                return marketTrader;
            }
        }
    }
}