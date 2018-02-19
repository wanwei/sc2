using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 实时数据导航
    /// 该接口主要有两项
    /// </summary>
    public interface IRealTimeDataNavigater : IRealTimeDataReader
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
        /// 导航到最新数据
        /// </summary>
        void NavigateToEnd();

        /// <summary>
        /// 数据变化事件触发
        /// </summary>
        event DataNavigateEventHandler OnDataNavigateHandler;
    }

    public delegate void DataNavigateEventHandler(Object source, RealTimeDataNavigateEventArgs e);

    /// <summary>
    /// 实时数据导航事件参数
    /// </summary>
    public class RealTimeDataNavigateEventArgs : System.EventArgs
    {
        private NavigateType navigateType;

        private double prevTime;

        private double time;

        private KLinePeriod forwardPeriod;

        private int forwardLength;

        /// <summary>
        /// 获得导航类型
        /// </summary>
        public NavigateType NavigateType
        {
            get
            {
                return navigateType;
            }
        }

        /// <summary>
        /// 获得导航之前的时间
        /// </summary>
        public double PrevTime
        {
            get
            {
                return prevTime;
            }
        }

        /// <summary>
        /// 获得导航之后的时间
        /// </summary>
        public double Time
        {
            get
            {
                return time;
            }
        }

        /// <summary>
        /// 向前导航的长度
        /// </summary>
        public int ForwardLength
        {
            get
            {
                return forwardLength;
            }
        }

        /// <summary>
        /// 导航的周期，比如说5秒，1分钟，1小时等
        /// </summary>
        public KLinePeriod ForwardPeriod
        {
            get
            {
                return forwardPeriod;
            }
        }

        /// <summary>
        /// 创建实时数据导航参数，传入参数为之前的时间和导航之后的时间
        /// </summary>
        /// <param name="prevTime"></param>
        /// <param name="time"></param>
        public RealTimeDataNavigateEventArgs(double prevTime, double time)
        {
            this.navigateType = NavigateType.NavigateTo;
            this.prevTime = prevTime;
            this.time = time;
        }

        public RealTimeDataNavigateEventArgs(NavigateType navigateType, double prevTime, double time, KLinePeriod forwardPeriod, int forwardLength)
        {
            this.navigateType = navigateType;
            this.prevTime = prevTime;
            this.time = time;
            this.forwardPeriod = forwardPeriod;
            this.forwardLength = forwardLength;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (navigateType)
            {
                case NavigateType.NavigateTo:
                    sb.Append("时间直接跳转:").Append("从" + prevTime + "修改为" + time);
                    break;
                case NavigateType.NavigateForward_Time:
                    sb.Append("时间前进:").Append(forwardLength).Append(" 周期").Append(forwardPeriod);
                    break;
                case NavigateType.NavigateForward_Period:
                    sb.Append("周期前进:").Append(forwardLength).Append(" 周期").Append(forwardPeriod);
                    break;
                case NavigateType.NavigateForward_Tick:
                    sb.Append("TICK前进:").Append(forwardLength).Append(" 周期TICK");
                    break;
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 导航类型
    /// </summary>
    public enum NavigateType
    {
        /// <summary>
        /// 导航到time位置
        /// </summary>
        /// <param name="time"></param>
        NavigateTo,

        /// <summary>
        /// 向前导航，前进一定的时间
        /// 比如，前进1小时，前进15分钟....
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        NavigateForward_Time,

        /// <summary>
        /// 向前导航，前进到下一个周期截至
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        NavigateForward_Period,

        /// <summary>
        /// 向前导航，前进tick条数据
        /// </summary>
        /// <param name="len"></param>
        NavigateForward_Tick
    }
}
