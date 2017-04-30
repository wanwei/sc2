using com.wer.sc.data;
using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui
{
    public partial class FormMarket : Form
    {
        private IPluginMgr pluginMgr;

        private List<PluginInfo> plugins;

        private List<ConnectionInfo> currentConnections;

        private IPlugin_Market currentMarket;

        private DataTable dtSubscribe = new DataTable();

        private DataTable dtPosition = new DataTable();

        private DataTable dtOrder = new DataTable();

        private DataTable dtTrade = new DataTable();

        public FormMarket()
        {
            InitializeComponent();
            this.pluginMgr = PluginMgrFactory.DefaultPluginMgr;
            //MessageBox.Show(pluginMgr.GetAllPlugins().Count.ToString());
            this.plugins = pluginMgr.GetPlugins(typeof(IPlugin_Market));
            InitMarkets();
            InitSubscribeGrid();
            InitPositionGrid();
            InitOrderGrid();
            InitTradeGrid();
        }

        private void InitMarkets()
        {            
            for (int i = 0; i < plugins.Count; i++)
            {
                PluginInfo plugin = plugins[i];
                cbMarket.Items.Add(plugin.PluginName);
            }
            cbMarket.SelectedIndex = 0;
        }

        private void InitSubscribeGrid()
        {
            dtSubscribe.Columns.Add("代码");
            dtSubscribe.Columns.Add("价格");
            dtSubscribe.Columns.Add("买价");
            dtSubscribe.Columns.Add("卖价");
            dtSubscribe.Columns.Add("买量");
            dtSubscribe.Columns.Add("卖量");
            dtSubscribe.Columns.Add("成交量");
            dtSubscribe.Columns.Add("持仓量");
            gridSubscribe.DataSource = dtSubscribe;
        }

        private void InitPositionGrid()
        {
            dtPosition.Columns.Add("代码");
            dtPosition.Columns.Add("多空");
            dtPosition.Columns.Add("总仓");
            dtPosition.Columns.Add("可用");
            dtPosition.Columns.Add("今仓");
            dtPosition.Columns.Add("今可用");
            dtPosition.Columns.Add("开仓均价");
            dtPosition.Columns.Add("逐笔浮盈");
            gridPosition.DataSource = dtPosition;
        }

        private void InitOrderGrid()
        {
            dtOrder.Columns.Add("代码");
            dtOrder.Columns.Add("多空");
            dtOrder.Columns.Add("总仓");
            dtOrder.Columns.Add("可用");
            dtOrder.Columns.Add("今仓");
            dtOrder.Columns.Add("今可用");
            dtOrder.Columns.Add("开仓均价");
            dtOrder.Columns.Add("逐笔浮盈");
            gridOrder.DataSource = dtOrder;
        }

        private void InitTradeGrid()
        {
            dtTrade.Columns.Add("代码");
            dtTrade.Columns.Add("多空");
            dtTrade.Columns.Add("总仓");
            dtTrade.Columns.Add("可用");
            dtTrade.Columns.Add("今仓");
            dtTrade.Columns.Add("今可用");
            dtTrade.Columns.Add("开仓均价");
            dtTrade.Columns.Add("逐笔浮盈");
            gridTrade.DataSource = dtTrade;
        }

        private void InitAccountGrid()
        {
            dtPosition.Columns.Add("代码");
            dtPosition.Columns.Add("多空");
            dtPosition.Columns.Add("总仓");
            dtPosition.Columns.Add("可用");
            dtPosition.Columns.Add("今仓");
            dtPosition.Columns.Add("今可用");
            dtPosition.Columns.Add("开仓均价");
            dtPosition.Columns.Add("逐笔浮盈");
            gridPosition.DataSource = dtPosition;
        }

        private void InitInstrumentGrid()
        {
            dtPosition.Columns.Add("代码");
            dtPosition.Columns.Add("多空");
            dtPosition.Columns.Add("总仓");
            dtPosition.Columns.Add("可用");
            dtPosition.Columns.Add("今仓");
            dtPosition.Columns.Add("今可用");
            dtPosition.Columns.Add("开仓均价");
            dtPosition.Columns.Add("逐笔浮盈");
            gridPosition.DataSource = dtPosition;
        }


        private void cbMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMarket();
            cbConnections.Items.Clear();
            this.currentConnections = GetCurrentConnections();
            for (int i = 0; i < currentConnections.Count; i++)
            {
                ConnectionInfo connection = currentConnections[i];
                cbConnections.Items.Add(connection.Name);
            }
            if (currentConnections.Count > 0)
                cbConnections.SelectedIndex = 0;
        }

        private void ChangeMarket()
        {
            PluginInfo selectedPlugin = plugins[cbMarket.SelectedIndex];
            this.currentMarket = pluginMgr.CreatePluginObject<IPlugin_Market>(selectedPlugin);
        }

        private List<ConnectionInfo> GetCurrentConnections()
        {
            if (this.currentMarket == null)
            {
                PluginInfo selectedPlugin = plugins[cbMarket.SelectedIndex];
                this.currentMarket = pluginMgr.CreatePluginObject<IPlugin_Market>(selectedPlugin);
            }
            return this.currentMarket.MarketData.GetAllConnections();
        }

        private ConnectionInfo GetCurrentConnection()
        {
            if (this.currentConnections == null || this.currentConnections.Count == 0)
                return null;
            int index = this.cbConnections.SelectedIndex;
            return this.currentConnections[index];
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            ConnectionInfo connection = GetCurrentConnection();
            if (connection == null)
            {
                MessageBox.Show("没有合适的连接");
                return;
            }
            this.cbMarket.Enabled = false;
            this.cbConnections.Enabled = false;
            DisConnect();
            PluginInfo selectedPlugin = plugins[cbMarket.SelectedIndex];
            this.currentMarket = pluginMgr.CreatePluginObject<IPlugin_Market>(selectedPlugin);
            this.currentMarket.MarketData.OnConnectionStatus = OnConnectionStatus_Quote;
            this.currentMarket.MarketData.OnReturnMarketData = OnReturnMarketData;

            this.currentMarket.MarketTrader.OnConnectionStatus = OnConnectionStatus_Trade;
            this.currentMarket.MarketTrader.OnReturnInstruments = OnReturnInstrument;
            this.currentMarket.MarketTrader.OnReturnOrder = OnReturnOrder;
            this.currentMarket.MarketTrader.OnReturnTrade = OnReturnTrade;
            this.currentMarket.MarketTrader.OnReturnInvestorPosition = OnReturnInvestorPosition;
            this.currentMarket.MarketTrader.OnReturnAccount = OnReturnAccount;

            this.currentMarket.MarketData.Connect(connection);
            this.currentMarket.MarketTrader.Connect(connection);
        }

        #region delegate
        public void OnConnectionStatus_Quote(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            AppendQueryText("Quote登录：" + status);
        }

        public void OnConnectionStatus_Trade(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            AppendQueryText("Trade登录：" + status);
            if (status == ConnectionStatus.Logined)
            {
                this.currentMarket.MarketTrader.QueryAccount();
                this.currentMarket.MarketTrader.QueryInstruments();
            }
        }

        private void AppendQueryText(string text)
        {
            if (this.tbQueryInfo.InvokeRequired)
            {
                AppendQueryTextCallBack d = new AppendQueryTextCallBack(AppendQueryText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tbQueryInfo.AppendText(text + "\r\n");
            }
        }
        delegate void AppendQueryTextCallBack(string text);

        public void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            //MessageBox.Show("111");
            int index = FindObjectIndexFromDataTable(dtSubscribe, marketData.Code, 0);
            if (index < 0)
            {
                //MessageBox.Show(marketData.ToString());
                dtSubscribe.Rows.Add(new object[] {
                    marketData.Code, marketData.Price, marketData.BuyPrice, marketData.SellPrice, marketData.BuyMount, marketData.SellMount, marketData.Mount, marketData.Hold });
                //dtSubscribe.Rows.Add(new object[] { marketData.Code, marketData.Price, marketData.BuyPrice, marketData.SellPrice, marketData.BuyMount, marketData.SellMount });
            }
            else
            {
                DataRow row = dtSubscribe.Rows[index];
                row[0] = marketData.Code;
                row[1] = marketData.Price;
                row[2] = marketData.BuyPrice;
                row[3] = marketData.SellPrice;
                row[4] = marketData.BuyMount;
                row[5] = marketData.SellMount;
                row[6] = marketData.Mount;
                row[7] = marketData.Hold;
                gridSubscribe.DataSource = dtSubscribe;
            }
        }

        private int FindObjectIndexFromDataTable(DataTable dt, object obj, int columnIndex)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object rowobj = dt.Rows[i][columnIndex];
                if (rowobj == null)
                    continue;
                if (rowobj == obj)
                    return i;
            }
            return -1;
        }


        public void OnReturnInvestorPosition(object sender, ref PositionInfo position)
        {
            int index = FindObjectIndexFromDataTable(dtPosition, position.InstrumentID, 0);
            if (index < 0)
            {
                //MessageBox.Show(marketData.ToString());
                //dtSubscribe.Rows.Add(new object[] { marketData.Code, marketData.Price, marketData.BuyPrice, marketData.SellPrice, marketData.BuyMount, marketData.SellMount, marketData.Mount, marketData.Hold });
                //dtSubscribe.Rows.Add(new object[] { marketData.Code, marketData.Price, marketData.BuyPrice, marketData.SellPrice, marketData.BuyMount, marketData.SellMount });
            }
            else
            {
                //DataRow row = dtSubscribe.Rows[index];
                //row[0] = marketData.Code;
                //row[1] = marketData.Price;
                //row[2] = marketData.BuyPrice;
                //row[3] = marketData.SellPrice;
                //row[4] = marketData.BuyMount;
                //row[5] = marketData.SellMount;
                //row[6] = marketData.Mount;
                //row[7] = marketData.Hold;
                //gridSubscribe.DataSource = dtSubscribe;
            }
        }

        public void OnReturnInstrument(object sender, ref List<plugin.market.InstrumentInfo> instruments)
        {

        }

        public void OnReturnOrder(object sender, ref OrderInfo order)
        {

        }

        /// <summary>
        /// 返回成交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="trade"></param>
        public void OnReturnTrade(object sender, ref TradeInfo trade)
        {

        }

        private void OnReturnAccount(object sender, ref AccountInfo trade)
        {
        }

        #endregion

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            this.cbMarket.Enabled = true;
            this.cbConnections.Enabled = true;
            DisConnect();
        }

        private void DisConnect()
        {
            if (this.currentMarket != null)
            {
                this.currentMarket.MarketData.DisConnect();
                this.currentMarket.MarketTrader.DisConnect();
                this.currentMarket = null;
            }
        }

        private void btSubscribe_Click(object sender, EventArgs e)
        {
            string[] codes = tbSubscribeCode.Text.Split(',');
            this.currentMarket.MarketData.Subscribe(codes);
        }

        private void btBuy_Click(object sender, EventArgs e)
        {

        }

        private void btSell_Click(object sender, EventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {

        }

        private void gridMarketData_SelectionChanged(object sender, EventArgs e)
        {
            int selectedIndex = gridSubscribe.SelectedRows[0].Index;
            if (selectedIndex >= dtSubscribe.Rows.Count)
                return;
            DataRow row = dtSubscribe.Rows[selectedIndex];
            tbCode.Text = row[0].ToString();
            tbPrice.Text = row[1].ToString();
        }
    }
}
