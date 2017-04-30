using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana
{
    public class KLineTrade
    {

        /**
         * 设置了autofilter之后，用bk指令买入后不会再次买入
         */
        public bool AutoFilter = true;

        private IKLineData data;

        //初始现金
        private float initMoney;

        //当前的现金
        private float money;

        //默认买入或平仓的手数
        private int defaultHand;

        private KLineHoldInfo buyHoldInfo;

        private KLineHoldInfo sellHoldInfo;

        private KLineTradeFee tradeFee;

        private List<KLineTradeDetailInfo> details = new List<KLineTradeDetailInfo>();

        public KLineTrade(IKLineData data, KLineTradeFee fee, int defaultHand, float initMoney)
        {
            this.data = data;
            this.tradeFee = fee;
            this.defaultHand = defaultHand;
            this.initMoney = initMoney;
            this.money = initMoney;
            this.buyHoldInfo = new KLineHoldInfo();
            this.sellHoldInfo = new KLineHoldInfo();
        }

        public void bk()
        {
            if (AutoFilter)
            {
                if (buyHoldInfo.mount != 0)
                    return;
            }
            bk(defaultHand);
        }

        public void bk(int cnt)
        {
            lock(this) {
                float price = data.End;
                float money = tradeFee.calcMoney(true, cnt, price);

                buyHoldInfo.addHold(true, price, cnt);
                KLineTradeDetailInfo trade = new KLineTradeDetailInfo(data.Code,data.Time, true, true, cnt, price, 0);
                details.Add(trade);
                //减掉花掉的钱
                this.money = this.money - money;
            }
        }

        public void bp()
        {
            lock(this) {
                bp2(buyHoldInfo.mount);
            }
        }

        public void bp(int cnt)
        {
            lock(this) {
                bp2(cnt);
            }
        }

        private void bp2(int cnt)
        {
            if (buyHoldInfo.mount == 0)
                return;
            float price = data.End;
            float money = tradeFee.calcMoney(false, cnt, buyHoldInfo.cost);
            float earn = calcEarn(buyHoldInfo);

            buyHoldInfo.removeHold(price, cnt);
            KLineTradeDetailInfo trade = new KLineTradeDetailInfo(data.Code, data.Time, false, true, cnt, price, earn);
            details.Add(trade);
            this.money = this.money + money + earn;
        }

        public void sk()
        {
            if (AutoFilter)
            {
                if (sellHoldInfo.mount != 0)
                    return;
            }
            sk(defaultHand);
        }

        public void sk(int cnt)
        {
            lock(this) {
                float price = data.End;
                float money = tradeFee.calcMoney(true, cnt, price);

                sellHoldInfo.addHold(false, price, cnt);
                KLineTradeDetailInfo trade = new KLineTradeDetailInfo(data.Code, data.Time, true, false, cnt, price,
                        0);
                details.Add(trade);
                this.money = this.money - money;
            }
        }

        public void sp()
        {
            lock (this) {
                sp2(defaultHand);
            }
        }

        public void sp(int cnt)
        {
            lock(this) {
                sp2(cnt);
            }
        }

        private void sp2(int cnt)
        {
            if (sellHoldInfo.mount == 0)
                return;
            float price = data.End;
            float money = tradeFee.calcMoney(false, cnt, sellHoldInfo.cost);
            float earn = calcEarn(sellHoldInfo);

            sellHoldInfo.removeHold(price, cnt);
            KLineTradeDetailInfo trade = new KLineTradeDetailInfo(data.Code, data.Time, false, false, cnt, price,
                    earn);
            details.Add(trade);
            this.money = this.money + money + earn;
        }

        public float getInitMoney()
        {
            return initMoney;
        }

        public float getMoney()
        {
            return money + calcEarn(buyHoldInfo) + calcEarn(sellHoldInfo);
        }

        private float calcHoldAsset(KLineHoldInfo hold)
        {
            //成本+赚到的钱
            return tradeFee.calcMoney(hold.mount, hold.cost) + calcEarn(hold);
        }

        private float calcEarn(KLineHoldInfo hold)
        {
            float earn = (data.End - hold.cost) * (tradeFee.getHandCount() * hold.mount);
            return hold.isMoreOrLess ? earn : -earn;
        }

        public float getAsset()
        {
            return money + calcHoldAsset(buyHoldInfo) + calcHoldAsset(sellHoldInfo);
        }

        public List<KLineTradeDetailInfo> getDetail()
        {
            return details;
        }
    }
}
