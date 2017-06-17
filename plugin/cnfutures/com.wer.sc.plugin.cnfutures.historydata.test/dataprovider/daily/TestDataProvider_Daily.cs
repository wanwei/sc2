using com.wer.sc.data;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.daily
{
    [TestClass]
    public class TestDataProvider_Daily
    {
        [TestMethod]
        public void TestLoadDaily()
        {
            //string srcDataPath = @"E:\FUTURES\CSV\TICK_DAILY\";
            //string pluginPath = @"D:\SCWORK\DEV\SC2\bin\Debug\plugin\cnfutures\";

            IDataProvider dataProvider = new DataProvider_Daily(DataUpdateConst.SRCDATAPATH_DAILY, DataUpdateConst.PLUGINPATH);
            List<int> tradingDays = dataProvider.GetNewTradingDays();
            tradingDays.RemoveRange(40, tradingDays.Count - 40);
            AssertUtils.AssertEqual_List("tradingdays", GetType(), tradingDays);

            List<CodeInfo> codes = dataProvider.GetNewCodes();
            codes.Sort(new CodeInfoComparer());
            //AssertUtils.PrintLineList(codes);
            //AssertUtils.AssertEqual_List("codes", GetType(), codes);

            ITickData tickData = dataProvider.LoadTickData("A1705", 20170405);
            //AssertUtils.PrintTickData(tickData);
            AssertUtils.AssertEqual_TickData("TickData_A1705_20170405", GetType(), tickData);

        }

        [TestMethod]
        public void TestDataProvider_Daily_CodeInfo()
        {
            string srcDataPath = @"E:\FUTURES\CSV\TICK_DAILY\";
            string pluginPath = DataUpdateConst.PLUGINPATH;
            DataProvider_Daily_CodeInfo provider = new DataProvider_Daily_CodeInfo(srcDataPath, pluginPath);
            List<CodeInfo> codes = provider.GenerateCodes();
            codes.Sort(new CodeInfoComparer());
            AssertUtils.PrintLineList(codes);
        }
    }
}
