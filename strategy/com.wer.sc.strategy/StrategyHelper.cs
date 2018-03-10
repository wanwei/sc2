using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyHelper : IStrategyHelper
    {
        private IStrategyDrawer strategyDrawer;

        private IStrategyTrader strategyTrader;

        private IStrategyQueryResultManager queryResultManager;

        public StrategyHelper()
        {
            this.queryResultManager = new StrategyQueryResultManager();
        }

        /// <summary>
        /// 得到画图帮助接口
        /// </summary>
        public IStrategyDrawer Drawer
        {
            get
            {
                return strategyDrawer;
            }
            set
            {
                this.strategyDrawer = value;
            }
        }

        public IStrategyTrader Trader
        {
            get
            {
                return strategyTrader;
            }
            set
            {
                strategyTrader = value;
            }
        }

        public IStrategyQueryResultManager QueryResultManager
        {
            get
            {
                return queryResultManager;
            }
            set
            {
                this.queryResultManager = value;
            }
        }
    }
}