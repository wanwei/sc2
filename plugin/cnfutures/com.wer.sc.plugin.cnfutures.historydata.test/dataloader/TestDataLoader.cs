using com.wer.sc.data;
using com.wer.sc.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.updater
{
    [TestClass]
    public class TestDataLoader
    {
        [TestMethod]
        public void TestLoadInstrumentInfo()
        {
            Console.WriteLine(ScConfig.Instance.ScPath);
            //Console.WriteLine(GeneratorConfig.ConfigPath);            
            //Console.WriteLine(PathUtils.InstrumentPath);
            //DataLoader dl = new DataLoader("", "", "");
            //List<InstrumentInfo> instruments = dl.DataLoader_Instrument.GetAllInstruments();
            //for (int i = 0; i < instruments.Count; i++)
            //    Console.WriteLine(instruments);
        }
    }
}
