using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.platform
{
    /// <summary>
    /// 平台，指一段波动比较小的时间段
    /// </summary>
    public class Platform : IStrategyQueryResultRow
    {
        public static string[] Title = new string[] { "开始时间", "结束时间", "长度", "高度", "上沿", "下沿" };

        private List<object> data = new List<object>();

        private int topCalCount = 5;

        private StrategyArray<KLineBarInfo> topList = new StrategyArray<KLineBarInfo>();

        private StrategyArray<KLineBarInfo> bottomList = new StrategyArray<KLineBarInfo>();

        private IKLineData klineData;

        private int startIndex;

        private int endIndex;

        public IKLineData KlineData
        {
            get
            {
                return klineData;
            }

            set
            {
                klineData = value;
            }
        }

        public int StartIndex
        {
            get
            {
                return startIndex;
            }

            set
            {
                startIndex = value;
            }
        }

        public int EndIndex
        {
            get
            {
                return endIndex;
            }

            set
            {
                endIndex = value;
            }
        }

        public StrategyArray<KLineBarInfo> TopList
        {
            get
            {
                return topList;
            }
        }

        public StrategyArray<KLineBarInfo> BottomList
        {
            get
            {
                return bottomList;
            }
        }

        public string Code
        {
            get
            {
                return klineData.Code;
            }
        }

        public double Time
        {
            get
            {
                return klineData.Arr_Time[endIndex];
            }
        }

        public IList<object> Data
        {
            get
            {
                return data;
            }
        }

        public Platform(IKLineData klineData, int startIndex, int endIndex) : this(klineData, startIndex, endIndex, 5)
        {
        }

        public Platform(IKLineData klineData, int startIndex, int endIndex, int topCalCount)
        {
            this.klineData = klineData;
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.topCalCount = topCalCount;
            this.Init();
        }

        private void Init()
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                float high = klineData.Arr_High[i];
                AddHighData(high, i);
                float low = klineData.Arr_Low[i];
                AddLowData(low, i);
            }

            data.Add(klineData.Arr_Time[startIndex]);
            data.Add(klineData.Arr_Time[endIndex]);
            data.Add(endIndex - startIndex + 1);
            double highPrice = topList[0].KLineBar.High;
            double lowPrice = bottomList[0].KLineBar.Low;
            data.Add(highPrice - lowPrice);
            data.Add(highPrice);
            data.Add(lowPrice);
        }

        private void AddHighData(float highValue, int barPos)
        {
            int index = FindInsertHighIndex(highValue);
            if (index < 0)
                return;
            InsertIntoHighData(highValue, barPos, index);
        }

        private int FindInsertHighIndex(float high)
        {
            for (int i = topCalCount - 1; i >= 0; i--)
            {
                KLineBarInfo klineBarInfo = topList[i];
                if (klineBarInfo == null)
                    continue;
                double highValue = klineBarInfo.KLineBar.High;
                if (high < highValue)
                {
                    int insertIndex = i + 1;
                    if (insertIndex >= topCalCount)
                        return -1;
                    return insertIndex;
                }
            }
            return 0;
        }

        private void InsertIntoHighData(float high, int barPos, int index)
        {
            for (int i = topCalCount - 1; i > index; i--)
            {
                topList[i] = topList[i - 1];
            }
            topList[index] = new KLineBarInfo(klineData.GetBar(barPos), barPos);
        }

        private void AddLowData(float lowValue, int barPos)
        {
            int index = FindInsertLowIndex(lowValue);
            if (index < 0)
                return;
            InsertIntoLowData(lowValue, barPos, index);
        }

        private int FindInsertLowIndex(float low)
        {
            for (int i = topCalCount - 1; i >= 0; i--)
            {
                KLineBarInfo klineBarInfo = bottomList[i];
                if (klineBarInfo == null)
                    continue;
                double lowValue = klineBarInfo.KLineBar.Low;
                if (low > lowValue)
                {
                    int insertIndex = i + 1;
                    if (insertIndex >= topCalCount)
                        return -1;
                    return insertIndex;
                }
            }
            return 0;
        }

        private void InsertIntoLowData(float low, int barPos, int index)
        {
            for (int i = topCalCount - 1; i > index; i--)
            {
                bottomList[i] = bottomList[i - 1];
            }
            bottomList[index] = new KLineBarInfo(klineData.GetBar(barPos), barPos);
        }

        public override string ToString()
        {
            return startIndex + "," + endIndex + "," + TopList[0] + "," + BottomList[0];
        }
    }

    public class KLineBarListInfo
    {
        public int StartBarPos;

        public int EndBarPos;

        public IKLineData KlineData;
    }

    public class KLineBarInfo
    {
        public int BarPos;

        public IKLineBar KLineBar;

        public KLineBarInfo()
        {

        }

        public KLineBarInfo(IKLineBar klineBar, int barPos)
        {
            this.KLineBar = klineBar;
            this.BarPos = barPos;
        }
    }
}