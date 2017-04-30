using com.wer.sc.data.cache.impl;
using com.wer.sc.data.reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.navigate
{
    /// <summary>
    /// 一日的数据导航
    /// </summary>
    public class DayNavigate
    {
        private DataReaderFactory dataReaderFactory;

        private string code;

        private int date;

        private int currentTickIndex = 0;

        private ITickData tickData;

        private ITimeLineData timeLineData;

        public DayNavigate(DataReaderFactory dataReaderFactory, string code, int date)
        {
            this.dataReaderFactory = dataReaderFactory;
            this.code = code;
            this.date = date;
            this.tickData = dataReaderFactory.TickDataReader.GetTickData(code, date);
            this.timeLineData = dataReaderFactory.TimeLineDataReader.GetData(code, date);
        }

        public ITimeLineData GetRealData()
        {
            return timeLineData;
        }

        public ITickData GetTickData()
        {
            return tickData;
        }

        public void NavigateTo(double time)
        {
            int index = timeLineData.IndexOfTime(time);

        }

        public void NavigateForward_Tick(int len)
        {
            tickData.BarPos += len;
        }
    }
}