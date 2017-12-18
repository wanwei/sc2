using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils
{
    [TestClass]
    public class TestDynamicArray
    {
        [TestMethod]
        public void TestSetValue()
        {
            DynamicArray<double> arr = new DynamicArray<double>();
            arr[3] = 5;
            arr[10] = 20;
            arr[2] = 12;

            Assert.AreEqual(11, arr.Length);            
            Assert.AreEqual(5, arr[3]);
            Assert.AreEqual(20, arr[10]);
            Assert.AreEqual(12, arr[2]);
            Assert.AreEqual(0, arr[0]);
        }
    }
}