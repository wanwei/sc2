﻿using com.wer.sc.data.receiver.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.receiver;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    /// <summary>
    /// 单个股票或期货的接收器
    /// 该类实现
    /// </summary>
    public class RealTimeDataReceiver_Code : IRealTimeData_Code, IRealTimeDataReader
    {
        private string code;

        private int date;

        private ITickData tickData;

        private TimeLineData_Present timeLineData;

        private Dictionary<KLinePeriod, KLineData_Present> dicKLineData = new Dictionary<KLinePeriod, KLineData_Present>();

        public event DelegateOnRealTimeChanged OnRealTimeChanged;

        public string Code
        {
            get
            {
                return code;
            }
        }

        public double Time
        {
            get
            {
                return tickData.Time;
            }
        }

        public float Price
        {
            get
            {
                return tickData.Price;
            }
        }

        public IList<string> ListenedCodes
        {
            get
            {
                return new string[] { Code };
            }
        }

        public RealTimeDataReceiver_Code(string code, int date, IDataReader dataReaderFactory, int lastOpenDate, List<KLinePeriod> periods, List<double[]> openTime)
        {
            this.code = code;
            this.date = date;
            RecentKLineDataLoader recentKLineDataLoader = new RecentKLineDataLoader(dataReaderFactory);
            for (int i = 0; i < periods.Count; i++)
            {
                KLinePeriod period = periods[i];
                //dicKLineData.Add(period, new KLineData_Present(recentKLineDataLoader.GetRecentKLineData(code, lastOpenDate, period), KLineTimeListUtils.GetKLineTimeList(date, lastOpenDate, openTime, period), period));
            }
        }

        public void Receive(ITickData tickData, ITickBar tickBar)
        {
            this.tickData = tickData;
            foreach (KLineData_Present klineData in dicKLineData.Values)
            {
                klineData.Receive(tickBar);
            }
            timeLineData.Receive(tickBar);
            if (this.OnRealTimeChanged != null)
                this.OnRealTimeChanged(this, new RealTimeChangedArgument(-1, Time, this));
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            KLineData_Present klineData;
            bool exist = dicKLineData.TryGetValue(period, out klineData);
            if (exist)
                return klineData;
            return null;
        }

        public ITickData GetTickData()
        {
            return tickData;
        }

        public ITimeLineData GetTimeLineData()
        {
            return timeLineData;
        }

        public bool IsPeriodEnd(KLinePeriod period)
        {
            //TODO 需要实现获取当日K线每个时间
            return false;
        }

        public IRealTimeData_Code GetRealTimeData(string code)
        {
            return this;
        }
    }
}