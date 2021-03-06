﻿using com.wer.sc.data.reader;
using com.wer.sc.data.store;
using com.wer.sc.data.store.file;
using com.wer.sc.data.transfer;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class TimeLineDataReader : ITimeLineDataReader
    {
        private IDataReader dataReader;

        public TimeLineDataReader(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public ITimeLineData GetData(String code, int date)
        {
            List<ITimeLineData> timeLineDataList = GetData(code, date, date);
            if (timeLineDataList == null || timeLineDataList.Count == 0)
                return null;
            return timeLineDataList[0];
        }

        public ITimeLineData_Extend GetData_Extend(string code, int date)
        {
            ITimeLineData timeLineData = GetData(code, date);
            return new TimeLineData_Extend(timeLineData, dataReader.CreateTradingTimeReader(code).GetTradingTime(date).TradingPeriods);
        }

        public List<ITimeLineData> GetData(String code, int startDate, int endDate)
        {
            if (startDate > endDate)
                return new List<ITimeLineData>();
            CodeInfo codeInfo = dataReader.CodeReader.GetCodeInfo(code);
            if (codeInfo == null)
                return new List<ITimeLineData>();
            if ((codeInfo.End != 0 && codeInfo.End < startDate) || (codeInfo.Start != 0 && codeInfo.Start > endDate))
                return new List<ITimeLineData>();

            int lastTradingDay = dataReader.TradingDayReader.GetPrevTradingDay(startDate);
            IKLineData klineData_Day = dataReader.KLineDataReader.GetData(code, lastTradingDay, lastTradingDay, KLinePeriod.KLinePeriod_1Day);
            IKLineData klineData_1Min = dataReader.KLineDataReader.GetData(code, startDate, endDate, KLinePeriod.KLinePeriod_1Minute);

            ITradingTimeReader_Code tradingSessionReader = dataReader.CreateTradingTimeReader(code);
            float lastEndPrice = klineData_Day == null ? -1 : klineData_Day.Arr_End[0];
            return DataTransfer_KLine2TimeLine.ConvertTimeLineDataList(klineData_1Min, lastEndPrice, tradingSessionReader);
        }
    }
}