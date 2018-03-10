using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.graphic;

namespace com.wer.sc.strategy
{
    public class StrategyDrawer : IStrategyDrawer
    {
        public IStrategyDrawer_PriceRect GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            throw new NotImplementedException();
        }

        public IStrategyDrawer_PriceRect GetDrawer_Tick()
        {
            throw new NotImplementedException();
        }

        public IStrategyDrawer_PriceRect GetDrawer_TimeLine()
        {
            throw new NotImplementedException();
        }
    }
}
