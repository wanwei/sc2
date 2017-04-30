using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market.cnstock
{
    [Plugin("MARKET.CNSTOCK", "中国股票市场", "中国股票市场", MarketType.CnStock)]
    public class Plugin_Market_CnStock : Plugin_MarketAbstract
    {
        private Plugin_MarketData_CnStock plugin_MarketData_XApi = new Plugin_MarketData_CnStock();

        private Plugin_MarketTrader_CnStock plugin_MarketTrader_XApi = new Plugin_MarketTrader_CnStock();

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
    }
}
