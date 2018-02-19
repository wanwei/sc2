using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyHelper : IStrategyHelper
    {        
        private IStrategyDrawer drawHelper;

        private IStrategyTrader strategyTrader_Code;

        private IStrategyQueryResultManager queryResultManager = new StrategyQueryResultManager();

        public StrategyHelper(IStrategyDrawer drawHelper)
        {
            this.drawHelper = drawHelper;
        }

        /// <summary>
        /// 得到画图帮助接口
        /// </summary>
        public IStrategyDrawer Drawer
        {
            get
            {
                return drawHelper;
            }
        }

        public IStrategyTrader Trader
        {
            get
            {
                return strategyTrader_Code;
            }
            set
            {
                strategyTrader_Code = value;
            }
        }        

        public IStrategyQueryResultManager QueryResultManager
        {
            get
            {
                return queryResultManager;
            }
        }        
    }
}
