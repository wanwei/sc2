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

namespace com.wer.sc.strategy.common.ma
{
    //[Strategy("STRATEGY.MA", "计算均线", "用来计算和显示MA线", "基础策略")]
    public class Strategy_MultiMa : StrategyAbstract
    {
        private const string PARAMKEY_MA1 = "MA_1";
        private const string PARAMKEY_MA2 = "MA_2";
        private const string PARAMKEY_MA3 = "MA_3";
        private const string PARAMKEY_MA4 = "MA_4";
        private const string PARAMKEY_MA5 = "MA_5";

        private int Param_1;
        private int Param_2;
        private int Param_3;
        private int Param_4;
        private int Param_5;

        private StrategyArray<float> maArr_1;

        public IList<float> MaArr_1
        {
            get { return maArr_1; }
        }

        public float MAPrice_1
        {
            get { return maArr_1.Last(); }
        }

        private StrategyArray<float> maArr_2;

        public IList<float> MaArr_2
        {
            get { return maArr_2; }
        }

        public float MAPrice_2
        {
            get { return maArr_2.Last(); }
        }

        private StrategyArray<float> maArr_3;

        public IList<float> MaArr_3
        {
            get { return maArr_3; }
        }

        public float MAPrice_3
        {
            get { return maArr_3.Last(); }
        }

        private StrategyArray<float> maArr_4;

        public IList<float> MaArr_4
        {
            get { return maArr_4; }
        }

        public float MAPrice_4
        {
            get { return maArr_4.Last(); }
        }

        private StrategyArray<float> maArr_5;

        public IList<float> MaArr_5
        {
            get { return maArr_5; }
        }

        public float MAPrice_5
        {
            get { return maArr_5.Last(); }
        }

        public Strategy_MultiMa()
        {
            maArr_1 = new StrategyArray<float>();
            maArr_2 = new StrategyArray<float>();
            maArr_3 = new StrategyArray<float>();
            maArr_4 = new StrategyArray<float>();
            maArr_5 = new StrategyArray<float>();

            this.Parameters.AddParameter(PARAMKEY_MA1, "MA5", "MA5", utils.param.ParameterType.INTEGER, 5);
            this.Parameters.AddParameter(PARAMKEY_MA2, "MA10", "MA10", utils.param.ParameterType.INTEGER, 10);
            this.Parameters.AddParameter(PARAMKEY_MA3, "MA20", "MA20", utils.param.ParameterType.INTEGER, 20);
            this.Parameters.AddParameter(PARAMKEY_MA4, "MA40", "MA40", utils.param.ParameterType.INTEGER, 40);
            this.Parameters.AddParameter(PARAMKEY_MA5, "MA60", "MA60", utils.param.ParameterType.INTEGER, 60);
        }

        public override StrategyReferedPeriods GetReferedPeriods()
        {
            return null;
        }

        bool isLastPeriodEnd = true;

        public override void OnBar(Object sender, IStrategyOnBarArgument currentData)
        {
            IKLineData klineData = currentData.CurrentData.GetKLineData(MainKLinePeriod);
            if (klineData == null)
                throw new StrategyException("没找到" + MainKLinePeriod + "K线数据");
            GenMa(klineData);
            isLastPeriodEnd = currentData.CurrentData.IsPeriodEnd(MainKLinePeriod);
        }

        private void GenMa(IKLineData klineData)
        {
            GenMa(klineData, maArr_1, Param_1);
            GenMa(klineData, maArr_2, Param_2);
            GenMa(klineData, maArr_3, Param_3);
            GenMa(klineData, maArr_4, Param_4);
            GenMa(klineData, maArr_5, Param_5);
        }

        private void GenMa(IKLineData klineData, IList<float> maList, int length)
        {
            int barPos = klineData.BarPos;
            int startPos = barPos - length + 1;
            startPos = startPos < 0 ? 0 : startPos;

            float total = 0;
            for (int i = startPos; i <= barPos; i++)
            {
                total += klineData.Arr_End[i];
            }
            maList[barPos] = total / (barPos - startPos + 1);
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
            drawHelper.DrawPolyLine(maArr_1, color_1);
            drawHelper.DrawPolyLine(maArr_2, color_2);
            drawHelper.DrawPolyLine(maArr_3, color_3);
            drawHelper.DrawPolyLine(maArr_4, color_4);
            drawHelper.DrawPolyLine(maArr_5, color_5);
            drawHelper.DrawTitle(1, "MA组合(" + Param_1 + "," + Param_2 + "," + Param_3 + "," + Param_4 + "," + Param_5 + ")", color_2);
        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            Param_1 = (int)this.Parameters.GetParameter(PARAMKEY_MA1).Value;
            Param_2 = (int)this.Parameters.GetParameter(PARAMKEY_MA2).Value;
            Param_3 = (int)this.Parameters.GetParameter(PARAMKEY_MA3).Value;
            Param_4 = (int)this.Parameters.GetParameter(PARAMKEY_MA4).Value;
            Param_5 = (int)this.Parameters.GetParameter(PARAMKEY_MA5).Value;
        }
    }
}