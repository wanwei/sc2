using com.wer.sc.graphic;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.sample.draw
{
    /// <summary>
    /// 绘图
    /// </summary>
    public class Strategy_DrawLine : StrategyAbstract
    {
        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {
            
        }

        private Color color_1 = ColorUtils.GetColor("#E7C681");

        private Color color_2 = ColorUtils.GetColor("#DCDC06");

        private Color color_3 = ColorUtils.GetColor("#FF00FF");

        private Color color_4 = ColorUtils.GetColor("#00F000");

        private Color color_5 = ColorUtils.GetColor("#677878");

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            IStrategyDrawer_PriceRect drawHelper = StrategyHelper.Drawer.GetDrawer_KLine(MainKLinePeriod);

            //drawHelper.DrawRect()
            //drawHelper.DrawPolyLine(maArr_1, color_1);
            //drawHelper.DrawTitle(1, "MA组合(" + Param_1 + "," + Param_2 + "," + Param_3 + "," + Param_4 + "," + Param_5 + ")", color_2);
        }
    }
}
