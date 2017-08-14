using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using System.Drawing;
using com.wer.sc.strategy.draw;

namespace com.wer.sc.strategy.cnfutures.draw
{
    /// <summary>
    /// 画5分钟K线
    /// 画高低点
    /// </summary>
    [Strategy("STRATEGY.DRAWPOINTS", "尝试画线", "画线", "画线")]
    public class Strategy_DrawPoints : StrategyAbstract
    {
        private List<float> maPrice_5 = new List<float>();

        private List<float> maPrice_10 = new List<float>();

        private List<float> maPrice_20 = new List<float>();

        private List<float> maPrice_40 = new List<float>();

        private List<float> maPrice_60 = new List<float>();

        private List<float> topPoints = new List<float>();

        private List<float> bottomPoints = new List<float>();

        private bool isLastTop = false;

        //private int length = 5;

        public Strategy_DrawPoints()
        {

        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }

        public override void OnBar(IRealTimeDataReader currentData)
        {
            IKLineData klineData = currentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            GenMa(klineData);
            GenPoint(klineData);
        }

        private void GenMa(IKLineData klineData)
        {
            GenMa(klineData, maPrice_5, 5);
            GenMa(klineData, maPrice_10, 10);
            GenMa(klineData, maPrice_20, 20);
            GenMa(klineData, maPrice_40, 40);
            GenMa(klineData, maPrice_60, 60);
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

        private void GenPoint(IKLineData klineData)
        {
            topPoints.Add(klineData.High);
            bottomPoints.Add(klineData.Low);
            //int barPos = klineData.BarPos;
            //if (barPos < length)
            //    return;

            //if (isLastTop)
            //{

            //}
        }

        public override void OnTick(IRealTimeDataReader currentData)
        {

        }

        public override void StrategyEnd()
        {
            IDrawHelper drawHelper = StrategyHelper.DrawHelper;
            drawHelper.DrawPolyLine(maPrice_5, Color.Green);
            drawHelper.DrawPolyLine(maPrice_10, Color.AliceBlue);
            drawHelper.DrawPolyLine(maPrice_20, Color.Yellow);
            drawHelper.DrawPolyLine(maPrice_40, Color.White);
            drawHelper.DrawPolyLine(maPrice_60, Color.Silver);
            drawHelper.DrawPoints(topPoints, Color.Red);
            drawHelper.DrawPoints(bottomPoints, Color.Green);
            //this.StrategyHelper.DrawLabel()
        }

        public override void StrategyStart()
        {

        }
    }
}
