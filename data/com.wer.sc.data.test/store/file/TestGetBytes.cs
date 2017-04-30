using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test
{
    [TestClass]
    public class TestGetBytes
    {
        [TestMethod]
        public void TestBitConverter()
        {
            double d = 20100105.093000;
            byte[] bs = BitConverter.GetBytes(d);
            Console.WriteLine(bs.Length);
            double dd = BitConverter.ToDouble(bs, 0);
            Console.WriteLine(dd);

            float f = 200.35f;
            bs = BitConverter.GetBytes(f);
            Console.WriteLine(bs.Length);
            float ff = (float)BitConverter.ToSingle(bs, 0);
            Console.WriteLine(ff);
        }
    }
}
