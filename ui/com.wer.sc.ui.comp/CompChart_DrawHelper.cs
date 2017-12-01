using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.ui.comp
{
    public class CompChart_DrawHelper : IDrawOperator
    {
        private Dictionary<KLinePeriod, IStrategyDrawer> dic_Period_Drawer = new Dictionary<KLinePeriod, IStrategyDrawer>();

        private IStrategyDrawer drawer_TimeLine;

        private IStrategyDrawer drawer_Tick;

        private CompChart compChart;

        public CompChart_DrawHelper(CompChart compChart)
        {
            this.compChart = compChart;
        }

        public IStrategyDrawer GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_Period_Drawer.ContainsKey(klinePeriod))
            {
                return dic_Period_Drawer[klinePeriod];
            }
            IStrategyDrawer drawer = new CompChart_Drawer(compChart);
            dic_Period_Drawer.Add(klinePeriod, drawer);
            return drawer;
        }

        public IStrategyDrawer GetDrawer_Tick()
        {
            return drawer_Tick;
        }

        public IStrategyDrawer GetDrawer_TimeLine()
        {
            return drawer_TimeLine;
        }
    }
}