using com.wer.sc.data.market.receiver;
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
    public partial class FormTestTragger_Writer : Form
    {
        private IMarket market;

        private ConnectionInfo conn;

        private string[] instruments;

        public FormTestTragger_Writer()
        {
            InitializeComponent();
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

        private void btWrite_Click(object sender, EventArgs e)
        {
            MarketFactory fac = new MarketFactory(MarketType.CnFutures);
            IMarket market = fac.CreateMarket();
            this.conn = GetConnection(fac);
            this.market = market;

            //自动写入数据
            MarketDataReceiveTragger_TickWriter tragger_Writer = new MarketDataReceiveTragger_TickWriter(tbPath.Text, 30);
            market.MarketData.Traggers.Add(tragger_Writer);
            market.MarketTrader.ConnectionStatusChanged += MarketTrader_ConnectionStatusChanged;
            market.MarketTrader.InstrumentsReturned += MarketTrader_InstrumentsReturned;

            ConnectionInfo conn = GetConnection(fac);
            market.MarketData.Connect(conn);
            market.MarketTrader.Connect(conn);
        }

        private void MarketTrader_InstrumentsReturned(object sender, ref List<InstrumentInfo> instruments)
        {
            //IMarketData market = (IMarketData)sender;
            this.instruments = new string[instruments.Count];
            for (int i = 0; i < instruments.Count; i++)
            {
                this.instruments[i] = instruments[i].Symbol;
                AppendQueryText(this.instruments[i]);
            }
            this.market.MarketData.Subscribe(this.instruments);
        }

        private void MarketTrader_ConnectionStatusChanged(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            AppendQueryText("连接交易服务器" + conn.Name + ":" + status);
            if (status == ConnectionStatus.Logined)
            {
                //获得所有instruments
                market.MarketTrader.QueryInstruments();
            }                       
        }

        private void btStopWrite_Click(object sender, EventArgs e)
        {
            market.MarketData.DisConnect();
            market.MarketTrader.DisConnect();
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
    }
}