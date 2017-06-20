using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan
{
    [TestClass]
    public class TestDataProvider_JinShuYuan
    {
        [TestMethod]
        public void TestLoadJinShuYuan()
        {
            //string srcDataPath = @"E:\FUTURES\CSV\TICK_JSY\";
            //string pluginPath = @"D:\SCWORK\DEV\SC2\bin\Debug\plugin\cnfutures\";

            DataProvider_JinShuYuan dataProvider = new DataProvider_JinShuYuan(DataUpdateConst.SRCDATAPATH_JINSHUYUAN, DataUpdateConst.PLUGINPATH);

            List<CodeInfo> codes = dataProvider.GetNewCodes();
            //AssertUtils.PrintLineList(codes);
            AssertUtils.AssertEqual_List("codes", GetType(), codes);

            List<int> tradingDays = dataProvider.GetNewTradingDays();
            AssertUtils.AssertEqual_List("tradingdays", GetType(), tradingDays);

            ITickData tickData = dataProvider.LoadTickData("a1607", 20160503);
            AssertUtils.AssertEqual_TickData("TickData_A1607_20160503", GetType(), tickData);

            tickData = dataProvider.LoadTickData("CF1607", 20160503);
            AssertUtils.AssertEqual_TickData("TickData_CF1607_20160503", GetType(), tickData);
            //AssertUtils.PrintTickData(tickData);
        }

        //[TestMethod]
        public void TestGetCodeInfo_JinShuYuan()
        {
            string srcDataPath = DataUpdateConst.SRCDATAPATH_JINSHUYUAN;
            string pluginPath = DataUpdateConst.PLUGINPATH;
            DataProvider_JinShuYuan_CodeInfo provider = new DataProvider_JinShuYuan_CodeInfo(srcDataPath, pluginPath);
            List<CodeInfo> codes = provider.GenerateCodes();
            codes.Sort(new CodeInfoComparer());
            AssertUtils.PrintLineList(codes);
        }
    }
}