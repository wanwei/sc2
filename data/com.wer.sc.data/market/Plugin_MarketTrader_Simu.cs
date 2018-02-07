using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data.market;
using com.wer.sc.data.account;
using com.wer.sc.utils;
using com.wer.sc.data.reader;

namespace com.wer.sc.data.market
{
    public class Plugin_MarketTrader_Simu : IPlugin_MarketTrader
    {
        /// <summary>
        /// 新建账号，传入true，false
        /// </summary>
        public const string KEY_ISNEW = "ISNEW";

        /// <summary>
        /// 新建账号的路径
        /// </summary>
        public const string KEY_PATH = "PATH";

        /// <summary>
        /// 账号名称，如test，mock等
        /// </summary>
        public const string KEY_NAME = "NAME";

        /// <summary>
        /// 账号初始资金，传入double如 100000
        /// </summary>
        public const string KEY_MONEY = "MONEY";

        /// <summary>
        /// 交易历史数据获取器，必须传入IRealTimeDataReader的实现
        /// </summary>
        public const string KEY_DATAREADER = "DATAREADER";

        /// <summary>
        /// 自动过滤掉交易单，默认false
        /// TODO
        /// </summary>
        public const string KEY_AUTOFILTER = "AUTOFILTER";

        /// <summary>
        /// 交易方式：
        /// 0，立即成交（不管市价多少都立刻成交）
        /// 1，以当前市价成交，下单即开始
        /// 2，以当前市价延迟一定时间成交
        /// 3，以当前市价延迟一定数量tick成交
        /// </summary>
        public const string KEY_TRADETYPE = "TRADETYPE";

        /// <summary>
        /// 延时成交，如0.000001表示延时一秒
        /// </summary>
        public const string KEY_DELAYTIME = "DELAYTIME";

        /// <summary>
        /// 延迟tick成交，用整数
        /// </summary>
        public const string KEY_DELAYTICK = "DELAYTICK";

        /// <summary>
        /// 滑点方式
        /// 0，不滑点
        /// 1，按最小价格滑点，股票最小价格变动0.01，那滑点为5，表示滑点0.05
        /// 2，按百分比滑点，传入如0.1，表示滑点0.1%
        /// 3，按绝对价格滑点
        /// </summary>
        public const string KEY_SLIPTYPE = "SLIPTYPE";

        /// <summary>
        /// 按最小价格滑点成交
        /// </summary>
        public const string KEY_SLIPMINPRICE = "SLIPMINPRICE";

        /// <summary>
        /// 按百分比滑点成交
        /// </summary>
        public const string KEY_SLIPPERCENT = "SLIPPERCENT";

        /// <summary>
        /// 按绝对价格滑点成交
        /// </summary>
        public const string KEY_SLIPPRICE = "SLIPPRICE";

        private IAccount account;

        private IAccountManager accountManager;

        private string path;

        private string accountName;

        public Plugin_MarketTrader_Simu()
        {
        }

        public Plugin_MarketTrader_Simu(IAccount account)
        {
            this.account = account;
            this.account.OnReturnOrder += Account_OnReturnOrder;
            this.account.OnReturnTrade += Account_OnReturnTrade;
        }

        public Plugin_MarketTrader_Simu(IAccount account, IAccountManager accountManager, string path, string accountName) : this(account)
        {
            this.accountManager = accountManager;
            this.path = path;
            this.accountName = accountName;
        }

        private void Account_OnReturnOrder(object sender, ref OrderInfo order)
        {
            if (this.onReturnOrder != null)
                this.onReturnOrder(this, ref order);
            if (this.accountManager != null)
                this.accountManager.Save(path, accountName, account, true);
        }

        private void Account_OnReturnTrade(object sender, ref TradeInfo trade)
        {
            if (this.onReturnTrade != null)
                this.onReturnTrade(this, ref trade);
            if (this.accountManager != null)
                this.accountManager.Save(path, accountName, account, true);
        }

        public string SendOrder(OrderInfo order)
        {
            if(order.OpenClose == OpenCloseType.Open)
                this.account.Open(order.Instrumentid, order.Price, order.Direction, order.Volume);
            else
                this.account.Close(order.Instrumentid, order.Price, order.Direction, order.Volume);
            return "";
        }

        public string CancelOrder(string orderid)
        {
            this.account.CancelOrder(orderid);
            return "";
        }

        /// <summary>
        /// connectionInfo:
        /// 
        /// 新建一个账户
        /// isnew : true
        /// id : testmock
        /// money : 100000   (初始资金)        
        /// datareader : (实时数据读取器，IRealTimeDataReader的实现接口)
        /// TradeType : 交易方式：0，立即成交；1，市价成交；2、市价延迟时间成交；3、市价延迟tick成交
        /// 
        /// isnew = false
        /// id:testmock
        /// 
        /// </summary>
        /// <param name="connectionInfo"></param>
        public void Connect(ConnectionInfo connectionInfo)
        {
            if (this.account != null)
            {
                this.account.UnBind();
                this.account = null;
            }

            Object objIsNew = connectionInfo.GetValue(KEY_ISNEW);

            bool isNew = BooleanUtils.Parse(objIsNew);
            if (isNew)
            {
                double money = double.Parse(connectionInfo.GetValue(KEY_MONEY).ToString());
                this.account = DataCenter.Default.AccountManager.CreateAccount(money);
                this.account.AccountSetting.AutoFilter = BooleanUtils.Parse(connectionInfo.GetValue(KEY_AUTOFILTER));
                this.account.AccountSetting.TradeType = (AccountTradeType)EnumUtils.Parse(typeof(AccountTradeType), connectionInfo.GetValue(KEY_TRADETYPE));
                this.account.AccountSetting.DelayTime = NumberUtils.ParseInt(connectionInfo.GetValue(KEY_DELAYTIME));
                this.account.AccountSetting.DelayTick = NumberUtils.ParseInt(connectionInfo.GetValue(KEY_DELAYTICK));

                this.account.AccountSetting.SlipType = (AccountSlipType)EnumUtils.Parse(typeof(AccountTradeType), connectionInfo.GetValue(KEY_SLIPTYPE));
                this.account.AccountSetting.SlipMinPrice = NumberUtils.ParseInt(connectionInfo.GetValue(KEY_SLIPMINPRICE));
                this.account.AccountSetting.SlipPerccent = NumberUtils.ParseInt(connectionInfo.GetValue(KEY_SLIPPERCENT));
                this.account.AccountSetting.SlipPrice = NumberUtils.ParseInt(connectionInfo.GetValue(KEY_SLIPPRICE));
                this.account.BindRealTimeReader((IRealTimeDataReader)connectionInfo.GetValue(KEY_DATAREADER));

                string path = connectionInfo.GetValue(KEY_PATH).ToString();
                string accountName = connectionInfo.GetValue(KEY_NAME).ToString();
                DataCenter.Default.AccountManager.Save(path, accountName, account);
            }
            else
            {
                string path = connectionInfo.GetValue(KEY_PATH).ToString();
                string accountName = connectionInfo.GetValue(KEY_NAME).ToString();
                this.account = DataCenter.Default.AccountManager.Load(path, accountName);
                this.account.BindRealTimeReader((IRealTimeDataReader)connectionInfo.GetValue(KEY_DATAREADER));
            }

            this.account.OnReturnOrder += Account_OnReturnOrder;
            this.account.OnReturnTrade += Account_OnReturnTrade;
        }

        public void DisConnect()
        {
            this.account.OnReturnOrder -= Account_OnReturnOrder;
            this.account.OnReturnTrade -= Account_OnReturnTrade;
            this.account.UnBind();
            this.account = null;
        }

        public List<ConnectionInfo> GetAllConnections()
        {
            return null;
        }

        public void QueryAccount()
        {

        }

        public void QueryInstruments(string[] instruments = null)
        {

        }

        public void QueryPosition()
        {
            if (onReturnInvestorPosition != null)
            {
                IList<PositionInfo> positions = this.account.CurrentPositionInfo;
                onReturnInvestorPosition(this, ref positions);
            }
        }

        public void QueryOrders()
        {
            if (onRspOrder != null)
            {
                IList<OrderInfo> orders = account.CurrentOrderInfo;
                onRspOrder(this, ref orders);
            }
        }

        public void QueryTrades()
        {
            if (onRspTrade != null)
            {
                IList<TradeInfo> trades = account.CurrentTradeInfo;
                onRspTrade(this, ref trades);
            }
        }

        private DelegateOnConnectionStatus onConnectionStatus;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                this.onConnectionStatus = value;
            }
        }

        private DelegateOnReturnAccount onReturnAccount;

        public DelegateOnReturnAccount OnReturnAccount
        {
            get
            {
                return onReturnAccount;
            }

            set
            {
                this.onReturnAccount = value;
            }
        }

        private DelegateOnReturnInstrument onReturnInstruments;

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstruments;
            }

            set
            {
                this.onReturnInstruments = value;
            }
        }

        private DelegateOnRspInvestorPosition onReturnInvestorPosition;

        public DelegateOnRspInvestorPosition OnRspInvestorPosition
        {
            get
            {
                return onReturnInvestorPosition;
            }

            set
            {
                this.onReturnInvestorPosition = value;
            }
        }

        private DelegateOnReturnOrder onReturnOrder;

        public DelegateOnReturnOrder OnReturnOrder
        {
            get
            {
                return onReturnOrder;
            }

            set
            {
                this.onReturnOrder = value;
            }
        }

        private DelegateOnReturnTrade onReturnTrade;

        public DelegateOnReturnTrade OnReturnTrade
        {
            get
            {
                return onReturnTrade;
            }

            set
            {
                this.onReturnTrade = value;
            }
        }

        private DelegateOnRspOrder onRspOrder;

        public DelegateOnRspOrder OnRspOrder
        {
            get
            {
                return onRspOrder;
            }

            set
            {
                onRspOrder = value;
            }
        }

        private DelegateOnRspTrade onRspTrade;

        public DelegateOnRspTrade OnRspTrade
        {
            get
            {
                return onRspTrade;
            }

            set
            {
                onRspTrade = value;
            }
        }
    }
}