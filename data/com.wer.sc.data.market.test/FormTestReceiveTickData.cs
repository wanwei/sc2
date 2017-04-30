using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.market
{
    public partial class FormTestReceiveTickData : Form
    {
        private IMarketData marketData;
        private ConnectionInfo conn;

        public FormTestReceiveTickData()
        {
            InitializeComponent();
        }

        private void btSubscribe_Click(object sender, EventArgs e)
        {
            marketData.Subscribe(tbSubscribeCode.Text.Split(','));
        }

        private void btUnSubscribe_Click(object sender, EventArgs e)
        {
            marketData.UnSubscribe(tbSubscribeCode.Text.Split(','));
        }

        private void AppendQueryText(string text)
        {
            if (this.tbTickReceived.InvokeRequired)
            {
                AppendQueryTextCallBack d = new AppendQueryTextCallBack(AppendQueryText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tbTickReceived.AppendText(text + "\r\n");
            }
        }

        delegate void AppendQueryTextCallBack(string text);

        private void btConnect_Click(object sender, EventArgs e)
        {
            MarketFactory fac = new MarketFactory(MarketType.CnFutures);
            IMarket market = fac.CreateMarket();
            this.conn = GetConnection(fac);
            this.marketData = market.MarketData;
            market.MarketData.Connect(conn);
            market.MarketData.ConnectionStatusChanged += MarketData_ConnectionStatusChanged;
            market.MarketData.DataReceived += MarketData_DataReceived;
        }

        private void MarketData_DataReceived(object sender, ITickData tickData)
        {
            AppendQueryText(tickData.ToString());
        }

        private void MarketData_ConnectionStatusChanged(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            AppendQueryText("连接服务器" + conn.Name + ":" + status);
        }

        public static ConnectionInfo GetConnection(MarketFactory fac)
        {
            List<ConnectionInfo> conns = fac.GetMarketDataConnections();

            ConnectionInfo testConn = conns[0];
            for (int i = 1; i < conns.Count; i++)
            {
                ConnectionInfo conn = conns[i];
                //if (conn.Id == "SIMNOWMOCK")
                if (conn.Id == "SIMNOW2")
                    testConn = conn;
            }
            return testConn;
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            marketData.DisConnect();
        }
    }
}