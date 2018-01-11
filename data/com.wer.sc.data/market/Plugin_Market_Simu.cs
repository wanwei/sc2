using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market
{
    public class Plugin_Market_Simu : IPlugin_Market
    {
        private IPlugin_MarketData marketData;

        private IPlugin_MarketTrader marketTrader;

        public Plugin_Market_Simu()
        {
            
        }

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
