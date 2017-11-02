using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class KLineUtils
    {
        public static KLineBar GetKLineBar(ITickBar tickBar)
        {
            KLineBar bar = new KLineBar();
            bar.Time = tickBar.Time;
            bar.Start = tickBar.Price;
            bar.High = tickBar.Price;
            bar.Low = tickBar.Price;
            bar.End = tickBar.Price;
            bar.Money = tickBar.Mount * tickBar.Price;
            bar.Mount = tickBar.Mount;
            bar.Hold = tickBar.Hold;
            return bar;
        }

        public static KLineBar GetKLineBar(IKLineBar klineBar, ITickBar tickBar)
        {
            KLineBar bar = new KLineBar();
            bar.Time = tickBar.Time;
            bar.Start = klineBar.Start;
            bar.High = tickBar.Price > klineBar.High ? tickBar.Price : klineBar.High;
            bar.Low = tickBar.Price < klineBar.Low ? tickBar.Price : klineBar.Low;
            bar.End = tickBar.Price;
            bar.Money = klineBar.Money + tickBar.Mount * tickBar.Price;
            bar.Mount = klineBar.Mount + tickBar.Mount;
            bar.Hold = tickBar.Hold;
            return bar;
        }

        public static KLineBar GetKLineBar(ITickData tickData, int startIndex, int endIndex)
        {
            KLineBar bar = new KLineBar();
            ITickBar endTickBar = tickData.GetBar(endIndex);

            bar.Time = endTickBar.Time;
            bar.Start = tickData.Arr_Price[startIndex];

            float high = 0;
            float low = float.MaxValue;
            float money = bar.Money;
            int mount = bar.Mount;
            for (int i = startIndex; i <= endIndex; i++)
            {
                ITickBar tickBar = tickData.GetBar(i);
                if (high < tickBar.Price)
                    high = tickBar.Price;
                if (low > tickBar.Price)
                    low = tickBar.Price;
                money += tickBar.Mount * tickBar.Price;
                mount += tickBar.Mount;
            }
            bar.High = high;
            bar.Low = low;
            bar.End = endTickBar.Price;
            bar.Money = money;
            bar.Mount = mount;
            bar.Hold = endTickBar.Hold;
            return bar;
        }

        public static KLineBar GetKLineBar(IKLineBar klineBar, ITickData tickData, int startIndex, int endIndex)
        {
            KLineBar bar = new KLineBar();
            ITickBar endTickBar = tickData.GetBar(endIndex);

            bar.Time = endTickBar.Time;
            bar.Start = klineBar.Start;

            float high = klineBar.High;
            float low = klineBar.Low;
            float money = klineBar.Money;
            int mount = klineBar.Mount;
            for (int i = startIndex; i <= endIndex; i++)
            {
                ITickBar tickBar = tickData.GetBar(i);
                if (high < tickBar.Price)
                    high = tickBar.Price;
                if (low > tickBar.Price)
                    low = tickBar.Price;
                money += tickBar.Mount * tickBar.Price;
                mount += tickBar.Mount;
            }
            bar.High = high;
            bar.Low = low;
            bar.End = endTickBar.Price;
            bar.Money = money;
            bar.Mount = mount;
            bar.Hold = endTickBar.Hold;
            return bar;
        }
    }
}
