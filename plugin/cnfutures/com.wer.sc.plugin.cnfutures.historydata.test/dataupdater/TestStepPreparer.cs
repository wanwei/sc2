using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua;
using com.wer.sc.utils.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [TestClass]
    public class TestStepPreparer
    {
        [TestMethod]
        public void TestPrepare()
        {
            string pluginPath = DataUpdateConst.PLUGINPATH;
            string srcDataPath = DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA;
            string targetDataPath = DataUpdateConst.DATACENTERSOURCEPATH;
            dataprovider.IDataProvider dataProvider = new DataProvider_BiaoPuYongHua(srcDataPath, pluginPath);
            StepPreparer stepPreparer = new StepPreparer(pluginPath, srcDataPath, targetDataPath, false, dataProvider);
            List<IStep> steps = stepPreparer.GetAllSteps();
            AssertUtils.PrintLineList(steps);
        }
    }
}
