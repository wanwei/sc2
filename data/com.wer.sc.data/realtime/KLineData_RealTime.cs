using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    /// <summary>
    /// 实时k线数据
    /// 
    /// </summary>
    public class KLineData_RealTime : KLineData_Abstract, IKLineData
    {
        private IKLineData klineData;

        private ReadOnlyList_TmpValue<double> list_Time;
        private ReadOnlyList_TmpValue<float> list_Start;
        private ReadOnlyList_TmpValue<float> list_High;
        private ReadOnlyList_TmpValue<float> list_Low;
        private ReadOnlyList_TmpValue<float> list_End;
        private ReadOnlyList_TmpValue<int> list_Mount;
        private ReadOnlyList_TmpValue<float> list_Money;
        private ReadOnlyList_TmpValue<int> list_Hold;
        private ReadOnlyList_TmpValue<float> list_Height;
        private ReadOnlyList_TmpValue<float> list_HeightPercent;
        private ReadOnlyList_TmpValue<float> list_BlockHigh;
        private ReadOnlyList_TmpValue<float> list_BlockLow;
        private ReadOnlyList_TmpValue<float> list_BlockHeight;
        private ReadOnlyList_TmpValue<float> list_BlockHeightPercent;
        private ReadOnlyList_TmpValue<float> list_UpPercent;

        public KLineData_RealTime(IKLineData klineData)
        {
            this.klineData = klineData;

            this.list_Time = new ReadOnlyList_TmpValue<double>(klineData.Arr_Time);
            this.list_Start = new ReadOnlyList_TmpValue<float>(klineData.Arr_Start);
            this.list_High = new ReadOnlyList_TmpValue<float>(klineData.Arr_High);
            this.list_Low = new ReadOnlyList_TmpValue<float>(klineData.Arr_Low);
            this.list_End = new ReadOnlyList_TmpValue<float>(klineData.Arr_End);
            this.list_Mount = new ReadOnlyList_TmpValue<int>(klineData.Arr_Mount);
            this.list_Money = new ReadOnlyList_TmpValue<float>(klineData.Arr_Money);
            this.list_Hold = new ReadOnlyList_TmpValue<int>(klineData.Arr_Hold);
            this.list_Height = new ReadOnlyList_TmpValue<float>(klineData.Arr_Height);
            this.list_HeightPercent = new ReadOnlyList_TmpValue<float>(klineData.Arr_HeightPercent);
            this.list_BlockHigh = new ReadOnlyList_TmpValue<float>(klineData.Arr_BlockHigh);
            this.list_BlockLow = new ReadOnlyList_TmpValue<float>(klineData.Arr_BlockLow);
            this.list_BlockHeight = new ReadOnlyList_TmpValue<float>(klineData.Arr_BlockHeight);
            this.list_BlockHeightPercent = new ReadOnlyList_TmpValue<float>(klineData.Arr_BlockHeightPercent);
            this.list_UpPercent = new ReadOnlyList_TmpValue<float>(klineData.Arr_UpPercent);
        }

        public void SetRealTimeData(IKLineBar chart, int barPos)
        {
            this.BarPos = barPos;
            if (chart == null)
            {
                list_Time.ClearTmpValue();
                list_Start.ClearTmpValue();
                list_High.ClearTmpValue();
                list_Low.ClearTmpValue();
                list_End.ClearTmpValue();
                list_Mount.ClearTmpValue();
                list_Money.ClearTmpValue();
                list_Hold.ClearTmpValue();

                list_Height.ClearTmpValue();
                list_HeightPercent.ClearTmpValue();
                list_BlockHigh.ClearTmpValue();
                list_BlockLow.ClearTmpValue();
                list_BlockHeight.ClearTmpValue();
                list_BlockHeightPercent.ClearTmpValue();
                list_UpPercent.ClearTmpValue();
            }
            else
            {
                list_Time.SetTmpValue(barPos, chart.Time);
                list_Start.SetTmpValue(barPos, chart.Start);
                list_High.SetTmpValue(barPos, chart.High);
                list_Low.SetTmpValue(barPos, chart.Low);
                list_End.SetTmpValue(barPos, chart.End);
                list_Mount.SetTmpValue(barPos, chart.Mount);
                list_Money.SetTmpValue(barPos, chart.Money);
                list_Hold.SetTmpValue(barPos, chart.Hold);

                list_Height.SetTmpValue(barPos, chart.Height);
                list_HeightPercent.SetTmpValue(barPos, chart.HeightPercent);
                list_BlockHigh.SetTmpValue(barPos, chart.BlockHigh);
                list_BlockLow.SetTmpValue(barPos, chart.BlockLow);
                list_BlockHeight.SetTmpValue(barPos, chart.BlockHeight);
                list_BlockHeightPercent.SetTmpValue(barPos, chart.BlockHeightPercent);
                float upPercent = barPos == 0 ?
                    (float)NumberUtils.percent(chart.End, chart.Start) :
                    (float)NumberUtils.percent(chart.End, klineData.Arr_End[barPos - 1]);
                list_UpPercent.SetTmpValue(barPos, upPercent);
            }
        }

        /// <summary>
        /// 修改当前chart，
        /// </summary>
        /// <param name="chart"></param>
        public void SetRealTimeData(IKLineBar chart)
        {
            SetRealTimeData(chart, BarPos);
        }

        #region 得到完整数据

        public override IList<double> Arr_Time
        {
            get
            {
                return list_Time;
            }
        }
        public override IList<float> Arr_Start
        {
            get
            {
                return list_Start;
            }
        }

        public override IList<float> Arr_High
        {
            get
            {
                return list_High;
            }
        }


        public override IList<float> Arr_Low
        {
            get
            {
                return list_Low;
            }
        }

        public override IList<float> Arr_End
        {
            get
            {
                return list_End;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return list_Mount;
            }
        }

        public override IList<float> Arr_Money
        {
            get
            {
                return list_Money;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return list_Hold;
            }
        }

        /// <summary>
        /// 得到每个k线的振幅数组
        /// </summary>
        public override IList<float> Arr_Height
        {
            get
            {
                return list_Height;
            }
        }


        /// <summary>
        /// 得到每个k线的振幅百分比数组
        /// </summary>
        public override IList<float> Arr_HeightPercent
        {
            get
            {
                return list_HeightPercent;
            }
        }

        public override IList<float> Arr_BlockHigh
        {
            get
            {
                return list_BlockHigh;
            }
        }

        public override IList<float> Arr_BlockLow
        {
            get
            {
                return list_BlockLow;
            }
        }

        public override IList<float> Arr_BlockHeight
        {
            get
            {
                return list_BlockHeight;
            }
        }


        public override IList<float> Arr_BlockHeightPercent
        {
            get
            {
                return list_BlockHeightPercent;
            }
        }


        public override IList<float> Arr_UpPercent
        {
            get
            {
                return list_UpPercent;
            }
        }

        #endregion       

        public IKLineBar GetCurrentRealBar()
        {
            return new RealKLineBar_RealTime(this, BarPos);
        }

        class RealKLineBar_RealTime : KLineBar_Abstract
        {
            private KLineData_RealTime klineData;

            private int barPos;

            public RealKLineBar_RealTime(KLineData_RealTime klineData, int barPos)
            {
                this.klineData = klineData;
                this.barPos = barPos;
            }

            public override string Code
            {
                get
                {
                    return klineData.Code;
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float End
            {
                get
                {
                    return klineData.list_End.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float High
            {
                get
                {
                    return klineData.list_High.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override int Hold
            {
                get
                {
                    return klineData.list_Hold.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float Low
            {
                get
                {
                    return klineData.list_Low.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float Money
            {
                get
                {
                    return klineData.list_Money.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override int Mount
            {
                get
                {
                    return klineData.list_Mount.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float Start
            {
                get
                {
                    return klineData.list_Start.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override double Time
            {
                get
                {
                    return klineData.list_Time.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}