using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    public class PriceGetter_HistoryDataForward : IPriceGetter
    {
        private IHistoryDataForward historyDataForward;

        public PriceGetter_HistoryDataForward(IHistoryDataForward historyDataForward)
        {
            this.historyDataForward = historyDataForward;
        }

        public event TimeChange timeChange;

        public bool Forward()
        {
            return historyDataForward.Forward();
        }

        public double Time
        {
            get
            {
                return historyDataForward.Time;
            }
        }

        public int GetBuyMount(string code)
        {
            //return historyDataForward.GetHistoryDataForward(code).
            return -1;
        }

        public double GetBuyPrice(string code)
        {
            throw new NotImplementedException();
        }

        public int GetSellMount(string code)
        {
            throw new NotImplementedException();
        }

        public double GetSellPrice(string code)
        {
            throw new NotImplementedException();
        }
    }
}
