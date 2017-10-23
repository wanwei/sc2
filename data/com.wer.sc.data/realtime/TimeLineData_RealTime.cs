using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.realtime
{
    public class TimeLineData_RealTime : TimeLineData_Abstract, ITimeLineData
    {
        private ITimeLineData timeLineData;

        private ReadOnlyList_TmpValue<double> list_time;
        private ReadOnlyList_TmpValue<float> list_price;
        private ReadOnlyList_TmpValue<int> list_mount;
        private ReadOnlyList_TmpValue<int> list_hold;
        private ReadOnlyList_TmpValue<float> list_upPercent;
        private ReadOnlyList_TmpValue<float> list_upRange;

        public TimeLineData_RealTime(ITimeLineData timeLineData)
        {
            this.timeLineData = timeLineData;
            this.list_time = new ReadOnlyList_TmpValue<double>(timeLineData.Arr_Time);
            this.list_price = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_Price);
            this.list_mount = new ReadOnlyList_TmpValue<int>(timeLineData.Arr_Mount);
            this.list_hold = new ReadOnlyList_TmpValue<int>(timeLineData.Arr_Hold);
            this.list_upPercent = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_UpPercent);
            this.list_upRange = new ReadOnlyList_TmpValue<float>(timeLineData.Arr_UpRange);
        }

        public void ResetCurrentBar()
        {
            this.ChangeCurrentBar(null);
        }

        public void ChangeCurrentBar(ITimeLineBar chart, int barPos)
        {
            this.BarPos = barPos;
            ReadOnlyList_TmpValue<double> timelist = (ReadOnlyList_TmpValue<double>)Arr_Time;
            ReadOnlyList_TmpValue<float> pricelist = (ReadOnlyList_TmpValue<float>)Arr_Price;
            ReadOnlyList_TmpValue<int> mountlist = (ReadOnlyList_TmpValue<int>)Arr_Mount;
            ReadOnlyList_TmpValue<int> holdlist = (ReadOnlyList_TmpValue<int>)Arr_Hold;
            ReadOnlyList_TmpValue<float> upPercentlist = (ReadOnlyList_TmpValue<float>)Arr_UpPercent;
            ReadOnlyList_TmpValue<float> upRangelist = (ReadOnlyList_TmpValue<float>)Arr_UpRange;

            if (chart == null)
            {
                timelist.ClearTmpValue();
                pricelist.ClearTmpValue();
                mountlist.ClearTmpValue();
                holdlist.ClearTmpValue();
                upPercentlist.ClearTmpValue();
                upRangelist.ClearTmpValue();
            }
            else
            {
                timelist.SetTmpValue(barPos, chart.Time);
                pricelist.SetTmpValue(barPos, chart.Price);
                mountlist.SetTmpValue(barPos, chart.Mount);
                holdlist.SetTmpValue(barPos, chart.Hold);
                upPercentlist.SetTmpValue(barPos, chart.UpPercent);
                upRangelist.SetTmpValue(barPos, chart.UpRange);
            }
        }

        /// <summary>
        /// 修改当前chart，
        /// </summary>
        /// <param name="chart"></param>
        public void ChangeCurrentBar(ITimeLineBar chart)
        {
            ChangeCurrentBar(chart, BarPos);
        }

        //public ITimeLineBar GetCurrentBar_Original()
        //{
        //    return timeLineData.GetCurrentBar();
        //}

        public override string Code
        {
            get
            {
                return timeLineData.Code;
            }

            set
            {

            }
        }

        public override float YesterdayEnd
        {
            get
            {
                return timeLineData.YesterdayEnd;
            }

            set
            {

            }
        }

        #region 完整数据信息

        public override IList<double> Arr_Time
        {
            get
            {
                return list_time;
            }
        }

        public override IList<float> Arr_Price
        {
            get
            {
                return list_price;
            }
        }

        public override IList<int> Arr_Mount
        {
            get
            {
                return list_mount;
            }
        }

        public override IList<int> Arr_Hold
        {
            get
            {
                return list_hold;
            }
        }

        public override IList<float> Arr_UpPercent
        {
            get
            {
                return list_upPercent;
            }
        }

        public override IList<float> Arr_UpRange
        {
            get
            {
                return list_upRange;
            }
        }

        #endregion
        public ITimeLineBar GetCurrentBar_Original()
        {
            return new RealTimeLineBar_RealTime(this, BarPos);
        }

        class RealTimeLineBar_RealTime : TimeLineBar_Abstract
        {
            private int barPos;
            private TimeLineData_RealTime timeLineData_RealTime;

            public RealTimeLineBar_RealTime(TimeLineData_RealTime timeLineData_RealTime, int barPos)
            {
                this.timeLineData_RealTime = timeLineData_RealTime;
                this.barPos = barPos;
            }

            public override string Code
            {
                get
                {
                    return timeLineData_RealTime.Code;
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
                    return timeLineData_RealTime.list_hold.GetRealValue(barPos);
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
                    return timeLineData_RealTime.list_mount.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float Price
            {
                get
                {
                    return timeLineData_RealTime.list_price.GetRealValue(barPos);
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
                    return timeLineData_RealTime.list_time.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float UpPercent
            {
                get
                {
                    return timeLineData_RealTime.list_upPercent.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override float UpRange
            {
                get
                {
                    return timeLineData_RealTime.list_upRange.GetRealValue(barPos);
                }

                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
