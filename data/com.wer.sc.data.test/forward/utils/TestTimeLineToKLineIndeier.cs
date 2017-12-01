using com.wer.sc.data.datapackage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    [TestClass]
    public class TestTimeLineToKLineIndeier
    {
        [TestMethod]
        public void TestTimeLineToKLine()
        {
            string code = "RB1710";
            int start = 20170601;
            int end = 20170605;

            IDataPackage_Code datapackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end, 0, 0);
            IKLineData_Extend klineData = datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            //IKLineData_Extend klineData = datapackage.GetKLineData(KLinePeriod.KLinePeriod_5Minute);
            IList<int> tradingDays = datapackage.GetTradingDays();
            ITimeLineData timeLineData = datapackage.GetTimeLineData(tradingDays[0]);
            TimeLineToKLineIndeier indeier = new TimeLineToKLineIndeier(klineData, timeLineData);

            int tradingDayIndex = 0;
            bool isFirst = true;
            for (int i = klineData.BarPos; i < klineData.Length; i++)
            {
                if (!isFirst && klineData.IsDayStart(i))
                {
                    tradingDayIndex++;
                    if (tradingDayIndex >= tradingDays.Count)
                        return;
                    timeLineData = datapackage.GetTimeLineData(tradingDays[tradingDayIndex]);
                    indeier.ChangeTradingDay(timeLineData);
                }
                isFirst = false;
                klineData.BarPos = i;
                Console.WriteLine(klineData.Period + ":" + klineData.GetBar(i));
                int barPos = indeier.GetTimeLineBarPosIfFinished(i);
                if (barPos >= 0) {
                    timeLineData.BarPos = barPos;
                    Console.WriteLine("分时线:" + timeLineData.GetBar(barPos));
                }
                Assert.AreEqual(klineData.Time, timeLineData.Time);
                Assert.AreEqual(klineData.End, timeLineData.Price);
            }
        }
    }
}
