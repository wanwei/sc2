using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.strategy.cnfutures
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

        public static KLineData_RealTime GetKLineData_RealTime(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            IKLineData klineData = GetDataReader().KLineDataReader.GetData(code, startDate, endDate, klinePeriod);
            return new KLineData_RealTime(klineData);
        }
    }
}
