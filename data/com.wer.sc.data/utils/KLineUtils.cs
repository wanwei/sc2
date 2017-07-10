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

    }
}
