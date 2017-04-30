using com.wer.sc.data.receiver2.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.present.data
{
    [TestClass]
    public class TestKLineDataMerge
    {
        public void TestMerge()
        {
            DataReaderFactory dataReaderFactory = ResourceLoader.GetDefaultDataReaderFactory();
            string code = "m05";
            int date1 = 20160108;
            int date2 = 20160111;
            KLinePeriod klinePerid = KLinePeriod.KLinePeriod_1Minute;
            IKLineData klineData1 = dataReaderFactory.KLineDataReader.GetData(code, date1, date1, klinePerid);
            IKLineData klineData2 = dataReaderFactory.KLineDataReader.GetData(code, date2, date2, klinePerid);

            KLineData_Merge klineData_Merge = new KLineData_Merge(klineData1, klineData2);
            for(int i = 0; i < klineData_Merge.Length; i++)
            {
                //klineData_Merge.BarPos = i;
                //Console.WriteLine(klineData_Merge)
            }
        }
    }
}
