using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.forward
{
    [TestClass]
    public class TestHistoryDataForward_Code_AttachData
    {
        [TestMethod]
        public void TestTickAttachTick()
        {
            string code = "RB1710";
            int start = 20170601;
            int endDate = 20170603;
            //string code = "A0401";
            //int start = 20040106;
            //int endDate = 20040106;

            IDataForward_Code historyDataForward = CommonData.GetHistoryDataForward_Code(code, start, endDate, true);
            historyDataForward.OnBar += HistoryDataForward_OnBar;
            historyDataForward.OnTick += HistoryDataForward_OnTick;
            //historyDataForward.OnTick += KlineDataForward_OnTick;
            while (historyDataForward.Forward())
            {

            }
        }

        private void HistoryDataForward_OnTick(object sender, ForwardOnTickArgument argument)
        {
            
        }

        private void HistoryDataForward_OnBar(object sender, ForwardOnBarArgument arguments)
        {
            
        }
    }
}