using com.wer.sc.data;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [TestClass]
    public class TestDataUpdateHelper
    {
        [TestMethod]
        public void Test_DataUpdateHelper()
        {
            DataUpdateHelper dataUpdateHelper = GetDataUpdateHelper_JinShuYuan();

            List<int> tradingDays = dataUpdateHelper.GetAllTradingDayReader().GetAllTradingDays();
            AssertUtils.PrintLineList(tradingDays);

            List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            AssertUtils.PrintLineList(codes);
        }

        public static DataUpdateHelper GetDataUpdateHelper_BiaoPuYongHua()
        {
            string pluginPath = DataUpdateConst.PLUGINPATH;
            string srcDataPath = DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA;            
            UpdatedDataLoader updatedDataLoader = new UpdatedDataLoader(pluginPath);
            IDataProvider dataProvider = new DataProvider_BiaoPuYongHua(srcDataPath, pluginPath);
            DataUpdateHelper dataUpdateHelper = new DataUpdateHelper(pluginPath, updatedDataLoader, dataProvider);
            return dataUpdateHelper;
        }

        private static DataUpdateHelper GetDataUpdateHelper_JinShuYuan()
        {
            string pluginPath = DataUpdateConst.PLUGINPATH;
            //string srcDataPath = DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA;
            string srcDataPath = DataUpdateConst.SRCDATAPATH_JINSHUYUAN;
            UpdatedDataLoader updatedDataLoader = new UpdatedDataLoader(pluginPath);
            IDataProvider dataProvider = new DataProvider_JinShuYuan(srcDataPath, pluginPath);
            DataUpdateHelper dataUpdateHelper = new DataUpdateHelper(pluginPath, updatedDataLoader, dataProvider);
            return dataUpdateHelper;
        }

        [TestMethod]
        public void TestGetNotUpdateTradingDays()
        {
            //DataUpdateHelper dataUpdateHelper = GetDataUpdateHelper(DataUpdateConst.SRCDATAPATH_JINSHUYUAN);
            //List<int> tradingDays = dataUpdateHelper.GetNotUpdateTradingDays_TickData("au1611", true);
            ////AssertUtils.PrintLineList(tradingDays);
            //tradingDays = dataUpdateHelper.GetNotUpdateTradingDays_KLineData("au1611", true);
            //AssertUtils.PrintLineList(tradingDays);

            DataUpdateHelper dataUpdateHelper = GetDataUpdateHelper_BiaoPuYongHua();
            List<int> tradingDays = dataUpdateHelper.GetNotUpdateTradingDays_TickData("au1611", true);
            AssertUtils.PrintLineList(tradingDays);
            //tradingDays = dataUpdateHelper.GetNotUpdateTradingDays_KLineData("au1611", true);
            //AssertUtils.PrintLineList(tradingDays);
            //List<int> tradingDays = dataUpdateHelper.GetAllTradingDayReader().GetAllTradingDays();
            //AssertUtils.PrintLineList(tradingDays);

            //List<CodeInfo> codes = dataUpdateHelper.GetNewCodes();
            //AssertUtils.PrintLineList(codes);
        }
    }
}
