using com.wer.sc.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace com.wer.sc.mockdata
{
    /// <summary>
    /// 该类用于
    /// </summary>
    public class AssertUtils
    {
        /// <summary>
        /// 检验K线数据正确性
        /// </summary>
        /// <param name="expectedResult">期望的结果</param>
        /// <param name="klineData">K线数据</param>
        public static void AssertEqual_KLineData(String expectedResult, IKLineData klineData)
        {
            string[] periodArr = expectedResult.Split('\r');
            Assert.AreEqual(periodArr.Length, klineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                klineData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, klineData.ToString());
            }
        }

        /// <summary>
        /// 检验K线数据正确性
        /// </summary>
        /// <param name="excepedFileName">testcase的文件名</param>
        /// <param name="type">testcase测试的类型</param>
        /// <param name="klineData"></param>
        public static void AssertEqual_KLineData(String excepedFileName, Type type, IKLineData klineData)
        {
            string expcetedResult = TestCaseManager.LoadTestCaseFile(type.Namespace, excepedFileName);
            AssertEqual_KLineData(expcetedResult, klineData);
        }

        public static void AssertEqual_KLineData(IKLineData klineData, IKLineData newklineData)
        {
            Assert.AreEqual(klineData.Length, newklineData.Length);
            for (int i = 0; i < klineData.Length; i++)
            {
                Assert.AreEqual(klineData.GetBar(i).ToString(), newklineData.GetBar(i).ToString());
            }
        }

        public static void AssertEqual_TimeLineData(string expectedResult, ITimeLineData timeLineData)
        {
            string[] periodArr = expectedResult.Split('\r');
            Assert.AreEqual(periodArr.Length, timeLineData.Length);
            for (int i = 0; i < timeLineData.Length; i++)
            {
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, timeLineData.GetBar(i).ToString());
            }
        }

        public static void AssertEqual_TimeLineData(string expectedFileName, Type type, ITimeLineData timeLineData)
        {
            string expcetedResult = TestCaseManager.LoadTestCaseFile(type.Namespace, expectedFileName);
            AssertEqual_TimeLineData(expcetedResult, timeLineData);
        }

        /// <summary>
        /// 检验tick数据的正确性
        /// </summary>
        /// <param name="expectedResult"></param>
        /// <param name="tickData"></param>
        public static void AssertEqual_TickData(string expectedResult, ITickData tickData)
        {
            string[] periodArr = expectedResult.Split('\r');
            Assert.AreEqual(periodArr.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                tickData.BarPos = i;
                string periodStr = periodArr[i].Trim();
                Assert.AreEqual(periodStr, tickData.ToString());
            }
        }

        /// <summary>
        /// 检验tick数据的正确性
        /// </summary>
        /// <param name="excepedFileName"></param>
        /// <param name="type"></param>
        /// <param name="tickData"></param>
        public static void AssertEqual_TickData(String excepedFileName, Type type, ITickData tickData)
        {
            string expcetedResult = TestCaseManager.LoadTestCaseFile(type.Namespace, excepedFileName);
            AssertEqual_TickData(expcetedResult, tickData);
        }

        /// <summary>
        /// 检验tick数据的正确性
        /// </summary>
        /// <param name="tickData"></param>
        /// <param name="newTickData"></param>
        public static void AssertEqual_TickData(ITickData tickData, ITickData newTickData)
        {
            Assert.AreEqual(newTickData.Length, tickData.Length);
            for (int i = 0; i < tickData.Length; i++)
            {
                Assert.AreEqual(newTickData.GetBar(i).ToString(), tickData.GetBar(i).ToString());
            }
        }

        public static void AssertEqual_List<T>(IList<T> expectedResult, IList<T> actualList)
        {
            Assert.AreEqual(expectedResult.Count, actualList.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], actualList[i]);
            }
        }

        public static void AssertEqual_List_ToString<T>(IList<T> expectedResult, IList<T> actualList)
        {
            Assert.AreEqual(expectedResult.Count, actualList.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].ToString(), actualList[i].ToString());
            }
        }

        /// <summary>
        /// 检验List的正确性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expectedResult"></param>
        /// <param name="actualList"></param>
        public static void AssertEqual_List<T>(string expectedResult, IList<T> actualList)
        {
            string[] periodArr = expectedResult.Split('\r');
            Assert.AreEqual(periodArr.Length, actualList.Count);

            for (int i = 0; i < actualList.Count; i++)
            {
                T t = actualList[i];
                if (t is IList)
                {

                }
                else
                    Assert.AreEqual(periodArr[i].Trim(), actualList[i].ToString());
            }
        }

        private static string GetListString(IList list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                Object obj = list[i];
                sb.Append(obj);
                if (i != list.Count - 1)
                    sb.Append(",");
            }
            return sb.ToString();
        }

        public static void AssertEqual_List<T>(String excepedFileName, Type type, IList<T> actualList)
        {
            string expcetedResult = TestCaseManager.LoadTestCaseFile(type.Namespace, excepedFileName);
            AssertEqual_List(expcetedResult, actualList);
        }

        public static void PrintTickData(ITickData tickData)
        {
            for (int i = 0; i < tickData.Length; i++)
            {
                Console.WriteLine(tickData.GetBar(i));
            }
        }

        public static void PrintKLineData(IKLineData klineData)
        {
            for (int i = 0; i < klineData.Length; i++)
            {
                Console.WriteLine(klineData.GetBar(i));
            }
        }

        public static void PrintTimeLineData(ITimeLineData timeLineData)
        {
            for (int i = 0; i < timeLineData.Length; i++)
            {
                Console.WriteLine(timeLineData.GetBar(i));
            }
        }

        public static void PrintLineList(IList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Object t = list[i];
                if (t is IList)
                {
                    PrintList((IList)t);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(t);
                }
            }
        }

        public static void PrintList(IList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Object obj = list[i];
                Console.Write(obj);
                if (i != list.Count - 1)
                    Console.Write(",");
            }
        }
    }
}
