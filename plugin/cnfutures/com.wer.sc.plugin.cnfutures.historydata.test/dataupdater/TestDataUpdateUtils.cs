using com.wer.sc.data;
using com.wer.sc.mockdata;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider;
using com.wer.sc.plugin.cnfutures.historydata.dataprovider.jinshuyuan;
using com.wer.sc.plugin.historydata;
using com.wer.sc.plugin.historydata.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater
{
    [TestClass]
    public class TestDataUpdateUtils
    {
        [TestMethod]
        public void Test_DataUpdateUtils()
        {
            //string path = Plugin_HistoryData_CnFutures.DATAPATH;
            //DataUpdateHelper dataUpdateHelper = GetDataUpdateHelper();
            //IUpdatedDataInfo updatedDataInfo = new UpdatedInfo_Csv(path);
            //DataUpdateUtils2 dataUpdateUtils = new DataUpdateUtils2(path, dataUpdateHelper.GetAllTradingDayReader(), updatedDataInfo, dataUpdateHelper);

            //List<InstrumentDatesInfo> instruments = dataUpdateUtils.GetTickNewData(true);
            //AssertUtils.PrintLineList(instruments);
        }

        private DataUpdateHelper GetDataUpdateHelper()
        {
            string pluginPath = DataUpdateConst.PLUGINPATH;
            //string srcDataPath = DataUpdateConst.SRCDATAPATH_BIAOPUYONGHUA;
            string srcDataPath = DataUpdateConst.SRCDATAPATH_JINSHUYUAN;
            UpdatedDataLoader updatedDataLoader = new UpdatedDataLoader();
            IDataProvider dataProvider = new DataProvider_JinShuYuan(srcDataPath, pluginPath);
            return new DataUpdateHelper(pluginPath, updatedDataLoader, dataProvider);
        }
    }
}
