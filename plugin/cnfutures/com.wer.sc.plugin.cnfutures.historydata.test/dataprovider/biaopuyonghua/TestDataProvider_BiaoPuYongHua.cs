using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua
{
    [TestClass]
    public class TestDataProvider_BiaoPuYongHua
    {
        [TestMethod]
        public void TestBiaoPuYongHua()
        {            
            IDataProvider dataProvider = new DataProvider_BiaoPuYongHua(DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA, DataUpdateConst.PLUGINPATH);
            List<int> tradingDays = dataProvider.GetNewTradingDays();
            AssertUtils.AssertEqual_List("tradingdays", GetType(), tradingDays);
            //AssertUtils.PrintLineList(tradingDays);

            List<CodeInfo> codes = dataProvider.GetNewCodes();
            AssertUtils.PrintLineList(codes);
            AssertUtils.AssertEqual_List("codes", GetType(), codes);

            ITickData tickData = dataProvider.LoadTickData("A1005", 20100105);
            //AssertUtils.PrintTickData(tickData);
            AssertUtils.AssertEqual_TickData("TickData_A1005_20100105", GetType(), tickData);
        }

        //[TestMethod]
        public void TestDataProvider_BiaoPuYongHua_Code()
        {
            string srcDataPath = DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA;
            string pluginPath = DataUpdateConst.PLUGINPATH;
            DataProvider_BiaoPuYongHua_CodeInfo provider = new DataProvider_BiaoPuYongHua_CodeInfo(srcDataPath, pluginPath);
            List<CodeInfo> codes = provider.GenerateCodes();
            codes.Sort(new CodeInfoComparer());
            AssertUtils.PrintLineList(codes);
        }
    }
}