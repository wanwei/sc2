using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据导航接口
    /// </summary>
    public interface IDataNavigate
    {
        /// <summary>
        /// 导航到time位置
        /// </summary>
        /// <param name="time"></param>
        void NavigateTo(double time);

        /// <summary>
        /// 向前导航，前进一定的时间
        /// 比如，前进1小时，前进15分钟....
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        void NavigateForward_Time(KLinePeriod period, int len);

        /// <summary>
        /// 向前导航，前进到下一个周期截至
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        void NavigateForward_Period(KLinePeriod period, int len);

        /// <summary>
        /// 向前导航，前进tick条数据
        /// </summary>
        /// <param name="len"></param>
        void NavigateForward_Tick(int len);

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double Time { get; }

        /// <summary>
        /// 得到指定周期的K线
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        IKLineData GetKLineData(KLinePeriod period);

        /// <summary>
        /// 得到当前的分时线
        /// </summary>
        /// <returns></returns>
        ITimeLineData GetRealData();

        /// <summary>
        /// 得到今日的TICK数据
        /// </summary>
        /// <returns></returns>
        ITickData GetTickData();

        /// <summary>
        /// 
        /// </summary>
        event DataChangeEventHandler OnDataChangeHandler;
    }

    public delegate void DelegateOnNavigateTo(Object sender, DataNavigateEventArgs e);

    public class DataNavigateEventArgs : System.EventArgs
    {
        public const int CHANGETYPE_TO = 0;

        public const int CHANGETYPE_FORWARDTIME = 1;

        public const int CHANGETYPE_FORWARDPERIOD = 2;

        public const int CHANGETYPE_FORWARDTICK = 3;

        private string time;

        private string prevTime;

        private int changeType;

        private int forwardLength;

        private KLinePeriod forwardPeriod;

        public string Time
        {
            get
            {
                return time;
            }
        }

        public string PrevTime
        {
            get
            {
                return prevTime;
            }
        }

        public int ChangeType
        {
            get
            {
                return changeType;
            }
        }

        public int ForwardLength
        {
            get
            {
                return forwardLength;
            }
        }

        public KLinePeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        public DataNavigateEventArgs()
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (changeType)
            {
                case CHANGETYPE_TO:

                    break;
                case CHANGETYPE_FORWARDTIME:
                    sb.Append("时间前进").Append(forwardLength).Append(forwardPeriod);
                    break;
                case CHANGETYPE_FORWARDPERIOD:
                    break;
                case CHANGETYPE_FORWARDTICK:
                    break;
            }
            //if (IsForwardChange)
            //{
            //    if (forwardLength >= 0)
            //        sb.Append("时间前进");
            //    else
            //        sb.Append("时间后退");
            //    sb.Append(forwardLength).Append(forwardPeriod);
            //}
            //else
            //{

            //}
            return sb.ToString();
        }
    }
}
