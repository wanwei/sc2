using com.wer.sc.data.cache;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 当前的一分钟k线数据构造器
    /// </summary>
    public class RealTimeDataBuilder_DayData
    {
        private ITickData tickData;

        private IKLineData minuteKlineData;

        private double currentTime;

        private KLineBar currentChart = new KLineBar();

        public RealTimeDataBuilder_DayData(IDataCache_CodeDate cache_CodeDate, double currentTime)
        {
            this.minuteKlineData = cache_CodeDate.GetMinuteKLineData();
            this.tickData = cache_CodeDate.GetTickData();
            this.ChangeTime(currentTime);
        }

        public void ChangeTime(double time)
        {
            if (this.currentTime == time)
                return;
            this.currentTime = time;
            double splitTime = Math.Round(currentTime, 4);
            int splitIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, splitTime);
            int currentTickIndex = TimeIndeierUtils.IndexOfTime_Tick(tickData, currentTime);
            tickData.BarPos = currentTickIndex;

            currentChart.Time = splitTime;
            ModifyChart(currentChart, splitIndex, currentTickIndex);
        }

        public IKLineData MinuteKlineData
        {
            get
            {
                return minuteKlineData;
            }
        }

        public ITickData TickData
        {
            get
            {
                return tickData;
            }
        }

        public KLineBar GetCurrentChart()
        {
            return currentChart;
        }

        private void ModifyChart(KLineBar chart, int tickStart, int tickEnd)
        {
            float high = 0;
            float low = float.MaxValue;
            int mount = 0;
            for (int i = tickStart; i <= tickEnd; i++)
            {
                float p = tickData.Arr_Price[i];
                if (high < p)
                    high = p;
                if (low > p)
                    low = p;
                mount += tickData.Arr_Mount[i];
            }

            chart.Code = tickData.Code;
            chart.Start = tickData.Arr_Price[tickStart];
            chart.End = tickData.Arr_Price[tickEnd];
            chart.High = high;
            chart.Low = low;
            chart.Mount = mount;
            chart.Hold = tickData.Arr_Hold[tickEnd];
        }

        public bool Forward(KLinePeriod period, int len)
        {
            return false;
        }

        public bool ForwardTick(int len)
        {
            return false;
        }
    }
}
