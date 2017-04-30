using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    public abstract class Plugin_MarketAbstract : IPlugin_Market
    {
        public abstract IPlugin_MarketData MarketData
        {
            get;
        }

        public abstract IPlugin_MarketTrader MarketTrader
        {
            get;
        }

        public virtual IPlugin_MarketUtils MarketUtils
        {
            get
            {
                return null;
            }
        }
    }
}
