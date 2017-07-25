using com.wer.sc.data.forward;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 数据导航接口
    /// </summary>
    public interface IDataNavigate_Code : IRealTimeDataReader
    {
        /// <summary>
        /// 将当前时间指定到time
        /// </summary>
        /// <param name="time"></param>
        void NavigateTo(double time);

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        bool Backward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 导航到
        /// </summary>
        event DelegateOnNavigateTo OnNavigateTo;
    }

    public delegate void DelegateOnNavigateTo(Object sender, DataNavigateEventArgs e);

    public class DataNavigateEventArgs : System.EventArgs
    {
        private double time;

        private double prevTime;


        public double Time
        {
            get
            {
                return time;
            }
        }

        public double PrevTime
        {
            get
            {
                return prevTime;
            }
        }


        public DataNavigateEventArgs(double prevTime, double time)
        {
            this.prevTime = prevTime;
            this.time = time;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prevTime).Append(",").Append(time);
            return sb.ToString();
        }
    }
}
