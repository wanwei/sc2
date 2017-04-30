using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.navigate
{
    /// <summary>
    /// 夜盘测试，各种情况
    /// 1.周末的夜盘 20141226、20141229 
    /// 例：20141226235959、20141227000000、20141229090000
    /// 2.平常的夜盘 20141229、20141230
    /// 例：20141229235959、20141230000000、20141230090000
    /// 3.没啥交易的夜盘 M05_20150512、M05_20150504
    /// 4.假期取消夜盘 M05_20150930、M05_20151008
    /// </summary>
    [TestClass]
    public class TestDataNavigate_Night
    {
        //[TestMethod]
        //public void Test()
        //{

        //}
    }
}
