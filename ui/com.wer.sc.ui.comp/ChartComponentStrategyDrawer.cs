using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;
using com.wer.sc.graphic;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentStrategyDrawer : IStrategyDrawer
    {
        private Dictionary<KLinePeriod, IShapeDrawer_PriceRect> dic_Period_Drawer = new Dictionary<KLinePeriod, IShapeDrawer_PriceRect>();

        private IShapeDrawer_PriceRect drawer_TimeLine;

        private IShapeDrawer_PriceRect drawer_Tick;

        private ChartComponentDrawer compChart;

        private int startPos_TimeLine;

        private int startPos_Tick; 

        public ChartComponentStrategyDrawer(ChartComponentDrawer compChart, Dictionary<KLinePeriod, int> startPos_KLine, int startPos_TimeLine, int startPos_Tick)
        {
            this.compChart = compChart;
            this.startPos_TimeLine = startPos_TimeLine;
            this.startPos_Tick = startPos_Tick;
        }

        public IShapeDrawer_PriceRect GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_Period_Drawer.ContainsKey(klinePeriod))
            {
                return dic_Period_Drawer[klinePeriod];
            }
            //每个周期单独建立drawer
            IShapeDrawer_PriceRect drawer = new ChartComponentShapeDrawer(compChart.Drawer_PriceRect, compChart.GraphicData_Candle, compChart.GraphicMapping);
            dic_Period_Drawer.Add(klinePeriod, drawer);
            return drawer;
        }

        public IShapeDrawer_PriceRect GetDrawer_Tick()
        {
            return drawer_Tick;
        }

        public IShapeDrawer_PriceRect GetDrawer_TimeLine()
        {
            return drawer_TimeLine;
        }
    }
}