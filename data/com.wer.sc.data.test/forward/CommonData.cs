using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using com.wer.sc.data.forward.impl;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using com.wer.sc.strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    public class CommonData
    {
        private static IDataReader dataReader;

        private static object lockObj = new object();

        public static IDataReader GetDataReader()
        {
            if (dataReader != null)
                return dataReader;
            lock (lockObj)
            {
                if (dataReader != null)
                    return dataReader;
                dataReader = DataReaderFactory.CreateDataReader("file:/E:/SCDATA/CNFUTURES/");
                return dataReader;
            }
        }

        public static IDataPackage GetDataPackage(string code, int startDate, int endDate)
        {
            return DataPackageFactory.CreateDataPackage(GetDataReader(), code, startDate, endDate);
        }

        public static KLineData_RealTime GetKLineData_RealTime(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            IKLineData klineData = GetDataReader().KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
            return new KLineData_RealTime(klineData);
        }

        public static HistoryDataForward_Code GetRealTimeReader(string code, int startDate, int endDate, bool useTickData)
        {
            StrategyReferedPeriods referedPeriods = new StrategyReferedPeriods();
            referedPeriods.isReferTimeLineData = false;
            referedPeriods.UseTickData = useTickData;
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);

            ForwardPeriod forwardPeriod = new ForwardPeriod(useTickData, KLinePeriod.KLinePeriod_1Minute);

            IDataPackage dataPackage = DataPackageFactory.CreateDataPackage(GetDataReader(), code, startDate, endDate);
            HistoryDataForward_Code realTimeReader = new HistoryDataForward_Code(dataPackage, referedPeriods, forwardPeriod);
            return realTimeReader;
        }
    }
}
