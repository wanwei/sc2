using com.wer.sc.strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward.impl
{
    [TestClass]
    public class TestHistoryDataForward_Code_Play
    {
        [TestMethod]
        public void TestHistoryDataForward_Play()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170601;

            HistoryDataForward_Code realTimeReader = CommonData.GetHistoryDataForward_Code(code, start, endDate, true);
            realTimeReader.OnTick += RealTimeReader_OnTick;
            realTimeReader.NavigateTo(20170531.210011);
            realTimeReader.Play();
            while (realTimeReader.Time < 20170531.210015)
            {

            }
            realTimeReader.Pause();
        }

        private void RealTimeReader_OnTick(object sender, ITickData tickData, int index)
        {
            Console.WriteLine(tickData);
        }    
    }
}
