using com.wer.sc.data.update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    [TestClass]
    public class TestKLineDataIndex
    {
        [TestMethod]
        public void TestKLineDataIndex_DoIndex_Normal()
        {
            KLineData data_ = ResourceLoader.GetKLineData_1Min();
            IKLineData data = data_.GetRange(0, 449);

            MockDataProvider provider = new MockDataProvider();
            provider.DataPathDir = "testindex";
            String targetPath = provider.GetDataPath() + "\\testindex.kline";

            KLineDataStore store = new KLineDataStore(targetPath);
            store.Save(data);

            KLineDataIndexer indexer = new KLineDataIndexer(targetPath);
            indexer.DoIndex();

            KLineDataIndexResult result = indexer.GetIndexResult();
            Assert.AreEqual(2, result.DateList.Count);
            Assert.AreEqual(20131202, result.DateList[0]);
            Assert.AreEqual(20131203, result.DateList[1]);

            data = data_.GetRange(450, data_.Length - 1);
            store.Append(data);
            indexer.DoIndex();
            result = indexer.GetIndexResult();
            Assert.AreEqual(10, result.DateList.Count);

            Directory.Delete(provider.GetDataPath(), true);
        }

        private string GetTestOutputPath()
        {
            return ResourceLoader.GetTestOutputPath("klinedataindex\\");
        }

        [TestMethod]
        public void TestKLineDataIndex_DoIndex_HasNight()
        {
            KLineData klineData = ResourceLoader.GetKLineData(Resources.KLineData_M05_20130101_20151231_1Minute);

            String targetPath = GetTestOutputPath() + "M05_20130101_20151231_1Minute.kline";
            KLineDataStore store = new KLineDataStore(targetPath);
            store.Save(klineData);

            KLineDataIndexer indexer = new KLineDataIndexer(targetPath);
            indexer.DoIndex();
            
            string indexPath = targetPath + ".index";
            string[] indexLines = File.ReadAllLines(indexPath);
            string[] lines = Resources.KLineData_M05_20130101_20151231_1Minute_Index.Split('\r');
            Assert.AreEqual(lines.Length, indexLines.Length);
            for(int i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i].Trim(), indexLines[i].Trim());
            }
            Directory.Delete(GetTestOutputPath(), true);
        }
    }
}