using com.wer.sc.data;
using com.wer.sc.data.market;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnfutures.market
{
    public class StructTransfer
    {
        public static LoginInfo TransferUserLogin(XAPI.RspUserLoginField userLogin)
        {
            LoginInfo loginInfo = new LoginInfo();
            loginInfo.AccountID = userLogin.AccountID;
            if (userLogin.InvestorName != null)
                loginInfo.InvestorName = GetStringFromByte(userLogin.InvestorName);
            loginInfo.LoginTime = userLogin.LoginTime;
            loginInfo.SessionID = userLogin.SessionID;
            if (userLogin.Text != null)
                loginInfo.Text = GetStringFromByte(userLogin.Text);
            loginInfo.TradingDay = userLogin.TradingDay;
            loginInfo.UserID = userLogin.UserID;
            loginInfo.XErrorID = userLogin.XErrorID;
            return loginInfo;
        }

        private static string GetStringFromByte(byte[] bs)
        {
            int count = bs.Length;
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i] == default(byte))
                {
                    count = i;
                    break;
                }
            }
            return Encoding.Default.GetString(bs, 0, count);
        }

        //public static MarketData TransferMarketData(XAPI.DepthMarketDataNClass marketData)
        //{
        //    MarketData rspMarketData = new MarketData();
        //    XAPI.DepthField bidField = marketData.Bids[0];
        //    XAPI.DepthField askField = marketData.Asks[0];
        //    rspMarketData.BuyMount = askField.Size;
        //    rspMarketData.BuyPrice = (float)askField.Price;
        //    rspMarketData.SellMount = bidField.Size;
        //    rspMarketData.SellPrice = (float)bidField.Price;
        //    rspMarketData.Price = (float)marketData.OpenPrice;
        //    rspMarketData.Time = marketData.ActionDay + Math.Round(((double)marketData.UpdateTime) / 100000, 6);

        //    return rspMarketData;
        //}

        public static ITickBar TransferTickBar(XAPI.DepthMarketDataNClass xapiMarketData)
        {
            TickBar tickBar = new TickBar();
            //tickBar.Code = transferCode(xapiMarketData.InstrumentID);
            tickBar.Code = xapiMarketData.InstrumentID;
            tickBar.Time = xapiMarketData.ActionDay + Math.Round((double)xapiMarketData.UpdateTime / 1000000, 6);
            tickBar.Price = (float)xapiMarketData.LastPrice;
            tickBar.Mount = 0;
            tickBar.TotalMount = (int)xapiMarketData.Volume;
            tickBar.Add = 0;
            tickBar.Hold = (int)xapiMarketData.OpenInterest;
            tickBar.BuyMount = xapiMarketData.Bids.Length == 0 ? 0 : xapiMarketData.Bids[0].Size;
            tickBar.BuyPrice = xapiMarketData.Bids.Length == 0 ? 0 : (float)xapiMarketData.Bids[0].Price;
            tickBar.IsBuy = xapiMarketData.LastPrice == tickBar.BuyPrice;
            tickBar.SellMount = xapiMarketData.Asks.Length == 0 ? 0 : xapiMarketData.Asks[0].Size;
            tickBar.SellPrice = xapiMarketData.Asks.Length == 0 ? 0 : (float)xapiMarketData.Asks[0].Price;
            return tickBar;
        }

        private static string transferCode(string code)
        {
            string prefix = code.Substring(0, code.Length - 4);
            return prefix + code.Substring(code.Length - 2);
        }

        public static sc.data.market.InstrumentInfo TransferInstrumentInfo(XAPI.InstrumentField instrumentField)
        {
            sc.data.market.InstrumentInfo instrument = new sc.data.market.InstrumentInfo();
            instrument.ExchangeID = instrumentField.ExchangeID;
            instrument.ExpireDate = instrumentField.ExpireDate;
            instrument.InstLifePhase = EnumTransfer.TransferInstLifePhaseType(instrumentField.InstLifePhase);
            if (instrumentField.InstrumentName != null)
                instrument.InstrumentName = GetStringFromByte(instrumentField.InstrumentName);
            instrument.PriceTick = instrumentField.PriceTick;
            instrument.ProductID = instrumentField.ProductID;
            instrument.StrikePrice = instrumentField.StrikePrice;
            instrument.Symbol = instrumentField.Symbol;
            instrument.SaveID = transferCode(instrument.Symbol);
            instrument.UnderlyingInstrID = instrumentField.UnderlyingInstrID;
            instrument.VolumeMultiple = instrumentField.VolumeMultiple;
            return instrument;
        }

        public static AccountInfo TransferAccountInfo(XAPI.AccountField accountField)
        {
            AccountInfo accountInfo = new AccountInfo();
            accountInfo.AccountID = accountField.AccountID;
            accountInfo.Available = accountField.Available;
            accountInfo.Balance = accountField.Balance;
            accountInfo.CashIn = accountField.CashIn;
            accountInfo.CloseProfit = accountField.CloseProfit;
            accountInfo.Commission = accountField.Commission;
            accountInfo.CurrMargin = accountField.CurrMargin;
            accountInfo.Deposit = accountField.Deposit;
            accountInfo.FrozenCash = accountField.FrozenCash;
            accountInfo.FrozenCommission = accountField.FrozenCommission;
            accountInfo.FrozenStampTax = accountField.FrozenStampTax;
            accountInfo.FrozenTransferFee = accountField.FrozenTransferFee;
            accountInfo.PositionProfit = accountField.PositionProfit;
            accountInfo.PreBalance = accountField.PreBalance;
            accountInfo.StampTax = accountField.StampTax;
            accountInfo.TransferFee = accountField.TransferFee;
            accountInfo.Withdraw = accountField.Withdraw;
            accountInfo.WithdrawQuota = accountField.WithdrawQuota;
            return accountInfo;
        }

        public static OrderInfo TransferOrderInfo(XAPI.OrderField order)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.OrderTime = order.Date + Math.Round((double)order.Time / 1000000, 6);
            orderInfo.Instrumentid = order.InstrumentID;
            orderInfo.Direction = EnumTransfer.TransferOrderSide(order.Side);
            orderInfo.Price = order.Price;
            orderInfo.Volume = (int)order.Qty;
            return orderInfo;
        }

        public static TradeInfo TransferTradeInfo(XAPI.TradeField trade)
        {
            TradeInfo tradeInfo = new TradeInfo();
            tradeInfo.AccountID = trade.AccountID;
            tradeInfo.InstrumentID = trade.InstrumentID;
            tradeInfo.InstrumentName = GetStringFromByte(trade.InstrumentName);
            tradeInfo.OpenClose = EnumTransfer.TransferOpenCloseType(trade.OpenClose);
            tradeInfo.Price = trade.Price;
            tradeInfo.Qty = trade.Qty;
            tradeInfo.Side = EnumTransfer.TransferOrderSide(trade.Side);
            tradeInfo.Time = trade.Date + Math.Round((double)trade.Time / 1000000, 6);
            tradeInfo.TradeID = trade.TradeID;
            return tradeInfo;
        }

        public static PositionInfo TransferPositionInfo(XAPI.PositionField position)
        {
            PositionInfo positionInfo = new PositionInfo();
            positionInfo.AccountID = position.AccountID;
            positionInfo.ClientID = position.ClientID;
            positionInfo.Date = position.Date;
            positionInfo.ExchangeID = position.ExchangeID;
            positionInfo.HistoryFrozen = position.HistoryFrozen;
            positionInfo.HistoryPosition = position.HistoryPosition;
            positionInfo.InstrumentID = position.InstrumentID;
            positionInfo.InstrumentName = GetStringFromByte(position.InstrumentName);
            positionInfo.Position = position.Position;
            positionInfo.PositionCost = position.PositionCost;
            positionInfo.Side = EnumTransfer.TransferPositionSide(position.Side);
            positionInfo.Symbol = position.Symbol;
            positionInfo.TodayBSFrozen = position.TodayBSFrozen;
            positionInfo.TodayBSPosition = position.TodayBSPosition;
            positionInfo.TodayPosition = position.TodayPosition;
            positionInfo.TodayPRFrozen = position.TodayPRFrozen;
            positionInfo.TodayPRPosition = position.TodayPRPosition;
            return positionInfo;
        }
    }

    //public class TickBarWarpper : TickBar_Abstract, ITickBar
    //{
    //    private XAPI.DepthMarketDataNClass xapiMarketData;
    //    public TickBarWarpper(XAPI.DepthMarketDataNClass xapiMarketData)
    //    {
    //        this.xapiMarketData = xapiMarketData;
    //    }

    //    public override int Add
    //    {
    //        get
    //        {
    //            return 0;
    //        }
    //    }

    //    public override int BuyMount
    //    {
    //        get
    //        {
    //            if (xapiMarketData.Bids.Length == 0)
    //            {
    //                return 0;
    //            }
    //            return xapiMarketData.Bids[0].Size;
    //        }
    //    }

    //    public override float BuyPrice
    //    {
    //        get
    //        {
    //            if (xapiMarketData.Bids.Length == 0)
    //            {
    //                return Price;
    //            }
    //            return (float)xapiMarketData.Bids[0].Price;
    //        }
    //    }

    //    public override string Code
    //    {
    //        get
    //        {
    //            return xapiMarketData.Symbol;
    //        }
    //    }

    //    public override int Hold
    //    {
    //        get
    //        {
    //            return (int)xapiMarketData.OpenInterest;
    //        }
    //    }

    //    public override bool IsBuy
    //    {
    //        get
    //        {
    //            return xapiMarketData.ClosePrice == BuyPrice;
    //        }
    //    }

    //    public override int Mount
    //    {
    //        get
    //        {
    //            return (int)xapiMarketData.Volume;
    //        }
    //    }

    //    public override float Price
    //    {
    //        get
    //        {
    //            return (float)xapiMarketData.ClosePrice;
    //        }
    //    }

    //    public override int SellMount
    //    {
    //        get
    //        {
    //            if (xapiMarketData.Asks.Length == 0)
    //            {
    //                return 0;
    //            }
    //            return xapiMarketData.Asks[0].Size;
    //        }
    //    }

    //    public override float SellPrice
    //    {
    //        get
    //        {
    //            if (xapiMarketData.Asks.Length == 0)
    //            {
    //                return Price;
    //            }
    //            return (float)xapiMarketData.Asks[0].Price;
    //        }
    //    }

    //    public override double Time
    //    {
    //        get
    //        {
    //            return xapiMarketData.TradingDay + Math.Round((double)xapiMarketData.UpdateTime / 1000000, 6);
    //        }
    //    }

    //    public override int TotalMount
    //    {
    //        get
    //        {
    //            return 0;
    //        }
    //    }
    //}
}
