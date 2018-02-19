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
    public class DataNavigate_Code_KLine
    {
        private IDataPackage_Code dataPackage;

        private double time;

        private int date;

        private KLinePeriod klinePeriod;

        private KLineData_RealTime klineData_RealTime;

        private IKLineData klineData;

        private ITickData tickData;

        private ITradingTimeReader_Code sessionReader;

        public DataNavigate_Code_KLine(IDataPackage_Code dataPackage, double time, KLinePeriod klinePeriod)
        {
            this.dataPackage = dataPackage;
            this.klinePeriod = klinePeriod;
            this.sessionReader = dataPackage.GetTradingTimeReader();
            this.ChangeTime(time);
        }

        //public DataNavigate_Code_KLine(IDataPackage_Code dataPackage, KLineData_RealTime klineData_RealTime)
        //{
        //    this.dataPackage = dataPackage;
        //    this.klinePeriod = klineData_RealTime.Period;
        //    this.sessionReader = dataPackage.GetTradingTimeReader();
        //    this.klineData_RealTime = klineData_RealTime;
        //}

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
                this.klineData_RealTime = GetKLineData_RealTime(date, time);
            }
            else
            {
                this.klineData_RealTime = GetKLineData_RealTime(klineData, tickData, time);
            }
        }

        private KLineData_RealTime GetKLineData_RealTime(int date, double time)
        {
            this.tickData = dataPackage.GetTickData(date);
            this.klineData = dataPackage.GetKLineData(klinePeriod);
            return GetKLineData_RealTime(klineData, tickData, time);
        }

        private KLineData_RealTime GetKLineData_RealTime(IKLineData klineData, ITickData tickData, double time)
        {
            KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
            int klineIndex = IndexOfTime(klineData_RealTime, klinePeriod, time, date);
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time, true);
            double klineTime = klineData_RealTime.Arr_Time[klineIndex];
            int startTickIndex;
            if (klinePeriod.PeriodType == KLineTimeType.DAY)
                startTickIndex = 0;
            else
                startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime);
            KLineBar klineBar = GetKLineBar(tickData, startTickIndex, tickIndex);
            klineData_RealTime.ChangeCurrentBar(klineBar, klineIndex);
            return klineData_RealTime;
        }

        private int IndexOfTime(IKLineData klineData, KLinePeriod klinePeriod, double time, int date)
        {
            if (klinePeriod.PeriodType == KLineTimeType.DAY)
            {
                return TimeIndeierUtils.IndexOfTime_KLine(klineData, date);
            }
            else
                return TimeIndeierUtils.IndexOfTime_KLine(klineData, time);
        }

        private KLineBar GetKLineBar(ITickData tickData, int startIndex, int endIndex)
        {
            KLineBar klineBar = KLineUtils.GetKLineBar(tickData.GetBar(startIndex));
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                klineBar = KLineUtils.GetKLineBar(klineBar, tickData.GetBar(i));
            }
            return klineBar;
        }

        public IKLineData GetKLineData()
        {
            return klineData_RealTime;
        }
    }
}
