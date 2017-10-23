﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;

namespace com.wer.sc.data.navigate
{
    public class DataNavigate_Code : IDataNavigate_Code
    {
        private DataForNavigate_Code dataForNavigate;

        public DataNavigate_Code(IDataPackage_Code dataPackage, double time)
        {
            this.dataForNavigate = new DataForNavigate_Code(dataPackage, time);
            this.NavigateTo(time);
        }

        public string Code
        {
            get
            {
                return dataForNavigate.Code;
            }
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataForNavigate.DataPackage;
            }
        }

        public double Time
        {
            get
            {
                return dataForNavigate.Time;
            }
        }

        public event DelegateOnNavigateTo OnNavigateTo;
        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public bool Backward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int prevBarPos = klineData.BarPos - 1;
            if (prevBarPos < 0)
                return false;
            double time = klineData.Arr_Time[prevBarPos];
            NavigateTo(time);
            return true;
        }

        public bool Forward(KLinePeriod forwardPeriod)
        {
            IKLineData klineData = GetKLineData(forwardPeriod);
            int nextBarPos = klineData.BarPos + 1;
            if (nextBarPos >= klineData.Length)
            {
                ITickData tickData = GetTickData();
                if (tickData.BarPos == tickData.Length - 1)
                    return false;
                double endtime = tickData.Arr_Time[tickData.Length - 1];
                NavigateTo(endtime);
                return true;
            }
            double time = klineData.Arr_Time[nextBarPos];
            NavigateTo(time);
            return true;
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            return dataForNavigate.GetKLineData(period);
        }

        public ITickData GetTickData()
        {
            return dataForNavigate.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            return dataForNavigate.GetTimeLineData();
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            return false;
        }

        public bool NavigateTo(double time)
        {
            double prevTime = time;
            bool canNav = this.dataForNavigate.NavigateTo(time);
            if (OnRealTimeChanged != null)
                OnRealTimeChanged(this, new RealTimeChangedArgument(prevTime, this.Time, this));
            if (OnNavigateTo != null)
                OnNavigateTo(this, new DataNavigateEventArgs(Code, Code, prevTime, time));
            return canNav;
        }
    }
}
