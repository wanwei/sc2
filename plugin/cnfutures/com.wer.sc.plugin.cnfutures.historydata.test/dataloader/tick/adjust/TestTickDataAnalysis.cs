using com.wer.sc.data;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.biaopuyonghua.adjust;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataloader.tick.adjust
{
    [TestClass]
    public class TestTickDataAnalysis
    {
        [TestMethod]
        public void TestAnalysis()
        {
            //DataLoaderFactory.CreateDataLoader(DataSourceType.TaoBao1, "", "");
            //AssertAnalysisTickData("m05", 20040106);
        }

        private void AssertAnalysisTickData(string code, int date)
        {
            TickData tickData = (TickData)MockDataLoader.GetTickData(code, date);
            List<double[]> openTime = MockDataLoader.GetTradingTime(code, date);
            List<TickInfo_Period> periods = TickDataAnalysis.Analysis(tickData, openTime);
            AssertUtils.PrintLineList(periods);
        }
    }
}
