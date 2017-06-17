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
    public class KLineModel_Simple : IPlugin_KLineModel
    {
        public int ZZLen = 2;

        public int HLLen = 9;

        public List<int> Arr_PosDD = new List<int>();

        public List<int> Arr_PosGD = new List<int>();

        public List<int> Arr_PosRealDD = new List<int>();

        public List<int> Arr_PosRealGD = new List<int>();

        public List<float> Arr_RealDD = new List<float>();

        public List<float> Arr_RealGD = new List<float>();

        public KLineModel_Simple()
        {

        }

        public override void ModelStart()
        {
            Debug.WriteLine("模型计算开始");
        }

        public override void ModelEnd()
        {
            List<PricePoint> gdpoints = new List<PricePoint>();
            for (int i = 0; i < Arr_PosRealGD.Count; i++)
            {
                int gdIndex = Arr_PosRealGD[i];
                gdpoints.Add(new PricePoint(gdIndex, KLineData.Arr_High[gdIndex]));
            }
            this.AddPoint(new PointList(gdpoints, Color.Red, 8));

            List<PricePoint> ddpoints = new List<PricePoint>();
            for (int i = 0; i < Arr_PosRealDD.Count; i++)
            {
                int ddIndex = Arr_PosRealDD[i];
                ddpoints.Add(new PricePoint(ddIndex, KLineData.Arr_Low[ddIndex]));
            }
            this.AddPoint(new PointList(ddpoints, Color.Green, 8));
            //for (int i = 0; i < KLineData.Length; i++)
            //{
            //    KLineData.BarPos = i;
            //    Console.WriteLine(KLineData);
            //}

            //int startDD = Arr_PosRealDD[0];
            //int startGD = Arr_PosRealGD[0];
            //bool isStartDD = startDD < startGD;

            //int size = Arr_RealDD.Count < Arr_RealGD.Count ? Arr_RealDD.Count : Arr_RealGD.Count;
            //for (int i = 0; i < size; i++)
            //{
            //    if (isStartDD)
            //    {
            //        //System.out.println(arr_time()[Arr_PosRealDD.get(i)] + ":" + Arr_RealDD.get(i) + "-"
            //        //        + arr_time()[Arr_PosRealGD.get(i)] + ":" + Arr_RealGD.get(i));
            //    }
            //    else
            //    {
            //        //System.out.println(arr_time()[Arr_PosRealGD.get(i)] + ":" + Arr_RealGD.get(i) + "-"
            //        //        + arr_time()[Arr_PosRealDD.get(i)] + ":" + Arr_RealDD.get(i));
            //    }
            //}

            //		for (int i = 0; i < Arr_PosDD.size(); i++) {
            //			System.out.println(arr_time()[Arr_PosDD.get(i)]);
            //		}
            //System.out.println("模型计算结束");
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
            int zzLen = ZZLen;
            int hlLen = HLLen;

            /**
             * 条件：
             * 1.向前第二个chart是最近9个chart里最低点
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

            int lastType = getLastPointType();

            int pointPos = BarPos - zzLen;
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
                    float lastHighPrice = RefData(Arr_RealGD, 0);
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
                    float lastLowPrice = RefData(Arr_RealDD, 0);
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

            addPosPoint(zzLen, con_dd, con_gd);
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

        private void addPosPoint(int zzLen, bool con_dd, bool con_gd)
        {
            //找到疑似的高点低点
            if (con_dd)
                AddData(Arr_PosDD, BarPos - zzLen);
            if (con_gd)
                AddData(Arr_PosGD, BarPos - zzLen);
        }

        private int getLastPointType()
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
