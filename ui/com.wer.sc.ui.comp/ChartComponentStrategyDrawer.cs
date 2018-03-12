using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.strategy;
using com.wer.sc.graphic;
using System.Xml;

namespace com.wer.sc.ui.comp
{
    public class ChartComponentStrategyDrawer : IStrategyDrawer
    {
        private Dictionary<KLinePeriod, IStrategyDrawer_PriceRect> dic_Period_Drawer = new Dictionary<KLinePeriod, IStrategyDrawer_PriceRect>();

        private IStrategyDrawer_PriceRect drawer_TimeLine;

        private IStrategyDrawer_PriceRect drawer_Tick;

        private ChartComponentDrawer compChart;

        private int startPos_TimeLine;

        private int startPos_Tick; 

        public ChartComponentStrategyDrawer(ChartComponentDrawer compChart, Dictionary<KLinePeriod, int> startPos_KLine, int startPos_TimeLine, int startPos_Tick)
        {
            this.compChart = compChart;
            this.startPos_TimeLine = startPos_TimeLine;
            this.startPos_Tick = startPos_Tick;
        }

        public IStrategyDrawer_PriceRect GetDrawer_KLine(KLinePeriod klinePeriod)
        {
            if (dic_Period_Drawer.ContainsKey(klinePeriod))
            {
                return dic_Period_Drawer[klinePeriod];
            }
            //每个周期单独建立drawer
            IStrategyDrawer_PriceRect drawer = new ChartComponentShapeDrawer(compChart.Drawer_PriceRect, compChart.GraphicData_Candle, compChart.GraphicMapping);
            dic_Period_Drawer.Add(klinePeriod, drawer);
            return drawer;
        }

        public IStrategyDrawer_PriceRect GetDrawer_Tick()
        {
            return drawer_Tick;
        }

        public IStrategyDrawer_PriceRect GetDrawer_TimeLine()
        {
            return drawer_TimeLine;
        }

        public IStrategyDrawer_PriceRect GetDrawer_TimeLine(int date)
        {
            throw new NotImplementedException();
        }

        public IStrategyDrawer_PriceRect GetDrawer_Tick(int date)
        {
            throw new NotImplementedException();
        }

        public void Save(XmlElement xmlElem)
        {
            throw new NotImplementedException();
        }

        public void Load(XmlElement xmlElem)
        {
            throw new NotImplementedException();
        }
    }
}