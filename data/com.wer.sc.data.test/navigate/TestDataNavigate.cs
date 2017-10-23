﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    [TestClass]
    public class TestDataNavigate
    {
        [TestMethod]
        public void TestNavigate()
        {
            string code = "rb1705";
            double time = 20170405.093001;
            IDataNavigate dataNavigate = DataCenter.Default.DataNavigateFactory.CreateDataNavigate(code, time);

            Assert.AreEqual("20170405.093001,3470,3470,3470,3470,272,943840,731414", dataNavigate.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString());

            time = 20170405.093058;
            bool canNav = dataNavigate.NavigateTo(time);
            Assert.IsTrue(canNav);            
            Assert.AreEqual("20170405.093058,3470,3470,3463,3463,1062,3681108,731424", dataNavigate.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString());

            time = 20170405.093059;
            canNav = dataNavigate.NavigateTo(time);
            Assert.IsTrue(canNav);
            Assert.AreEqual("20170405.093,3470,3470,3463,3463,1062,0,731424", dataNavigate.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString());

            Console.WriteLine(dataNavigate.GetKLineData(KLinePeriod.KLinePeriod_1Minute));
        }
    }
}
