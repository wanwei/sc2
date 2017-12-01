using com.wer.sc.data.datapackage;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.utils
{
    [TestClass]
    public class TestForwardDataIndeier
    {
        [TestMethod]
        public void TestIndeier()
        {
            List<string> list_OnBar = new List<string>();
            string code = "RB1710";
            int start = 20170601;
            int end = 20170603;

            AssertForwardData(code, start, end, list_OnBar);
            AssertUtils.AssertEqual_List("ForwardDataIndeier", GetType(), list_OnBar);
        }

        [TestMethod]
        public void TestIndeier2()
        {
            List<string> list_OnBar = new List<string>();
            //string code = "RB1710";
            //int start = 20170601;
            //int end = 20170603;
            string code = "RB1012";
            int start = 20091225;
            int end = 20091225;

            AssertForwardData(code, start, end, list_OnBar);
            AssertUtils.AssertEqual_List("ForwardDataIndeier2", GetType(), list_OnBar);
        }

        private void AssertForwardData(string code, int start, int end, List<string> list_OnBar)
        {
            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end, 0, 0);
            List<KLinePeriod> usedKLinePeriods = new List<KLinePeriod>();
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            usedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods(usedKLinePeriods, true, true);

            DataForForward_Code dataForForward = new DataForForward_Code(dataPackage, referedPeriods);
            List<int> tradingDays = dataForForward.TradingDayReader.GetAllTradingDays();
            dataForForward.TradingDay = tradingDays[0];
            ForwardDataIndeier indeier = new ForwardDataIndeier(dataForForward);
            IKLineData_Extend klineData = dataForForward.MainKLine;

            int currentBarPos = klineData.BarPos;
            for (int i = 0; i < tradingDays.Count; i++)
            {
                int tradingDay = tradingDays[i];
                dataForForward.TradingDay = tradingDay;
                currentBarPos = Forward(dataForForward, indeier, tradingDay, currentBarPos, list_OnBar);
            }
        }

        private int Forward(DataForForward_Code dataForForward, ForwardDataIndeier indeier, int tradingDay, int klineIndex, List<string> list_OnBar)
        {
            IKLineData_Extend klineData = dataForForward.GetMainKLineData();
            ITickData_Extend tickData = dataForForward.CurrentTickData;
            int lastMainKLineBarPos;
            int mainBarPos = 0;
            KLinePeriod mainPeriod = dataForForward.MainKLinePeriod;
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                Console.WriteLine("tick:" + tickData);
                list_OnBar.Add("tick:" + tickData);
                mainBarPos = indeier.GetMainKLineBarPosIfFinished(i, out lastMainKLineBarPos);
                if (mainBarPos < 0)
                    continue;
                PrintData(dataForForward, indeier, klineData, mainBarPos, mainPeriod, list_OnBar);
                if (lastMainKLineBarPos >= 0 && lastMainKLineBarPos != mainBarPos)
                {
                    for (int m = mainBarPos + 1; m <= lastMainKLineBarPos; m++)
                    {
                        PrintData(dataForForward, indeier, klineData, m, mainPeriod, list_OnBar);
                    }
                }
            }
            return mainBarPos;
        }

        private static void PrintData(DataForForward_Code dataForForward, ForwardDataIndeier indeier, IKLineData_Extend klineData, int mainBarPos, KLinePeriod mainPeriod, List<string> list_OnBar)
        {
            Console.WriteLine(klineData.Period + ":" + klineData.GetBar(mainBarPos));
            list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(mainBarPos));
            for (int m = 0; m < dataForForward.ReferedKLinePeriods.Count; m++)
            {
                KLinePeriod period = dataForForward.ReferedKLinePeriods[m];
                if (mainPeriod.Equals(period))
                    continue;
                int barPos = indeier.GetOtherKLineBarPosIfFinished(mainBarPos, period);
                if (barPos >= 0)
                {
                    IKLineData otherKLine = dataForForward.GetKLineData(period);
                    Console.WriteLine(period + ":" + otherKLine.GetBar(barPos));
                    list_OnBar.Add(period + ":" + otherKLine.GetBar(barPos));
                }
            }
            Console.WriteLine("分时线:" + dataForForward.CurrentTimeLineData.GetBar(mainBarPos));
            list_OnBar.Add("分时线:" + dataForForward.CurrentTimeLineData.GetBar(mainBarPos));
        }
    }
}
