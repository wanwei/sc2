using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{

    public interface IPriceGetter
    {
        //bool Forward();

        event TimeChange timeChange;

        double Time { get; }

        double GetBuyPrice(string code);

        int GetBuyMount(string code);

        double GetSellPrice(string code);

        int GetSellMount(string code);
    }


    public delegate void TimeChange(IPriceGetter priceGetter);
}
