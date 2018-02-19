using com.wer.sc.data;
using com.wer.sc.graphic;
using com.wer.sc.graphic.shape;
using com.wer.sc.strategy;
using com.wer.sc.strategy.common.looper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.platform
{
    public class Strategy_Platform : StrategyAbstract
    {
        private Looper_Ma looper_ma;

        private KLinePeriod period = KLinePeriod.KLinePeriod_1Minute;

        private int MaPeriod = 60;

        private List<Platform> platForms = new List<Platform>();

        private IKLineData klineData;

        public override void OnStart(object sender, IStrategyOnStartArgument argument)
        {
            klineData = argument.CurrentData.GetKLineData(period);
            List<int> periods = new List<int>();
            periods.Add(MaPeriod);
            looper_ma = new Looper_Ma(klineData, periods);
        }

        public override void OnBar(object sender, IStrategyOnBarArgument currentData)
        {
            looper_ma.Loop(currentData.CurrentData.GetKLineData(KLinePeriod.KLinePeriod_1Minute).BarPos);
            MaData maData = looper_ma.GetMaData(MaPeriod);
            FindPlatForm(maData, platForms.Count == 0 ? null : platForms[platForms.Count - 1]);
        }

        private void FindPlatForm(MaData maData, Platform lastPlatForm)
        {
            List<MaCrossPoint> points = maData.RecentCrossPoints;
            if (points.Count < 15)
                return;

            MaCrossPoint startPoint = points[points.Count - 15];
            int lastEndIndex = lastPlatForm == null ? 0 : lastPlatForm.EndIndex;
            if (startPoint.BarPos < lastEndIndex)
                return;
            MaCrossPoint endPoint = points[points.Count - 1];
            if (endPoint.BarPos - startPoint.BarPos < 60)
            {
                int startIndex = startPoint.BarPos;
                int endIndex = endPoint.BarPos;
                Platform platform = new Platform(klineData, startIndex, endIndex);
                platForms.Add(platform);
            }
        }

        public override void OnTick(object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnEnd(object sender, IStrategyOnEndArgument argument)
        {
            IShapeDrawer_PriceRect drawHelper = StrategyOperator.Drawer.GetDrawer_KLine(period);
            StrategyArray<double> arr = looper_ma.GetMaData(MaPeriod).Data;
            List<float> ff = new List<float>();
            for (int i = 0; i < arr.Count; i++)
            {
                ff.Add((float)arr[i]);
            }
            drawHelper.DrawPolyLine(ff, System.Drawing.Color.Green);

            for (int i = 0; i < platForms.Count; i++)
            {
                Platform platform = platForms[i];
                DrawPlatform(platform, drawHelper);
            }

            StrategyOperator.QueryResultManager.AddQueryResult(new StrategyQueryResult_Platform(platForms));
        }

        private void DrawPlatform(Platform platform, IShapeDrawer_PriceRect drawHelper)
        {
            PriceShape_Rect priceRect = new PriceShape_Rect();
            priceRect.PriceLeft = platform.StartIndex;
            priceRect.PriceTop = platform.TopList[2].KLineBar.High;
            priceRect.PriceRight = platform.EndIndex;
            priceRect.PriceBottom = platform.BottomList[2].KLineBar.Low;
            priceRect.Color = System.Drawing.Color.Red;
            //priceRect.FillRect = true;
            drawHelper.DrawRect(priceRect);
        }
    }

    class StrategyQueryResult_Platform : IStrategyQueryResult
    {
        private List<IStrategyQueryResultRow> rows;

        public StrategyQueryResult_Platform(IList<Platform> platforms)
        {
            rows = new List<IStrategyQueryResultRow>(platforms.Count);
            for (int i = platforms.Count - 1; i >= 0; i--)
                rows.Add(platforms[i]);
        }

        public string Name
        {
            get
            {
                return "平台";
            }
        }

        public string[] Title
        {
            get
            {
                return Platform.Title;
            }
        }

        public IList<IStrategyQueryResultRow> StrategyResults
        {
            get
            {
                return rows;
            }
        }
    }
}