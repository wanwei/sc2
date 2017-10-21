using com.wer.sc.data.reader;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 数据包创建工厂
    /// 该工厂
    /// </summary>
    public class DataPackageFactory : IDataPackageFactory
    {
        /// <summary>
        /// 创建单支股票或期货在一段时间内的数据包
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static IDataPackage_Code CreateDataPackage(IDataReader dataReader, string code, int startDate, int endDate)
        {
            return CreateDataPackage(dataReader, code, startDate, endDate, 500, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="minKlineBefore"></param>
        /// <param name="minKlineAfter"></param>
        /// <returns></returns>
        internal static IDataPackage_Code CreateDataPackage(IDataReader dataReader, string code, int startDate, int endDate, int minKlineBefore, int minKlineAfter)
        {
            return new DataPackage_Code(dataReader, code, startDate, endDate, minKlineBefore, minKlineAfter);
        }

        internal static IDataPackage_Code CreateDataPackage(IDataReader dataReader, string code, int openDate, int beforeDays, int afterDays)
        {
            return CreateDataPackage(dataReader, code, openDate, beforeDays, afterDays, 500, 0);
        }

        internal static IDataPackage_Code CreateDataPackage(IDataReader dataReader, string code, int openDate, int beforeDays, int afterDays, int minKlineBefore, int minKlineAfter)
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

        private IDataReader dataReader;

        public DataPackageFactory(IDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public IDataPackage_Code CreateDataPackage_Code(string code, int startDate, int endDate)
        {
            return CreateDataPackage(dataReader, code, startDate, endDate);
        }

        public IDataPackage_Code CreateDataPackage_Code(string code, int startDate, int endDate, int minKlineBefore, int minKlineAfter)
        {
            return CreateDataPackage(dataReader, code, startDate, endDate, minKlineBefore, minKlineAfter);
        }

        public IDataPackage_Code CreateDataPackage_Code(string code, int openDate, int beforeDays, int afterDays)
        {
            return CreateDataPackage(dataReader, code, openDate, beforeDays, afterDays);
        }

        public IDataPackage_Code CreateDataPackage_Code(string code, int openDate, int beforeDays, int afterDays, int minKlineBefore, int minKlineAfter)
        {
            return CreateDataPackage(dataReader, code, openDate, beforeDays, afterDays, minKlineBefore, minKlineAfter);
        }

        public IDataPackage CreateDataPackage(string[] codes, int startDate, int endDate)
        {
            throw new NotImplementedException();
        }

        public IDataPackage CreateDataPackage(string[] codes, int startDate, int endDate, int minKlineBefore, int minKlineAfter)
        {
            throw new NotImplementedException();
        }
    }
}