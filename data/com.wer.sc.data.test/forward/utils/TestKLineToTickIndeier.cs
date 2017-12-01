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
    public class TestKLineToTickIndeier
    {
        [TestMethod]
        public void TestKLineToTick()
        {
            string code = "RB1710";
            int start = 20170601;
            int end = 20170603;

            AssertText(code, start, end, "klinetotick");
        }

        [TestMethod]
        public void TestKLineToTick2()
        {
            //RB1012_20091225
            string code = "RB1012";
            int start = 20091225;
            int end = 20091225;
            AssertText(code, start, end, "klinetotick2");
        }

        private void AssertText(string code, int start, int end, string fileName)
        {
            List<string> list_OnBar = new List<string>();
            IDataPackage_Code datapackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end, 0, 0);
            IKLineData_Extend klineData = datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            ITickData tickData = datapackage.GetTickData(start);
            KLineToTickIndeier indeier = new KLineToTickIndeier(tickData, klineData);

            int lastBarPos;
            for (int i = 0; i < tickData.Length; i++)
            {
                list_OnBar.Add("tick:" + tickData.GetBar(i));
                Console.WriteLine("tick:" + tickData.GetBar(i));
                int pos = indeier.GetKLineBarPosIfFinished(i, out lastBarPos);
                if (pos >= 0)
                {
                    list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(pos));
                    Console.WriteLine(klineData.Period + ":" + klineData.GetBar(pos));
                    for (int m = pos + 1; m <= lastBarPos; m++)
                    {
                        list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(m));
                        Console.WriteLine(klineData.Period + ":" + klineData.GetBar(m));
                        //lastBarPos = 1;
                    }
                }
            }
            AssertUtils.AssertEqual_List(fileName, GetType(), list_OnBar);
        }

        [TestMethod]
        public void TestKLineToTick_Days()
        {
            string code = "RB1710";
            int start = 20170601;
            int end = 20170605;

            List<string> list_OnBar = new List<string>();
            IDataPackage_Code datapackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end, 0, 0);
            IKLineData_Extend klineData = datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Minute);

            IList<int> tradingDays = datapackage.GetTradingDays();
            ITickData tickData = datapackage.GetTickData(tradingDays[0]);
            KLineToTickIndeier indeier = new KLineToTickIndeier(tickData, klineData);
            DoIndex(list_OnBar, klineData, tickData, indeier);

            for (int i = 1; i < tradingDays.Count; i++)
            {
                tickData = datapackage.GetTickData(tradingDays[i]);
                indeier.ChangeTradingDay(tickData);
                DoIndex(list_OnBar, klineData, tickData, indeier);
            }            
        }

        private static void DoIndex(List<string> list_OnBar, IKLineData_Extend klineData, ITickData tickData, KLineToTickIndeier indeier)
        {
            int lastBarPos;
            for (int i = 0; i < tickData.Length; i++)
            {
                list_OnBar.Add("tick:" + tickData.GetBar(i));
                Console.WriteLine("tick:" + tickData.GetBar(i));
                int pos = indeier.GetKLineBarPosIfFinished(i, out lastBarPos);
                if (pos >= 0)
                {
                    list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(pos));
                    Console.WriteLine(klineData.Period + ":" + klineData.GetBar(pos));
                    for (int m = pos + 1; m <= lastBarPos; m++)
                    {
                        list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(m));
                        Console.WriteLine(klineData.Period + ":" + klineData.GetBar(m));
                    }
                }
            }
        }
    }
}