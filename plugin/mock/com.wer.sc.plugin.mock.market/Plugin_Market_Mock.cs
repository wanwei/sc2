using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.mock.market
{
    [Plugin("MOCK.MARKET", "模拟市场", "模拟市场，测试专用", MarketType.CnFutures)]
    public class Plugin_Market_Mock : Plugin_MarketAbstract
    {
        private Plugin_MarketData_Mock marketData = new Plugin_MarketData_Mock();

        private Plugin_MarketTrader_Mock marketTrader = new Plugin_MarketTrader_Mock();

        public override IPlugin_MarketData MarketData
        {
            get
            {
                return marketData;
            }
        }

        public override IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return marketTrader;
            }
        }
    }
}
