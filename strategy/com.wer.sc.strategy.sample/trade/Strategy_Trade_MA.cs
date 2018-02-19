using com.wer.sc.data.market;
using com.wer.sc.graphic;
using com.wer.sc.strategy;
using com.wer.sc.strategy.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.sample.trade
{
    public class Strategy_Trade_MA : StrategyAbstract
    {
        private IStrategyTrader trader;

        private Strategy_Ma referedStrategy_MA10;

        private Strategy_Ma referedStrategy_MA20;

        private List<IStrategy> referedStrategies = new List<IStrategy>();

        public Strategy_Trade_MA()
        {
            this.referedStrategy_MA10 = new Strategy_Ma();
            this.referedStrategy_MA20 = new Strategy_Ma();
            this.referedStrategy_MA10.Parameters.SetParameterValue(Strategy_Ma.PARAMKEY_MA, 10);
            this.referedStrategy_MA20.Parameters.SetParameterValue(Strategy_Ma.PARAMKEY_MA, 20);
            this.referedStrategies.Add(referedStrategy_MA10);
            this.referedStrategies.Add(referedStrategy_MA20);
        }

        public override IList<IStrategy> GetReferedStrategies()
        {
            return referedStrategies;
        }

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            base.OnStart(sender, argument);
            trader = this.StrategyOperator.Trader;
            trader.AutoFilter = true;
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            List<float> ma10 = referedStrategy_MA10.MAList;
            List<float> ma20 = referedStrategy_MA20.MAList;
            if (ma10.Count < 2)
                return;
            if (ma10[ma10.Count - 2] > ma20[ma20.Count - 2] && ma10[ma10.Count - 1] > ma20[ma20.Count - 1])
            {
                this.StrategyOperator.Trader.CloseAll();
                return;
            }
            if (ma10[ma10.Count - 2] < ma20[ma20.Count - 2] && ma10[ma10.Count - 1] < ma20[ma20.Count - 1])
            {                
                this.StrategyOperator.Trader.Open(currentData.Code, OrderSide.Buy, currentData.CurrentData.Price, 5);
                return;
            }
        }
        private Color color_1 = ColorUtils.GetColor("#E7C681");

        private Color color_2 = ColorUtils.GetColor("#DCDC06");

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {
            DrawAccount();
            IShapeDrawer_PriceRect drawHelper = StrategyOperator.Drawer.GetDrawer_KLine(MainKLinePeriod);
            List<float> ma10 = referedStrategy_MA10.MAList;
            List<float> ma20 = referedStrategy_MA20.MAList;
            drawHelper.DrawPolyLine(ma10, color_1);
            drawHelper.DrawPolyLine(ma20, color_2);
            //drawHelper.DrawTitle(1, "MA组合(" + Param_1 + "," + Param_2 + "," + Param_3 + "," + Param_4 + "," + Param_5 + ")", color_2);
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }
    }
}