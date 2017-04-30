using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver2.data
{
    public class KLineData_Merge : KLineData_Abstract
    {
        private IKLineData klineData1;
        private IKLineData klineData2;

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

        public KLineData_Merge(IKLineData klineData1, IKLineData klineData2)
        {
            this.klineData1 = klineData1;
            this.klineData2 = klineData2;

            this.arr_time = new ReadOnlyList_Merge<double>(klineData1.Arr_Time, klineData2.Arr_Time);
            this.arr_start = new ReadOnlyList_Merge<float>(klineData1.Arr_Start, klineData2.Arr_Start);
            this.arr_high = new ReadOnlyList_Merge<float>(klineData1.Arr_High, klineData2.Arr_High);
            this.arr_low = new ReadOnlyList_Merge<float>(klineData1.Arr_Low, klineData2.Arr_Low);
            this.arr_end = new ReadOnlyList_Merge<float>(klineData1.Arr_End, klineData2.Arr_End);

            this.arr_mount = new ReadOnlyList_Merge<int>(klineData1.Arr_Mount, klineData2.Arr_Mount);
            this.arr_money = new ReadOnlyList_Merge<float>(klineData1.Arr_Money, klineData2.Arr_Money);
            this.arr_hold = new ReadOnlyList_Merge<int>(klineData1.Arr_Hold, klineData2.Arr_Hold);
            this.arr_height = new ReadOnlyList_Merge<float>(klineData1.Arr_Height, klineData2.Arr_Height);
            this.arr_heightPercent = new ReadOnlyList_Merge<float>(klineData1.Arr_HeightPercent, klineData2.Arr_HeightPercent);

            this.arr_blockHigh = new ReadOnlyList_Merge<float>(klineData1.Arr_BlockHigh, klineData2.Arr_BlockHigh);
            this.arr_BlockLow = new ReadOnlyList_Merge<float>(klineData1.Arr_BlockLow, klineData2.Arr_BlockLow);
            this.arr_blockHeight = new ReadOnlyList_Merge<float>(klineData1.Arr_BlockHeight, klineData2.Arr_BlockHeight);
            this.arr_blockHeightPercent = new ReadOnlyList_Merge<float>(klineData1.Arr_BlockHeightPercent, klineData2.Arr_BlockHeightPercent);
            this.arr_upPercent = new ReadOnlyList_Merge<float>(klineData1.Arr_UpPercent, klineData2.Arr_UpPercent);
        }

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
    }
}