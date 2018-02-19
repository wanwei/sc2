using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据导航
    /// 该接口的用途是对一支股票或期货数据进行导航
    /// </summary>
    public interface IDataNavigate3
    {


        /// <summary>
        /// 得到这支股票或期货代码
        /// </summary>
        String Code { get; }

        /// <summary>
        /// 得到当前K线数据
        /// </summary>
        IKLineData CurrentKLineData { get; }

        int CurrentKLineIndex { get; }

        /// <summary>
        /// 得到当前分时数据
        /// </summary>
        ITimeLineData CurrentRealData { get; }

        int CurrentRealIndex { get; }

        ITickData CurrentTickData { get; }

        int CurrentTickIndex { get; }

        double CurrentTime { get; }

        void Change(IKLineData data, double time);

        void Change(String code, double time, KLinePeriod period);

        void ChangeCode(String code);

        void ChangeTime(double time);

        void ChangeIndex(int index);

        void ChangePeriod(KLinePeriod period);

        event DataChangeEventHandler OnDataChangeHandler;
    }

    public interface IDataNavigate2
    {
        /// <summary>
        /// 设置或获取导航的开始日期
        /// </summary>
        int StartDate { get;  }

        /// <summary>
        /// 设置或获取导航的结束时间
        /// </summary>
        int EndDate { get; }

        /// <summary>
        /// 得到当前股票或期货代码
        /// </summary>
        String Code { get; }

        /// <summary>
        /// 得到当前时间
        /// </summary>
        double CurrentTime { get; }

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
        /// 修改数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="time"></param>
        void ChangeData(string code, double time);

        /// <summary>
        /// 修改当前时间
        /// </summary>
        /// <param name="time"></param>
        void ChangeTime(double time);

        /// <summary>
        /// 前进，前进时会修改当前时间
        /// </summary>
        /// <param name="period"></param>
        /// <param name="len"></param>
        void Forward(KLinePeriod period, int len);

        /// <summary>
        /// 前进一个tick数据
        /// </summary>
        /// <param name="len"></param>
        void ForwardTick(int len);

        event DataChangeEventHandler OnDataChangeHandler;
    }

    public delegate void DataChangeEventHandler(Object source, DataChangeEventArgs e);

    public class DataChangeEventArgs : System.EventArgs
    {
        internal string code;

        internal string prevCode;

        internal string time;

        internal string prevTime;

        internal bool isForwardChange;

        internal int forwardLength;

        internal KLinePeriod forwardPeriod;

        public string Code
        {
            get
            {
                return code;
            }
        }

        public string PrevCode
        {
            get
            {
                return prevCode;
            }
        }

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

        public bool IsForwardChange
        {
            get
            {
                return isForwardChange;
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

        public DataChangeEventArgs()
        {

        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsForwardChange)
            {
                if (forwardLength >= 0)
                    sb.Append("时间前进");
                else
                    sb.Append("时间后退");
                sb.Append(forwardLength).Append(forwardPeriod);
            }
            else
            {
                if (code == prevCode)
                {
                    sb.Append("时间修改:").Append("从" + prevTime + "修改为" + time);
                }
                else if (prevTime == time)
                {
                    sb.Append("CODE修改:").Append("从" + prevCode + "修改为" + code);
                }
                else
                {
                    sb.Append("数据修改:");
                    sb.Append("CODE从" + prevCode + "修改为" + code + ";");
                    sb.Append("时间从" + prevTime + "修改为" + time);
                }
            }
            return sb.ToString();
        }
    }
}