using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy
{
    public class StrategyHelper : IStrategyHelper
    {
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
    }
}
