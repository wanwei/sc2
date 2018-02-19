using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_Code_TimeLine
    {
        private IDataPackage_Code dataPackage;

        private double time;

        private int date;

        private ITradingTimeReader_Code sessionReader;

        private TimeLineData timeLineData;

        private TimeLineData_RealTime timeLineData_RealTime;

        private ITickData tickData;

        public DataNavigate_Code_TimeLine(IDataPackage_Code dataPackage, double time)
        {
            this.dataPackage = dataPackage;     
            this.sessionReader = dataPackage.GetTradingTimeReader();
            this.ChangeTime(time);
        }

        public double Time
        {
            get { return time; }
        }

        public void ChangeTime(double time)
        {
            if (this.time == time)
                return;
            this.time = time;

            int date = this.sessionReader.GetTradingDay(time);
            if (date < 0)
                date = this.sessionReader.GetRecentTradingDay(time);

            if (this.date != date)
            {
                this.date = date;
                this.timeLineData_RealTime = GetTimeLineData_RealTime(date, time);
            }
            else
            {
                this.timeLineData_RealTime = GetTimeLineData_RealTime(timeLineData, tickData, time);
            }
        }

        private TimeLineData_RealTime GetTimeLineData_RealTime(TimeLineData timeLineData, ITickData tickData, double time)
        {
            this.timeLineData_RealTime = new TimeLineData_RealTime(this.timeLineData);
            int timeLineIndex = TimeIndeierUtils.IndexOfTime_TimeLine(this.timeLineData, time);
                //this.timeLineData_RealTime.IndexOfTime(time);            
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            double klineTime = timeLineData_RealTime.Arr_Time[timeLineIndex];
            int startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime);

            //IKLineData klineData = dataPackage.GetKLineData(date, date, 1, 0, KLinePeriod.KLinePeriod_1Day);
            float lastEndPrice = dataPackage.GetLastEndPrice(date);
            TimeLineBar klineBar = GetTimeLineBar(tickData, startTickIndex, tickIndex, lastEndPrice);
            timeLineData_RealTime.ChangeCurrentBar(klineBar, timeLineIndex);
            return timeLineData_RealTime;
        }

        private TimeLineData_RealTime GetTimeLineData_RealTime(int date, double time)
        {
            this.tickData = dataPackage.GetTickData(date);
            this.timeLineData = (TimeLineData)dataPackage.GetTimeLineData(date);
            return GetTimeLineData_RealTime(timeLineData, tickData, time);
        }

        private TimeLineBar GetTimeLineBar(ITickData tickData, int startIndex, int endIndex, float lastEndPrice)
        {
            TimeLineBar timeLineBar = TimeLineUtils.GetTimeLineBar(tickData.GetBar(startIndex), lastEndPrice);
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                timeLineBar = TimeLineUtils.GetTimeLineBar(timeLineBar, tickData.GetBar(i), lastEndPrice);
            }
            return timeLineBar;
        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData_RealTime;
        }
    }
}
