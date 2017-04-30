using com.wer.sc.data.reader.cache;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.impl
{
    public class TradingSessionReader_Instrument : ITradingSessionReader_Instrument
    {
        private string dataPath;

        private Dictionary<string, OpenDateCalculator> dic = new Dictionary<string, OpenDateCalculator>();

        public TradingSessionReader_Instrument(string dataPath)
        {
            this.dataPath = dataPath;
        }

        public TradingSession GetOpenTime(string code, int date)
        {
            OpenDateCalculator dayStartTime = GetOrCreateDayStartTime(code);
            if (dayStartTime == null)
                return null;
            return dayStartTime.GetOpenTime(date);
        }

        private string GetDayOpenTimePath(string code)
        {
            return dataPath + "\\" + code + "\\" + code + "_dayopentime";
        }

        public int GetOpenDate(string code, double time)
        {
            OpenDateCalculator dayStartTime = GetOrCreateDayStartTime(code);
            if (dayStartTime == null)
                return (int)time;
            return dayStartTime.GetOpenDate(time);
        }

        private OpenDateCalculator GetOrCreateDayStartTime(string code)
        {
            OpenDateCalculator dayStartTime;
            if (!dic.ContainsKey(code))
            {
                dayStartTime = OpenDateCalculator.CreateDayStartTime(GetDayOpenTimePath(code));
                if (dayStartTime == null)
                    return null;
                dic.Add(code, dayStartTime);
            }
            else
                dayStartTime = dic[code];
            return dayStartTime;
        }

        public int GetRecentOpenDate(string code, double time)
        {
            OpenDateCalculator dayStartTime = GetOrCreateDayStartTime(code);
            if (dayStartTime == null)
                return (int)time;
            return dayStartTime.GetRecentOpenDate(time);
        }

        public string GetInstrument()
        {
            throw new NotImplementedException();
        }

        public TradingSession GetTradingSession(int date)
        {
            throw new NotImplementedException();
        }

        public int GetTradingDay(double time)
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time)
        {
            throw new NotImplementedException();
        }

        public int GetRecentTradingDay(double time, bool forward)
        {
            throw new NotImplementedException();
        }

        public bool IsStartTime(double time)
        {
            throw new NotImplementedException();
        }
    }

    class OpenDateCalculator
    {
        private List<int> openDates = new List<int>();
        private Dictionary<int, double[]> dicOpenTime = new Dictionary<int, double[]>();
        private TradingDayCache cache;

        private OpenDateCalculator(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] arr = line.Split(',');
                int date = int.Parse(arr[0]);
                double starttime = double.Parse(arr[1]);
                double endtime = double.Parse(arr[2]);
                openDates.Add(date);
                dicOpenTime.Add(date, new double[] { starttime, endtime });
            }
            cache = new TradingDayCache(openDates);
        }

        public static OpenDateCalculator CreateDayStartTime(string path)
        {
            if (!File.Exists(path))
                return null;
            return new OpenDateCalculator(path);
        }

        public TradingSession GetOpenTime(int date)
        {
            double[] openTime;
            dicOpenTime.TryGetValue(date, out openTime);
            if (openTime == null)
                return null;
            return new TradingSession(date, openTime[0], openTime[1]);
        }

        public int GetOpenDate(double time)
        {
            /*
             * 如果time对应的日子是开盘日
             * 如果时间早于当日开盘时间，那么返回之前的一天
             * 如果时间晚于开盘时间，如果早于第二天开盘时间             
             */
            int date = (int)time;
            if (dicOpenTime.ContainsKey(date))
            {
                return GetOpenDateTimeIsInOpenDate(time, date);
            }

            int nextOpenDate = cache.GetNextTradingDay(date);
            if (nextOpenDate < 0)
                return -1;
            if (IsInOpenTime(nextOpenDate, time))
                return nextOpenDate;

            int prevOpenDate = cache.GetPrevTradingDay(date);
            if (prevOpenDate < 0)
                return -1;
            if (IsInOpenTime(prevOpenDate, time))
                return prevOpenDate;

            return -1;
        }

        private int GetOpenDateTimeIsInOpenDate(double time, int date)
        {
            if (IsInOpenTime(date, time))
                return date;

            double[] startEnd = dicOpenTime[date];

            double start = startEnd[0];
            if (time < start)
            {
                int prevOpenDate = cache.GetPrevTradingDay(date);
                if (IsInOpenTime(prevOpenDate, time))
                    return prevOpenDate;
                return -1;
            }

            double end = startEnd[1];
            if (time > end)
            {
                int nextOpenDate = cache.GetNextTradingDay(date);
                if (IsInOpenTime(nextOpenDate, time))
                    return nextOpenDate;
                return -1;
            }

            return -1;
        }

        private bool IsInOpenTime(int date, double time)
        {
            double[] startEnd = dicOpenTime[date];
            double start = startEnd[0];
            double end = startEnd[1];
            if (time >= start && time <= end)
                return true;
            return false;
        }

        public int GetRecentOpenDate(double time)
        {
            int date = GetOpenDate(time);
            if (date >= 0)
                return date;
            date = (int)time;
            if (dicOpenTime.ContainsKey(date))
            {
                TradingSession dayOpenTime = GetOpenTime(date);
                if (time < dayOpenTime.StartTime)
                    return date;
                return cache.GetNextTradingDay((int)time);
            }
            if (date >= cache.LastTradingDay)
                return -1;
            return cache.GetNextTradingDay((int)time);
        }
    }
}