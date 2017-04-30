using com.wer.sc.data.reader.cache;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    [TestClass]
    public class TestDataTransfer_KLine2KLine
    {
        [TestMethod]
        public void TestTransferKLine_Minute()
        {
            string code = "m05";
            int start = 20131216;
            int end = 20131231;
            List<double[]> tradingTime = MockDataLoader.GetTradingTime(code, start);
            IKLineData data_1min = MockDataLoader.GetKLineData(code, start, end, KLinePeriod.KLinePeriod_1Minute);
            TradingSessionCache_Instrument cache = new TradingSessionCache_Instrument(code, MockDataLoader.GetTradingSessions(code));

            //转换成5分钟线
            IKLineData data = DataTransfer_KLine2KLine.Transfer(data_1min, KLinePeriod.KLinePeriod_5Minute, cache);
            AssertUtils.AssertEqual_KLineData("Kline2Kline_M05_20131216_20131231_5Minute", GetType(), data);

            //转换成15分钟
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLineTimeType.MINUTE, 15), cache);
            AssertUtils.AssertEqual_KLineData("Kline2Kline_M05_20131216_20131231_15Minute", GetType(), data);

            //转换成1小时
            data = DataTransfer_KLine2KLine.Transfer(data_1min, new KLinePeriod(KLineTimeType.HOUR, 1), cache);
            AssertUtils.AssertEqual_KLineData("Kline2Kline_M05_20131216_20131231_1Hour", GetType(), data);
        }

        [TestMethod]
        public void TestTransferKLine_Day()
        {
            string code = "m05";
            IKLineData data_1min = MockDataLoader.GetKLineData(code, 20131216, 20131231, new KLinePeriod(KLineTimeType.MINUTE, 1));
            TradingSessionCache_Instrument cache = new TradingSessionCache_Instrument(code, MockDataLoader.GetTradingSessions(code));
            IKLineData data = DataTransfer_KLine2KLine.Transfer_Day(data_1min, new KLinePeriod(KLineTimeType.DAY, 1), cache);
            AssertUtils.AssertEqual_KLineData("Kline2kline_M05_20131216_20131231_Day", GetType(), data);
        }

        [TestMethod]
        public void TestTransferKLine_DayOverNight()
        {
            string code = "m05";
            IKLineData klineData = MockDataLoader.GetKLineData(code, 20141215, 20150116, KLinePeriod.KLinePeriod_1Minute);
            TradingSessionCache_Instrument cache = new TradingSessionCache_Instrument(code, MockDataLoader.GetTradingSessions(code));
            IKLineData data = DataTransfer_KLine2KLine.Transfer_Day(klineData, new KLinePeriod(KLineTimeType.DAY, 1), cache);
            AssertUtils.AssertEqual_KLineData("Kline2Kline_M05_20141215_20150116_Day", GetType(), data);
        }
    }
}
