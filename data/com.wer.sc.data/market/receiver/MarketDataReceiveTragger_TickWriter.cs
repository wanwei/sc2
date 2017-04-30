using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.data.market.receiver
{
    public class MarketDataReceiveTragger_TickWriter : IMarketDataReceiveTragger
    {
        private int writeInterval;

        private string storePath;

        private TickData_RealWriter tickDataBuilder;

        public MarketDataReceiveTragger_TickWriter() : this(@"d:\scpresent\", 200)
        {
        }

        public MarketDataReceiveTragger_TickWriter(string storePath, int writeInterval)
        {
            this.storePath = storePath;
            this.writeInterval = writeInterval;
        }

        public void TickDataReceived(object sender, ITickData tickData)
        {
            if (this.tickDataBuilder != null)
                tickDataBuilder.Receive(tickData);
        }

        public void TradingDayChanged(int currentTradingDay)
        {
            if (this.tickDataBuilder != null)
                this.tickDataBuilder.Dispose();
            this.tickDataBuilder = new TickData_RealWriter(storePath, currentTradingDay, writeInterval);
        }
    }
}