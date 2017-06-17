using com.wer.sc.comp.graphic;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.test.model
{
    public class KLineModel_Simple3 : IPlugin_KLineModel
    {
        public int ZZLen = 2;

        public int HLLen = 9;

        public List<int> Arr_PosDD = new List<int>();

        public List<int> Arr_PosGD = new List<int>();

        /// <summary>
        /// 高点
        /// </summary>
        [ModelPoints("#FF0000", 6)]
        //[ModelLines("#FF0000", 2)]
        public List<PricePoint> HighPoints = new List<PricePoint>();

        /// <summary>
        /// 低点
        /// </summary>
        [ModelPoints("#00FF00", 6)]
        //[ModelLines("#00FF00", 2)]
        public List<PricePoint> LowPoints = new List<PricePoint>();

        public KLineModel_Simple3()
        {
            //Color.Red.
        }

        public override void ModelEnd()
        {
            //this.AddPoint(new PointList(HighPoints, Color.Red, 8));
            //this.AddPoint(new PointList(LowPoints, Color.Green, 8));
        }

        public override void Loop()
        {
            try
            {
                initTurnPoint();
            }
            catch (Exception e)
            {

            }
        }

        /**
         * 综合算法：
         * 1.找到疑似的高点低点
         * 2.
         */
        private void initTurnPoint()
        {
            IList<float> arr_HighPrice = Arr_High;
            IList<float> arr_LowPrice = Arr_Low;
            //float[] arr_HighPrice = Arr_End;
            //float[] arr_LowPrice = Arr_End;
            int zzLen = ZZLen;
            int hlLen = HLLen;

            /**
             * 条件：
             * 1.向前第二个chart是最近ZZLen个chart里最低点
             * 2.该点和该点之前的点都高于之前的低点
             */
            float refLow = RefData(arr_LowPrice, zzLen);
            bool con_dd = refLow == Lowest(arr_LowPrice, hlLen) && RefData(arr_LowPrice, 1) > refLow
                    && arr_LowPrice[BarPos] > refLow;

            float refHigh = RefData(arr_HighPrice, zzLen);
            bool con_gd = refHigh == Highest(arr_HighPrice, hlLen) && RefData(arr_HighPrice, 1) < refHigh
                    && arr_HighPrice[BarPos] < refHigh;

            if (!(con_gd || con_dd))
                return;

            int lastType = GetLastPointType();

            int pointPos = BarPos - zzLen;
            //发生在chart刚开始，之前还没有低点高点
            if (lastType == 0)
            {
                if (con_gd)
                    AddHighPoint(arr_HighPrice, pointPos);
                else
                    AddLowPoint(arr_LowPrice, pointPos);
            }
            //上一个是高点
            else if (lastType == 1)
            {
                //又找到一个新高点
                if (con_gd)
                {
                    float currentHighPrice = arr_HighPrice[pointPos];
                    float lastHighPrice = HighPoints[HighPoints.Count - 1].Y;
                    if (currentHighPrice > lastHighPrice)
                    {
                        HighPoints.RemoveAt(HighPoints.Count - 1);
                        AddHighPoint(arr_HighPrice, pointPos);
                    }
                }
                else if (con_dd)
                    AddLowPoint(arr_LowPrice, pointPos);
            }
            else
            {
                if (con_dd)
                {
                    float currentLowPrice = arr_LowPrice[pointPos];
                    float lastLowPrice = LowPoints[LowPoints.Count - 1].Y;
                    if (currentLowPrice < lastLowPrice)
                    {
                        LowPoints.RemoveAt(LowPoints.Count - 1);
                        AddLowPoint(arr_LowPrice, pointPos);
                    }
                }
                else if (con_gd)
                    AddHighPoint(arr_HighPrice, pointPos);
            }

            AddPosPoint(zzLen, con_dd, con_gd);
        }

        private void AddLowPoint(IList<float> arr_LowPrice, int pointPos)
        {
            LowPoints.Add(new PricePoint(pointPos, arr_LowPrice[pointPos]));
        }

        private void AddHighPoint(IList<float> arr_HighPrice, int pointPos)
        {
            HighPoints.Add(new PricePoint(pointPos, arr_HighPrice[pointPos]));
        }

        private void AddPosPoint(int zzLen, bool con_dd, bool con_gd)
        {
            //找到疑似的高点低点
            if (con_dd)
                AddData(Arr_PosDD, BarPos - zzLen);
            if (con_gd)
                AddData(Arr_PosGD, BarPos - zzLen);
        }

        /// <summary>
        /// 上一个找到的转折点的类型
        /// 0：未知；1：高点；-1：低点
        /// </summary>
        /// <returns></returns>
        private int GetLastPointType()
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

            int POS_LASTDD = RefData(Arr_PosDD, 0);
            int POS_LASTGD = RefData(Arr_PosGD, 0);
            return POS_LASTDD < POS_LASTGD ? 1 : -1;
        }
    }
}