using com.wer.sc.data.datapackage;
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
    public interface IDataNavigate_Code : IRealTimeData_Code, IRealTimeDataReader
    {
        /// <summary>
        /// 跳转到指定时间
        /// 执行该操作后，GetKLineData等获取数据的操作都会返回该时间上的数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns>如果不能够导航到该时间，则返回false</returns>
        bool NavigateTo(double time);

        /// <summary>
        /// 前进
        /// </summary>
        /// <returns></returns>
        bool Forward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 前进
        /// </summary>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        bool Forward(ForwardPeriod forwardPeriod);

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="forwardPeriod"></param>
        /// <returns></returns>
        bool Backward(KLinePeriod forwardPeriod);

        /// <summary>
        /// 得到它所属的数据包
        /// </summary>
        IDataPackage_Code DataPackage { get; }

        /// <summary>
        /// 导航到
        /// </summary>
        event DelegateOnNavigateTo OnNavigateTo;
    }

    public delegate void DelegateOnNavigateTo(Object sender, DataNavigateEventArgs e);

    public class DataNavigateEventArgs : System.EventArgs
    {
        private string prevCode;

        private string code;

        private double time;

        private double prevTime;

        public string Code
        {
            get { return code; }
        }

        public string PrevCode
        {
            get { return prevCode; }
        }

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

        public DataNavigateEventArgs(string prevCode, string code, double prevTime, double time)
        {
            this.prevCode = prevCode;
            this.code = code;
            this.prevTime = prevTime;
            this.time = time;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prevCode).Append(",").Append(code).AppendFormat(",");
            sb.Append(prevTime).Append(",").Append(time);
            return sb.ToString();
        }
    }
}