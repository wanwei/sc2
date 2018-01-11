using com.wer.sc.data;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.common.zigzag
{
    /// <summary>
    /// zigzag算法
    /// </summary>
    public class Zigzag : IDataLooper
    {
        private IKLineData klineData;

        private Zigzag_Simple zigzag_Simple;

        private Zigzag_Merge zigzag_Merge;

        public Zigzag(IKLineData klineData) : this(klineData, 2, 5)
        {
        }

        public Zigzag(IKLineData klineData, int turnLength, int highLowLength)
        {
            this.klineData = klineData;
            this.zigzag_Simple = new Zigzag_Simple(klineData, turnLength, highLowLength);
            this.zigzag_Merge = new Zigzag_Merge(zigzag_Simple);
        }

        public void Loop(int barPos)
        {
            this.zigzag_Merge.Loop(barPos);
        }

        public List<ZigzagPoint> GetPoints()
        {
            return zigzag_Simple.GetPoints();
        }

        public List<ZigzagPoint> GetMergedPoints()
        {
            return zigzag_Merge.GetMergedPoints();
        }
    }
}