using com.wer.sc.data.reader.realtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.reader.realtime
{
    [TestClass]
    public class TestRealTimeDataNavigater_Tick
    {
        [TestMethod]
        public void TestForwardTick()
        {
            string code = "m05";
            int startDate = 20140101;
            int endDate = 20160101;
            //int endDate = 20100130;
            DataReaderFactory dataReaderFactory = ResourceLoader.GetDefaultDataReaderFactory();
            Dictionary<KLinePeriod, KLineData_RealTime> dicKLineData = new Dictionary<KLinePeriod, KLineData_RealTime>();

            dicKLineData.Add(KLinePeriod.KLinePeriod_1Minute, GetKLineData(dataReaderFactory, code, startDate, endDate, KLinePeriod.KLinePeriod_1Minute));
            dicKLineData.Add(KLinePeriod.KLinePeriod_5Minute, GetKLineData(dataReaderFactory, code, startDate, endDate, KLinePeriod.KLinePeriod_5Minute));
            dicKLineData.Add(KLinePeriod.KLinePeriod_15Minute, GetKLineData(dataReaderFactory, code, startDate, endDate, KLinePeriod.KLinePeriod_15Minute));
            dicKLineData.Add(KLinePeriod.KLinePeriod_1Day, GetKLineData(dataReaderFactory, code, startDate, endDate, KLinePeriod.KLinePeriod_1Day));
            RealTimeDataNavigateForward_Tick navigater = new RealTimeDataNavigateForward_Tick(dataReaderFactory, code, startDate, endDate, dicKLineData);

            //Console.WriteLine(navigater.DicKLineGetValue(KLinePeriod.KLinePeriod_1Minute]);
            //for (int i = 0; i < 50000; i++)
            //{
            int tickCount = 0;
            while (navigater.NavigateForward(1))
            {
                //Console.WriteLine(navigater.DicKLineGetValue(KLinePeriod.KLinePeriod_1Minute]);
                //Console.WriteLine(navigater.DicKLineGetValue(KLinePeriod.KLinePeriod_1Day]);
                tickCount++;
            }
            Console.WriteLine(tickCount);
        }

        private KLineData_RealTime GetKLineData(DataReaderFactory fac, string code, int startDate, int endDate, KLinePeriod period)
        {
            IKLineData klineData = fac.KLineDataReader.GetData(code, startDate, endDate, period);
            return new KLineData_RealTime(klineData);
        }
    }
}
