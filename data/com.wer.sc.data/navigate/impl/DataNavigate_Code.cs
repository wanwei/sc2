using com.wer.sc.data.reader;
using com.wer.sc.data.utils;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate.impl
{
    /// <summary>
    /// 数据导航实现类
    /// </summary>
    public class DataNavigate_Code : IDataNavigate_Code
    {
        private IDataReader dataReader;

        private string code;

        private double time;

        //当前tick数据
        private DataNavigate_Code_Tick navigate_Tick;

        //当前的k线数据
        private Dictionary<KLinePeriod, DataNavigate_Code_KLine> dicNavigateKLine = new Dictionary<KLinePeriod, DataNavigate_Code_KLine>();

        //当前分时线数据
        private DataNavigate_Code_TimeLine navigate_TimeLine;

        public DataNavigate_Code(IDataReader dataReader, string code, double time)
        {
            this.dataReader = dataReader;
            this.code = code;
            this.NavigateTo(time);
        }

        public void NavigateTo(double time)
        {
            double prevTime = this.time;
            bool timeChage = this.time == time;
            this.time = time;
            if (timeChage)
                OnNavigateTo(this, new DataNavigateEventArgs(prevTime, time));
        }

        public double Time
        {
            get
            {
                return time;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public IKLineData GetKLineData(KLinePeriod period)
        {
            if (!dicNavigateKLine.ContainsKey(period))
            {
                DataNavigate_Code_KLine navigate = new DataNavigate_Code_KLine(dataReader, code, time, period);
                dicNavigateKLine.Add(period, navigate);
            }
            DataNavigate_Code_KLine navigate_KLine = dicNavigateKLine[period];
            if (time != navigate_KLine.Time)
                navigate_KLine.ChangeTime(time);
            return navigate_KLine.GetKLineData();
        }

        public ITickData GetTickData()
        {
            if (navigate_Tick == null)
            {
                navigate_Tick = new DataNavigate_Code_Tick(dataReader, code, time);
                return navigate_Tick.GetTickData();
            }
            if (time != navigate_Tick.Time)
                navigate_Tick.ChangeTime(time);
            return navigate_Tick.GetTickData();
        }

        public ITimeLineData GetTimeLineData()
        {
            if (navigate_TimeLine == null)
            {
                navigate_TimeLine = new DataNavigate_Code_TimeLine(dataReader, code, time);
                return navigate_TimeLine.GetTimeLineData();
            }
            if (time != navigate_TimeLine.Time)
                navigate_TimeLine.ChangeTime(time);
            return navigate_TimeLine.GetTimeLineData();
        }

        public event DelegateOnNavigateTo OnNavigateTo;
    }
}