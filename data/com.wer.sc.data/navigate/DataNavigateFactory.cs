using com.wer.sc.data.datapackage;
using com.wer.sc.data.navigate.impl;
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
    public class DataNavigateFactory
    {
        public static IDataNavigate_Code CreateDataNavigate(IDataPackage dataPackage, double time)
        {
            return new DataNavigate_Code(dataPackage, time);
        }

        public static IDataNavigate_Code CreateDataNavigate(IDataReader dataReader, string code, double time)
        {
            int openDate = dataReader.CreateTradingSessionReader(code).GetRecentTradingDay(time);
            IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, openDate, 100, 50);
            return CreateDataNavigate(dataPackage, time);
        }

        public static IDataNavigate_Code CreateDataNavigate(String dataCenterUri, string code, double time)
        {
            IDataReader dataReader = DataReaderFactory.CreateDataReader(dataCenterUri);
            return new DataNavigate_Code(dataReader, code, time);
        }
    }
}
