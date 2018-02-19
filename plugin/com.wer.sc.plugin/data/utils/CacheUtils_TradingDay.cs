using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class CacheUtils_TradingDay : ITradingDayReader
    {
        private List<int> openDatesList;

        private Dictionary<int, int> dicOpenDateIndex;

        public CacheUtils_TradingDay(IList<int> openDateList)
        {
            this.openDatesList = openDateList.ToList<int>();
            this.dicOpenDateIndex = new Dictionary<int, int>();
            for (int i = 0; i < openDatesList.Count; i++)
            {
                dicOpenDateIndex.Add(openDatesList[i], i);
            }
        }

        public bool IsTrade(int date)
        {
            return dicOpenDateIndex.ContainsKey(date);
        }

        public List<int> GetAllTradingDays()
        {
            return openDatesList;
        }

        public int FirstTradingDay
        {
            get
            {
                if (openDatesList.Count == 0)
                    return -1;
                return openDatesList[0];
            }
        }

        public int LastTradingDay
        {
            get
            {
                if (openDatesList.Count == 0)
                    return -1;
                return openDatesList[openDatesList.Count - 1];
            }
        }

        public IList<int> GetTradingDays(int startDate, int endDate)
        {
            if (startDate <= 0)
                startDate = openDatesList[0];
            if (endDate <= 0)
                endDate = openDatesList[openDatesList.Count - 1];
            if (endDate < startDate)
                return ListUtils.EmptyIntList;

            int startIndex = GetTradingDayIndex(startDate, false);
            int endIndex = GetTradingDayIndex(endDate, true);

            if (startIndex < 0)
                return ListUtils.EmptyIntList;

            int[] opendates = new int[endIndex - startIndex + 1];
            for (int i = startIndex; i <= endIndex; i++)
            {
                opendates[i - startIndex] = openDatesList[i];
            }
            return opendates;
        }

        /**
         * 获取两日间的所有开盘日
         * @param beginDate
         * @param endDate
         * @return
         */
        public int GetTradingDayCount(int beginDate, int endDate)
        {
            return GetTradingDayIndex(endDate, true) - GetTradingDayIndex(beginDate, false) + 1;
        }

        public int GetTradingDay(int index)
        {
            if (index < 0 || index >= openDatesList.Count)
                return -1;
            return openDatesList[index];
        }

        public int GetTradingDayIndex(int date)
        {
            if (dicOpenDateIndex.ContainsKey(date))
                return dicOpenDateIndex[date];
            return -1;
        }

        public int GetTradingDayIndex(int date, bool isFindPrev)
        {
            if (date < 0)
            {
                if (isFindPrev)
                    return openDatesList.Count - 1;
                return 0;
            }
            int index = GetTradingDayIndex(date);
            if (index >= 0)
                return index;
            date = GetRecentOpenDate(date, isFindPrev);
            return GetTradingDayIndex(date);
        }

        public int GetNextTradingDay(int date)
        {
            return GetNextTradingDay(date, 1);
        }

        public int GetNextTradingDay(int date, int length)
        {
            int nextOpenDate = GetRecentOpenDate(date, length < 0);
            if (nextOpenDate < 0)
                return -1;
            int index = GetTradingDayIndex(nextOpenDate);
            index += length;
            if (nextOpenDate != date)
                index += length < 0 ? 1 : -1;
            if (index < 0 || index >= openDatesList.Count)
                return -1;
            return openDatesList[index];
        }

        private int GetRecentOpenDate(int date, bool isFindPrev)
        {
            if (IsTrade(date))
                return date;
            int firstOpen = FirstTradingDay;
            if (date < firstOpen)
                return isFindPrev ? -1 : firstOpen;

            int lastOpen = LastTradingDay;
            if (date > lastOpen)
                return isFindPrev ? lastOpen : -1;

            int addPeriod = isFindPrev ? -1 : 1;
            while (!IsTrade(date))
                date = (int)TimeUtils.AddDays(date, addPeriod);
            return date;
        }

        public int GetPrevTradingDay(int date)
        {
            return GetPrevTradingDay(date, 1);
        }

        public int GetPrevTradingDay(int date, int length)
        {
            return GetNextTradingDay(date, -length);
        }
    }
}
