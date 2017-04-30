using com.wer.sc.mockdata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.data
{
    [TestClass]
    public class TestTickData_RealTime
    {
        [TestMethod]
        public void TestTickData_Receive()
        {
            string code = "m05";
            int date = 20100111;
            TickData_RealTime tickData_RealTime = new TickData_RealTime(code, 20100111, 500);

            ITickData tickData = MockDataLoader.GetTickData(code, date);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                tickData_RealTime.Recieve(tickData);
                Console.WriteLine(tickData_RealTime);
                Assert.AreEqual(tickData.Price, tickData_RealTime.Price);
            }
        }
    }
}
