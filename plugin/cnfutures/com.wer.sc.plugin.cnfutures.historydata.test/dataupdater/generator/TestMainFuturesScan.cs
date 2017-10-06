using com.wer.sc.data;
using com.wer.sc.data.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.historydata.dataupdater.generator
{
    [TestClass]
    public class TestMainFuturesScan
    {
        [TestMethod]
        public void TestScan()
        {
            string path = @"E:\FUTURES\CSV\DATACENTERSOURCE\";
            string codePath = path + "instruments.csv";                        
            List<CodeInfo> codes = CsvUtils_Code.Load(codePath);
            string datePath = path + "tradingdays.csv";
            MainFuturesScan scan = new MainFuturesScan(path, codes);
            List<int> openDates = CsvUtils_TradingDay.Load(datePath);
            MainFutures mainFutures = scan.Scan("B", openDates);
            Console.WriteLine(mainFutures);
        }

        [TestMethod]
        public void TestScan2()
        {
            string path = @"E:\FUTURES\CSV\DATACENTERSOURCE\";
            string codePath = path + "instruments.csv";
            List<CodeInfo> codes = CsvUtils_Code.Load(codePath);
            string datePath = path + "tradingdays.csv";
            MainFuturesScan scan = new MainFuturesScan(path, codes);
            List<int> openDates = new List<int>();
            openDates.Add(20170927);
            openDates.Add(20170928);
            openDates.Add(20170929);
            MainFutures mainFutures = scan.Scan("ER", openDates);
            Console.WriteLine(mainFutures);
        }
    }
}
