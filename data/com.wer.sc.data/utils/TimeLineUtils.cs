using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class TimeLineUtils
    {
        public static TimeLineBar GetTimeLineBar(ITickBar tickBar, float lastEndPrice)
        {
            TimeLineBar bar = new TimeLineBar();
            bar.Code = tickBar.Code;
            bar.Time = tickBar.Time;
            bar.Price = tickBar.Price;
            bar.Hold = tickBar.Hold;
            bar.Mount = tickBar.Mount;
            bar.UpRange = tickBar.Price - lastEndPrice;
            bar.UpPercent = (float)NumberUtils.percent(tickBar.Price, lastEndPrice);
            return bar;
        }

        public static TimeLineBar GetTimeLineBar(ITimeLineBar timelineBar, ITickBar tickBar, float lastEndPrice)
        {
            TimeLineBar bar = new TimeLineBar();
            bar.Code = tickBar.Code;
            bar.Time = tickBar.Time;
            bar.Price = tickBar.Price;
            bar.Hold = tickBar.Hold;
            bar.Mount = timelineBar.Mount + tickBar.Mount;
            bar.UpRange = tickBar.Price - lastEndPrice;
            bar.UpPercent = (float)NumberUtils.percent(tickBar.Price, lastEndPrice);
            return bar;
        }

        public static TimeLineBar GetTimeLineBar(ITickData tickData, int startIndex, int endIndex, float lastEndPrice)
        {
            TimeLineBar bar = new TimeLineBar();
            bar.Code = tickData.Code;
            
            ITickBar endTickBar = tickData.GetBar(endIndex);
            bar.Time = endTickBar.Time;
            bar.Price = endTickBar.Price;
            bar.Hold = endTickBar.Hold;
            bar.UpRange = endTickBar.Price - lastEndPrice;
            bar.UpPercent = (float)NumberUtils.percent(endTickBar.Price, lastEndPrice);

            int mount = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                mount += tickData.Arr_Mount[i];
            }
            bar.Mount = mount;
            bar.Hold = endTickBar.Hold;
            return bar;
        }

        public static TimeLineBar GetTimeLineBar(ITimeLineBar timeLineBar, ITickData tickData, int startIndex, int endIndex, float lastEndPrice)
        {
            TimeLineBar bar = new TimeLineBar();
            bar.Code = tickData.Code;

            ITickBar endTickBar = tickData.GetBar(endIndex);
            bar.Time = endTickBar.Time;
            bar.Price = endTickBar.Price;
            bar.Hold = endTickBar.Hold;
            bar.UpRange = endTickBar.Price - lastEndPrice;
            bar.UpPercent = (float)NumberUtils.percent(endTickBar.Price, lastEndPrice);

            int mount = timeLineBar.Mount;
            for (int i = startIndex; i <= endIndex; i++)
            {
                mount += tickData.Arr_Mount[i];
            }
            bar.Mount = mount;
            bar.Hold = endTickBar.Hold;
            return bar;
        }

    }
}
