using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    /// <summary>
    /// 该类专门给测试用例提供数据
    /// </summary>
    public class MockDataLoader
    {
        private static Plugin_HistoryData_MockData mockData = new Plugin_HistoryData_MockData();

        public static List<CodeInfo> GetAllInstruments()
        {
            return mockData.GetInstruments();
        }

        public static List<int> GetAllTradingDays()
        {
            return mockData.GetTradingDays();
        }

        //public static List<TradingSession> GetTradingSessions(String code)
        //{
        //    return mockData.GetTradingSessions(code);
        //}

        public static IList<ITradingTime> GetTradingTimeList(String code)
        {
            return mockData.GetTradingTime(code);
        }


        /// <summary>
        /// 得到股票或期货的Tick数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static ITickData GetTickData(String code, int date)
        {
            return mockData.GetTickData(code, date);
        }

        /// <summary>
        /// 得到股票或期货的K线数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="klinePeriod"></param>
        /// <returns></returns>
        public static IKLineData GetKLineData(String code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            return mockData.GetKLineData(code, startDate, endDate, klinePeriod);
        }

        public static List<double[]> GetTradingTime(String code, int date)
        {
            if (date <= 20141226)
                return GetTradingTime_Normal();
            if (date == 20150105 || date == 20150504)
                return GetTradingTime_Normal();
            if (date <= 20150508)
                return GetTradingTime_NightEarly();
            return GetTradingTime_Night();
        }

        private static List<double[]> GetTradingTime_Normal()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        private static List<double[]> GetTradingTime_NightEarly()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .02300 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }

        private static List<double[]> GetTradingTime_Night()
        {
            List<double[]> openTime = new List<double[]>();
            openTime.Add(new double[] { .210000, .233000 });
            openTime.Add(new double[] { .090000, .101500 });
            openTime.Add(new double[] { .103000, .113000 });
            openTime.Add(new double[] { .133000, .150000 });
            return openTime;
        }
    }
}