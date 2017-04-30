using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// K线数据的子类，用于获取一段完整K线数据中的一段，
    /// 如现在得到的K线数据是20140101-20150101，该类可以实现截取中间的一段如20140501-20140601，而不需要重新拷贝数据
    /// </summary>
    public class KLineData_Sub : KLineData_Abstract, IKLineData
    {
        private IKLineData klineData;

        private int klineStartIndex;
        private int klineEndIndex;

        private IList<double> arr_time;
        private IList<float> arr_start;
        private IList<float> arr_high;
        private IList<float> arr_low;
        private IList<float> arr_end;
        private IList<int> arr_mount;
        private IList<float> arr_money;
        private IList<int> arr_hold;
        private IList<float> arr_height;
        private IList<float> arr_heightPercent;
        private IList<float> arr_blockHigh;
        private IList<float> arr_BlockLow;
        private IList<float> arr_blockHeight;
        private IList<float> arr_blockHeightPercent;
        private IList<float> arr_upPercent;

        public KLineData_Sub(IKLineData klineData, int startIndex, int endIndex)
        {
            this.klineData = klineData;
            this.klineStartIndex = startIndex;
            this.klineEndIndex = endIndex;

            this.arr_time = new ReadOnlyList_Sub<double>(klineData.Arr_Time, startIndex, endIndex);
            this.arr_start = new ReadOnlyList_Sub<float>(klineData.Arr_Start, startIndex, endIndex);
            this.arr_high = new ReadOnlyList_Sub<float>(klineData.Arr_High, startIndex, endIndex);
            this.arr_low = new ReadOnlyList_Sub<float>(klineData.Arr_Low, startIndex, endIndex);
            this.arr_end = new ReadOnlyList_Sub<float>(klineData.Arr_End, startIndex, endIndex);
            this.arr_mount = new ReadOnlyList_Sub<int>(klineData.Arr_Mount, startIndex, endIndex);
            this.arr_money = new ReadOnlyList_Sub<float>(klineData.Arr_Money, startIndex, endIndex);
            this.arr_hold = new ReadOnlyList_Sub<int>(klineData.Arr_Hold, startIndex, endIndex);
            this.arr_height = new ReadOnlyList_Sub<float>(klineData.Arr_Height, startIndex, endIndex);
            this.arr_heightPercent = new ReadOnlyList_Sub<float>(klineData.Arr_HeightPercent, startIndex, endIndex);
            this.arr_blockHigh = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHigh, startIndex, endIndex);
            this.arr_BlockLow = new ReadOnlyList_Sub<float>(klineData.Arr_BlockLow, startIndex, endIndex);
            this.arr_blockHeight = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHeight, startIndex, endIndex);
            this.arr_blockHeightPercent = new ReadOnlyList_Sub<float>(klineData.Arr_BlockHeightPercent, startIndex, endIndex);
            this.arr_upPercent = new ReadOnlyList_Sub<float>(klineData.Arr_UpPercent, startIndex, endIndex);
        }

        #region 得到完整数据

        public override IList<double> Arr_Time
        {
            get
            {
                return arr_time;
            }
        }
        public override IList<float> Arr_Start
        {
            get
            {
                return arr_start;
            }
        }

        public override IList<float> Arr_High
        {
            get
            {
                return arr_high;
            }
        }


        public override IList<float> Arr_Low
        {
            get
            {
                return arr_low;
            }
        }


        public override IList<float> Arr_End
        {
            get
            {
                return arr_end;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return arr_mount;
            }
        }

        public override IList<float> Arr_Money
        {
            get
            {
                return arr_money;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return arr_hold;
            }
        }

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public override IList<float> Arr_Height
        {
            get
            {
                return arr_height;
            }
        }

        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public override IList<float> Arr_HeightPercent
        {
            get
            {
                return arr_heightPercent;
            }
        }

        public override IList<float> Arr_BlockHigh
        {
            get
            {
                return arr_blockHigh;
            }
        }

        public override IList<float> Arr_BlockLow
        {
            get
            {
                return arr_BlockLow;
            }
        }

        public override IList<float> Arr_BlockHeight
        {
            get
            {
                return arr_blockHeight;
            }
        }

        public override IList<float> Arr_BlockHeightPercent
        {
            get
            {
                return arr_blockHeightPercent;
            }
        }

        public override IList<float> Arr_UpPercent
        {
            get
            {
                return arr_upPercent;
            }
        }

        #endregion
    }
}