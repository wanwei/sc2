using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils;

namespace com.wer.sc.strategy.common.zigzag
{
    /// <summary>
    /// 
    /// </summary>
    [Strategy("STRATEGY.ZIGZAG", "ZIGZAG指标", "ZIGZAG指标", "指标")]
    public class Strategy_Zigzag : StrategyAbstract
    {
        private const int LASTTYPE_UNKNOWN = 0;
        private const int LASTTYPE_LOW = -1;
        private const int LASTTYPE_HIGH = 1;

        private const string PARAMKEY_TURNLENGTH = "TURN_LENGTH";
        private const string PARAMKEY_HIGHLOWLENGTH = "HIGHLOW_LENGTH";

        private Zigzag zigzag;

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
            IStrategyOnBarInfo bar = argument.GetFinishedBar(zigzagPeriod);
            if (bar != null)
                zigzag.Loop(bar.KLineData, bar.BarPos);
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
            IStrategyDrawer drawHelper = StrategyOperator.DrawOperator.GetDrawer_KLine(MainKLinePeriod);

            List<ZigzagPoint> points = this.zigzag.GetPoints();

            
            //drawHelper.DrawPoints(arr_Price_Top, System.Drawing.Color.Blue);
            //drawHelper.DrawPoints(arr_Price_Bottom, System.Drawing.Color.White);

            //drawHelper.DrawPoints(arr_Price_SureTop, System.Drawing.Color.Red);
            //drawHelper.DrawPoints(arr_Price_SureBottom, System.Drawing.Color.Green);
        }
    }
}