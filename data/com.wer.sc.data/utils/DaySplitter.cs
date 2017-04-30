using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class DaySplitter
    {
        public static List<SplitterResult> Split(TimeGetter timeGetter, ITradingDayReader openDateReader)
        {
            return Split(timeGetter);
        }

        /// <summary>
        /// 返回一个整型数组的list
        /// 整型数组第一项是日期，第二项是index
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <returns></returns>
        public static List<SplitterResult> Split(TimeGetter timeGetter)
        {
            double lastTime = timeGetter.GetTime(0);
            double time = timeGetter.GetTime(1);

            //算法                
            List<SplitterResult> indeies = new List<SplitterResult>(500);
            int len = timeGetter.Count;
            int currentIndex = 0;
            bool hasNight = IsNight(timeGetter.GetTime(0));
            bool overNight = false;
            for (int index = 1; index < len; index++)
            {
                time = timeGetter.GetTime(index);

                int date = (int)time;
                int lastDate = (int)lastTime;

                /*
                 * 四种情况：
                 * 1.无夜盘，直接生成即可：如20100105090000、...
                 * 2.有夜盘，但夜盘不过夜：如20160329210000、...、20160329233000、...、20160330090000
                 * 3.有夜盘，要过夜，不是周末：如20150324210000、...、20150325023000、...、20150325090000
                 * 4.有夜盘，要过夜，而且是周末：如20141226210000、...、20141227023000、...、20141229090000
                 */

                /*
                 * 规则：
                 * 1.夜盘开始，一定是新的一天开始
                 * 2.
                 */

                //夜盘开始，则一定是新的一天开始
                if (IsNightStart(time, lastTime))
                {
                    indeies.Add(new SplitterResult((int)lastTime, currentIndex));
                    overNight = false;
                    currentIndex = index;
                    hasNight = true;
                }
                else if (hasNight)
                {
                    if (date != lastDate)
                        overNight = true;
                    //对于夜盘来说，如果到了第二天，而且时间大于6点，则说明夜盘结束了
                    if (overNight && (time - (int)time) > 0.06)
                        hasNight = false;
                }
                //只要过了夜都算第二天的
                else if (date != lastDate)
                {
                    indeies.Add(new SplitterResult((int)lastTime, currentIndex));
                    currentIndex = index;
                    overNight = false;
                }

                lastTime = time;
            }

            //如果最后一个时间是晚上
            int endDate = (int)lastTime;
            if (IsNight(lastTime))
                endDate += 1;

            indeies.Add(new SplitterResult(endDate, currentIndex));
            return indeies;
        }

        public static List<SplitterResult> Split(IKLineData klineData, ITradingDayReader openDateReader)
        {
            return Split(klineData);
        }

        public static List<SplitterResult> Split(IKLineData klineData)
        {
            return DaySplitter.Split(new KLineDataTimeGetter(klineData));
        }

        public static bool IsNight(double time)
        {
            double t1 = time - (int)time;
            return t1 >= 0.18;
        }

        public static bool IsNightStart(double time, double lastTime)
        {
            //time在晚上6点之后，lasttime在晚上6点之前
            //且前后时间相隔超过100分钟，说明time是夜盘开始
            if (!IsNight(time))
                return false;

            if (IsNight(lastTime))
                return false;

            TimeSpan span = TimeUtils.Substract(time, lastTime);
            if (span.Hours * 60 + span.Minutes > 100)
            {
                return true;
            }

            return false;
        }

        public static int GetTimeDate(double time, ITradingDayReader openDateReader)
        {
            int date = (int)time;
            double t = time - date;
            if (t < 0.18)
                return date;
            return openDateReader.GetNextTradingDay(date);
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
}