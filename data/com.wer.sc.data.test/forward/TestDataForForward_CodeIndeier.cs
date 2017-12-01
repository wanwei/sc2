using com.wer.sc.data.datapackage;
using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestDataForForward_CodeIndeier
    {
        [TestMethod]
        public void TestForwardCodeIndeier()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;

            IDataPackage_Code dataPackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, endDate);
            ForwardReferedPeriods referedPeriods = new ForwardReferedPeriods();
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_5Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            referedPeriods.UsedKLinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            referedPeriods.UseTickData = true;
            referedPeriods.UseTimeLineData = true;

            printLine.Clear();
            DataForForward_Code data1 = new DataForForward_Code(dataPackage, referedPeriods);
            DataForForward_CodeIndeier indeier = new DataForForward_CodeIndeier(data1);

            int len = data1.GetMainKLineData().Length;
            for (int i = 0; i < len; i++)
            {
                IKLineBar bar = data1.GetMainKLineData().GetBar(i);
                Print(bar, indeier.GetFinishedBarsRelativeToMainKLine(i), data1);
            }

            AssertUtils.AssertEqual_List("forwardcodeindeier", GetType(), printLine);
        }
        private List<string> printLine = new List<string>();

        private void Print(IKLineBar bar, IList<KLineBarPos> posList, DataForForward_Code data1)
        {
            if (posList == null)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append(bar.Time + ":");
            //Console.Write(bar.Time + ":");
            for (int i = 0; i < posList.Count; i++)
            {
                KLineBarPos periodBar = posList[i];
                //Console.Write(periodBar + "," + data1.GetKLineData(periodBar.KLinePeriod).GetBar(periodBar.BarPos).Time + "|");
                sb.Append(periodBar + "," + data1.GetKLineData(periodBar.KLinePeriod).GetBar(periodBar.BarPos).Time + "|");
                //Assert.AreEqual(bar.Time, data1.GetKLineData(posList[i].KLinePeriod).Time);
            }
            //Console.WriteLine();
            printLine.Add(sb.ToString());
        }
    }
}
