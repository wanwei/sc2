using com.wer.sc.data;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater.generator
{
    [TestClass]
    public class TestDataGenerator_TickData_Index
    {
        [TestMethod]
        public void TestGenerator_TickData_Index()
        {
            DataUpdateHelper dataUpdateHelper = TestDataUpdateHelper.GetDataUpdateHelper_BiaoPuYongHua();
            DataGenerator_TickData_Index generator = new DataGenerator_TickData_Index(dataUpdateHelper);
            ITickData tickdata = generator.Generate("m", 20100106);
            AssertUtils.AssertEqual_TickData("TickData_M_20100106_Index", GetType(), tickdata);
            //AssertUtils.PrintTickData(tickdata);

            tickdata = generator.Generate("m", 20150106);
            //AssertUtils.PrintTickData(tickdata);
            AssertUtils.AssertEqual_TickData("TickData_M_20150106_Index", GetType(), tickdata);
        }
    }
}
