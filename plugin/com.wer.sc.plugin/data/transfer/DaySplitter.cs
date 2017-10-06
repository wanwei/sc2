using com.wer.sc.data;
using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    public class DaySplitter
    {
        /// <summary>
        /// 返回一个整型数组的list
        /// 整型数组第一项是日期，第二项是index
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <returns></returns>
        public static List<SplitterResult> Split(TimeGetter timeGetter, ITradingTimeReader_Code tradingSessionReader)
        {
            List<SplitterResult> indeies = new List<SplitterResult>(500);
            double time = timeGetter.GetTime(0);
            int date = tradingSessionReader.GetTradingDay(time);
            indeies.Add(new SplitterResult(date, 0));
            for (int index = 1; index < timeGetter.Count; index++)
            {
                time = timeGetter.GetTime(index);
                if (tradingSessionReader.IsStartTime(time))
                {
                    date = tradingSessionReader.GetTradingDay(time);
                    indeies.Add(new SplitterResult(date, index));
                }
            }
            return indeies;
        }

        public static List<SplitterResult> Split(IKLineData data, ITradingTimeReader_Code tradingSessionReader)
        {
            return Split(new KLineDataTimeGetter(data), tradingSessionReader);
        }
    }

    public struct SplitterResult
    {
        private int date;
        private int index;

        public SplitterResult(int date, int index)
        {
            this.date = date;
            this.index = index;
        }

        public int Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public override string ToString()
        {
            return date + "," + index;
        }
    }

    public interface TimeGetter
    {
        double GetTime(int index);

        int Count
        {
            get;
        }
    }

    public class KLineDataTimeGetter : TimeGetter
    {
        private IKLineData data;
        public KLineDataTimeGetter(IKLineData data)
        {
            this.data = data;
        }

        public int Count
        {
            get { return data.Length; }
        }

        public double GetTime(int index)
        {
            return data.Arr_Time[index];
        }
    }
}