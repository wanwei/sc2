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
    public class TestKLineToKLineIndeier
    {
        private List<string> list_OnBar = new List<string>();

        [TestMethod]
        public void TestKLineToKLine()
        {
            list_OnBar.Clear();
            string code = "RB1710";
            int start = 20170601;
            int end = 20170603;

            AssertKLineToKLine(code, start, end, list_OnBar);
            AssertUtils.AssertEqual_List("forward_kline", GetType(), list_OnBar);
        }

        private void AssertKLineToKLine(string code, int start, int end, List<string> list_OnBar)
        {
            IDataPackage_Code datapackage = DataCenter.Default.DataPackageFactory.CreateDataPackage_Code(code, start, end);
            IKLineData_Extend mainKLine = datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Minute);
            IList<IKLineData_Extend> indexKLines = new List<IKLineData_Extend>();
            indexKLines.Add(datapackage.GetKLineData(KLinePeriod.KLinePeriod_5Minute));
            indexKLines.Add(datapackage.GetKLineData(KLinePeriod.KLinePeriod_15Minute));
            indexKLines.Add(datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Hour));
            indexKLines.Add(datapackage.GetKLineData(KLinePeriod.KLinePeriod_1Day));
            KLineToKLineIndeier indeier = new KLineToKLineIndeier(mainKLine, indexKLines);

            int startMainPos = mainKLine.BarPos;
            int endMainPos = mainKLine.Length - 1;
            for (int i = startMainPos; i <= endMainPos; i++)
            {
                Console.WriteLine(mainKLine.Period + ":" + mainKLine.GetBar(i));
                list_OnBar.Add(mainKLine.Period + ":" + mainKLine.GetBar(i));
                PrintKLines(i, indeier, indexKLines, list_OnBar);
            }
        }

        private void PrintKLines(int mainPos, KLineToKLineIndeier indeier, IList<IKLineData_Extend> indexKLines, List<string> list_OnBar)
        {
            for (int i = 0; i < indexKLines.Count; i++)
            {
                IKLineData klineData = indexKLines[i];
                int pos = indeier.GetOtherKLineBarPosIfFinished(mainPos, klineData.Period);
                if (pos >= 0) { 
                    list_OnBar.Add(klineData.Period + ":" + klineData.GetBar(pos));
                    Console.WriteLine(klineData.Period + ":" + klineData.GetBar(pos));
                }
            }
        }
    }
}
