using com.wer.sc.data.datacenter;
using com.wer.sc.data.market.receiver;
using com.wer.sc.data.reader;
using com.wer.sc.mockdata;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.market
{
    public partial class FormTestRealTimeDataBuilder : Form
    {
        private IMarketData marketData;
        private ConnectionInfo conn;

        public FormTestRealTimeDataBuilder()
        {
            InitializeComponent();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            MarketFactory fac = new MarketFactory(MarketType.CnFutures);
            IMarket market = fac.CreateMarket();
            this.conn = GetConnection(fac);
            this.marketData = market.MarketData;

            string configPath = TestCaseManager.GetTestCasePath(GetType(), "datacenter.config");
            DataCenter dc = DataCenterManager.Create(configPath).GetDataCenter("file:/e:/FUTURES/MOCKDATACENTER/");

            IDataReader dataReader = dc.DataReader;
            List<KLinePeriod> klinePeriods = dc.Config.StoredDataTypes.StoreKLinePeriods;
            MarketDataReceiveTragger_RealTimeBuilder tragger = new MarketDataReceiveTragger_RealTimeBuilder(dataReader, klinePeriods);
            tragger.RealTimeDataChanged += Tragger_RealTimeDataChanged;

            this.marketData.Traggers.Add(tragger);

            market.MarketData.Connect(conn);
            //market.MarketData.ConnectionStatusChanged += MarketData_ConnectionStatusChanged;
            //market.MarketData.DataReceived += MarketData_DataReceived;
        }

        private void Tragger_RealTimeDataChanged(object sender, IRealTimeDataReader realTimeDataReader)
        {
            AppendTextBox(tbKLineData, realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Day).ToString());
            AppendTextBox(tbKLineData, realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Hour).ToString());
            AppendTextBox(tbKLineData, realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_15Minute).ToString());
            AppendTextBox(tbKLineData, realTimeDataReader.GetKLineData(KLinePeriod.KLinePeriod_1Minute).ToString());

            AppendTextBox(tbTimeLineData, realTimeDataReader.GetTimeLineData().ToString());
        }

        private void AppendTextBox(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                AppendQueryTextCallBack d = new AppendQueryTextCallBack(AppendTextBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                textBox.AppendText(text + "\r\n");
            }
        }

        delegate void AppendQueryTextCallBack(TextBox textBox, string text);

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

        }

        private void btSubscribe_Click(object sender, EventArgs e)
        {
            marketData.Subscribe(tbSubscribeCode.Text.Split(','));
        }

        private void btUnSubscribe_Click(object sender, EventArgs e)
        {
            marketData.UnSubscribe(tbSubscribeCode.Text.Split(','));
        }

    }
}
