using com.wer.sc.data.forward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.impl
{
    public class PriceGetter_HistoryDataForward_Code : IPriceGetter
    {
        private IPriceGetter priceGetter;

        public PriceGetter_HistoryDataForward_Code(IDataForward_Code historyDataForward_Code)
        {
            if (historyDataForward_Code.ForwardPeriod.IsTickForward)
                this.priceGetter = new PriceGetter_HistoryDataForward_Code_Tick(historyDataForward_Code);
            else
                this.priceGetter = new PriceGetter_HistoryDataForward_Code_KLine(historyDataForward_Code);

            this.priceGetter.timeChange += PriceGetter_timeChange;
        }

        private void PriceGetter_timeChange(IPriceGetter priceGetter)
        {
            if (timeChange != null)
                this.timeChange(priceGetter);
        }

        public event TimeChange timeChange;

        public double Time
        {
            get
            {
                return priceGetter.Time;
            }
        }

        public int GetBuyMount(string code)
        {
            return priceGetter.GetBuyMount(code);
        }

        public double GetBuyPrice(string code)
        {
            return priceGetter.GetBuyPrice(code);
        }

        public int GetSellMount(string code)
        {
            return priceGetter.GetSellMount(code);
        }

        public double GetSellPrice(string code)
        {
            return priceGetter.GetSellPrice(code);
        }
    }

    class PriceGetter_HistoryDataForward_Code_Tick : IPriceGetter
    {
        private IDataForward_Code historyDataForward_Code;

        public PriceGetter_HistoryDataForward_Code_Tick(IDataForward_Code historyDataForward_Code)
        {
            this.historyDataForward_Code = historyDataForward_Code;
            this.historyDataForward_Code.OnTick += HistoryDataForward_Code_OnTick;
        }

        private void HistoryDataForward_Code_OnTick(object sender, ForwardOnTickArgument argument)
        {
            if (timeChange != null)
                timeChange(this);
        }

        public event TimeChange timeChange;

        public bool Forward()
        {
            return this.historyDataForward_Code.Forward();
        }
        public double Time
        {
            get
            {
                return historyDataForward_Code.Time;
            }
        }
        public int GetBuyMount(string code)
        {
            return this.historyDataForward_Code.GetTickData().BuyMount;
        }

        public double GetBuyPrice(string code)
        {
            return this.historyDataForward_Code.GetTickData().BuyPrice;
        }

        public int GetSellMount(string code)
        {
            return this.historyDataForward_Code.GetTickData().SellMount;
        }

        public double GetSellPrice(string code)
        {
            return this.historyDataForward_Code.GetTickData().SellPrice;
        }
    }

    class PriceGetter_HistoryDataForward_Code_KLine : IPriceGetter
    {
        private IDataForward_Code historyDataForward_Code;

        public PriceGetter_HistoryDataForward_Code_KLine(IDataForward_Code historyDataForward_Code)
        {
            this.historyDataForward_Code = historyDataForward_Code;
            this.historyDataForward_Code.OnBar += HistoryDataForward_Code_OnBar;
        }

        private void HistoryDataForward_Code_OnBar(object sender, ForwardOnBarArgument argument)
        {
            if (timeChange != null)
                timeChange(this);
        }

        public event TimeChange timeChange;

        public bool Forward()
        {
            return this.historyDataForward_Code.Forward();
        }

        public double Time
        {
            get
            {
                return historyDataForward_Code.Time;
            }
        }

        public int GetBuyMount(string code)
        {
            return this.historyDataForward_Code.GetKLineData().Mount;
        }

        public double GetBuyPrice(string code)
        {
            return this.historyDataForward_Code.GetKLineData().End;
        }

        public int GetSellMount(string code)
        {
            return this.historyDataForward_Code.GetKLineData().Mount;
        }

        public double GetSellPrice(string code)
        {
            return this.historyDataForward_Code.GetKLineData().End;
        }
    }
}