using com.wer.sc.data;
using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using com.wer.sc.graphic;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.cnfutures
{
    //[Strategy("STRATEGY.CNFUTURES.DEFAULT", "默认策略", "默认策略，用来显示MA线等", "")]
    public class Strategy_Default : StrategyAbstract
    {
        private int Param_1;
        private int Param_2;
        private int Param_3;
        private int Param_4;
        private int Param_5;

        private List<float> maPrice_1 = new List<float>();

        private List<float> maPrice_2 = new List<float>();

        private List<float> maPrice_3 = new List<float>();

        private List<float> maPrice_4 = new List<float>();

        private List<float> maPrice_5 = new List<float>();

        private List<float> topPoints = new List<float>();

        private List<float> bottomPoints = new List<float>();

        public Strategy_Default()
        {
            this.Parameters.AddParameter("MA_1", "MA5", "MA5", utils.param.ParameterType.INTEGER, 5);
            this.Parameters.AddParameter("MA_2", "MA10", "MA10", utils.param.ParameterType.INTEGER, 10);
            this.Parameters.AddParameter("MA_3", "MA20", "MA20", utils.param.ParameterType.INTEGER, 20);
            this.Parameters.AddParameter("MA_4", "MA40", "MA40", utils.param.ParameterType.INTEGER, 40);
            this.Parameters.AddParameter("MA_5", "MA60", "MA60", utils.param.ParameterType.INTEGER, 60);
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(MainKLinePeriod);
            GenMa(klineData);
        }

        private void GenMa(IKLineData klineData)
        {
            GenMa(klineData, maPrice_1, Param_1);
            GenMa(klineData, maPrice_2, Param_2);
            GenMa(klineData, maPrice_3, Param_3);
            GenMa(klineData, maPrice_4, Param_4);
            GenMa(klineData, maPrice_5, Param_5);
        }

        private void GenMa(IKLineData klineData, List<float> maList, int length)
        {
            int barPos = klineData.BarPos;
            int startPos = barPos - length;
            startPos = startPos < 0 ? 0 : startPos;

            float total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                total += klineData.Arr_End[i];
            }
            maList.Add(total / (barPos - startPos + 1));
        }


        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        private Color color_1 = ColorUtils.GetColor("#E7C681");

        private Color color_2 = ColorUtils.GetColor("#DCDC06");

        private Color color_3 = ColorUtils.GetColor("#FF00FF");

        private Color color_4 = ColorUtils.GetColor("#00F000");

        private Color color_5 = ColorUtils.GetColor("#677878");

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            IShapeDrawer_PriceRect drawHelper = StrategyOperator.Drawer.GetDrawer_KLine(MainKLinePeriod);
            drawHelper.DrawPolyLine(maPrice_1, color_1);
            drawHelper.DrawPolyLine(maPrice_2, color_2);
            drawHelper.DrawPolyLine(maPrice_3, color_3);
            drawHelper.DrawPolyLine(maPrice_4, color_4);
            drawHelper.DrawPolyLine(maPrice_5, color_5);
            drawHelper.DrawTitle(1, "MA组合(" + Param_1 + "," + Param_2 + "," + Param_3 + "," + Param_4 + "," + Param_5 + ")", color_2);
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            Param_1 = (int)this.Parameters.GetParameter(0).Value;
            Param_2 = (int)this.Parameters.GetParameter(1).Value;
            Param_3 = (int)this.Parameters.GetParameter(2).Value;
            Param_4 = (int)this.Parameters.GetParameter(3).Value;
            Param_5 = (int)this.Parameters.GetParameter(4).Value;
        }
    }
}
