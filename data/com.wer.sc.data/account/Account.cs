using com.wer.sc.data.forward;
using com.wer.sc.data.market;
using com.wer.sc.data.reader;
using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.account
{
    public class Account : IAccount, IXmlExchange
    {
        private IDataCenter dataCenter;

        public static TradeFee_Code DEFAULTFEECODE = new TradeFee_Code(null, 10, 1, 3, 3, true, 15);

        private AccountSetting accountSetting = new AccountSetting();

        private double initMoney;

        internal double money;

        private List<PositionInfo> positions = new List<PositionInfo>();

        protected Dictionary<String, PositionInfo> mapPosition_Buy = new Dictionary<String, PositionInfo>();

        protected Dictionary<String, PositionInfo> mapPosition_Sell = new Dictionary<String, PositionInfo>();

        //所有的交易
        private List<TradeInfo> currentTrades = new List<TradeInfo>();

        //所有的委托
        private List<OrderInfo> historyOrders = new List<OrderInfo>();

        //还没执行完成的委托
        private List<OrderInfo> waitingOrders = new List<OrderInfo>();

        private List<OrderInfo> loopOrders = new List<OrderInfo>();

        private ITradeFee fee;

        private TradeFee_Code defaultFee_Code = Account.DEFAULTFEECODE;

        private String description = null;

        private IDataForward_Code dataForward_Code;

        private object lockObj = new object();

        private Dictionary<string, double> dic_OrderID_LockMoney = new Dictionary<string, double>();

        private Dictionary<string, OrderDelayInfo> dic_Code_OrderDelayInfo = new Dictionary<string, OrderDelayInfo>();

        internal Account(IDataCenter dataCenter)
        {
            this.dataCenter = dataCenter;
        }

        internal Account(double money) : this(money, null, null)
        {
            this.AccountSetting.TradeType = AccountTradeType.IMMEDIATELY;
        }

        internal Account(double money, IDataForward_Code realTimeDataReader) : this(money, realTimeDataReader, null)
        {

        }

        internal Account(double money, IDataForward_Code realTimeDataReader, ITradeFee fee)
        {
            this.money = money;
            this.initMoney = money;
            this.fee = fee;
            if (realTimeDataReader != null)
            {
                this.dataForward_Code = realTimeDataReader;
                this.dataForward_Code.OnRealTimeChanged += RealTimeDataReader_RealTimeChanged;
            }
        }

        public AccountSetting AccountSetting
        {
            get { return accountSetting; }
        }

        public IDataForward_Code DataForward_Code
        {
            get { return dataForward_Code; }
        }

        #region 交易

        private void RealTimeDataReader_RealTimeChanged(object sender, RealTimeChangedArgument e)
        {
            lock (lockObj)
            {
                //过了新的一天，昨日的委托需要全部清除
                if (e.TradingDayChanged)
                {
                    CancelAllOrder();
                    return;
                }

                loopOrders.Clear();
                loopOrders.AddRange(waitingOrders);

                //TODO 价格变化后需要修改现金余额
                for (int i = 0; i < loopOrders.Count; i++)
                {
                    OrderInfo order = loopOrders[i];
                    if (AccountSetting.TradeType == AccountTradeType.DELAYTIME)
                    {
                        double delayTime = AccountSetting.DelayTime;
                        double betweenTime = e.Time - order.OrderTime;
                        if (betweenTime <= delayTime)
                            continue;
                    }
                    else if (accountSetting.TradeType == AccountTradeType.DELAYTICK)
                    {
                        ITickData tickData = this.dataForward_Code.GetTickData();
                        if (tickData != null)
                        {
                            if (this.dic_Code_OrderDelayInfo.ContainsKey(order.OrderID))
                            {
                                OrderDelayInfo delayInfo = this.dic_Code_OrderDelayInfo[order.OrderID];
                                int tickIndex = delayInfo.orderTickIndex;
                                int delayTick = AccountSetting.DelayTick;
                                if (tickData.BarPos - tickIndex < delayTick)
                                    continue;
                            }
                        }
                    }
                    DoTrade(order);
                }
            }
        }

        private bool DoTrade(OrderInfo orderInfo)
        {
            if (orderInfo.OpenClose == OpenCloseType.Open)
                return DoTrade_Open(orderInfo);
            else
                return DoTrade_Close(orderInfo);
        }

        private bool DoTrade_Open(OrderInfo orderInfo)
        {
            /*
             * 开仓成交：
             * 1.确定成交价格和能成交的量
             * 2.生成成交信息
             * 3.增加对应持仓    
             * 4.修改委托信息
             */
            int tradeMount;
            if (accountSetting.TradeType == AccountTradeType.IMMEDIATELY)
                tradeMount = orderInfo.Volume;
            else
                tradeMount = GetTradeMount(orderInfo);

            if (tradeMount == 0)
                return false;
            TradeInfo tradeInfo = AddTradeInfo(orderInfo, tradeMount);
            AddPositionInfo_Open(tradeInfo, tradeMount);

            bool isAllTrade;
            if (tradeMount == orderInfo.LeavesQty)
            {
                orderInfo.CumQty = orderInfo.Volume;
                orderInfo.ExecType = ExecType.Trade;
                isAllTrade = true;
            }
            else
            {
                orderInfo.CumQty += tradeMount;
                orderInfo.ExecType = ExecType.Trade;
                isAllTrade = false;
            }

            if (isAllTrade)
            {
                this.dic_Code_OrderDelayInfo.Remove(orderInfo.OrderID);
                this.dic_OrderID_LockMoney.Remove(orderInfo.OrderID);
                this.waitingOrders.Remove(orderInfo);
            }
            else
            {
                double money = CalcTradeMoney_Open(orderInfo.Instrumentid, tradeInfo.Price, tradeMount, OpenCloseType.Open);
                double lockMoney = this.dic_OrderID_LockMoney[orderInfo.OrderID] - money;
                this.dic_OrderID_LockMoney[orderInfo.OrderID] = lockMoney;
            }

            if (OnReturnTrade != null)
            {
                OnReturnTrade(this, ref tradeInfo);
            }

            return isAllTrade;
        }

        private void AddPositionInfo_Open(TradeInfo tradeInfo, int tradeMount)
        {
            if (tradeInfo.Side == OrderSide.Buy)
            {
                if (this.mapPosition_Buy.ContainsKey(tradeInfo.InstrumentID))
                {
                    PositionInfo positionInfo = this.mapPosition_Buy[tradeInfo.InstrumentID];
                    MergePosition_Open(positionInfo, tradeInfo, tradeMount);
                }
                else
                {
                    AddNewPositionInfo(tradeInfo, tradeMount);
                }
            }
            else
            {
                if (this.mapPosition_Sell.ContainsKey(tradeInfo.InstrumentID))
                {
                    PositionInfo positionInfo = this.mapPosition_Sell[tradeInfo.InstrumentID];
                    MergePosition_Open(positionInfo, tradeInfo, tradeMount);
                }
                else
                {
                    AddNewPositionInfo(tradeInfo, tradeMount);
                }
            }
        }

        private void AddNewPositionInfo(TradeInfo tradeInfo, int tradeMount)
        {
            PositionInfo positionInfo = new PositionInfo();
            positionInfo.InstrumentID = tradeInfo.InstrumentID;
            positionInfo.Position = tradeMount;
            positionInfo.PositionCost = tradeInfo.Price;
            positionInfo.Side = tradeInfo.Side == OrderSide.Buy ? PositionSide.Long : PositionSide.Short;
            this.mapPosition_Buy.Add(positionInfo.InstrumentID, positionInfo);
            this.positions.Add(positionInfo);
        }

        private void MergePosition_Open(PositionInfo positionInfo, TradeInfo tradeInfo, int tradeMount)
        {
            int currentMount = positionInfo.Position;
            positionInfo.Position = currentMount + tradeMount;
            positionInfo.PositionCost = (currentMount * positionInfo.PositionCost + tradeInfo.Price * tradeMount) / positionInfo.Position;
        }

        private TradeInfo AddTradeInfo(OrderInfo orderInfo, int tradeMount)
        {
            TradeInfo tradeInfo = new TradeInfo();
            tradeInfo.InstrumentID = orderInfo.Instrumentid;
            tradeInfo.OpenClose = orderInfo.OpenClose;
            //TODO 应该是当前价格
            tradeInfo.Price = GetTradePrice(orderInfo.Instrumentid, orderInfo.Direction, orderInfo.Price);
            tradeInfo.Qty = tradeMount;
            tradeInfo.Side = orderInfo.Direction;
            tradeInfo.Time = Time;
            tradeInfo.TradeID = Guid.NewGuid().ToString();
            this.currentTrades.Add(tradeInfo);
            return tradeInfo;
        }

        private double GetTradePrice(string code, OrderSide orderSide, double price)
        {
            if (AccountSetting.SlipPerccent != 0)
            {
                if (orderSide == OrderSide.Buy)
                    return price * (1 + AccountSetting.SlipPerccent);
                else
                    return price * (1 - AccountSetting.SlipPerccent);
            }
            else if (accountSetting.SlipPrice != 0)
            {
                ITradeFee_Code fee = this.GetTradeFee_Code(code);
                double minPriceChange = fee.MinPriceChange;
                if (orderSide == OrderSide.Buy)
                    return price + (accountSetting.SlipPrice * minPriceChange);
                else
                    return price * (accountSetting.SlipPrice * minPriceChange);
            }

            return price;
        }

        private int GetTradeMount(OrderInfo orderInfo)
        {
            double orderPrice = orderInfo.Price;
            ITickData tickData = dataForward_Code.GetTickData();
            if (tickData != null)
            {
                if (orderInfo.Direction == OrderSide.Buy)
                {
                    if (orderInfo.Price < tickData.BuyPrice)
                        return 0;
                    return tickData.BuyMount >= orderInfo.LeavesQty ? orderInfo.LeavesQty : tickData.BuyMount;
                }
                else
                {
                    if (orderInfo.Price > tickData.SellPrice)
                        return 0;
                    return tickData.SellMount >= orderInfo.LeavesQty ? orderInfo.LeavesQty : tickData.SellMount;
                }
            }
            else
            {
                double price = dataForward_Code.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
                if (orderInfo.Direction == OrderSide.Buy)
                    return orderInfo.Price >= price ? orderInfo.Volume : 0;
                else
                    return orderInfo.Price <= price ? orderInfo.Volume : 0;
            }
        }

        private bool DoTrade_Close(OrderInfo orderInfo)
        {
            /*
            * 平仓成交：
            * 1.确定成交价格和能成交的量
            * 2.生成成交信息
            * 3.减少对应持仓    
            * 4.将钱转为现金
            * 5.修改委托信息
            */
            int tradeMount;
            if (accountSetting.TradeType == AccountTradeType.IMMEDIATELY)
                tradeMount = orderInfo.Volume;
            else
                tradeMount = GetTradeMount(orderInfo);
            if (tradeMount == 0)
                return false;
            TradeInfo tradeInfo = AddTradeInfo(orderInfo, tradeMount);

            PositionInfo positionInfo = GetPositionInfo_Close(orderInfo.Instrumentid, orderInfo.Direction);
            if (positionInfo == null)
                return false;
            double tradeMoney = CalcTradeMoney_Close(positionInfo, tradeInfo.Price, tradeMount);
            this.money += tradeMoney;

            AddPositionInfo_Close(tradeInfo, tradeMount);

            bool isAllTrade;
            if (tradeMount == orderInfo.LeavesQty)
            {
                orderInfo.CumQty = orderInfo.Volume;
                orderInfo.ExecType = ExecType.Trade;
                isAllTrade = true;
            }
            else
            {
                orderInfo.CumQty += tradeMount;
                orderInfo.ExecType = ExecType.Trade;
                isAllTrade = false;
            }

            if (isAllTrade)
            {
                this.dic_Code_OrderDelayInfo.Remove(orderInfo.OrderID);
                this.waitingOrders.Remove(orderInfo);
            }

            if (OnReturnTrade != null)
            {
                OnReturnTrade(this, ref tradeInfo);
            }
            return isAllTrade;
        }

        private PositionInfo GetPositionInfo_Close(String code, OrderSide orderSide)
        {
            Dictionary<string, PositionInfo> dic_position = orderSide == OrderSide.Buy ? mapPosition_Sell : mapPosition_Buy;
            if (!dic_position.ContainsKey(code))
                return null;
            return dic_position[code];
        }

        private void AddPositionInfo_Close(TradeInfo tradeInfo, int tradeMount)
        {
            if (tradeInfo.Side == OrderSide.Buy)
            {
                if (this.mapPosition_Sell.ContainsKey(tradeInfo.InstrumentID))
                {
                    PositionInfo positionInfo = this.mapPosition_Sell[tradeInfo.InstrumentID];
                    MergePosition_Close(positionInfo, tradeInfo, tradeMount);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (this.mapPosition_Buy.ContainsKey(tradeInfo.InstrumentID))
                {
                    PositionInfo positionInfo = this.mapPosition_Buy[tradeInfo.InstrumentID];
                    MergePosition_Close(positionInfo, tradeInfo, tradeMount);
                }
                else
                {
                    return;
                }
            }
        }

        private void MergePosition_Close(PositionInfo positionInfo, TradeInfo tradeInfo, int tradeMount)
        {
            int currentMount = positionInfo.Position;
            positionInfo.Position = currentMount - tradeMount;
            if (positionInfo.Position == 0)
            {
                if (positionInfo.Side == PositionSide.Long)
                    this.mapPosition_Buy.Remove(positionInfo.InstrumentID);
                else
                    this.mapPosition_Sell.Remove(positionInfo.InstrumentID);
                this.positions.Remove(positionInfo);
                return;
            }
        }

        #endregion

        public ITradeFee Fee
        {
            get
            {
                return fee;
            }
        }

        public double Money
        {
            get
            {
                return Math.Round(money, 2);
            }
        }

        public double InitMoney
        {
            get
            {
                return initMoney;
            }
        }

        public String Description
        {
            get
            {
                return description;
            }
        }

        public double Asset
        {
            get
            {
                return CalcAsset();
            }
        }

        private double CalcAsset()
        {
            double asset = money;
            foreach (string code in dic_OrderID_LockMoney.Keys)
            {
                asset += dic_OrderID_LockMoney[code];
            }
            foreach (PositionInfo positionInfo in this.positions)
            {
                double price;
                if (dataForward_Code == null)
                    price = positionInfo.PositionCost;
                else
                {
                    if (positionInfo.InstrumentID.Equals(this.dataForward_Code.Code))
                        price = dataForward_Code.Price;
                    else
                        price = dataForward_Code.GetAttachedDataReader(positionInfo.InstrumentID).Price;
                }
                asset += CalcTradeMoney_Close(positionInfo, price, positionInfo.Position);
            }
            return asset;
        }

        public double Time
        {
            get
            {
                return dataForward_Code == null ? 0 : dataForward_Code.Time;
                //return realTimeDataReader.Time;
            }
        }

        private bool CanOpen(string code)
        {
            if (accountSetting.AutoFilter)
            {
                if (this.CurrentOrderInfo.Count > 0 || this.positions.Count > 0)
                    return false;
            }
            //if (this.GetPosition(code, OrderSide.Buy) > 0 || this.GetPosition(code, OrderSide.Sell) > 0)
            //    return false;
            return true;
        }

        public void Open(string code, double price, OrderSide orderSide, int mount)
        {
            if (!CanOpen(code))
                return;

            OrderInfo orderInfo;
            lock (lockObj)
            {
                orderInfo = OpenInternal(code, price, orderSide, mount);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        public void OpenPercent(string code, double price, OrderSide orderSide, float percent)
        {
            if (!CanOpen(code))
                return;
            OrderInfo orderInfo;
            lock (lockObj)
            {
                int maxCanBuy = GetMaxCanBuy(code, price, percent);
                orderInfo = OpenInternal(code, price, orderSide, maxCanBuy);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        public void OpenAll(string code, double price, OrderSide orderSide)
        {
            if (!CanOpen(code))
                return;
            OrderInfo orderInfo;
            lock (lockObj)
            {
                int maxCanBuy = GetMaxCanBuy(code, price, 100);
                orderInfo = OpenInternal(code, price, orderSide, maxCanBuy);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        private OrderInfo OpenInternal(string code, double price, OrderSide orderSide, int mount)
        {
            if (mount <= 0)
                return null;
            OrderInfo orderInfo = new OrderInfo(code, this.Time, OpenCloseType.Open, price, mount, orderSide, OrderType.Market);
            orderInfo.OrderID = Guid.NewGuid().ToString();
            double tradePrice = GetTradePrice(code, orderSide, price);
            double buymoney = CalcTradeMoney_Open(code, tradePrice, mount, OpenCloseType.Open);
            if (buymoney > money)
                return null;
            if (AccountSetting.TradeType == AccountTradeType.IMMEDIATELY)
            {
                this.historyOrders.Add(orderInfo);
                this.DoTrade(orderInfo);
                this.money -= buymoney;
            }
            else
            {
                if (AccountSetting.TradeType == AccountTradeType.DELAYTICK)
                {
                    ITickData tickData = this.dataForward_Code.GetTickData();
                    if (tickData != null)
                    {
                        OrderDelayInfo delayInfo = new OrderDelayInfo();
                        delayInfo.orderTickIndex = tickData.BarPos;
                        this.dic_Code_OrderDelayInfo.Add(orderInfo.OrderID, delayInfo);
                    }
                }
                this.waitingOrders.Add(orderInfo);
                this.historyOrders.Add(orderInfo);
                this.money -= buymoney;
                this.dic_OrderID_LockMoney.Add(orderInfo.OrderID, buymoney);
            }
            return orderInfo;
        }

        //计算买入需要花的钱
        private double CalcTradeMoney_Open(String code, double price, int hand, OpenCloseType openCloseType)
        {
            ITradeFee_Code tradeFee = GetTradeFee_Code(code);
            //交易费用 TODO 按百分比收交易费用
            double handFee = tradeFee.BuyFee;
            return hand * (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) + handFee);
        }

        private double CalcEarnMoney(PositionInfo position, int mount, double price)
        {
            string code = position.InstrumentID;
            double dprice = price - position.PositionCost;
            if (position.Side == PositionSide.Short)
                dprice = -dprice;
            return dprice * mount * GetTradeFee_Code(code).HandCount;
        }

        private double CalcTradeMoney_Close(PositionInfo positionInfo, double price, int mount)
        {
            ITradeFee_Code tradeFee = GetTradeFee_Code(positionInfo.InstrumentID);
            //交易费用
            double handFee = tradeFee.SellFee;
            double earnmoney = CalcEarnMoney(positionInfo, mount, price);
            //return hand * (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) - handFee);
            return mount * (positionInfo.PositionCost * tradeFee.HandCount * (tradeFee.DepositPercent / 100) - handFee) + earnmoney;
        }

        //计算该价格下最大可买数量
        private int GetMaxCanBuy(String code, double price, double percent)
        {
            ITradeFee_Code tradeFee = GetTradeFee_Code(code);
            return (int)(this.money * percent / 100 / (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) + tradeFee.BuyFee));
        }

        public void Close(string code, double price, OrderSide orderSide, int mount)
        {
            OrderInfo orderInfo;
            lock (lockObj)
            {
                orderInfo = CloseInternal(code, price, orderSide, mount);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        public void ClosePercent(string code, double price, OrderSide orderSide, float percent)
        {
            OrderInfo orderInfo;
            lock (lockObj)
            {
                int position = GetPosition(code, orderSide);
                if (position == 0)
                    return;
                orderInfo = CloseInternal(code, price, orderSide, (int)(position * percent / 100));
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        public void CloseAll(string code, double price, OrderSide orderSide)
        {
            OrderInfo orderInfo;
            lock (lockObj)
            {
                int position = GetPosition(code, GetCloseSide(orderSide));
                if (position == 0)
                    return;
                orderInfo = CloseInternal(code, price, orderSide, position);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        private OrderInfo CloseInternal(string code, double price, OrderSide orderSide, int mount)
        {
            if (mount <= 0)
                return null;
            //OrderSide positionSide = orderSide == OrderSide.Buy ? OrderSide.Sell : OrderSide.Buy;
            //int position = GetPosition(code, positionSide);
            //if (position < mount)
            //    return null;
            if (!CanClose(code, orderSide, mount))
                return null;
            OrderInfo orderInfo = new OrderInfo(code, this.Time, OpenCloseType.Close, price, mount, orderSide, OrderType.Market);
            orderInfo.OrderID = Guid.NewGuid().ToString();
            if (AccountSetting.TradeType == AccountTradeType.IMMEDIATELY)
            {
                this.historyOrders.Add(orderInfo);
                DoTrade(orderInfo);
            }
            else
            {
                this.historyOrders.Add(orderInfo);
                this.waitingOrders.Add(orderInfo);
            }
            return orderInfo;
        }

        private bool CanClose(string code, OrderSide orderSide, int mount)
        {
            OrderSide positionSide = GetCloseSide(orderSide);
            int position = GetPosition(code, positionSide);
            if (position < mount)
                return false;
            int orderMount = 0;
            for (int i = 0; i < waitingOrders.Count; i++)
            {
                if (waitingOrders[i].Instrumentid == code && waitingOrders[i].Direction == orderSide)
                    orderMount += waitingOrders[i].LeavesQty;
            }
            if (position - orderMount < mount)
                return false;
            return true;
        }

        private static OrderSide GetCloseSide(OrderSide orderSide)
        {
            return orderSide == OrderSide.Buy ? OrderSide.Sell : OrderSide.Buy;
        }

        private int GetPosition(string code, OrderSide orderSide)
        {
            PositionInfo positionInfo = GetPositionInfo(code, orderSide);
            if (positionInfo == null)
                return 0;
            return positionInfo.Position;
        }

        private PositionInfo GetPositionInfo(String code, OrderSide orderSide)
        {
            Dictionary<string, PositionInfo> dic_position = orderSide == OrderSide.Buy ? mapPosition_Buy : mapPosition_Sell;
            if (!dic_position.ContainsKey(code))
                return null;
            return dic_position[code];
        }

        internal ITradeFee_Code GetTradeFee_Code(String code)
        {
            if (fee == null)
                return defaultFee_Code;
            ITradeFee_Code tradeFee = fee.GetFee(code);
            return tradeFee == null ? defaultFee_Code : tradeFee;
        }

        public void CancelAllOrder()
        {
            OrderInfo[] waitingArr = waitingOrders.ToArray();
            for (int i = 0; i < waitingArr.Length; i++)
            {
                CancelOrder(waitingArr[i].OrderID);
            }
        }

        public void CancelOrder(string orderid)
        {
            for (int i = 0; i < waitingOrders.Count; i++)
            {
                OrderInfo orderInfo = waitingOrders[i];
                if (orderInfo.OrderID == orderid)
                {
                    orderInfo.ExecType = ExecType.Cancelled;
                    double money = this.dic_OrderID_LockMoney[orderid];
                    this.dic_OrderID_LockMoney.Remove(orderid);
                    this.money += money;
                    waitingOrders.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 得到当前委托
        /// </summary>
        public IList<OrderInfo> CurrentOrderInfo
        {
            get
            {
                return waitingOrders;
            }
        }

        public IList<OrderInfo> HistoryOrderInfo
        {
            get
            {
                return this.historyOrders;
            }
        }

        /// <summary>
        /// 当前持仓
        /// </summary>
        public IList<PositionInfo> CurrentPositionInfo
        {
            get
            {
                return positions;
            }
        }

        /// <summary>
        /// 当前交易信息
        /// </summary>
        public IList<TradeInfo> CurrentTradeInfo
        {
            get
            {
                return currentTrades;
            }
        }

        //public List<OrderInfo> getWaitingOrders()
        //{
        //    return Collections.emptyList();
        //}

        //public List<OrderInfo> getAllOrders()
        //{
        //    return Collections.emptyList();
        //}

        //public List<HoldInfo> getAllHoldInfo()
        //{
        //    List<HoldInfo> holds = new ArrayList<HoldInfo>(mapPosition_Buy.size());
        //    for (HoldInfoImpl hold : mapPosition_Buy.values())
        //    {
        //        holds.add(hold);
        //    }
        //    return holds;
        //}

        //private HoldInfoImpl getHoldInfoInternal(String code)
        //{
        //    HoldInfoImpl holdinfo = mapPosition_Buy.get(code);
        //    if (holdinfo == null)
        //    {
        //        holdinfo = new HoldInfoImpl(code, this);
        //        mapPosition_Buy.put(code, holdinfo);
        //    }
        //    return holdinfo;
        //}

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }

        public void Save(XmlElement elem)
        {
            lock (lockObj)
            {
                elem.SetAttribute("initMoney", initMoney.ToString());
                elem.SetAttribute("money", money.ToString());
                elem.SetAttribute("description", description);

                SaveTradeFee(elem);
                SaveOrders(elem);
                SaveTrades(elem);
                SavePositions(elem);

                SaveForwardInfo(elem);
            }
        }

        private void SaveTradeFee(XmlElement elem)
        {
            if (fee == null)
                return;
            XmlElement elemTradeFee = elem.OwnerDocument.CreateElement("tradeFee");
            elem.AppendChild(elemTradeFee);

            List<string> codes = new List<string>();
            codes.Add(dataForward_Code.Code);
            codes.AddRange(this.dataForward_Code.GetAttachedCodes());
            SaveFee(fee, codes, elemTradeFee);
        }

        private void SaveFee(ITradeFee tradeFee, List<string> codes, XmlElement xmlElem)
        {
            foreach (string code in codes)
            {
                XmlElement subElem = xmlElem.OwnerDocument.CreateElement("tradefee");
                xmlElem.AppendChild(subElem);
                ITradeFee_Code tradeFee_Code = tradeFee.GetFee(code);
                tradeFee_Code.Save(subElem);
            }
        }

        private void SaveTrades(XmlElement elem)
        {
            XmlElement elemTrades = elem.OwnerDocument.CreateElement("trades");
            elem.AppendChild(elemTrades);
            foreach (TradeInfo trade in this.currentTrades)
            {
                XmlElement elemOrder = elemTrades.OwnerDocument.CreateElement("trade");
                elemTrades.AppendChild(elemOrder);

                elemOrder.SetAttribute("tradeid", trade.TradeID.ToString());
                elemOrder.SetAttribute("code", trade.InstrumentID);
                elemOrder.SetAttribute("direction", trade.Side.ToString());
                elemOrder.SetAttribute("openclose", trade.OpenClose.ToString());
                elemOrder.SetAttribute("price", trade.Price.ToString());
                elemOrder.SetAttribute("qty", trade.Qty.ToString());
                elemOrder.SetAttribute("time", trade.Time.ToString());
            }
        }

        private void SaveOrders(XmlElement elem)
        {
            XmlElement elemOrders = elem.OwnerDocument.CreateElement("waitingOrders");
            elem.AppendChild(elemOrders);
            foreach (OrderInfo order in this.waitingOrders)
            {
                XmlElement elemOrder = elemOrders.OwnerDocument.CreateElement("order");
                elemOrders.AppendChild(elemOrder);
                SaveOrderInfo(order, elemOrder);
            }
            XmlElement elemHistoryOrders = elem.OwnerDocument.CreateElement("historyOrders");
            elem.AppendChild(elemHistoryOrders);
            foreach (OrderInfo order in this.historyOrders)
            {
                XmlElement elemOrder = elemHistoryOrders.OwnerDocument.CreateElement("order");
                elemHistoryOrders.AppendChild(elemOrder);
                SaveOrderInfo(order, elemOrder);
            }
        }

        private static void SaveOrderInfo(OrderInfo order, XmlElement elemOrder)
        {
            elemOrder.SetAttribute("orderid", order.OrderID.ToString());
            elemOrder.SetAttribute("code", order.Instrumentid);
            elemOrder.SetAttribute("direction", order.Direction.ToString());
            elemOrder.SetAttribute("openclose", order.OpenClose.ToString());
            elemOrder.SetAttribute("price", order.Price.ToString());
            elemOrder.SetAttribute("volume", order.Volume.ToString());
            elemOrder.SetAttribute("ordertime", order.OrderTime.ToString());
            elemOrder.SetAttribute("exectype", order.ExecType.ToString());
            elemOrder.SetAttribute("leavesqty", order.LeavesQty.ToString());
            elemOrder.SetAttribute("cumqty", order.CumQty.ToString());
        }

        private void SavePositions(XmlElement elem)
        {
            XmlElement elemPositions = elem.OwnerDocument.CreateElement("positions");
            elem.AppendChild(elemPositions);
            foreach (PositionInfo position in this.positions)
            {
                XmlElement elemOrder = elemPositions.OwnerDocument.CreateElement("position");
                elemPositions.AppendChild(elemOrder);

                elemOrder.SetAttribute("code", position.InstrumentID);
                elemOrder.SetAttribute("side", position.Side.ToString());
                elemOrder.SetAttribute("position", position.Position.ToString());
                elemOrder.SetAttribute("positioncost", position.PositionCost.ToString());
            }
        }

        private void SaveForwardInfo(XmlElement elem)
        {
            if (this.dataForward_Code != null)
            {
                XmlElement elemForwardInfo = elem.OwnerDocument.CreateElement("forwardInfo");
                elem.AppendChild(elemForwardInfo);
                dataForward_Code.Save(elemForwardInfo);
            }
        }

        public void Load(XmlElement elem)
        {
            this.initMoney = double.Parse(elem.GetAttribute("initMoney"));
            this.money = double.Parse(elem.GetAttribute("money"));
            this.description = elem.GetAttribute("description");

            this.LoadFee(elem);
            this.LoadOrders(elem);
            this.LoadTrades(elem);
            this.LoadPositions(elem);
            this.LoadForwardInfo(elem);
        }

        private void LoadFee(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("tradeFee");
            if (nodes.Count == 0)
                return;
            XmlElement elemFee = (XmlElement)nodes[0];
            this.fee = new TradeFee();
            fee.Load(elemFee);
        }

        private void LoadOrders(XmlElement xmlElem)
        {
            XmlElement xmlElemWaitingOrders = (XmlElement)xmlElem.GetElementsByTagName("waitingOrders")[0];
            XmlNodeList xmlNodes = xmlElemWaitingOrders.GetElementsByTagName("order");
            for (int i = 0; i < xmlNodes.Count; i++)
            {
                XmlElement xmlElemOrder = (XmlElement)xmlNodes[i];
                OrderInfo orderInfo = LoadOrderInfo(xmlElemOrder);
                this.waitingOrders.Add(orderInfo);
            }

            XmlElement xmlElemHistoryOrders = (XmlElement)xmlElem.GetElementsByTagName("historyOrders")[0];
            xmlNodes = xmlElemHistoryOrders.GetElementsByTagName("order");
            for (int i = 0; i < xmlNodes.Count; i++)
            {
                XmlElement xmlElemOrder = (XmlElement)xmlNodes[i];
                OrderInfo orderInfo = LoadOrderInfo(xmlElemOrder);
                this.historyOrders.Add(orderInfo);
            }
        }

        private static OrderInfo LoadOrderInfo(XmlElement elemOrder)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.OrderID = elemOrder.GetAttribute("orderid");
            orderInfo.Instrumentid = elemOrder.GetAttribute("code");
            orderInfo.Direction = (OrderSide)Enum.Parse(typeof(OrderSide), elemOrder.GetAttribute("direction"));
            orderInfo.OpenClose = (OpenCloseType)Enum.Parse(typeof(OpenCloseType), elemOrder.GetAttribute("openclose"));
            orderInfo.Price = double.Parse(elemOrder.GetAttribute("price"));
            orderInfo.Volume = int.Parse(elemOrder.GetAttribute("volume"));
            orderInfo.OrderTime = double.Parse(elemOrder.GetAttribute("ordertime"));
            orderInfo.ExecType = (ExecType)Enum.Parse(typeof(ExecType), elemOrder.GetAttribute("exectype"));
            orderInfo.LeavesQty = int.Parse(elemOrder.GetAttribute("leavesqty"));
            orderInfo.CumQty = int.Parse(elemOrder.GetAttribute("cumqty"));
            return orderInfo;
        }

        private void LoadTrades(XmlElement xmlElem)
        {
            XmlElement xmlElemWaitingOrders = (XmlElement)xmlElem.GetElementsByTagName("trades")[0];
            XmlNodeList xmlNodes = xmlElemWaitingOrders.GetElementsByTagName("trade");

            for (int i = 0; i < xmlNodes.Count; i++)
            {
                XmlElement xmlElemTrade = (XmlElement)xmlNodes[i];
                TradeInfo tradeInfo = new TradeInfo();
                tradeInfo.TradeID = xmlElemTrade.GetAttribute("tradeid");
                tradeInfo.InstrumentID = xmlElemTrade.GetAttribute("code");
                tradeInfo.Side = (OrderSide)Enum.Parse(typeof(OrderSide), xmlElemTrade.GetAttribute("direction"));
                tradeInfo.OpenClose = (OpenCloseType)Enum.Parse(typeof(OpenCloseType), xmlElemTrade.GetAttribute("openclose"));
                tradeInfo.Price = double.Parse(xmlElemTrade.GetAttribute("price"));
                tradeInfo.Qty = double.Parse(xmlElemTrade.GetAttribute("qty"));
                tradeInfo.Time = double.Parse(xmlElemTrade.GetAttribute("time"));
                this.currentTrades.Add(tradeInfo);
            }
        }

        private void LoadPositions(XmlElement xmlElem)
        {
            XmlElement xmlElemWaitingOrders = (XmlElement)xmlElem.GetElementsByTagName("positions")[0];
            XmlNodeList xmlNodes = xmlElemWaitingOrders.GetElementsByTagName("position");

            for (int i = 0; i < xmlNodes.Count; i++)
            {
                XmlElement xmlElemTrade = (XmlElement)xmlNodes[i];
                PositionInfo positionInfo = new PositionInfo();
                positionInfo.InstrumentID = xmlElemTrade.GetAttribute("code");
                positionInfo.Side = (PositionSide)Enum.Parse(typeof(PositionSide), xmlElemTrade.GetAttribute("side"));
                positionInfo.Position = int.Parse(xmlElemTrade.GetAttribute("position"));
                positionInfo.PositionCost = double.Parse(xmlElemTrade.GetAttribute("positioncost"));
                this.positions.Add(positionInfo);
            }
        }

        private void LoadForwardInfo(XmlElement xmlElem)
        {
            XmlNodeList nodes = xmlElem.GetElementsByTagName("forwardInfo");
            if (nodes.Count == 0)
                return;
            this.dataForward_Code = dataCenter.HistoryDataForwardFactory.CreateDataForward_Code((XmlElement)nodes[0]);
        }

        /// <summary>
        /// 委托成功事件
        /// </summary>
        public event DelegateOnReturnOrder OnReturnOrder;

        /// <summary>
        /// 成交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="trade"></param>
        public event DelegateOnReturnTrade OnReturnTrade;
    }

    class OrderDelayInfo
    {
        public int orderTickIndex;
    }
}