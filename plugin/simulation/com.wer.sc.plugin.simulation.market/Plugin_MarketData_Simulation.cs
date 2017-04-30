using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin.market;
using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.data.market;
using com.wer.sc.data.reader;

namespace com.wer.sc.plugin.market.history
{
    /// <summary>
    /// 基于历史数据的仿真市场
    /// 提供以下几种仿真：
    /// 1.类似真实市场，每隔一段时间推送数据
    /// 2.
    /// </summary>
    public class Plugin_MarketData_Simulation : Plugin_XApi_Base, IPlugin_MarketData
    {
        private const string DATAPATH = "DataPath";
        private const string INTERVAL = "Interval";
        private const string TIME = "Time";
        private const string ENDTIME = "EndTime";
        private const string TIMEFORWARD = "TimeForward";

        private List<String> subscribedCodes = new List<string>();

        private System.Timers.Timer marketDataTimer;

        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                this.onConnectionStatus = value;
            }
        }

        private DelegateOnReturnMarketData onReturnMarketData;

        public DelegateOnReturnMarketData OnReturnMarketData
        {
            get
            {
                return this.onReturnMarketData;
            }

            set
            {
                this.onReturnMarketData = value;
            }
        }

        public Plugin_MarketData_Simulation()
        {
            //一秒钟返回两次数据
            this.marketDataTimer = new System.Timers.Timer();
            this.marketDataTimer.Elapsed += MarketDataTimer_Elapsed;
            this.marketDataTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            this.marketDataTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
        }

        private void MarketDataTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        public List<double[]> GetTradingSession(string code, int date)
        {
            return null;
        }

        /// <summary>
        /// 历史数据的ConnectionInfo参数
        /// 1.DataPath 使用的数据中心的地址
        /// 2.StartTime 使用的开始日期
        /// 3.频率，
        /// </summary>
        /// <param name="connectionInfo"></param>
        public void Connect(ConnectionInfo connectionInfo)
        {
            //数据中心地址：这个地址是
            string dataPath = connectionInfo.GetValue(DATAPATH);
            //每次返回的时间间隔，如果设定为-1，则不用定时器，直接按序返回。默认为-1            
            int interval = connectionInfo.ContainsKey(INTERVAL) ? int.Parse(connectionInfo.GetValue(INTERVAL)) : -1;
            //设定当前时间，如传入20150105.093000，则从该时间开始
            double time = double.Parse(connectionInfo.GetValue(TIME));
            //结束时间，如传入20150106.150000,则到该时间截止，默认不设置则到最新的历史时间
            double endTime = connectionInfo.ContainsKey(ENDTIME) ? double.Parse(connectionInfo.GetValue(ENDTIME)) : -1;
            //每次向前进的时间，单位是毫秒，默认是500
            int timeForward = connectionInfo.ContainsKey(TIMEFORWARD) ? int.Parse(connectionInfo.GetValue(TIMEFORWARD)) : 500;

            int currentDate = (int)time;
            IDataReader fac = DataReaderFactory.CreateDataReader(dataPath);
            LoopDate(fac, currentDate, time, Math.Round(((double)timeForward) / 100000000, 9));

            //for (int i = 0; i < subscribedCodes.Count; i++)
            //{
            //    string code = subscribedCodes[i];
            //    TickData tickData = fac.TickDataReader.GetTickData(code, currentDate);
            //}

            //if (interval < 0)
            //{
            //    //fac.OpenTimeReader.GetOpenDate(code, time);
            //    //fac.OpenTimeReader.GetOpenDate()
            //}
            //else
            //{

            //}
            //IList<int> dates = fac.OpenDateReader.GetOpenDates(startDate, endDate);
            //    for (int i = 0; i < dates.Count; i++)
            //    {
            //        int date = dates[i];
            //        Dictionary<string, ITickData> dic = PrepareTickDataDictionary(fac, date);
            //        //fac.OpenTimeReader.GetOpenTime()
            //        //M05_20141229
            //        //夜盘开始时间是20141229
            //        double startTime;
            //        if (date < 20141229)
            //        {
            //            startTime = date + 0.09000;
            //        }
            //        else
            //        {
            //            if (i > 0)
            //                startTime = dates[i - 1] + 0.210000;
            //            else
            //                startTime = fac.OpenDateReader.GetPrevOpenDate(date) + 0.210000;
            //        }
            //        DoReturnMarketData(dic, Math.Round(((double)frequency) / 1000000, 8), startTime);
            //    }
        }

        private void LoopDate(IDataReader fac, int date, double time, double timeForward)
        {
            Dictionary<string, ITickData> dicTickData = PrepareTickDataDictionary(fac, date);
            Dictionary<string, int> dicTickIndex = PrepareTickIndexDictionary(dicTickData, time);

            if (time < 0)
                time = GetFirstStartTime(dicTickData);
            HashSet<string> finishedCodes = new HashSet<string>();
            while (finishedCodes.Count != dicTickData.Count)
            {
                LoopTime(dicTickData, dicTickIndex, finishedCodes, time);
                time += timeForward;
            }
        }

        private void LoopTime(Dictionary<string, ITickData> dicTickData, Dictionary<string, int> dicTickIndex, HashSet<string> finishedCodes, double time)
        {
            for (int i = 0; i < subscribedCodes.Count; i++)
            {
                string code = subscribedCodes[i];
                if (finishedCodes.Contains(code))
                    continue;
                ITickData tickData = dicTickData[code];
                int currentIndex = dicTickIndex[code];
                if (currentIndex >= tickData.Length)
                {
                    finishedCodes.Add(code);
                }
                double currentTime = tickData.Arr_Time[currentIndex];

                if (time >= currentTime)
                {
                    if (onReturnMarketData != null)
                    {
                        ITickBar bar = tickData.GetBar(currentIndex);
                        onReturnMarketData(this, ref bar);
                    }
                    dicTickIndex.Remove(code);
                    dicTickIndex.Add(code, currentIndex + 1);
                }
            }
        }

        private double GetFirstStartTime(Dictionary<string, ITickData> dicTickData)
        {
            double startTime = Double.MaxValue;
            foreach (ITickData tickData in dicTickData.Values)
            {
                double currentTickTime = tickData.Arr_Time[0];
                if (startTime > currentTickTime)
                {
                    startTime = currentTickTime;
                }
            }
            return startTime;
        }

        private Dictionary<string, ITickData> PrepareTickDataDictionary(IDataReader fac, int date)
        {
            Dictionary<string, ITickData> dicTickData = new Dictionary<string, ITickData>();
            for (int i = 0; i < subscribedCodes.Count; i++)
            {
                string code = subscribedCodes[i];
                ITickData tickData = fac.TickDataReader.GetTickData(code, date);
                dicTickData.Add(code, tickData);
            }
            return dicTickData;
        }

        private Dictionary<string, int> PrepareTickIndexDictionary(Dictionary<string, ITickData> dicTickData, double time)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (string code in dicTickData.Keys)
            {
                ITickData tickData = dicTickData[code];
                int index;
                if (time < 0)
                    index = TimeIndeierUtils.IndexOfTime_Tick(tickData, time);
                else
                    index = 0;
                dic.Add(code, index);
            }
            return dic;
        }

        private void DoReturnMarketData(Dictionary<String, ITickData> dic, double timeForward, double startTime)
        {
            if (onReturnMarketData == null)
                return;
            HashSet<string> endCodes = new HashSet<string>();

            for (int i = 0; i < subscribedCodes.Count; i++)
            {
                string code = subscribedCodes[i];
                ITickData tickData = dic[code];
                DoReturnMarketData_First(tickData);
                if (tickData.BarPos == tickData.Length - 1)
                {
                    // Console.WriteLine(code + "结束了");
                    endCodes.Add(code);
                }
            }
            bool isFinished = endCodes.Count == dic.Count;
            while (!isFinished)
            {
                // Console.WriteLine(subscribedCodes.Count);
                for (int i = 0; i < subscribedCodes.Count; i++)
                {
                    string code = subscribedCodes[i];
                    if (endCodes.Contains(code))
                        continue;
                    ITickData tickData = dic[code];
                    DoReturnMarketData(tickData, startTime);
                    if (tickData.BarPos == tickData.Length - 1)
                    {
                        endCodes.Add(code);
                        Console.WriteLine(code + "结束了");
                    }
                }
                isFinished = endCodes.Count == dic.Count;
                startTime += timeForward;
            }
        }

        private object lockObj = new object();
        private void DoReturnMarketData_First(ITickData tickData)
        {
            ITickBar tickBar = tickData.GetBar(0);
            onReturnMarketData(this, ref tickBar);
        }

        private void DoReturnMarketData(ITickData tickData, double time)
        {
            int nextBarPos = tickData.BarPos + 1;
            if (nextBarPos >= tickData.Length)
                return;
            double nextTickTime = tickData.Arr_Time[nextBarPos];
            if (time >= nextTickTime)
            {
                tickData.BarPos = nextBarPos;
                ITickBar tickBar = tickData.GetBar(nextBarPos);
                onReturnMarketData(this, ref tickBar);
            }
        }


        public void DisConnect()
        {

        }

        public void Subscribe(string[] codes)
        {
            if (codes == null)
                return;
            foreach (string code in codes)
            {
                subscribedCodes.Add(code);
            }
        }

        public void UnSubscribe(string[] codes)
        {
            if (codes == null)
                return;
            foreach (string code in codes)
            {
                subscribedCodes.Remove(code);
            }
        }
    }
}