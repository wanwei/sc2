using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate.impl
{
    public class DataNavigate_Code_KLine
    {
        private int startDate;

        private int endDate;

        private IDataReader dataReader;

        private string code;

        private double time;

        private int date;

        private KLinePeriod klinePeriod;

        private KLineData_RealTime klineData_RealTime;

        private IKLineData klineData;

        private ITickData tickData;

        private ITradingSessionReader_Instrument sessionReader;

        public DataNavigate_Code_KLine(IDataReader dataReader, string code, double time, KLinePeriod klinePeriod) : this(dataReader, code, time, klinePeriod, -1, -1)
        {

        }

        public DataNavigate_Code_KLine(IDataReader dataReader, string code, double time, KLinePeriod klinePeriod, int startDate, int endDate)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.klinePeriod = klinePeriod;
            this.sessionReader = dataReader.CreateTradingSessionReader(code);
            this.startDate = startDate;
            this.endDate = endDate;
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
                this.klineData_RealTime = GetKLineData_RealTime(date, time);
            }
            else
            {
                this.klineData_RealTime = GetKLineData_RealTime(klineData, tickData, time);
            }
        }

        private KLineData_RealTime GetKLineData_RealTime(int date, double time)
        {
            this.tickData = dataReader.TickDataReader.GetTickData(code, date);
            this.klineData = GetKLineData(date, time);
            return GetKLineData_RealTime(klineData, tickData, time);
        }

        private IKLineData GetKLineData(int date, double time)
        {
            if (this.klinePeriod.PeriodType == KLineTimeType.SECOND)
            {
                return dataReader.KLineDataReader.GetData(code, date, date, klinePeriod);
            }
            else
            {
                return dataReader.KLineDataReader.GetData(code, date, date, 1000, 0, klinePeriod);
            }
        }

        private KLineData_RealTime GetKLineData_RealTime(IKLineData klineData, ITickData tickData, double time)
        {
            KLineData_RealTime klineData_RealTime = new KLineData_RealTime(klineData);
            int klineIndex = IndexOfTime(klineData_RealTime, klinePeriod, time, date);
            int tickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
            double klineTime = klineData_RealTime.Arr_Time[klineIndex];
            int startTickIndex;
            if (klinePeriod.PeriodType == KLineTimeType.DAY)
                startTickIndex = 0;
            else
                startTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, klineTime);
            KLineBar klineBar = GetKLineBar(tickData, startTickIndex, tickIndex);
            klineData_RealTime.SetRealTimeData(klineBar, klineIndex);
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
