using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentStrategyDrawOperator : IDrawOperator
    {
        private Dictionary<KLinePeriod, IStrategyDrawer> dic_Period_Drawer = new Dictionary<KLinePeriod, IStrategyDrawer>();

        private IStrategyDrawer drawer_TimeLine;

        private IStrategyDrawer drawer_Tick;

        private ChartComponentDrawer compChart;

        private int startPos_TimeLine;

        private int startPos_Tick; 

        public ChartComponentStrategyDrawOperator(ChartComponentDrawer compChart, Dictionary<KLinePeriod, int> startPos_KLine, int startPos_TimeLine, int startPos_Tick)
        {
            this.compChart = compChart;
            this.startPos_TimeLine = startPos_TimeLine;
            this.startPos_Tick = startPos_Tick;
        }

        public IStrategyDrawer GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_Period_Drawer.ContainsKey(klinePeriod))
            {
                return dic_Period_Drawer[klinePeriod];
            }
            //每个周期单独建立drawer
            IStrategyDrawer drawer = new ChartComponentStrategyDrawer(compChart.Drawer_PriceRect, compChart.GraphicData_Candle, compChart.GraphicMapping);
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