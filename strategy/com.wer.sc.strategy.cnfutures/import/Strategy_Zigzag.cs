using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.reader;
using com.wer.sc.data;
using com.wer.sc.utils;

namespace com.wer.sc.strategy.cnfutures
{
    /// <summary>
    /// 
    /// </summary>
    [Strategy("STRATEGY.ZIGZAG", "ZIGZAG指标", "ZIGZAG指标", "指标")]
    public class Strategy_Zigzag : StrategyAbstract, IStrategy
    {
        private KLinePeriod klinePeriod;

        public int ZZLen = 2;

        public int HLLen = 9;

        public List<int> Arr_PosDD = new List<int>();

        public List<int> Arr_PosGD = new List<int>();

        public List<int> Arr_PosRealDD = new List<int>();

        public List<int> Arr_PosRealGD = new List<int>();

        public List<float> Arr_RealDD = new List<float>();

        public List<float> Arr_RealGD = new List<float>();

        public Strategy_Zigzag()
        {
            this.klinePeriod = KLinePeriod.KLinePeriod_1Minute;
        }

        public override void OnBar(IRealTimeDataReader currentData)
        {
            initTurnPoint(currentData);
        }

        /**
        * 综合算法：
        * 1.找到疑似的高点低点
        * 2.
        */
        private void initTurnPoint(IRealTimeDataReader currentData)
        {
            IKLineData klineData = currentData.GetKLineData(klinePeriod);
            int barPos = klineData.BarPos;

            IList<float> arr_HighPrice = klineData.Arr_High;
            IList<float> arr_LowPrice = klineData.Arr_Low;
            int zzLen = ZZLen;
            int hlLen = HLLen;

            /**
             * 条件：
             * 1.向前第二个chart是最近9个chart里最低点
             * 2.该点和该点之前的点都高于之前的低点
             */
            float refLow = GetRefData(arr_LowPrice, barPos, zzLen);
            bool con_dd = refLow == MathUtils.Lowest(arr_LowPrice, barPos, hlLen) && MathUtils.GetPreviousData(arr_LowPrice, barPos, 1) > refLow
                    && arr_LowPrice[barPos] > refLow;

            float refHigh = MathUtils.GetPreviousData(arr_HighPrice, barPos, zzLen);
            bool con_gd = refHigh == MathUtils.Highest(arr_HighPrice, barPos, hlLen) && MathUtils.GetPreviousData(arr_HighPrice, barPos, 1) < refHigh
                    && arr_HighPrice[barPos] < refHigh;

            if (!(con_gd || con_dd))
                return;

            int lastType = getLastPointType(barPos);

            int pointPos = barPos - zzLen;
            //发生在chart刚开始，之前还没有低点高点
            if (lastType == 0)
            {
                if (con_gd)
                {
                    addRealGd(arr_HighPrice, pointPos);
                }
                else
                {
                    addRealDd(arr_LowPrice, pointPos);
                }
            }
            //上一个是高点
            else if (lastType == 1)
            {
                //又找到一个新高点
                if (con_gd)
                {
                    float currentHighPrice = arr_HighPrice[pointPos];
                    float lastHighPrice = MathUtils.GetPreviousData(Arr_RealGD, barPos, 0);
                    if (currentHighPrice > lastHighPrice)
                    {
                        Arr_PosRealGD.RemoveAt(Arr_PosRealGD.Count - 1);
                        Arr_RealGD.RemoveAt(Arr_RealGD.Count - 1);
                        addRealGd(arr_HighPrice, pointPos);
                    }
                }
                else if (con_dd)
                {
                    addRealDd(arr_LowPrice, pointPos);
                }
            }
            else
            {
                if (con_dd)
                {
                    float currentLowPrice = arr_LowPrice[pointPos];
                    float lastLowPrice = MathUtils.GetPreviousData(Arr_RealDD, barPos, 0);
                    if (currentLowPrice < lastLowPrice)
                    {
                        Arr_PosRealDD.RemoveAt(Arr_PosRealDD.Count - 1);
                        Arr_RealDD.RemoveAt(Arr_RealDD.Count - 1);
                        addRealDd(arr_HighPrice, pointPos);
                    }
                }
                else if (con_gd)
                {
                    addRealGd(arr_HighPrice, pointPos);
                }
            }

            addPosPoint(zzLen, barPos, con_dd, con_gd);
        }

        private float GetRefData(IList<float> arr, int currentIndex, int len)
        {
            return MathUtils.GetPreviousData(arr, currentIndex, len);
        }

        private void addRealDd(IList<float> arr_LowPrice, int pointPos)
        {
            Arr_PosRealDD.Add(pointPos);
            Arr_RealDD.Add(arr_LowPrice[pointPos]);
        }

        private void addRealGd(IList<float> arr_HighPrice, int pointPos)
        {
            Arr_PosRealGD.Add(pointPos);
            Arr_RealGD.Add(arr_HighPrice[pointPos]);
        }

        private void addPosPoint(int zzLen, int barPos, bool con_dd, bool con_gd)
        {
            //找到疑似的高点低点
            if (con_dd)
                AddData(Arr_PosDD, barPos - zzLen);
            if (con_gd)
                AddData(Arr_PosGD, barPos - zzLen);
        }

        private void AddData(List<int> arr_PosDD, int v)
        {
            //Arr_PosDD.Add()
        }

        private int getLastPointType(int barPos)
        {
            if (Arr_PosDD.Count == 0)
            {
                if (Arr_PosGD.Count == 0)
                    return 0;
                else
                    return 1;
            }
            else if (Arr_PosGD.Count == 0)
                return -1;

            int POS_LASTDD = MathUtils.GetPreviousData(Arr_PosDD, barPos, 0);
            int POS_LASTGD = MathUtils.GetPreviousData(Arr_PosGD, barPos, 0);
            return POS_LASTDD < POS_LASTGD ? 1 : -1;
        }

        public override void OnTick(IRealTimeDataReader currentData)
        {

        }

        public override void StrategyStart()
        {

        }

        public override void StrategyEnd()
        {

        }

        public override StrategyReferedPeriods GetStrategyPeriods()
        {
            return null;
        }
    }
}
