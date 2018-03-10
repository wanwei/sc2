using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils;
using System.Drawing;
using com.wer.sc.graphic;

namespace com.wer.sc.strategy.common.zigzag
{
    /// <summary>
    /// 
    /// </summary>
    //[Strategy("STRATEGY.ZIGZAG", "ZIGZAG指标", "ZIGZAG指标", "指标")]
    public class Strategy_Zigzag : StrategyAbstract
    {
        private const int LASTTYPE_UNKNOWN = 0;
        private const int LASTTYPE_LOW = -1;
        private const int LASTTYPE_HIGH = 1;

        private const string PARAMKEY_TURNLENGTH = "TURN_LENGTH";
        private const string PARAMKEY_HIGHLOWLENGTH = "HIGHLOW_LENGTH";

        private Zigzag zigzag;

        public Zigzag Zigzag
        {
            get { return zigzag; }
        }

        private KLinePeriod zigzagPeriod;

        public KLinePeriod ZigzagPeriod
        {
            get
            {
                if (zigzagPeriod == null)
                    return MainKLinePeriod;
                return zigzagPeriod;
            }

            set
            {
                zigzagPeriod = value;
            }
        }

        public Strategy_Zigzag()
        {
            this.Parameters.AddParameter(PARAMKEY_TURNLENGTH, "转折起始长度", "", utils.param.ParameterType.INTEGER, 2);
            this.Parameters.AddParameter(PARAMKEY_HIGHLOWLENGTH, "高低点位置的长度", "", utils.param.ParameterType.INTEGER, 5);
        }

        public override void OnBar(Object sender, IStrategyOnBarArgument argument)
        {
            IStrategyOnBarInfo bar = argument.GetFinishedBar(ZigzagPeriod);
            if (bar != null)
                zigzag.Loop(bar.BarPos);

            //if (argument.Time == 20171218.0900)
            //{
            //    int start = bar.BarPos - 500;
            //    int end = bar.BarPos;
            //    for (int i = start; i < end; i++)
            //    {
            //        zigzag.Loop(i);
            //    }
            //}
        }

        public override void OnTick(Object sender, IStrategyOnTickArgument currentData)
        {

        }

        public override void OnStart(Object sender, IStrategyOnStartArgument argument)
        {
            int turnLength = (int)this.Parameters.GetParameter(PARAMKEY_TURNLENGTH).Value;
            int highLowLength = (int)this.Parameters.GetParameter(PARAMKEY_HIGHLOWLENGTH).Value;
            this.zigzag = new Zigzag(argument.CurrentData.GetKLineData(ZigzagPeriod), turnLength, highLowLength);
        }

        public override void OnEnd(Object sender, IStrategyOnEndArgument argument)
        {
            IStrategyDrawer_PriceRect drawHelper = StrategyHelper.Drawer.GetDrawer_KLine(ZigzagPeriod);
            List<ZigzagPoint> points = this.zigzag.GetPoints();
            ZigzagDrawer.DrawZigzagPoints(drawHelper, points, Color.Blue, Color.White, 8);
            ZigzagDrawer.DrawZigzagPoints(drawHelper, this.zigzag.GetMergedPoints(), 12);
        }
    }
}