using com.wer.sc.data.datapackage;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 数据导航器工厂类
    /// 该类用于创造数据导航器，数据导航器可以导航定位到一支股票或期货的任意时间，查看当时的K线分时线或tick数据
    /// </summary>
    public class DataNavigateFactory : IDataNavigateFactory
    {
        public IDataNavigate_Code CreateDataNavigate_Code(IDataPackage_Code dataPackage, double time)
        {
            return new DataNavigate_Code(dataPackage, time);
        }

        //internal static IDataNavigate_Code CreateDataNavigate(IDataReader dataReader, string code, double time)
        //{
        //    int openDate = dataReader.CreateTradingTimeReader(code).GetRecentTradingDay(time);
        //    if (openDate < 0)
        //        return null;
        //    IDataPackage_Code dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, openDate, 100, 50);
        //    return CreateDataNavigate(dataPackage, time);
        //}

        //internal static IDataNavigate_Code CreateDataNavigate(String dataCenterUri, string code, double time)
        //{
        //    IDataReader dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
        //    return new DataNavigate_Code2(dataReader, code, time);
        //}

        private IDataCenter dataCenter;

        private IDataReader dataReader;

        internal DataNavigateFactory(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
            this.dataReader = dataCenter.DataReader;
        }

        public IDataNavigate CreateDataNavigate(string code, double time, int beforeDays, int afterDays)
        {
            IDataNavigate_Code dataNav_Code = CreateDataNavigate_Code(code, time, beforeDays, afterDays);
            DataNavigate nav = new DataNavigate(this.dataCenter, code, time, beforeDays, afterDays);
            return nav;
        }

        public IDataNavigate CreateDataNavigate(string code, double time)
        {
            return CreateDataNavigate(code, time, 100, 50);
        }

        public IDataNavigate_Code CreateDataNavigate_Code(string code, double time)
        {
            int openDate = this.dataReader.CreateTradingTimeReader(code).GetRecentTradingDay(time);
            if (openDate < 0)
                return null;
            IDataPackage_Code dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, openDate, 100, 50);
            return CreateDataNavigate_Code(dataPackage, time);
        }

        public IDataNavigate_Code CreateDataNavigate_Code(string code, double time, int beforeDays, int afterDays)
        {
            int openDate = this.dataReader.CreateTradingTimeReader(code).GetRecentTradingDay(time);
            IDataPackage_Code dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, openDate, beforeDays, afterDays);
            return CreateDataNavigate_Code(dataPackage, time);
        }
    }
}
