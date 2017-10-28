using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils;
using com.wer.sc.strategy.draw;

namespace com.wer.sc.strategy.cnfutures
{
    /// <summary>
    /// 
    /// </summary>
    [Strategy("STRATEGY.ZIGZAG", "ZIGZAG指标", "ZIGZAG指标", "指标")]
    public class Strategy_Zigzag : StrategyAbstract, IStrategy
    {
        private const int LASTTYPE_UNKNOWN = 0;
        private const int LASTTYPE_LOW = -1;
        private const int LASTTYPE_HIGH = 1;

        private const string PARAMKEY_TURNLENGTH = "TURN_LENGTH";
        private const string PARAMKEY_HIGHLOWLENGTH = "HIGHLOW_LENGTH";

        //转折起始长度
        private int turnLength = 2;

        //确认高低点位置的长度
        private int highLowLength = 9;

        //第一次找到的低点index
        //private List<int> arr_Index_Bottom = new List<int>();

        //private List<int> arr_Index_Top = new List<int>();

        private List<int> arr_Index_Bottom = new List<int>();

        private List<int> arr_Index_Top = new List<int>();

        //第一次找到的低点
        private List<float> arr_Price_Bottom = new List<float>();
        //第一次找到的高点
        private List<float> arr_Price_Top = new List<float>();
        //最后复核后确认的低点
        private List<float> arr_Price_SureBottom = new List<float>();
        //最后复核后确认的高点
        private List<float> arr_Price_SureTop = new List<float>();

        public Strategy_Zigzag()
        {
            this.Parameters.AddParameter(PARAMKEY_TURNLENGTH, "转折起始长度", "", utils.param.ParameterType.INTEGER, 2);
            this.Parameters.AddParameter(PARAMKEY_HIGHLOWLENGTH, "高低点位置的长度", "", utils.param.ParameterType.INTEGER, 6);
        }

        public override void OnBar(Object sender, StrategyOnBarArgument currentData)
        {
            CalcTurnPoints(currentData);
        }

        /**
        * 查找高低点算法
        * 1.找到疑似的高点低点
        * 2.和之前的高低点进行比较，确认用之前的高低点还是现在的。
        */
        private void CalcTurnPoints(IRealTimeDataReader_Code currentData)
        {
            AddEmptyPoints();
            IKLineData klineData = currentData.GetKLineData(MainKLinePeriod);
            int barPos = klineData.BarPos;

            IList<float> arr_HighPrice = klineData.Arr_High;
            IList<float> arr_LowPrice = klineData.Arr_Low;

            bool hasFindLowPoint = IsPreviousBarTheLowestBar(arr_LowPrice, barPos, turnLength, highLowLength);
            bool hasFindHighPoint = IsPreviousBarTheHighestBar(arr_HighPrice, barPos, turnLength, highLowLength);

            if (barPos < highLowLength)            
                return;            

            if (!hasFindLowPoint && !hasFindHighPoint)            
                return;            

            int lastType = GetLastPointType(barPos);

            int turnPointIndex = barPos - turnLength;
            //发生在chart刚开始，之前还没有低点高点
            if (lastType == LASTTYPE_UNKNOWN)
            {
                if (hasFindHighPoint)
                    AddHighPoint(arr_HighPrice, turnPointIndex);
                else
                    AddLowPoint(arr_LowPrice, turnPointIndex);
            }
            else if (lastType == LASTTYPE_HIGH)
            {
                if (hasFindHighPoint)
                {
                    ReplaceHighPoint(arr_HighPrice, turnPointIndex);
                }
                else if (hasFindLowPoint)
                {
                    AddLowPoint(arr_LowPrice, turnPointIndex);
                }
            }
            else
            {
                if (hasFindLowPoint)
                {
                    ReplaceLowPoint(barPos, arr_HighPrice, arr_LowPrice, turnPointIndex);
                }
                else if (hasFindHighPoint)
                {
                    AddHighPoint(arr_HighPrice, turnPointIndex);
                }
            }
        }

        private void AddEmptyPoints()
        {
            arr_Price_Bottom.Add(0);
            arr_Price_Top.Add(0);
            arr_Price_SureBottom.Add(0);
            arr_Price_SureTop.Add(0);
        }

        private bool IsPreviousBarTheLowestBar(IList<float> prices, int currentIndex, int previousLength, int checkLength)
        {
            float previousPrice = MathUtils.GetPreviousData(prices, currentIndex, previousLength);
            float recentLow = MathUtils.Lowest(prices, currentIndex, checkLength);
            return previousPrice == recentLow;
        }

        private bool IsPreviousBarTheHighestBar(IList<float> prices, int currentIndex, int previousLength, int checkLength)
        {
            float previousPrice = MathUtils.GetPreviousData(prices, currentIndex, previousLength);
            float recentHigh = MathUtils.Highest(prices, currentIndex, checkLength);
            return previousPrice == recentHigh;
        }

        private void AddLowPoint(IList<float> arr_LowPrice, int pointPos)
        {
            int currentBarPos = arr_Price_Bottom.Count - 1;
            arr_Index_Bottom.Add(pointPos);
            float lowPrice = arr_LowPrice[pointPos];
            arr_Price_Bottom[pointPos] = lowPrice;
            arr_Price_SureBottom[pointPos] = lowPrice;
        }

        private void ReplaceLowPoint(int barPos, IList<float> arr_HighPrice, IList<float> arr_LowPrice, int pointPos)
        {
            float currentLowPrice = arr_LowPrice[pointPos];
            float recentLowPrice = arr_Price_SureBottom.Last();
            if (currentLowPrice > recentLowPrice)
                return;

            int recentLowIndex = arr_Index_Bottom.Last();
            arr_Index_Bottom.RemoveAt(arr_Index_Bottom.Count - 1);
            arr_Price_SureBottom[recentLowIndex] = 0;

            AddLowPoint(arr_LowPrice, pointPos);
        }

        private void AddHighPoint(IList<float> arr_HighPrice, int pointPos)
        {
            int currentBarPos = arr_Price_Top.Count - 1;
            arr_Index_Top.Add(pointPos);
            float highPrice = arr_HighPrice[pointPos];
            arr_Price_Top[pointPos] = highPrice;
            arr_Price_SureTop[pointPos] = highPrice;
        }

        private void ReplaceHighPoint(IList<float> arr_HighPrice, int pointPos)
        {
            float currentHighPrice = arr_HighPrice[pointPos];
            float recentHighPrice = arr_Price_SureTop.Last();
            if (currentHighPrice < recentHighPrice)
                return;

            int recentHighIndex = arr_Index_Top.Last();
            arr_Index_Top.RemoveAt(arr_Index_Top.Count - 1);
            arr_Price_SureTop[recentHighIndex] = 0;

            AddHighPoint(arr_HighPrice, pointPos);
        }

        private int GetLastPointType(int barPos)
        {
            if (arr_Index_Bottom.Count == 0)
            {
                if (arr_Index_Top.Count == 0)
                    return 0;
                else
                    return 1;
            }
            else if (arr_Index_Top.Count == 0)
                return -1;

            int lastLowIndex = arr_Index_Bottom.Last();
            int lastHighIndex = arr_Index_Top.Last();
            return lastLowIndex < lastHighIndex ? 1 : -1;
        }

        public override void OnTick(Object sender, StrategyOnTickArgument currentData)
        {

        }

        public override void OnStrategyStart(Object sender, StrategyOnStartArgument argument)
        {
            //this.arr_Index_Bottom.Clear();
            //this.arr_Index_Top.Clear();
            //this.arr_Price_Bottom.Clear();
            //this.arr_Price_Top.Clear();
            //this.arr_Price_SureBottom.Clear();
            //this.arr_Price_SureTop.Clear();

            this.turnLength = (int)this.Parameters.GetParameter(PARAMKEY_TURNLENGTH).Value;
            this.highLowLength = (int)this.Parameters.GetParameter(PARAMKEY_HIGHLOWLENGTH).Value;
        }

        public override void OnStrategyEnd(Object sender, StrategyOnEndArgument argument)
        {
            IDrawer drawHelper = StrategyOperator.DrawOperator.GetDrawer_KLine(MainKLinePeriod);
            //drawHelper.DrawPoints(arr_Price_Top, System.Drawing.Color.Blue);
            //drawHelper.DrawPoints(arr_Price_Bottom, System.Drawing.Color.White);

            drawHelper.DrawPoints(arr_Price_SureTop, System.Drawing.Color.Red);
            drawHelper.DrawPoints(arr_Price_SureBottom, System.Drawing.Color.Green);
        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }
    }
}