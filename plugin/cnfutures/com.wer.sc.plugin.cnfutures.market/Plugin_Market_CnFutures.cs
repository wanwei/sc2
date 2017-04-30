using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.market
{
    [Plugin("MARKET.CNFUTURES", "中国期货市场", "中国期货市场", MarketType.CnFutures)]
    public class Plugin_Market_CnFutures : Plugin_MarketAbstract
    {
        private Plugin_MarketData_CnFutures plugin_MarketData_XApi = new Plugin_MarketData_CnFutures();

        private Plugin_MarketTrader_CnFutures plugin_MarketTrader_XApi = new Plugin_MarketTrader_CnFutures();

        private IPlugin_MarketUtils marketUtils = new Plugin_MarketUtils_CnFutures();

        public override IPlugin_MarketData MarketData
        {
            get
            {
                return plugin_MarketData_XApi;
            }
        }

        public override IPlugin_MarketTrader MarketTrader
        {
            get
            {
                return plugin_MarketTrader_XApi;
            }
        }

        public override IPlugin_MarketUtils MarketUtils
        {
            get
            {
                return marketUtils;
            }
        }
    }
}
