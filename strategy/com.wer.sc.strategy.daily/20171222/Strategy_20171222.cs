using com.wer.sc.graphic;
using com.wer.sc.strategy.common.zigzag;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.daily._20171222
{
    public class Strategy_20171222 : StrategyAbstract
    {
        private Zigzag zigzag;

        public Strategy_20171222()
        {
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            this.zigzag = new Zigzag(argument.CurrentData.GetKLineData(MainKLinePeriod));
        }

        public override void OnBar(object sender, IStrategyOnBarArgument argument)
        {
            IStrategyOnBarInfo bar = argument.GetFinishedBar(MainKLinePeriod);
            if (bar != null)
                if (argument.Time == 20171221.2100)
                {
                    int start = bar.BarPos - 50;
                    int end = bar.BarPos;
                    for (int i = start; i < end; i++)
                    {
                        zigzag.Loop(i);
                    }
                }
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            IStrategyDrawer_PriceRect drawHelper = StrategyHelper.Drawer.GetDrawer_KLine(MainKLinePeriod);
            ZigzagDrawer.DrawZigzagPoints(drawHelper, this.zigzag.GetPoints(), Color.Blue, Color.White, 8);
            ZigzagDrawer.DrawZigzagPoints(drawHelper, this.zigzag.GetMergedPoints(), 12);
            //DrawZigzagPoints(drawHelper, this.zigzag.GetMergedPoints(), 12);
        }
    }
}
