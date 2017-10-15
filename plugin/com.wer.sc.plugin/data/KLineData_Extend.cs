using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineData_Extend : IKLineData_Extend
    {
        private IKLineData klineData;

        private KLineDataTimeInfo timeInfo;

        public KLineData_Extend(IKLineData klineData, KLineDataTimeInfo timeInfo) 
        {
            this.timeInfo = timeInfo;
            this.klineData = klineData;
        }

        public IList<float> Arr_BlockHeight
        {
            get
            {
                return klineData.Arr_BlockHeight;
            }
        }

        public IList<float> Arr_BlockHeightPercent
        {
            get
            {
                return klineData.Arr_BlockHeightPercent;
            }
        }

        public IList<float> Arr_BlockHigh
        {
            get
            {
                return klineData.Arr_BlockHigh;
            }
        }

        public IList<float> Arr_BlockLow
        {
            get
            {
                return klineData.Arr_BlockLow;
            }
        }

        public IList<float> Arr_End
        {
            get
            {
                return klineData.Arr_End;
            }
        }

        public IList<float> Arr_Height
        {
            get
            {
                return klineData.Arr_Height;
            }
        }

        public IList<float> Arr_HeightPercent
        {
            get
            {
                return klineData.Arr_HeightPercent;
            }
        }

        public IList<float> Arr_High
        {
            get
            {
                return klineData.Arr_High;
            }
        }

        public IList<int> Arr_Hold
        {
            get
            {
                return klineData.Arr_Hold;
            }
        }

        public IList<float> Arr_Low
        {
            get
            {
                return klineData.Arr_Low;
            }
        }

        public IList<float> Arr_Money
        {
            get
            {
                return klineData.Arr_Money;
            }
        }

        public IList<int> Arr_Mount
        {
            get
            {
                return klineData.Arr_Mount;
            }
        }

        public IList<float> Arr_Start
        {
            get
            {
                return klineData.Arr_Start;
            }
        }

        public IList<double> Arr_Time
        {
            get
            {
                return klineData.Arr_Time;
            }
        }

        public IList<float> Arr_UpPercent
        {
            get
            {
                return klineData.Arr_UpPercent;
            }
        }

        public int BarPos
        {
            get
            {
                return klineData.BarPos;
            }

            set
            {
                klineData.BarPos = value;
            }
        }

        public float BlockHeight
        {
            get
            {
                return klineData.BlockHeight;
            }
        }

        public float BlockHeightPercent
        {
            get
            {
                return klineData.BlockHeightPercent;
            }
        }

        public float BlockHigh
        {
            get
            {
                return klineData.BlockHigh;
            }
        }

        public float BlockLow
        {
            get
            {
                return klineData.BlockLow;
            }
        }

        public float BlockMiddle
        {
            get
            {
                return klineData.BlockMiddle;
            }
        }

        public float BottomShadow
        {
            get
            {
                return klineData.BottomShadow;
            }
        }

        public string Code
        {
            get
            {
                return klineData.Code;
            }
        }

        public float End
        {
            get
            {
                return klineData.End;
            }
        }

        public float Height
        {
            get
            {
                return klineData.Height;
            }
        }

        public float HeightPercent
        {
            get
            {
                return klineData.HeightPercent;
            }
        }

        public float High
        {
            get
            {
                return klineData.High;
            }
        }

        public int Hold
        {
            get
            {
                return klineData.Hold;
            }
        }

        public int Length
        {
            get
            {
                return klineData.Length;
            }
        }

        public float Low
        {
            get
            {
                return klineData.Low;
            }
        }

        public float Middle
        {
            get
            {
                return klineData.Middle;
            }
        }

        public float Money
        {
            get
            {
                return klineData.Money;
            }
        }

        public int Mount
        {
            get
            {
                return klineData.Mount;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return klineData.Period;
            }

            set
            {
                klineData.Period = value;
            }
        }

        public float Start
        {
            get
            {
                return klineData.Start;
            }
        }

        public double Time
        {
            get
            {
                return klineData.Time;
            }
        }

        public float TopShadow
        {
            get
            {
                return klineData.TopShadow;
            }
        }

        public IKLineBar GetAggrKLineBar(int startPos, int endPos)
        {
            return klineData.GetAggrKLineBar(startPos, endPos);
        }

        public IKLineBar GetBar(int index)
        {
            return klineData.GetBar(index);
        }

        public IKLineBar GetCurrentBar()
        {
            return klineData.GetCurrentBar();
        }

        public IKLineData GetRange(int startPos, int endPos)
        {
            return klineData.GetRange(startPos, endPos);
        }

        public int IndexOfTime(double time)
        {
            return klineData.IndexOfTime(time);
        }

        public bool isRed()
        {
            return klineData.isRed();
        }

        public IKLineData Sub(int startPos, int endPos)
        {
            return klineData.Sub(startPos, endPos);
        }

        public string ToString(int index)
        {
            return klineData.ToString(index);
        }

        public override string ToString()
        {
            return klineData.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<int> GetTradingTimeEndBarPoses()
        {
            return timeInfo.TradingTimeEndBarPoses;
        }

        /// <summary>
        /// 得到交易日
        /// </summary>
        /// <returns></returns>
        public IList<int> GetTradingDayEndBarPoses()
        {
            return timeInfo.DayEndBarPoses;
        }

        public bool IsDayStart(int barPos)
        {
            return timeInfo.IsDayStart(barPos);
        }

        public bool IsDayEnd(int barPos)
        {
            return timeInfo.IsDayEnd(barPos);
        }

        public bool IsTradingTimeStart(int barPos)
        {
            return timeInfo.IsPeriodStart(barPos);
        }

        public bool IsTradingTimeEnd(int barPos)
        {
            return timeInfo.IsPeriodEnd(barPos);
        }

        public double GetEndTime(int barPos)
        {
            return timeInfo.GetKLineTime(barPos)[1];
        }
    }
}