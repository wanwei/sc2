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
        //private AccountOrders accountOrders;
        //internal AccountOrders AccountOrders
        //{
        //    get { return accountOrders; }
        //}

        //private AccountTrades accountTrades;
        //internal AccountTrades AccountTrades
        //{
        //    get { return accountTrades; }
        //}

        //private AccountPosition accountPosition;
        //internal AccountPosition AccountPosition
        //{
        //    get { return accountPosition; }
        //}
        public static TradeFee_Code DEFAULTFEECODE = new TradeFee_Code(null, 10, 1, 3, 3, true, 15);
        //public static TradeFee_Code DEFAULTFEECODE = new TradeFee_Code(null, 100, 0.01, 0.03, 0.03, true, 100);

        private double initMoney;

        internal double money;

        private List<PositionInfo> positions = new List<PositionInfo>();

        protected Dictionary<String, PositionInfo> mapPosition_Buy = new Dictionary<String, PositionInfo>();

        protected Dictionary<String, PositionInfo> mapPosition_Sell = new Dictionary<String, PositionInfo>();

        private List<TradeInfo> historyTrades = new List<TradeInfo>();

        private List<TradeInfo> currentTrades = new List<TradeInfo>();

        //所有的委托
        private List<OrderInfo> historyOrders = new List<OrderInfo>();

        //还没执行完成的委托
        private List<OrderInfo> waitingOrders = new List<OrderInfo>();

        private TradeFee fee;

        private TradeFee_Code defaultFee_Code = Account.DEFAULTFEECODE;

        private String description = null;

        private IRealTimeDataReader realTimeDataReader;

        private object lockObj = new object();

        public Account(double money, IRealTimeDataReader realTimeDataReader) : this(money, realTimeDataReader, null)
        {

        }

        public Account(double money, IRealTimeDataReader realTimeDataReader, TradeFee fee)
        {
            //this.accountOrders = new AccountOrders(this);
            //this.accountTrades = new AccountTrades(this);
            //this.accountPosition = new AccountPosition(this);
            this.money = money;
            this.initMoney = money;
            this.fee = fee;
            this.realTimeDataReader = realTimeDataReader;
            this.realTimeDataReader.OnRealTimeChanged += RealTimeDataReader_RealTimeChanged;
        }

        #region 交易

        private void RealTimeDataReader_RealTimeChanged(object sender, RealTimeChangedArgument e)
        {
            lock (lockObj)
            {
                //过了新的一天，昨日的委托需要全部清除
                if (e.TradingDayChanged)
                {
                    DoTradingDayChange();
                    return;
                }

                //TODO 价格变化后需要修改现金余额

                for (int i = 0; i < waitingOrders.Count; i++)
                {
                    OrderInfo order = waitingOrders[i];
                    bool isAllTrade = DoTrade(order);
                    if (isAllTrade)
                    {
                        this.waitingOrders.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void DoTradingDayChange()
        {
            //accountOrders.DoTradingDayChange();
            //accountTrades.DoTradingDayChange();
            //accountPosition.DoTradingDayChange();
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
            int tradeMount = GetTradeMount(orderInfo);
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
            tradeInfo.Price = orderInfo.Price;
            tradeInfo.Qty = tradeMount;
            tradeInfo.Side = orderInfo.Direction;
            tradeInfo.Time = Time;
            tradeInfo.TradeID = Guid.NewGuid().ToString();
            this.currentTrades.Add(tradeInfo);
            return tradeInfo;
        }

        private int GetTradeMount(OrderInfo orderInfo)
        {
            double orderPrice = orderInfo.Price;
            ITickData tickData = realTimeDataReader.GetTickData();
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
                double price = realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
                if (orderInfo.Direction == OrderSide.Buy)
                    return orderInfo.Price >= price ? orderInfo.Volume : 0;
                else
                    return orderInfo.Price <= price ? orderInfo.Volume : 0;
            }
        }

        //public float BuyPrice
        //{
        //    get
        //    {
        //        ITickData tickData = realTimeDataReader.GetTickData();
        //        if (tickData != null)
        //            return tickData.BuyPrice;
        //        return realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
        //    }
        //}

        //public int BuyMount
        //{
        //    get
        //    {
        //        ITickData tickData = realTimeDataReader.GetTickData();
        //        if (tickData != null)
        //            return tickData.BuyMount;
        //        return int.MaxValue;
        //    }
        //}

        //public float SellPrice
        //{
        //    get
        //    {
        //        ITickData tickData = realTimeDataReader.GetTickData();
        //        if (tickData != null)
        //            return tickData.SellPrice;
        //        return realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).End;
        //    }
        //}

        //public int SellMount
        //{
        //    get
        //    {
        //        ITickData tickData = realTimeDataReader.GetTickData();
        //        if (tickData != null)
        //            return tickData.SellMount;
        //        return int.MaxValue;
        //    }
        //}

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
            int tradeMount = GetTradeMount(orderInfo);
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
            positionInfo.PositionCost = (currentMount * positionInfo.PositionCost - tradeInfo.Price * tradeMount) / positionInfo.Position;
        }

        #endregion

        public TradeFee Fee
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
                return money;
            }
        }

        public double Time
        {
            get
            {
                return realTimeDataReader.Time;
            }
        }

        public void Open(string code, double price, OrderSide orderSide, int mount)
        {
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
            OrderInfo orderInfo;
            lock (lockObj)
            {
                int maxCanBuy = GetMaxCanBuy(code, price, 100);
                orderInfo = OpenInternal(code, price, orderSide, maxCanBuy);
            }
            if (orderInfo != null)
                OnReturnOrder?.Invoke(this, ref orderInfo);
        }

        private Dictionary<string, double> dic_OrderID_LockMoney = new Dictionary<string, double>();

        private OrderInfo OpenInternal(string code, double price, OrderSide orderSide, int mount)
        {
            if (mount <= 0)
                return null;
            double buymoney = CalcTradeMoney_Open(code, price, mount, OpenCloseType.Open);
            if (buymoney > money)
                return null;
            OrderInfo orderInfo = new OrderInfo(code, this.Time, OpenCloseType.Open, price, mount, orderSide, OrderType.Market);
            orderInfo.OrderID = Guid.NewGuid().ToString();
            this.waitingOrders.Add(orderInfo);
            this.historyOrders.Add(orderInfo);
            this.money -= buymoney;
            this.dic_OrderID_LockMoney.Add(orderInfo.OrderID, buymoney);
            return orderInfo;
        }

        //计算买入需要花的钱
        private double CalcTradeMoney_Open(String code, double price, int hand, OpenCloseType openCloseType)
        {
            TradeFee_Code tradeFee = GetTradeFee_Code(code);
            //交易费用 TODO 按百分比收交易费用
            double handFee = tradeFee.BuyFee;
            return hand * (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) + handFee);
        }

        private double CalcEarnMoney(PositionInfo position, double price)
        {
            string code = position.InstrumentID;
            double dprice = price - position.PositionCost;
            if (position.Side == PositionSide.Short)
                dprice = -dprice;
            return dprice * position.Position * GetTradeFee_Code(code).HandCount;
        }

        private double CalcTradeMoney_Close(PositionInfo positionInfo, double price, int mount)
        {
            TradeFee_Code tradeFee = GetTradeFee_Code(positionInfo.InstrumentID);
            //交易费用
            double handFee = tradeFee.SellFee;
            double earnmoney = CalcEarnMoney(positionInfo, price);
            //return hand * (price * tradeFee.HandCount * (tradeFee.DepositPercent / 100) - handFee);
            return mount * (positionInfo.PositionCost * tradeFee.HandCount * (tradeFee.DepositPercent / 100) - handFee) + earnmoney;
        }

        //计算该价格下最大可买数量
        private int GetMaxCanBuy(String code, double price, double percent)
        {
            TradeFee_Code tradeFee = GetTradeFee_Code(code);
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
                int position = GetPosition(code, orderSide);
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
            this.waitingOrders.Add(orderInfo);
            return orderInfo;
        }

        private bool CanClose(string code, OrderSide orderSide, int mount)
        {
            OrderSide positionSide = orderSide == OrderSide.Buy ? OrderSide.Sell : OrderSide.Buy;
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

        internal TradeFee_Code GetTradeFee_Code(String code)
        {
            if (fee == null)
                return defaultFee_Code;
            TradeFee_Code tradeFee = fee.getFee(code);
            return tradeFee == null ? defaultFee_Code : tradeFee;
        }

        //计算
        private double calcCloseMoney(String code, double price, int hand)
        {
            //PositionInfo hold = mapPosition_Buy.get(code);
            //TradeFee_Code fee = GetTradeFee_Code(code);
            //return hold.PositionCost * hand * fee.HandCount * fee.getDepositPercent() / 100
            //        + calcEarnMoney(code, price, hand) - hand * fee.getSellFee();
            return 0;
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
                return historyOrders;
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
                return historyTrades;
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

                SaveOrders(elem);
                SaveTrades(elem);
                SavePositions(elem);
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
            XmlElement elemOrders = elem.OwnerDocument.CreateElement("orders");
            elem.AppendChild(elemOrders);
            foreach (OrderInfo order in this.waitingOrders)
            {
                XmlElement elemOrder = elemOrders.OwnerDocument.CreateElement("order");
                elemOrders.AppendChild(elemOrder);

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
        }

        private void SavePositions(XmlElement elem)
        {
            XmlElement elemPositions = elem.OwnerDocument.CreateElement("positions");
            elem.AppendChild(elemPositions);
            foreach (PositionInfo position in this.positions)
            {
                XmlElement elemOrder = elemPositions.OwnerDocument.CreateElement("order");
                elemPositions.AppendChild(elemOrder);

                elemOrder.SetAttribute("code", position.InstrumentID);
                elemOrder.SetAttribute("side", position.Side.ToString());
                elemOrder.SetAttribute("position", position.Position.ToString());
                elemOrder.SetAttribute("positioncost", position.PositionCost.ToString());
            }
        }

        public void Load(XmlElement elem)
        {
            //this.initMoney = NumberUtils.toDouble(elem.attributeValue("initMoney"), 0);
            //this.money = NumberUtils.toDouble(elem.attributeValue("money"), 0);
            //this.asset = NumberUtils.toDouble(elem.attributeValue("asset"), 0);
            //this.startTime = TimeFactory.getTimeObj(elem.attributeValue("startTime"));
            //this.currentTime = TimeFactory.getTimeObj(elem.attributeValue("currentTime"));
            //this.description = elem.attributeValue("description");
            //this.market = elem.attributeValue("market");
            //this.targetCode = elem.attributeValue("targetCode");


            //Element elemHolds = elem.element("holds");
            //for (Object obj : elemHolds.elements("hold"))
            //{
            //    Element elemHold = (Element)obj;
            //    HoldInfoImpl hold = new HoldInfoImpl(this);
            //    hold.loadFromXml(elemHold);
            //    this.mapPosition_Buy.put(hold.getCode(), hold);
            //}

            //Element elemOrders = elem.element("orders");
            //if (elemOrders != null)
            //{
            //    List <?> elems = elemOrders.elements("order");
            //    for (Object obj : elems)
            //    {
            //        Element elemOrder = (Element)obj;
            //        OrderInfo order = OrderInfoImpl.createOrderInfo(this, elemOrder);
            //        this.orders.add(order);
            //        if (!order.isFinished())
            //            this.waitingOrders.add(order);
            //    }
            //}

            //Element elemHistory = elem.element("trades");
            //if (elemHistory != null)
            //{
            //    for (Object obj : elemHistory.elements("trade"))
            //    {
            //        Element elemTrade = (Element)obj;
            //        TradeDetailImpl trade = new TradeDetailImpl();
            //        trade.loadFromXml(elemTrade);
            //        this.historyTrades.add(trade);
            //    }
            //}
        }

        //public List<TradeDetail> getTodayTrades()
        //{
        //    return null;
        //}

        //public List<TradeDetail> getAllTrades()
        //{
        //    return this.historyTrades;
        //}


        //public HistoryTradeAnalyser getHistoryAnalyser()
        //    {
        //        return new HistoryTradeAnalyserImpl(historyTrades);
        //    }

        //public static Account createAccount(XmlElement elem)
        //{
        //    Account account = new Account();
        //    account.loadFromXml(elem);
        //    return account;
        //}

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
}