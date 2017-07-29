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
    public class DataNavigateFactory
    {
        public static IDataNavigate_Code CreateDataNavigate(IDataPackage dataPackage, double time)
        {
            return new DataNavigate_Code(dataPackage, time);
        }

        public static IDataNavigate_Code CreateDataNavigate(IDataReader dataReader, string code, double time, int startDate, int endDate)
        {
            IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(dataReader, code, startDate, endDate);
            return CreateDataNavigate(dataPackage, time);
            //return new DataNavigate_Code(dataReader, code, time, startDate, endDate);
        }

        public static IDataNavigate_Code CreateDataNavigate(IDataReader dataReader, string code, double time)
        {
            //return new DataNavigate_Code(dataReader, code, time);
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
