using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.wer.sc.mockdata;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader
{
    [TestClass]
    public class TestDataLoader_Taobao1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string srcDataPath = @"E:\FUTURES\CSV\TICK";
            string csvDataPath = @"E:\FUTURES\CSV\TICKADJUSTED";
            string dataCenterUri = @"file:D:\SCDATA\CNFUTURES\";
            IDataLoader dataLoader = DataLoaderFactory.CreateDataLoader(DataSourceType.TaoBao1, srcDataPath, csvDataPath, dataCenterUri);
            //AssertUtils.PrintLineList(dataLoader.LoadAllInstruments());
            //AssertUtils.PrintLineList(dataLoader.LoadTradingDayReader().GetAllOpenDates());
            //AssertUtils.PrintLineList(dataLoader.LoadTradingSessions("m05"));

            AssertUtils.PrintTickData(dataLoader.LoadTickData("m05", 20100104));
        }
    }
}
