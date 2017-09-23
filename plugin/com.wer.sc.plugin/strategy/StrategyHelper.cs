using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    class StrategyHelper : IStrategyHelper
    {
        private List<IStrategyResult> results = new List<IStrategyResult>();

        private IDrawHelper drawHelper;

        public StrategyHelper(IDrawHelper drawHelper)
        {
            this.drawHelper = drawHelper;
        }

        /// <summary>
        /// 得到画图帮助接口
        /// </summary>
        public IDrawHelper DrawHelper
        {
            get
            {
                return drawHelper;
            }
        }

        public IStrategyTrader_Code Trader
        {
            get
            {
                return null;
            }
        }

        public List<IStrategyResult> Results
        {
            get
            {
                return results;
            }
        }

        public void AddStrategyResult(IStrategyResult strategyResult)
        {
            this.results.Add(strategyResult);
        }
    }
}
