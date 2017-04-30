using com.wer.sc.data.datacenter;
using com.wer.sc.data.market;
using com.wer.sc.data.market.receiver;
using com.wer.sc.plugin;
using com.wer.sc.utils;
using com.wer.sc.utils.attr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.receiver
{
    public partial class FormReceiver : Form
    {
        private RealDataReceiver realDataReceiver;

        private Dictionary<MarketType, MarketFactory> dic_MarketType_MarketFactory = new Dictionary<MarketType, MarketFactory>();

        public FormReceiver()
        {
            InitializeComponent();

            InitCbMarket();
        }

        private MarketFactory GetMarketFactory(MarketType marketType)
        {
            if (dic_MarketType_MarketFactory.ContainsKey(marketType))
                return dic_MarketType_MarketFactory[marketType];
            MarketFactory fac = new MarketFactory(marketType);
            dic_MarketType_MarketFactory.Add(marketType, fac);
            return fac;
        }

        private void InitCbMarket()
        {
            Array arr = Enum.GetValues(typeof(MarketType));
            ItemObject[] items = new ItemObject[arr.Length];
            int currentIndex = 0;
            foreach (MarketType item in Enum.GetValues(typeof(MarketType)))
            {
                string remark = RemarkExtend.GetRemark(item);
                ItemObject obj = new ItemObject(remark, item);
                items[currentIndex] = obj;
                currentIndex++;
            }
            cbMarket.Items.AddRange(items);
        }

        private void cbMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarketType selectedMarketType = GetMarketType();
            MarketFactory fac = GetMarketFactory(selectedMarketType);
            InitConnections(fac);
            InitCbDataCenter(selectedMarketType);
        }

        private void InitConnections(MarketFactory fac)
        {
            List<ConnectionInfo> marketDataConns = fac.GetMarketDataConnections();
            BindConnectionsToCombobox(cbMarketDataConnectionInfo, marketDataConns);
            List<ConnectionInfo> marketTraderConns = fac.GetTraderConnections();
            BindConnectionsToCombobox(cbMarketTraderConnectionInfo, marketTraderConns);
        }

        private void BindConnectionsToCombobox(ComboBox cb, List<ConnectionInfo> conns)
        {
            cb.Items.Clear();
            for (int i = 0; i < conns.Count; i++)
            {
                ConnectionInfo conn = conns[i];
                cb.Items.Add(new ItemObject(conn.Name, conn));
            }
        }

        private void InitCbDataCenter(MarketType marketType)
        {
            List<DataCenterConfig> dataCenterConfigs = DataCenterManager.Instance.GetConfigs(marketType);
            for (int i = 0; i < dataCenterConfigs.Count; i++)
            {
                DataCenterConfig config = dataCenterConfigs[i];
                //cbDataCenter.Items.Add(new ItemObject(config.Uri, config));
            }
        }

        private MarketType GetMarketType()
        {
            ItemObject itemobj = (ItemObject)cbMarket.SelectedItem;
            return (MarketType)itemobj.Value;
        }

        private ConnectionInfo GetMarketDataConnection()
        {
            ItemObject itemObj = (ItemObject)cbMarketDataConnectionInfo.SelectedItem;
            return (ConnectionInfo)itemObj.Value;
        }

        private ConnectionInfo GetMarketTraderConnection()
        {
            ItemObject itemObj = (ItemObject)cbMarketTraderConnectionInfo.SelectedItem;
            return (ConnectionInfo)itemObj.Value;
        }

        private string GetDataPath()
        {
            return cbDataPath.Text;
        }

        private DataCenterConfig GetDataCenterConfig()
        {
            return null;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            this.realDataReceiver = new RealDataReceiver(GetMarketType(), GetMarketDataConnection(), GetMarketTraderConnection());
            this.realDataReceiver.EnableDataPersistent(GetDataPath(), 200);
            this.realDataReceiver.Start();
        }
    }
}
