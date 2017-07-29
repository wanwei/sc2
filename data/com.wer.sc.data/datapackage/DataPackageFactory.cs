using com.wer.sc.data.datapackage.impl;
using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    public class DataPackageFactory
    {
        public static IDataPackage CreateDataPackage(IDataReader dataReader, string code, int startDate, int endDate)
        {
            //return new DataPackage(dataReader, code, startDate, endDate, 500, 100);
            return CreateDataPackage(dataReader, code, startDate, endDate, 500, 100);
        }

        public static IDataPackage CreateDataPackage(IDataReader dataReader, string code, int startDate, int endDate, int minKlineBefore, int minKlineAfter)
        {
            return new DataPackage(dataReader, code, startDate, endDate, minKlineBefore, minKlineAfter);
        }

        public static IDataPackage CreateDataPackage(IDataReader dataReader, string code, int openDate, int beforeDays, int afterDays)
        {
            return CreateDataPackage(dataReader, code, openDate, beforeDays, afterDays, 500, 100);
        }

        public static IDataPackage CreateDataPackage(IDataReader dataReader, string code, int openDate, int beforeDays, int afterDays, int minKlineBefore, int minKlineAfter)
        {
            ITradingDayReader tradingDayReader = dataReader.TradingDayReader;

            int index = dataReader.TradingDayReader.GetTradingDayIndex(openDate);
            int startIndex = index - beforeDays;
            startIndex = startIndex < 0 ? 0 : startIndex;
            int startDate = tradingDayReader.GetTradingDay(startIndex);

            int endIndex = index + afterDays;
            if (endIndex >= tradingDayReader.GetAllTradingDays().Count)
                endIndex = tradingDayReader.GetAllTradingDays().Count - 1;
            int endDate = tradingDayReader.GetTradingDay(endIndex);
            return CreateDataPackage(dataReader, code, startDate, endDate, minKlineBefore, minKlineAfter);
        }
    }
}