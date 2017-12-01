using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.tick
{
    class DataForward_AttachCode_Tick : IRealTimeDataReader_Code
    {
        private double time;

        private IDataPackage_Code dataPackage;

        private ForwardReferedPeriods referedPeriods;

        private DataForForward_Code dataForForward;

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public string Code
        {
            get
            {
                return dataPackage.Code;
            }
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public float Price
        {
            get
            {
                return dataForForward.CurrentTickData.Price;
            }
        }

        public DataForward_AttachCode_Tick(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.dataForForward = new DataForForward_Code(dataPackage, referedPeriods);
        }

        public void ForwardToday(double time)
        {
            this.time = time;

            int prevTickIndex = dataForForward.CurrentTickData.BarPos;
            ForwardToday_Tick(dataForForward.CurrentTickData, time);
            foreach (KLinePeriod period in dataForForward.ReferedKLinePeriods)
            {
                IKLineData_RealTime klineData = dataForForward.GetKLineData(period);
                ForwardToday_KLine(klineData, time, dataForForward.CurrentTickData, prevTickIndex);
            }
            ForwardToday_TimeLine(dataForForward.CurrentTimeLineData, time, dataForForward.CurrentTickData, prevTickIndex);
        }

        private void ForwardToday_Tick(ITickData tickData, double time)
        {
            tickData.BarPos = FindNextTickIndex(tickData, tickData.BarPos, time);
        }

        private int FindNextTickIndex(ITickData tickData, int startTickIndex, double time)
        {
            int barPos = tickData.BarPos;
            while (barPos < tickData.Length && tickData.Arr_Time[barPos] < time)
            {
                barPos++;
            }
            if (barPos == tickData.Length)
                barPos--;
            return barPos;
        }

        private void ForwardToday_KLine(IKLineData_RealTime klineData, double time, ITickData tickData, int prevTickIndex)
        {
            if (klineData.Period.Equals(KLinePeriod.KLinePeriod_1Day))
            {
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(klineData, tickData, prevTickIndex, tickData.BarPos));
                return;
            }

            int nextKLineIndex = DataForward_Code_Tick.FindNextKLineIndex(klineData, time);
            if (nextKLineIndex == klineData.BarPos)
            {
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(klineData, tickData, prevTickIndex, tickData.BarPos));
            }
            else
            {
                double periodStartTime = klineData.Arr_Time[nextKLineIndex];
                int startTickIndex = FindNextTickIndex(tickData, prevTickIndex, time);
                klineData.ChangeCurrentBar(KLineUtils.GetKLineBar(tickData, startTickIndex, tickData.BarPos), nextKLineIndex);
            }
        }

        private void ForwardToday_TimeLine(ITimeLineData_RealTime timeLineData, double time, ITickData tickData, int prevTickIndex)
        {
            if (timeLineData == null)
                return;
            int nextTimeLineIndex = DataForward_Code_Tick.FindNextTimeLineIndex(timeLineData, time);
            if (nextTimeLineIndex == timeLineData.BarPos)
            {
                timeLineData.ChangeCurrentBar(TimeLineUtils.GetTimeLineBar(timeLineData, tickData, prevTickIndex, tickData.BarPos, timeLineData.YesterdayEnd));
            }
            else
            {
                double periodStartTime = timeLineData.Arr_Time[nextTimeLineIndex];
                int startTickIndex = FindNextTickIndex(tickData, prevTickIndex, time);
                timeLineData.ChangeCurrentBar(TimeLineUtils.GetTimeLineBar(tickData, startTickIndex, tickData.BarPos, timeLineData.YesterdayEnd), nextTimeLineIndex);
            }
        }

        public void ForwardNextDay(int tradingDay, double time)
        {
            DataForward_Code_Tick.ForwardNextDay(this.dataForForward, tradingDay);
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return dataForForward.GetKLineData(period);
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        public ITimeLineData GetTimeLineData()
        {
            return dataForForward.CurrentTimeLineData;
        }

        public ITickData GetTickData()
        {
            return dataForForward.CurrentTickData;
        }
    }
}
