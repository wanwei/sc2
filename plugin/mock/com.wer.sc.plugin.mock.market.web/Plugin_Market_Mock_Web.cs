using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.mock.market
{
    [Plugin("MOCK.MARKET.WEB", "模拟市场，基于网页版交易", "模拟市场，基于网页版交易，测试专用", MarketType.CnStock)]
    public class Plugin_Market_Mock_Web : IPlugin_Market
    {
        private Plugin_MarketData_Mock_Web marketData = new Plugin_MarketData_Mock_Web();

        private Plugin_MarketTrader_Mock_Web marketTrader = new Plugin_MarketTrader_Mock_Web();

        public IPlugin_MarketData MarketData
        {
            get
            {
                return marketData;
            }
        }

        public IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return marketTrader;
            }
        }

        public IPlugin_MarketUtils MarketUtils
        {
            get
            {
                return null;
            }
        }
    }
}
