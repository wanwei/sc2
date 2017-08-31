using com.wer.sc.strategy.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.ui.comp
{
    public class CompChart_DrawHelper : IDrawHelper
    {
        private Dictionary<KLinePeriod, IDrawer> dic_Period_Drawer = new Dictionary<KLinePeriod, IDrawer>();

        private IDrawer drawer_TimeLine;

        private IDrawer drawer_Tick;

        private CompChart compChart;

        public CompChart_DrawHelper(CompChart compChart)
        {
            this.compChart = compChart;
        }

        public IDrawer GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_Period_Drawer.ContainsKey(klinePeriod))
            {
                return dic_Period_Drawer[klinePeriod];
            }
            IDrawer drawer = new CompChart_Drawer(compChart);
            dic_Period_Drawer.Add(klinePeriod, drawer);
            return drawer;
        }

        public IDrawer GetDrawer_Tick()
        {
            return drawer_Tick;
        }

        public IDrawer GetDrawer_TimeLine()
        {
            return drawer_TimeLine;
        }
    }
}