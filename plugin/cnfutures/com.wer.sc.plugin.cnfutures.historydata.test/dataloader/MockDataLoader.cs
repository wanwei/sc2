using com.wer.sc.data;
using com.wer.sc.data.cnfutures.generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.updater
{
    public class MockDataLoader
    {
        public static string originalDataPath = @"E:\FUTURES\CSV\TICK\";

        public static string pluginDataPath = @"E:\FUTURES\CSV\TICKADJUSTED";

        private static DataLoader dataLoader = new DataLoader(originalDataPath, pluginDataPath, "");

        public static TickData LoadTickData(string code, int date)
        {
            return dataLoader.DataLoader_TickData.GetTickData(code, date);
        }

        public static List<double[]> LoadOpenTime(string code, int date)
        {
            return dataLoader.DataLoader_OpenTime.GetTradingSessionDetail(code, date);
        }

        public static DataLoader DataLoader
        {
            get { return dataLoader; }
        }
    }
}
