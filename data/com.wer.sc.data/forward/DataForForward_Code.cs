﻿using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.forward
{
    /// <summary>
    /// 导航使用的数据
    /// </summary>
    public class DataForForward_Code : IDataForForward_Code
    {
        private IDataCenter dataCenter;

        //数据包
        protected IDataPackage_Code dataPackage;

        //数据引用的周期
        protected ForwardReferedPeriods referedPeriods;

        //当前交易日
        protected int tradingDay;

        private IKLineData_RealTime mainKLineData;

        private KLinePeriod mainKLinePeriod;

        //该周期内所有K线数据
        protected Dictionary<KLinePeriod, IKLineData_RealTime> dic_Period_KLineData = new Dictionary<KLinePeriod, IKLineData_RealTime>();

        //交易日缓存
        protected CacheUtils_TradingDay cache_TradingDay;

        //当前交易日的tick数据
        protected ITickData_Extend currentTickData;

        //当前交易日的分时数据
        protected ITimeLineData_RealTime currentTimeLineData;

        public DataForForward_Code(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        public DataForForward_Code(IDataPackage_Code dataPackage, ForwardReferedPeriods referedPeriods)
        {
            this.dataPackage = dataPackage;
            this.referedPeriods = referedPeriods;
            this.dic_Period_KLineData = dataPackage.CreateKLineData_RealTimes(referedPeriods.UsedKLinePeriods);
            this.mainKLinePeriod = referedPeriods.GetMinPeriod();
            this.mainKLineData = this.dic_Period_KLineData[mainKLinePeriod];
            this.cache_TradingDay = new CacheUtils_TradingDay(dataPackage.GetTradingDays());
        }

        public IDataPackage_Code DataPackage
        {
            get
            {
                return dataPackage;
            }
        }

        public string Code
        {
            get { return dataPackage.Code; }
        }

        public int TradingDay
        {
            get
            {
                return tradingDay;
            }
            set
            {
                if (tradingDay != value && cache_TradingDay.IsTrade(value))
                {
                    this.tradingDay = value;
                    if (this.referedPeriods.UseTickData)
                        this.currentTickData = dataPackage.CreateTickData_RealTime(TradingDay);
                    if (this.referedPeriods.UseTimeLineData)
                        this.currentTimeLineData = dataPackage.CreateTimeLineData_RealTime(TradingDay);
                }
            }
        }

        public int GetNextTradingDay()
        {
            return cache_TradingDay.GetNextTradingDay(tradingDay);
        }

        public ITradingDayReader TradingDayReader
        {
            get
            {
                return cache_TradingDay;
            }
        }

        public int StartDate
        {
            get { return dataPackage.StartDate; }
        }

        public int EndDate
        {
            get { return dataPackage.EndDate; }
        }

        public bool UseTickData
        {
            get { return referedPeriods.UseTickData; }
        }

        public bool UseTimeLineData
        {
            get { return referedPeriods.UseTimeLineData; }
        }

        /// <summary>
        /// 数据包里的K线周期
        /// </summary>
        public IList<KLinePeriod> ReferedKLinePeriods
        {
            get { return referedPeriods.UsedKLinePeriods; }
        }

        public KLinePeriod MainKLinePeriod
        {
            get
            {
                return mainKLinePeriod;
            }
        }

        public IKLineData_RealTime MainKLine
        {
            get { return mainKLineData; }
        }

        public ForwardReferedPeriods ReferedPeriods
        {
            get { return referedPeriods; }
        }

        public virtual IKLineData_RealTime GetKLineData(KLinePeriod klinePeriod)
        {
            if (dic_Period_KLineData.ContainsKey(klinePeriod))
                return dic_Period_KLineData[klinePeriod];
            return null;
        }

        internal void SetKLineData(KLinePeriod period, IKLineData_RealTime klineData)
        {
            if (dic_Period_KLineData.ContainsKey(period))
                dic_Period_KLineData[period] = klineData;
            else
                dic_Period_KLineData.Add(period, klineData);
        }

        public void Save(XmlElement xmlElem)
        {
            this.dataPackage.Save(xmlElem);
            this.referedPeriods.Save(xmlElem);
            xmlElem.SetAttribute("tradingDay", tradingDay.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            this.dataPackage = this.dataCenter.DataPackageFactory.CreateDataPackage_Code(xmlElem);
            this.referedPeriods = new ForwardReferedPeriods();
            this.referedPeriods.Load(xmlElem);

            this.dic_Period_KLineData = dataPackage.CreateKLineData_RealTimes(referedPeriods.UsedKLinePeriods);
            this.cache_TradingDay = new CacheUtils_TradingDay(dataPackage.GetTradingDays());
            this.TradingDay = int.Parse(xmlElem.GetAttribute("tradingDay"));
        }

        public IKLineData_RealTime GetMainKLineData()
        {
            return mainKLineData;
        }

        public virtual ITickData_Extend CurrentTickData
        {
            get
            {
                return currentTickData;
            }

            set
            {
                currentTickData = value;
            }
        }

        public virtual ITimeLineData_RealTime CurrentTimeLineData
        {
            get
            {
                return currentTimeLineData;
            }

            set
            {
                currentTimeLineData = value;
            }
        }

        public virtual double Time
        {
            get
            {
                if (currentTickData != null)
                    return currentTickData.Time;
                if (currentTimeLineData != null)
                    return currentTimeLineData.Time;
                foreach (KLinePeriod period in dic_Period_KLineData.Keys)
                {
                    return dic_Period_KLineData[period].Time;
                }
                return 0;
            }
        }

        public float Price
        {
            get
            {
                if (currentTickData != null)
                    return currentTickData.Price;
                if (currentTimeLineData != null)
                    return currentTimeLineData.Price;
                foreach (KLinePeriod period in dic_Period_KLineData.Keys)
                {
                    return dic_Period_KLineData[period].End;
                }
                return 0;
            }
        }
    }
}